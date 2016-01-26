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
            m_button.Label = "Test button";
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

            if (kbs.IsKeyDown(Keys.D1))
            {
                m_button.Label = "";
            }

            if (kbs.IsKeyDown(Keys.D2))
            {
                m_button.Label = "Test button";
                m_button.Color = Color.Red;
            }

            if (kbs.IsKeyDown(Keys.D3))
            {
                m_button.Label = "Lorum ipsum delor sit amet";
                m_button.Color = Color.Blue;
            }

            if (kbs.IsKeyDown(Keys.D4))
            {
                m_button.Label = "Lorum ipsum delor sit amet, the little brown dog jumped over the lazy cow";
                m_button.Color = Color.Purple;
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
