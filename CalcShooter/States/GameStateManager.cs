using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    /// <summary>
    ///  manadżer wszystkich poziomów, dzięki niemu odbywa się  "komunikacja" między poszczególnymi etapami rozgrywki, tzn przejścia między nimi.
    /// </summary>
    internal class GameStateManager : Component 
    {
        //stworzenie obiektów poszczególnych poziomów menu
        public MenuState menu = new MenuState();
        public DiffState difficulty = new DiffState();
        public InstructionState instruction = new InstructionState();
        public static GameState game = new GameState();
        public WinState win = new WinState();
        public LossState loss = new LossState();

        /// <summary>
        /// Metoda, która załadowuje do programu potrzebne zasoby, np tekstury danych obiektów, czionki
        /// </summary>
        /// <param name="Content"></param>
        public override void LoadContent(ContentManager Content) 
        {
            menu.LoadContent(Content);
            difficulty.LoadContent(Content);
            instruction.LoadContent(Content);
            game.LoadContent(Content);
            win.LoadContent(Content);
            loss.LoadContent(Content);
        }
        /// <summary>
        ///  Obsługa przełączania rysunków pomiędzy konkretnymi stanami rozgrywki
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            switch (Data.CurrentState) // rysowanie poszczególnych stanów rozgrywki w zależności do tego, który aktualnie jest przechowywany w Data.CurrentState
            {
                case Data.States.menu:
                    menu.Draw(gameTime, spriteBatch);
                    break;

                case Data.States.instruction:
                    instruction.Draw(gameTime, spriteBatch);
                    break;

                case Data.States.diff:
                    difficulty.Draw(gameTime, spriteBatch);
                    break;

                case Data.States.game:
                    game.Draw(gameTime, spriteBatch);
                    break;

                case Data.States.win:
                    win.Draw(gameTime, spriteBatch);
                    break;

                case Data.States.loss:
                    loss.Draw(gameTime, spriteBatch);
                    break;
            }
        }

        /// <summary>
        ///  aktualizacja poszczególnych stanów rozgrywki w zalezności od tego który aktualnie jest wybrany.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) 
        {
            switch (Data.CurrentState)
            {
                case Data.States.menu:
                    menu.Update(gameTime);
                    break;

                case Data.States.instruction:
                    instruction.Update(gameTime);
                    break;

                case Data.States.diff:
                    difficulty.Update(gameTime);
                    break;

                case Data.States.game:
                    game.Update(gameTime);
                    break;

                case Data.States.win:
                    win.Update(gameTime);
                    break;

                case Data.States.loss:
                    loss.Update(gameTime);
                    break;
            }
        }
    }
}