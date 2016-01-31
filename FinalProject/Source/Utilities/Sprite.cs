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

        public bool Intersects(Vector2 testPoint)
        {
            Vector2[] pts = GetPoints();

            // See <http://stackoverflow.com/a/14998816>.
            bool result = false;
            int j = pts.Length - 1;
            for (int i = 0; i < pts.Length; i++)
            {
                if (pts[i].Y < testPoint.Y && pts[j].Y >= testPoint.Y ||
                    pts[j].Y < testPoint.Y && pts[i].Y >= testPoint.Y)
                {
                    if (pts[i].X + (testPoint.Y - pts[i].Y) / (pts[j].Y - pts[i].Y) * (pts[j].X - pts[i].X) < testPoint.X)
                        result = !result;
                }

                j = i;
            }

            return result;
        }

        private Vector2[] GetPoints()
        {
            var topLeft = new Vector2(Position.X - Origin.X, Position.Y - Origin.Y);
            var topRight = new Vector2(topLeft.X + Texture.Width, topLeft.Y);
            var bottomRight = new Vector2(topLeft.X + Texture.Width, topLeft.Y + Texture.Height);
            var bottomLeft = new Vector2(topLeft.X, topLeft.Y + Texture.Height);

            // Create a list of points in clockwise order.
            Vector2[] points = { topLeft, topRight, bottomRight, bottomLeft };

            Matrix m =
                Matrix.CreateTranslation(-Position.X, -Position.Y, 0f) *
                Matrix.CreateScale(Scale.X * UniformScale, Scale.Y * UniformScale, 1f) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateTranslation(Position.X, Position.Y, 0f);

            for (int i = 0; i < points.Length; i++)
                points[i] = Vector2.Transform(points[i], m);

            return points;
        }
    }
}
