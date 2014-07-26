// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-22-2013
// ***********************************************************************
// <copyright file="Message.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     Class Message
    /// </summary>
    public class Message
    {
        #region Fields

        /// <summary>
        ///     The font
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        ///     The item
        /// </summary>
        private readonly Button item;

        /// <summary>
        ///     The textur
        /// </summary>
        private readonly Texture2D textur;

        /// <summary>
        ///     The border
        /// </summary>
        private int border = 20;

        /// <summary>
        ///     The content
        /// </summary>
        private List<string> content;

        /// <summary>
        ///     The content pos
        /// </summary>
        private List<Vector2> contentPos;

        /// <summary>
        ///     The head
        /// </summary>
        private string head;

        /// <summary>
        ///     The head pos
        /// </summary>
        private Vector2 headPos;

        /// <summary>
        ///     The own pos
        /// </summary>
        private Vector2 ownPos;

        /// <summary>
        ///     The visible
        /// </summary>
        private bool visible;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="screenwidth">The screenwidth.</param>
        /// <param name="screenheight">The screenheight.</param>
        /// <param name="textur">The textur.</param>
        public Message(SpriteFont font, int screenwidth, int screenheight, Texture2D textur)
        {
            this.font = font;
            this.textur = textur;
            ownPos = new Vector2((screenwidth - textur.Width)/2, (screenheight - textur.Height)/2);
            item = new Button(Texturen.hausbutton,
                ownPos +
                new Vector2((textur.Width - Texturen.hausbutton.Width)/2,
                    textur.Height - Texturen.hausbutton.Height - border), "OK", font);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;
            spriteBatch.Draw(textur, ownPos, Color.White);
            spriteBatch.DrawString(font, head, headPos, Color.Black);
            for (int i = 0; i < content.Count; i++)
                spriteBatch.DrawString(font, content[i], contentPos[i], Color.Black);
            item.Draw(spriteBatch, Color.Red, Color.Gold);
        }

        /// <summary>
        ///     Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
            item.hide();
        }

        /// <summary>
        ///     Mousekeyses this instance.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Mousekeys()
        {
            if (!visible) return false;
            if (item.MouseKeys())
                return false;
            return true;
        }

        /// <summary>
        ///     Sets the strings.
        /// </summary>
        /// <param name="head">The head.</param>
        /// <param name="content">The content.</param>
        public void setStrings(String head, String content)
        {
            this.content = new List<String>();
            contentPos = new List<Vector2>();
            this.head = head;
            headPos = new Vector2((textur.Width - font.MeasureString(head).X)/2, ownPos.Y + 0);
            String[] aux = content.Split(' ');
            String auxitem = aux[0];
            for (int i = 1; i < aux.Length; i++)
            {
                if (font.MeasureString(auxitem + aux[i] + ' ').X <= textur.Width - border)
                    auxitem += ' ' + aux[i];
                else
                {
                    this.content.Add(auxitem);
                    contentPos.Add(ownPos + new Vector2(border, this.content.Count*25));
                    auxitem = aux[i];
                }
            }
            this.content.Add(auxitem);
            contentPos.Add(ownPos + new Vector2(border, this.content.Count*25));
        }

        /// <summary>
        ///     Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
            item.show();
        }

        #endregion Methods
    }
}