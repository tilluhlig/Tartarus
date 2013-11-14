// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-22-2013
// ***********************************************************************
// <copyright file="Effectdata.cs">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace _4_1_
{
    /// <summary>
    /// Class Effectdata
    /// </summary>
    public static class Effectdata
    {
        // hier werden Effekte definiert

        #region Status

        /// <summary>
        /// The EINGEFROREN
        /// </summary>
        public static Effekt EINGEFROREN = new Effekt("Eingefroren", "Textures\\leer", 3, 1, 0, 0, 0, 0, 0, 0, 0, -200, 0, -200, 0, 0, 0, 200, 0, 0, 0, 20, 1, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The VERGIFTED
        /// </summary>
        public static Effekt VERGIFTED = new Effekt("Vergifted", "Textures\\leer", 3, 1, 0, 0, 0, 0, 0, 0, 0, 100, 0, 100, 0, 0, 0, 0, 0, 87, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The ELEKTRISIERT
        /// </summary>
        public static Effekt ELEKTRISIERT = new Effekt("Elektrisiert", "Textures\\leer", 3, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0);

        #endregion Status

        #region Upgrades

        /// <summary>
        /// The verteidiung1
        /// </summary>
        public static Effekt Verteidiung1 = new Effekt("Verteidigung", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, -10, 0, -10, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The verteidiung2
        /// </summary>
        public static Effekt Verteidiung2 = new Effekt("Verteidigung", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, -20, 0, -20, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The verteidiung3
        /// </summary>
        public static Effekt Verteidiung3 = new Effekt("Verteidigung", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, -40, 0, -40, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The tarn
        /// </summary>
        public static Effekt Tarn = new Effekt("Tarn", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0);

        /// <summary>
        /// The ziel1
        /// </summary>
        public static Effekt Ziel1 = new Effekt("Ziel", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 500, 0, 0, 0);

        /// <summary>
        /// The ziel2
        /// </summary>
        public static Effekt Ziel2 = new Effekt("Ziel", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1000, 0, 0, 0);

        /// <summary>
        /// The ziel3
        /// </summary>
        public static Effekt Ziel3 = new Effekt("Ziel", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1500, 0, 0, 0);

        /// <summary>
        /// The lager1
        /// </summary>
        public static Effekt Lager1 = new Effekt("Lager", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0);

        /// <summary>
        /// The lager2
        /// </summary>
        public static Effekt Lager2 = new Effekt("Lager", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0);

        /// <summary>
        /// The lager3
        /// </summary>
        public static Effekt Lager3 = new Effekt("Lager", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0);

        /// <summary>
        /// The arbeitsbereich1
        /// </summary>
        public static Effekt Arbeitsbereich1 = new Effekt("Arbeitsbereich", "Textures\\leer", -1, 2, 0, 0, 0, 0, 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The arbeitsbereich2
        /// </summary>
        public static Effekt Arbeitsbereich2 = new Effekt("Arbeitsbereich", "Textures\\leer", -1, 2, 0, 0, 0, 0, 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The arbeitsbereich3
        /// </summary>
        public static Effekt Arbeitsbereich3 = new Effekt("Arbeitsbereich", "Textures\\leer", -1, 2, 0, 0, 0, 0, 150, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// The verbrauch1
        /// </summary>
        public static Effekt Verbrauch1 = new Effekt("Verbrauch", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -10);

        /// <summary>
        /// The verbrauch2
        /// </summary>
        public static Effekt Verbrauch2 = new Effekt("Verbrauch", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -20);

        /// <summary>
        /// The verbrauch3
        /// </summary>
        public static Effekt Verbrauch3 = new Effekt("Verbrauch", "Textures\\leer", -1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -30);

        #endregion Upgrades

        #region Konsumierbares

        /// <summary>
        /// The heilen
        /// </summary>
        public static Effekt Heilen = new Effekt("Heilen", "Textures\\leer", 3, 0, 1000, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        #endregion Konsumierbares
    }
}