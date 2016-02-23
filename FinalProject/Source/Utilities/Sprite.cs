using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class Sprite
    {
        public Texture2D Texture;
        public Vector2 Origin = Vector2.Zero;
        public Color Color = Color.White;

        public Vector2 Position = Vector2.Zero;

        public SpriteEffects Effects = SpriteEffects.None;

        public bool Visible = true;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle
                {
                    X = (int)(Position.X - Origin.X),
                    Y = (int)(Position.Y - Origin.Y),
                    Width = Texture.Width,
                    Height = Texture.Height
                };
            }
        }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                spriteBatch.Draw(Texture, Position, color: Color, origin: Origin, effects: Effects);
            }
        }
    }
}
