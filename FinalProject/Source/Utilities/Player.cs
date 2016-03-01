using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Squared.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    class Player
    {
        private readonly Map m_map;

        private MapObject m_player
        {
            get { return m_map.ObjectGroups["Objects"].Objects["PlayerStart"]; }
        }

        private readonly List<Rectangle> m_collisionRects = new List<Rectangle>();

        public float X => m_player.X;
        public float Y => m_player.Y;

        private Vector2 m_velocity = new Vector2();
        private Vector2 m_acceleration = new Vector2();
        private Vector2 m_gravity = new Vector2(0, 9.8f);

        public Player(Map map, Texture2D texture)
        {
            m_map = map;

            foreach (var coll in m_map.ObjectGroups["Collision"].Objects.Values)
            {
                m_collisionRects.Add(coll.Bounds);
            }

            m_player.Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            m_velocity.X -= m_velocity.X * 0.2f;

            m_velocity += m_gravity * delta;
            m_velocity += m_acceleration * delta;

            AttemptMoveX((m_velocity.X * delta) * 400);
            AttemptMoveY((m_velocity.Y * delta) * 400);

            KeyboardState kbs = Keyboard.GetState();

            if (kbs.IsKeyDown(Keys.W) && m_onGround)
            {
                m_velocity.Y = -2;
            }

            if (kbs.IsKeyDown(Keys.A) && !kbs.IsKeyDown(Keys.D))
            {
                m_player.Effects = SpriteEffects.FlipHorizontally;
                m_velocity.X = -1.5f;
            }
            else if (kbs.IsKeyDown(Keys.D) && !kbs.IsKeyDown(Keys.A))
            {
                m_player.Effects = SpriteEffects.None;
                m_velocity.X = 1.5f;
            }
        }

        private void AttemptMoveX(float x)
        {
            Vector2 newPosition = m_player.Position + new Vector2(x, 0f);

            Rectangle newBounds = new Rectangle((int)newPosition.X, (int)newPosition.Y, m_player.Width, m_player.Height);
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
                m_player.X += x;
            }
        }

        private bool m_onGround = false;

        private void AttemptMoveY(float y)
        {
            Vector2 newPosition = m_player.Position + new Vector2(0f, y);
            Rectangle newBounds = new Rectangle((int)newPosition.X, (int)newPosition.Y, m_player.Width, m_player.Height);

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
                m_player.Y += y;
            }
        }
    }
}
