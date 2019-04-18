using System.Linq;
using Xunit;

namespace PhyndLogic.Tests
{
    public class StateTranslationTests
    {
        [Fact]
        public void ShouldFlipHorizontally()
        {
            var s = new State("XOO-X-OX-");
            var flipped = new State(s.FlipX(s.GetTranslatedPositions()).Select(f => f.Player));
            Assert.Equal("OOX-X--XO", flipped.ToString());
        }

        [Fact]
        public void ShouldFlipVertically()
        {
            var s = new State("XOO-X-OX-");
            var flipped = new State(s.FlipY(s.GetTranslatedPositions()).Select(f => f.Player));
            Assert.Equal("OX--X-XOO", flipped.ToString());
        }

        [Fact]
        public void ShouldRotate()
        {
            var s = new State("XOO-X-OX-");
            var flipped = new State(s.Rotate(s.GetTranslatedPositions()).Select(f => f.Player));
            Assert.Equal("O--OXXX-O", flipped.ToString());
        }
    }
}
