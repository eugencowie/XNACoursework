using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class Player : Sprite
    {
        private const int PPU = 400;    // pixels per unit
        private const float MU = 0.1f;  // friction

        private Vector2 m_velocity;
        private Vector2 m_acceleration;

        private KeyboardState prevKbs;

        public Player(Texture2D texture)
            : base(texture)
        {
            Origin = Texture.Bounds.Center.ToVector2();
        }

        public void Update(GameTime gameTime)
        {
            if (OnGround())
            {
                Position.Y = 500;
                m_velocity.Y = 0;
                m_acceleration.Y = 0;
            }
            else
            {
                m_acceleration.Y = 9.8f;
            }

            UpdateInput(gameTime);

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            m_velocity.X -= m_velocity.X * MU;
            m_velocity += m_acceleration * delta;
            Position += (m_velocity * delta) * PPU;
        }

        private void UpdateInput(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.Up) && prevKbs.IsKeyUp(Keys.Up) && OnGround())
            {
                m_velocity.Y = -2;
            }

            if (kbs.IsKeyDown(Keys.Right))
            {
                m_velocity.X = 1.5f;
                Effects = SpriteEffects.None;
            }
            else if (kbs.IsKeyDown(Keys.Left))
            {
                m_velocity.X = -1.5f;
                Effects = SpriteEffects.FlipHorizontally;
            }

            prevKbs = kbs;
        }

        private bool OnGround()
        {
            return (Position.Y >= 500);
        }
    }
}
