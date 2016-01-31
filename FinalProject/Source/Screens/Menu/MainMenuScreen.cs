using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class MainMenuScreen : IScreen
    {
        private readonly Game m_game;
        private readonly ScreenManager m_screenManager;

        private Sprite m_startButton;

        public MainMenuScreen(Game game, ScreenManager screenManager)
        {
            m_game = game;
            m_screenManager = screenManager;
        }

        public void Initialize(ContentManager content)
        {
            m_startButton = new Sprite(content.Load<Texture2D>("Textures/Menu/StartButton"))
            {
                Position = m_game.GraphicsDevice.Viewport.Bounds.Center.ToVector2(),
            };
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.Escape))
            {
                m_game.Exit();
            }

            MouseState ms = Mouse.GetState();
            if (m_startButton.Intersects(ms.Position.ToVector2()))
            {
                m_startButton.Color = Color.Green;

                if (ms.LeftButton == ButtonState.Pressed)
                {
                    m_screenManager.SwitchTo(new GameplayScreen(m_game));
                }
            }
            else
            {
                m_startButton.Color = Color.Red;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_startButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
