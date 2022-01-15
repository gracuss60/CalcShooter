using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CalcShooter
{
    /// <summary>
    ///  Klasa komponent jest klasą abstrakcyjna, po której dziedziczy większość klas programu
    /// </summary>
    public abstract class Component 
    {
        public abstract void LoadContent(ContentManager Content);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);
    }
}