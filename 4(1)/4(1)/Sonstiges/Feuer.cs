// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-11-2013
// ***********************************************************************
// <copyright file="Feuer.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///     Diese Klasse verwaltet Brandherde
    /// </summary>
    public static class Feuer
    {
        #region Fields

        /// <summary>
        ///     Liste der Brände (für jede x-Koordinate existiert eine Liste im array), y1, y2, zeit, altes Material an dieser
        ///     Position
        /// </summary>
        public static List<Vector4>[] Braende;

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Verringert die Lebenszeit aller Brandherde
        /// </summary>
        /// <returns>Gibt eine Liste an Bereichen zurück, die neu Gezeichnet werden müssen</returns>
        public static List<Vector3> AlleBrandherdeVerkleinern()
        {
            var list = new List<Vector3>();
            for (int b = 0; b < Braende.Count(); b++)
            {
                if (Braende[b] == null) continue;
                for (int i = 0; i < Braende[b].Count; i++)
                {
                    Braende[b][i] = new Vector4(Braende[b][i].X, Braende[b][i].Y, Braende[b][i].Z - 1, Braende[b][i].W);

                    if (Braende[b][i].Z <= 0)
                    {
                        var W = (int) Braende[b][i].W;
                        var X = (int) Braende[b][i].X;
                        var Y = (int) Braende[b][i].Y;
                        list.AddRange(Kartenformat.SetMaterialFromTo(b, X, Y, W));
                        Braende[b].RemoveAt(i);
                        i--;
                    }
                }
            }
            return list;
        }

        /// <summary>
        ///     Prüft, ob eine Position eine Kollision mit einem Brandherd hat
        /// </summary>
        /// <param name="Position">Position des Punktes der geprüft werden soll</param>
        /// <returns>true = x,y Kollidiert mit einem Brandherd, false = keine Kollision</returns>
        public static bool check_Feuer(Vector2 Position)
        {
            return check_Feuer((int) Position.X, (int) Position.Y);
        }

        /// <summary>
        ///     Prüft, ob eine Position eine Kollision mit einem Brandherd hat
        /// </summary>
        /// <param name="x">x-Koordinate</param>
        /// <param name="y">y-Koordinate</param>
        /// <returns>true = x,y Kollidiert mit einem Brandherd, false = keine Kollision</returns>
        public static bool check_Feuer(int x, int y)
        {
            if (x < 0 || x >= Braende.Count()) return false;
            if (y < 0) return false;
            for (int i = 0; i < Braende[x].Count; i++)
                if (y >= Braende[x][i].X && y <= Braende[x][i].Y)
                    return true;
            return false;
        }

        /// <summary>
        ///     Fügt einen neuen Brandbereich ein
        /// </summary>
        /// <param name="x1">x-Koordinate Anfang</param>
        /// <param name="x2">x-Koordinate Ende</param>
        /// <param name="Vergleichsposition">x-Koordinate Mitte (Vergleichspunkt)</param>
        /// <param name="y">y-Koordinate, Höhe in der gezündet wird</param>
        /// <param name="Brenndauer">Wie lange soll es brennen</param>
        /// <returns>Gibt eine Liste an Bereichen zurück, die neu Gezeichnet werden müssen</returns>
        public static List<Vector3> Generieren(int x1, int x2, int Vergleichsposition, int y, int Brenndauer)
        {
            if (Vergleichsposition < x1 || Vergleichsposition > x2) return null;
            if (Brenndauer < 0) return null;
            if (y < 0) return null;

            int standy = Kartenformat.BottomOf(Vergleichsposition, y);
            var list = new List<Vector3>();
            for (int i = x1; i <= x2; i++)
            {
                int posy = Kartenformat.BottomOf(i, y);
                if (posy - 50 > standy) continue;
                if (posy + 50 < standy) continue;
                int h = 5;
                Braende[i].Add(new Vector4(posy - h, posy, Brenndauer, Kartenformat.GetMaterial(i, standy - 2)));
                list.AddRange(Kartenformat.SetMaterialFromTo(i, posy - h, posy, Karte.FEUER));
            }
            return list;
        }

        /// <summary>
        ///     Initialisiert einmalig die Feuer-Klasse
        /// </summary>
        /// <param name="breite">Benötigt die Breite des Spielfeldes</param>
        public static void Initialisieren(int breite)
        {
            Braende = new List<Vector4>[breite];
            for (int i = 0; i < Braende.Count(); i++) Braende[i] = new List<Vector4>();
        }

        #endregion Methods
    }
}