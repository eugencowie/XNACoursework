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

        private Button m_button;

        public MainMenuScreen(Game game, ScreenManager screenManager)
        {
            m_game = game;
            m_screenManager = screenManager;
        }

        public void Initialize(ContentManager content)
        {
            m_button = new Button(
                content.Load<Texture2D>("Textures/Menu/Button_Cap"),
                content.Load<Texture2D>("Textures/Menu/Button_Center"),
                content.Load<SpriteFont>("Fonts/Menu"));

            m_button.Position = m_game.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
            m_button.Padding = new Vector2(25, 25);
            m_button.Label = "Start game";
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
            if (m_button.Bounds.Contains(ms.X, ms.Y))
            {
                m_button.Color = Color.Green;

                if (ms.LeftButton == ButtonState.Pressed)
                {
                    m_screenManager.Switch(new GameplayScreen(m_game));
                }
            }
            else
            {
                m_button.Color = Color.Red;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_button.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
