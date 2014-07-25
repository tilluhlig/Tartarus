// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 08-02-2013
//
// Last Modified By : Till
// Last Modified On : 08-04-2013
// ***********************************************************************
// <copyright file="Optimierung.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Linq;

namespace _4_1_
{
    /// <summary>
    /// Skaliert die Bilddateien auf ihre genutzte Größe
    /// </summary>
    public static class Optimierung
    {
        /// <summary>
        /// Optimieres the specified richtung.
        /// </summary>
        /// <param name="Richtung">if set to <c>true</c> [richtung].</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere(bool Richtung)
        {
            int anz = 0;
            anz += Optimiere_Bäume(Richtung);
            anz += Optimiere_Häuser(Richtung);
            anz += Optimiere_Fahrzeuge(Richtung);
            anz += Optimiere_Kisten(Richtung);
            anz += Optimiere_Tunnel(Richtung);
            anz += Optimiere_Waffen(Richtung);
            anz += Optimiere_Sonstige(Richtung);
            return anz;
        }

        /// <summary>
        /// Gibt je nachdem, ob DEBUG definiert ist, den Skalierungswert zurück
        /// </summary>
        /// <param name="Wert">Der Wert bei DEBUG</param>
        /// <returns>Wert wenn DEBUG , Differenz zu 1.0f wenn !DEBUG</returns>
        public static float Skalierung(float Wert)
        {
            #region DEBUG

#if DEBUG
            return Wert;
#else
            return 1.0f;
#endif

            #endregion DEBUG
        }

        /// <summary>
        /// Skaliert die Bilddateien der Bäume auf ihre genutzte Größe
        /// </summary>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere_Bäume(bool Richtung)
        {
            // Optimiere Baeume
            String[] BaumBilder = { "Baum", "Baum2", "Baum3", "Baum4", "Baum5", "Baum6", "Baum7", "Baum8", "Baum9", "a_tree04", "a_tree05", "a_tree06", "a_tree08", "a_tree12", "a_tree13", "a_tree22", "Palm_01", "Palm_02", "Palm_03", "Palm_04", "Palm_05", "Palm_06", "Palm_07", "Palm_08", "Palm_p01", "Palm_p02", "Palm_p03", "Palm_p04" };

            for (int i = 0; i < BaumBilder.Count(); i++)
            {
                Skaliere(BaumBilder[i], ".png", Baumdata.SKALIERUNG.oldWert[i], Richtung);
            }
            return BaumBilder.Count();
        }

        /// <summary>
        /// Skaliert die Bilddateien der Fahrzeuge auf ihre genutzte Größe
        /// </summary>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere_Fahrzeuge(bool Richtung)
        {
            String[] Bilder = { "Artillerie2", "Panzer2", "Baufahrzeug2", "Scout2", "Geschuetz", "Geschuetz2" };
            for (int i = 0; i < Bilder.Count(); i++)
            {
                Skaliere(Bilder[i], ".png", Fahrzeugdaten.SCALEP.oldWert[i], Richtung);
            }

            String[] Bilder2 = { "ArtillerieRuine", "PanzerRuine", "BaufahrzeugRuine", "ScoutRuine", "Geschuetz", "Geschuetz2" };
            for (int i = 0; i < Bilder2.Count(); i++)
            {
                Skaliere(Bilder2[i], ".png", Fahrzeugdaten.SCALEP.oldWert[i], Richtung);
            }

            String[] Bilder3 = { "nichts", "nichts", "nichts", "ScoutReifen", "nichts", "nichts" };
            for (int i = 0; i < Bilder3.Count(); i++)
            {
                Skaliere(Bilder3[i], ".png", Fahrzeugdaten.SCALEP.oldWert[i], Richtung);
            }

            String[] Bilder4 = { "Artillerie2Rohr", "Panzer2Rohr", "nichts", "Scout2Rohr", "GeschuetzRohr", "Geschuetz2Rohr" };
            for (int i = 0; i < Bilder4.Count(); i++)
            {
                Skaliere(Bilder4[i], ".png", Fahrzeugdaten.SCALER.oldWert[i], Richtung);
            }

            return Bilder.Count() + Bilder2.Count() + Bilder3.Count() + Bilder4.Count();
        }

        /// <summary>
        /// Skaliert die Bilddateien der Häuser auf ihre genutzte Größe
        /// </summary>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere_Häuser(bool Richtung)
        {
            // Optimiere Häuser
            String[] HausBilder = { "Haus", "Haus2", "Haus3", "Haus4", "Haus5", "Haus6", "Haus7", "Haus8", "Haus9", "Haus10", "Haus11", "Haus12", "Haus13", "Haus14", "Haus15", "Haus16", "Haus17", "Haus18" };

            for (int i = 0; i < HausBilder.Count(); i++)
            {
                Skaliere(HausBilder[i], ".png", Gebäudedaten.SKALIERUNG.oldWert[i], Richtung);
            }
            return HausBilder.Count();
        }

