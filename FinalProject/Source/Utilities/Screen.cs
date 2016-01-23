using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    abstract class Screen : IDisposable
    {
        protected readonly ScreenManager ScreenManager;
        protected readonly Game Game;

        protected ContentManager Content
        {
            get { return Game.Content; }
        }

        protected Viewport Viewport
        {
            get { return Game.GraphicsDevice.Viewport; }
        }

        protected Screen(Game game, ScreenManager screenManager)
        {
            ScreenManager = screenManager;
            Game = game;
        }

        public abstract void Initialize();
        public abstract void Dispose();

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
