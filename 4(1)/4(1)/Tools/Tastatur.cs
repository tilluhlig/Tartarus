// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="Tastatur.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    /// Verwaltet die Tastaturbelegung und erlaubte Tasten
    /// </summary>
    public static class Tastatur
    {
        #region Tastaturbelegung

        /// <summary>
        /// MOD-Variable, Taste für die Bewegung nach Links
        /// </summary>
        public static Var<Keys> BEWEGUNG_LINKS = new Var<Keys>("BEWEGUNG_LINKS", Keys.A);

        /// <summary>
        /// MOD-Variable, Taste für die Bewegung nach Rechts
        /// </summary>
        public static Var<Keys> BEWEGUNG_RECHTS = new Var<Keys>("BEWEGUNG_RECHTS", Keys.D);

        /// <summary>
        /// MOD-Variable, Taste für die Bewegung nach Oben (Rohr Bewegung)
        /// </summary>
        public static Var<Keys> BEWEGUNG_HOCH = new Var<Keys>("BEWEGUNG_HOCH", Keys.W);

        /// <summary>
        /// MOD-Variable, taste für die Bewegung nach Unten (Rohr Bewegung)
        /// </summary>
        public static Var<Keys> BEWEGUNG_RUNTER = new Var<Keys>("BEWEGUNG_RUNTER", Keys.S);

        /// <summary>
        /// MOD-Variable, Taste für die Fahrzeugwahl (nach Links)
        /// </summary>
        public static Var<Keys> FAHRZEUGWAHL_LINKS = new Var<Keys>("FAHRZEUGWAHL_LINKS", Keys.Q);

        /// <summary>
        /// MOD-Variable, Taste für die Fahrzeugwahl (nach Rechts)
        /// </summary>
        public static Var<Keys> FAHRZEUGWAHL_RECHTS = new Var<Keys>("FAHRZEUGWAHL_RECHTS", Keys.E);

        /// <summary>
        /// MOD-Variable, Taste für die Waffenwahl (nach Links)
        /// </summary>
        public static Var<Keys> WAFFENWAHL_LINKS = new Var<Keys>("WAFFENWAHL_LINKS", Keys.F);

        /// <summary>
        /// MOD-Variable, Taste für die Waffenwahl (nach Rechts)
        /// </summary>
        public static Var<Keys> WAFFENWAHL_RECHTS = new Var<Keys>("WAFFENWAHL_RECHTS", Keys.G);

        /// <summary>
        /// MOD-Variable, Taste zum einschalten und abschalten der "Replays"
        /// </summary>
        public static Var<Keys> REPLAY = new Var<Keys>("REPLAY", Keys.R);

        /// <summary>
        /// MOD-Variable, Taste zum anzeigen und abschalten Minimap
        /// </summary>
        public static Var<Keys> MINIMAP = new Var<Keys>("MINIMAP", Keys.M);

        /// <summary>
        /// MOD-Variable, Taste zum einschalten und abschalten der "Freien" Kartenbewegung
        /// </summary>
        public static Var<Keys> FREIEBEWGUNG = new Var<Keys>("FREIEBEWGUNG", Keys.N);

        // TODO wird das überhaupt benutzt???
        /// <summary>
        /// MOD-Variable, Taste zum Speichern
        /// </summary>
        public static Var<Keys> SPEICHERN = new Var<Keys>("SPEICHERN", Keys.F5);

        /// <summary>
        /// MOD-Variable, Taste zum Beenden des Zuges, Zug abschicken
        /// </summary>
        public static Var<Keys> ZUG_BEENDEN = new Var<Keys>("ZUG_BEENDEN", Keys.F9);

        /// <summary>
        /// MOD-Variable, Taste nach Links (Airstrike, Freie Bewegung)
        /// </summary>
        public static Var<Keys> LINKS = new Var<Keys>("LINKS", Keys.Left);

        /// <summary>
        /// MOD-Variable, Taste nach Rechts (Airtstrike, Freie Bewegung)
        /// </summary>
        public static Var<Keys> RECHTS = new Var<Keys>("RECHTS", Keys.Right);

        // TODO wird das überhaupt benutzt???
        /// <summary>
        /// MOD-Variable, Taste Hoch
        /// </summary>
        public static Var<Keys> HOCH = new Var<Keys>("HOCH", Keys.Up);

        // TODO wird das überhaupt benutzt???
        /// <summary>
        /// MOD-Variable, Taste Runter
        /// </summary>
        public static Var<Keys> RUNTER = new Var<Keys>("RUNTER", Keys.Down);

        /// <summary>
        /// MOD-Variable, Taste zum Abfeuern
        /// </summary>
        public static Var<Keys> SCHUSS = new Var<Keys>("SCHUSS", Keys.Space);

        #endregion Tastaturbelegung

        #region Tastenerlaubnis

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_BEWEGUNG_HOCH = new Var<bool>("TASTE_BEWEGUNG_HOCH", false);

        // Tasten
        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_BEWEGUNG_LINKS = new Var<bool>("TASTE_BEWEGUNG_LINKS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_BEWEGUNG_RECHTS = new Var<bool>("TASTE_BEWEGUNG_RECHTS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_BEWEGUNG_RUNTER = new Var<bool>("TASTE_BEWEGUNG_RUNTER", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_FAHRZEUGWAHL_LINKS = new Var<bool>("TASTE_FAHRZEUGWAHL_LINKS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_FAHRZEUGWAHL_RECHTS = new Var<bool>("TASTE_FAHRZEUGWAHL_RECHTS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_FREIEBEWGUNG = new Var<bool>("TASTE_FREIEBEWGUNG", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_HOCH = new Var<bool>("TASTE_HOCH", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_LINKS = new Var<bool>("TASTE_LINKS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_MINIMAP = new Var<bool>("TASTE_MINIMAP", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_RECHTS = new Var<bool>("TASTE_RECHTS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_REPLAY = new Var<bool>("TASTE_REPLAY", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_RUNTER = new Var<bool>("TASTE_RUNTER", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_SCHUSS = new Var<bool>("TASTE_SCHUSS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_SPEICHERN = new Var<bool>("TASTE_SPEICHERN", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_SPIELERMENU = new Var<bool>("TASTE_SPIELERMENU", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_WAFFENWAHL_LINKS = new Var<bool>("TASTE_WAFFENWAHL_LINKS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_WAFFENWAHL_RECHTS = new Var<bool>("TASTE_WAFFENWAHL_RECHTS", false);

        /// <summary>
        /// MOD-Variable, Erlaubt die Nutzung der Taste
        /// </summary>
        public static Var<bool> TASTE_ZUG_BEENDEN = new Var<bool>("TASTE_ZUG_BEENDEN", false);

        #endregion Tastenerlaubnis

        /// <summary>
        /// Liest die Tastaturbelegung ein
        /// </summary>
        /// <param name="Datei">aus angegebener Datei</param>
        public static void LadeTastaturbelegung(String Datei)
        {
            Var<Keys>.Open(Datei);
            BEWEGUNG_LINKS.Load();
            BEWEGUNG_RECHTS.Load();
            BEWEGUNG_HOCH.Load();
            BEWEGUNG_RUNTER.Load();
            FAHRZEUGWAHL_LINKS.Load();
            FAHRZEUGWAHL_RECHTS.Load();
            WAFFENWAHL_LINKS.Load();
            WAFFENWAHL_RECHTS.Load();
            REPLAY.Load();
            MINIMAP.Load();
            FREIEBEWGUNG.Load();
            SPEICHERN.Load();
            ZUG_BEENDEN.Load();
            LINKS.Load();
            RECHTS.Load();
            HOCH.Load();
            RUNTER.Load();
            SCHUSS.Load();
        }

        /// <summary>
        /// Speichert die Tastaturbelegung
        /// </summary>
        /// <param name="Datei">in angegebene Datei</param>
        public static void SpeichereTastaturbelegung(String Datei)
        {
            //   if (!File.Exists(Datei)) return;
            StreamWriter datei = new StreamWriter(Datei);
            datei.WriteLine(BEWEGUNG_LINKS.Name + "=" + BEWEGUNG_LINKS.Wert.ToString());
            datei.WriteLine(BEWEGUNG_RECHTS.Name + "=" + BEWEGUNG_RECHTS.Wert.ToString());
            datei.WriteLine(BEWEGUNG_HOCH.Name + "=" + BEWEGUNG_HOCH.Wert.ToString());
            datei.WriteLine(BEWEGUNG_RUNTER.Name + "=" + BEWEGUNG_RUNTER.Wert.ToString());
            datei.WriteLine(FAHRZEUGWAHL_LINKS + "=" + FAHRZEUGWAHL_LINKS.Wert.ToString());
            datei.WriteLine(FAHRZEUGWAHL_RECHTS + "=" + FAHRZEUGWAHL_RECHTS.Wert.ToString());
            datei.WriteLine(WAFFENWAHL_LINKS + "=" + WAFFENWAHL_LINKS.Wert.ToString());
            datei.WriteLine(WAFFENWAHL_RECHTS + "=" + WAFFENWAHL_RECHTS.Wert.ToString());
            datei.WriteLine(REPLAY + "=" + REPLAY.Wert.ToString());
            datei.WriteLine(MINIMAP + "=" + MINIMAP.Wert.ToString());
            datei.WriteLine(FREIEBEWGUNG + "=" + FREIEBEWGUNG.Wert.ToString());
            datei.WriteLine(SPEICHERN + "=" + SPEICHERN.Wert.ToString());
            datei.WriteLine(ZUG_BEENDEN + "=" + ZUG_BEENDEN.Wert.ToString());
            datei.WriteLine(LINKS + "=" + LINKS.Wert.ToString());
            datei.WriteLine(RECHTS + "=" + RECHTS.Wert.ToString());
            datei.WriteLine(HOCH + "=" + HOCH.Wert.ToString());
            datei.WriteLine(RUNTER + "=" + RUNTER.Wert.ToString());
            datei.WriteLine(SCHUSS + "=" + SCHUSS.Wert.ToString());
            datei.Close();
        }
    }
}