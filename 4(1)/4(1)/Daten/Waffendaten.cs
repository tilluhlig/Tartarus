using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    /// </summary>
    public static class Waffendaten
    {
        #region Waffennummern

        /// <summary>
        ///     The airstrike
        /// </summary>
        public static int airstrike = 5;

        /// <summary>
        ///     The bauen
        /// </summary>
        public static int bauen = 8;

        /// <summary>
        ///     The bigstandardmissle
        /// </summary>
        public static int bigstandardmissle = 1;

        /// <summary>
        ///     The bunker
        /// </summary>
        public static int bunker = 14;

        /// <summary>
        ///     The cryomissle
        /// </summary>
        public static int cryomissle = 2;

        /// <summary>
        ///     The mg
        /// </summary>
        public static int erobern = 20;

        /// <summary>
        ///     The geschoss
        /// </summary>
        public static int geschoss = 6;

        /// <summary>
        ///     The geschoss2
        /// </summary>
        public static int geschoss2 = 7;

        /// <summary>
        ///     The geschütz
        /// </summary>
        public static int geschütz = 16;

        /// <summary>
        ///     The geschütz2
        /// </summary>
        public static int geschütz2 = 17;

        /// <summary>
        ///     The graben
        /// </summary>
        public static int graben = 9;

        /// <summary>
        ///     The mg
        /// </summary>
        public static int mg = 18;

        /// <summary>
        ///     The mineblau
        /// </summary>
        public static int mineblau = 13;

        /// <summary>
        ///     The minegelb
        /// </summary>
        public static int minegelb = 11;

        /// <summary>
        ///     The minegrün
        /// </summary>
        public static int minegrün = 12;

        /// <summary>
        ///     The minerot
        /// </summary>
        public static int minerot = 10;

        /// <summary>
        ///     The nukemissle
        /// </summary>
        public static int nukemissle = 4;

        /// <summary>
        ///     The poisonmissle
        /// </summary>
        public static int poisonmissle = 3;

        /// <summary>
        ///     The mg
        /// </summary>
        public static int reparieren = 19;

        /// <summary>
        ///     The standardmissle
        /// </summary>
        public static int standardmissle = 0;

        /// <summary>
        ///     The tunnel
        /// </summary>
        public static int tunnel = 15;

        #endregion Waffennummern

        #region Daten

        /// <summary>
        ///     Sounds  ---  [Abschuss]
        /// </summary>
        public static int[] Abschuesse =
        {
            0, /*                                      */ 1,
            /*                                       */ 2, /*                                    */ 3,
            /*                                           */ 4, /*                                         */ 5,
            /*                                       */ 6, /*                                       */ 7,
            /*                                       */ 0, /*                                       */ 0,
            /*                             */ 0, /*                                   */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 8, /*                                                        */
            6, /*                                                        */ 6
        };

        /// <summary>
        ///     Cooldown der Rakete/ AP Kosten
        /// </summary>
        public static int[] APKosten =
        {
            600, /*                                      */ 1200,
            /*                                    */ 600, /*                                  */ 1200,
            /*                                        */ 12000, /*                                     */ 5000,
            /*                                    */ 30, /*                                      */ 150,
            /*                                     */ 0, /*                                       */ 0,
            /*                             */ 100, /*                                 */ 100,
            /*                                     */ 100, /*                                     */ 100,
            /*                                     */ 100, /*                                     */ 100,
            /*                                     */ 100, /*                                     */ 100,
            /*                                     */ 1, /*                                                       */ 5,
            /*                                                       */ 3
        };

        public static int[] Austauschgröße =
        {
            10, /*                                  */ 10,
            /*                                      */ 10, /*                                   */ 10,
            /*                                          */ 10, /*                                        */ 5,
            /*                                       */ 25, /*                                      */ 25,
            /*                                      */ 1, /*                                       */ 1,
            /*                             */ 10, /*                                  */ 10,
            /*                                      */ 10, /*                                      */ 10,
            /*                                      */ 1, /*                                       */ 1,
            /*                                       */ 1, /*                                       */ 1,
            /*                                       */ 100, /*                                                     */ 1,
            /*                                                      */ 1
        };

        /// <summary>
        ///     Brand nach Explosion
        ///     [Abstände zwischen Brandherden]
        /// </summary>
        public static int[] BrandAbstand =
        {
            15, /*                                   */ 15,
            /*                                      */ 99, /*                                   */ 99,
            /*                                          */ 25, /*                                        */ 15,
            /*                                      */ 5, /*                                       */ 10,
            /*                                      */ 10, /*                                      */ 10,
            /*                            */ 15, /*                                  */ 15,
            /*                                      */ 15, /*                                      */ 15,
            /*                                      */ 15, /*                                      */ 15,
            /*                                      */ 15, /*                                      */ 15,
            /*                                      */ 50, /*                                                       */
            50, /*                                                       */ 50
        };

        /// <summary>
        ///     Brandfläche der Rakete (Feuer.cs)
        /// </summary>
        public static int[] Brandherd =
        {
            0, /*                                       */ 0,
            /*                                       */ 0, /*                                    */ 0,
            /*                                           */ 350, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                             */ 0, /*                                   */ 50,
            /*                                      */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                                       */ 1,
            /*                                                       */ 1
        };

        /// <summary>
        ///     [Energie] [Partikel] [Breite] [Lebenszeit]
        /// </summary>
        public static Vector4[] Daten =
        {
            new Vector4(50, 25, 225, 750), /*           */ new Vector4(100, 30, 450, 1500),
            /*         */ new Vector4(50, 20, 50, 1000), /*        */ new Vector4(400, 15, 1000, 3000.0f), /*        */
            new Vector4(1000, 75, 1900, 1000.0f), /*       */ new Vector4(50, 20, 225, 750), /*           */
            new Vector4(10, 10, 50, 750), /*            */ new Vector4(25, 15, 150, 750), /*           */
            new Vector4(0, 0, 0, 0), /*                 */ new Vector4(0, 0, 0, 0), /*       */
            new Vector4(25, 15, 150, 750), /*       */ new Vector4(25, 15, 150, 750), /*           */
            new Vector4(25, 15, 150, 750), /*           */ new Vector4(25, 15, 150, 750), /*           */
            new Vector4(25, 15, 150, 750), /*           */ new Vector4(25, 15, 150, 750), /*           */
            new Vector4(25, 15, 150, 750), /*           */ new Vector4(25, 15, 150, 750), /*           */
            new Vector4(2, 0, 0, 0), /*                                  */ new Vector4(2, 0, 0, 0),
            /*                                  */ new Vector4(2, 0, 0, 0)
        };

        /// <summary>
        ///     [Timeout für Raketenfocus] [Explosionsverzoegerung] [Minentyp] [FREI]
        /// </summary>
        public static Vector4[] Daten2 =
        {
            new Vector4(80, 20, 0, 0), /*              */ new Vector4(100, 20, 0, 0),
            /*              */ new Vector4(80, 1, 0, 0), /*             */ new Vector4(230, 1, 0, 0),
            /*                   */ new Vector4(300, 20, 0, 0), /*                */ new Vector4(80, 20, 0, 0),
            /*               */ new Vector4(80, 20, 0, 0), /*               */ new Vector4(80, 20, 0, 0),
            /*               */ new Vector4(0, 0, 0, 0), /*                 */ new Vector4(0, 0, 0, 0), /*       */
            new Vector4(80, 20, 0, 0), /*           */ new Vector4(80, 20, 1, 0), /*               */
            new Vector4(80, 20, 2, 0), /*               */ new Vector4(80, 20, 3, 0), /*               */
            new Vector4(80, 20, 0, 0), /*               */ new Vector4(80, 20, 0, 0), /*               */
            new Vector4(80, 20, 0, 0), /*               */ new Vector4(80, 20, 0, 0), /*               */
            new Vector4(80, 1, 0, 0), /*                                 */ new Vector4(80, 1, 0, 0),
            /*                                  */ new Vector4(80, 1, 0, 0)
        };

        /// <summary>
        ///     Antriebsrauch
        ///     [Partikel anz.] [Size] [Lebenszeit] [Dichte der Partikelfarbe]
        /// </summary>
        public static Vector4[] Daten3 =
        {
            new Vector4(10, 5, 400, 100), /*           */ new Vector4(10, 30, 200, 50),
            /*            */ new Vector4(10, 10, 300, 50), /*         */ new Vector4(10, 15, 200, 50),
            /*                */ new Vector4(20, 17, 600, 160), /*             */ new Vector4(10, 10, 400, 30),
            /*            */ new Vector4(3, 1, 1000, 30), /*             */ new Vector4(5, 1, 1000, 30),
            /*             */ new Vector4(0, 0, 0, 0), /*                 */ new Vector4(0, 0, 0, 0), /*       */
            new Vector4(0, 0, 0, 0), /*             */ new Vector4(0, 0, 0, 0), /*                 */
            new Vector4(0, 0, 0, 0), /*                 */ new Vector4(0, 0, 0, 0), /*                 */
            new Vector4(0, 0, 0, 0), /*                 */ new Vector4(0, 0, 0, 0), /*                 */
            new Vector4(0, 0, 0, 0), /*                 */ new Vector4(0, 0, 0, 0), /*                 */
            new Vector4(2, 2, 5, 30), /*                                 */ new Vector4(2, 2, 5, 30),
            /*                                  */ new Vector4(2, 2, 5, 30)
        };

        /// <summary>
        ///     [Rot] [Grün] [Blau]  für Antriebsrauch
        /// </summary>
        public static Vector3[] Daten4 =
        {
            new Vector3(4f, 4f, 4f), /*              */ new Vector3(2.8f, 2f, 2f),
            /*               */ new Vector3(0.8f, 1.6f, 3f), /*          */ new Vector3(2.0f, 1.2f, 0.4f),
            /*               */ new Vector3(2.8f, 2f, 2f), /*                 */ new Vector3(2.8f, 2f, 2f),
            /*               */ new Vector3(0.7f, 0.7f, 0.5f), /*           */ new Vector3(0.7f, 0.7f, 0.5f),
            /*           */ new Vector3(0, 0, 0), /*                    */ new Vector3(0, 0, 0), /*          */
            new Vector3(0, 0, 0), /*                */ new Vector3(0, 0, 0), /*                    */
            new Vector3(0, 0, 0), /*                    */ new Vector3(0, 0, 0), /*                    */
            new Vector3(0, 0, 0), /*                    */ new Vector3(0, 0, 0), /*                    */
            new Vector3(0, 0, 0), /*                    */ new Vector3(0, 0, 0), /*                    */
            new Vector3(2.8f, 2f, 2f), /*                                */ new Vector3(2.8f, 2f, 2f),
            /*                                 */ new Vector3(2.8f, 2f, 2f)
        };

        /// <summary>
        ///     Energiefaktor
        /// </summary>
        public static float[] Energiefaktor =
        {
            3f, /*                                */ 2f,
            /*                                      */ 1.0f, /*                                 */ 1.0f,
            /*                                        */ 1.1f, /*                                      */ 2.0f,
            /*                                    */ 1.0f, /*                                    */ 1.0f,
            /*                                    */ 0.0f, /*                                    */ 0.0f,
            /*                          */ 2.0f, /*                                */ 2.0f,
            /*                                    */ 2.0f, /*                                    */ 2.0f,
            /*                                    */ 2.0f, /*                                    */ 2.0f,
            /*                                    */ 2.0f, /*                                    */ 2.0f,
            /*                                    */ 2.0f, /*                                                    */ 1,
            /*                                                       */ 1
        };

        /// <summary>
        ///     Sounds  ---  [Explosion]
        /// </summary>
        public static int[] Explosionen =
        {
            0, /*                                     */ 1,
            /*                                       */ 2, /*                                    */ 3,
            /*                                           */ 4, /*                                         */ 0,
            /*                                       */ 6, /*                                       */ 7,
            /*                                       */ 0, /*                                       */ 0,
            /*                             */ 0, /*                                   */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ -1, /*                                                       */
            6, /*                                                        */ 6
        };

        /// <summary>
        ///     Energiefaktor
        /// </summary>
        public static float[] Explosionscale =
        {
            70f, /*                              */ 70f,
            /*                                     */ 70.0f, /*                                */ 70.0f,
            /*                                       */ 70f, /*                                       */ 70f,
            /*                                     */ 20f, /*                                     */ 20f,
            /*                                     */ 70f, /*                                     */ 70f,
            /*                           */ 70f, /*                                 */ 70f,
            /*                                     */ 70f, /*                                     */ 2.0f,
            /*                                    */ 70f, /*                                     */ 70f,
            /*                                     */ 70f, /*                                     */ 70f,
            /*                                     */ 10f, /*                                                     */ 1,
            /*                                                       */ 1
        };

        /// <summary>
        ///     Slotgröße
        /// </summary>
        public static int[] Fachgröße =
        {
            10, /*                                       */ 10,
            /*                                      */ 10, /*                                   */ 10,
            /*                                          */ 10, /*                                        */ 5,
            /*                                       */ 25, /*                                      */ 25,
            /*                                      */ 1, /*                                       */ 1,
            /*                             */ 10, /*                                  */ 10,
            /*                                      */ 10, /*                                      */ 10,
            /*                                      */ 1, /*                                       */ 1,
            /*                                       */ 1, /*                                       */ 1,
            /*                                       */ 100, /*                                                     */ 1,
            /*                                                      */ 1
        };

        // Name
        //                                 standardmissle                                bigmissle                                      cryomissle                                  poisonmissle                                       nukemissle                                       airstrike                                      geschoss                                       geschoss2                                      bauen                                          graben                               minerot                                    minegelb                                       minegrün                                       mineblau                                       bunker                                         tunnel                                         Geschütz                                       Geschütz2                                      MG                                                              Reparieren                                                      Erobern
        // Basiswerte
        // [Rot] [Grün] [Blau]
        /// <summary>
        ///     The farben
        /// </summary>
        public static Vector3[] Farben =
        {
            new Vector3(0.97254902f, 0.97254902f, 1), /**/
            new Vector3(0.97254902f, 0.97254902f, 1), /**/ new Vector3(0, 0.749019608f, 1), /*      */
            new Vector3(0, 500, 0), /*                      */ new Vector3(0.97254902f, 0.97254902f, 1), /*  */
            new Vector3(0.97254902f, 0.97254902f, 1), /**/ new Vector3(0.97254902f, 0.97254902f, 1), /**/
            new Vector3(0.97254902f, 0.97254902f, 1), /**/ new Vector3(0.97254902f, 0.97254902f, 1), /**/
            new Vector3(0, 0, 0), /*          */ new Vector3(0, 0, 0), /*                */
            new Vector3(0.97254902f, 0.97254902f, 1), /**/ new Vector3(0.97254902f, 0.97254902f, 1), /**/
            new Vector3(0.97254902f, 0.97254902f, 1), /**/ new Vector3(0f, 0f, 0f), /*                 */
            new Vector3(02f, 0f, 0f), /*                */ new Vector3(0f, 0f, 0f), /*                 */
            new Vector3(0f, 0f, 0f), /*                 */ new Vector3(0.97254902f, 0.97254902f, 1),
            /*                 */ new Vector3(0.97254902f, 0.97254902f, 1), /*                 */
            new Vector3(0.97254902f, 0.97254902f, 1)
        };

        /// <summary>
        ///     min Abschusskraft
        /// </summary>
        public static int[] minShootingpower =
        {
            0, /*                                */ 0,
            /*                                       */ 0, /*                                    */ 0,
            /*                                           */ 0, /*                                         */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                             */ 0, /*                                   */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 100, /*                                                     */
            100, /*                                                       */ 100
        };

        public static int[] Preis =
        {
            10, /*                                           */ 10,
            /*                                      */ 10, /*                                   */ 10,
            /*                                          */ 10, /*                                        */ 5,
            /*                                       */ 25, /*                                      */ 25,
            /*                                      */ 1, /*                                       */ 1,
            /*                             */ 10, /*                                  */ 10,
            /*                                      */ 10, /*                                      */ 10,
            /*                                      */ 1, /*                                       */ 1,
            /*                                       */ 1, /*                                       */ 1,
            /*                                       */ 1, /*                                                       */ 1,
            /*                                                      */ 1
        };

        /// <summary>
        ///     Gibt an, ob es eine Munitionsart zum verschiessen ist (0== nein, 1==ja, 2==Airstrike, 3==Mine, 4==MG)
        /// </summary>
        public static int[] Verschiessbar =
        {
            1, /*                                   */ 1,
            /*                                       */ 1, /*                                    */ 1,
            /*                                           */ 1, /*                                         */ 2,
            /*                                       */ 1, /*                                       */ 1,
            /*                                       */ 0, /*                                       */ 0,
            /*                             */ 3, /*                                   */ 3,
            /*                                       */ 3, /*                                       */ 3,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 4, /*                                                        */
            5, /*                                                        */ 5
        };

        /// <summary>
        ///     Sounds --- [Wassertreffer]
        /// </summary>
        public static int[] Waterboom =
        {
            0, /*                                       */ 0,
            /*                                       */ 0, /*                                    */ 0,
            /*                                           */ 0, /*                                         */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                             */ 0, /*                                   */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                       */ 0,
            /*                                       */ 0, /*                                                        */
            6, /*                                                        */ 6
        };

        #region DEBUG

#if DEBUG

        /// <summary>
        ///     Skalierung der Rakete
        /// </summary>
        public static float[] Skalierung =
        {
            0.035f, /*                                */ 0.035f,
            /*                                  */ 0.02f, /*                                */ 0.035f,
            /*                                      */ 0.04f, /*                                    */ 0.035f,
            /*                                  */ 0.075f, /*                                  */ 0.03f,
            /*                                   */ 0, /*                                       */ 0,
            /*                             */ 0.05f, /*                               */ 0.05f,
            /*                                   */ 0.05f, /*                                   */ 0.05f,
            /*                                   */ 0.05f, /*                                   */ 0.05f,
            /*                                   */ 0.05f, /*                                   */ 0.05f,
            /*                                   */ 0.0035f, /*                                                 */
            0.0035f, /*                                                 */ 0.0035f
        };

#else

    /// <summary>
    /// Skalierung der Rakete
    /// </summary>
        public static float[] Skalierung = { 1f, /*                                    */  1f, /*                                     */ 1f, /*                                   */ 1f, /*                                          */ 1f, /*                                        */ 1, /*                                       */ 1f, /*                                      */ 1f, /*                                      */ 0, /*                                       */ 0, /*                             */ 1f, /*                                  */ 1f, /*                                      */ 1f, /*                                      */ 1f, /*                                      */ 1f, /*                                       */ 1f, /*                                     */ 1f, /*                                      */ 1f, /*                                       */ 0.046666667f, /*                                          */ 0.046666667f, /*                                             */ 0.046666667f };

#endif

        #endregion DEBUG

        /// <summary>
        ///     Zentrumschaden bei Direkttreffer
        /// </summary>
        public static int[] Zentrumschaden =
        {
            5000, /*                               */ 10000,
            /*                                   */ 0, /*                                    */ 0,
            /*                                           */ 25000, /*                                     */ 5000,
            /*                                    */ 500, /*                                    */ 1000,
            /*                                    */ 0, /*                                       */ 0,
            /*                             */ 1300, /*                                */ 1300,
            /*                                    */ 1300, /*                                    */ 1300,
            /*                                    */ 1300, /*                                    */ 1300,
            /*                                    */ 1300, /*                                    */ 1300,
            /*                                    */ 50, /*                                                      */ 1,
            /*                                                       */ 1
        };

        private static List<string> Waffentexte;

        #endregion Daten

        #region Methods

        public static String GibWaffentext(int id)
        {
            if (Waffentexte == null)
            {
                if (File.Exists("Content\\Konfiguration\\Waffenbeschreibungen.txt"))
                {
                    Waffentexte = new List<string>();
                    String aktuell = "";

                    var b = new ReaderStream.ReaderStream("Content\\Konfiguration\\Waffenbeschreibungen.txt");

                    while (!b.EndOfStream)
                    {
                        String d = b.ReadLine();
                        if (d == "---")
                        {
                            Waffentexte.Add(aktuell);
                            aktuell = "";
                        }
                        else
                            aktuell = aktuell + d + "\n";
                    }

                    if (aktuell != "")
                        Waffentexte.Add(aktuell);

                    b.Close();
                }
            }

            if (Waffentexte == null) return "Waffenbeschreibungen.txt existiert nicht";

            if (id < 0 || id >= Waffentexte.Count) return "Falsche ID";

            return Waffentexte[id];
        }

        #endregion Methods
    }
}