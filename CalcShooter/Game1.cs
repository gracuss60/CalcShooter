using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    public class Game1 : Game
    {
        //specyfikacje graficzne , rozdzielczość , system poziomów
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public const int windowWidth = 1280;
        public const int windowHeight = 720;
        public object CurrentLevel { get; private set; } // aktualny poziom rozgrywki
        private GameStateManager gsm; // obsługa poziomów samej gry

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; // widocznośc myszki
        }

        /// <summary>
        ///  inicjalizacja grafiki oraz systemu obsługi poziomów.
        /// </summary>
        protected override void Initialize() 
        {
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();
            gsm = new GameStateManager();
            Data.CurrentState = Data.States.menu;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            gsm.LoadContent(Content);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        ///  aktualizacja rozgrywki, informacja zwrotna aktualnego stanu poziomu.
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime) 
        {
            gsm.Update(gameTime);
            if (Data.Exit == true)
            {
                Exit();
            }

            base.Update(gameTime);
        }
        /// <summary>
        ///  rysowanie danego poziomu rozgrywki, który aktualnie jest wykonywany za pośrednictwem gsm.Draw oraz gsm.Update
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            gsm.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}