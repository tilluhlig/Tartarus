// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-22-2013
// ***********************************************************************
// <copyright file="Hausdata.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse wird für allgemeine Gebäudedaten bzw. den Daten zur Erstellung
    ///     von Dörfern und Städten genutzt
    /// </summary>
    public static class Gebäudedaten
    {
        #region Fields

        /// <summary>
        ///     Definiert DORF als Gebäudekopplung
        ///     -1 steht für zufälliges Haus aus HAEUSERDORF
        /// </summary>
        public static Var<int[]> DORF = new Var<int[]>("DORF", new[] { -1, 17, -1 });

        /// <summary>
        ///     Die ID der FABRIK
        /// </summary>
        public static int FABRIK = 16;

        /// <summary>
        ///     Enthält IDs zu allen Häusern, die in einem DORF vorkommen können
        /// </summary>
        public static Var<int[]> HAEUSERDORF = new Var<int[]>("HAEUSERDORF", new[] { 0, 2, 6, 11 });

        /// <summary>
        ///     Enthält IDs zu allen Häusern, die in einer STADT vorkommen können
        /// </summary>
        public static Var<int[]> HAEUSERSTADT = new Var<int[]>("HAEUSERSTADT", new[] { 1, 4, 5, 7, 8, 9, 10 });

        /// <summary>
        ///     Positionskorrektur der Fahne des Gebäudes (X-Richtung)
        /// </summary>
        /// 0    1    2    3    4    5    6    7    8    9   10   11   12   13   14   15   16   17
        public static Var<int[]> POSITIONX = new Var<int[]>("POSITIONX",
            new[] { -04, +00, +00, -99, +00, +00, +19, -02, -01, +00, +00, -03, -99, -99, -99, -99, -03, +00 });

        /// <summary>
        ///     Positionskorrektur der Fahne des Gebäudes (Y-Richtung)
        /// </summary>
        public static Var<int[]> POSITIONY = new Var<int[]>("POSITIONY",
            new[] { +00, +00, +00, -99, +00, +00, +00, +02, +00, +00, +01, +02, -99, -99, -99, -99, +00, +00 });

        /// <summary>
        ///     Die Skalierug der Gebäudetexturen
        /// </summary>
        public static Var<float[]> SKALIERUNG = new Var<float[]>("SKALIERUNG",
            new[]
            {
                0.09f, 0.09f, 0.09f, 0.09f, 0.09f, 0.09f, 0.11f, 0.09f, 0.08f, 0.075f, 0.085f, 0.15f, 1f, 1f, 1f, 1f, 0.25f,
                0.25f
            });

        /// <summary>
        ///     Definiert STADT als Gebäudekopplung
        ///     -1 steht für zufälliges Haus aus HAEUSERDORF
        /// </summary>
        public static Var<int[]> STADT = new Var<int[]>("STADT", new[] { -1, -1, 16, -1, -1 });

        /// <summary>
        ///     Die ID des WAFFENHÄNDLERs
        /// </summary>
        public static int WAFFENHÄNDLER = 17;

        #endregion Fields
    }
}