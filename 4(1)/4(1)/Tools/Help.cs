﻿// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-07-2013
// ***********************************************************************
// <copyright file="Help.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    /// Class Help
    /// </summary>
    unsafe public static class Help
    {
        public static Vector2 AufBereichVerkleinern(Texture2D Bild, int breite, int hoehe)
        {
            float maxy = hoehe; if (Bild.Height < hoehe) maxy = Bild.Height;

            Vector2 q = new Vector2((float)Bild.Width / ((float)Bild.Height / maxy), maxy);
            if (q.X <= breite) return q;

            // float maxx = q.X; if (Bild.Height < hoehe) maxx = Bild.Height;

            q = new Vector2(breite, (float)maxy / ((float)q.X / breite));
            return q;
        }

        /// <summary>
        /// The crypt
        /// </summary>
        public static String[] Crypt = { "q", "w", "e", "r", "t", "z", "u", "i", "o", "p", "ü", "a", "s", "d", "f", "g", "h", "j", "k", "l", "ö", "ä", "y", "x", "c", "v", "b", "n", "m", ",", ".", ";", ":", "_", "<", ">", "|", "!", "\"", "§", "%", "&", "/", "(", ")", "=", "?", "ß", "´", "`", "^", "°", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "+", "*", "~", "#", "'", "{", "}", "[", "]", "²", "³", "@", "€", "\\", "¡", "£", "¤", "¥", "¨", "«", "©", "¢", "¬", " ", "®", "¯", "±", "µ", "¶", "¹", "»", "¼", "½", "¾", "¿", "À", "Á", "Â", "Q", "W", "E", "R", "T", "Z", "U", "I", "O", "P", "Ü", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Ö", "Ä", "Y", "X", "C", "V", "B", "N", "M", "Ã", "Å", "Æ", "Ç", "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ð", "Ñ", "Ò", "Ó", "Ô", "Õ", "×", "Ø", "Ù", "Ú", "Û", "Ý", "Þ", "à", "á", "â", "ã", "å", "æ", "ç", "è", "é", "ê", "ì", "í", "î", "ï", "ð", "ñ", "ò", "ó", "ô", "õ", "÷", "ø", "ù", "ú", "û", "ý", "þ", "ÿ" };

        /// <summary>
        /// The RND
        /// </summary>
        public static Random rnd = new Random();

        /// <summary>
        /// The spielfeld
        /// </summary>
        public static List<UInt16>[] Spielfeld;

        /// <summary>
        /// Abstands the specified A.
        /// </summary>
        /// <param name="A">The A.</param>
        /// <param name="B">The B.</param>
        /// <returns>System.Single.</returns>
        public static float Abstand(Vector2 A, Vector2 B)
        {
            int dist = (int)Math.Sqrt((float)(Math.Pow(A.X - B.X, 2) + Math.Pow(A.Y - B.Y, 2)));
            if (dist < 0) dist = -dist;
            return dist;
        }

        // Angrabbeln (Trick 17)
        /// <summary>
        /// Angrabbel_funktions this instance.
        /// </summary>
        public static void angrabbel_funktion()
        {
            // damit die statischen Klassen auch wirklich existieren
            String text = "";
            Texture2D c = Spielermenu.spielermenu;
            //  c = Spielermenu.spielermenuok;
            c = Spielermenu.spielermenutextbox;
            text += Karte.LUFT.ToString();
            text += Spiel.Kartenbreite.ToString();
            text += Spieler.PLAYER_FUEL.Wert.ToString();
            text += Eingabefenster.ErsterAufruf.ToString();
            text += Baum.BAEUME.ToString();
            text += Haus.HAEUSER.ToString();
            text += Bunker.BUNKER.ToString();
            text += Allgemein.MaxTunnel.ToString();
            String q = "" + text;
            String q2 = q + text;
        }

        /// <summary>
        /// Converts the specified zahl.
        /// </summary>
        /// <param name="Zahl">The zahl.</param>
        /// <returns>String.</returns>
        public static String Convert(double Zahl)
        {
            String erg = "";
            Zahl = Zahl + 0.00001;
            int KEY = Crypt.Length;
            int b = (int)Zahl;
            for (int i = 3; i > -1; i--)
            {
                if (Zahl >= Math.Pow(KEY, i))
                {
                    int a = (int)(Zahl / Math.Pow(KEY, i));
                    Zahl = Zahl - (a * Math.Pow(KEY, i));
                    erg = erg + Crypt[a];
                }
                else
                    erg = erg + Crypt[0];
            }

            if ((int)Zahl > 0)
            {
                int a = (int)Zahl;
                erg = erg + Crypt[a];
            }

            for (; erg.Length > 1; )
            {
                if (erg.Substring(0, 1) == Crypt[0])
                {
                    erg = erg.Substring(1, erg.Length - 1);
                }
                else
                    break;
            }
            return erg;
        }

        /// <summary>
        /// Converts the texture2 D to MAP.
        /// </summary>
        /// <param name="Bild">The bild.</param>
        /// <returns>List{System.Int32}[][].</returns>
        public static List<int>[] ConvertTexture2DToMAP(Texture2D Bild)
        {
            List<int>[] Data = new List<int>[Bild.Width];
            Color[] BildData = new Color[Bild.Width * Bild.Height];
            Bild.GetData(BildData);

            for (int i = 0; i < Bild.Width; i++)
            {
                Data[i] = new List<int>();
                bool mode = false;
                int count = 0;
                for (int b = 0; b < Bild.Height; b++)
                {
                    if (mode == false && BildData[i + b * Bild.Width] != Color.Transparent)
                    {
                        Data[i].Add(count);
                        mode = true;
                        count = 0;
                    }
                    else
                        if (mode == true && BildData[i + b * Bild.Width] == Color.Transparent)
                        {
                            Data[i].Add(count);
                            mode = false;
                            count = 0;
                        }
                    count++;
                }
                if (count > 0) Data[i].Add(count);
            }
            return Data;
        }

        public static Texture2D ConvertMAPToTexture2D(Texture2D Bild, List<int>[] MAP)
        {
            Color[] BildData = new Color[Bild.Width * Bild.Height];
            Bild.GetData(BildData);

            for (int i = 0; i < MAP.Length; i++)
            {
                int pos = 0;
                for (int b = 0; b < MAP[i].Count; b++)
                {
                    if (b % 2 == 0)
                    {
                        for (int c = 0; c < MAP[i][b]; c++)
                        {
                            BildData[i + pos + c * Bild.Width] = Color.Transparent;
                        }
                    }
                    pos += MAP[i][b];
                }
            }

            Texture2D Bild2 = new Texture2D(Bild.GraphicsDevice, Bild.Width, Bild.Height);
            Bild2.SetData(BildData);

            return Bild2;
        }

        /// <summary>
        /// Converts the texture2 D to MASK.
        /// </summary>
        /// <param name="Bild">The bild.</param>
        /// <returns>System.Byte[][].</returns>
        public static byte[] ConvertTexture2DToMASK(Texture2D Bild)
        {
            byte[] Data = new byte[(int)Math.Ceiling((double)((Bild.Width * Bild.Height) / 8))];
            Color[] BildData = new Color[Bild.Width * Bild.Height];
            Bild.GetData(BildData);

            int pos = 0;
            byte zahl = 0;
            int pow = 0;

            for (int i = 0; i < Bild.Width; i++)
            {
                for (int b = 0; b < Bild.Height; b++)
                {
                    if (BildData[i + b * Bild.Width] != Color.Transparent)
                    {
                        zahl += (byte)Math.Pow(2.0d, (double)pow);
                    }

                    pow++;
                    if (pow >= 8)
                    {
                        Data[pos] = zahl;
                        pow = 0;
                        zahl = 0;
                        pos++;
                    }
                }
            }
            if (pos < Data.Count() - 1) Data[pos] = zahl;
            return Data;
        }

        /// <summary>
        /// Deconverts the specified zahl.
        /// </summary>
        /// <param name="Zahl">The zahl.</param>
        /// <returns>System.Int32.</returns>
        public static int Deconvert(String Zahl)
        {
            int KEY = 182;
            int erg = 0;
            for (int i = 1; i < Zahl.Length + 1; i++)
            {
                erg += (int)(find(Zahl.Substring(i, 1)) * Math.Pow(KEY, Zahl.Length - i));
            }
            return erg;
        }

        /// <summary>
        /// Deletes from to.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="x">The x.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="y2">The y2.</param>
        public static void DeleteFromTo(List<int>[] array, int x, int y1, int y2)
        {
            if (x >= array.Length) return;
            if (x < 0) return;
            if (y1 < 0) y1 = 0;
            if (y2 > Game1.screenHeight) y2 = Game1.screenHeight;
            int sum = 0;
            int last = 0;
            int sum1 = 0; int sum2 = 0; int last1 = 0; int last2 = 0;
            int a = -1;
            int b = -1;
            for (int i = 0; i < array[x].Count; i++)
            {
                last = sum;
                sum += array[x][i];
                if (a == -1 && y1 <= sum)
                {
                    a = i;
                    sum1 = sum;
                    last1 = last;
                }

                if (b == -1 && y2 <= sum)
                {
                    b = i;
                    sum2 = sum;
                    last2 = last;
                    break;
                }
            }

            if (a != -1 && b != -1)
            {
                // entferne
                if (a == b)
                {
                    if (a % 2 == 0)
                    {
                        // brauche nichts löschen
                    }
                    else
                        if (a % 2 == 1)
                        {
                            array[x].Insert(a + 1, sum2 - y2);
                            array[x].Insert(a + 1, y2 - y1);
                            array[x].Insert(a + 1, y1 - last1);
                            array[x].RemoveAt(a);
                        }
                }
                else
                {
                    if (a % 2 == 1 && b % 2 == 1)
                    {
                        for (int c = b - 1; c > a + 1; c--) array[x].RemoveAt(a + 1);
                        array[x][a] -= sum1 - y1;
                        array[x][a + 1] = (y2 - y1);
                        array[x][a + 2] -= y2 - last2;
                    }
                    else
                        if (a % 2 == 0 && b % 2 == 1)
                        {
                            for (int c = b - 1; c > a; c--) array[x].RemoveAt(a + 1);
                            array[x][a] += (y2 - y1) - (sum1 - y1);
                            array[x][a + 1] -= y2 - last2;
                        }
                        else
                            if (a % 2 == 1 && b % 2 == 0)
                            {
                                for (int c = b - 1; c > a; c--) array[x].RemoveAt(a + 1);
                                array[x][a + 1] += (y2 - y1) - (y2 - last2);
                                array[x][a] -= sum1 - y1;
                            }
                            else
                                if (a % 2 == 0 && b % 2 == 0)
                                {
                                    int summe = array[x][b];
                                    for (int c = b; c > a; c--) array[x].RemoveAt(a + 1);
                                    array[x][a] += (y2 - y1) - (sum1 - y1) - (y2 - last2) + summe;
                                }
                }
            }
        }

        /// <summary>
        /// Dirs the specified text.
        /// </summary>
        /// <param name="Text">The text.</param>
        public static void Dir(String Text)
        {
            if (!Directory.Exists(Text))
                Directory.CreateDirectory(Text);
        }

        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <param name="color">The color.</param>
        /// <param name="size">The size.</param>
        public static void DrawLine(SpriteBatch spriteBatch, Vector2 p1, Vector2 p2, Color color, int size)
        {
            float distance = Vector2.Distance(p1, p2);

            float angle = (float)Math.Atan2((double)(p2.Y - p1.Y), (double)(p2.X - p1.X));

            spriteBatch.Draw(Texturen.Punkt, p1, null, color, angle, Vector2.Zero, new Vector2(distance, size), SpriteEffects.None, 1);
        }

        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="coords">The coords.</param>
        /// <param name="color">The color.</param>
        public static void DrawRectangle(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Rectangle coords, Color color, float Transparenz)
        {
            //  var rect = new Texture2D(graphicsDevice, 1, 1);
            // rect.SetData(new[] { color });
            spriteBatch.Draw(Texturen.Punkt, coords, new Rectangle(0, 0, 1, 1), color * Transparenz, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        // Text mit schwarzem hintergrund ausgeben
        /// <summary>
        /// Draws the string.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        /// <param name="Text">The text.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="StringColor">Color of the string.</param>
        /// <param name="BackColor">Color of the back.</param>
        public static void DrawString(SpriteBatch spriteBatch, SpriteFont font, String Text, Vector2 pos, Color StringColor, Color BackColor)
        {
            // spriteBatch.Draw(Texturen.Punkt, new Rectangle((int)pos.X, (int)(pos.Y + font.MeasureString(Text).Y * 0.25f), (int)font.MeasureString(Text).X, (int)(font.MeasureString(Text).Y * 0.5f)), BackColor);

            spriteBatch.DrawString(font, Text, pos + new Vector2(1, 1), BackColor);
            spriteBatch.DrawString(font, Text, pos, StringColor);
        }

        /// <summary>
        /// Finds the specified zeich.
        /// </summary>
        /// <param name="zeich">The zeich.</param>
        /// <returns>System.Int32.</returns>
        public static int find(String zeich)
        {
            int KEY = 182;
            for (int i = 0; i < KEY; i++)
            {
                if (zeich == Crypt[i]) return i;
            }
            return 0;
        }

        /// <summary>
        /// Gets the messpunkte.
        /// </summary>
        /// <param name="Bild">The bild.</param>
        /// <returns>Vector2[][].</returns>
        public static Vector2[] GetMesspunkte(Texture2D Bild)
        {
            Vector2[] Data2 = new Vector2[6];
            List<int>[] Data = new List<int>[Bild.Width];
            Color[] BildData = new Color[Bild.Width * Bild.Height];
            Bild.GetData(BildData);

            bool found = false;
            for (int i = 0; i < Bild.Width && i < Bild.Height; i++)
                if (BildData[i + i * Bild.Width] != Color.Transparent)
                {
                    Data2[0] = new Vector2(i, i);
                    found = true;
                }
            if (!found) Data2[0] = new Vector2(0, 0);

            found = false;
            for (int i = Bild.Width - 1; i >= 0; i--)
                for (int b = Bild.Height - 1; b >= 0; b--)
                    if (BildData[i + b * Bild.Width] != Color.Transparent)
                    {
                        Data2[1] = new Vector2(i, b);
                        found = true;
                    }
            if (!found) Data2[1] = new Vector2(Bild.Width - 1, Bild.Height - 1);

            found = false;
            for (int i = 0; i < Bild.Width; i++)
                for (int b = Bild.Height - 1; b >= 0; b--)
                    if (BildData[i + b * Bild.Width] != Color.Transparent)
                    {
                        Data2[2] = new Vector2(i, b);
                        found = true;
                    }
            if (!found) Data2[2] = new Vector2(0, Bild.Height - 1);

            found = false;
            for (int i = Bild.Width - 1; i >= 0; i--)
                for (int b = 0; b < Bild.Height; b++)
                    if (BildData[i + b * Bild.Width] != Color.Transparent)
                    {
                        Data2[3] = new Vector2(i, b);
                        found = true;
                    }
            if (!found) Data2[3] = new Vector2(Bild.Width - 1, 0);

            found = false;
            for (int b = 0; b < Bild.Height; b++)
                if (BildData[Bild.Width / 2 + b * Bild.Width] != Color.Transparent)
                {
                    Data2[4] = new Vector2(Bild.Width / 2, b);
                    found = true;
                }
            if (!found) Data2[4] = new Vector2(Bild.Width / 2, 0);

            found = false;
            for (int b = Bild.Height - 1; b >= 0; b--)
                if (BildData[Bild.Width / 2 + b * Bild.Width] != Color.Transparent)
                {
                    Data2[5] = new Vector2(Bild.Width / 2, b);
                    found = true;
                }
            if (!found) Data2[5] = new Vector2(Bild.Width / 2, Bild.Height - 1);

            return Data2;
        }

        public static Vector2 GetMousePos()
        {
            System.Drawing.Point offset = Hauptfenster.Program.Formular.pictureBox1.PointToScreen(new System.Drawing.Point(0, 0));

            MouseState temp = Mouse.GetState();
            //System.Drawing.Point iss = Hauptfenster.Program.Formular.pictureBox1.PointToClient(new System.Drawing.Point(temp.X, temp.Y));
            // System.Drawing.Point offset = Hauptfenster.Program.Formular.pictureBox1.PointToScreen(iss);
            return new Vector2(temp.X - offset.X, temp.Y - offset.Y);
        }

        public static MouseState GetMouseState()
        {
            System.Drawing.Point offset = Hauptfenster.Program.Formular.pictureBox1.PointToScreen(new System.Drawing.Point(0, 0));

            MouseState temp = Mouse.GetState();
            System.Drawing.Point iss = Hauptfenster.Program.Formular.pictureBox1.PointToClient(new System.Drawing.Point(temp.X, temp.Y));
            //temp.X - offset.X, temp.Y - offset.Y
            MouseState result = new MouseState(temp.X - offset.X, temp.Y - offset.Y, temp.ScrollWheelValue, temp.LeftButton, temp.MiddleButton, temp.RightButton, temp.XButton1, temp.XButton2);
            return result;
        }

        /// <summary>
        /// Gets the pixel anzahl.
        /// </summary>
        /// <param name="Bild">The bild.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPixelAnzahl(Texture2D Bild)
        {
            List<int>[] Data = new List<int>[Bild.Width];
            Color[] BildData = new Color[Bild.Width * Bild.Height];
            Bild.GetData(BildData);
            int Anzahl = 0;

            for (int i = 0; i < Bild.Width; i++)
            {
                for (int b = 0; b < Bild.Height; b++)
                {
                    if (BildData[i + b * Bild.Width] != Color.Transparent)
                    {
                        Anzahl++;
                    }
                }
            }
            return Anzahl;
        }

        /// <summary>
        /// Gleichers the anteil.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Single.</returns>
        public static float GleicherAnteil(float a, float b)
        {
            if (a > 0 && b > 0)
                return a < b ? a : b;

            if (a < 0 && b < 0)
                return a > b ? a : b;

            return 0;
        }

        public static String HASH(String Datei)
        {
            ReaderStream.ReaderStream daten = new ReaderStream.ReaderStream(Datei);
            MemoryStream a = new MemoryStream();

            while (!daten.EndOfStream)
            {
                a.WriteByte(daten.ReadByte());
            }
            daten.Close();

            a.Position = 0;

            String q = HASH(a);
            a.Close();
            return q;
        }

        /// <summary>
        /// HASHs the specified daten.
        /// </summary>
        /// <param name="Daten">The daten.</param>
        /// <returns>String.</returns>
        public static String HASH(Stream Daten)
        {
            MD5 hash = MD5.Create();
            char[] g = Encoding.ASCII.GetChars(hash.ComputeHash(Daten));
            String q = new string(g);
            return q;
        }

        /// <summary>
        /// Determines whether the specified array is set.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns><c>true</c> if the specified array is set; otherwise, <c>false</c>.</returns>
        public static bool isSet(List<int>[] array, float x, float y) // neu
        {
            if ((int)x >= array.Length) return false;
            if ((int)x < 0) return false;
            int sum = 0;
            for (int i = 0; i < array[(int)x].Count; i++)
            {
                sum += array[(int)x][i];
                if ((int)y < sum) return i % 2 == 0 ? false : true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is set in mask] [the specified array].
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="BildBreite">The bild breite.</param>
        /// <returns><c>true</c> if [is set in mask] [the specified array]; otherwise, <c>false</c>.</returns>
        public static bool isSetInMask(byte[] array, float x, float y, int BildBreite)
        {
            if (x < 0 || y < 0 || x >= BildBreite || y >= array.Length / BildBreite) return false;
            byte Byte = array[(int)((x + y * BildBreite) / 8)];
            int Bit = (int)((x + y * BildBreite) % 8);
            byte q = 128;
            q = (byte)(q >> Bit);
            byte p = (byte)(Byte & q);
            if (p > 0) return true;
            return false;
        }

        public static String ListeZuString(List<String> Text)
        {
            String result = "";
            for (int i = 0; i < Text.Count; i++)
            {
                result = result + Text[i] + "\n";
            }
            return result;
        }

        // Gibt den Winkel für die Drehung zurück (als Bogenmaß)
        /// <summary>
        /// Punkts the drehen um bestimmte entfernung.
        /// </summary>
        /// <param name="Drehpunkt">The drehpunkt.</param>
        /// <param name="EigenePosition">The eigene position.</param>
        /// <param name="Strecke">The strecke.</param>
        /// <returns>System.Single.</returns>
        public static float PunktDrehenUmBestimmteEntfernung(Vector2 Drehpunkt, Vector2 EigenePosition, float Strecke)
        {
            float r = Abstand(EigenePosition, Drehpunkt);
            return Strecke / r;
        }

        /// <summary>
        /// Punkts the linie.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>System.Single[][][].</returns>
        public static float[][] PunktLinie(float x1, float x2, float y1, float y2)
        {
            Vector2 Richtung = new Vector2(x2 - x1, y2 - y1);
            //double VektorLange = Math.Sqrt(Richtung.X * Richtung.X + Richtung.Y * Richtung.Y);
            int AnzPunkte = 100;
            Richtung = new Vector2(Richtung.X / AnzPunkte, Richtung.Y / AnzPunkte);
            float[][] Punktarray = new float[AnzPunkte][];
            for (int i = 0; i < AnzPunkte; i++)
            {
                Punktarray[i][0] = x1 + i * Richtung.X;
                Punktarray[i][1] = y1 + i * Richtung.Y;
            }
            return Punktarray;
        }

        // neu
        /// <summary>
        /// Reparieres the bild.
        /// </summary>
        /// <param name="Bild">The bild.</param>
        /// <param name="Original">The original.</param>
        /// <param name="Pixel">The pixel.</param>
        /// <returns>System.Int32.</returns>
        public static int RepariereBild(Texture2D Bild, Texture2D Original, int Pixel)
        {
            Color[] temp = new Color[Bild.Width * Bild.Height];
            Bild.GetData(temp);

            Color[] temp2 = new Color[Original.Width * Original.Height];
            Original.GetData(temp2);

            int anz = 0;
            for (int i = Bild.Width - 1; i >= 0 && anz < Pixel; i--)
                for (int b = 0; b < Bild.Height && anz < Pixel; b++)
                {
                    if (temp[b + i * Bild.Height] == Color.Transparent && temp2[b + i * Bild.Height] != Color.Transparent)
                    {
                        temp[b + i * Bild.Height] = temp2[b + i * Bild.Height];
                        anz++;
                    }
                }

            Bild.SetData(temp);
            return anz;
        }

        // fehlerhaft
        /// <summary>
        /// Rests the anteil.
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <param name="id">The id.</param>
        /// <returns>System.Single.</returns>
        public static float RestAnteil(float a, float b, int id) // 0 == a , 1 == b
        {
            if (a >= 0 && b >= 0)
                return a < b ? (id == 0 ? 0 : b - a) : (id == 0 ? a - b : 0);

            if (a <= 0 && b <= 0)
                return a > b ? (id == 0 ? 0 : b - a) : (id == 0 ? a - b : 0);

            if ((a <= 0 && b >= 0) || (a >= 0 && b <= 0))
                return Math.Abs(a) < Math.Abs(b) ? (id == 0 ? 0 : a + b) : (id == 0 ? a + b : 0);

            return 0;
        }

        // Rotiert den Punkt "EigenePosition" um den "Drehpunkt"
        /// <summary>
        /// Rotates the position.
        /// </summary>
        /// <param name="Drehpunkt">The drehpunkt.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="EigenePosition">The eigene position.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 RotatePosition(Vector2 Drehpunkt, float angle, Vector2 EigenePosition)
        {
            return Drehpunkt - KollisionsObjekt.Rotiere(angle, new Vector3(0, 0, 1), Drehpunkt - EigenePosition);
        }

        // Rotiert einen Punkt mit einem Abstand "offset" zum "Drehpunkt" um den "Drehpunkt"
        /// <summary>
        /// Rotates the position offset.
        /// </summary>
        /// <param name="Drehpunkt">The drehpunkt.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 RotatePositionOffset(Vector2 Drehpunkt, float angle, Vector2 offset)
        {
            return Drehpunkt - KollisionsObjekt.Rotiere(angle, new Vector3(0, 0, 1), offset);
        }

        // Funktionen für Kartenformat
        /// <summary>
        /// Summes the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="x">The x.</param>
        /// <returns>System.Int32.</returns>
        public static int Summe(List<int>[] array, int x) // neu
        {
            int sum = 0;
            for (int i = 0; i < array[x].Count; i++) sum += array[x][i];
            return sum;
        }

        // fehlerhaft
        // Der Betrag eines Vektors a
        /// <summary>
        /// Vektors the betrag.
        /// </summary>
        /// <param name="a">A.</param>
        /// <returns>System.Double.</returns>
        public static double VektorBetrag(Vector2 a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        // Kürzt eine Vektor
        /// <summary>
        /// Vektors the kürzen.
        /// </summary>
        /// <param name="Vektor">The vektor.</param>
        /// <param name="Faktor">The faktor.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 VektorKürzen(Vector2 Vektor, float Faktor)
        {
            return Vektor * Faktor;
        }

        // Reflektiert einen Vektor an der Geraden die durch P1, P2 geht    (Reflektionsfaktor = Energieverlust beim Reflektieren)
        /// <summary>
        /// Vektors the reflexion.
        /// </summary>
        /// <param name="EingehenderVektor">The eingehender vektor.</param>
        /// <param name="P1">The p1.</param>
        /// <param name="P2">The p2.</param>
        /// <param name="Reflektionsfaktor">The reflektionsfaktor.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 VektorReflexion(Vector2 EingehenderVektor, Vector2 P1, Vector2 P2, float Reflektionsfaktor)
        {
            Vector2 Gerade = P1 - P2;
            Vector2 normale = new Vector2(Gerade.Y, -Gerade.X);
            //   return EingehenderVektor - (2 * EingehenderVektor * normale) * normale;
            return Vector2.Reflect(EingehenderVektor, normale) * Reflektionsfaktor;
        }

        /// <summary>
        /// tut den Text auf Zeilen unterteilen
        /// </summary>
        public static List<string> ZerhackeTextAufFesteBreite(SpriteFont font, String content, int maxPixelInZeile, bool mitEnterzeichen)
        {
            List<string> output = new List<string>();

            string[] aux2 = content.Split('\n');
            for (int i = 0; i < aux2.Count(); i++)
            {
                string[] aux = aux2[i].Split(' ');

                string temp = "";
                for (int b = 0; b < aux.Count(); b++)
                {
                    Vector2 Maße = font.MeasureString(temp + aux[b] + " ");
                    if (Maße.X <= maxPixelInZeile)
                    {
                        temp = temp + aux[b] + " ";
                    }
                    else
                    {
                        if (font.MeasureString(aux[b]).X > maxPixelInZeile)
                        {
                            aux[b] = temp + aux[b] + " ";

                            List<string> res = ZerlegeInFesteBreiten(font, aux[b], maxPixelInZeile);
                            for (int r = 0; r < res.Count - 1; r++)
                            {
                                //  if (res[r][res[r].Length - 1] == ' ') res[r] = res[r].Substring(0, res[r].Length - 1);
                                output.Add(res[r]);
                            }
                            temp = res[res.Count - 1] + " ";
                        }
                        else
                        {
                            // if (temp[temp.Length - 1] == ' ') temp = temp.Substring(0, temp.Length - 1);
                            output.Add(temp);
                            temp = aux[b] + " ";
                        }
                    }
                }

                if (temp.Length > 0)
                {
                    if (font.MeasureString(temp).X > maxPixelInZeile)
                    {
                        List<string> res = ZerlegeInFesteBreiten(font, temp, maxPixelInZeile);
                        for (int r = 0; r < res.Count; r++)
                        {
                            //  if (res[r][res[r].Length - 1] == ' ') res[r] = res[r].Substring(0, res[r].Length - 1);
                            output.Add(res[r]);
                        }
                    }
                    else
                    {
                        //  if (temp[temp.Length - 1] == ' ') temp = temp.Substring(0, temp.Length - 1);
                        output.Add(temp);
                    }
                }

                if ((aux2[i].Length > 0 && aux2[i][aux2[i].Length - 1] != ' ') || (aux2[i].Length == 0))
                {
                    if (output[output.Count - 1].Length > 0)
                        if (output[output.Count - 1][output[output.Count - 1].Length - 1] == ' ')
                            output[output.Count - 1] = output[output.Count - 1].Substring(0, output[output.Count - 1].Length - 1);
                }

                if (mitEnterzeichen)//&& output[output.Count - 1].Length>0
                {
                    output[output.Count - 1] = output[output.Count - 1] + "\n";
                }
            }

            if (output.Count > 0 && output[output.Count - 1].Length > 0)
                if (output[output.Count - 1][output[output.Count - 1].Length - 1] == '\n')
                    output[output.Count - 1] = output[output.Count - 1].Substring(0, output[output.Count - 1].Length - 1);

            return output;
        }

        private static List<string> ZerlegeInFesteBreiten(SpriteFont font, String Text, int PixelBreite)
        {
            List<string> output = new List<string>();
            for (int i = 1; i <= Text.Length; i++)
            {
                if (font.MeasureString(Text.Substring(0, i)).X > PixelBreite)
                {
                    output.Add(Text.Substring(0, i - 1));
                    Text = Text.Substring(i - 1, Text.Length - (i - 1));
                    i = 0;
                }
            }

            if (Text.Length > 0)
                output.Add(Text);

            return output;
        }
    }
}