using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhyndData;
using PhyndData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhyndLogic
{
    public class MoveService
    {
        private PhyndContext db;
        private LearningConfiguration config;
        private Random rng = new Random();

        public MoveService(PhyndContext db, IOptions<LearningConfiguration> options)
        {
            this.db = db;
            config = options.Value;
        }

        public async Task<Guid> StartGame()
        {
            var game = new Game();
            db.Games.Add(game);
            await db.SaveChangesAsync();
            return game.Id;
        }

        public async Task<State> GetGameState(Guid gameId)
        {
            var game = await GetGame(gameId);
            var state = new State();
            game.Moves.ToList().ForEach(m => state.PlayPosition(m.Player, m.Position));
            return state;
        }

        public async Task<State> Play(Guid gameId, int index)
        {
            var game = await GetGame(gameId);

            var weights = ReconstructGame(game.Moves);
            var state = weights.Any() ? new State(weights.Last().Scenario) : new State();
            var lastMove = game.Moves.OrderByDescending(m => m.Progress).FirstOrDefault();
            if (lastMove != null)
                state.PlayPosition(lastMove.Player, lastMove.Position);
            var moveCount = game.Moves.Select(m => m.Progress).DefaultIfEmpty(0).Max() + 1;

            state.PlayPosition(Player.Human, index);
            db.Moves.Add(new Move
            {
                GameId = gameId,
                Player = Player.Human,
                Position = index,
                Progress = moveCount
            });

            if (!state.ShouldEnd())
            {
                var compMove = await GetNextMove(state);
                state.PlayPosition(Player.Computer, compMove);
                db.Moves.Add(new Move
                {
                    GameId = gameId,
                    Player = Player.Computer,
                    Position = compMove,
                    Progress = ++moveCount
                });
            }

            await db.SaveChangesAsync();
            if (state.ShouldEnd())
                await HandleGameEnd(gameId, state.GetWinner() == Player.Computer);
            return state;
        }

        public async Task HandleGameEnd(Guid id, bool won)
        {
            var game = await GetGame(id);

            game.IsCompleted = true;
            game.WasWon = won;
            await db.SaveChangesAsync();

            var localWeights = ReconstructGame(game.Moves);
            foreach (var weight in localWeights)
            {
                var comp = await db.Weights
                    .Where(w => w.Scenario == weight.Scenario)
                    .FirstOrDefaultAsync(w => w.NextMove == weight.NextMove);

                if (comp != null)
                {
                    ApplyWeightAdjustment(comp, won);
                    await db.SaveChangesAsync();
                }
            }
        }

        private async Task<int> GetNextMove(State s)
        {
            var normalized = s.Normalize();
            var translatedState = new State(normalized.Select(n => n.Player));
            await EnsureScenariosCreated(translatedState);

            var moves = await GetAvailableMoves(translatedState)
                .AsNoTracking()
                .ToListAsync();
            var translatedMove = SelectMove(moves).NextMove;
            return normalized[translatedMove].OriginalIndex;
        }

        private async Task<Game> GetGame(Guid id)
        {
            return await db.Games
                .Include(g => g.Moves)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        private async Task EnsureScenariosCreated(State s)
        {
            var existingWeights = await GetAvailableMoves(s)
                .Select(w => w.NextMove)
                .ToListAsync();

            var newWeights = s.AvailableIndices
                .Where(i => !existingWeights.Contains(i))
                .Select(i => new Weight
                {
                    Scenario = s.ToString(),
                    NextMove = i,
                    Attempts = 0,
                    Rank = config.Seed
                });

            db.Weights.AddRange(newWeights);
            await db.SaveChangesAsync();
        }

        private void ApplyWeightAdjustment(Weight w, bool isWin)
        {
            w.Attempts++;
            if (isWin)
                w.Rank += config.RewardMultiplier * 2 / w.Attempts;
            else
            {
                var decrease = w.Rank - (config.PenaltyMultiplier * 2 / w.Attempts);
                w.Rank = decrease > 0 ? decrease : 0;
            }
        }

        private IEnumerable<Weight> ReconstructGame(IEnumerable<Move> moves)
        {
            var board = new State();
            var result = new List<Weight>();
            Move previousMove = null;
            string previousScenario = null;
            foreach (var move in moves.OrderBy(m => m.Progress))
            {
                previousScenario = board.ToString();
                board.PlayPosition(move.Player, move.Position);
                result.Add(new Weight
                {
                    NextMove = move.Position,
                    Scenario = previousScenario
                });

                previousMove = move;
            }
            return result;
        }

        private IQueryable<Weight> GetAvailableMoves(State s) => db.Weights
            .Where(w => w.Scenario == s.ToString())
            .Where(w => s.AvailableIndices.Contains(w.NextMove));

        private Weight SelectMove(IEnumerable<Weight> options)
        {
            var max = options.Sum(m => m.Rank);
            var target = rng.NextDouble() * max;

            float progress = 0;
            foreach (var o in options)
            {
                progress += o.Rank;
                if (progress >= target)
                    return o;
            }
            return options.OrderByDescending(o => o.Rank).First();
        }
    }
}
