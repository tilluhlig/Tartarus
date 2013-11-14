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
    /// Class Hausdata
    /// </summary>
    public static class Gebäudedaten
    {
        /// <summary>
        /// The dorf
        /// </summary>
        public static Var<int[]> DORF = new Var<int[]>("DORF", new int[] { -1, 17, -1 });

        /// <summary>
        /// The fabrik
        /// </summary>
        public static int FABRIK = 16;

        /// <summary>
        /// The waffenhändler
        /// </summary>
        public static int WAFFENHÄNDLER = 17;

        /// <summary>
        /// The haeuser dorf
        /// </summary>
        public static Var<int[]> HAEUSERDORF = new Var<int[]>("HAEUSERDORF", new int[] { 0, 2, 6, 11 });

        /// <summary>
        /// The haeuser stadt
        /// </summary>
        public static Var<int[]> HAEUSERSTADT = new Var<int[]>("HAEUSERSTADT", new int[] { 1, 4, 5, 7, 8, 9, 10 });

        /// <summary>
        /// The position
        /// </summary>                                                                 0    1    2    3    4    5    6    7    8    9   10   11   12   13   14   15   16   17
        public static Var<int[]> POSITIONX = new Var<int[]>("POSITIONX", new int[] { -04, +00, +00, -99, +00, +00, +19, -02, -01, +00, +00, -03, -99, -99, -99, -99, -03, +00 });

        public static Var<int[]> POSITIONY = new Var<int[]>("POSITIONY", new int[] { +00, +00, +00, -99, +00, +00, +00, +02, +00, +00, +01, +02, -99, -99, -99, -99, +00, +00 });

        /// <summary>
        /// The stadt
        /// </summary>
        public static Var<int[]> STADT = new Var<int[]>("STADT", new int[] { -1, -1, 16, -1, -1 });

        /// <summary>
        /// Die Skalierug der Gebäudetexturen
        /// </summary>
        public static Var<float[]> SKALIERUNG = new Var<float[]>("SKALIERUNG", new float[] { 0.09f, 0.09f, 0.09f, 0.09f, 0.09f, 0.09f, 0.11f, 0.09f, 0.08f, 0.075f, 0.085f, 0.15f, 1f, 1f, 1f, 1f, 0.25f, 0.25f });
    }
}