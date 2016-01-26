using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace FinalProject
{
    class Tile
    {
        public int Id;
    }

    class Layer
    {
        public int Id;
        public List<List<Tile>> Tiles = new List<List<Tile>>();
    }

    class Map
    {
        public int TilesWide;
        public int TilesHigh;

        public int TileWidth;
        public int TileHeight;

        public List<Layer> Layers = new List<Layer>();
    }

    static class MapLoader
    {
        public static Map Load(string mapPath)
        {
            Map map = new Map();

            if (!File.Exists(mapPath))
            {
                throw new System.ArgumentException("mapPath does not exist");
            }

            using (StreamReader file = new StreamReader(mapPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("tileswide"))
                    {
                        int.TryParse(line.Replace("tileswide ", ""), out map.TilesWide);
                    }
                    else if (line.StartsWith("tileshigh"))
                    {
                        int.TryParse(line.Replace("tileshigh ", ""), out map.TilesHigh);
                    }
                    else if (line.StartsWith("tilewidth"))
                    {
                        int.TryParse(line.Replace("tilewidth ", ""), out map.TileWidth);
                    }
                    else if (line.StartsWith("tileheight"))
                    {
                        int.TryParse(line.Replace("tileheight ", ""), out map.TileHeight);
                    }
                    else if (line.StartsWith("layer"))
                    {
                        Layer layer = new Layer();
                        int.TryParse(line.Replace("layer ", ""), out layer.Id);

                        while (!string.IsNullOrWhiteSpace(line = file.ReadLine()))
                        {
                            List<Tile> row = new List<Tile>();

                            foreach (string tileString in line.Split(','))
                            {
                                if (!string.IsNullOrWhiteSpace(tileString))
                                {
                                    Tile tile = new Tile();
                                    int.TryParse(tileString, out tile.Id);
                                    
                                    row.Add(tile);
                                }
                            }

                            layer.Tiles.Add(row);
                        }

                        map.Layers.Add(layer);
                    }
                }
            }

            return map;
        }
    }
}
