// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-21-2013
// ***********************************************************************
// <copyright file="TextBox.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#region Using Statements

using System.Windows.Forms; // This class exposes WinForms-style key events.

#endregion Using Statements

namespace _4_1_
{
    /// <summary>
    /// Class TextBox
    /// </summary>
    public class Textfeld
    {
        /// <summary>
        /// The textbox
        /// </summary>
        public static Texture2D textbox;

        /// <summary>
        /// The border
        /// </summary>
        public int border = 2;

        /// <summary>
        /// The border color
        /// </summary>
        public Color BorderColor = Color.Black;

        /// <summary>
        /// The height
        /// </summary>
        public int height = 0;

        /// <summary>
        /// The input
        /// </summary>
        public string input = "";

        /// <summary>
        /// The ispassword
        /// </summary>
        public bool ispassword = false;

        /// <summary>
        /// The length
        /// </summary>
        public int Length = 30;

        /// <summary>
        /// The offsetx
        /// </summary>
        public int offsetx = 10;

        /// <summary>
        /// The offsety
        /// </summary>
        public int offsety = 5;

        /// <summary>
        /// The pos
        /// </summary>
        public Vector2 pos;

        /// <summary>
        /// The selected
        /// </summary>
        public bool Ausgewählt = false;

        /// <summary>
        /// The textfont
        /// </summary>
        public SpriteFont textfont = Texturen.font2;

        /// <summary>
        /// The visible
        /// </summary>
        public bool Sichtbar = false;

        /// <summary>
        /// The width
        /// </summary>
        public int width = 0;

        /// <summary>
        /// The content
        /// </summary>
        private string content;

        /// <summary>
        /// The item
        /// </summary>
        private Rectangle item;

