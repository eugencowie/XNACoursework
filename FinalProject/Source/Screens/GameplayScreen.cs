using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject
{
    class GameplayScreen : IScreen
    {
        private readonly Game m_game;

        private Map m_map;
        private Player m_player;

        public GameplayScreen(Game game)
        {
            m_game = game;
        }

        public void Initialize(ContentManager content)
        {
            m_map = new Map(content, "Content/Maps/Test.txt", "Content/Maps/Test.info.txt", "Maps/Test/tile");
            m_player = new Player(content.Load<Texture2D>("Textures/Character1"));

            // Set the player position to the first occurence of a player start tile. This
            // could be expanded to randomly select a start tile instead.
            foreach (var tile in m_map.Tiles.Where(t => t.Flags.Contains("playerstart")))
            {
                m_player.Position = new Vector2(tile.X * m_map.TileWidth, tile.Y * m_map.TileHeight - 50);
                break;
            }
        }

        public void Dispose()
        {
        }

        private bool coll(float angle, float center, float range)
        {
            return (angle > (center - range) && angle < (center + range));
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                m_game.Exit();
            }

            m_player.Update(gameTime);

            bool c = false;
            foreach (var tile in m_map.Tiles.Where(t => t.Flags.Contains("collision") && t.Bounds.Intersects(m_player.Bounds)))
            {
                Vector2 direction = m_player.Position - tile.Bounds.Center.ToVector2();
                float angle = MathHelper.ToDegrees((float)Math.Atan2(direction.X, -direction.Y));

                float range = 35;

                if (coll(angle, 0, range))
                {
                    if (m_player.Velocity.Y > 0)
                    {
                        m_player.Acceleration.Y = -9.8f;
                        m_player.Velocity.Y = 0;
                    }
                }
                else if (coll(angle, 90, range))
                {
                    if (m_player.Velocity.X > 0)
                        m_player.Velocity.X = 0;
                }
                else if (coll(angle, 180, range))
                {
                    if (m_player.Velocity.Y < 0)
                        m_player.Velocity.Y = 0;
                }
                else if (coll(angle, -90, range))
                {
                    if (m_player.Velocity.X > 0)
                        m_player.Velocity.X = 0;
                }

                c = true;
            }

            if (!c)
            {
                m_player.Color = Color.White;

                m_player.Acceleration.Y = 0;
            }

            //Color color = Color.White;
            //bool onGround = false;
            /*foreach (var tile in m_map.Tiles.Where(t => t.Flags.Contains("collision") && t.Bounds.Intersects(m_player.Bounds)))
            {
                m_player.Acceleration.Y = -9.8f;

                Vector2 direction = m_player.Position - tile.Bounds.Center.ToVector2();
                //direction.Normalize();

                float angle = MathHelper.ToDegrees((float)Math.Atan2(direction.X, -direction.Y));

                if (angle > -45 && angle < 45)
                {
                    //color = Color.Red;
                }

                if (angle > -135 && angle < -45)
                {
                    //color = Color.Purple;

                    //m_player.Velocity.X = 0;
                }

                if (direction.Y < 0)
                {
                    //m_player.Position.Y = tile.Bounds.Y - (m_player.Texture.Height / 2f) + 1;
                    //onGround = true;
                    //m_player.Acceleration.Y = -9.8f;
                }
                else
                {
                    //m_player.Velocity.Y = 1;
                    //onGround = false;
                }

                if (direction.X < 0)
                {

                }
            }*/

            //m_player.OnGround = onGround;
            //m_player.Color = color;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            m_map.Draw(spriteBatch);
            m_player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
