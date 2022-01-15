using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    /// <summary>
    ///  ekran instrukcji
    /// </summary>
    internal class InstructionState : Component 
    {
        private Texture2D backgroundSprite; // tło
        private SpriteFont font; // czcionka
        private Button instructionButton; // przycisk

        public override void LoadContent(ContentManager Content)
        {
            backgroundSprite = Content.Load<Texture2D>("Sprites/backgroundInstruction");
            font = Content.Load<SpriteFont>("fonts/myFont");
            instructionButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 600),
                ButtonText = "menu",
            };
            instructionButton.Click += menuButton_Click;
        }
        /// <summary>
        ///  rysowanie tła oraz przycisku powrotu
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);

            instructionButton.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        ///  aktualizacja przycisku
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) 
        {
            instructionButton.Update(gameTime);
        }

        /// <summary>
        ///  po wciśnięciu przycisku: zmiana stanu gry na menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuButton_Click(object sender, System.EventArgs e) 
        {
            Data.CurrentState = Data.States.menu;
        }
    }
}