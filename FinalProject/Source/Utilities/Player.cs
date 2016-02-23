using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class Player : Sprite
    {
        private const int PPU = 400;    // pixels per unit
        private const float MU = 0.1f;  // friction

        public Vector2 Velocity;
        public Vector2 Acceleration;

        private KeyboardState prevKbs;

        public Player(Texture2D texture)
            : base(texture)
        {
            Origin = Texture.Bounds.Center.ToVector2();
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Velocity.X -= Velocity.X * MU;
            Velocity += new Vector2(0, 9.8f) * delta;
            Velocity += Acceleration * delta;
            Position += (Velocity * delta) * PPU;

            UpdateInput(gameTime);
        }

        private void UpdateInput(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            if (kbs.IsKeyDown(Keys.Up) && prevKbs.IsKeyUp(Keys.Up))
            {
                Velocity.Y = -3;
            }

            if (kbs.IsKeyDown(Keys.Right))
            {
                Velocity.X = 1.5f;
                Effects = SpriteEffects.None;
            }
            else if (kbs.IsKeyDown(Keys.Left))
            {
                Velocity.X = -1.5f;
                Effects = SpriteEffects.FlipHorizontally;
            }

            prevKbs = kbs;
        }
    }
}
