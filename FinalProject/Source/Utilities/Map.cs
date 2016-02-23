using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FinalProject
{
    class Tile
    {
        public Layer Layer;

        public int Id = -1;

        public int X;
        public int Y;

        public List<string> Flags = new List<string>();

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle
                {
                    X = X * Layer.Map.TileWidth,
                    Y = Y * Layer.Map.TileHeight,
                    Width = Layer.Map.TileWidth,
                    Height = Layer.Map.TileHeight
                };
            }
        }
    }

    class Layer
    {
        public Map Map;

        public int Id = -1;

        public IEnumerable<Tile> Tiles
        {
            get { return Map.Tiles.Where(t => t.Layer == this); }
        }
    }

    class Map
    {
        public int TilesWide;
        public int TilesHigh;

        public int TileWidth;
        public int TileHeight;

        public List<Layer> Layers = new List<Layer>();
        public List<Tile> Tiles = new List<Tile>();

        private List<Texture2D> m_textures = new List<Texture2D>();

        public Map(ContentManager content, string mapPath, string infoPath, string tilePathPrefix)
        {
            LoadPyxelMap(content, mapPath, infoPath, tilePathPrefix);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tile in Tiles.Where(t => !t.Flags.Contains("playerstart")))
            {
                spriteBatch.Draw(m_textures[tile.Id], tile.Bounds, Color.White);
            }
        }

        private void LoadPyxelMap(ContentManager content, string mapPath, string infoPath, string tilePathPrefix)
        {
            TilesWide = 0;
            TilesHigh = 0;
            TileWidth = 0;
            TileHeight = 0;
            Layers.Clear();
            Tiles.Clear();
            m_textures.Clear();

            ParsePyxelFile(mapPath);
            ParseInfoFile(infoPath);

            for (int i = 0; i <= Tiles.Max(t => t.Id); i++)
            {
                m_textures.Add(content.Load<Texture2D>(tilePathPrefix + i));
            }
        }

        private void ParsePyxelFile(string mapPath)
        {
            if (!File.Exists(mapPath))
            {
                throw new ArgumentException(string.Format("missing: '{0}'", mapPath));
            }

            using (StreamReader file = new StreamReader(mapPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("tileswide"))
                    {
                        string property = line.Replace("tileswide", "").Trim();
                        int.TryParse(property, out TilesWide);
                    }
                    else if (line.StartsWith("tileshigh"))
                    {
                        string property = line.Replace("tileshigh", "").Trim();
                        int.TryParse(property, out TilesHigh);
                    }
                    else if (line.StartsWith("tilewidth"))
                    {
                        string property = line.Replace("tilewidth", "").Trim();
                        int.TryParse(property, out TileWidth);
                    }
                    else if (line.StartsWith("tileheight"))
                    {
                        string property = line.Replace("tileheight", "").Trim();
                        int.TryParse(property, out TileHeight);
                    }
                    else if (line.StartsWith("layer"))
                    {
                        Layer layer = new Layer();
                        layer.Map = this;

                        string property = line.Replace("layer", "").Trim();
                        int.TryParse(property, out layer.Id);

                        int row = -1;
                        while (!string.IsNullOrWhiteSpace(line = file.ReadLine()))
                        {
                            row++;

                            int column = -1;
                            foreach (string tileString in line.Split(','))
                            {
                                column++;

                                if (!string.IsNullOrWhiteSpace(tileString))
                                {
                                    Tile tile = new Tile()
                                    {
                                        Layer = layer,
                                        X = column,
                                        Y = row
                                    };

                                    int.TryParse(tileString, out tile.Id);

                                    if (tile.Id != -1)
                                        Tiles.Add(tile);
                                }
                            }
                        }

                        Layers.Add(layer);
                    }
                }
            }
        }

        private void ParseInfoFile(string infoPath)
        {
            if (!File.Exists(infoPath))
            {
                throw new ArgumentException(string.Format("missing: '{0}'", infoPath));
            }

            foreach (string line in File.ReadAllLines(infoPath))
            {
                string[] tokens = line.Split('=');
                if (tokens.Length != 2)
                {
                    throw new ArgumentException(string.Format("invalid line '{0}' in info file '{1}'", line, infoPath));
                }

                string keyword = tokens[0].Trim().ToLower();
                List<int> tileIds = new List<int>();

                foreach (string tileStr in tokens[1].Trim().Split(' '))
                {
                    int tileId;

                    try { int.TryParse(tileStr, out tileId); }
                    catch { tileId = -1; }

                    if (tileId >= 0)
                    {
                        tileIds.Add(tileId);
                    }
                }

                foreach (int tileId in tileIds)
                {
                    foreach (Layer layer in Layers)
                    {
                        foreach (var tile in layer.Tiles.Where(t => t.Id == tileId))
                        {
                            tile.Flags.Add(keyword);
                        }
                    }
                }
            }
        }
    }
}
