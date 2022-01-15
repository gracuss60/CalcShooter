using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    /// <summary>
    ///  obsługia poziomów trudności
    /// </summary>
    internal class DiffState : Component 
    {
        private Texture2D lossBackground; // tło
        private SpriteFont font; //czcionka
        //przyciski
        private Button easyButton;
        private Button mediumButton;
        private Button hardButton;
        private ContentManager _content;

        /// <summary>
        /// Rysowanie przycisków oraz tła
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(lossBackground, new Vector2(0, 0), Color.White);
            easyButton.Draw(gameTime, spriteBatch);
            mediumButton.Draw(gameTime, spriteBatch);
            hardButton.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        ///załadowanie przycisków, tła, czcionki
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content) 
        {
            this._content = Content;
            lossBackground = Content.Load<Texture2D>("Sprites/backgroundDiff");
            font = Content.Load<SpriteFont>("fonts/myFont");
            easyButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 100),
                ButtonText = "1",
            };
            easyButton.Click += easyButton_Click;

            mediumButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 300),
                ButtonText = "2",
            };
            mediumButton.Click += mediumButton_Click;
            hardButton = new Button(Content.Load<Texture2D>("Controls/textBox"), font)
            {
                Position = new Vector2(1280 / 2 - 500 / 2, 500),
                ButtonText = "3",
            };
            hardButton.Click += hardButton_Click;
        }
        /// <summary>
        ///aktualizacja obsługi przycisków
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) 
        {
            easyButton.Update(gameTime);
            mediumButton.Update(gameTime);
            hardButton.Update(gameTime);
        }

        /// <summary>
        /// metoda wciśnięcia przycisku, ustawienie parametrów wg wybranego poziomu trudności
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void easyButton_Click(object sender, System.EventArgs e) 
        {
            GameStateManager.game = new GameState(); ///generowanie nowego obiektu gry do poprawnego restartu rozgrywki po przegraniu//wygraniu i ponownym rozpoczeciu.
            GameStateManager.game.LoadContent(_content);
            Equation.velocity = 0.4f; // prędkośc lotu
            GameState.HowMany = 10; // ilość
            GameState.rownania.Clear(); // reset listy obiektów w przypadku ponownej rozgrywki
            GameState.lives = 5; // życia
            for (int i = 0; i < 10; i++) // generowanie listy obiektów równan z ich parametrami generowanymi w klasie Equation
            {
                GameState.rownania.Add(new Equation());
            }
            Data.CurrentState = Data.States.game;
        }

        /// <summary>
        /// ponownie, reset stanu gry, ustawienie parametrów rozgrywki, generacja listy obiektów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mediumButton_Click(object sender, System.EventArgs e) 
        {
            GameStateManager.game = new GameState();
            GameStateManager.game.LoadContent(_content);
            Equation.velocity = 0.6f;
            GameState.HowMany = 15;
            GameState.rownania.Clear();
            GameState.lives = 4;
            for (int i = 0; i < 15; i++)
            {
                GameState.rownania.Add(new Equation());
            }
            Data.CurrentState = Data.States.game;
        }
        /// <summary>
        /// Raz jeszcze, reset stanu gry, ustawienie parametrów rozgrywki adekwatne do poziomu trudności, generacja listy obiektów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void hardButton_Click(object sender, System.EventArgs e)
        {
            GameStateManager.game = new GameState();
            GameStateManager.game.LoadContent(_content);
            Equation.velocity = 1;
            GameState.HowMany = 20;
            GameState.rownania.Clear();
            GameState.lives = 3;
            for (int i = 0; i < 20; i++)
            {
                GameState.rownania.Add(new Equation());
            }
            Data.CurrentState = Data.States.game;
        }
    }
}