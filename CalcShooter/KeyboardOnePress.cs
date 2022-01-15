using Microsoft.Xna.Framework.Input;

namespace CalcShooter
{
    /// <summary>
    /// obsługa klawiatury, dokładniej wpisywania pojedyńczych liter przy jednokrotnym przycisku.
    /// </summary>
    public class KeyboardOnePress 
    {
        private static KeyboardState currentKeyState;
        private static KeyboardState previousKeyState;

        public KeyboardOnePress()
        {
        }

        /// <summary>
        /// stan klawiatury.
        /// </summary>
        /// <returns></returns>
        public static KeyboardState GetState() 
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        /// <summary>
        /// przytrzymanie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsPressed(Keys key) 
        {
            return currentKeyState.IsKeyDown(key);
        }

        /// <summary>
        ///  pojedyńczy przycisk, rejestr pojedyńczego wciśnięcia
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool HasNotBeenPressed(Keys key)
        {
            bool buff = currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
    }
}