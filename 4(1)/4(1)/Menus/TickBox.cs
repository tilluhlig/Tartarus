// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-21-2013
// ***********************************************************************
// <copyright file="TickBox.cs" company="">
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
    ///     Class TickBox
    /// </summary>
    internal class TickBox
    {
        #region Vars

        /// <summary>
        ///     The on off
        /// </summary>
        private readonly Texture2D[] onOff;

        /// <summary>
        ///     The scale
        /// </summary>
        private readonly float scale;

        /// <summary>
        ///     The selected
        /// </summary>
        private Color FarbeAusgewählt;

        /// <summary>
        ///     The unselected
        /// </summary>
        private Color FarbeNichtAusgewählt;

        /// <summary>
        ///     The is on
        /// </summary>
        public bool isOn;

        /// <summary>
        ///     The itembox
        /// </summary>
        private BoundingBox itembox;

        /// <summary>
        ///     The own pos
        /// </summary>
        public Vector2 ownPos;

        /// <summary>
        ///     The visible
        /// </summary>
        public bool visible = false;

        #endregion Vars

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TickBox" /> class.
        /// </summary>
        /// <param name="selected">The selected.</param>
        /// <param name="unselected">The unselected.</param>
        /// <param name="ownPos">The own pos.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="isOn">if set to <c>true</c> [is on].</param>
        public TickBox(Color selected, Color unselected, Vector2 ownPos, float scale, bool isOn)
        {
            FarbeAusgewählt = selected;
            FarbeNichtAusgewählt = unselected;
            this.scale = scale;
            this.ownPos = ownPos;
            this.isOn = isOn;

            onOff = new Texture2D[2];
            onOff[0] = Texturen.tickboxOn;
            onOff[1] = Texturen.tickboxOff;

            itembox = new BoundingBox(new Vector3(ownPos.X, ownPos.Y, 0),
                new Vector3(ownPos.X + scale*onOff[1].Width, ownPos.Y + onOff[1].Height*scale, 0));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Shows this instance.
        /// </summary>
        public void Anzeigen()
        {
            visible = true;
        }

        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;
            if (isOn)
            {
                spriteBatch.Draw(onOff[0], ownPos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(onOff[1], ownPos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        ///     Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool MouseKeys(MouseState mouseState)
        {
            if (!visible) return isOn;
            if (mouseState.LeftButton != Help.GetMouseState().LeftButton)
            {
                if (itembox.Contains(new Vector3(mouseState.X, mouseState.Y, 0)) == ContainmentType.Contains &&
                    Help.GetMouseState().LeftButton == ButtonState.Pressed)
                {
                    if (isOn)
                        isOn = false;
                    else
                        isOn = true;
                }
            }
            return isOn;
        }

        /// <summary>
        ///     Hides this instance.
        /// </summary>
        public void Verstecken()
        {
            visible = false;
        }

        #endregion Methods
    }
}