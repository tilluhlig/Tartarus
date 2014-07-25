// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-29-2013
// ***********************************************************************
// <copyright file="Fog.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    /// Class Fog
    /// </summary>
    public static class Fog
    {
        /// <summary>
        /// The endnebel
        /// </summary>
        public static Texture2D[] endnebel = new Texture2D[2];

        /// <summary>
        /// Creates the fog.
        /// </summary>
        public static void CreateFog() // Ersetzen
        {
            /* Color[,] vordergrund = Farbwahl(Texturen.nebel);
             Color[,] water = Farbwahl(Texturen.wasser);

             int Bildbreite = 2048;
             Spiel2.fogColors = new Color[Bildbreite * screenHeight];

             for (int x = 0; x < 2048; x++)
             {
                 for (int y = 0; y < screenHeight; y++)
                 {
                     Spiel2.fogColors[x + y * Bildbreite] = vordergrund[x % Texturen.nebel.Width, y % Texturen.nebel.Height];
                 }
             }

             //Spiel2.Nebelkreis = Help.ConvertTexture2DToMAP(Texturen.nebelkreis);
             Spiel2.Nebelkreis = new Color[Texturen.nebelkreis.Width * Texturen.nebelkreis.Height];
             Texturen.nebelkreis.GetData(Spiel2.Nebelkreis);*/
        }

        /// <summary>
        /// Generates the scenery fog.
        /// </summary>
        public static void GenerateSceneryFog() // Zeichnet den Nebel (Karte)
        {
            /*
            if (Spiel2 == null) return;

            int zz = 0;
            for (int i = 0; i < Spiel2.foreground.Length; i++)
            {
                if (Spiel2.Fenster.X > (i + 1) * 2048 || Spiel2.Fenster.X + screenWidth < i * 2048) continue;
                if ((int)Spiel2.Fenster.X + screenWidth < i * 2048) continue;
                int x = (int)Spiel2.Fenster.X - i * 2048;
                int y = (int)Spiel2.Fenster.Y;

                endnebel[zz] = new Texture2D(device, 2048, screenHeight, false, SurfaceFormat.Color);
                Color[] daten = new Color[2048 * screenHeight];
                Spiel2.fogColors.CopyTo(daten, 0);
                //  Spiel2.fog[i].GetData(daten);

                int halbernebel = Texturen.nebelkreis.Width / 2;
                for (int p = 0; p < Spiel2.players.Count(); p++)
                    for (int b = 0; b < Spiel2.players[p].Angle.Count; b++)
                    {
                        Vector2 pos = Spiel2.players[p].pos[b];
                        pos.X -= i * 2048;
                        if (pos.X + halbernebel < 0 || pos.X - halbernebel >= 2048 || pos.Y + halbernebel < 0 || pos.Y - halbernebel >= screenHeight) continue;
                        pos.X -= halbernebel;
                        pos.Y -= halbernebel;
                        int left = 0; if (pos.X < 0) { left = (int)-pos.X; pos.X = 0; }
                        int top = 0; if (pos.Y < 0) { top = (int)-pos.Y; pos.Y = 0; }
                        int width = 2 * halbernebel - left; if (pos.X + width >= 2048) width = (2 * halbernebel) - (((int)pos.X + width) - 2048);
                        int height = 2 * halbernebel - top; if (pos.Y + height >= screenHeight) height = (2 * halbernebel) - (((int)pos.Y + height) - screenHeight);

                        // das ist ein Kreis
                        for (int l = left; l < left + width; l++)
                            for (int k = top; k < top + height; k++)
                            {
                                if (Spiel2.Nebelkreis[l + k * Texturen.nebelkreis.Width] == Color.Transparent)
                                //if (!Help.isSet(Spiel2.Nebelkreis,l,k))
                                {
                                    daten[(int)pos.X + (l - left) + ((int)pos.Y + (k - top)) * 2048] = Color.Transparent;
                                }
                            }

                        endnebel[zz].SetData(daten);
                    }
                zz++;
            }*/
        }

        /// <summary>
        /// Draws the scenery fog.
        /// </summary>
        public static void DrawSceneryFog() // Zeichnet den Nebel (Karte)
        {/*
            if (Spiel2 == null) return;
            Vector3 screenScalingFactor;

            float horScaling = (float)device.PresentationParameters.BackBufferWidth / baseScreenSize.X;
            float verScaling = (float)device.PresentationParameters.BackBufferHeight / baseScreenSize.Y;
            screenScalingFactor = new Vector3(horScaling, verScaling, 1);
            Matrix globalTransformation = Matrix.CreateScale(screenScalingFactor);
            int zz = 0;
            for (int i = 0; i < Spiel2.foreground.Length; i++)
            {
                if (Spiel2.Fenster.X > (i + 1) * 2048 || Spiel2.Fenster.X + screenWidth < i * 2048) continue;
                if ((int)Spiel2.Fenster.X + screenWidth < i * 2048) continue;
                int x = (int)Spiel2.Fenster.X - i * 2048;
                int y = (int)Spiel2.Fenster.Y;

                Rectangle a;
                if (x < 0)
                {
                    //continue;
                    x = (i * 2048 - (int)Spiel2.Fenster.X);
                    screen = new Rectangle((int)0, 0, screenWidth, screenHeight);
                    a = new Rectangle(0, y, screenWidth, screenHeight);
                    spriteBatch.Draw(endnebel[zz], screen, a, Color.White * 0.8f, 0.0f, new Vector2(-x, 0), SpriteEffects.None, 1);
                }
                else
                {
                    //continue;
                    int l = 2048 - x; if (l > screenWidth) l = screenWidth; if (l < 0) l = 0;
                    screen = new Rectangle((int)0, 0, l, screenHeight);
                    a = new Rectangle(x, y, l, screenHeight);
                    spriteBatch.Draw(endnebel[zz], screen, a, Color.White * 0.8f, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1);
                }
                zz++;
            }*/
        }
    }
}