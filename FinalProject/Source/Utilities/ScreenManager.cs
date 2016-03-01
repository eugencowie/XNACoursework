using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalProject
{
    class ScreenManager : IDisposable
    {
        private readonly ContentManager m_content;
        private readonly SpriteBatch m_spriteBatch;

        private readonly Stack<IScreen> m_screens = new Stack<IScreen>();

        public ScreenManager(ContentManager content, SpriteBatch spriteBatch)
        {
            m_content = content;
            m_spriteBatch = spriteBatch;
        }

        /// <summary>
        /// Remove all existing screens and then add the specified screen.
        /// </summary>
        public void SwitchTo(IScreen screen)
        {
            RemoveAll();

            Push(screen);
        }

        /// <summary>
        /// Add a new screen on top of the current screen.
        /// </summary>
        public void Push(IScreen screen)
        {
            if (m_screens.Any())
            {
                m_screens.Peek().Covered(true);
            }

            screen.Initialize(m_content);
            m_screens.Push(screen);
        }

        /// <summary>
        /// Remove the top-most screen.
        /// </summary>
        public void Pop()
        {
            if (m_screens.Any())
            {
                m_screens.Peek().Dispose();
                m_screens.Pop();

                if (m_screens.Any())
                {
                    m_screens.Peek().Covered(false);
                }
            }
        }

        /// <summary>
        /// Remove all screens.
        /// </summary>
        private void RemoveAll()
        {
            while (m_screens.Any())
            {
                Pop();
            }
        }

        /// <summary>
        /// Update all screens, from bottom-most first to top-most last.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            foreach (var screen in m_screens.Reverse())
            {
                screen.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw all screens, from bottom-most first to top-most last.
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            foreach (var screen in m_screens.Reverse())
            {
                screen.Draw(gameTime, m_spriteBatch);
            }
        }

        public void Dispose()
        {
            RemoveAll();
        }
    }
}
