// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-24-2013
// ***********************************************************************
// <copyright file="Destruction_Object.cs" company="">
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
    ///     Class Destruction_Object
    /// </summary>
    public class ZerstörungsObjekt
    {
        #region Fields

        /// <summary>
        ///     The bild_ width
        /// </summary>
        public int BildBreite = 0;

        /// <summary>
        ///     The bild_ height
        /// </summary>
        public int BildHöhe = 0;

        /// <summary>
        ///     The rotateable
        /// </summary>
        public bool Drehbar = false;

        /// <summary>
        ///     The scale
        /// </summary>
        public float Skalierung = 1.0f;

        /// <summary>
        ///     The overreachable
        /// </summary>
        public bool Spiegelbar = false;

        /// <summary>
        ///     The centered
        /// </summary>
        public bool Zentriert = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ZerstörungsObjekt" /> class.
        /// </summary>
        /// <param name="_Bild_Width">Width of the _ bild_.</param>
        /// <param name="_Bild_Height">Height of the _ bild_.</param>
        /// <param name="_scale">The _scale.</param>
        /// <param name="_centered">if set to <c>true</c> [_centered].</param>
        /// <param name="_overreachable">if set to <c>true</c> [_overreachable].</param>
        /// <param name="_rotateable">if set to <c>true</c> [_rotateable].</param>
        public ZerstörungsObjekt(int _Bild_Width, int _Bild_Height, float _scale, bool _centered, bool _overreachable,
            bool _rotateable)
        {
            BildBreite = _Bild_Width;
            BildHöhe = _Bild_Height;
            Skalierung = _scale;
            Zentriert = _centered;
            Spiegelbar = _overreachable;
            Drehbar = _rotateable;
        }

        public ZerstörungsObjekt()
        {
        }

        #endregion Constructors

        #region Methods

        public static ZerstörungsObjekt Laden(List<String> Text, ZerstörungsObjekt Objekt)
        {
            ZerstörungsObjekt temp = Objekt;
            if (temp == null) temp = new ZerstörungsObjekt();

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "ZERSTÖRUNGSOBJEKT");

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.Zentriert = TextLaden.LadeBool(Liste, "Zentriert", temp.Zentriert);
            temp.Spiegelbar = TextLaden.LadeBool(Liste, "Spiegelbar", temp.Spiegelbar);
            temp.Skalierung = TextLaden.LadeFloat(Liste, "Skalierung", temp.Skalierung);
            temp.Drehbar = TextLaden.LadeBool(Liste, "Drehbar", temp.Drehbar);
            temp.BildHöhe = TextLaden.LadeInt(Liste, "BildHöhe", temp.BildHöhe);
            temp.BildBreite = TextLaden.LadeInt(Liste, "BildBreite", temp.BildBreite);

            return temp;
        }

        /// <summary>
        ///     Explodes the specified bild.
        /// </summary>
        /// <param name="Bild">The bild.</param>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        /// <param name="Object_Position">The object_ position.</param>
        /// <returns>System.Int32.</returns>
        public int BerechneZerstörung(Texture2D Bild, Vector2 Explosion, int Energie, Vector2 Object_Position)
        {
            return BerechneZerstörung(Bild, Explosion, Energie, Object_Position, false, 0, null);
        }

        public int BerechneZerstörung(Texture2D Bild, Vector2 Explosion, int Energie, Vector2 Object_Position, List<Vector3> Bereiche)
        {
            return BerechneZerstörung(Bild, Explosion, Energie, Object_Position, false, 0, Bereiche);
        }

        public int BerechneZerstörung(Texture2D Bild, Vector2 Explosion, int Energie, Vector2 Object_Position,
            bool overreach, float angle)
        {
            return BerechneZerstörung(Bild, Explosion, Energie, Object_Position,
             overreach, angle, null);
        }

        /// <summary>
        ///     Explodes the specified bild.
        /// </summary>
        /// <param name="Bild">The bild.</param>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        /// <param name="Object_Position">The object_ position.</param>
        /// <param name="overreach">if set to <c>true</c> [overreach].</param>
        /// <param name="angle">The angle.</param>
        /// <returns>System.Int32.</returns>
        public int BerechneZerstörung(Texture2D Bild, Vector2 Explosion, int Energie, Vector2 Object_Position,
            bool overreach, float angle, List<Vector3> Bereiche)
        {
            // Triviale ignorieren (zu weit weg)
            var abstand = (int)Help.Abstand(new Vector2(0, 0), new Vector2(BildBreite * Skalierung, BildHöhe * Skalierung));
            if (Help.Abstand(Explosion, Object_Position) > Energie + 4 * abstand)
                return 0;
            //new Vector2(Object_Position.X + Bild_Width * scale / 2, Object_Position.Y + Bild_Height * scale / 2)

            // Rotateable korrektur
            if (Drehbar)
                Explosion = Help.RotatePosition(Object_Position, MathHelper.ToRadians(360) - angle, Explosion);

            // Bringe Object und Eindringling relativ zueinander
            var x = (int)(Explosion.X - Object_Position.X);
            var y = (int)(Explosion.Y - Object_Position.Y);

            // Korrektur für Bilder, die nach ihrem Mittelpunkt ausgerichtet wurden
            if (Zentriert) x += (int)(BildBreite * Skalierung / 2);

            // Korrektur (Bild wurde verschoben)
            y += (int)(BildHöhe * Skalierung);

            // Korrektur der Skalierung
            x = (int)(x / Skalierung);
            y = (int)(y / Skalierung);

            // Korrektur des Overreach
            if (overreach) x = BildBreite - x;

            var Picture = new Color[BildBreite * BildHöhe];
            Bild.GetData(Picture);

            // Punkte berechnen
            int found = 0;
            Energie = (int)(Energie / Skalierung);
            var aa = (int)(Math.Log((((Energie) - 0) * Math.PI), Math.E) * Math.Sqrt(Energie));
            for (int i = -aa; i < aa; i++)
            {
                int dist = i;
                if (dist < 0) dist = -dist;
                var add = (int)(Math.Log((((aa) - dist) * Math.PI), Math.E) * Math.Sqrt(Energie));

                Vector3 tempZerstoert = new Vector3(x + i, -1, -1);
                for (int b = -add; b < add; b++)
                {
                    var Delete = new Vector2(x + i, y + b);
                    if (Delete.X < 0 || Delete.Y < 0 || Delete.X >= BildBreite || Delete.Y >= BildHöhe) continue;

                    if (Picture[(int)(Delete.X + Delete.Y * BildBreite)] != Color.Transparent)
                    {
                        found++;
                        Picture[(int)(Delete.X + Delete.Y * BildBreite)] = Color.Transparent;
                        if (tempZerstoert.Y == -1)
                        {
                            tempZerstoert.Y = Delete.Y;
                            tempZerstoert.Z = Delete.Y;
                        }
                        else
                            tempZerstoert.Z++;
                    }
                }
                if (Bereiche != null && tempZerstoert.Y != -1f)
                    Bereiche.Add(tempZerstoert);
            }

            if (found > 0)
            {
                // es wurde etwas zerstört
                Bild.SetData(Picture);
                return found;
            }
            return 0;
        }

        public List<String> Speichern()
        {
            var data = new List<String>();
            data.Add("[ZERSTÖRUNGSOBJEKT]");
            data.Add("Zentriert=" + Zentriert);
            data.Add("Spiegelbar=" + Spiegelbar);
            data.Add("Skalierung=" + Skalierung);
            data.Add("Drehbar=" + Drehbar);
            data.Add("BildHöhe=" + BildHöhe);
            data.Add("BildBreite=" + BildBreite);
            data.Add("[/ZERSTÖRUNGSOBJEKT]");

            return data;
        }

        #endregion Methods
    }
}