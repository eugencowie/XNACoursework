using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Squared.Tiled;

namespace FinalProject
{
    /// <summary>
    /// The gameplay screen is the screen which handles the actual gameplay.
    /// </summary>
    class GameplayScreen : IScreen
    {
        private readonly Game m_game;

        private Map m_map;
        private Player m_player;

        public GameplayScreen(Game game)
        {
            m_game = game;
        }

        public void Initialize(ContentManager content)
        {
            m_map = Map.Load(content.RootDirectory + "/Maps/Test.tmx", content);

            m_player = new Player(m_map, content.Load<Texture2D>("Textures/Character1"));
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
            m_player.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            var cameraTarget = new Vector2(m_player.X, m_player.Y);
            var screenBounds = m_game.GraphicsDevice.Viewport.Bounds;

            m_map.Draw(spriteBatch, screenBounds, cameraTarget - screenBounds.Center.ToVector2());

            spriteBatch.End();
        }

        public void Covered(bool covered)
        {
        }
    }
}
