using PhyndData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhyndLogic
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
            serialized = serialized.ToUpper();
            if (serialized.Length != SIDE_SIZE * SIDE_SIZE)
            {
                throw new FormatException();
            }

            for (int i = 0; i < serialized.Length; i++)
            {
                var xIndex = i % SIDE_SIZE;
                var yIndex = i / SIDE_SIZE;
                var player = CharToPlayer(serialized[i]);
                if (player.HasValue)
                {
                    PlayPosition(player.Value, xIndex, yIndex);
                }
            }
        }

        private Player?[] _Positions { get; set; } = new Player?[SIDE_SIZE * SIDE_SIZE];

        public Player?[] Positions => _Positions;

        public void PlayPosition(Player player, int x, int y) => PlayPosition(player, new Coordinate(x, y));

        public void PlayPosition(Player player, Coordinate coord) => _Positions[CoordinateToIndex(coord)] = player;

        public override string ToString() => string.Concat(_Positions.Select(PlayerToChar));

        public IEnumerable<Coordinate> AvailableCoordinates => AvailableIndices.Select(IndexToCoordinate);

        public IEnumerable<int> AvailableIndices => _Positions.Zip(Enumerable.Range(0, _Positions.Count()), (pos, index) => new
        {
            Position = pos,
            Index = index
        }).Where(c => !c.Position.HasValue)
        .Select(c => c.Index);


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

        private int CoordinateToIndex(Coordinate c) => c.y * SIDE_SIZE + c.x;

        private Coordinate IndexToCoordinate(int i) => new Coordinate
        {
            x = i % SIDE_SIZE,
            y = i / SIDE_SIZE
        };
    }
}
