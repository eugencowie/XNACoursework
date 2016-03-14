using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    /// <summary>
    /// The gameplay screen is the screen which handles the actual gameplay.
    /// </summary>
    class GameplayScreen : IScreen
    {
        Game m_game;
        ScreenManager m_screenManager;

        Camera m_camera;

        Map m_map;
        Player m_player;
        
        KeyboardState m_prevKbState;

        public GameplayScreen(Game game, ScreenManager screenManager)
        {
            m_game = game;
            m_screenManager = screenManager;
        }

        public void Initialize(ContentManager content)
        {
            m_camera = new Camera();
            m_camera.Origin = (m_game.GraphicsDevice.Viewport.Bounds.Size.ToVector2() / 2f);

            m_map = new Map(content, "Maps/Test");

            m_player = new Player(m_game, content, m_map.CollisionList);
            m_player.Position = m_map.PlayerStart;
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyUp(Keys.Escape) && m_prevKbState.IsKeyDown(Keys.Escape))
            {
                m_screenManager.SwitchTo(new MainMenuScreen(m_game, m_screenManager));
            }

            m_prevKbState = kbState;

            m_player.Update(gameTime);
            m_camera.Position = m_player.Position;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix: m_camera.GetViewMatrix());

            m_map.Draw(gameTime, spriteBatch);
            m_player.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        public void Covered()
        {
        }

        public void Uncovered()
        {
        }
    }
}
