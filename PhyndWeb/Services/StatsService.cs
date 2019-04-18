using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhyndData;
using PhyndLogic;
using System.Linq;
using System.Threading.Tasks;

namespace PhyndWeb.Services
{
    public class StatsService
    {
        private PhyndContext db;
        private LearningConfiguration config;

        public StatsService(PhyndContext db, IOptions<LearningConfiguration> options)
        {
            this.db = db;
            config = options.Value;
        }

        public async Task<int> GetCompletedGames() => await db.Games.CountAsync(g => g.IsCompleted);

        public async Task<int> GetWonGames() => await db.Games.CountAsync(g => g.IsCompleted && g.WasWon == true);

        public async Task<int> GetLostGames() => await db.Games.CountAsync(g => g.IsCompleted && g.WasWon == false);

        public async Task<int> GetMoveCount() => await db.Moves.CountAsync();

        public async Task<float> GetMovesPerGame()
        {
            var completed = await GetCompletedGames();
            return completed > 0
                ? (float)await db.Moves.Where(m => m.Game.IsCompleted).CountAsync() / completed
                : 0;
        }

        public async Task<int> GetUniqueScenarios() => await db.Weights.Select(w => w.Scenario).Distinct().CountAsync();

        public async Task<int> GetModifiedOutcomes() => await db.Weights.CountAsync(w => w.Rank != config.Seed);
    }
}
