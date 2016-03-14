using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalProject
{
    class ScreenManager
    {
        ContentManager m_content;
        SpriteBatch m_spriteBatch;

        List<IScreen> m_screens = new List<IScreen>();

        public ScreenManager(ContentManager content, SpriteBatch spriteBatch)
        {
            m_content = content;
            m_spriteBatch = spriteBatch;
        }

        public void Dispose()
        {
            Clear();
        }
        
        /// <summary>
        /// Update all screens, from bottom-most to top-most.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            List<IScreen> copy = m_screens.ToList();

            foreach (var screen in copy)
            {
                screen.Update(gameTime);
            }
        }

        /// <summary>
        /// Draw all screens, from bottom-most to top-most.
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            List<IScreen> copy = m_screens.ToList();

            foreach (var screen in copy)
            {
                screen.Draw(gameTime, m_spriteBatch);
            }
        }

        /// <summary>
        /// Get the top-most screen.
        /// </summary>
        public IScreen Top()
        {
            if (m_screens.Count == 0)
            {
                return null;
            }

            return m_screens[m_screens.Count - 1];
        }

        /// <summary>
        /// Remove all screens.
        /// </summary>
        public void Clear()
        {
            while (m_screens.Count > 0)
            {
                Pop();
            }
        }

        /// <summary>
        /// Remove all existing screens and then add the specified screen.
        /// </summary>
        public void SwitchTo(IScreen screen)
        {
            Clear();

            Push(screen);
        }

        /// <summary>
        /// Add a new screen on top of the current screen.
        /// </summary>
        public void Push(IScreen screen)
        {
            if (m_screens.Count > 0)
            {
                Top().Covered();
            }

            m_screens.Add(screen);
            screen.Initialize(m_content);
        }

        /// <summary>
        /// Remove the top-most screen.
        /// </summary>
        public void Pop()
        {
            if (m_screens.Count > 0)
            {
                Top().Dispose();
                m_screens.RemoveAt(m_screens.Count - 1);

                if (m_screens.Count > 0)
                {
                    Top().Uncovered();
                }
            }
        }
    }
}
