// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-10-2013
// ***********************************************************************
// <copyright file="Materialien.cs">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse ermöglicht die Definition von Materialien
    /// </summary>
    public class Materialien
    {
        #region Fields

        /// <summary>
        ///     ob der Rand des Materials abgedunkelt werden soll (beispielsweise zur Luft hin)
        /// </summary>
        public bool Abdunkeln = true;

        /// <summary>
        ///     die Textrur des Materials
        /// </summary>
        public Texture2D Bild;

        /// <summary>
        ///     die Textur des Materials als Array von Farbwerten (Farbe=false)
        /// </summary>
        public Color[] CBild;

        /// <summary>
        ///     wenn das Material lediglich als Farbe angegeben wird (Farbe=true), wird diese Farbe genutzt
        /// </summary>
        public Color CFarbe = Color.Transparent;

        /// <summary>
        ///     ob es als Farbwert definiert wird
        ///     Farbe=true, nutzt CFarbe
        ///     Farbe=false, nutzt CBild
        /// </summary>
        public bool Farbe = false;

        /// <summary>
        ///     die ID des Materials welches nach Zerstörung dieses Materials gesetzt werden soll
        /// </summary>
        public int FolgeID = 0;

        /// <summary>
        ///     ob dieses Material Kollisionen auslöst
        /// </summary>
        public bool Kollision = true;

        /// <summary>
        ///     die Skalierung der Materialtextur
        /// </summary>
        public float Skalierung = 1.0f;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     der Konstruktur, zur erzeugung eines neuen Materials
        /// </summary>
        /// <param name="Content">ein ContentManager</param>
        /// <param name="Bilddatei">der Name der Bilddatei</param>
        /// <param name="_Farbe">ob der _CFarbe oder die Bilddatei die Textur liefern soll</param>
        /// <param name="_CFarbe">ein Farbwert als Textur</param>
        /// <param name="_FolgeID">die Folgetextur (nach entfernung dieser)</param>
        /// <param name="_Skalierung">die Skalierung der Textur (bei nutzung der Bilddatei als Textur)</param>
        /// <param name="_Abdunkeln">ob der Rand des Materials zur Luft abgedunkelt werden soll</param>
        /// <param name="_Kollision">ob das Material eine Kollision auslösen kann</param>
        public Materialien(ContentManager Content, String Bilddatei, bool _Farbe, Color _CFarbe, int _FolgeID,
            float _Skalierung, bool _Abdunkeln, bool _Kollision)
        {
            if (!_Farbe)
            {
                Skalierung = _Skalierung;
                Bild = Texturen.FromFile("Content\\Textures\\" + Bilddatei + ".jpg");
                //Content.Load<Texture2D>("Textures\\" + Bilddatei);
                CBild = new Color[Bild.Width * Bild.Height];
                Bild.GetData(CBild);

                var rt = new RenderTarget2D(Game1.device, (int)(Bild.Width * Skalierung), (int)(Bild.Height * Skalierung));
                Game1.device.SetRenderTarget(rt);
                var rect = new Rectangle(0, 0, (int)(Bild.Width * Skalierung), (int)(Bild.Height * Skalierung));
                Game1.spriteBatch.Begin();
                Game1.device.Clear(Color.Transparent);
                Game1.spriteBatch.Draw(Bild, rect, Color.White);
                Game1.spriteBatch.End();
                Game1.device.SetRenderTarget(null);
                Bild = rt;
                Skalierung = 1.0f;
            }

            Abdunkeln = _Abdunkeln;
            Kollision = _Kollision;
            FolgeID = _FolgeID;
            Farbe = _Farbe;
            CFarbe = _CFarbe;
        }

        #endregion Constructors
    }
}