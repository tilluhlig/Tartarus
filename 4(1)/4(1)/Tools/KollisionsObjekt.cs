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
    ///     ermöglicht die Kollisionsprüfung für Objekte
    /// </summary>
    public class KollisionsObjekt
    {
        #region Fields

        /// <summary>
        ///     die Texturmaske
        /// </summary>
        public List<int>[] Bild = null;

        /// <summary>
        ///     die Bildbreite
        /// </summary>
        public int BildBreite = 0;

        /// <summary>
        ///     die Bildhöhe
        /// </summary>
        public int BildHöhe = 0;

        /// <summary>
        ///     ist das Objekt drehbar?
        /// </summary>
        public bool Drehbar = false;

        /// <summary>
        ///     der Drehpunkt
        /// </summary>
        public Vector2 Drehpunkt = new Vector2(0, 0);

        /// <summary>
        ///     die Skalierung der Textur
        /// </summary>
        public float Skalierung = 1.0f;

        /// <summary>
        ///     ist das Objekt spiegelbar?
        /// </summary>
        public bool Spiegelbar = false;

        /// <summary>
        ///     wird die Textur zentriert?
        /// </summary>
        public bool Zentriert = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues KollisionsObjekt
        /// </summary>
        /// <param name="_Bild">die Bildmaske</param>
        /// <param name="_BildBreite">die Bildbreite</param>
        /// <param name="_BildHöhe">die Bildhöhe</param>
        /// <param name="_Skalierung">die Skalierung</param>
        /// <param name="_Spiegelbar">ist das Objekt spiegelbar</param>
        /// <param name="_Drehbar">ist das Objekt drehbar</param>
        /// <param name="_Zentriert">wird die Textur zentriert</param>
        /// <param name="_Drehpunkt">ist das Objekt drehbar</param>
        public KollisionsObjekt(List<int>[] _Bild, int _BildBreite, int _BildHöhe, float _Skalierung, bool _Spiegelbar,
            bool _Drehbar, bool _Zentriert, Vector2 _Drehpunkt)
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

        /// <summary>
        /// Erzeugt ein neues KollisionsObjekt
        /// </summary>
        public KollisionsObjekt()
        {
        }

        /// <summary>
        /// Erzeugt ein neues KollisionsObjekt
        /// </summary>
        /// <param name="_Bild">die Textur</param>
        /// <param name="_BildBreite">die Bildbreite</param>
        /// <param name="_BildHöhe">die Bildhöhe</param>
        /// <param name="_Skalierung">die Skalierung</param>
        /// <param name="_Spiegelbar">ist das Objekt spiegelbar</param>
        /// <param name="_Drehbar">ist das Objekt drehbar</param>
        /// <param name="_Zentriert">wird die Textur zentriert</param>
        /// <param name="_Drehpunkt">ist das Objekt drehbar</param>
        public KollisionsObjekt(Texture2D _Bild, int _BildBreite, int _BildHöhe, float _Skalierung, bool _Spiegelbar,
            bool _Drehbar, bool _Zentriert, Vector2 _Drehpunkt)
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

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Erstellt ein KollisionsObjekt aus einer Textdarstellung
        /// </summary>
        /// <param name="Text">die Textdarstellung des Objektes</param>
        /// <param name="Objekt">null = erzeuge neues Objekt, sonst = wende es auf dieses Objekt an</param>
        /// <returns></returns>
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

            Text2 = TextLaden.ErmittleBereich(Text2, "MASK");
            if (Text2.Count == 0) return temp;

            temp.Bild = new List<int>[temp.BildBreite];
            for (int b = 0; b < temp.BildBreite; b++)
            {
                temp.Bild[b] = new List<int>();
                String[] q = Text2[b].Split('-');
                for (int i = 0; i < q.Length; i++)
                    temp.Bild[b].Add(Convert.ToInt32(q[i]));
            }

            return temp;
        }

        /// <summary>
        ///     Rotiert einen Vektor
        /// </summary>
        /// <param name="Winkel">der Rotationswinkel</param>
        /// <param name="u">die Rotationsachse</param>
        /// <param name="BB">der Vektor, der gedreht werden soll</param>
        /// <returns>Vector3.</returns>
        public static Vector3 Rotiere(double Winkel, Vector3 u, Vector3 BB)
        {
            double c = Math.Cos(Winkel);
            double s = Math.Sin(Winkel);
            double[] A =
            {
                (float) c + (1 - c)*u.X*u.X, (float) -s*u.Z + (1 - c)*u.X*u.Y, (float) s*u.Y + (1 - c)*u.X*u.Z
            };
            double[] B =
            {
                (float) s*u.Z + (1 - c)*u.X*u.Y, (float) c + (1 - c)*u.Y*u.Y, (float) -s*u.X + (1 - c)*u.Y*u.Z
            };
            double[] C =
            {
                (float) -s*u.Y + (1 - c)*u.X*u.Z, (float) s*u.X + (1 - c)*u.Y*u.Z, (float) c + (1 - c)*u.Z*u.Z
            };
            return new Vector3((float) ((float) A[0]*BB.X + A[1]*BB.Y + A[2]*BB.Z),
                (float) ((float) B[0]*BB.X + B[1]*BB.Y + B[2]*BB.Z), (float) ((float) C[0]*BB.X + C[1]*BB.Y + C[2]*BB.Z));
        }

        /// <summary>
        ///     Dreht einen Vektor
        /// </summary>
        /// <param name="Winkel">der Drehwinkel</param>
        /// <param name="u">die Rotationsachse</param>
        /// <param name="B">der Vektor, welcher gedreht werden soll</param>
        /// <returns>Vector2.</returns>
        public static Vector2 Rotiere(double Winkel, Vector3 u, Vector2 B)
        {
            Vector3 B2 = Rotiere(Winkel, u, new Vector3(B.X, B.Y, 1));
            return new Vector2(B2.X, B2.Y);
        }

        /// <summary>
        ///     Prüft, ob eine Punkt mit dem Objekt kollidiert
        /// </summary>
        /// <param name="Incoming_Position">die zu prüfende Position</param>
        /// <param name="Object_Position">die Position des Objektes</param>
        /// <param name="_Winkel">der Rotationswinkel des Objektes</param>
        /// <returns>true = es gibt eine Kollision, false = keine Kollision</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position, float _Winkel)
        {
            return collision(Incoming_Position, Object_Position, _Winkel, false);
        }

        /// <summary>
        ///     Prüft, ob eine Punkt mit dem Objekt kollidiert
        /// </summary>
        /// <param name="Incoming_Position">die zu prüfende Position</param>
        /// <param name="Object_Position">die Position des Objektes</param>
        /// <param name="_Gespiegelt">ist das Objekt gespiegelt</param>
        /// <returns>true = es gibt eine Kollision, false = keine Kollision</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position, bool _Gespiegelt)
        {
            return collision(Incoming_Position, Object_Position, 0, _Gespiegelt);
        }

        /// <summary>
        ///     Prüft, ob eine Punkt mit dem Objekt kollidiert
        /// </summary>
        /// <param name="Incoming_Position">die zu prüfende Position</param>
        /// <param name="Object_Position">die Position des Objektes</param>
        /// <returns>true = es gibt eine Kollision, false = keine Kollision</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position)
        {
            return collision(Incoming_Position, Object_Position, 0, false);
        }

        /// <summary>
        ///     Prüft, ob eine Punkt mit dem Objekt kollidiert
        /// </summary>
        /// <param name="Incoming_Position">die zu prüfende Position</param>
        /// <param name="Object_Position">die Position des Objektes</param>
        /// <param name="_Winkel">der Winkel des Objektes</param>
        /// <param name="_Gespiegelt">ist das Objekt gespiegelt</param>
        /// <returns>true = es gibt eine Kollision, false = keine Kollision</returns>
        public bool collision(Vector2 Incoming_Position, Vector2 Object_Position, float _Winkel, bool _Gespiegelt)
        {
            if (Bild == null) return false;

            // Rotateable korrektur
            if (Drehbar)
                Incoming_Position = Help.RotatePosition(Object_Position, MathHelper.ToRadians(360) - _Winkel,
                    Incoming_Position);

            // Bringe Object und Eindringling relativ zueinander
            var x = (int) (Incoming_Position.X - Object_Position.X);
            var y = (int) (Incoming_Position.Y - Object_Position.Y);

            // Korrektur für Bilder, die nach ihrem Mittelpunkt ausgerichtet wurden
            if (Zentriert) x += (int) (BildBreite*Skalierung/2);

            // Korrektur (Bild wurde verschoben)
            y += (int) (BildHöhe*Skalierung);

            // eigentliche Kollisionsabfrage
            return isSet(x, y, _Gespiegelt);
        }

        /// <summary>
        ///     prüft, ob ein bestimmter Punkt der Textur gesetzt ist
        /// </summary>
        /// <param name="x">die X-Koordinate</param>
        /// <param name="y">die y-Koordinate</param>
        /// <param name="_Gespiegelt">gespiegelt?</param>
        /// <returns>true = ist gesetzt, false = nicht gesetzt</returns>
        public bool isSet(int x, int y, bool _Gespiegelt)
        {
            if (x < 0 || y < 0) return false;
            if (x >= BildBreite*Skalierung || y >= BildHöhe*Skalierung) return false;
            if (Bild == null) return false;

            // Korrektur der Skalierung
            x = (int) (x/Skalierung);
            y = (int) (y/Skalierung);

            // Korrektur des Overreach
            if (_Gespiegelt) x = BildBreite - x;

            if (!Help.isSet(Bild, x, y)) return false;
            //if (!Help.isSetInMask(Bild, x, y, Bild_Width)) return false;
            return true;
        }

        /// <summary>
        /// wandelt das Objekt in Text um
        /// </summary>
        /// <returns>die Textdarstelung des Objektes</returns>
        public List<String> Speichern()
        {
            var data = new List<String>();
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
                    add = add + Bild[b][c];
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
        ///     wendet eine Maskierung auf eine Textur an
        /// </summary>
        /// <param name="_Bild">die Textur</param>
        /// <returns>die maskierte Textur</returns>
        public Texture2D UseMaskOnTexture2D(Texture2D _Bild)
        {
            if (Bild == null) return null;
            var Data = new Color[_Bild.Width*_Bild.Height];
            _Bild.GetData(Data);

            for (int i = 0; i < _Bild.Width; i++)
                for (int b = 0; b < _Bild.Height; b++)
                {
                    if (!Help.isSet(Bild, i, b))
                    {
                        Data[i + b*_Bild.Width] = Color.Transparent;
                    }
                }

            // Color[] Data2 = new Color[_Bild.Width * _Bild.Height];
            var result = new Texture2D(Game1.device, _Bild.Width, _Bild.Height);
            result.SetData(Data);
            return result;
        }

        #endregion Methods
    }
}