using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class ScreenManager : IDisposable
    {
        private readonly Game m_game;
        private readonly SpriteBatch m_spriteBatch;

        private readonly Stack<IScreen> m_screens = new Stack<IScreen>();

        public ScreenManager(Game game, SpriteBatch spriteBatch)
        {
            m_game = game;
            m_spriteBatch = spriteBatch;
        }

        public void Switch(IScreen screen)
        {
            RemoveAll();

            Push(screen);
        }

        public void Push(IScreen screen)
        {
            screen.Initialize(m_game.Content);

            m_screens.Push(screen);
        }

        public void Pop()
        {
            if (m_screens.Any())
            {
                m_screens.Peek().Dispose();
                m_screens.Pop();
            }
        }

        private void RemoveAll()
        {
            while (m_screens.Any())
            {
                Pop();
            }
        }

        public void Dispose()
        {
            RemoveAll();
        }

        public void Update(GameTime gameTime)
        {
            foreach (IScreen screen in m_screens.Reverse())
            {
                screen.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IScreen screen in m_screens.Reverse())
            {
                screen.Draw(gameTime, m_spriteBatch);
            }
        }
    }
}
