// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="Umriss.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     Hilfsklasse zur Erstellung von Umrissen für 2D Bilder
    /// </summary>
    public static class Umriss
    {
        #region Methods

        /// <summary>
        ///     Erstellt einen Umriss eines 2D Bildes
        /// </summary>
        /// <param name="Bild">Das Bild</param>
        /// <param name="Farbe">Die Farbe des Umrisses</param>
        /// <param name="Breite">die Pixelbreite des Umrisses</param>
        /// <returns>Gibt die 2D Textur des Umrisses zurück</returns>
        public static Texture2D Generieren(Texture2D Bild, Color Farbe, int Breite)
        {
            if (Bild == null) return null;
            var Data = new Color[Bild.Width*Bild.Height];
            Bild.GetData(Data);
            var Data2 = new Color[Bild.Width*Bild.Height];

            for (int i = 0; i < Bild.Width; i++)
                for (int b = 0; b < Bild.Height; b++)
                {
                    bool found = false;
                    if (i > Breite - 1 && Data[i - Breite + b*Bild.Width] == Color.Transparent) found = true;
                    if (i <= Breite - 1) found = true;
                    if (i < Bild.Width - Breite && Data[i + Breite + b*Bild.Width] == Color.Transparent) found = true;
                    if (i >= Bild.Width - Breite) found = true;
                    if (b > Breite - 1 && Data[i + (b - Breite)*Bild.Width] == Color.Transparent) found = true;
                    if (b <= Breite - 1) found = true;
                    if (b < Bild.Height - Breite && Data[i + (b + Breite)*Bild.Width] == Color.Transparent)
                        found = true;
                    if (b >= Bild.Height - Breite) found = true;

                    if (Data[i + b*Bild.Width] != Color.Transparent && found)
                    {
                        Data2[i + b*Bild.Width] = Farbe;
                    }
                    else
                        Data2[i + b*Bild.Width] = Color.Transparent;
                }

            var result = new Texture2D(Game1.device, Bild.Width, Bild.Height);
            result.SetData(Data2);
            return result;
        }

        #endregion Methods
    }
}