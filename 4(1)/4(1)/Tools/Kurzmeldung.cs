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
    /// die Klasse verwaltet Kurzmeldungen, auf dem Bildschirm für eine bestimmte Zeit
    /// angezeigt werden
    /// </summary>
    public static class Kurzmeldung
    {
        /// <summary>
        /// der Inhalt der Kurzmeldung / der Text
        /// </summary>
        private static List<string> Inhalt = new List<string>();

        /// <summary>
        /// wielange die Kurzmeldung angezeigt werden soll, bevor sie verschwindet
        /// </summary>
        private static List<int> Lebenszeit = new List<int>();

        /// <summary>
        /// die Position der Kurzmeldung
        /// </summary>
        private static List<Vector2> Position = new List<Vector2>();

        /// <summary>
        /// die Farbe des Textes
        /// </summary>
        private static List<Color> Textfarbe = new List<Color>();

        /// <summary>
        /// Aktualisiert die Lebenszeit, Farbe und Position aller Kurzmeldungen
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
        /// fügt eine Kurzmeldung ein
        /// </summary>
        /// <param name="_content">der Text</param>
        /// <param name="_pos">die Position</param>
        /// <param name="_textColor">die Farbe</param>
        public static void Hinzufügen(string _content, Vector2 _pos, Color _textColor)
        {
            Inhalt.Add(_content);
            Position.Add(_pos);
            Textfarbe.Add(_textColor);
            Lebenszeit.Add(0);
        }

        /// <summary>
        /// fügt eine Kurzmeldung ein
        /// </summary>
        /// <param name="_content">der Text</param>
        /// <param name="_pos">die Position</param>
        /// <param name="_textColor">die Farbe</param>
        /// <param name="initlebenszeit">die initale Lebenszeit</param>
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
        /// entfernt eine bestimmte Kurzmeldung
        /// </summary>
        /// <param name="i">die ID</param>
        private static void Entfernen(int i)
        {
            Inhalt.RemoveAt(i);
            Position.RemoveAt(i);
            Lebenszeit.RemoveAt(i);
            Textfarbe.RemoveAt(i);
        }
    }
}