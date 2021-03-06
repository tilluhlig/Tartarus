﻿// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-22-2013
// ***********************************************************************
// <copyright file="Vordergrund.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     Klasse zur Erstellung, Zeichnung und Aktualisierung des Vordergrundes
    /// </summary>
    public static class Vordergrund
    {
        private static Semaphore _VordergrundSemaphore = new Semaphore(1, 1);

        #region Methods

        /// <summary>
        ///     Aktualisiert das Bildmaterial für den Vordergrund (schneller)
        /// </summary>
        /// <param name="Vierecke">
        ///     Ist eine Liste von Vierecken, welche zu aktualisierende
        ///     Bereiche angeben.
        ///     x = X-Position, z = Breite, y = Y-Position, w = Höhe
        /// </param>
        public static void AktualisiereVordergrund(List<Vector4> Vierecke)
        {
            // Übergabe:  x , y , Breite, Höhe
            Spiel Spiel2 = Game1.Spiel2;
            int screenWidth = Game1.screenWidth;
            int screenHeight = Game1.screenHeight;
            Rectangle screen = Game1.screen;
            SpriteBatch spriteBatch = Game1.spriteBatch;

            if (Vierecke.Count == 0) return;
            Color[,] water = Game1.Farbwahl(Texturen.wasser);

            for (int c = 0; c < Spiel2.foreground.Length; c++)
            {
                if (Vierecke[0].X + Vierecke[0].Z < c*2048 || Vierecke[0].X > c*2048 + 2048) continue;
                int Bildbreite = 2048;

                for (int x = 2048*c; x < Spiel2.Spielfeld.Length && x < 2048*(c + 1); x++)
                {
                    if (x < Vierecke[0].X || x > Vierecke[0].X + Vierecke[0].Z) continue;
                    int h = (x - c*2048);

                    int sum = 0;
                    for (int y = 0;
                        y < Spiel2.Spielfeld[x].Count;
                        sum += Kartenformat.Laenge(Spiel2.Spielfeld[x][y]), y++)
                    {
                        int sorte = Kartenformat.Material(Spiel2.Spielfeld[x][y]);
                        int anzahl = Kartenformat.Laenge(Spiel2.Spielfeld[x][y]);
                        for (int d = 0; d < anzahl; d++)
                        {
                            if (sum + d < Vierecke[0].Y || sum + d > Vierecke[0].Y + Vierecke[0].W)
                                continue; // fehlerhaft?

                            Color t;
                            if (Karte.Material[sorte].Farbe)
                            {
                                t = Karte.Material[sorte].CFarbe;
                            }
                            else
                                t =
                                    Karte.Material[sorte].CBild[
                                        (x%Karte.Material[sorte].Bild.Width) +
                                        ((sum + d)%Karte.Material[sorte].Bild.Height)*Karte.Material[sorte].Bild.Width];
                            if (!Kartenformat.isSet(x, sum + d) && sum + d > screenHeight - 20)
                            {
                                t = water[x%Texturen.wasser.Width, (sum + d)%Texturen.wasser.Height];
                            }
                            else if ((!Kartenformat.isSet(x, sum + d - 5) || !Kartenformat.isSet(x + 5, sum + d) ||
                                      !Kartenformat.isSet(x, sum + d + 5) || !Kartenformat.isSet(x - 5, sum + d)) &&
                                     Karte.Material[sorte].Abdunkeln)
                            {
                                t.R /= (int) 2f;
                                t.G /= (int) 2f;
                                t.B /= (int) 2f;
                            }
                            Spiel2.foregroundColors[c][h + (sum + d)*Bildbreite] = t;
                        }
                    }
                }

                _VordergrundSemaphore.WaitOne();
                Spiel2.foreground[c] = new Texture2D(Game1.device, Bildbreite, screenHeight, false, SurfaceFormat.Color);
                Game1.device.Textures[0] = null;
                Spiel2.foreground[c].SetData(Spiel2.foregroundColors[c]);
                _VordergrundSemaphore.Release();
            }

            Vierecke.RemoveAt(0);
            AktualisiereVordergrund(Vierecke);
        }

        /// <summary>
        ///     Aktualisiert das Bildmaterial für den Vordergrund (sehr schnell)
        /// </summary>
        /// <param name="Bereiche">
        ///     Ist eine Liste von zu aktualisierenden Bereichen.
        ///     x = X-Position, y = Y-Begin, z = Y-Ende
        /// </param>
        public static void AktualisiereVordergrund(List<Vector3> Bereiche)
        {
            // Übergabe:  x , y , Breite, Höhe
            Spiel Spiel2 = Game1.Spiel2;
            int screenWidth = Game1.screenWidth;
            int screenHeight = Game1.screenHeight;
            Rectangle screen = Game1.screen;
            SpriteBatch spriteBatch = Game1.spriteBatch;

            if (Bereiche.Count == 0) return;
            Color[,] water = Game1.Farbwahl(Texturen.wasser);
            var ischanged = new bool[Spiel2.foreground.Count()];

            int Bildbreite = 2048;
            for (int i = 0; i < Bereiche.Count; i++)
            {
                if (Bereiche[i].X < 0 || Bereiche[i].X >= Spiel2.Spielfeld.Count()) continue;
                if (Bereiche[i].Y < 0) Bereiche[i] = new Vector3(Bereiche[i].X, 0, Bereiche[i].Z);
                if (Bereiche[i].Z >= screenHeight)
                    Bereiche[i] = new Vector3(Bereiche[i].X, Bereiche[i].Y, screenHeight - 1);

                var c = (int) (Bereiche[i].X/Bildbreite);
                ischanged[c] = true;
                var x = (int) Bereiche[i].X;
                int h = (x - c*2048);
                int sum = 0;
                for (int y = 0; y < Spiel2.Spielfeld[x].Count; sum += Kartenformat.Laenge(Spiel2.Spielfeld[x][y]), y++)
                {
                    int anzahl = Kartenformat.Laenge(Spiel2.Spielfeld[x][y]);
                    if (sum >= Bereiche[i].Z) break;
                    if (sum + anzahl < Bereiche[i].Y) continue;
                    int sorte = Kartenformat.Material(Spiel2.Spielfeld[x][y]);
                    //(int) (Bereiche[i].Y - sum),  && sum + d <= Bereiche[i].Z
                    for (int d = 0; d < anzahl && sum + d <= Bereiche[i].Z; d++)
                    {// || sum + d > Bereiche[i].Z
                        if (sum + d < Bereiche[i].Y) continue; // fehlerhaft?

                        Color t;
                        if (Karte.Material[sorte].Farbe)
                        {
                            t = Karte.Material[sorte].CFarbe;
                        }
                        else
                            t = 
                                Karte.Material[sorte].CBild[
                                    (x%Karte.Material[sorte].Bild.Width) +
                                    ((sum + d)%Karte.Material[sorte].Bild.Height)*Karte.Material[sorte].Bild.Width];
                        if (!Kartenformat.isSet(x, sum + d) && sum + d > screenHeight - 20)
                        {
                           t = water[x%Texturen.wasser.Width, (sum + d)%Texturen.wasser.Height];
                        }
                        else if (!Editor.visible &&
                                 (!Kartenformat.isSet(x, sum + d - 5) || !Kartenformat.isSet(x + 5, sum + d) ||
                                  !Kartenformat.isSet(x, sum + d + 5) || !Kartenformat.isSet(x - 5, sum + d)) &&
                                 Karte.Material[sorte].Abdunkeln)
                        {
                            t.R /= (int) 2f;
                            t.G /= (int) 2f;
                            t.B /= (int) 2f;
                        }
                        Spiel2.foregroundColors[c][h + (sum + d)*Bildbreite] = t;
                    }
                }
            }
            Bereiche.Clear();

            for (int i = 0; i < Spiel2.foreground.Count(); i++)
                if (ischanged[i])
                {
                    Game1.SpriteBatchSemaphor.WaitOne();
                    _VordergrundSemaphore.WaitOne();
                    Game1.device.Textures[0] = null;
                    Spiel2.foreground[i].SetData(Spiel2.foregroundColors[i]);
                    _VordergrundSemaphore.Release();
                    Game1.SpriteBatchSemaphor.Release();
                }
        }

        /// <summary>
        ///     Erstellt das Bildmaterial für den Vordergrund (einmalig, sehr langsam)
        /// </summary>
        public static void ErstelleVordergrund()
        {
            Spiel Spiel2 = Game1.Spiel2;
            SpriteBatch spriteBatch = Game1.spriteBatch;

            if (Spiel2.foreground==null)
                Spiel2.foreground = new Texture2D[(int)Math.Ceiling((double)Spiel.Kartenbreite / 2048)];

            if (Spiel2.foregroundColors==null)
                Spiel2.foregroundColors = new Color[(int)Math.Ceiling((double)Spiel.Kartenbreite / 2048)][];

            for (int c = 0; c < Spiel2.foreground.Length; c++)
            {
                //Color[,] vordergrund = Farbwahl(Texturen.tilltexture);
                Color[,] water = Game1.Farbwahl(Texturen.wasser);

                int Bildbreite = 2048;
                Spiel2.foregroundColors[c] = new Color[Bildbreite * Game1.Kartenhoehe];

                for (int x = 2048*c; x < Spiel2.Spielfeld.Length && x < 2048*(c + 1); x++)
                {
                    int h = (x - c*2048);

                    int sum = 0;
                    for (int y = 0;
                        y < Spiel2.Spielfeld[x].Count;
                        sum += Kartenformat.Laenge(Spiel2.Spielfeld[x][y]), y++)
                    {
                        int sorte = Kartenformat.Material(Spiel2.Spielfeld[x][y]);
                        int anzahl = Kartenformat.Laenge(Spiel2.Spielfeld[x][y]);
                        for (int d = 0; d < anzahl; d++)
                        {
                            Color t;
                            if (Karte.Material[sorte].Farbe)
                            {
                                t = Karte.Material[sorte].CFarbe;
                            }
                            else
                                t =
                                    Karte.Material[sorte].CBild[
                                        (x%Karte.Material[sorte].Bild.Width) +
                                        ((sum + d)%Karte.Material[sorte].Bild.Height)*Karte.Material[sorte].Bild.Width];
                            if (!Kartenformat.isSet(x, sum + d) && sum + d > Game1.Kartenhoehe - 20)
                            {
                                t = water[x%Texturen.wasser.Width, (sum + d)%Texturen.wasser.Height];
                            }
                            else if ((!Kartenformat.isSet(x, sum + d - 5) || !Kartenformat.isSet(x + 5, sum + d) ||
                                      !Kartenformat.isSet(x, sum + d + 5) || !Kartenformat.isSet(x - 5, sum + d)) &&
                                     Karte.Material[sorte].Abdunkeln)
                            {
                                t.R /= (int) 2f;
                                t.G /= (int) 2f;
                                t.B /= (int) 2f;
                            }
                            Spiel2.foregroundColors[c][h + (sum + d)*Bildbreite] = t;
                        }
                    }
                }
                _VordergrundSemaphore.WaitOne();
                Spiel2.foreground[c] = new Texture2D(Game1.device, Bildbreite, Game1.Kartenhoehe, false, SurfaceFormat.Color);
                Game1.device.Textures[0] = null;
                Spiel2.foreground[c].SetData(Spiel2.foregroundColors[c]);
                _VordergrundSemaphore.Release();
            }
        }

        /// <summary>
        ///     Zeichnet den Vordergrund
        /// </summary>
        public static void ZeichneVordergrund()
        {
            Spiel Spiel2 = Game1.Spiel2;
            int screenWidth = Game1.screenWidth;
            int screenHeight = Game1.screenHeight;
            Rectangle screen = Game1.screen;
            SpriteBatch spriteBatch = Game1.spriteBatch;

            if (Spiel2 == null) return;

            for (int i = -1; i <= Spiel2.foreground.Length; i++)
            {
                int b = i;
                if (i == -1)
                {
                    b = Spiel2.foreground.Length - 1;
                    if (!Karte.KARTE_ZYKLISCH) continue;
                }
                if (i == Spiel2.foreground.Length)
                {
                    b = 0;
                    if (!Karte.KARTE_ZYKLISCH) continue;
                }

                if (Spiel2.Fenster.X > (i + 1)*2048 || Spiel2.Fenster.X + screenWidth < i*2048) continue;
                if ((int) Spiel2.Fenster.X + screenWidth < i*2048) continue;
                int x = (int)Spiel2.Fenster.X -i * 2048;
                var y = (int) Spiel2.Fenster.Y;

                Rectangle a;
                //if (x < 0)
                {
                    //continue;
                    //x = (i*2048 - (int) Spiel2.Fenster.X);
                    //screen = new Rectangle(0, 0, screenWidth, Game1.Kartenhoehe);
                    //a = new Rectangle(0, y2, screenWidth, h);
                    _VordergrundSemaphore.WaitOne();

                    if (b < Spiel2.foreground.Count())
                        spriteBatch.Draw(Spiel2.foreground[b],(Vector2) new Vector2(-x, -y), Color.White);
                    //spriteBatch.Draw(Spiel2.foreground[b], screen, a, Color.White, 0.0f, new Vector2(-x, 0),SpriteEffects.None, 1);
                    _VordergrundSemaphore.Release();
                }
                //else
                {
                    //continue;
                   // int l = 2048 - x;
                   // if (l > screenWidth) l = screenWidth;
                   // if (l < 0) l = 0;
                  //  screen = new Rectangle(0, 0, l, Game1.Kartenhoehe);
                   // a = new Rectangle(x, y2, l, h);
                    _VordergrundSemaphore.WaitOne();

                    //if (b<Spiel2.foreground.Count())
                    //    spriteBatch.Draw(Spiel2.foreground[b],new Vector2(l,y), Color.White);
                   // spriteBatch.Draw(Spiel2.foreground[b], screen, a, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1);
                    _VordergrundSemaphore.Release();
                }
            }

        }

        public static void ZeichneUnterwelt()
        {
            Spiel Spiel2 = Game1.Spiel2;
            int screenWidth = Game1.screenWidth;
            int screenHeight = Game1.screenHeight;
            Rectangle screen = Game1.screen;
            SpriteBatch spriteBatch = Game1.spriteBatch;

             // Wassser ???
            if (screenHeight - (Game1.Kartenhoehe - Spiel2.Fenster.Y) > 0)
            {
                int h = (int)(Game1.Kartenhoehe - Spiel2.Fenster.Y);
               // if (h > screenHeight) h = screenHeight;
                Help.DrawRectangle(Game1.spriteBatch, Game1.device, new Rectangle(0, (int)(h), screenWidth, (int)screenHeight-h), Microsoft.Xna.Framework.Color.Black, 1f);

            }
        }

        /// <summary>
        ///     Zeichnet die Minimap am unteren Rand des Bildschirms, anhand des Vordergrundes
        /// </summary>
        public static void DrawMinimap()
        {
            Spiel Spiel2 = Game1.Spiel2;
            int screenWidth = Game1.screenWidth;
            int screenHeight = Game1.screenHeight;
            SpriteBatch spriteBatch = Game1.spriteBatch;

            if (Spiel2 == null) return;
            int screenWidth2 = screenWidth * 4;
            var fensterx = (int)(Spiel2.Fenster.X - screenWidth2 / 2 + screenWidth / 2);
            float fact = 0.15f;

            for (int i = 0; i < Spiel2.foreground.Length; i++)
            {
                if (fensterx > (i + 1) * 2048 || fensterx + screenWidth2 < i * 2048) continue;
                if (fensterx + screenWidth2 < i * 2048) continue;
                int x = fensterx - i * 2048;
                var y = 0;// (int)Spiel2.Fenster.Y;

                Color col = Color.Gold;
                col = Color.DarkGreen;
                Rectangle a;
                if (x < 0)
                {
                    x = (i * 2048 - fensterx);
                    a = new Rectangle(0, y, 2048, Game1.Kartenhoehe); //screenWidth - screenWidth2 * fact
                    _VordergrundSemaphore.WaitOne();
                    spriteBatch.Draw(Spiel2.foreground[i],
                        new Vector2(screenWidth - screenWidth2 * fact, screenHeight - Game1.Kartenhoehe * fact), a, col, 0.0f,
                        new Vector2(-x, 0), fact, SpriteEffects.None, 1);
                    _VordergrundSemaphore.Release();
                }
                else
                {
                    int l = 2048 - x;
                    if (l > 2048) l = 2048;
                    if (l < 0) l = 0;
                    a = new Rectangle(x, y, l, Game1.Kartenhoehe);
                    // - screenHeight * fact
                    _VordergrundSemaphore.WaitOne();
                    spriteBatch.Draw(Spiel2.foreground[i],
                        new Vector2(screenWidth - screenWidth2 * fact, screenHeight - Game1.Kartenhoehe * fact), a, col, 0.0f,
                        new Vector2(0, 0), fact, SpriteEffects.None, 1);
                    _VordergrundSemaphore.Release();
                }
            }

            var c = new Rectangle(0, 0, screenWidth, Game1.Kartenhoehe);
            // - screenHeight * fact
            spriteBatch.Draw(Texturen.kasten,
                new Vector2(screenWidth - screenWidth2 / 2 * fact - screenWidth / 2 * fact, screenHeight - Game1.Kartenhoehe * fact), c,
                Color.White, 0.0f, new Vector2(0, 0), fact, SpriteEffects.None, 1);
        }


        #endregion Methods
    }
}