﻿// ***********************************************************************
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
    ///     Dies Klasse verwaltet Schaltflächen
    /// </summary>
    public class Button
    {
        #region Fields

        /// <summary>
        ///     die Textur der Schaltfläche
        /// </summary>
        private readonly Texture2D button;

        /// <summary>
        ///     erste Beschriftungszeile
        /// </summary>
        private readonly string description;

        /// <summary>
        ///    zweite Beschriftungszeile
        /// </summary>
        private readonly string description2 = "";

        /// <summary>
        ///     die verwendete Schriftart
        /// </summary>
        private readonly SpriteFont font;

        /// <summary>
        ///     die Position der ersten Zeile
        /// </summary>
        private readonly Vector2 stringPos;

        /// <summary>
        ///     die Position der zweiten Zeile
        /// </summary>
        private readonly Vector2 stringPos2 = Vector2.Zero;

        /// <summary>
        ///     zwei Beschriftungszeilen? true = ja, false = nein
        /// </summary>
        private readonly bool zweiStrings;

        /// <summary>
        ///     die Kollisionsbox
        /// </summary>
        public BoundingBox ButtonBox;

        /// <summary>
        ///     die Position des Buttons
        /// </summary>
        public Vector2 ownPos;

        /// <summary>
        ///     selektiert, true = ja
        /// </summary>
        public bool selected;

        /// <summary>
        ///     Sichtbarkeit, true = sichtbar, false = unsichtar
        /// </summary>
        private bool visible;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     initialisiert eine neue Schaltfläche
        /// </summary>
        /// <param name="button">die Textur</param>
        /// <param name="pos">die Position</param>
        /// <param name="description">die Bezeichnung</param>
        /// <param name="font">die Schriftart</param>
        public Button(Texture2D button, Vector2 pos, string description, SpriteFont font)
        {
            this.button = button;
            this.description = description;
            ownPos = pos;
            this.font = font;

            ButtonBox = new BoundingBox(new Vector3(pos, 0),
                new Vector3(pos.X + button.Width, pos.Y + button.Height - 10, 0));
            stringPos = pos +
                        new Vector2((button.Width - font.MeasureString(description).X)/2,
                            (button.Height - font.MeasureString(description).Y)/2);
        }

        /// <summary>
        ///     initialisiert eine neue Schaltfläche
        /// </summary>
        /// <param name="button">die Textur</param>
        /// <param name="pos">dir Position</param>
        /// <param name="description1">die erste Beschriftungszeile der Schaltfläche</param>
        /// <param name="description2">die zweite Beschriftungszeile der Schaltfläche</param>
        /// <param name="font">die Schriftart</param>
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
                        new Vector2((button.Width - font.MeasureString(description).X)/2,
                            button.Height/2 - font.MeasureString(description).Y);
            stringPos2 = pos + new Vector2((button.Width - font.MeasureString(description2).X)/2, button.Height/2);

            zweiStrings = true;
        }

        #endregion Constructors

        #region Methods

        public void Draw(SpriteBatch spriteBatch, Color Cselected, Color unselected)
        {
            Draw(spriteBatch, Cselected, unselected, 1);
        }

        /// <summary>
        ///     Zeichnet die Schaltfläche
        /// </summary>
        /// <param name="spriteBatch">eine Zeichenfläche</param>
        /// <param name="Cselected">die Farbe, wenn der Button ausgewählt ist</param>
        /// <param name="unselected">eine Farbe, für nicht ausgewählte Buttons</param>
        public void Draw(SpriteBatch spriteBatch, Color Cselected, Color unselected, float Transparenz)
        {
            if (selected)
                spriteBatch.Draw(button, ownPos, Cselected);
            else
                spriteBatch.Draw(button, ownPos, unselected);

            if (zweiStrings)
            {
                if (selected)
                {
                    spriteBatch.DrawString(font, description, stringPos, Color.Black * Transparenz);
                    spriteBatch.DrawString(font, description2, stringPos2, Color.Black * Transparenz);
                }
                else
                {
                    spriteBatch.DrawString(font, description, stringPos, Color.Black * Transparenz);
                    spriteBatch.DrawString(font, description2, stringPos2, Color.Black * Transparenz);
                }
            }
            else
            {
                if (selected)
                    spriteBatch.DrawString(font, description, stringPos, Color.Black * Transparenz);
                else
                    spriteBatch.DrawString(font, description, stringPos, Color.Black * Transparenz);
            }
        }

        /// <summary>
        ///     macht den Button unsichtbar
        /// </summary>
        public void hide()
        {
            visible = false;
        }

        /// <summary>
        ///     Prüft Interaktionen mit der Maus
        /// </summary>
        /// <returns>true = der Button wurde gedrückt, false = nichts</returns>
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
        ///     macht den Button sichtbar
        /// </summary>
        public void show()
        {
            visible = true;
        }

        #endregion Methods
    }
}