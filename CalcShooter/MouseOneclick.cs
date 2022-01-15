using Microsoft.Xna.Framework.Input;

namespace CalcShooter
{
    /// <summary>
    /// obsługa myszy textboxa 

    /// </summary>
    public class MouseOneclick     {
        private static MouseState currentMouseState;
        private static MouseState previousMouseState;

        public MouseOneclick()
        {
        }
        /// <summary>
        /// obsługa myszy, 
        /// </summary>
        /// <returns></returns>
        public static MouseState GetState() 
        {
            previousMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            return currentMouseState;
        }
        /// <summary>
        ///  przytrzymanie przycisku
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool IsPressed(bool left) 
        {
            if (left)
                return currentMouseState.LeftButton == ButtonState.Pressed;
            else
                return currentMouseState.RightButton == ButtonState.Pressed;
        }
        /// <summary>
        ///  pojedyńczy przycisk
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static bool HasNotBeenPressed(bool left) 
        {
            if (left)
                return currentMouseState.LeftButton == ButtonState.Pressed && !(previousMouseState.LeftButton == ButtonState.Pressed);
            else
                return currentMouseState.RightButton == ButtonState.Pressed && !(previousMouseState.RightButton == ButtonState.Pressed);
        }
    }
}