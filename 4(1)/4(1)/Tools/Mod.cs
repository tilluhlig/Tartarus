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
    ///     Diese Klasse verwaltet einige MOD-Variablen und zusätzliche Funktionen für MOD-Variablen
    /// </summary>
    public static class Mod
    {
        #region Fields

        /// <summary>
        ///     MOD-Variable, ob das Aktionspunktefeld angezeigt wird
        /// </summary>
        public static Var<bool> ACTION_BUTTON_VISIBLE = new Var<bool>("ACTION_BUTTON_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, aktuelle MunitionButton (links oben)
        /// </summary>
        public static Var<bool> AKTUELLE_MUNITION_BUTTON_VISIBLE = new Var<bool>("AKTUELLE_MUNITION_BUTTON_VISIBLE",
            false);

        /// <summary>
        ///     MOD-Variable, die X Position des aktuelle MunitionButton (links oben)
        /// </summary>
        public static Var<int> AKTUELLE_MUNITION_BUTTON_X = new Var<int>("AKTUELLE_MUNITION_BUTTON_X", 10);

        /// <summary>
        ///     MOD-Variable, die Y Position des aktuelle MunitionButton (links oben)
        /// </summary>
        public static Var<int> AKTUELLE_MUNITION_BUTTON_Y = new Var<int>("AKTUELLE_MUNITION_BUTTON_Y", 10);

        /// <summary>
        ///     MOD-Variable, ob extra markiert werden soll, welches Fahrzeug auf dem Spielfeld
        ///     aktuell ausgewählt ist
        /// </summary>
        public static Var<bool> AKTUELLER_ON_TANK_VISIBLE = new Var<bool>("AKTUELLER_ON_TANK_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob auf dem Fahrzeug ein Zähler für eine eventuelle Nutzung von Sperrzeiten angezeigt werden soll
        /// </summary>
        public static Var<bool> COOLDOWN_ON_TANK_VISIBLE = new Var<bool>("COOLDOWN_ON_TANK_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob die Eingabezeile verfügbar ist
        /// </summary>
        public static Var<bool> EINGABEZEILE_VISIBLE = new Var<bool>("EINGABEZEILE_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob die "Freie-Kartenbewegung" nutzbar ist
        /// </summary>
        public static Var<bool> FREIE_KARTENBEWEGUNG = new Var<bool>("FREIE_KARTENBEWEGUNG", false);

        /// <summary>
        ///     MOD-Variable, ob der Fuel / Aktionspunkte Button (oben rechts) angezeigt werden soll
        /// </summary>
        public static Var<bool> FUEL_BUTTON_VISIBLE = new Var<bool>("FUEL_BUTTON_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob auf Gebäuden die Fahnen in Farbe des Besitzers angezeigt werden soll
        /// </summary>
        public static Var<bool> HAEUSER_FAHNE_VISIBLE = new Var<bool>("HAEUSER_FAHNE_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob die Lebenspunkte über einem Fahrzeug als Lebenslinie angezeigt werden soll
        /// </summary>
        public static Var<bool> LEBENSLINIE_ON_TANK_VISIBLE = new Var<bool>("LEBENSLINIE_ON_TANK_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob in der Leiste, die zeigt, welche Fahrzeuge man besitzt, extra angezeigt werden soll,
        ///     welches aktuell ausgewählt ist
        /// </summary>
        public static Var<bool> LEISTE_AKTUELL_VISIBLE = new Var<bool>("LEISTE_AKTUELL_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob in der Leiste, die zeigt, welche Fahrzeuge man besitzt, angezeigt werden soll
        /// </summary>
        public static Var<bool> LEISTE_BUTTON_VISIBLE = new Var<bool>("LEISTE_BUTTON_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob in der Leiste, die zeigt, welche Fahrzeuge man besitzt, die Lebenslinie der Fahrzeuge
        ///     sichtbar sein soll
        /// </summary>
        public static Var<bool> LEISTE_LEBENSLINIE_VISIBLE = new Var<bool>("LEISTE_LEBENSLINIE_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob in der Leiste, die zeigt, welche Fahrzeuge man besitzt, die Art der Fahrzeuge gezeigt werden soll
        /// </summary>
        public static Var<bool> LEISTE_TANK_VISIBLE = new Var<bool>("LEISTE_TANK_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob die Minimap sichtbar sein soll
        /// </summary>
        public static Var<bool> MINIMAP_VISIBLE = new Var<bool>("MINIMAP_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob für Geschosse, welche sich ausserhalb des sichtbaren Bereichs befinden, an der Oberseite
        ///     des Bildschirms ein Strich gezeichnet werden soll, der die aktuelle Y Koordinate des Geschosses darstellt.
        /// </summary>
        public static Var<bool> MISSILE_STRICH_VISIBLE = new Var<bool>("MISSILE_STRICH_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob über Fahrzeugen der Name des Fahrzeugs sichtbar sein soll
        /// </summary>
        public static Var<bool> NAME_TANK_VISIBLE = new Var<bool>("NAME_TANK_VISIBLE", true);

        /// <summary>
        ///     MOD-Variable, ob die Spieldaten an den Server geschickt werden sollen und die Runde dabei beendet werden kann
        /// </summary>
        public static Var<bool> ONLINE_ESCAPE_VISIBLE = new Var<bool>("ONLINE_ESCAPE_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob über Orten deren Name angezeigt werden soll
        /// </summary>
        public static Var<bool> ORTSSCHILD_VISIBLE = new Var<bool>("ORTSSCHILD_VISIBLE", true);

        /// <summary>
        ///     MOD-Variable, ob das Pausemenü nutzbar ist
        /// </summary>
        public static Var<bool> PAUSEMENU_VISIBLE = new Var<bool>("PAUSEMENU_VISIBLE", false);

        /// <summary>
        ///     MOD-Variable, ob beim verlassen des Spiels automatisch gespeichert werden soll
        /// </summary>
        public static Var<bool> SAVE_ON_EXIT = new Var<bool>("SAVE_ON_EXIT", false);

        /// <summary>
        ///     MOD-Variable, ob das Spielermenü nutzbar ist
        /// </summary>
        public static Var<bool> SPIELERMENU_VISIBLE = new Var<bool>("SPIELERMENU_VISIBLE", false);

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Lädt die Mod-Variablen aus der angegebenen Datei (Variable=Wert)
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
            Game1.DEBUG_AKTIV.Wert = true;
#endif
        }

        /// <summary>
        ///     Speichert die Mod-Variablen in die angegebene Datei (Variable=Wert)
        /// </summary>
        /// <param name="Datei">Die Datei, in welche die Daten gespeichert werden sollen</param>
        /// ///
        /// <param name="Append">Sollen die Daten an eine mögliche bestehende Datei angehängt werden?</param>
        public static void SpeichereModVariablen(String Datei, bool Append)
        {
            var file = new StreamWriter(Datei, Append);

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

        #endregion Methods
    }
}