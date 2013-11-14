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
    /// Class Materialien
    /// </summary>
    public class Materialien
    {
        /// <summary>
        /// The bild
        /// </summary>
        public Texture2D Bild;

        /// <summary>
        /// The blur
        /// </summary>
        public bool Abdunkeln = true;

        /// <summary>
        /// The C bild
        /// </summary>
        public Color[] CBild;

        /// <summary>
        /// The C farbe
        /// </summary>
        public Color CFarbe = Color.Transparent;

        /// <summary>
        /// The collision
        /// </summary>
        public bool Kollision = true;

        /// <summary>
        /// The farbe
        /// </summary>
        public bool Farbe = false;

        /// <summary>
        /// The folge ID
        /// </summary>
        public int FolgeID = 0;

        /// <summary>
        /// The scale
        /// </summary>
        public float Skalierung = 1.0f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Materialien"/> class.
        /// </summary>
        /// <param name="Content">The content.</param>
        /// <param name="Bilddatei">The bilddatei.</param>
        /// <param name="_Farbe">if set to <c>true</c> [_ farbe].</param>
        /// <param name="_CFarbe">The _ C farbe.</param>
        /// <param name="_FolgeID">The _ folge ID.</param>
        /// <param name="_Skalierung">The _scale.</param>
        /// <param name="_Abdunkeln">if set to <c>true</c> [_blur].</param>
        /// <param name="_Kollision">if set to <c>true</c> [_collision].</param>
        public Materialien(ContentManager Content, String Bilddatei, bool _Farbe, Color _CFarbe, int _FolgeID, float _Skalierung, bool _Abdunkeln, bool _Kollision)
        {
            if (!_Farbe)
            {
                Skalierung = _Skalierung;
                Bild = Texturen.FromFile("Content\\Textures\\" + Bilddatei + ".jpg");//Content.Load<Texture2D>("Textures\\" + Bilddatei);
                CBild = new Color[Bild.Width * Bild.Height];
                Bild.GetData(CBild);

                RenderTarget2D rt = new RenderTarget2D(Game1.device, (int)(Bild.Width * Skalierung), (int)(Bild.Height * Skalierung));
                Game1.device.SetRenderTarget(rt);
                Rectangle rect = new Rectangle(0, 0, (int)(Bild.Width * Skalierung), (int)(Bild.Height * Skalierung));
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
    }
}