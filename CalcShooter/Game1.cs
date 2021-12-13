using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CalcShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // private GameStates _gameState;
        public const int windowWidth = 1280;

        public const int windowHeight = 720;
        private Texture2D backgroundSprite;// background screen
        private Texture2D tankSprite; // our artillery
        private float tankX = 120;
        private float tankY = 120;
        public Vector2 _position;
        private SpriteFont font;

        public KeyboardState keyState, previousKeyState;

        //bool keyReleased = true; //keyboard input
        //int score = 0; // potential score
        private Equation rownanie1 = new Equation();

        private Equation rownanie2 = new Equation();
        private Equation rownanie3 = new Equation();

        public enum GameStates
        {
            Menu,
            Playing,
            Paused
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /* public bool NewKey(Keys key)
         {
             return keyState.IsKeyDown(key) && previousKeyState.IsKeyUp(key);
         }*/

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges(); //window resolution settings
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundSprite = Content.Load<Texture2D>("Sprites/background");
            tankSprite = Content.Load<Texture2D>("Sprites/tankSmall");//120x120
            font = Content.Load<SpriteFont>("fonts/LightFont");

            _position = rownanie1.position;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //   Exit();

            // TODO: Add your update logic here
            previousKeyState = keyState;
            keyState = Keyboard.GetState();

            rownanie1.Update();
            rownanie2.Update();
            rownanie3.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);

            rownanie1.Draw(_spriteBatch, font);
            rownanie2.Draw(_spriteBatch, font);
            rownanie3.Draw(_spriteBatch, font);
            _spriteBatch.Draw(tankSprite, new Vector2(windowWidth / 2 - tankSprite.Width / 2, windowHeight / 2 - tankSprite.Height / 2), Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}