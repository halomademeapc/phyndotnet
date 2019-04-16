using PhyndData;
using System.Linq;
using Xunit;

namespace PhyndLogic.Tests
{
    public class StateLogicTests
    {
        [Fact]
        public void ShouldHaveAvailableMoves()
        {
            var s = new State("-OX--XO--");
            Assert.NotEmpty(s.AvailableIndices);
            Assert.False(s.ShouldEnd());
        }

        [Fact]
        public void ShouldHaveNAvailableMoves()
        {
            var positions = new Player?[9]{
                null, Player.Human, Player.Computer,
                null, null, Player.Computer,
                Player.Human, null, null
            };
            var s = new State(positions);
            Assert.NotEmpty(s.AvailableIndices);
            Assert.Equal(positions.Count(p => !p.HasValue), s.AvailableIndices.Count());
            Assert.False(s.ShouldEnd());
        }

        [Fact]
        public void ShouldHaveNoAvailableMoves()
        {
            var s = new State("OOXXOXOXO");
            Assert.Empty(s.AvailableIndices);
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void ShouldHaveHumanWin()
        {
            var s = new State("OOXXOXOXO");
            Assert.Equal(Player.Human, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void ShouldHaveComputerWin()
        {
            var s = new State("OOXXOXOXX");
            Assert.Equal(Player.Computer, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndX1()
        {
            var s = new State("XXX-O-OO-");
            Assert.Equal(Player.Computer, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndX2()
        {
            var s = new State("-X-OOOX-X");
            Assert.Equal(Player.Human, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndX3()
        {
            var s = new State("X-OOO-XXX");
            Assert.Equal(Player.Computer, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndY1()
        {
            var s = new State("OX-O-XOX-");
            Assert.Equal(Player.Human, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndY2()
        {
            var s = new State("OX--XOOX-");
            Assert.Equal(Player.Computer, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndY3()
        {
            var s = new State("XOO-XO-XO");
            Assert.Equal(Player.Human, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndDiagA()
        {
            var s = new State("OX-XO-XXO");
            Assert.Equal(Player.Human, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndDiagB()
        {
            var s = new State("-OX-XOXO-");
            Assert.Equal(Player.Computer, s.GetWinner());
            Assert.True(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldContinue()
        {
            var s = new State("-OX-OX---");
            Assert.Null(s.GetWinner());
            Assert.False(s.ShouldEnd());
        }

        [Fact]
        public void GameShouldEndDraw()
        {
            var s = new State("XOOOXXXOO");
            Assert.Null(s.GetWinner());
            Assert.True(s.ShouldEnd());
        }
    }
}
