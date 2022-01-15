namespace CalcShooter
{
    internal class Data
    {
        public static bool Exit { get; set; } = false;

        /// <summary>
        /// enum ze stanami rozgrywki.
        /// </summary>
        public enum States 
        {
            menu,
            game,
            diff,
            instruction,
            win,
            loss
        }

        public static States CurrentState { get; set; } = States.menu;
    }
}