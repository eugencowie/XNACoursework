using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Squared.Tiled;

namespace FinalProject
{
    class GameplayScreen : IScreen
    {
        private readonly Game m_game;

        private Map m_map;

        private Object m_player => m_map.ObjectGroups["Objects"].Objects["PlayerStart"];

        private Vector2 m_cameraTarget => new Vector2(m_player.X, m_player.Y);

        private readonly List<Rectangle> m_collisionRect = new List<Rectangle>();

        public GameplayScreen(Game game)
        {
            m_game = game;
        }

        public void Initialize(ContentManager content)
        {
            m_map = Map.Load(content.RootDirectory + "/Maps/Test.tmx", content);

            m_player.Texture = content.Load<Texture2D>("Textures/Character1");

            foreach (var coll in m_map.ObjectGroups["Collision"].Objects.Values)
            {
                m_collisionRect.Add(new Rectangle {
                    X = coll.X,
                    Y = coll.Y,
                    Width = coll.Width,
                    Height = coll.Height
                });
            }
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kbs = Keyboard.GetState();

            var oldPos = new Point(m_player.X, m_player.Y);

            if (kbs.IsKeyDown(Keys.W))
            {
                AttemptMoveY(-5);
            }

            else if (kbs.IsKeyDown(Keys.S))
            {
                AttemptMoveY(5);
            }

            else
            {
                AttemptMoveY(10);
            }

            if (kbs.IsKeyDown(Keys.A))
            {
                AttemptMoveX(-5);
            }

            else if (kbs.IsKeyDown(Keys.D))
            {
                AttemptMoveX(5);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_map.Draw(spriteBatch, m_game.GraphicsDevice.Viewport.Bounds, m_cameraTarget - m_game.GraphicsDevice.Viewport.Bounds.Center.ToVector2());

            spriteBatch.End();
        }

        private void AttemptMoveX(int x)
        {
            var playerBounds = new Rectangle(m_player.X, m_player.Y, m_player.Width, m_player.Height);
            playerBounds.X += x;

            bool intersects = (m_collisionRect.Count(r => r.Intersects(playerBounds)) > 0);
            if (intersects)
            {
                // If there will be a collision, try to move half the distance instead.
                int newX = (int)(x / 2f);
                if (newX > 0)
                    AttemptMoveX(newX);
            }
            else
            {
                // If no collision, move the player.
                m_player.X += x;
            }
        }

        private void AttemptMoveY(int y)
        {
            var playerBounds = new Rectangle(m_player.X, m_player.Y, m_player.Width, m_player.Height);
            playerBounds.Y += y;

            bool intersects = (m_collisionRect.Count(r => r.Intersects(playerBounds)) > 0);
            if (intersects)
            {
                // If there will be a collision, try to move half the distance instead.
                int newY = (int)(y / 2f);
                if (newY > 0)
                    AttemptMoveY(newY);
            }
            else
            {
                // If no collision, move the player.
                m_player.Y += y;
            }
        }
    }
}
