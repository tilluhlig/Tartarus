// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-19-2013
// ***********************************************************************
// <copyright file="Mod.cs">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;

namespace _4_1_
{
    /// <summary>
    /// Class Mod
    /// </summary>
    public static class Mod
    {
        /// <summary>
        /// The ACTIO n_ BUTTO n_ VISIBLE
        /// </summary>
        public static Var<bool> ACTION_BUTTON_VISIBLE = new Var<bool>("ACTION_BUTTON_VISIBLE", false);

        // aktuelle MunitionButton (links oben)
        /// <summary>
        /// The AKTUELL e_ MUNITIO n_ BUTTO n_ VISIBLE
        /// </summary>
        public static Var<bool> AKTUELLE_MUNITION_BUTTON_VISIBLE = new Var<bool>("AKTUELLE_MUNITION_BUTTON_VISIBLE", false);

        /// <summary>
        /// The AKTUELL e_ MUNITIO n_ BUTTO n_ X
        /// </summary>
        public static Var<int> AKTUELLE_MUNITION_BUTTON_X = new Var<int>("AKTUELLE_MUNITION_BUTTON_X", 10);

        /// <summary>
        /// The AKTUELL e_ MUNITIO n_ BUTTO n_ Y
        /// </summary>
        public static Var<int> AKTUELLE_MUNITION_BUTTON_Y = new Var<int>("AKTUELLE_MUNITION_BUTTON_Y", 10);

        /// <summary>
        /// The AKTUELLE r_ O n_ TAN k_ VISIBLE
        /// </summary>
        public static Var<bool> AKTUELLER_ON_TANK_VISIBLE = new Var<bool>("AKTUELLER_ON_TANK_VISIBLE", false);

        // Panzer zeichnungen
        /// <summary>
        /// The COOLDOW n_ O n_ TAN k_ VISIBLE
        /// </summary>
        public static Var<bool> COOLDOWN_ON_TANK_VISIBLE = new Var<bool>("COOLDOWN_ON_TANK_VISIBLE", false);

        /// <summary>
        /// The EINGABEZEIL e_ VISIBLE
        /// </summary>
        public static Var<bool> EINGABEZEILE_VISIBLE = new Var<bool>("EINGABEZEILE_VISIBLE", false);

        /// <summary>
        /// The FREI e_ KARTENBEWEGUNG
        /// </summary>
        public static Var<bool> FREIE_KARTENBEWEGUNG = new Var<bool>("FREIE_KARTENBEWEGUNG", false);

        // Fuel / Aktionspunkte (oben rechts)
        /// <summary>
        /// The FUE l_ BUTTO n_ VISIBLE
        /// </summary>
        public static Var<bool> FUEL_BUTTON_VISIBLE = new Var<bool>("FUEL_BUTTON_VISIBLE", false);

        /// <summary>
        /// The HAEUSER_FAHNE_VISIBLE
        /// </summary>
        public static Var<bool> HAEUSER_FAHNE_VISIBLE = new Var<bool>("HAEUSER_FAHNE_VISIBLE", false);

        /// <summary>
        /// The LEBENSLINI e_ O n_ TAN k_ VISIBLE
        /// </summary>
        public static Var<bool> LEBENSLINIE_ON_TANK_VISIBLE = new Var<bool>("LEBENSLINIE_ON_TANK_VISIBLE", false);

        /// <summary>
        /// The LEIST e_ AKTUEL l_ VISIBLE
        /// </summary>
        public static Var<bool> LEISTE_AKTUELL_VISIBLE = new Var<bool>("LEISTE_AKTUELL_VISIBLE", false);

        // Fahrzeugleiste (unten links)
        /// <summary>
        /// The LEIST e_ BUTTO n_ VISIBLE
        /// </summary>
        public static Var<bool> LEISTE_BUTTON_VISIBLE = new Var<bool>("LEISTE_BUTTON_VISIBLE", false);

        /// <summary>
        /// The LEIST e_ LEBENSLINI e_ VISIBLE
        /// </summary>
        public static Var<bool> LEISTE_LEBENSLINIE_VISIBLE = new Var<bool>("LEISTE_LEBENSLINIE_VISIBLE", false);

        /// <summary>
        /// The LEIST e_ TAN k_ VISIBLE
        /// </summary>
        public static Var<bool> LEISTE_TANK_VISIBLE = new Var<bool>("LEISTE_TANK_VISIBLE", false);

        /// <summary>
        /// The LEIST e_ TAN k_ VISIBLE
        /// </summary>
        public static Var<bool> NAME_TANK_VISIBLE = new Var<bool>("NAME_TANK_VISIBLE", true);

