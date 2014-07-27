// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-22-2013
// ***********************************************************************
// <copyright file="Itemdata.cs">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace _4_1_
{
    /// <summary>
    ///     hier werden Items definiert (Konsum und Upgrades)
    /// </summary>
    internal class Itemdata
    {
        #region Upgrades

        /// <summary>
        ///     Arbeitsbereichupgrade I
        /// </summary>
        public static Item Arbeitsbereich1 = new Item("Arbeitsbereich I", 1000, Effectdata.Arbeitsbereich1, 1, 0);

        /// <summary>
        ///     Arbeitsbereichupgrade II
        /// </summary>
        public static Item Arbeitsbereich2 = new Item("Arbeitsbereich II", 1000, Effectdata.Arbeitsbereich2, 1, 0);

        /// <summary>
        ///     Arbeitsbereichupgrade III
        /// </summary>
        public static Item Arbeitsbereich3 = new Item("Arbeitsbereich III", 1000, Effectdata.Arbeitsbereich3, 1, 0);

        /// <summary>
        ///     Inventar Vergrößerung I
        /// </summary>
        public static Item Lager1 = new Item("Lager I", 1000, Effectdata.Lager1, 1, 0);

        /// <summary>
        ///     Inventar Vergrößerung II
        /// </summary>
        public static Item Lager2 = new Item("Lager II", 1000, Effectdata.Lager2, 1, 0);

        /// <summary>
        ///     Inventar Vergrößerung III
        /// </summary>
        public static Item Lager3 = new Item("Lager III", 1000, Effectdata.Lager3, 1, 0);

        /// <summary>
        ///     The tarn
        /// </summary>
        public static Item Tarn = new Item("Tarn", 1000, Effectdata.Tarn, 1, 0);

        /// <summary>
        ///     Treibstoffverbrauch I
        /// </summary>
        public static Item Verbrauch1 = new Item("Verbrauch I", 1000, Effectdata.Verbrauch1, 1, 0);

        /// <summary>
        ///     Treibstoffverbrauch II
        /// </summary>
        public static Item Verbrauch2 = new Item("Verbrauch II", 1000, Effectdata.Verbrauch2, 1, 0);

        /// <summary>
        ///     Treibstoffverbrauch III
        /// </summary>
        public static Item Verbrauch3 = new Item("Verbrauch III", 1000, Effectdata.Verbrauch3, 1, 0);

        /// <summary>
        ///     Verteidigungswerte I
        /// </summary>
        public static Item Verteidiung1 = new Item("Verteidigung I", 1000, Effectdata.Verteidiung1, 1, 0);

        /// <summary>
        ///     Verteidigungswerte II
        /// </summary>
        public static Item Verteidiung2 = new Item("Verteidigung II", 1000, Effectdata.Verteidiung2, 1, 0);

        /// <summary>
        ///     Verteidigungswerte III
        /// </summary>
        public static Item Verteidiung3 = new Item("Verteidigung III", 1000, Effectdata.Verteidiung3, 1, 0);

        /// <summary>
        ///     Zielhilfe I
        /// </summary>
        public static Item Ziel1 = new Item("Ziel I", 1000, Effectdata.Ziel1, 1, 0);

        /// <summary>
        ///     Zielhilfe II
        /// </summary>
        public static Item Ziel2 = new Item("Ziel II", 1000, Effectdata.Ziel2, 1, 0);

        /// <summary>
        ///     Zielhilfe III
        /// </summary>
        public static Item Ziel3 = new Item("Ziel III", 1000, Effectdata.Ziel3, 1, 0);

        #endregion Upgrades

        #region listen

        /// <summary>
        ///     Die Liste der Konsumitems
        /// </summary>
        public static Item[] Konsumierbares = {Heilen};

        /// <summary>
        ///     Die Liste der Upgrades
        /// </summary>
        public static Item[] Upgrades =
        {
            Verteidiung1, Verteidiung2, Verteidiung3, Tarn, Ziel1, Ziel2, Ziel3, Lager1,
            Lager2, Lager3, Arbeitsbereich1, Arbeitsbereich2, Arbeitsbereich3, Verbrauch1, Verbrauch2, Verbrauch3
        };

        #endregion listen

        #region Konsumierbares

        /// <summary>
        ///     Lebenspunkte regenerieren
        /// </summary>
        public static Item Heilen = new Item("Heilen", 1000, Effectdata.Heilen, 1, 1);

        #endregion Konsumierbares

        // Hier werden neue Items definiert und mit Effekten verbunden
    }
}