        /// <summary>
        /// Skaliert die Bilddateien der Kisten auf ihre genutzte Größe
        /// </summary>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere_Kisten(bool Richtung)
        {
            Skaliere("kiste", ".png", Kiste.sc, Richtung);
            return 1;
        }

        /// <summary>
        /// Skaliert die Bilddateien der Übrigen auf ihre genutzte Größe
        /// </summary>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere_Sonstige(bool Richtung)
        {
            foreach (var pair in Texturen.Bilddateien)
            {
                Skaliere(pair.Key, ".png", pair.Value, Richtung);
            }
            return Texturen.Bilddateien.Count;
        }

        /// <summary>
        /// Skaliert die Bilddateien der Tunnel auf ihre genutzte Größe
        /// </summary>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere_Tunnel(bool Richtung)
        {
            return 1;
        }

        /// <summary>
        /// Skaliert die Bilddateien der Waffen auf ihre genutzte Größe
        /// </summary>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        /// <returns>System.Int32.</returns>
        public static int Optimiere_Waffen(bool Richtung)
        {
            String[] WaffenBilder = { "missle","bigmissle","cryomissle","poisonrocket1","nuke","missle","geschoss"
                                        ,"geschoss2","nichts","nichts","Mine1","Mine4","Mine3","Mine2","nichts"
                                        ,"nichts","nichts","nichts","nichts"};

            for (int i = 0; i < WaffenBilder.Count(); i++)
            {
                Skaliere(WaffenBilder[i], ".png", Waffendaten.Skalierung[i], Richtung);
            }
            return WaffenBilder.Count();
        }

        /// <summary>
        /// Skaliert eine Datei mit dem relativen Pfad
        /// </summary>
        /// <param name="Datei">der relative Pfad in "Textures\\Datei"</param>
        /// <param name="Extension">The extension.</param>
        /// <param name="Skalierung">Skalierung ... 0.4f</param>
        /// <param name="Richtung">Skalieren auf Original oder Ideal,   true = auf Release größe, false = Debug</param>
        private static void Skaliere(String Datei, String Extension, float Skalierung, bool Richtung)
        {
            string[] list = Environment.CurrentDirectory.Split('\\');
            String Path2 = "";
            for (int b = 0; b < list.Count() - 4; b++) Path2 = Path2 + list[b] + "\\";

            // Backup Ordner erstellen
            if (!Directory.Exists(Path2 + "4(1)Content\\Backup\\Textures"))
            {
                Directory.CreateDirectory(Path2 + "4(1)Content\\Backup\\Textures");
            }

            if (Richtung == false)
            {
                if (File.Exists(Path2 + "4(1)Content\\Backup\\Textures\\" + Datei + Extension))
                {
                    if (!Directory.Exists(Path.GetDirectoryName(Path2 + "4(1)Content\\Textures\\" + Datei + Extension)))
                        Directory.CreateDirectory(Path.GetDirectoryName(Path2 + "4(1)Content\\Textures\\" + Datei + Extension));

                    File.Copy(Path2 + "4(1)Content\\Backup\\Textures\\" + Datei + Extension, Path2 + "4(1)Content\\Textures\\" + Datei + Extension, true);
                }
            }
            else
                if (Richtung == true)
                {
                    // Datei sichern
                    if (!File.Exists(Path2 + "4(1)Content\\Backup\\Textures\\" + Datei + Extension))
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(Path2 + "4(1)Content\\Backup\\Textures\\" + Datei + Extension)))
                            Directory.CreateDirectory(Path.GetDirectoryName(Path2 + "4(1)Content\\Backup\\Textures\\" + Datei + Extension));

                        File.Copy(Path2 + "4(1)Content\\Textures\\" + Datei + Extension, Path2 + "4(1)Content\\Backup\\Textures\\" + Datei + Extension, true);
                    }

                    // Skalieren
                    String SkalierungS = Convert.ToString((float)((float)Skalierung * 100)).Replace(',', '.');
                    String zo = "\"" + Path2 + "4(1)Content\\Backup\\Textures\\" + Datei + Extension + "\" -resize " + SkalierungS + "% \"" + Path2 + "4(1)Content\\Textures\\" + Datei + Extension + "\"";
                    System.Diagnostics.Process Prozess = System.Diagnostics.Process.Start("..\\..\\..\\..\\ImageMagick\\convert.exe", zo);
                    //do
                    //{
                    System.Threading.Thread.Sleep(300);
                    // } while (!Prozess.HasExited);
                }
        }
    }
}