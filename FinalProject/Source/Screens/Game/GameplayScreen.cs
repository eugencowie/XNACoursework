using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class GameplayScreen : Screen
    {
        private Player m_player;

        public GameplayScreen(Game game, ScreenManager screenManager)
            : base(game, screenManager)
        {
        }

        public override void Initialize()
        {
            m_player = new Player(Content.Load<Texture2D>("Textures/Character1"))
            {
                Position = Viewport.Bounds.Center.ToVector2()
            };
        }

        public override void Dispose()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Game.Exit();

            m_player.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
