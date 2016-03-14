using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class Sprite
    {
        public Texture2D Texture = null;
        public Vector2 Origin = Vector2.Zero;
        public Color Color = Color.White;

        public Vector2 Position = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public float UniformScale = 1f;

        public SpriteEffects Effects = SpriteEffects.None;

        public bool Visible = true;

        private Vector2 TopLeft
        {
            get { return Position - (Origin * (Scale * UniformScale)); }
        }

        private Vector2 Size
        {
            get { return Texture.Bounds.Size.ToVector2() * (Scale * UniformScale); }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Size.X, (int)Size.Y); }
        }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible && Texture != null)
            {
                spriteBatch.Draw(
                    Texture,
                    Position,
                    null,
                    Color,
                    0f,
                    Origin,
                    Scale * UniformScale,
                    Effects,
                    0f);
            }
        }
    }
}
