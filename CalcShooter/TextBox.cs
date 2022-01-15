using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CalcShooter
{
    public class TextBox
    {
        //Zmienne dotyczące ustawień textboxa, text,jego pozycja, wyświetlacz znaku | oraz elementy graficzne, czcionak.
        public static string CurrentText { get; set; }
        public Vector2 CurrentTextPosition { get; set; }
        public Vector2 CursorPosition { get; set; }
        public int AnimationTime { get; set; }
        public bool Visible { get; set; }
        public float LayerDepth { get; set; }
        public Vector2 Position { get; set; }
        public bool Selected { get; set; }
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }
        private int _cursorWidth;
        private int _cursorHeight;
        private int _length;
        private Texture2D _texture;
        private Texture2D _cursorTexture;
        private Point _cursorDimensions;
        private SpriteFont _font;

        /// <summary>
        /// konstruktor z wyżej wymienionymi parametrami.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="cursorTexture"></param>
        /// <param name="dimensions"></param>
        /// <param name="cursorDimensions"></param>
        /// <param name="position"></param>
        /// <param name="length"></param>
        /// <param name="visible"></param>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="LayerDepth"></param>
        public TextBox(Texture2D texture, Texture2D cursorTexture, Point dimensions, Point cursorDimensions, Vector2 position, int length, bool visible, SpriteFont font, string text, float LayerDepth)
        {
            
            _texture = texture;
            CellWidth = dimensions.X;
            CellHeight = dimensions.Y;
            _cursorWidth = cursorDimensions.X;
            _cursorHeight = cursorDimensions.Y;
            _length = length;
            AnimationTime = 0;
            Visible = visible;
            this.LayerDepth = LayerDepth;
            Position = position;
            CursorPosition = new Vector2(position.X + 7, position.Y + 6);
            CurrentTextPosition = new Vector2(position.X + 7, position.Y + 3);
            CurrentText = String.Empty;
            _cursorTexture = cursorTexture;
            _cursorDimensions = cursorDimensions;
            Selected = false;
            _font = font;
            CurrentText = text;
        }

        /// <summary>
        ///  aktualizacja animacji znaku |
        /// </summary>
        public void Update()
        {
            AnimationTime++;
        }

        /// <summary>
        /// animacja wyświetlania znaku wpisywania 
        /// </summary>
        /// <returns></returns>
        public bool IsFlashingCUrsorVisible() 
        {
            int time = AnimationTime % 60;
            if (time >= 0 && time < 31)
                return true;
            else
                return false;
        }

        /// <summary>
        /// implementacja tekstu do pola tekstowego za pomocą klawiatury
        /// </summary>
        /// <param name="text"></param>
        public void AddMoreText(char text)
        {
            Vector2 spacing = new Vector2(); // pozycje
            KeyboardState keyboardState = KeyboardOnePress.GetState(); // stan klawiatury
            bool lowerThisCharacter = true; // małe litery
            if (keyboardState.CapsLock || keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) //  duża litera
                lowerThisCharacter = false;
            if ((keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))) // gdy wciśnięta 8 wraz z shiftem - input znaku iloczynu.
            {
                int TextToInt = (int)Char.GetNumericValue(text);
                if (text == '8')
                {
                    text = (char)(text - 14);
                }
            }

            if (text != '\b') 
            {
                if (CurrentText.Length < _length) // przypisanie zmiennym faktycznie wpisywanie znaków oraz ich pozycji w textbox'ie
                {
                    if (lowerThisCharacter)
                        text = Char.ToLower(text);
                    CurrentText += text; // dodawania kolejnych wciśniętych liter.
                    spacing = _font.MeasureString(text.ToString());// dzięki temu kursor wie jak daleko ma się odsunąc w zależności od wpisanej litery/cyfry
                    CursorPosition = new Vector2(CursorPosition.X + spacing.X, CursorPosition.Y);
                }
            }
            else
            {
                if (CurrentText.Length > 0)
                {
                    spacing = _font.MeasureString(CurrentText.Substring(CurrentText.Length - 1)); //
                    CurrentText = CurrentText.Remove(CurrentText.Length - 1, 1);
                    CursorPosition = new Vector2(CursorPosition.X - spacing.X, CursorPosition.Y);
                }
            }
        }

        /// <summary>
        ///  ustawienia graficzne textboxa oraz wyświetlanie aktualnie pisanego tekstu.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Render(SpriteBatch spriteBatch) 
        {
            if (Visible)
            {
                spriteBatch.Draw(_texture, Position, Color.White);
                spriteBatch.DrawString(_font, CurrentText, CurrentTextPosition, Color.Black, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, LayerDepth);
                if (Selected && IsFlashingCUrsorVisible()) // rysuje migający kursor
                {
                    Rectangle sourceRectangle = new Rectangle(0, 0, _cursorWidth, _cursorHeight);
                    Rectangle destinationRectangle = new Rectangle((int)CursorPosition.X, (int)CursorPosition.Y, _cursorWidth, _cursorHeight);
                    spriteBatch.Draw(_cursorTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, LayerDepth);
                }
            }
        }
    }
}