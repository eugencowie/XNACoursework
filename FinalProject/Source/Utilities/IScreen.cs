using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    interface IScreen
    {
        void Initialize(ContentManager content);
        void Dispose();

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        void Covered();
        void Uncovered();
    }
}
