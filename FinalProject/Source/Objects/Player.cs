using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace FinalProject
{
    class Player
    {
        private readonly Game m_game;

        private readonly List<Rectangle> m_collisionRects;

        private readonly List<Bullet> m_bullets = new List<Bullet>();
        Texture2D m_bulletTexture;

        private Vector2 m_velocity = new Vector2();
        private Vector2 m_acceleration = new Vector2();
        private Vector2 m_gravity = new Vector2(0, 9.8f);

        private bool m_facingLeft;
        private bool m_onGround;

        private const int WEAPON_COOLDOWN = 200;
        private int m_weaponCooldown = WEAPON_COOLDOWN;

        private KeyboardState m_prevKbState;

        private Texture2D m_playerTexture;

        public Vector2 Position = new Vector2();

        SpriteEffects m_effects = SpriteEffects.None;

        public Player(Game game, ContentManager content, List<Rectangle> collRects)
        {
            m_game = game;

            m_playerTexture = content.Load<Texture2D>("Textures/Character1");
            m_bulletTexture = content.Load<Texture2D>("Textures/Bullet");

            m_collisionRects = collRects;
        }

        public void Update(GameTime gameTime)
        {
            // PHYSICS:

            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            m_velocity.X -= m_velocity.X * 0.2f;

            m_velocity += m_gravity * delta;
            m_velocity += m_acceleration * delta;

            AttemptMoveX((m_velocity.X * delta) * 400);
            AttemptMoveY((m_velocity.Y * delta) * 400);

            // INPUT:

            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Z) && m_prevKbState.IsKeyUp(Keys.Z) && m_onGround)
            {
                m_velocity.Y = -2.5f;
            }

            if (kbState.IsKeyDown(Keys.Left) && !kbState.IsKeyDown(Keys.Right))
            {
                m_facingLeft = true;
                m_velocity.X = -1.5f;
            }
            else if (kbState.IsKeyDown(Keys.Right) && !kbState.IsKeyDown(Keys.Left))
            {
                m_facingLeft = false;
                m_velocity.X = 1.5f;
            }

            if (kbState.IsKeyDown(Keys.X) && m_weaponCooldown > WEAPON_COOLDOWN)
            {
                Vector2 velocity = new Vector2(10f, 0f);
                if (m_facingLeft)
                {
                    velocity.X *= -1;
                }

                Vector2 offset = new Vector2(30f, 14f);

                m_bullets.Add(new Bullet(m_bulletTexture, Position + offset, velocity, m_collisionRects));

                m_weaponCooldown = 0;
            }
            m_weaponCooldown += gameTime.ElapsedGameTime.Milliseconds;

            for (int i=m_bullets.Count-1; i>=0; i--)
            {
                m_bullets[i].Update(gameTime);

                if (m_collisionRects.Any(r => r.Intersects(m_bullets[i].Bounds)))
                {
                    m_bullets.RemoveAt(i);
                    continue;
                }

                Rectangle bulletBounds = m_bullets[i].Bounds;
                if (bulletBounds.Left < 0 ||
                    bulletBounds.Right > m_game.GraphicsDevice.Viewport.Width ||
                    bulletBounds.Top < 0 ||
                    bulletBounds.Bottom > m_game.GraphicsDevice.Viewport.Height)
                {
                    m_bullets.RemoveAt(i);
                    continue;
                }
            }

            m_prevKbState = kbState;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (m_facingLeft)
            {
                m_effects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                m_effects = SpriteEffects.None;
            }

            foreach (var bullet in m_bullets)
            {
                bullet.Draw(gameTime, spriteBatch);
            }

            spriteBatch.Draw(m_playerTexture, Position, effects: m_effects);

#if DEBUG
            Debug.Draw(spriteBatch, Position, m_playerTexture.Bounds.Size.ToVector2());
#endif
        }

        private void AttemptMoveX(float x)
        {
            Vector2 newPosition = Position + new Vector2(x, 0f);

            Rectangle newBounds = new Rectangle((int)newPosition.X, (int)newPosition.Y, m_playerTexture.Width, m_playerTexture.Height);
            bool intersects = (m_collisionRects.Count(r => r.Intersects(newBounds)) > 0);

            if (intersects)
            {
                // If there will be a collision, try to move half the distance instead.
                float newX = x / 2f;
                if (newX >= 0.5f)
                {
                    AttemptMoveX(newX);
                }
                else
                {
                    // If unable to move, set the velocity to zero.
                    m_velocity.X = 0f;
                }
            }
            else
            {
                // If no collision, move the player.
                Position.X += x;
            }
        }

        private void AttemptMoveY(float y)
        {
            Vector2 newPosition = Position + new Vector2(0f, y);
            Rectangle newBounds = new Rectangle((int)newPosition.X, (int)newPosition.Y, m_playerTexture.Width, m_playerTexture.Height);

            m_onGround = false;
            bool intersects = false;
            foreach (var rect in m_collisionRects.Where(r => r.Intersects(newBounds)))
            {
                Vector2 difference = newPosition - rect.Location.ToVector2();
                if (difference.Y < 0f)
                {
                    m_onGround = true;
                }

                intersects = true;
            }

            if (intersects)
            {
                // If there will be a collision, try to move half the distance instead.
                float newY = y / 2f;
                if (newY >= 0.5f)
                {
                    AttemptMoveY(newY);
                }
                else
                {
                    // If unable to move, set the velocity to zero.
                    m_velocity.Y = 0f;
                }
            }
            else
            {
                // If no collision, move the player.
                Position.Y += y;
            }
        }
    }
}
