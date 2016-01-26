using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class BackgroundScreen : IScreen
    {
        private readonly Game m_game;

        private Texture2D m_backgroundTexture;

        public BackgroundScreen(Game game)
        {
            m_game = game;
        }

        public void Initialize(ContentManager content)
        {
            m_backgroundTexture = content.Load<Texture2D>("Textures/Menu/Background");
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

            spriteBatch.Draw(m_backgroundTexture, m_game.GraphicsDevice.Viewport.Bounds, Color.White);

            spriteBatch.End();
        }
    }
}
