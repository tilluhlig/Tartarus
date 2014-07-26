// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="Baeume.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///     Diese Klasse verwaltet einen Baum
    /// </summary>
    public class Baum
    {
        #region Fields

        /// <summary>
        ///     MOD-Variable, Es gibt im Spiel Bäume
        /// </summary>
        public static bool BAEUME;

        /// <summary>
        ///     MOD-Variable, Bäume können Kollidieren
        /// </summary>
        public static bool BAEUME_KOLLISION;

        /// <summary>
        ///     MOD-Variable, Bäume können zerstört werden
        /// </summary>
        public static bool BAEUME_ZERSTOERUNG;

        #endregion Fields

        #region Privat

        /// <summary>
        ///     MOD-Variable, Es gibt im Spiel Bäume
        /// </summary>
        private static Var<bool> MOD_BAEUME = new Var<bool>("BAEUME", false, ref BAEUME);

        /// <summary>
        ///     MOD-Variable, Bäume können Kollidieren
        /// </summary>
        private static Var<bool> MOD_BAEUME_KOLLISION = new Var<bool>("BAEUME_KOLLISION", false, ref BAEUME_KOLLISION);

        /// <summary>
        ///     MOD-Variable, Bäume können zerstört werden
        /// </summary>
        private static Var<bool> MOD_BAEUME_ZERSTOERUNG = new Var<bool>("BAEUME_ZERSTOERUNG", false,
            ref BAEUME_ZERSTOERUNG);

        #endregion Privat

        #region Methods

        /// <summary>
        ///     setzt zufällig Bäume auf der Karte
        /// </summary>
        /// <param name="Spielfeld">Das Spielfeld im üblichen Kartenformat</param>
        /// <param name="symmetrisch">true = die Bäume auf dem Spielfeld werden symmetrisch angeordnet</param>
        public void set_Baeume(List<UInt16>[] Spielfeld, bool symmetrisch)
        {
            if (BAEUME)
            {
                int x = 0;
                int anze = 0;
                for (int i = 0;; i++)
                {
                    x += Spiel.rand.Next(70, 400);
                    if (x >= (symmetrisch ? Spielfeld.Length/2 : Spielfeld.Length)) break;
                    int id = Spiel.rand.Next(0, Texturen.baum.Length);
                    Nutzloses.Hinzufügen(Texturen.baum[id], new Vector2(x, Kartenformat.BottomOf(x, 0) + 7), 0, false,
                        Baumdata.SKALIERUNG.Wert[id], BAEUME_KOLLISION, BAEUME_ZERSTOERUNG);
                    anze++;
                }

                if (symmetrisch)
                {
                    int anz = anze;
                    int middle = Spielfeld.Length/2;
                    int max = Nutzloses.GibAnzahl();
                    for (int i = 0; i < anz; i++)
                    {
                        int id = Spiel.rand.Next(0, Texturen.baum.Length);
                        Nutzloses.Hinzufügen(Texturen.baum[id],
                            new Vector2(middle + (middle - Nutzloses.GibPosition(max - anz + i).X),
                                Nutzloses.GibPosition(max - anz + i).Y), 0, false, Baumdata.SKALIERUNG.Wert[id],
                            BAEUME_KOLLISION, BAEUME_ZERSTOERUNG);
                    }
                }
            }
        }

        #endregion Methods
    }
}