        /// <summary>
        /// The MINIMA p_ VISIBLE
        /// </summary>
        public static Var<bool> MINIMAP_VISIBLE = new Var<bool>("MINIMAP_VISIBLE", false);

        /// <summary>
        /// The MISSIL e_ STRIC h_ VISIBLE
        /// </summary>
        public static Var<bool> MISSILE_STRICH_VISIBLE = new Var<bool>("MISSILE_STRICH_VISIBLE", false);

        /// <summary>
        /// The ONLIN e_ ESCAP e_ VISIBLE
        /// </summary>
        public static Var<bool> ONLINE_ESCAPE_VISIBLE = new Var<bool>("ONLINE_ESCAPE_VISIBLE", false);

        /// <summary>
        /// The PAUSEMEN u_ VISIBLE
        /// </summary>
        public static Var<bool> PAUSEMENU_VISIBLE = new Var<bool>("PAUSEMENU_VISIBLE", false);

        /// <summary>
        /// The SAV e_ O n_ EXIT
        /// </summary>
        public static Var<bool> SAVE_ON_EXIT = new Var<bool>("SAVE_ON_EXIT", false);

        /// <summary>
        /// The SPIELERMEN u_ VISIBLE
        /// </summary>
        public static Var<bool> SPIELERMENU_VISIBLE = new Var<bool>("SPIELERMENU_VISIBLE", false);

        /// <summary>
        /// The SPIELERMEN u_ VISIBLE
        /// </summary>
        public static Var<bool> ORTSSCHILD_VISIBLE = new Var<bool>("ORTSSCHILD_VISIBLE", true);

        /// <summary>
        /// Lädt die Mod-Variablen aus der angegebenen Datei (Variable=Wert)
        /// </summary>
        /// <param name="Datei">Die Datei, aus der die Daten geladen werden sollen</param>
        public static void LadeModVariablen(String Datei)
        {
            // Angrabbeln (Trick 17)
            Help.angrabbel_funktion();

            // Laden
            Var<bool>.Open(Datei);
            for (int i = 0; i < Var<String>.ALLE.Count; i++) Var<String>.ALLE[i].Load();
            for (int i = 0; i < Var<int>.ALLE2.Count; i++) Var<int>.ALLE2[i].Load();
            for (int i = 0; i < Var<bool>.ALLE3.Count; i++) Var<bool>.ALLE3[i].Load();
            for (int i = 0; i < Var<float>.ALLE4.Count; i++) Var<float>.ALLE4[i].Load();

            for (int i = 0; i < Var<String[]>.ALLE5.Count; i++) Var<String[]>.ALLE5[i].Load();
            for (int i = 0; i < Var<int[]>.ALLE6.Count; i++) Var<int[]>.ALLE6[i].Load();
            for (int i = 0; i < Var<bool[]>.ALLE7.Count; i++) Var<bool[]>.ALLE7[i].Load();
            for (int i = 0; i < Var<float[]>.ALLE8.Count; i++) Var<float[]>.ALLE8[i].Load();

#if DEBUG
                Game1.DEBUG_AKTIV.Wert=true;
#endif
        }

        /// <summary>
        /// Speichert die Mod-Variablen in die angegebene Datei (Variable=Wert)
        /// </summary>
        /// <param name="Datei">Die Datei, in welche die Daten gespeichert werden sollen</param>
        public static void SpeichereModVariablen(String Datei)
        {
            StreamWriter file = new StreamWriter(Datei, false);

            // Speichern
            for (int i = 0; i < Var<String>.ALLE.Count; i++) file.WriteLine(Var<String>.ALLE[i].Save());
            for (int i = 0; i < Var<int>.ALLE2.Count; i++) file.WriteLine(Var<int>.ALLE2[i].Save());
            for (int i = 0; i < Var<bool>.ALLE3.Count; i++) file.WriteLine(Var<bool>.ALLE3[i].Save());
            for (int i = 0; i < Var<float>.ALLE4.Count; i++) file.WriteLine(Var<float>.ALLE4[i].Save());

            for (int i = 0; i < Var<String[]>.ALLE5.Count; i++) file.WriteLine(Var<String[]>.ALLE5[i].Save());
            for (int i = 0; i < Var<int[]>.ALLE6.Count; i++) file.WriteLine(Var<int[]>.ALLE6[i].Save());
            for (int i = 0; i < Var<bool[]>.ALLE7.Count; i++) file.WriteLine(Var<bool[]>.ALLE7[i].Save());
            for (int i = 0; i < Var<float[]>.ALLE8.Count; i++) file.WriteLine(Var<float[]>.ALLE8[i].Save());

            file.Close();
        }
    }
}