namespace PhyndData
{
    public static class Extensions
    {
        public static char ToChar(this Player player)
        {
            switch (player)
            {
                case Player.Computer:
                    return State.COMPUTER_SYMBOL;
                case Player.Human:
                    return State.HUMAN_SYMBOL;
                default:
                    return State.NULL_SYMBOL;
            }
        }
    }
}
