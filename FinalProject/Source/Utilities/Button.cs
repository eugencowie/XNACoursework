using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FinalProject
{
    class Button
    {
        public Texture2D CapTexture;
        public Texture2D CenterTexture;
        public Color Color = Color.Red;

        public SpriteFont Font;
        public string Label;
        public Color LabelColor = Color.White;

        public Vector2 Position;
        public Vector2 Padding;

        public bool Visible = true;

        public Rectangle Bounds
        {
            get { return GetBounds(); }
        }

        public Button(Texture2D capTexture, Texture2D centerTexture, SpriteFont font)
        {
            CapTexture = capTexture;
            CenterTexture = centerTexture;
            Font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible && !string.IsNullOrWhiteSpace(Label))
            {
                Vector2 textSize = Font.MeasureString(Label);
                Vector2 centerSize = textSize + Padding;

                Rectangle leftCapRect = new Rectangle {
                    Location = new Point(0, 0),
                    Size = new Point(CapTexture.Width, (int)centerSize.Y)
                };

                Rectangle centerRect = new Rectangle {
                    Location = leftCapRect.Location + new Point(leftCapRect.Width, 0),
                    Size = new Point((int)centerSize.X, (int)centerSize.Y)
                };

                Rectangle rightCapRect = new Rectangle {
                    Location = centerRect.Location + new Point(centerRect.Width, 0),
                    Size = leftCapRect.Size
                };

                Vector2 textPos = centerRect.Location.ToVector2() + (Padding / 2f);

                // Center the button on the position.
                Vector2 totalSize = new Vector2(leftCapRect.Width + centerRect.Width + rightCapRect.Width, leftCapRect.Height);
                Vector2 topLeft = Position - (totalSize / 2f);
                leftCapRect.Offset(topLeft);
                centerRect.Offset(topLeft);
                rightCapRect.Offset(topLeft);
                textPos += topLeft;

                spriteBatch.Draw(CapTexture, leftCapRect, Color);
                spriteBatch.Draw(CenterTexture, centerRect, Color);
                spriteBatch.Draw(CapTexture, rightCapRect, null, Color, 0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.DrawString(Font, Label, textPos, LabelColor);
            }
        }

        private Rectangle GetBounds()
        {
            Vector2 textSize = Font.MeasureString(Label);
            Vector2 centerSize = textSize + Padding;

            Rectangle leftCapRect = new Rectangle
            {
                Location = new Point(0, 0),
                Size = new Point(CapTexture.Width, (int)centerSize.Y)
            };

            Rectangle centerRect = new Rectangle
            {
                Location = leftCapRect.Location + new Point(leftCapRect.Width, 0),
                Size = new Point((int)centerSize.X, (int)centerSize.Y)
            };

            Rectangle rightCapRect = new Rectangle
            {
                Location = centerRect.Location + new Point(centerRect.Width, 0),
                Size = leftCapRect.Size
            };

            Vector2 textPos = centerRect.Location.ToVector2() + (Padding / 2f);

            // Center the button on the position.
            Vector2 totalSize = new Vector2(leftCapRect.Width + centerRect.Width + rightCapRect.Width, leftCapRect.Height);
            Vector2 topLeft = Position - (totalSize / 2f);
            leftCapRect.Offset(topLeft);
            centerRect.Offset(topLeft);
            rightCapRect.Offset(topLeft);
            textPos += topLeft;

            return new Rectangle
            {
                X = leftCapRect.X,
                Y = leftCapRect.Y,
                Width = leftCapRect.Width + centerRect.Width + rightCapRect.Width,
                Height = Math.Max(leftCapRect.Height, Math.Max(centerRect.Height, rightCapRect.Height))
            };
        }
    }
}
