// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-25-2013
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
    /// Class Tankdata
    /// </summary>
    public static class Fahrzeugdaten
    {
        #region FahrzeugIDs

        /// <summary>
        /// The artillerie
        /// </summary>
        public static int ARTILLERIE = 0;

        /// <summary>
        /// The panzer
        /// </summary>
        public static int PANZER = 1;

        /// <summary>
        /// The baufahrzeug
        /// </summary>
        public static int BAUFAHRZEUG = 2;

        /// <summary>
        /// The scout
        /// </summary>
        public static int SCOUT = 3;

        /// <summary>
        /// The geschütz
        /// </summary>
        public static int GESCHÜTZ = 4;

        /// <summary>
        /// The geschütz2
        /// </summary>
        public static int GESCHÜTZ2 = 5;

        #endregion FahrzeugIDs

        /// <summary>
        /// The MAXANGLEA
        /// </summary>
        public static Var<float[]> MAXANGLEA = new Var<float[]>("MAXANGLEA", new float[] { MathHelper.ToRadians(80), MathHelper.ToRadians(35), MathHelper.ToRadians(0), MathHelper.ToRadians(0), MathHelper.ToRadians(35), MathHelper.ToRadians(35) });

        /// <summary>
        /// The MAXANGLEB
        /// </summary>
        public static Var<float[]> MAXANGLEB = new Var<float[]>("MAXANGLEA", new float[] { MathHelper.ToRadians(100), MathHelper.ToRadians(155), MathHelper.ToRadians(180), MathHelper.ToRadians(180), MathHelper.ToRadians(155), MathHelper.ToRadians(155) });

        /// <summary>
        /// The MAXHP
        /// </summary>
        public static Var<int[]> MAXHP = new Var<int[]>("MAXHP", new int[] { 10000, 10000, 10000, 5000, 5000, 5000 });

        /// <summary>
        /// The SCALEP
        /// </summary>
        public static Var<float[]> SCALEP = new Var<float[]>("SCALEP", new float[] { 0.25f, 0.19f, 0.32f, 0.11f, 0.45f, 0.45f });

        /// <summary>
        /// The SCALER
        /// </summary>
        public static Var<float[]> SCALER = new Var<float[]>("SCALER", new float[] { 0.25f, 0.19f, 0.32f, 0.11f, 0.45f, 0.45f });

        /// <summary>
        /// The ARBEITSBEREICH
        /// </summary>
        public static Var<int[]> KANNHANDELN = new Var<int[]>("KANNHANDELN", new int[] { 1, 1, 1, 1, 0, 0 });

        /// <summary>
        /// The ARBEITSBEREICH
        /// </summary>
        public static Var<int[]> ARBEITSBEREICH = new Var<int[]>("ARBEITSBEREICH", new int[] { 1000, 1000, 1000, 1000, 0, 0 });

        /// <summary>
        /// The FAHRM
        /// </summary>
        public static Var<int[]> FAHRM = new Var<int[]>("FAHRM", new int[] { 32, 34, 32, 32, 32, 32 });

        /// <summary>
        /// The FALLG
        /// </summary>
        public static Var<float[]> FALLG = new Var<float[]>("FALLG", new float[] { 1.00f, 1.00f, 1.00f, 1.5f, 1.5f, 1.5f });

        /// <summary>
        /// The FALLW
        /// </summary>
        public static Var<float[]> FALLW = new Var<float[]>("FALLW", new float[] { 0.03f, 0.03f, 0.025f, 0.25f, 0.25f, 0.25f });

        /// <summary>
        /// The GESCHWV
        /// </summary>
        public static Var<float[]> GESCHWV = new Var<float[]>("GESCHWV", new float[] { 1.0f, 1.5f, 1.8f, 3f, 0f, 0f });

        /// <summary>
        /// The GESCHWR
        /// </summary>
        public static Var<float[]> GESCHWR = new Var<float[]>("GESCHWR", new float[] { 0.50f, 0.75f, 0.9f, 1.5f, 0f, 0f });

        public static Var<float[]> VERBRAUCH = new Var<float[]>("VERBRAUCH", new float[] { 2.625f, 2.4375f, 0f, 1.875f, 0f, 0f });

        // Artillerie
        /// <summary>
        /// The SHOOTABL e0
        /// </summary>
        public static Var<int[]> SHOOTABLE0 = new Var<int[]>("SHOOTABLE0", new int[] { 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 });

        // Panzer
        /// <summary>
        /// The SHOOTABL e1
        /// </summary>
        public static Var<int[]> SHOOTABLE1 = new Var<int[]>("SHOOTABLE1", new int[] { 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 });

        // Baufahrzeug
        /// <summary>
        /// The SHOOTABL e2
        /// </summary>
        public static Var<int[]> SHOOTABLE2 = new Var<int[]>("SHOOTABLE2", new int[] { 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1 });

        // Scout
        /// <summary>
        /// The SHOOTABL e3
        /// </summary>
        public static Var<int[]> SHOOTABLE3 = new Var<int[]>("SHOOTABLE3", new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 });

        // Geschütz
        /// <summary>
        /// The SHOOTABL e4
        /// </summary>
        public static Var<int[]> SHOOTABLE4 = new Var<int[]>("SHOOTABLE4", new int[] { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        // Geschütz2
        /// <summary>
        /// The SHOOTABL e5
        /// </summary>
        public static Var<int[]> SHOOTABLE5 = new Var<int[]>("SHOOTABLE5", new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

        /// <summary>
        /// The WINKEL
        /// </summary>
        public static Var<int[]> WINKEL = new Var<int[]>("WINKEL", new int[] { 040, 040, 050, 070, 0, 0 });

        /// <summary>
        /// The EXPTOLVU p0
        /// </summary>
        public static Var<int[]> EXPTOLVUP0 = new Var<int[]>("EXPTOLVUP0", new int[] { 100, 100, 100 });

        /// <summary>
        /// The EXPTOLVU p1
        /// </summary>
        public static Var<int[]> EXPTOLVUP1 = new Var<int[]>("EXPTOLVUP1", new int[] { 100, 100, 100 });

        /// <summary>
        /// The EXPTOLVU p2
        /// </summary>
        public static Var<int[]> EXPTOLVUP2 = new Var<int[]>("EXPTOLVUP2", new int[] { 100, 100, 100 });

        /// <summary>
        /// The EXPTOLVU p3
        /// </summary>
        public static Var<int[]> EXPTOLVUP3 = new Var<int[]>("EXPTOLVUP3", new int[] { 100, 100, 100 });

        /// <summary>
        /// The EXPTOLVU p4
        /// </summary>
        public static Var<int[]> EXPTOLVUP4 = new Var<int[]>("EXPTOLVUP4", new int[] { 100, 100, 100 });

        /// <summary>
        /// The EXPTOLVU p5
        /// </summary>
        public static Var<int[]> EXPTOLVUP5 = new Var<int[]>("EXPTOLVUP5", new int[] { 100, 100, 100 });

        /// <summary>
        /// The VOLUMES
        /// </summary>
        public static Var<float[]> VOLUMES = new Var<float[]>("VOLUMES", new float[] { 0.3f, 0.6f, 0.4f, 0.7f, 0f, 0f });

        /// <summary>
        /// The SINKSOUND
        /// </summary>
        public static Var<int[]> SINKSOUND = new Var<int[]>("SINKSOUND", new int[] { 0, 0, 1, 1, 0, 0 });

        /// <summary>
        /// The EXPREWARDED
        /// </summary>
        public static Var<int[]> EXPREWARDED = new Var<int[]>("EXPREWARDED", new int[] { 80, 90, 70, 60, 60, 60 });

        public static Var<int[]> PREIS = new Var<int[]>("PREIS", new int[] { 1000, 1000, 1000, 1000, 0, 0 });

        public static Var<int[]> KANNGEBAUTWERDEN = new Var<int[]>("PREIS", new int[] { 1, 1, 1, 1, 0, 0 });

        /// <summary>
        /// The shootable
        /// </summary>
        public static int[,] Shootable = new int[MAXHP.Wert.Count(), SHOOTABLE0.Wert.Count()]; // Abfeuerbare Waffen

        /// <summary>
        /// The init inventar
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

        public static int[,] VariabelInventar = {
                    /*Artillerie          */  { 000000000, 00000000000002, 00000000000000001, 0000000000, 000000000000, 0000000000, 000000000, 00000010, 000000010, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Panzer              */  { 000000300, 00000000000002, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000010, 000000005, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Baufahrzeug         */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Scout               */  { 000000500, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000010, 000000005, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0200, 0, 0},
                    /*Geschütz            */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz2           */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0}
                                            };

        public static int[,] RundenInventar = {
                    /*Artillerie          */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Panzer              */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Baufahrzeug         */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Scout               */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz            */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0},
                    /*Geschütz2           */  { 000000000, 00000000000000, 00000000000000000, 0000000000, 000000000000, 0000000000, 000000000, 00000000, 000000000, 00000, 000000, 0000000, 00000000, 00000000, 00000000, 000000, 000000, 00000000, 000000000, 0000, 0, 0}
                                            };

        /// <summary>
        /// The messpunkte
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
        /// Reset_s the tankdata.
        /// </summary>
        public static void Reset_Tankdata()
        {
        }

        /// <summary>
        /// Lade_s the tankdata.
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