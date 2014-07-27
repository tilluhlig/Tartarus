// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-17-2013
// ***********************************************************************
// <copyright file="Button.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     Class Button
    /// </summary>
    public class Button
    {
        #region Fields

        /// <summary>
        ///     The button
        /// </summary>
        private readonly Texture2D button;

        /// <summary>
        ///     The description
        /// </summary>
        private readonly string description;

        /// <summary>
        ///     The description2
        /// </summary>
        private readonly string description2 = "";

        /// <summary>
        ///     The font
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        ///     The string pos
        /// </summary>
        private readonly Vector2 stringPos;

        /// <summary>
        ///     The string pos2
        /// </summary>
        private readonly Vector2 stringPos2 = Vector2.Zero;

        /// <summary>
        ///     The zwei strings
        /// </summary>
        private readonly bool zweiStrings;

        /// <summary>
        ///     The button box
        /// </summary>
        public BoundingBox ButtonBox;

        /// <summary>
        ///     The own pos
        /// </summary>
        public Vector2 ownPos;

        /// <summary>
        ///     The selected
        /// </summary>
        public bool selected;

        /// <summary>
        ///     The visible
        /// </summary>
        private bool visible;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Button" /> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="description">The description.</param>
        /// <param name="font">The font.</param>
        public Button(Texture2D button, Vector2 pos, string description, SpriteFont font)
        {
            this.button = button;
            this.description = description;
            ownPos = pos;
            this.font = font;

            ButtonBox = new BoundingBox(new Vector3(pos, 0),
                new Vector3(pos.X + button.Width, pos.Y + button.Height - 10, 0));
            stringPos = pos +
                        new Vector2((button.Width - font.MeasureString(description).X) / 2,
                            (button.Height - font.MeasureString(description).Y) / 2);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Button" /> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="description1">The description1.</param>
        /// <param name="description2">The description2.</param>
        /// <param name="font">The font.</param>
        public Button(Texture2D button, Vector2 pos, string description1, string description2, SpriteFont font)
        {
            this.button = button;
            description = description1;
            this.description2 = description2;
            ownPos = pos;
            this.font = font;

            ButtonBox = new BoundingBox(new Vector3(pos, 0),
                new Vector3(pos.X + button.Width, pos.Y + button.Height - 10, 0));
            stringPos = pos +
                        new Vector2((button.Width - font.MeasureString(description).X) / 2,
                            button.Height / 2 - font.MeasureString(description).Y);
            stringPos2 = pos + new Vector2((button.Width - font.MeasureString(description2).X) / 2, button.Height / 2);

            zweiStrings = true;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="Cselected">The cselected.</param>
        /// <param name="unselected">The unselected.</param>
        public void Draw(SpriteBatch spriteBatch, Color Cselected, Color unselected)
        {
            if (selected)
                spriteBatch.Draw(button, ownPos, Cselected);
            else
                spriteBatch.Draw(button, ownPos, unselected);

            if (zweiStrings)
            {
                if (selected)
                {
                    spriteBatch.DrawString(font, description, stringPos, Color.Black);
                    spriteBatch.DrawString(font, description2, stringPos2, Color.Black);
                }
                else
                {
                    spriteBatch.DrawString(font, description, stringPos, Color.Black);
                    spriteBatch.DrawString(font, description2, stringPos2, Color.Black);
                }
            }
            else
            {
                if (selected)
                    spriteBatch.DrawString(font, description, stringPos, Color.Black);
                else
                    spriteBatch.DrawString(font, description, stringPos, Color.Black);
            }
        }

        /// <summary>
        ///     Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
        }

        /// <summary>
        ///     Mouses the keys.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool MouseKeys()
        {
            //gibt true zurück, wenn gedrückt
            if (ButtonBox.Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                ContainmentType.Contains)
            {
                selected = true;
                if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    return true;
            }
            else
            {
                selected = false;
            }
            return false;
        }

        /// <summary>
        ///     Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
        }

        #endregion Methods
    }
}