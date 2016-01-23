using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class Sprite
    {
        public Texture2D Texture;
        public Rectangle? SourceRectangle = null;
        public Vector2 Origin = Vector2.Zero;
        public Color Color = Color.White;

        public Vector2 Position = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public float UniformScale = 1f;
        public float Rotation = 0f;

        public SpriteEffects Effects = SpriteEffects.None;
        public float Depth = 0f;

        public bool Visible = true;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(
                    Texture,
                    Position,
                    SourceRectangle,
                    Color,
                    Rotation,
                    Origin,
                    Scale * UniformScale,
                    Effects,
                    Depth);
            }
        }
    }
}
