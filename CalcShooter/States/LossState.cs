using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    /// <summary>
    ///  ekran przegranej
    /// </summary>
    public class LossState : Component 
    {
        private Texture2D backgroundSprite;// tło
        private SpriteFont font;//czcionki
        private Button playAgainButton; // przyciski , ponownej gry bądź wyjścia do menu
        private Button quitButton;

        /// <summary>
        ///  załadowanie tła,czcionki, parametrów przycisku.
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content) 
        {
            backgroundSprite = Content.Load<Texture2D>("Sprites/backgroundLoss");
            font = Content.Load<SpriteFont>("fonts/myFont");
            playAgainButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 300),
                ButtonText = "tak",
            };
            playAgainButton.Click += playAgainButton_Click;

            quitButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 500),
                ButtonText = "nie",
            };
            quitButton.Click += quitButton_Click;
        }

        /// <summary>
        ///  rysowanie grafiki, tła, przycisków
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            quitButton.Draw(gameTime, spriteBatch);
            playAgainButton.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        ///  akutalizacja przycisków
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) 
        {
            quitButton.Update(gameTime);
            playAgainButton.Update(gameTime);
        }

        /// <summary>
        ///  zerowanie clocka, punktów, przejście do wyboru poziomu trudności
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void playAgainButton_Click(object sender, System.EventArgs e) 
        {
            GameState.clock = 0;
            GameState.points = 0;
            Data.CurrentState = Data.States.diff;
        }

        /// <summary>
        ///  zerowanie clocka, punktów, przejście do menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void quitButton_Click(object sender, System.EventArgs e)
        {
            GameState.clock = 0;
            GameState.points = 0;
            Data.CurrentState = Data.States.menu;
        }
    }
}