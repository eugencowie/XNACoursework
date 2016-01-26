using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class GameplayScreen : IScreen
    {
        private readonly Game m_game;

        private Player m_player;

        public GameplayScreen(Game game)
        {
            m_game = game;
        }

        public void Initialize(ContentManager content)
        {
            m_player = new Player(content.Load<Texture2D>("Textures/Character1"))
            {
                Position = m_game.GraphicsDevice.Viewport.Bounds.Center.ToVector2()
            };
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                m_game.Exit();

            m_player.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
