using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    /// <summary>
    ///  stan gry po zwycięstwie 
    /// </summary>
    public class WinState : Component 
    {
        // czcionka, tło oraz przyciski
        private Texture2D backgroundSprite;
        private SpriteFont font;
        private Button playAgainButton;
        private Button quitButton;

        /// <summary>
        ///  załadowanie czcionki , przcysików wraz z ich tekstem oraz tła.
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content) 
        {
            backgroundSprite = Content.Load<Texture2D>("Sprites/backgroundWin");

            font = Content.Load<SpriteFont>("fonts/myFont");
            playAgainButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(200, 600),
                ButtonText = "tak",
            };
            playAgainButton.Click += playAgainButton_Click;

            quitButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(600, 600),
                ButtonText = "nie",
            };
            quitButton.Click += quitButton_Click;
        }
        /// <summary>
        ///  rysowane tła, przycisków, oraz czasu rogrywki wraz z zdobytymi punktami
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            quitButton.Draw(gameTime, spriteBatch);
            playAgainButton.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Zdobyte punkty: " + GameState.points.ToString(), new Vector2(400, 250), Color.Black);
            spriteBatch.DrawString(font, "Czas gry " + GameState.clock.ToString("0.0"), new Vector2(700, 250), Color.Black);
        }
        /// <summary>
        ///  aktualizacja stanów  przycisków
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) 
        {
            quitButton.Update(gameTime);
            playAgainButton.Update(gameTime);
        }

        /// <summary>
        ///  po wciśniętu przycisku play, reset clocka, punktów, przejście do ponownego poziomu trudności
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
        ///  reset clocka, punktów, zmiana poziomu do menu.       
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