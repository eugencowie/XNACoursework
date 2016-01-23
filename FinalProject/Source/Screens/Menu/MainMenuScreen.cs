using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class MainMenuScreen : Screen
    {
        private Button m_button;

        public MainMenuScreen(Game game, ScreenManager screenManager)
            : base(game, screenManager)
        {
        }

        public override void Initialize()
        {
            m_button = new Button(
                Content.Load<Texture2D>("Textures/Menu/Button_Cap"),
                Content.Load<Texture2D>("Textures/Menu/Button_Center"),
                Content.Load<SpriteFont>("Fonts/Menu"));

            m_button.Position = Viewport.Bounds.Center.ToVector2();
            m_button.Padding = new Vector2(25, 25);
            m_button.Label = "Test button";
        }

        public override void Dispose()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.Escape))
                Game.Exit();

            if (kbs.IsKeyDown(Keys.D1))
                m_button.Label = "";

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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_button.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
