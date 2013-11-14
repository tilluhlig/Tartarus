// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 08-03-2013
//
// Last Modified By : Till
// Last Modified On : 08-04-2013
// ***********************************************************************
// <copyright file="SpezialBefehle.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    /// Diese Klasse verwaltet gesonderte Befehle, die per "String" aufgerufen werden
    /// </summary>
    public static class SpezialBefehle
    {
        /// <summary>
        /// Die Bezeichner der Befehle, wird in "Eingabefenster.cs" genutzt, um Befehle anzubieten
        /// </summary>
        public static String[] Befehle = { "TEXTUREN_RELEASE_ALLE", "TEXTUREN_DEBUG_ALLE",
                                           "TEXTUREN_RELEASE_BAEUME", "TEXTUREN_DEBUG_BAEUME",
                                           "TEXTUREN_RELEASE_HAEUSER", "TEXTUREN_DEBUG_HAEUSER",
                                           "TEXTUREN_RELEASE_FAHRZEUGE", "TEXTUREN_DEBUG_FAHRZEUGE",
                                           "TEXTUREN_RELEASE_KISTEN", "TEXTUREN_DEBUG_KISTEN",
                                           "TEXTUREN_RELEASE_TUNNEL", "TEXTUREN_DEBUG_TUNNEL",
                                           "TEXTUREN_RELEASE_WAFFEN", "TEXTUREN_DEBUG_WAFFEN",
                                           "TEXTUREN_RELEASE_SONSTIGE", "TEXTUREN_DEBUG_SONSTIGE",
                                         "SCHNEE","FEHLER"
                                         };

        /// <summary>
        /// Diese Methode Prüft, ob der übergebene Text ein spezieller Befehl ist
        /// </summary>
        /// <param name="Text">Der zu prüfende Text</param>
        /// <returns>Gibt zurück, ob es ein Befehl ist:  true = ja, false = nein</returns>
        public static bool PrüfeBefehl(String Text)
        {
            Text = Text.ToUpper();

            // "TEXTUREN_RELEASE_ALLE"
            if (Text == Befehle[0])
            {
                Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere(true).ToString());
                return true;
            }
            else

                // "TEXTUREN_DEBUG_ALLE"
                if (Text == Befehle[1])
                {
                    Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere(false).ToString());
                    return true;
                }
                else

                    // "TEXTUREN_RELEASE_BAEUME"
                    if (Text == Befehle[2])
                    {
                        Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Bäume(true).ToString());
                        return true;
                    }
                    else

                        // "TEXTUREN_DEBUG_BAEUME"
                        if (Text == Befehle[3])
                        {
                            Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Bäume(false).ToString());
                            return true;
                        }

                        else

                            // "TEXTUREN_RELEASE_HAEUSER"
                            if (Text == Befehle[4])
                            {
                                Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Häuser(true).ToString());
                                return true;
                            }
                            else

                                // "TEXTUREN_DEBUG_HAEUSER"
                                if (Text == Befehle[5])
                                {
                                    Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Häuser(false).ToString());
                                    return true;
                                }

                                else

                                    // "TEXTUREN_RELEASE_FAHRZEUGE"
                                    if (Text == Befehle[6])
                                    {
                                        Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Fahrzeuge(true).ToString());
                                        return true;
                                    }
                                    else

                                        // "TEXTUREN_DEBUG_FAHRZEUGE"
                                        if (Text == Befehle[7])
                                        {
                                            Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Fahrzeuge(false).ToString());
                                            return true;
                                        }

                                        else

                                            // "TEXTUREN_RELEASE_KISTEN"
                                            if (Text == Befehle[8])
                                            {
                                                Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Kisten(true).ToString());
                                                return true;
                                            }
                                            else

                                                // "TEXTUREN_DEBUG_KISTEN"
                                                if (Text == Befehle[9])
                                                {
                                                    Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Kisten(false).ToString());
                                                    return true;
                                                }

                                                else

                                                    // "TEXTUREN_RELEASE_TUNNEL"
                                                    if (Text == Befehle[10])
                                                    {
                                                        Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Tunnel(true).ToString());
                                                        return true;
                                                    }
                                                    else

                                                        // "TEXTUREN_DEBUG_TUNNEL"
                                                        if (Text == Befehle[11])
                                                        {
                                                            Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Tunnel(false).ToString());
                                                            return true;
                                                        }

                                                        else

                                                            // "TEXTUREN_RELEASE_WAFFEN"
                                                            if (Text == Befehle[12])
                                                            {
                                                                Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Waffen(true).ToString());
                                                                return true;
                                                            }
                                                            else

                                                                // "TEXTUREN_DEBUG_WAFFEN"
                                                                if (Text == Befehle[13])
                                                                {
                                                                    Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Waffen(false).ToString());
                                                                    return true;
                                                                }

                                                                else

                                                                    // "TEXTUREN_RELEASE_SONSTIGE"
                                                                    if (Text == Befehle[14])
                                                                    {
                                                                        Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Sonstige(true).ToString());
                                                                        return true;
                                                                    }
                                                                    else

                                                                        // "TEXTUREN_DEBUG_SONSTIGE"
                                                                        if (Text == Befehle[15])
                                                                        {
                                                                            Game1.Meldungen.addMessage("Bilder: " + Optimierung.Optimiere_Sonstige(false).ToString());
                                                                            return true;
                                                                        }
                                                                        else

                                                                            // "SCHNEE"
                                                                            if (Text == Befehle[16])
                                                                            {
                                                                                for (int i = 0; i < 100; i++)
                                                                                {
                                                                                    // for (int b = 0; b < 100; b++)
                                                                                    // {
                                                                                    Vector2 Pos = Game1.Spiel2.Fenster + new Vector2(Help.rnd.Next(0, Game1.screenWidth), -Help.rnd.Next(0, Game1.screenHeight)); //- Help.rnd.Next(0,1000
                                                                                    Game1.Spiel2.AddRakete(0, Pos, Vector2.Zero, 300 * 4, Waffendaten.mg, 0);
                                                                                    // }
                                                                                }
                                                                                return true;
                                                                            }
                                                                            else

                                                                                // "FEHLER"
                                                                                if (Text == Befehle[17])
                                                                                {
                                                                                    Mine temp = null;
                                                                                    int a = temp.ID;
                                                                                    return true;
                                                                                }

            return false;
        }
    }
}