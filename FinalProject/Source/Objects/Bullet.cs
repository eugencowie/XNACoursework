using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FinalProject
{
    class Bullet
    {
        public Rectangle Bounds
        {
            get { return new Rectangle((int)m_position.X, (int)m_position.Y, m_texture.Width, m_texture.Height); }
        }

        private Texture2D m_texture;
        private Vector2 m_position;
        private Vector2 m_velocity;

        private readonly List<Rectangle> m_collisionRects;

        public Bullet(Texture2D texture, Vector2 position, Vector2 velocity, List<Rectangle> collisionRects)
        {
            m_texture = texture;
            m_position = position;
            m_velocity = velocity;

            m_collisionRects = collisionRects;
        }

        public void Update(GameTime gameTime)
        {
            m_position += m_velocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_texture, m_position, Color.White);

#if DEBUG
            Debug.Draw(spriteBatch, m_position, m_texture.Bounds.Size.ToVector2());
#endif
        }
    }
}
