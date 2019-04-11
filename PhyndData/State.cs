using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhyndData
{
    public class State
    {
        const char HUMAN_SYMBOL = 'O';
        const char COMPUTER_SYMBOL = 'X';
        const char NULL_SYMBOL = '-';
        const int SIDE_SIZE = 3;

        public State() { }

        public State(string serialized)
        {
            if (serialized.Length != SIDE_SIZE * SIDE_SIZE)
                throw new FormatException();
            for (int i = 0; i < serialized.Length; i++)
            {
                var xIndex = i % SIDE_SIZE;
                var yIndex = i / SIDE_SIZE;
                var player = CharToPlayer(serialized[i]);
                if (player.HasValue)
                    PlayPosition(player.Value, xIndex, yIndex);
            }
        }

        private Player?[][] Positions { get; set; } = Enumerable.Range(SIDE_SIZE, 0).Select(i => new Player?[SIDE_SIZE]).ToArray();

        public IEnumerable<Player?> LinearizedPositions => Positions.SelectMany(p => p);

        public void PlayPosition(Player player, int x, int y) => Positions[x][y] = player;

        public override string ToString() => string.Concat(LinearizedPositions.Select(PlayerToChar));

        public State Normalize()
        {
            return this;
        }

        private char PlayerToChar(Player? player) => !player.HasValue
            ? NULL_SYMBOL
            : player.Value == Player.Computer
                ? HUMAN_SYMBOL
                : COMPUTER_SYMBOL;

        private Player? CharToPlayer(char c)
        {
            switch (c)
            {
                case HUMAN_SYMBOL:
                    return Player.Human;
                case COMPUTER_SYMBOL:
                    return Player.Computer;
                default:
                    return null;
            }
        }
    }
}
