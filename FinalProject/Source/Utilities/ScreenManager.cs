using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalProject
{
    class ScreenManager : IDisposable
    {
        private readonly SpriteBatch m_spriteBatch;

        private readonly Stack<Screen> m_screens = new Stack<Screen>();

        public ScreenManager(SpriteBatch spriteBatch)
        {
            m_spriteBatch = spriteBatch;
        }

        public void Push(Screen screen)
        {
            screen.Initialize();

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
            foreach (Screen screen in m_screens.Reverse())
            {
                screen.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Screen screen in m_screens.Reverse())
            {
                screen.Draw(gameTime, m_spriteBatch);
            }
        }
    }
}
