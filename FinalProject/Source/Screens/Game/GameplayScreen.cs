using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace FinalProject
{
    class GameplayScreen : IScreen
    {
        private readonly Game m_game;

        private Player m_player;

        private Map m_map;
        private List<Texture2D> m_mapTextures = new List<Texture2D>();

        public GameplayScreen(Game game)
        {
            m_game = game;
        }

        public void Initialize(ContentManager content)
        {
            m_player = new Player(content.Load<Texture2D>("Textures/Character1"))
            {
                Position = m_game.GraphicsDevice.Viewport.Bounds.Center.ToVector2()
            };

            m_map = MapLoader.Load("Content/Maps/Test.txt");

            for (int i=0; i<=23; i++)
            {
                m_mapTextures.Add(content.Load<Texture2D>("Maps/Test/tile" + i));
            }
        }

        public void Dispose()
        {
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                m_game.Exit();

            m_player.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var layer in m_map.Layers)
            {
                for (int y=0; y<layer.Tiles.Count; y++)
                {
                    for (int x=0; x<layer.Tiles[y].Count; x++)
                    {
                        Vector2 position = new Vector2(x * m_map.TileWidth, y * m_map.TileHeight);
                        var tile = layer.Tiles[y][x];

                        if (tile.Id != -1)
                        {
                            spriteBatch.Draw(m_mapTextures[tile.Id], position, Color.White);
                        }
                    }
                }
            }

            m_player.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
