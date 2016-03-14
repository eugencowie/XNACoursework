using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    public class FinalProjectGame : Game
    {
        SpriteBatch m_spriteBatch;
        ScreenManager m_screenManager;

        public FinalProjectGame()
        {
            var graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
            };

            IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
#if DEBUG
            Debug.Initialize(Content);
#endif

            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            m_screenManager = new ScreenManager(Content, m_spriteBatch);
            
            m_screenManager.SwitchTo(new MainMenuScreen(this, m_screenManager));
        }

        protected override void UnloadContent()
        {
            m_screenManager.Dispose();
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
