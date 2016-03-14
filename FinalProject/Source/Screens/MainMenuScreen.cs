using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    /// <summary>
    /// The main (or primary) menu screen, which contains buttons for starting/quitting the
    /// game as well as buttons to show the options menu screen or the credits menu screen.
    /// </summary>
    class MainMenuScreen : IScreen
    {
        Game m_game;
        ScreenManager m_screenManager;

        Sprite m_background;
        Sprite m_startButton;

        KeyboardState m_prevKb;
        MouseState m_prevMouse;

        bool m_isCovered;

        public MainMenuScreen(Game game, ScreenManager screenManager)
        {
            m_game = game;
            m_screenManager = screenManager;
        }

        public void Initialize(ContentManager content)
        {
            m_background = new Sprite(content.Load<Texture2D>("Textures/Menu/Background"));
            m_background.Scale.X = (float)m_game.GraphicsDevice.Viewport.Width / m_background.Texture.Width;
            m_background.Scale.Y = (float)m_game.GraphicsDevice.Viewport.Height / m_background.Texture.Height;

            m_startButton = new Sprite(content.Load<Texture2D>("Textures/Menu/StartButton"));
            m_startButton.Origin = m_startButton.Texture.Bounds.Center.ToVector2();
            m_startButton.Position = m_game.GraphicsDevice.Viewport.Bounds.Center.ToVector2();
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!m_isCovered)
            {
                KeyboardState kb = Keyboard.GetState();
                MouseState mouse = Mouse.GetState();

                // Exit the game if the escape key is pressed.
                if (kb.IsKeyUp(Keys.Escape) && m_prevKb.IsKeyDown(Keys.Escape))
                {
                    m_game.Exit();
                }

                // If the mouse is over the start button, set its colour to green,
                // otherwise set its colour to red.
                if (m_startButton.Bounds.Contains(mouse.Position))
                {
                    m_startButton.Color = Color.LightGreen;

                    // If the mouse is clicked while over the start button, switch to
                    // the gameplay screen.
                    if (mouse.LeftButton == ButtonState.Released && m_prevMouse.LeftButton == ButtonState.Pressed)
                    {
                        m_screenManager.SwitchTo(new GameplayScreen(m_game, m_screenManager));
                    }
                }
                else
                {
                    m_startButton.Color = Color.White;
                }

                m_prevKb = kb;
                m_prevMouse = mouse;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            
            m_background.Draw(spriteBatch);

            if (!m_isCovered)
            {
                m_startButton.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public void Covered()
        {
            m_isCovered = true;
        }

        public void Uncovered()
        {
            m_isCovered = false;
        }
    }
}
