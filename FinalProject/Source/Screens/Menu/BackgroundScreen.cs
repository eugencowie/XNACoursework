using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class BackgroundScreen : Screen
    {
        private Texture2D m_backgroundTexture;

        public BackgroundScreen(Game game, ScreenManager screenManager)
            : base(game, screenManager)
        {
        }

        public override void Initialize()
        {
            m_backgroundTexture = Content.Load<Texture2D>("Textures/Menu/Background");
        }

        public override void Dispose()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(m_backgroundTexture, Viewport.Bounds, Color.White);

            spriteBatch.End();
        }
    }
}
