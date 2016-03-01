using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    interface IScreen : IDisposable
    {
        void Initialize(ContentManager content);

        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        void Covered(bool covered);
    }
}
