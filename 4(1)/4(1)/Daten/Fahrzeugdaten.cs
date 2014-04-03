// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Anton
// Last Modified On : 04-01-2014
// ***********************************************************************
// <copyright file="Tankdata.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    /// Enthält sämtliche Standarddaten für alle Fahrzeugklassen
    /// </summary>
    public static class Fahrzeugdaten
    {
        #region FahrzeugIDs

        /// <summary>
        /// ARTILLERIE ID ist 0
        /// </summary>
        public static int ARTILLERIE = 0;

        /// <summary>
        /// PANZER ID ist 1
        /// </summary>
        public static int PANZER = 1;

        /// <summary>
        /// BAUFAHRZEUG ID ist 2
        /// </summary>
        public static int BAUFAHRZEUG = 2;

        /// <summary>
        /// SCOUT ID ist 3
        /// </summary>
        public static int SCOUT = 3;

        /// <summary>
        /// RAKETEN-GESCHÜTZ ID ist 4
        /// </summary>
        public static int GESCHÜTZ = 4;

        /// <summary>
        /// MG-GESCHÜTZ ID ist 5
        /// </summary>
        public static int GESCHÜTZ2 = 5;

        #endregion FahrzeugIDs

        /// <summary>
        /// Zeigt den maximalen Ausschwenkungsgrad des Zielrohres des Fahrzeugs (gegen Uhrzeigersinn)
        /// </summary>
        public static Var<float[]> MAXANGLEA = new Var<float[]>("MAXANGLEA", new float[] { MathHelper.ToRadians(80), MathHelper.ToRadians(35), MathHelper.ToRadians(0), MathHelper.ToRadians(0), MathHelper.ToRadians(35), MathHelper.ToRadians(35) });

        /// <summary>
        /// Zeigt den maximalen Ausschwenkungsgrad des Zielrohres des Fahrzeugs (in Uhrzeigersinn)
        /// </summary>
        public static Var<float[]> MAXANGLEB = new Var<float[]>("MAXANGLEA", new float[] { MathHelper.ToRadians(100), MathHelper.ToRadians(155), MathHelper.ToRadians(180), MathHelper.ToRadians(180), MathHelper.ToRadians(155), MathHelper.ToRadians(155) });

        /// <summary>
        /// wie viel Maximalgesundheit welcher Art von Fahrzeug zustehen
        /// </summary>
        public static Var<int[]> MAXHP = new Var<int[]>("MAXHP", new int[] { 10000, 10000, 10000, 5000, 5000, 5000 });

        /// <summary>
        /// wie stark die Fahrzeuge auf den Feld skaliert werden (Korpus)
        /// </summary>
        public static Var<float[]> SCALEP = new Var<float[]>("SCALEP", new float[] { 0.25f, 0.19f, 0.32f, 0.11f, 0.45f, 0.45f });

        /// <summary>
        /// wie stark die Fahrzeuge auf den Feld skaliert werden (Zielrohr)
        /// </summary>
        public static Var<float[]> SCALER = new Var<float[]>("SCALER", new float[] { 0.25f, 0.19f, 0.32f, 0.11f, 0.45f, 0.45f });

        /// <summary>
        /// Stellt fest, ob dieses Fahrzeug/Geschütz mit anderen Fahrzeugen/Geschützen/Händlern Items tauschen kann
        /// </summary>
        public static Var<int[]> KANNHANDELN = new Var<int[]>("KANNHANDELN", new int[] { 1, 1, 1, 1, 0, 0 });

        /// <summary>
        /// Gibt an, wie weit der Fahrzeugtyp maximal in der Runde nach rechts/links fahren kann
        /// </summary>
        public static Var<int[]> ARBEITSBEREICH = new Var<int[]>("ARBEITSBEREICH", new int[] { 1000, 1000, 1000, 1000, 0, 0 });

        /// <summary>
        /// Gibt an, wo bei den Fahrzeug die Mitte ist
        /// </summary>
        public static Var<int[]> FAHRM = new Var<int[]>("FAHRM", new int[] { 32, 34, 32, 32, 32, 32 });

        /// <summary>
        /// Enthält Fallgeschwindigkeit für jeden Fahrzeugtyp
        /// </summary>
        public static Var<float[]> FALLG = new Var<float[]>("FALLG", new float[] { 1.00f, 1.00f, 1.00f, 1.5f, 1.5f, 1.5f });

        /// <summary>
        /// ???
        /// </summary>
        public static Var<float[]> FALLW = new Var<float[]>("FALLW", new float[] { 0.03f, 0.03f, 0.025f, 0.25f, 0.25f, 0.25f });

        /// <summary>
        /// Enthält die Geländegeschwindigkeit für alle Fahrzeugarten (Vorwärts)
        /// </summary>
        public static Var<float[]> GESCHWV = new Var<float[]>("GESCHWV", new float[] { 1.0f, 1.5f, 1.8f, 3f, 0f, 0f });

        /// <summary>
        /// Enthält die Geländegeschwindigkeit für alle Fahrzeugarten (Rückwärts)
        /// </summary>
        public static Var<float[]> GESCHWR = new Var<float[]>("GESCHWR", new float[] { 0.50f, 0.75f, 0.9f, 1.5f, 0f, 0f });

        /// <summary>
        /// Enthält den Sprittverbrauch für alle Fahrzeugarten
        /// </summary>
        public static Var<float[]> VERBRAUCH = new Var<float[]>("VERBRAUCH", new float[] { 2.625f, 2.4375f, 0f, 1.875f, 0f, 0f });

        /// <summary>
        /// Enthält, welche Muntionsklassen von der ARTILLERIE verschossen werden können
        /// </summary>
        public static Var<int[]> SHOOTABLE0 = new Var<int[]>("SHOOTABLE0", new int[] { 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 });

        /// <summary>
        /// Enthält, welche Muntionsklassen von den PANZER verschossen werden können
        /// </summary>
        public static Var<int[]> SHOOTABLE1 = new Var<int[]>("SHOOTABLE1", new int[] { 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 });

        /// <summary>
        /// Enthält, welche Muntionsklassen von dem BAUFAHRZEUG verschossen werden können
        /// </summary>
        public static Var<int[]> SHOOTABLE2 = new Var<int[]>("SHOOTABLE2", new int[] { 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 });

        /// <summary>
        /// Enthält, welche Muntionsklassen von dem SCOUT verschossen werden können
        /// </summary>
        public static Var<int[]> SHOOTABLE3 = new Var<int[]>("SHOOTABLE3", new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 });

        /// <summary>
        /// Enthält, welche Muntionsklassen von dem RAKETEN-GESCHÜTZ verschossen werden können
        /// </summary>
        public static Var<int[]> SHOOTABLE4 = new Var<int[]>("SHOOTABLE4", new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        /// <summary>
        /// Enthält, welche Muntionsklassen von dem MG-GESCHÜTZ verschossen werden können
        /// </summary>
        public static Var<int[]> SHOOTABLE5 = new Var<int[]>("SHOOTABLE5", new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        /// <summary>
        /// Enthält, welche Geländesteigung (in Grad) das Fahrzeug erklimmen kann
        /// </summary>
        public static Var<int[]> WINKEL = new Var<int[]>("WINKEL", new int[] { 040, 040, 050, 070, 0, 0 });

        /// <summary>
        /// Enthält, wieviel Erfahrung die ARTILLERIE braucht, um die nächste Stufe aufzusteigen
        /// Jeweils für Stufe 1, 2 und 3
        /// </summary>
        public static Var<int[]> EXPTOLVUP0 = new Var<int[]>("EXPTOLVUP0", new int[] { 100, 100, 100 });

        /// <summary>
        /// Enthält, wieviel Erfahrung der PANZER braucht, um die nächste Stufe aufzusteigen
        /// Jeweils für Stufe 1, 2 und 3
        /// </summary>
        public static Var<int[]> EXPTOLVUP1 = new Var<int[]>("EXPTOLVUP1", new int[] { 100, 100, 100 });

        /// <summary>
        /// Enthält, wieviel Erfahrung das BAUFAHRZEUG braucht, um die nächste Stufe aufzusteigen
        /// Jeweils für Stufe 1, 2 und 3
        /// </summary>
        public static Var<int[]> EXPTOLVUP2 = new Var<int[]>("EXPTOLVUP2", new int[] { 100, 100, 100 });

        /// <summary>
        /// Enthält, wieviel Erfahrung der SCOUT braucht, um die nächste Stufe aufzusteigen
        /// Jeweils für Stufe 1, 2 und 3
        /// </summary>
        public static Var<int[]> EXPTOLVUP3 = new Var<int[]>("EXPTOLVUP3", new int[] { 100, 100, 100 });

        /// <summary>
        /// Enthält, wieviel Erfahrung das RAKETEN-GESCHÜTZ braucht, um die nächste Stufe aufzusteigen
        /// Jeweils für Stufe 1, 2 und 3
        /// </summary>
        public static Var<int[]> EXPTOLVUP4 = new Var<int[]>("EXPTOLVUP4", new int[] { 100, 100, 100 });

        /// <summary>
        /// Enthält, wieviel Erfahrung das MG-GESCHÜTZ braucht, um die nächste Stufe aufzusteigen
        /// Jeweils für Stufe 1, 2 und 3
        /// </summary>
        public static Var<int[]> EXPTOLVUP5 = new Var<int[]>("EXPTOLVUP5", new int[] { 100, 100, 100 });

        /// <summary>
        /// Die Lautstärke (Motor) je Fahrzeugtyp
        /// </summary>
        public static Var<float[]> VOLUMES = new Var<float[]>("VOLUMES", new float[] { 0.3f, 0.6f, 0.4f, 0.7f, 0f, 0f });

        /// <summary>
        /// Id des Blubb-Sounds, der benutz wird, ween das fahrzeug ins Wasser fällt
        /// </summary>
        public static Var<int[]> SINKSOUND = new Var<int[]>("SINKSOUND", new int[] { 0, 0, 1, 1, 0, 0 });

        /// <summary>
        /// Enthält, wieviel Erfahrung für die Zerstörung des Fahrzeugtyps zusteht 
        /// </summary>
        public static Var<int[]> EXPREWARDED = new Var<int[]>("EXPREWARDED", new int[] { 80, 90, 70, 60, 60, 60 });

        /// <summary>
        /// Enthält die Kosten eines neuen Fahrzeugs dieser Art
        /// </summary>
        public static Var<int[]> PREIS = new Var<int[]>("PREIS", new int[] { 1000, 1000, 1000, 1000, 0, 0 });

        /// <summary>
        /// Stellt fest, welche Fahrzeuge in einer Fabrik angeferigt werden können
        /// </summary>
        public static Var<int[]> KANNGEBAUTWERDEN = new Var<int[]>("KANNGEBAUTWERDEN", new int[] { 1, 1, 1, 1, 0, 0 });

        /// <summary>
        /// Beinhaltet die verschießbare Munition (alle Fahrzeuge, alle Munitionsarten)
        /// </summary>
        public static int[,] Shootable = new int[MAXHP.Wert.Count(), SHOOTABLE0.Wert.Count()];

        /// <summary>
        /// Enthält den "default" Inventar je Fahrzeug
        /// </summary>
        ///                                    Treibstoff, standardmissle, bigstandardmissle, cryomissle, poisonmissle, nukemissle, airstrike, geschoss, geschoss2, bauen, graben, minerot, minegelb, minegrün, mineblau, bunker, tunnel, geschütz, geschütz2,   mg, reparieren erobern
        public static int[,] InitInventar = {
                    /*Artillerie          */  { 000001000, 00000000000005, 00000000000000001, 0000000000, 000000000000, 0000000000, 000000000, 00000025, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Panzer              */  { 000002000, 00000000000005, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000020, 000000010, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Baufahrzeug         */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Scout               */  { 000003000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000015, 000000005, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz            */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz2           */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0}
                                            };

        /// <summary>
        /// Diese Items bekommt man zusätzlich zum normalen Inventar (mit dem Anteil von 0 bis 100%)
        /// </summary>
        public static int[,] VariabelInventar = {
                    /*Artillerie          */  { 000000000, 00000000000002, 00000000000000001, 0000000000, 000000000000, 0000000000, 000000000, 00000010, 000000010, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Panzer              */  { 000000300, 00000000000002, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000010, 000000005, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Baufahrzeug         */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Scout               */  { 000000500, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000010, 000000005, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0200, 0, 0},
                    /*Geschütz            */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz2           */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0}
                                            };
        /// <summary>
        /// Enthält, was das Fahrzeug an Items pro Runde bekommt
        /// </summary>
        public static int[,] RundenInventar = {
                    /*Artillerie          */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Panzer              */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Baufahrzeug         */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Scout               */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz            */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz2           */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0}
                                            };

        /// <summary>
        /// Die Maske des Fahrzeugs zur Schadensbestimmung (pro Fahrzeugtyp), wird in nicht genutzter Fahrlogik verwendet
        /// (! keine Verwendung)
        /// </summary>
        public static Vector2[][] Messpunkte = new Vector2[MAXHP.Wert.Count()][];

        /// <summary>
        ///gibt an, wieviel Exp das Fahrzeug braucht, um ein Level aufzusteigen
        /// </summary>
        public static int[,] ExpToLvUp = new int[MAXHP.Wert.Count(), EXPTOLVUP0.Wert.Count()];

        /// <summary>
        ///gibt an, wieviel Exp bei Abschuss dieses Fahrzeugs gibt
        /// </summary>
        public static int[] ExpRewarded = new int[MAXHP.Wert.Count()];

        /// <summary>
        /// Setzt die Tankdata zurück
        /// Ist leer
        /// </summary>
        public static void Reset_Tankdata()
        {
        }

        /// <summary>
        /// Ladet Shootable und ExpToLvUp mit Werten aus korrespondierenden modifizierbaren Variablen
        /// </summary>
        public static void LadePanzerdaten()
        {
            for (int i = 0; i < SHOOTABLE0.Wert.Count(); i++) Shootable[0, i] = SHOOTABLE0.Wert[i];
            for (int i = 0; i < SHOOTABLE1.Wert.Count(); i++) Shootable[1, i] = SHOOTABLE1.Wert[i];
            for (int i = 0; i < SHOOTABLE2.Wert.Count(); i++) Shootable[2, i] = SHOOTABLE2.Wert[i];
            for (int i = 0; i < SHOOTABLE3.Wert.Count(); i++) Shootable[3, i] = SHOOTABLE3.Wert[i];
            for (int i = 0; i < SHOOTABLE4.Wert.Count(); i++) Shootable[4, i] = SHOOTABLE4.Wert[i];
            for (int i = 0; i < SHOOTABLE5.Wert.Count(); i++) Shootable[5, i] = SHOOTABLE5.Wert[i];
            for (int i = 0; i < EXPTOLVUP0.Wert.Count(); i++) ExpToLvUp[0, i] = EXPTOLVUP0.Wert[i];
            for (int i = 0; i < EXPTOLVUP1.Wert.Count(); i++) ExpToLvUp[1, i] = EXPTOLVUP1.Wert[i];
            for (int i = 0; i < EXPTOLVUP2.Wert.Count(); i++) ExpToLvUp[2, i] = EXPTOLVUP2.Wert[i];
            for (int i = 0; i < EXPTOLVUP3.Wert.Count(); i++) ExpToLvUp[3, i] = EXPTOLVUP3.Wert[i];
            for (int i = 0; i < EXPTOLVUP4.Wert.Count(); i++) ExpToLvUp[4, i] = EXPTOLVUP4.Wert[i];
            for (int i = 0; i < EXPTOLVUP5.Wert.Count(); i++) ExpToLvUp[5, i] = EXPTOLVUP5.Wert[i];
            ExpRewarded = EXPREWARDED.Wert;
        }
    }
}