// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="Haeuser.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     Class Haus
    /// </summary>
    public class Haus
    {
        #region Fields

        /// <summary>
        ///     The HAEUSER
        /// </summary>
        public static bool HAEUSER;

        /// <summary>
        ///     The HAEUSE r_ KOLLISION
        /// </summary>
        public static bool HAEUSER_KOLLISION;

        /// <summary>
        ///     The HAEUSE r_ LEBENSLINIE
        /// </summary>
        public static bool HAEUSER_LEBENSLINIE;

        /// <summary>
        ///     The HAEUSE r_ ZERSTOERUNG
        /// </summary>
        public static bool HAEUSER_ZERSTOERUNG;

        public static Inventar Rucksack = new Inventar(999, null, 0);

        /// <summary>
        ///     The besitzer
        /// </summary>
        public List<int> Besitzer = new List<int>();

        public List<int> BesitzerEroberer = new List<int>();
        public List<int> BesitzerPunkte = new List<int>();

        /// <summary>
        ///     The bild
        /// </summary>
        public List<Texture2D> Bild = new List<Texture2D>();

        /// <summary>
        ///     The haus typ
        /// </summary>
        public List<int> HausTyp = new List<int>();

        public List<KoerperObjekt> Koerper = new List<KoerperObjekt>();

        /// <summary>
        ///     The collision
        /// </summary>
        public List<KollisionsObjekt> Kollision = new List<KollisionsObjekt>();

        /// <summary>
        ///     The collision2
        /// </summary>
        public List<KollisionsObjekt> Kollision2 = new List<KollisionsObjekt>();

        /// <summary>
        ///     The hp
        /// </summary>
        public List<int> Lebenspunkte = new List<int>();

        /// <summary>
        ///     The maxhp
        /// </summary>
        public List<int> MaximaleLebenspunkte = new List<int>();

        /// <summary>
        ///     The haeuser
        /// </summary>
        public List<Vector2> Position = new List<Vector2>();

        public List<int> Produktion = new List<int>();

        /// <summary>
        ///     The destruction
        /// </summary>
        public List<ZerstörungsObjekt> Zerstörung = new List<ZerstörungsObjekt>();

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Generate_staedte_doerfers the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public static void generate_staedte_doerfer(List<UInt16>[] array, bool symmetrisch)
        {
            Doerfer.Clear();
            DoerferPos.Clear();
            Staedte.Clear();
            StaedtePos.Clear();
            var rnd = new Random();
            int Abstand = 1150; // 1000;
            int HintenVorne = 150; // 150;
            int AbstandGebäude = 0;

            int breite = (symmetrisch ? array.Length/2 : array.Length);
            bool dorf = true;
            for (int i = Abstand/2; i < breite - Abstand/2; i++)
            {
                if (dorf)
                {
                    var temp = new int[Gebäudedaten.DORF.Wert.Count() + 2];
                    var temp2 = new int[Gebäudedaten.DORF.Wert.Count()];
                    temp[temp.Count() - 2] = i;

                    i += HintenVorne;
                    for (int b = 0; b < Gebäudedaten.DORF.Wert.Count(); b++)
                    {
                        if (Gebäudedaten.DORF.Wert[b] == -1)
                        {
                            temp[b] = Gebäudedaten.HAEUSERDORF.Wert[rnd.Next(0, Gebäudedaten.HAEUSERDORF.Wert.Count())];
                        }
                        else
                            temp[b] = Gebäudedaten.DORF.Wert[b];
                        temp2[b] = i;
                        i += (int) (Texturen.haus[temp[b]].Width*Gebäudedaten.SKALIERUNG.Wert[temp[b]]);
                        i += AbstandGebäude;
                    }
                    i -= AbstandGebäude;
                    i += HintenVorne;
                    temp[temp.Count() - 1] = i - temp[temp.Count() - 2];
                    if (i < breite - Abstand/2)
                    {
                        Doerfer.Add(temp);
                        DoerferPos.Add(temp2);
                    }
                    dorf = false;
                }
                else
                {
                    var temp = new int[Gebäudedaten.STADT.Wert.Count() + 2];
                    var temp2 = new int[Gebäudedaten.STADT.Wert.Count()];

                    temp[temp.Count() - 2] = i;

                    i += HintenVorne;
                    for (int b = 0; b < Gebäudedaten.STADT.Wert.Count(); b++)
                    {
                        if (Gebäudedaten.STADT.Wert[b] == -1)
                        {
                            temp[b] =
                                Gebäudedaten.HAEUSERSTADT.Wert[rnd.Next(0, Gebäudedaten.HAEUSERSTADT.Wert.Count())];
                        }
                        else
                            temp[b] = Gebäudedaten.STADT.Wert[b];
                        temp2[b] = i;
                        i += (int) (Texturen.haus[temp[b]].Width*Gebäudedaten.SKALIERUNG.Wert[temp[b]]);
                        i += AbstandGebäude;
                    }
                    i -= AbstandGebäude;
                    i += HintenVorne;
                    temp[temp.Count() - 1] = i - temp[temp.Count() - 2];
                    if (i < breite - Abstand/2)
                    {
                        Staedte.Add(temp);
                        StaedtePos.Add(temp2);
                    }
                    dorf = true;
                }

                i += Abstand;
            }
        }

        public static void InventarAuffüllen()
        {
            Rucksack.Treibstoff = 9999;
            Rucksack.Munition[0] = 10;
            Rucksack.Munition[1] = 10;
            Rucksack.Munition[2] = 10;
            Rucksack.Munition[3] = 10;
            Rucksack.Munition[4] = 5;
            Rucksack.Munition[5] = 5;
            Rucksack.Munition[6] = 100;
            Rucksack.Munition[7] = 100;
            Rucksack.Munition[8] = 0;
            Rucksack.Munition[9] = 0;
            Rucksack.Munition[10] = 25;
            Rucksack.Munition[11] = 25;
            Rucksack.Munition[12] = 25;
            Rucksack.Munition[13] = 25;
            Rucksack.Munition[14] = 0;
            Rucksack.Munition[15] = 0;
            Rucksack.Munition[16] = 0;
            Rucksack.Munition[17] = 0;
            Rucksack.Munition[18] = 5000;
        }

        #endregion Methods

        #region Orte

        /// <summary>
        ///     The doerfer
        /// </summary>
        public static List<int[]> Doerfer = new List<int[]>();

        /// <summary>
        ///     The doerfer pos
        /// </summary>
        public static List<int[]> DoerferPos = new List<int[]>();

        /// <summary>
        ///     The orte
        /// </summary>
        public static List<Vector2> Orte = new List<Vector2>();

        /// <summary>
        ///     The ortemaxheight
        /// </summary>
        public static List<int> Ortemaxheight = new List<int>();

        /// <summary>
        ///     The ortsname
        /// </summary>
        public static List<String> Ortsname = new List<String>();

        /// <summary>
        ///     The staedte
        /// </summary>
        public static List<int[]> Staedte = new List<int[]>();

        /// <summary>
        ///     The staedte pos
        /// </summary>
        public static List<int[]> StaedtePos = new List<int[]>();

        #endregion Orte

        /// <summary>
        ///     Zeichnes the häuser.
        /// </summary>
        /// <param name="found">if set to <c>true</c> [found].</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool ZeichneHäuser(bool found, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice,
            Spiel Spiel2) // Zeichnet die Haeuser
        {
            if (Spiel2 == null) return false;

            for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
            {
                int id = Spiel2.Haeuser.HausTyp[i];
                if (Spiel2.Haeuser.Position[i].X == -1) continue;
                if (Spiel2.Haeuser.Position[i].X + Texturen.haus[id].Width*Gebäudedaten.SKALIERUNG.Wert[id] <
                    Spiel2.Fenster.X || Spiel2.Fenster.X + Game1.screenWidth + 10 < Spiel2.Haeuser.Position[i].X)
                    continue;
                var xPos = (int) (Spiel2.Haeuser.Position[i].X - (int) Spiel2.Fenster.X);
                var yPos = (int) (Spiel2.Haeuser.Position[i].Y - Spiel2.Fenster.Y); // - (baum[i].Width / 2)

                spriteBatch.Begin();
                // Lebenslinie Zeichnen
                /*  if (Haus.HAEUSER_LEBENSLINIE)
                  {
                      if (Spiel2.Haeuser.Lebenspunkte[i] > 0)
                      {
                          int Hausbreite = (int)(Spiel2.Haeuser.Bild[i].Width * Gebäudedaten.SKALIERUNG.Wert[id]);

                          Rectangle leben = new Rectangle(0, 0, (int)(Hausbreite * 1), Texturen.leben.Height / 4);
                          spriteBatch.Draw(Texturen.leben, new Vector2(xPos, Spiel2.Haeuser.Position[i].Y - Texturen.haus[id].Height - 10), leben,
                                 Color.DarkRed * 0.5f);

                          leben = new Rectangle(0, 0, (int)(Hausbreite * ((float)Spiel2.Haeuser.Lebenspunkte[i] / Spiel2.Haeuser.MaximaleLebenspunkte[i])), Texturen.leben.Height / 4);
                          spriteBatch.Draw(Texturen.leben, new Vector2(xPos, Spiel2.Haeuser.Position[i].Y - Texturen.haus[id].Height - 10), leben,
                                 Color.Lime * 0.5f);
                      }
                  }*/

                // Fahnen malen
                if (Mod.HAEUSER_FAHNE_VISIBLE.Wert)
                {
                    float scale2 = Optimierung.Skalierung(0.15f);
                    if (Spiel2.Haeuser.Besitzer[i] != -1 &&
                        Spiel2.Haeuser.BesitzerPunkte[i] >= Allgemein.MinBesitzerPunkte)
                        spriteBatch.Draw(Texturen.fahne,
                            new Vector2(
                                xPos + Spiel2.Haeuser.Bild[i].Width/2*Gebäudedaten.SKALIERUNG.Wert[id] +
                                Gebäudedaten.POSITIONX.Wert[Spiel2.Haeuser.HausTyp[i]],
                                yPos - Texturen.fahne.Height*scale2 +
                                Gebäudedaten.POSITIONY.Wert[Spiel2.Haeuser.HausTyp[i]] -
                                Spiel2.Haeuser.Bild[i].Height*Gebäudedaten.SKALIERUNG.Wert[id]), null,
                            Spiel2.players[Spiel2.Haeuser.Besitzer[i]].Farbe, 0, new Vector2(0, 0), scale2,
                            SpriteEffects.None, 1); // haus.Width / 2
                }
                spriteBatch.End();

                spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                // Häuser malen
                spriteBatch.Draw(Spiel2.Haeuser.Bild[i], new Vector2(xPos, yPos), null, Color.White, 0,
                    new Vector2(0, Texturen.haus[id].Height), Gebäudedaten.SKALIERUNG.Wert[id], SpriteEffects.None, 1);

                spriteBatch.End();

                bool treffer = false;
                if (!Editor.visible)
                    if (Tausch.SpielAktiv)
                        if (Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
                            if (Spiel2.Haeuser.IsCollision2(i,
                                Spiel2.players[Spiel2.CurrentPlayer].pos[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + new Vector2(0, -2)))
                            {
                                treffer = true;
                            }

                spriteBatch.Begin();
                if (Game1.DEBUG_AKTIV.Wert)
                {
                    // Zeichne Schwerpunkt
                    Vector2 Schwerpunkt = Spiel2.Haeuser.Koerper[i].objektSchwerpunkt.ObjektSchwerpunkt;
                    Help.DrawRectangle(spriteBatch, graphicsDevice,
                        new Rectangle((int) (xPos + Schwerpunkt.X - 2),
                            (int) (yPos - Texturen.haus[id].Height*Gebäudedaten.SKALIERUNG.Wert[id] + Schwerpunkt.Y - 2),
                            4, 4),
                        Color.Red, 1);
                }

                // erobern malen
                if (Spiel2.Haeuser.BesitzerPunkte[i] < Allgemein.MaxBesitzerPunkte)
                {
                    //spriteBatch.Draw(Texturen.fahne, new Vector2(xPos + Spiel2.Haeuser.Bild[i].Width / 2 * Gebäudedaten.SKALIERUNG[id] + Gebäudedaten.POSITION[Spiel2.Haeuser.HausTyp[i]].X, yPos - Texturen.fahne.Height * scale2 + Gebäudedaten.POSITION[Spiel2.Haeuser.HausTyp[i]].Y - Spiel2.Haeuser.Bild[i].Height * Gebäudedaten.SKALIERUNG[id]), null, Spiel2.players[Spiel2.Haeuser.Besitzer[i]].Farbe, 0, new Vector2(0, 0), scale2, SpriteEffects.None, 1); // haus.Width / 2
                    Color farb = Color.SandyBrown;
                    if (Spiel2.Haeuser.BesitzerEroberer[i] != -1)
                    {
                        farb = Spiel2.players[Spiel2.Haeuser.BesitzerEroberer[i]].Farbe;
                    }

                    var y = (int) ((Spiel2.Haeuser.BesitzerPunkte[i]/1000.0f)*Texturen.haus[id].Height);
                    spriteBatch.Draw(Spiel2.Haeuser.Bild[i], new Vector2(xPos, yPos),
                        new Rectangle(0, y, Texturen.haus[id].Width, Texturen.haus[id].Height - y), farb, 0,
                        new Vector2(0, Texturen.haus[id].Height - y), Gebäudedaten.SKALIERUNG.Wert[id],
                        SpriteEffects.None, 1);
                }

                if (Tausch.SpielAktiv)
                {
                    if (treffer)
                    {
                        spriteBatch.Draw(Texturen.hausumriss[id], new Vector2(xPos, yPos), null, Color.Lime, 0,
                            new Vector2(0, Texturen.haus[id].Height), Gebäudedaten.SKALIERUNG.Wert[id],
                            SpriteEffects.None, 1);
                    }

                    if (!found)
                    {
                        bool treffer3 = false;
                        MouseState mouseState = Help.GetMouseState();
                        if (Game1.MausAktiv && Spiel2.Haeuser.HausTyp[i] == Gebäudedaten.FABRIK &&
                            Spiel2.Haeuser.Besitzer[i] == Spiel2.CurrentPlayer &&
                            Spiel2.Haeuser.BesitzerPunkte[i] >= Allgemein.MinBesitzerPunkte &&
                            Spiel2.Haeuser.Lebenspunkte[i] > 0)
                            if (Spiel2.Haeuser.IsCollision2(i,
                                new Vector2(mouseState.X + Spiel2.Fenster.X, mouseState.Y + Spiel2.Fenster.Y) +
                                new Vector2(0, 0)))
                            {
                                treffer3 = true;
                                found = true;
                            }

                        if (treffer3)
                        {
                            spriteBatch.Draw(Texturen.hausumriss[id], new Vector2(xPos, yPos), null, Color.OrangeRed, 0,
                                new Vector2(0, Texturen.haus[id].Height), Gebäudedaten.SKALIERUNG.Wert[id],
                                SpriteEffects.None, 1);
                        }
                    }
                }

                // Bauen malen
                if (Spiel2.Haeuser.Besitzer[i] > -1)
                    if (Spiel2.Haeuser.Produktion[i] > -1)
                        if (Spiel2.CurrentPlayer == Spiel2.Haeuser.Besitzer[i])
                        {
                            float specialscale = 0.75f;
                            //
                            Vector2 pos = new Vector2(xPos, yPos) +
                                          new Vector2(
                                              +(Spiel2.Haeuser.Bild[i].Width*Gebäudedaten.SKALIERUNG.Wert[id]/2) -
                                              (Texturen.panzerbutton[Spiel2.Haeuser.Produktion[i]].Width*0.75f*
                                               Optimierung.Skalierung(0.25f))/2,
                                              -Texturen.haus[id].Height*Gebäudedaten.SKALIERUNG.Wert[id]/2);

                            Help.DrawRectangle(spriteBatch, graphicsDevice,
                                new Rectangle((int) pos.X - 1, (int) pos.Y - 1,
                                    (int)
                                        (Texturen.panzerbutton[Spiel2.Haeuser.Produktion[i]].Width*specialscale*
                                         Optimierung.Skalierung(0.25f) + 2),
                                    (int)
                                        (Texturen.panzerbutton[Spiel2.Haeuser.Produktion[i]].Height*specialscale*
                                         Optimierung.Skalierung(0.25f) + 2)),
                                Spiel2.players[Spiel2.Haeuser.Besitzer[i]].Farbe, 0.3f);

                            spriteBatch.Draw(Texturen.panzerbutton[Spiel2.Haeuser.Produktion[i]], pos, null, Color.White,
                                0, Vector2.Zero, Optimierung.Skalierung(0.25f)*specialscale, SpriteEffects.None, 1);
                        }

                spriteBatch.End();
                spriteBatch.Begin();

                if (Editor.visible && Editor.mouseover == 0 && Editor.mouseoverid == i)
                {
                    // Häuser malen
                    spriteBatch.Draw(Texturen.haus[Spiel2.Haeuser.HausTyp[i]], new Vector2(xPos, yPos), null,
                        Color.Blue*255, 0, new Vector2(0, Texturen.haus[id].Height), Gebäudedaten.SKALIERUNG.Wert[id],
                        SpriteEffects.None, 0);
                }

                // ID zeichnen
                if (Game1.DEBUG_AKTIV.Wert)
                    spriteBatch.DrawString(Texturen.font2, Spiel2.Haeuser.HausTyp[i].ToString(),
                        new Vector2(xPos, Spiel2.Haeuser.Position[i].Y - Texturen.haus[id].Height), Color.Yellow);
                spriteBatch.End();
            }
            return found;
        }

        /// <summary>
        ///     Adds the specified position.
        /// </summary>
        /// <param name="_Position">The _ position.</param>
        /// <param name="_hp">The _HP.</param>
        /// <param name="Typ">The typ.</param>
        /// <param name="_Besitzer">The _ besitzer.</param>
        public void Add(Vector2 _Position, int _hp, int Typ, int _Besitzer) // fügt ein Haus ein
        {
            Position.Add(_Position);
            HausTyp.Add(Typ);
            Bild.Add(null);

            // TODO das rausmachen
            // _Besitzer = 0;

            Besitzer.Add(_Besitzer);
            Produktion.Add(-1);
            BesitzerPunkte.Add(Allgemein.MaxBesitzerPunkte);
            BesitzerEroberer.Add(-1);
            //Lebenslinien.Add(new Rectangle(0, 0, 100, Texturen.leben.Height));
            // UpdateHausSchaden(Lebenslinien.Count - 1, 0);
            var temp = new Color[Texturen.haus[Typ].Width*Texturen.haus[Typ].Height];
            Texturen.haus[Typ].GetData(temp);
            Bild[Besitzer.Count - 1] = new Texture2D(Game1.device, Texturen.haus[Typ].Width, Texturen.haus[Typ].Height);
            Bild[Besitzer.Count - 1].SetData(temp);

            //MaxPixel.Add(Help.GetPixelAnzahl(Bild[Besitzer.Count - 1]));
            MaximaleLebenspunkte.Add((int) (Help.GetPixelAnzahl(Bild[Besitzer.Count - 1])*0.6f));
            if (_hp != -9999)
            {
                Lebenspunkte.Add(_hp);
            }
            else
                Lebenspunkte.Add(MaximaleLebenspunkte[Besitzer.Count - 1]);

            if (HAEUSER_KOLLISION)
            {
                Kollision.Add(null);
                Kollision2.Add(null);
                Kollision2[Besitzer.Count - 1] = new KollisionsObjekt(Bild[Besitzer.Count - 1],
                    Bild[Besitzer.Count - 1].Width, Bild[Besitzer.Count - 1].Height,
                    Gebäudedaten.SKALIERUNG.Wert[HausTyp[Besitzer.Count - 1]], false, false, false, new Vector2(0, 0));
                load(Besitzer.Count - 1);

                Koerper.Add(new KoerperObjekt());
                Koerper[Besitzer.Count - 1].objektSchwerpunkt.BerechneSchwerpunkt(Kollision[Besitzer.Count - 1].Bild);
            }

            if (HAEUSER_ZERSTOERUNG)
            {
                Zerstörung.Add(null);
                load2(Besitzer.Count - 1);
            }
        }

        public List<String> EditorSpeichern(int id)
        {
            var data = new List<String>();
            data.Add("[HAUS]");
            data.Add("Position=" + Position[id]);
            data.Add("HausTyp=" + HausTyp[id]);
            data.Add("Besitzer=" + Besitzer[id]);
            data.Add("Produktion=" + Produktion[id]);
            data.Add("BesitzerPunkte=" + BesitzerPunkte[id]);
            data.Add("BesitzerEroberer=" + BesitzerEroberer[id]);
            data.Add("[/HAUS]");
            return data;
        }

        public bool Erobern(int HausID, int wert, int ErobererID)
        {
            if (Besitzer[HausID] == ErobererID && BesitzerPunkte[HausID] == Allgemein.MaxBesitzerPunkte) return false;
            if (Lebenspunkte[HausID] <= 0) return false;

            // if (BesitzerEroberer[HausID] != ErobererID)
            //     BesitzerPunkte[HausID] = Allgemein.MaxBesitzerPunkte;

            if (BesitzerPunkte[HausID] < Allgemein.MinBesitzerPunkte)
                Besitzer[HausID] = -1;

            if (BesitzerEroberer[HausID] == -1) BesitzerEroberer[HausID] = ErobererID;

            if (BesitzerEroberer[HausID] == ErobererID)
            {
                BesitzerPunkte[HausID] -= wert;
            }
            else if (BesitzerEroberer[HausID] != ErobererID)
            {
                BesitzerPunkte[HausID] += wert;

                if (BesitzerPunkte[HausID] >= Allgemein.MaxBesitzerPunkte)
                {
                    BesitzerPunkte[HausID] = Allgemein.MaxBesitzerPunkte;
                    BesitzerEroberer[HausID] = ErobererID;
                }
            }

            if (BesitzerPunkte[HausID] <= 0)
            {
                Sounds.erobert.PlaySoundAny();
                Besitzer[HausID] = ErobererID;
                BesitzerPunkte[HausID] = Allgemein.MaxBesitzerPunkte;
                BesitzerEroberer[HausID] = -1;
            }

            return true;
        }

        /// <summary>
        ///     Determines whether the specified i is collision.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <returns><c>true</c> if the specified i is collision; otherwise, <c>false</c>.</returns>
        public bool IsCollision(int i, Vector2 Incoming_Position)
        {
            if (Kollision[i] == null) return false;
            return Kollision[i].collision(Incoming_Position, Position[i]);
        }

        /// <summary>
        ///     Determines whether the specified i is collision2.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <returns><c>true</c> if the specified i is collision2; otherwise, <c>false</c>.</returns>
        public bool IsCollision2(int i, Vector2 Incoming_Position)
        {
            if (Kollision2[i] == null) return false;
            return Kollision2[i].collision(Incoming_Position, Position[i]);
        }

        /// <summary>
        ///     Determines whether the specified i is explode.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        public int IsExplode(int i, Vector2 Explosion, int Energie)
        {
            if (Zerstörung[i] == null) return 0;

            var Bereiche = new List<Vector3>();
            int result = Zerstörung[i].BerechneZerstörung(Bild[i], Explosion, Energie, Position[i], Bereiche);
            if (result == 0) return 0;
            Koerper[i].objektSchwerpunkt.VerringereSchwerpunktmasse(Bereiche);
            return result;
        }

        public void Laden(List<String> Text, int id)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "HAUS");
            if (Text2.Count == 0) return;

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            int Typ = TextLaden.LadeInt(Liste, "HausTyp", id == -1 ? 0 : HausTyp[id]);

            int altid = id;
            if (id == -1)
            {
                Add(Vector2.Zero, 0, Typ, -9999);
                id = HausTyp.Count - 1;
            }

            if (HausTyp[id] == Typ && altid == id)
            {
                HausTyp[id] = Typ;
                Bild[id] = Texturen.haus[Typ];
                Lebenspunkte[id] = TextLaden.LadeInt(Liste, "Lebenspunkte", Lebenspunkte[id]);
                MaximaleLebenspunkte[id] = TextLaden.LadeInt(Liste, "MaximaleLebenspunkte", MaximaleLebenspunkte[id]);
                Besitzer[id] = TextLaden.LadeInt(Liste, "Besitzer", Besitzer[id]);
                Produktion[id] = TextLaden.LadeInt(Liste, "Produktion", Produktion[id]);
                BesitzerPunkte[id] = TextLaden.LadeInt(Liste, "BesitzerPunkte", BesitzerPunkte[id]);
                BesitzerEroberer[id] = TextLaden.LadeInt(Liste, "BesitzerEroberer", BesitzerEroberer[id]);
                Position[id] = TextLaden.LadeVector2(Liste, "Position", Position[id]);
            }
            else
            {
                HausTyp[id] = Typ;
                Bild[id] = Texturen.haus[Typ];
                Besitzer[id] = TextLaden.LadeInt(Liste, "Besitzer", Besitzer[id]);
                Produktion[id] = TextLaden.LadeInt(Liste, "Produktion", Produktion[id]);
                BesitzerPunkte[id] = TextLaden.LadeInt(Liste, "BesitzerPunkte", BesitzerPunkte[id]);
                BesitzerEroberer[id] = TextLaden.LadeInt(Liste, "BesitzerEroberer", BesitzerEroberer[id]);
                Position[id] = TextLaden.LadeVector2(Liste, "Position", Position[id]);
                MaximaleLebenspunkte[id] = ((int) (Help.GetPixelAnzahl(Bild[id])*0.6f));

                Lebenspunkte[id] = MaximaleLebenspunkte[id];
            }

            Kollision[id] = new KollisionsObjekt(Bild[id], Bild[id].Width, Bild[id].Height,
                Gebäudedaten.SKALIERUNG.Wert[HausTyp[id]], false, false, false, new Vector2(0, 0));
            Kollision2[id] = new KollisionsObjekt(Bild[id], Bild[id].Width, Bild[id].Height,
                Gebäudedaten.SKALIERUNG.Wert[HausTyp[id]], false, false, false, new Vector2(0, 0));
            Zerstörung[id] = new ZerstörungsObjekt(Bild[id].Width, Bild[id].Height,
                Gebäudedaten.SKALIERUNG.Wert[HausTyp[id]], false, false, false);

            Kollision[id] = KollisionsObjekt.Laden(Text2, altid == -1 ? null : Kollision[id]);
            Zerstörung[id] = ZerstörungsObjekt.Laden(Text2, altid == -1 ? null : Zerstörung[id]);

            Bild[id] = Kollision[id].UseMaskOnTexture2D(Bild[id]);
        }

        /// <summary>
        ///     Loads the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
        public void load(int i)
        {
            Kollision[i] = new KollisionsObjekt(Bild[i], Bild[i].Width, Bild[i].Height,
                Gebäudedaten.SKALIERUNG.Wert[HausTyp[i]], false, false, false, new Vector2(0, 0));
        }

        /// <summary>
        ///     Load2s the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
        public void load2(int i)
        {
            Zerstörung[i] = new ZerstörungsObjekt(Bild[i].Width, Bild[i].Height,
                Gebäudedaten.SKALIERUNG.Wert[HausTyp[i]], false, false, false);
        }

        /// <summary>
        ///     Set_s the haeuser.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public void set_Haeuser(List<UInt16>[] Spielfeld, bool symmetrisch) // setzt zufällig Häuser auf der Karte
        {
            int x = 0;
            for (; x < (symmetrisch ? Spielfeld.Length/2 : Spielfeld.Length); x++)
            {
                int setnext = Help.rnd.Next(0, Texturen.haus.Count());
                // setnext = 17;
                //  setnext = 5;
                int find = 0;
                int begin = Kartenformat.BottomOf(x, 0);
                for (int b = x + 1;
                    b < (symmetrisch ? Spielfeld.Length/2 : Spielfeld.Length) &&
                    find < Texturen.haus[setnext].Width*Gebäudedaten.SKALIERUNG.Wert[setnext];
                    b++)
                {
                    if (begin == Kartenformat.BottomOf(b, 0)) // Spielfeld[b][0]
                    {
                        find++;
                    }
                    else
                        break;
                }

                if (find >= Texturen.haus[setnext].Width*Gebäudedaten.SKALIERUNG.Wert[setnext])
                {
                    Add(new Vector2(x, Kartenformat.BottomOf(x, 0)), -9999, setnext, Spiel.rand.Next(-1, -1));
                    x += (int) (Texturen.haus[setnext].Width*Gebäudedaten.SKALIERUNG.Wert[setnext] + 130);
                }
            }

            if (symmetrisch)
            {
                int anz = Position.Count;
                int middle = Spielfeld.Length/2;
                for (int i = 0; i < anz; i++)
                {
                    Add(
                        new Vector2(
                            middle + (middle - Position[i].X) - Bild[i].Width*Gebäudedaten.SKALIERUNG.Wert[HausTyp[i]],
                            Position[i].Y), Lebenspunkte[i], HausTyp[i], Besitzer[i]);
                }
            }
        }

        /// <summary>
        ///     Set_s the haeuser_staedte_doerfer.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public void set_Haeuser_staedte_doerfer(List<UInt16>[] Spielfeld, bool symmetrisch)
            // setzt zufällig Häuser auf der Karte
        {
            Orte.Clear();
            Ortemaxheight.Clear();
            Ortsname.Clear();

            for (int i = 0; i < Staedte.Count; i++)
            {
                float min = 9999;
                for (int b = 0; b < Staedte[i].Count() - 2; b++)
                {
                    int setnext = Staedte[i][b];

                    int x = StaedtePos[i][b];
                    Add(new Vector2(x, Kartenformat.BottomOf(x, 0)), -9999, setnext, -1);

                    float y = Kartenformat.BottomOf(x, 0) -
                              Texturen.haus[setnext].Height*Gebäudedaten.SKALIERUNG.Wert[setnext];
                    if (y < min) min = y;
                }

                int ma = Staedte[i].Count();
                Orte.Add(new Vector2(Staedte[i][ma - 2], Staedte[i][ma - 1]));

                if (min < 25 + 25 || min > Game1.screenHeight) min = 25 + 25;
                Ortemaxheight.Add((int) min);
            }

            for (int i = 0; i < Doerfer.Count; i++)
            {
                float min = 9999;
                for (int b = 0; b < Doerfer[i].Count() - 2; b++)
                {
                    int setnext = Doerfer[i][b];

                    int x = DoerferPos[i][b];
                    Add(new Vector2(x, Kartenformat.BottomOf(x, 0)), -9999, setnext, -1);

                    float y = Kartenformat.BottomOf(x, 0) -
                              Texturen.haus[setnext].Height*Gebäudedaten.SKALIERUNG.Wert[setnext];
                    if (y < min) min = y;
                }

                int ma = Doerfer[i].Count();
                Orte.Add(new Vector2(Doerfer[i][ma - 2], Doerfer[i][ma - 1]));

                if (min < 25 + 25 || min > Game1.screenHeight) min = 25 + 25;
                Ortemaxheight.Add((int) min);
            }

            if (symmetrisch)
            {
                int anz = Position.Count;
                int middle = Spielfeld.Length/2;
                for (int i = 0; i < anz; i++)
                {
                    Add(
                        new Vector2(
                            middle + (middle - Position[i].X) - Bild[i].Width*Gebäudedaten.SKALIERUNG.Wert[HausTyp[i]],
                            Position[i].Y), Lebenspunkte[i], HausTyp[i], Besitzer[i]);
                }

                anz = Orte.Count;
                for (int i = 0; i < anz; i++)
                {
                    Orte.Add(new Vector2(middle + (middle - Orte[i].X) - Orte[i].Y, Orte[i].Y));
                    Ortemaxheight.Add(Ortemaxheight[i]);
                }
            }

            var file = new StreamReader("Content\\Konfiguration\\Orte.txt");
            var temp = new List<String>();
            while (!file.EndOfStream)
            {
                temp.Add(file.ReadLine());
            }

            for (int i = 0; i < Orte.Count; i++)
            {
                int id = Spiel.rand.Next(0, temp.Count());
                Ortsname.Add(temp[id]);
                temp.RemoveAt(id);
            }
        }

        public List<String> Speichern()
        {
            var data = new List<String>();

            for (int i = 0; i < Bild.Count; i++)
            {
                data.Add("[HAUS]");
                data.Add("HausTyp=" + HausTyp[i]);
                data.Add("Lebenspunkte=" + Lebenspunkte[i]);
                data.Add("MaximaleLebenspunkte=" + MaximaleLebenspunkte[i]);
                data.Add("Position=" + Position[i]);
                data.Add("Besitzer=" + Besitzer[i]);
                data.Add("Produktion=" + Produktion[i]);
                data.Add("BesitzerPunkte=" + BesitzerPunkte[i]);
                data.Add("BesitzerEroberer=" + BesitzerEroberer[i]);
                data.AddRange(Kollision[i].Speichern());
                data.AddRange(Zerstörung[i].Speichern());
                data.Add("[/HAUS]");
            }

            return data;
        }

        //gibt zuruck, ob das haus noch besteht
        /// <summary>
        ///     Updates the haus schaden.
        /// </summary>
        /// <param name="Hausnummer">The hausnummer.</param>
        /// <param name="recievedDamage">The recieved damage.</param>
        public void UpdateHausSchaden(int Hausnummer, int recievedDamage)
        {
            if (!HAEUSER_LEBENSLINIE) return;
            Lebenspunkte[Hausnummer] -= recievedDamage;
            if (Lebenspunkte[Hausnummer] <= 0)
            {
                Besitzer[Hausnummer] = -1;
            }
        }

        #region Privat

        /// <summary>
        ///     The HAEUSER
        /// </summary>
        private static Var<bool> MOD_HAEUSER = new Var<bool>("HAEUSER", false, ref HAEUSER);

        /// <summary>
        ///     The HAEUSE r_ KOLLISION
        /// </summary>
        private static Var<bool> MOD_HAEUSER_KOLLISION = new Var<bool>("HAEUSER_KOLLISION", false, ref HAEUSER_KOLLISION);

        /// <summary>
        ///     The HAEUSE r_ LEBENSLINIE
        /// </summary>
        private static Var<bool> MOD_HAEUSER_LEBENSLINIE = new Var<bool>("HAEUSER_LEBENSLINIE", false,
            ref HAEUSER_LEBENSLINIE);

        /// <summary>
        ///     The HAEUSE r_ ZERSTOERUNG
        /// </summary>
        private static Var<bool> MOD_HAEUSER_ZERSTOERUNG = new Var<bool>("HAEUSER_ZERSTOERUNG", false,
            ref HAEUSER_ZERSTOERUNG);

        #endregion Privat
    }
}