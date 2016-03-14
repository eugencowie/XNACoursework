using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class Camera
    {
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }
        public float Zoom { get; set; }

        public Camera()
        {
            Position = Vector2.Zero;
            Origin = Vector2.Zero;
            Rotation = 0f;
            Zoom = 1f;
        }

        public Matrix GetViewMatrix()
        {
            return
                Matrix.CreateTranslation(new Vector3(Origin, 0f)) *
                Matrix.CreateTranslation(new Vector3(-Position, 0f)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(Zoom, Zoom, 1f);
        }
    }
}
