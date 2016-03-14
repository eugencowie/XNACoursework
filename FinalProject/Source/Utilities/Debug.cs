using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    static class Debug
    {
        static Sprite m_redPixel;

        public static void Initialize(ContentManager content)
        {
            m_redPixel = new Sprite(content.Load<Texture2D>("Textures/RedPixel"));
        }

        public static void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 size)
        {
            if (m_redPixel != null)
            {
                m_redPixel.Position = position;
                m_redPixel.Scale = size;
                m_redPixel.Draw(spriteBatch);
            }
        }

        public static void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            Draw(spriteBatch, rectangle.Location.ToVector2(), rectangle.Size.ToVector2());
        }
    }
}
