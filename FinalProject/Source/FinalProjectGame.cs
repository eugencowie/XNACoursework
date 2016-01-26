using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    public class FinalProjectGame : Game
    {
        private SpriteBatch m_spriteBatch;
        private ScreenManager m_screenManager;

        public FinalProjectGame()
        {
            var graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600
            };

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);

            m_screenManager = new ScreenManager(this, m_spriteBatch);
            m_screenManager.Push(new BackgroundScreen(this));
            m_screenManager.Push(new MainMenuScreen(this, m_screenManager));
        }

        protected override void UnloadContent()
        {
            if (m_screenManager != null)
            {
                m_screenManager.Dispose();
                m_screenManager = null;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            m_screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_screenManager.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
