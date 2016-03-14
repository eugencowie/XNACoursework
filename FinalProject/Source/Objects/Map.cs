using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class Map
    {
        public Vector2 PlayerStart { get; protected set; }

        Sprite m_sprite;

        public List<Rectangle> CollisionList = new List<Rectangle>();

        public Map(ContentManager content, string mapPath)
        {
            m_sprite = new Sprite(content.Load<Texture2D>(mapPath));

            string infoFile = Path.Combine(content.RootDirectory, mapPath) + ".csv";

            if (!File.Exists(infoFile))
                throw new ArgumentException(infoFile + " does not exist");

            foreach (string row in File.ReadAllLines(infoFile))
            {
                string[] columns = row.Split(',');

                if (columns.Length != 5)
                    throw new ArgumentException("invalid number of columns: " + columns.Length);

                string id = columns[0].ToLower().Trim();
                if (id == "start")
                {
                    int x, y;
                    int.TryParse(columns[1], out x);
                    int.TryParse(columns[2], out y);
                    
                    PlayerStart = new Vector2(x, y);
                }
                else if (id == "coll")
                {
                    int x, y, width, height;
                    int.TryParse(columns[1], out x);
                    int.TryParse(columns[2], out y);
                    int.TryParse(columns[3], out width);
                    int.TryParse(columns[4], out height);

                    CollisionList.Add(new Rectangle(x, y, width, height));
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            m_sprite.Draw(spriteBatch);

#if DEBUG
            foreach (var rect in CollisionList)
            {
                Debug.Draw(spriteBatch, rect);
            }
#endif
        }
    }
}
