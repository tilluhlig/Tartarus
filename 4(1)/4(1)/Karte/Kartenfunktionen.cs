// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-24-2013
//
// Last Modified By : Till
// Last Modified On : 08-02-2013
// ***********************************************************************
// <copyright file="Kartenfunktionen.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///     Diese Klasse stellt erweiterte Funktion fürs Kartenformat bereit
    /// </summary>
    public static class Kartenfunktionen
    {
        #region Methods

        /// <summary>
        ///     Baue Brücke nach Oben
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Bauen_Hoch(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            var list = new List<Vector3>();
            for (var i = (int)(-width * 0.75f); i < width * 0.75f; i++)
            {
                Kartenformat.SetMaterialFromTo((int)(Position.X + i), (int)Position.Y - 5, (int)(Position.Y + 5),
                    Karte.BACKSTEIN1);
                list.Add(new Vector3((int)(Position.X + i), (int)(Position.Y - 5), (int)(Position.Y + 5)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Baue Brücke nach Links
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Bauen_Links(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            var list = new List<Vector3>();
            for (int i = -10 - width; i < width; i++)
            {
                Kartenformat.SetMaterialFromTo((int)(Position.X + i), (int)Position.Y, (int)(Position.Y + 10),
                    Karte.BACKSTEIN1);
                list.Add(new Vector3((int)(i + Position.X), (int)(Position.Y), (int)(Position.Y + 10)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Baue Brücke nach Links Oben
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Bauen_Links_Hoch(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            float diff = 2.75f;
            var list = new List<Vector3>();
            int b = 10 + 2 * width;
            for (int i = -10 - width - 30; i < width - 30; i++, b--)
            {
                if (b < 0) b = 0;
                Kartenformat.SetMaterialFromTo((int)(Position.X + i), (int)(Position.Y - b / diff),
                    (int)(Position.Y + 12 - b / diff), Karte.BACKSTEIN1);
                list.Add(new Vector3((int)(i + Position.X), (int)(Position.Y - b / diff),
                    (int)(Position.Y + 12 - b / diff)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Baue Brücke nach Links Unten
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Bauen_Links_Runter(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            float diff = 2.75f;
            var list = new List<Vector3>();
            int b = 10 + 2 * width;
            for (int i = -10 - width - 30; i < width - 30; i++, b--)
            {
                if (b < 0) b = 0;
                Kartenformat.SetMaterialFromTo((int)(Position.X + i), (int)(Position.Y + b / diff),
                    (int)(Position.Y + 12 + b / diff), Karte.BACKSTEIN1);
                list.Add(new Vector3((int)(i + Position.X), (int)(Position.Y + b / diff),
                    (int)(Position.Y + 12 + b / diff)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Baue Brücke nach Rechts
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Bauen_Rechts(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            var list = new List<Vector3>();
            for (int i = -width; i < width + 10; i++)
            {
                Kartenformat.SetMaterialFromTo((int)(Position.X + i), (int)Position.Y, (int)(Position.Y + 10),
                    Karte.BACKSTEIN1);
                list.Add(new Vector3((int)(i + Position.X), (int)(Position.Y), (int)(Position.Y + 10)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Baue Brücke nach Rechts Oben
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Bauen_Rechts_Hoch(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            float diff = 2.75f;
            var list = new List<Vector3>();
            int b = 10 + 2 * width;
            for (int i = width + 10 - 1 + 30; i >= -width + 30; i--, b--)
            {
                if (b < 0) b = 0;
                Kartenformat.SetMaterialFromTo((int)(Position.X + i), (int)(Position.Y - b / diff),
                    (int)(Position.Y + 12 - b / diff), Karte.BACKSTEIN1);
                list.Add(new Vector3((int)(i + Position.X), (int)(Position.Y - b / diff),
                    (int)(Position.Y + 12 - b / diff)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Baue Brücke nach Rechts Unten
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Bauen_Rechts_Runter(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            float diff = 2.75f;
            var list = new List<Vector3>();
            int b = 10 + 2 * width;
            for (int i = width + 10 - 1 + 30; i >= -width + 30; i--, b--)
            {
                if (b < 0) b = 0;
                Kartenformat.SetMaterialFromTo((int)(Position.X + i), (int)(Position.Y + b / diff),
                    (int)(Position.Y + 12 + b / diff), Karte.BACKSTEIN1);
                list.Add(new Vector3((int)(i + Position.X), (int)(Position.Y + b / diff),
                    (int)(Position.Y + 12 + b / diff)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Grabe nach Links
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Graben_Links(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            var list = new List<Vector3>();
            for (int i = 0; i < 10 + 2 * width; i++)
            {
                Kartenformat.DeleteFromTo((int)(Position.X + width - i),
                    (int)(Position.Y - Texturen.panzerindex[Typ].Height * Fahrzeugdaten.SCALEP.Wert[Typ]),
                    (int)(Position.Y));
                list.Add(new Vector3((int)(Position.X + width - i),
                    (int)(Position.Y - Texturen.panzerindex[Typ].Height * Fahrzeugdaten.SCALEP.Wert[Typ]),
                    (int)(Position.Y)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Grabe nach Rechts
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Graben_Rechts(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            var list = new List<Vector3>();
            for (int i = 0; i < 10 + 2 * width; i++)
            {
                Kartenformat.DeleteFromTo((int)(Position.X - width + i),
                    (int)(Position.Y - Texturen.panzerindex[Typ].Height * Fahrzeugdaten.SCALEP.Wert[Typ]),
                    (int)(Position.Y));
                list.Add(new Vector3((int)(Position.X - width + i),
                    (int)(Position.Y - Texturen.panzerindex[Typ].Height * Fahrzeugdaten.SCALEP.Wert[Typ]),
                    (int)(Position.Y)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        /// <summary>
        ///     Grabe nach Unten
        /// </summary>
        /// <param name="Typ">ID des Fahrzeugtyps</param>
        /// <param name="Position">Position des Fahrzeugs</param>
        public static void Graben_Runter(int Typ, Vector2 Position)
        {
            int width = Fahrzeugdaten.FAHRM.Wert[Typ];
            var list = new List<Vector3>();
            for (int i = 0; i < 2 * width; i++)
            {
                Kartenformat.DeleteFromTo((int)(i + Position.X - width),
                    (int)(Position.Y - Texturen.panzerindex[Typ].Height * Fahrzeugdaten.SCALEP.Wert[Typ]),
                    (int)(Position.Y + 10));
                list.Add(new Vector3((int)(i + Position.X - width),
                    (int)(Position.Y - Texturen.panzerindex[Typ].Height * Fahrzeugdaten.SCALEP.Wert[Typ]),
                    (int)(Position.Y + 10)));
            }
            Vordergrund.AktualisiereVordergrund(list);
        }

        #endregion Methods
    }
}