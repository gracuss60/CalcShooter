using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CalcShooter
{
    /// <summary>
    ///  obsługa przycisków w menu i posczególnych stanach rozgrywki
    /// </summary>
    public class Button 
    {
        //parametry , czcionka, tekstura, stan myszy oraz  bool najechania na przycisk.
        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;

        public event EventHandler Click;

        public bool Clicked { get; private set; }
        public Vector2 Position { get; set; }

        public Color penColour { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string ButtonText { get; set; }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            penColour = Color.Black;
        }
        /// <summary>
        ///  grafika przycisku, jego tekstu oraz animacja najechania na przycisk.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            var colour = Color.White;
            if (_isHovering)
                colour = Color.Gray;
            spriteBatch.Draw(_texture, Rectangle, colour);
            if (!string.IsNullOrEmpty(ButtonText))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(ButtonText).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(ButtonText).Y / 2);
                spriteBatch.DrawString(_font, ButtonText, new Vector2(x, y), penColour);
            }
        }
        /// <summary>
        ///  aktualizacja obsługi przycisku po jego wciśnięciu jak i najechaniu na niego.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime) 
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;
            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}