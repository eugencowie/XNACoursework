using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class BackgroundScreen : IScreen
    {
        private Texture2D m_texture;
        private Rectangle m_bounds;

        public BackgroundScreen(Game game)
        {
            m_bounds = game.GraphicsDevice.Viewport.Bounds;
        }

        public void Initialize(ContentManager content)
        {
            m_texture = content.Load<Texture2D>("Textures/Menu/Background");
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_texture, m_bounds, Color.White);

            spriteBatch.End();
        }
    }
}