        /// <summary>
        /// The string pos
        /// </summary>
        private Vector2 stringPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="Textfeld"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        public Textfeld(Vector2 pos)
        {
            Initialisieren(pos, "", 30, 10, 5, 2, Color.Black, textfont, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Textfeld"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="content">The content.</param>
        public Textfeld(Vector2 pos, string content)
        {
            Initialisieren(pos, content, 30, 10, 5, 2, Color.Black, textfont, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Textfeld"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="content">The content.</param>
        /// <param name="_ispassword">if set to <c>true</c> [_ispassword].</param>
        public Textfeld(Vector2 pos, string content, bool _ispassword)
        {
            Initialisieren(pos, content, 30, 10, 5, 2, Color.Black, textfont, _ispassword);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Textfeld"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="content">The content.</param>
        /// <param name="_Length">Length of the _.</param>
        public Textfeld(Vector2 pos, string content, int _Length)
        {
            Initialisieren(pos, content, _Length, 10, 5, 2, Color.Black, textfont, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Textfeld"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="content">The content.</param>
        /// <param name="_Length">Length of the _.</param>
        /// <param name="_offsetx">The _offsetx.</param>
        /// <param name="_offsety">The _offsety.</param>
        public Textfeld(Vector2 pos, string content, int _Length, int _offsetx, int _offsety)
        {
            Initialisieren(pos, content, _Length, _offsetx, _offsety, 2, Color.Black, textfont, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Textfeld"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="content">The content.</param>
        /// <param name="_Length">Length of the _.</param>
        /// <param name="_offsetx">The _offsetx.</param>
        /// <param name="_offsety">The _offsety.</param>
        /// <param name="_border">The _border.</param>
        /// <param name="_BorderColor">Color of the _ border.</param>
        public Textfeld(Vector2 pos, string content, int _Length, int _offsetx, int _offsety, int _border, Color _BorderColor)
        {
            Initialisieren(pos, content, _Length, _offsetx, _offsety, _border, _BorderColor, textfont, false);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Textfeld"/> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="content">The content.</param>
        /// <param name="_Length">Length of the _.</param>
        /// <param name="_offsetx">The _offsetx.</param>
        /// <param name="_offsety">The _offsety.</param>
        /// <param name="_border">The _border.</param>
        /// <param name="_BorderColor">Color of the _ border.</param>
        /// <param name="_textfont">The _textfont.</param>
        public Textfeld(Vector2 pos, string content, int _Length, int _offsetx, int _offsety, int _border, Color _BorderColor, SpriteFont _textfont)
        {
            Initialisieren(pos, content, _Length, _offsetx, _offsety, _border, _BorderColor, _textfont, false);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void ZeichneTextfeld(SpriteBatch spriteBatch)
        {
            if (!Sichtbar) return;
            if (Ausgewählt || input != "")
            {
                spriteBatch.Draw(textbox, new Rectangle((int)pos.X, (int)pos.Y, (int)width + 2 * offsetx, (int)height + 2 * offsety), new Rectangle(0, 0, textbox.Width, textbox.Height), BorderColor);
                spriteBatch.Draw(textbox, new Rectangle((int)pos.X + border, (int)((float)pos.Y + border), (int)width + 2 * offsetx - 2 * border, (int)(height + 2 * offsety - border - 0.5f * border)), new Rectangle(0, 0, textbox.Width, textbox.Height), Color.Brown);
                String aus = input;
                if (ispassword) aus = ("").PadLeft(aus.Length, '*');
                if (aus.Length > Length) aus = aus.Substring(aus.Length - Length, Length);
                spriteBatch.DrawString(textfont, aus, stringPos, Color.Black);
            }
            else
            {
                spriteBatch.Draw(textbox, new Rectangle((int)pos.X, (int)pos.Y, (int)width + 2 * offsetx, (int)height + 2 * offsety), new Rectangle(0, 0, textbox.Width, textbox.Height), BorderColor);
                spriteBatch.Draw(textbox, new Rectangle((int)pos.X + border, (int)(pos.Y + 0.5f * border), (int)width + 2 * offsetx - 2 * border, (int)(height + 2 * offsety - border - 0.5f * border)), new Rectangle(0, 0, textbox.Width, textbox.Height), Color.White);
                String aus = content;
                if (aus.Length > Length) aus = aus.Substring(aus.Length - Length, Length);
                spriteBatch.DrawString(textfont, aus, stringPos, Color.Black);
            }
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void Verstecken()
        {
            Sichtbar = false;
        }

        /// <summary>
        /// Inits the specified pos.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="content">The content.</param>
        /// <param name="_Length">Length of the _.</param>
        /// <param name="_offsetx">The _offsetx.</param>
        /// <param name="_offsety">The _offsety.</param>
        /// <param name="_border">The _border.</param>
        /// <param name="_BorderColor">Color of the _ border.</param>
        /// <param name="_textfont">The _textfont.</param>
        /// <param name="_ispassword">if set to <c>true</c> [_ispassword].</param>
        public void Initialisieren(Vector2 pos, string content, int _Length, int _offsetx, int _offsety, int _border, Color _BorderColor, SpriteFont _textfont, bool _ispassword)
        {
            ispassword = _ispassword;
            textfont = _textfont;
            this.pos = pos;
            this.content = content;
            // stringPos = pos + new Vector2(10, (textbox.Height - Texturen.font2.MeasureString(content).Y) / 2);
            height = (int)textfont.MeasureString(" ").Y;
            width = (int)textfont.MeasureString(("").PadLeft(_Length, ' ')).X;
            stringPos = pos + new Vector2(10, 5);
            Length = _Length;
            border = _border;
            BorderColor = _BorderColor;

            item = new Rectangle((int)pos.X + border, (int)((float)pos.Y + border), (int)width + 2 * offsetx - 2 * border, (int)(height + 2 * offsety - border - 0.5f * border));
        }

        /// <summary>
        /// Mouses the keys.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool mouseKeys()
        {
            if (!Sichtbar) return false;
            if (Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (Help.GetMouseState().X >= item.Left && Help.GetMouseState().X <= item.Right && Help.GetMouseState().Y >= item.Top && Help.GetMouseState().Y <= item.Bottom)
                {
                    Ausgewählt = true;
                }
                else Ausgewählt = false;
            }
            return Ausgewählt;
        }

        /// <summary>
        /// Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Sichtbar) return;
            e.Handled = true;
            if (Ausgewählt)
            {
                if (e.KeyChar == 8 && input.Length != 0)
                {
                    input = input.Substring(0, input.Length - 1);
                }
                else
                    if (e.KeyChar >= 32 && e.KeyChar <= 126)
                    {
                        input += e.KeyChar.ToString();
                    }
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Zurücksetzen()
        {
            input = "";
            Ausgewählt = false;
        }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void Anzeigen()
        {
            Sichtbar = true;
        }
    }
}