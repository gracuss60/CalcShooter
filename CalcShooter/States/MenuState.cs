using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    /// <summary>
    /// Stan Menu
    /// </summary>
    public class MenuState : Component 
    {
        private Texture2D menuBackground;// tło
        private SpriteFont font;//czcionka
        private Button gameButton;// przyciski wyjścia, instrukcji oraz poziomu trudności.
        private Button quitButton;
        private Button instructionButton;
        private Texture2D instruction;

        /// <summary>
        ///  załadowanie parametrów przycisku, tła, czcionki
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content) 
        {
            menuBackground = Content.Load<Texture2D>("Sprites/backgroundMenu");
            instruction = Content.Load<Texture2D>("Sprites/instruction");
            font = Content.Load<SpriteFont>("fonts/myFont");
            gameButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 100),
                ButtonText = "graj",
            };
            gameButton.Click += gameButton_Click;

            instructionButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 300),
                ButtonText = "",
            };
            instructionButton.Click += instructionButton_Click;
            quitButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 500),
                ButtonText = "wyjdz",
            };
            quitButton.Click += quitButton_Click;
        }

        /// <summary>
        ///   rysowanie  tła, przycisków
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spritebatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spritebatch) 
        {
            spritebatch.Draw(menuBackground, new Vector2(0, 0), Color.White);
            quitButton.Draw(gameTime, spritebatch);
            instructionButton.Draw(gameTime, spritebatch);
            spritebatch.Draw(instruction, new Vector2(1280 / 2 - 500 / 2, 300), Color.White);
            gameButton.Draw(gameTime, spritebatch);
        }

        /// <summary>
        ///  aktualizacja stanów przycisków.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) 
        {
            quitButton.Update(gameTime);
            instructionButton.Update(gameTime);
            gameButton.Update(gameTime);
        }

        /// <summary>
        ///  przejście do poszczególnych poziomów w zależności od tego który przycisk został wciśnięty, kolejno:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void gameButton_Click(object sender, System.EventArgs e) 
        {
            Data.CurrentState = Data.States.diff; //gra- poziomy trudności
        }

        /// <summary>
        ///  wyjście z gry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void quitButton_Click(object sender, System.EventArgs e)
        {
            Data.Exit = true; 
        }
        /// <summary>
        ///  Stan instrukcji
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void instructionButton_Click(object sender, System.EventArgs e)
        {
            Data.CurrentState = Data.States.instruction; 
        }
    }
}