// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-24-2013
// ***********************************************************************
// <copyright file="Collision_Object.cs" company="">
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
    /// Class Collision_Object
    /// </summary>
    public class KollisionsObjekt
    {
        /// <summary>
        /// The bild
        /// </summary>
        public List<int>[] Bild = null;

        /// <summary>
        /// The bild_ width
        /// </summary>
        public int BildBreite = 0;

        /// <summary>
        /// The bild_ height
        /// </summary>
        public int BildHöhe = 0;

        /// <summary>
        /// The rotateable
        /// </summary>
        public bool Drehbar = false;

        /// <summary>
        /// The rotationpos
        /// </summary>
        public Vector2 Drehpunkt = new Vector2(0, 0);

        /// <summary>
        /// The scale
        /// </summary>
        public float Skalierung = 1.0f;

        /// <summary>
        /// The overreachable
        /// </summary>
        public bool Spiegelbar = false;

        /// <summary>
        /// The centered
        /// </summary>
        public bool Zentriert = false;

        public static KollisionsObjekt Laden(List<String> Text, KollisionsObjekt Objekt)
        {
            KollisionsObjekt temp = Objekt;
            if (temp == null) temp = new KollisionsObjekt();

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "KOLLISIONSOBJEKT");
            if (Text2.Count == 0) return Objekt;

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.BildBreite = TextLaden.LadeInt(Liste, "BildBreite", temp.BildBreite);
            temp.BildHöhe = TextLaden.LadeInt(Liste, "BildHöhe", temp.BildHöhe);
            temp.Drehbar = TextLaden.LadeBool(Liste, "Drehbar", temp.Drehbar);
            temp.Drehpunkt = TextLaden.LadeVector2(Liste, "Drehpunkt", temp.Drehpunkt);
            temp.Skalierung = TextLaden.LadeFloat(Liste, "Skalierung", temp.Skalierung);
            temp.Spiegelbar = TextLaden.LadeBool(Liste, "Spiegelbar", temp.Spiegelbar);
            temp.Zentriert = TextLaden.LadeBool(Liste, "Zentriert", temp.Zentriert);

            Text2 = TextLaden.ErmittleBereich(Text, "MASK");
            if (Text2.Count == 0) return temp;

            temp.Bild = new List<int>[temp.BildBreite];
            for (int b = 0; b < temp.BildBreite; b++)
            {
                String[] q = Text2[b].Split('-');
                for (int i = 0; i < q.Length; i++)
                    temp.Bild[b].Add(Convert.ToInt32(q[i]));
            }

            return temp;
        }

        public List<String> Speichern()
        {
            List<String> data = new List<String>();
            data.Add("[KOLLISIONSOBJEKT]");
            data.Add("BildBreite=" + BildBreite);
            data.Add("BildHöhe=" + BildHöhe);
            data.Add("Drehbar=" + Drehbar);
            data.Add("Drehpunkt=" + Drehpunkt);
            data.Add("Skalierung=" + Skalierung);
            data.Add("Spiegelbar=" + Spiegelbar);
            data.Add("Zentriert=" + Zentriert);

            data.Add("[MASK]");

            for (int b = 0; b < Bild.Length; b++)
            {
                String add = "";
                for (int c = 0; c < Bild[b].Count; c++)
                {
                    add = add + Bild[b][c].ToString();
                    if (c + 1 < Bild[b].Count)
                        add = add + ('-');
                }
                data.Add(add);
            }

            data.Add("[/MASK]");

            data.Add("[/KOLLISIONSOBJEKT]");

            return data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KollisionsObjekt"/> class.
        /// </summary>
        /// <param name="_Bild">The _ bild.</param>
        /// <param name="_BildBreite">Width of the _ bild_.</param>
        /// <param name="BildHöhe">Height of the _ bild_.</param>
        /// <param name="_Skalierung">The _scale.</param>
        /// <param name="_Spiegelbar">if set to <c>true</c> [_overreachable].</param>
        /// <param name="_Drehbar">if set to <c>true</c> [_rotateable].</param>
        /// <param name="_Zentriert">if set to <c>true</c> [_centered].</param>
        /// <param name="_Drehpunkt">The _rotationpos.</param>
        public KollisionsObjekt(List<int>[] _Bild, int _BildBreite, int _BildHöhe, float _Skalierung, bool _Spiegelbar, bool _Drehbar, bool _Zentriert, Vector2 _Drehpunkt)
        {
            //Bild = new byte[(int)Math.Ceiling((double)((_Bild.Width * _Bild.Height) / 8))];
            //Help.ConvertTexture2DToMASK(_Bild).CopyTo(Bild, 0);
            _Bild.CopyTo(Bild, 0);
            BildBreite = _BildBreite;
            BildHöhe = _BildHöhe;
            Skalierung = _Skalierung;
            Spiegelbar = _Spiegelbar;
            Drehbar = _Drehbar;
            Zentriert = _Zentriert;
            Drehpunkt = _Drehpunkt;
        }

        public KollisionsObjekt()
        { }

        public KollisionsObjekt(Texture2D _Bild, int _BildBreite, int _BildHöhe, float _Skalierung, bool _Spiegelbar, bool _Drehbar, bool _Zentriert, Vector2 _Drehpunkt)
        {
            Bild = new List<int>[_Bild.Width];
            //Bild = new byte[(int)Math.Ceiling((double)((_Bild.Width * _Bild.Height) / 8))];
            //Help.ConvertTexture2DToMASK(_Bild).CopyTo(Bild, 0);
            Help.ConvertTexture2DToMAP(_Bild).CopyTo(Bild, 0);
            BildBreite = _BildBreite;
            BildHöhe = _BildHöhe;
            Skalierung = _Skalierung;
            Spiegelbar = _Spiegelbar;
            Drehbar = _Drehbar;
            Zentriert = _Zentriert;
            Drehpunkt = _Drehpunkt;
        }

        /// <summary>
        /// Rotiert einen Vektor
        /// </summary>
        /// <param name="Winkel">The winkel.</param>
        /// <param name="u">The u.</param>
        /// <param name="BB">The BB.</param>
        /// <returns>Vector3.</returns>
        public static Vector3 Rotiere(double Winkel, Vector3 u, Vector3 BB)
        {
            double c = Math.Cos(Winkel);
            double s = Math.Sin(Winkel);
            double[] A = { (float)c + (1 - c) * u.X * u.X, (float)-s * u.Z + (1 - c) * u.X * u.Y, (float)s * u.Y + (1 - c) * u.X * u.Z };
            double[] B = { (float)s * u.Z + (1 - c) * u.X * u.Y, (float)c + (1 - c) * u.Y * u.Y, (float)-s * u.X + (1 - c) * u.Y * u.Z };
            double[] C = { (float)-s * u.Y + (1 - c) * u.X * u.Z, (float)s * u.X + (1 - c) * u.Y * u.Z, (float)c + (1 - c) * u.Z * u.Z };
            return new Vector3((float)((float)A[0] * BB.X + A[1] * BB.Y + A[2] * BB.Z), (float)((float)B[0] * BB.X + B[1] * BB.Y + B[2] * BB.Z), (float)((float)C[0] * BB.X + C[1] * BB.Y + C[2] * BB.Z));
        }

        /// <summary>
        /// Rotates the specified winkel.
        /// </summary>
        /// <param name="Winkel">The winkel.</param>
        /// <param name="u">The u.</param>
        /// <param name="B">The B.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 Rotiere(double Winkel, Vector3 u, Vector2 B)
        {
            Vector3 B2 = Rotiere(Winkel, u, new Vector3(B.X, B.Y, 1));
            return new Vector2(B2.X, B2.Y);
        }

        /// <summary>
        /// Collisions the specified incoming_ position.
        /// </summary>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <param name="Object_Position">The object_ position.</param>
        /// <param name="angle">The angle.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position, float angle)
        {
            return collision(Incoming_Position, Object_Position, angle, false);
        }

        /// <summary>
        /// Collisions the specified incoming_ position.
        /// </summary>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <param name="Object_Position">The object_ position.</param>
        /// <param name="overreach">if set to <c>true</c> [overreach].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position, bool overreach)
        {
            return collision(Incoming_Position, Object_Position, 0, overreach);
        }

        /// <summary>
        /// Collisions the specified incoming_ position.
        /// </summary>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <param name="Object_Position">The object_ position.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position)
        {
            return collision(Incoming_Position, Object_Position, 0, false);
        }

        /// <summary>
        /// Collisions the specified incoming_ position.
        /// </summary>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <param name="Object_Position">The object_ position.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="overreach">if set to <c>true</c> [overreach].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position, float angle, bool overreach)
        {
            if (Bild == null) return false;

            // Rotateable korrektur
            if (Drehbar)
                Incoming_Position = Help.RotatePosition(Object_Position, MathHelper.ToRadians(360) - angle, Incoming_Position);

            // Bringe Object und Eindringling relativ zueinander
            int x = (int)(Incoming_Position.X - Object_Position.X);
            int y = (int)(Incoming_Position.Y - Object_Position.Y);

            // Korrektur für Bilder, die nach ihrem Mittelpunkt ausgerichtet wurden
            if (Zentriert) x += (int)(BildBreite * Skalierung / 2);

            // Korrektur (Bild wurde verschoben)
            y += (int)(BildHöhe * Skalierung);

            // eigentliche Kollisionsabfrage
            return isSet(x, y, overreach);
        }

        /// <summary>
        /// Determines whether the specified x is set.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="overreach">if set to <c>true</c> [overreach].</param>
        /// <returns><c>true</c> if the specified x is set; otherwise, <c>false</c>.</returns>
        public bool isSet(int x, int y, bool overreach)
        {
            if (x < 0 || y < 0) return false;
            if (x >= BildBreite * Skalierung || y >= BildHöhe * Skalierung) return false;
            if (Bild == null) return false;

            // Korrektur der Skalierung
            x = (int)((float)x / Skalierung);
            y = (int)((float)y / Skalierung);

            // Korrektur des Overreach
            if (overreach) x = BildBreite - x;

            if (!Help.isSet(Bild, x, y)) return false;
            //if (!Help.isSetInMask(Bild, x, y, Bild_Width)) return false;
            return true;
        }

        /// <summary>
        /// Uses the mask on texture2 D.
        /// </summary>
        /// <param name="_Bild">The _ bild.</param>
        /// <returns>Texture2D.</returns>
        public Texture2D UseMaskOnTexture2D(Texture2D _Bild)
        {
            if (Bild == null) return null;
            Color[] Data = new Color[_Bild.Width * _Bild.Height];
            _Bild.GetData(Data);

            for (int i = 0; i < _Bild.Width; i++)
                for (int b = 0; b < _Bild.Height; b++)
                {
                    if (!Help.isSet(Bild, i, b))
                    {
                        Data[i + b * _Bild.Width] = Color.Transparent;
                    }
                }

            // Color[] Data2 = new Color[_Bild.Width * _Bild.Height];
            Texture2D result = new Texture2D(Game1.device, _Bild.Width, _Bild.Height);
            result.SetData(Data);
            return result;
        }
    }
}