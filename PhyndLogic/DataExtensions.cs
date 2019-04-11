using PhyndData.Entities;

namespace PhyndLogic
{
    public static class DataExtensions
    {
        public static State GetState(this Weight w) => new State(w.Scenario);
    }
}
