// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-31-2013
// ***********************************************************************
// <copyright file="Chatbox.cs">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    /// Class Chatbox
    /// </summary>
    public class Chatbox
    {
        /// <summary>
        /// The messages
        /// </summary>
        private static List<string> messages = new List<string>();

        /// <summary>
        /// The own pos
        /// </summary>
        private static Vector2 ownPos = Vector2.Zero;

        /// <summary>
        /// The pos
        /// </summary>
        private static Vector2[] pos = new Vector2[6];

        /// <summary>
        /// The timeout
        /// </summary>
        private static List<int> timeout = new List<int>();

        /// <summary>
        /// The firstmessage
        /// </summary>
        private byte firstmessage = 0;

        /// <summary>
        /// The timeoutscroll
        /// </summary>
        private int timeoutscroll = 0;

        /// <summary>
        /// The scrolling
        /// </summary>
        private static bool scrolling = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chatbox"/> class.
        /// </summary>
        /// <param name="Pos">The pos.</param>
        public Chatbox(Vector2 Pos)
        {
            ownPos = Pos;
            for (int i = 0; i < 6; i++)
                pos[i] = ownPos + new Vector2(0, i * 15);
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void addMessage(string message)
        {
            messages.Add(message);
            if (messages.Count > 6)
                firstmessage++;
            if (messages.Count > 50)
                messages.RemoveAt(0);
            timeout.Add(480);
            if (timeout.Count > 6)
            {
                timeout.RemoveAt(0);
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            if (timeoutscroll > 0)
            {
                timeoutscroll--;
            }
            else
                changemode(false);

            for (byte i = 0; i < timeout.Count; i++)
            {
                timeout[i]--;
                if (timeout[i] < 1)
                {
                    timeout.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Changemodes the specified status.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void changemode(bool status)
        {
            if (!status && scrolling)
            {
                scrolling = false;
                firstmessage = (byte)(messages.Count - timeout.Count);
            }
            else
                if (status && !scrolling)
                {
                    scrolling = true;
                    if (messages.Count > 6)
                        firstmessage = (byte)(messages.Count - 6);
                    else firstmessage = 0;
                }
        }

        /// <summary>
        /// Keyboards the keys.
        /// </summary>
        /// <param name="kbstate">The kbstate.</param>
        public void keyboardKeys(KeyboardState kbstate)
        {
            if (kbstate != Keyboard.GetState())
            {
                if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
                {
                    changemode(true);
                    timeoutscroll = 480;
                    if (firstmessage > 0)
                        firstmessage--;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
                {
                    changemode(true);
                    timeoutscroll = 480;
                    if (firstmessage < messages.Count - 6)
                        firstmessage++;
                }
            }
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (scrolling)
                for (int i = 0; i < messages.Count && i < 6; i++)
                    spriteBatch.DrawString(font, messages[firstmessage + i], pos[i], Color.Lime * ((float)(timeoutscroll / 480.0f)));
            else
                for (byte i = 0; i < timeout.Count; i++)
                {
                    spriteBatch.DrawString(font, messages[messages.Count - timeout.Count + i], pos[i], Color.Lime * ((float)(timeout[i] / 480.0f)));
                }
        }
    }
}