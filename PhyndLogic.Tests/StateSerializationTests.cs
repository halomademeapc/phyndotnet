using PhyndData;
using Xunit;

namespace PhyndLogic.Tests
{
    public class StateSerializationTests
    {
        [Fact]
        public void ShouldDeserialize()
        {
            var s = new State("XOO---XXO");
            var target = new Player?[9]
            {
                Player.Computer, Player.Human, Player.Human,
                null, null, null,
                Player.Computer, Player.Computer, Player.Human
            };
            Assert.Equal(target, s.Positions);
        }

        [Fact]
        public void ShouldSerialize()
        {
            var s = new State(new Player?[9]{
                null, Player.Human, Player.Computer,
                null, null, Player.Computer,
                Player.Human, null, null
            });
            Assert.Equal("-OX--XO--", s.ToString());
        }
    }
}
