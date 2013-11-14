// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-22-2013
// ***********************************************************************
// <copyright file="Kurzmeldung.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    /// Class Kurzmeldung
    /// </summary>
    public static class Kurzmeldung
    {
        /// <summary>
        /// The content
        /// </summary>
        private static List<string> Inhalt = new List<string>();

        /// <summary>
        /// The has existed
        /// </summary>
        private static List<int> Lebenszeit = new List<int>();

        /// <summary>
        /// The pos
        /// </summary>
        private static List<Vector2> Position = new List<Vector2>();

        /// <summary>
        /// The text color
        /// </summary>
        private static List<Color> Textfarbe = new List<Color>();

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public static void Aktualisieren()
        {
            for (int i = 0; i < Inhalt.Count; i++)
            {
                if (Lebenszeit[i] > 150)
                {
                    Entfernen(i);
                    i--;
                    continue;
                }
                Lebenszeit[i]++;
                Position[i] -= new Vector2(0, 0.5f);
                Textfarbe[i] = new Color(Textfarbe[i].R, Textfarbe[i].G, Textfarbe[i].B, Textfarbe[i].A - 1);
            }
        }

        /// <summary>
        /// Adds the specified _content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_pos">The _pos.</param>
        /// <param name="_textColor">Color of the _text.</param>
        public static void Hinzufügen(string _content, Vector2 _pos, Color _textColor)
        {
            Inhalt.Add(_content);
            Position.Add(_pos);
            Textfarbe.Add(_textColor);
            Lebenszeit.Add(0);
        }

        public static void Hinzufügen(string _content, Vector2 _pos, Color _textColor, int initlebenszeit)
        {
            Inhalt.Add(_content);
            Position.Add(_pos);
            Textfarbe.Add(_textColor);
            Lebenszeit.Add(initlebenszeit);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        /// <param name="minX">The min X.</param>
        /// <param name="maxX">The max X.</param>
        public static void Zeichnen(SpriteBatch spriteBatch, SpriteFont font, int minX, int maxX)
        {
            for (int i = 0; i < Inhalt.Count; i++)
                if (Position[i].X >= minX && Position[i].X <= maxX)
                    spriteBatch.DrawString(font, Inhalt[i], new Vector2(Position[i].X - minX, Position[i].Y), Textfarbe[i]);
        }

        /// <summary>
        /// Deletes the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
        private static void Entfernen(int i)
        {
            Inhalt.RemoveAt(i);
            Position.RemoveAt(i);
            Lebenszeit.RemoveAt(i);
            Textfarbe.RemoveAt(i);
        }
    }
}