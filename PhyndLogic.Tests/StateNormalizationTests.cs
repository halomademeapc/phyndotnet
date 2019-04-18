using System.Linq;
using Xunit;

namespace PhyndLogic.Tests
{
    public class StateNormalizationTests
    {
        [Fact]
        public void ShouldNormalize()
        {
            var stateA = new State(new State("OX-XO-XX-").Normalize().Select(i => i.Player));
            var stateB = new State(new State("---XOXXXO").Normalize().Select(i => i.Player));
            Assert.Equal(stateA.ToString(), stateB.ToString());
        }
    }
}
