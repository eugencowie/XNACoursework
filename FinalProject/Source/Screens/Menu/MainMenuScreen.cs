using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class MainMenuScreen : Screen
    {
        public MainMenuScreen(Game game, ScreenManager screenManager)
            : base(game, screenManager)
        {
        }

        public override void Initialize()
        {
        }

        public override void Dispose()
        {
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.Escape))
                Game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
    }
}
