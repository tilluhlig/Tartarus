// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-31-2013
// ***********************************************************************
// <copyright file="MapReader.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    /// Class MapReader
    /// </summary>
    public class MapReader
    {
        /// <summary>
        /// The convert
        /// </summary>
        public static bool Convert = false;

        /// <summary>
        /// The list
        /// </summary>
        public static List<String> list = new List<String>();

        /// <summary>
        /// Finds the begin.
        /// </summary>
        /// <param name="Text">The text.</param>
        /// <param name="Data">The data.</param>
        /// <returns>System.Int32.</returns>
        private int FindBegin(String Text, List<String> Data)
        {
            for (int i = 0; i < Data.Count; i++)
                if (Data[i] == Text)
                    return i;
            return -1;
        }

        /// <summary>
        /// INTs the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Int32.</returns>
        private int INT(String data)
        {
            return System.Convert.ToInt32(data);
        }

        /// <summary>
        /// FLOATs the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Single.</returns>
        private float FLOAT(String data)
        {
            return (float)(System.Convert.ToDouble(data));
        }

        /// <summary>
        /// Loads the map.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="Map">The map.</param>
        /// <param name="_Data">The _ data.</param>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="screen">The screen.</param>
        public void LoadMap(Game1 game, String Map, String _Data, ref Spiel Spielfeld, Vector2 screen)
        {
            Hauptfenster.Program.Formular.progressBar1.Value = 0;
            Hauptfenster.Program.Formular.progressBar1.Show();
            Hauptfenster.Program.Formular.progressBar1.BringToFront();

            Hauptfenster.Program.Formular.label31.Show();
            Hauptfenster.Program.Formular.label31.BringToFront();
            Spielfeld = null;

            // Reset
            Karte.Reset_Materialien();
            Fahrzeugdaten.Reset_Tankdata();

            Game1.LadeText("    Tastatur...    ");
            if (!File.Exists(Map)) { Spielfeld = null; return; }
            // Tastatur laden
            Tastatur.LadeTastaturbelegung("Content\\Konfiguration\\Tastatur.conf");

            Game1.LadeText("    Mod...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 20;
            // Mod laden
            Mod.LadeModVariablen("Content\\Konfiguration\\" + Hauptfenster.Tausch.Mod);

            Game1.LadeText("    Komponenten...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 40;
            // Komponenten laden
            game.loadAllContent();

            StreamReader datei = new StreamReader(Map);
            List<String> Data = new List<String>();
            for (; !datei.EndOfStream; ) Data.Add(datei.ReadLine());
            datei.Close();

            Game1.LadeText("    Kartendaten...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 65;
            // Kartendaten laden
            int Kartengroesse;
            int Kartenhoehe;
            {
                int a = FindBegin("Kartendaten", Data); if (a == -1) { Spielfeld = null; return; } a++;
                Spielfeld = new Spiel();
                Kartengroesse = INT(Data[a]);
                Kartenhoehe = INT(Data[a + 1]);

                Spielfeld.Schuesse = INT(Data[a + 2]);
                Spielfeld.WindTimeout = INT(Data[a + 3]);
                Spielfeld.Wind = new Vector2(INT(Data[a + 4]), INT(Data[a + 5]));
                Spielfeld.Timeout = INT(Data[a + 6]);
                Spielfeld.CurrentMissile = INT(Data[a + 7]);
                Spielfeld.CurrentPlayer = INT(Data[a + 8]);

                Spielfeld.increaseshot = false;
                Spielfeld.Spielfeld = new List<UInt16>[Kartengroesse];
                for (int i = 0; i < Kartengroesse; i++) Spielfeld.Spielfeld[i] = new List<UInt16>();

                Spielfeld.Missile = new Waffen[100];
                for (int i = 0; i < Spielfeld.Missile.Length; i++) { Spielfeld.Missile[i] = new Waffen(); Spielfeld.Missile[i].ID = i; }
                //SetUpPlayers();

                Spielfeld.Fenster.X = 0; Spielfeld.Fenster.Y = 0;
                Spielfeld.Karte = new Karte();

                Spielfeld.Width = (int)screen.X;
                Spielfeld.Height = (int)screen.Y;

                Spielfeld.foreground = new Texture2D[(int)Math.Ceiling((double)Kartengroesse / 2048)];
                Spielfeld.foregroundColors = new Color[(int)Math.Ceiling((double)Kartengroesse / 2048)][];
            }

            Game1.LadeText("    Bäume...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 70;

            Game1.LadeText("    Häuser...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 75;
            // Haeuser
            {
                int a = FindBegin("Haeuser", Data); if (a == -1) { Spielfeld = null; return; } a++;
                int anz = INT(Data[a]); a++;
                for (int i = 0; i < anz; i++, a += 7)
                {
                    Spielfeld.Haeuser.Add(new Vector2(FLOAT(Data[a]), FLOAT(Data[a + 1])), INT(Data[a + 2]), INT(Data[a + 3]), INT(Data[a + 4]));
                    //  Spielfeld.Haeuser.hp[Spielfeld.Haeuser.hp.Count - 1] = INT(Data[a + 2]);
                    //  Spielfeld.Haeuser.Besitzer[Spielfeld.Haeuser.Besitzer.Count - 1] = INT(Data[a + 4]);
                    int id = Spielfeld.Haeuser.Position.Count - 1;
                    // Maske einlesen

                    if (Data[a + 5] == "MASK" && Data[a + 5 + 1] != "ENDMASK")
                    {
                        int b = a + 5;
                        b++;
                        List<List<int>> list = new List<List<int>>();
                        int p = 0;
                        for (; b < Data.Count; b++, p++)
                        {
                            if (Data[b] == "ENDMASK") break;
                            list.Add(new List<int>());
                            string[] dat = Data[b].Split('-');
                            for (int q = 0; q < dat.Count(); q++) list[p].Add((INT(dat[q])));
                        }

                        Spielfeld.Haeuser.Kollision[id].Bild = new List<int>[list.Count];
                        for (int c = 0; c < list.Count; c++)
                        {
                            Spielfeld.Haeuser.Kollision[id].Bild[c] = new List<int>();
                            for (int d = 0; d < list[c].Count; d++)
                            {
                                Spielfeld.Haeuser.Kollision[id].Bild[c].Add(list[c][d]);
                            }
                        }
                        Spielfeld.Haeuser.Bild[id] = Spielfeld.Haeuser.Kollision[id].UseMaskOnTexture2D(Spielfeld.Haeuser.Bild[id]);
                        list = null;
                        a = b - 7 + 1;
                    }
                    // ENDMASKE einlesen
                }
            }

            Game1.LadeText("    Bunker...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 80;
            // Bunker
            {
                int a = FindBegin("Bunker", Data); if (a == -1) { Spielfeld = null; return; } a++;
                int anz = INT(Data[a]); a++;
                for (int i = 0; i < anz; i++, a += 7)
                {
                    Spielfeld.Bunker.Position.Add(new Vector2(FLOAT(Data[a]), FLOAT(Data[a + 1])));
                    Spielfeld.Bunker.Lebenspunkte.Add(INT(Data[a + 2]));
                    // Spielfeld.Bunker.HausGetroffen.Add(INT(Data[a + 3]));
                    Spielfeld.Bunker.Besitzer.Add(INT(Data[a + 4]));
                    //  Spielfeld.Bunker.BunkerTyp.Add(INT(Data[a + 5]));
                    int id = Spielfeld.Bunker.Position.Count - 1;
                    // Maske einlesen

                    if (Data[a + 6] == "MASK" && Data[a + 5 + 1] != "ENDMASK")
                    {
                        int b = a + 6;
                        b++;
                        List<List<int>> list = new List<List<int>>();
                        int p = 0;
                        for (; b < Data.Count; b++, p++)
                        {
                            if (Data[b] == "ENDMASK") break;
                            list.Add(new List<int>());
                            string[] dat = Data[b].Split('-');
                            for (int q = 0; q < dat.Count(); q++) list[p].Add((INT(dat[q])));
                        }

                        Spielfeld.Bunker.Kollision[id].Bild = new List<int>[list.Count];
                        for (int c = 0; c < list.Count; c++)
                        {
                            Spielfeld.Bunker.Kollision[id].Bild[c] = new List<int>();
                            for (int d = 0; d < list[c].Count; d++)
                            {
                                Spielfeld.Bunker.Kollision[id].Bild[c].Add(list[c][d]);
                            }
                        }
                        //Spielfeld.Bunker.Bild[id] = Spielfeld.Bunker.Kollision[id].UseMaskOnTexture2D(Spielfeld.Bunker.Bild[id]);
                        list = null;
                        a = b - 7 + 1;
                    }
                    // ENDMASKE einlesen
                }
            }

            Game1.LadeText("    Fahrzeuge...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 85;
            // Panzer
            {
                Color[] Spielerfarben = { new Color(1f, 0, 0), new Color(0, 0, 1f) };
                float[] Angle = { 180, 0 };

                int a = FindBegin("Panzer", Data); if (a == -1) { Spielfeld = null; return; } a++;
                int anz = INT(Data[a]); a++;
                Spielfeld.players = new Spieler[anz];
                for (int c = 0; c < Spielfeld.players.Length; c++) { Spielfeld.players[c] = new Spieler(); } // Initialenergie

                for (int q = 0; q < anz; q++)
                {
                    Spielfeld.players[q].Credits = INT(Data[a + 1]);
                    Spielfeld.players[q].fuelRemains = FLOAT(Data[a + 2]);
                    Spielfeld.players[q].ActionPoints = INT(Data[a + 3]);
                    if (Path.GetFileName(Hauptfenster.Tausch.Data) == "OldData.dat" && q == Hauptfenster.Tausch.CurrentPlayer)
                    {
                        Spielfeld.players[q].fuelRemains = 999;
                        Spielfeld.players[q].ActionPoints += 999;
                    }

                    int besitzer = q;
                    int anz2 = INT(Data[a + 4]);

                    for (int c = 0; c < anz2; c++)
                    {
                        Spielfeld.players[q].Munition[c] = new List<int>();
                        for (int b = 0; b < Waffendaten.Daten.Count(); b++)
                        {
                            Spielfeld.players[q].Munition[c].Add(99);
                        }
                    }

                    a += 5;
                    for (int i = 0; i < anz2; i++, a += 15)
                    {
                        Spielfeld.players[besitzer].KindofTank.Add(0);
                        Spielfeld.players[besitzer].hp.Add(0);
                        Spielfeld.players[besitzer].Angle.Add(0);
                        Spielfeld.players[besitzer].vehikleAngle.Add(0);
                        Spielfeld.players[besitzer].isthere.Add(true);
                        Spielfeld.players[besitzer].overreach.Add(false);
                        //Spielfeld.players[besitzer].freezed.Add(0);
                        Spielfeld.players[besitzer].Size.Add(0.0f);
                        Spielfeld.players[besitzer].SizeOfCannon.Add(0.0f);
                        Spielfeld.players[besitzer].pos.Add(new Vector2(-1, -1));
                        // Spielfeld.players[besitzer].ExpProgress.Add(0);
                        // Spielfeld.players[besitzer].ExpNow.Add(0);
                        Spielfeld.players[besitzer].CurrentLv.Add(0);
                        Spielfeld.players[besitzer].Cooldown.Add(0);
                        //Spielfeld.players[besitzer].poisoned.Add(0);

                        int pos = Spielfeld.players[besitzer].hp.Count - 1;
                        int di = INT(Data[a + 1]);
                        //  Spielfeld.players[besitzer].fuelRemains2[pos] = 1000;
                        Spielfeld.players[besitzer].KindofTank[pos] = di;
                        Spielfeld.players[besitzer].Farbe = Spielerfarben[besitzer];
                        Spielfeld.players[besitzer].pos[pos] = new Vector2(FLOAT(Data[a + 2]), FLOAT(Data[a + 3]));
                        Spielfeld.players[besitzer].hp[pos] = INT(Data[a + 4]);//Tankdata.MaxHP[di];
                        Spielfeld.players[besitzer].Angle[pos] = FLOAT(Data[a + 5]);
                        Spielfeld.players[besitzer].vehikleAngle[pos] = FLOAT(Data[a + 6]);
                        Spielfeld.players[besitzer].overreach[pos] = System.Convert.ToBoolean(Data[a + 7]);
                        //Spielfeld.players[besitzer].freezed[pos] = INT(Data[a + 8]);
                        //Spielfeld.players[besitzer].ExpProgress[pos] = INT(Data[a + 9]);
                        Spielfeld.players[besitzer].ExpNow[pos] = INT(Data[a + 10]);
                        Spielfeld.players[besitzer].CurrentLv[pos] = INT(Data[a + 11]);
                        Spielfeld.players[besitzer].isthere[pos] = System.Convert.ToBoolean(Data[a + 12]);
                        Spielfeld.players[besitzer].Rucksack[pos].Treibstoff = FLOAT(Data[a + 13]);
                        Spielfeld.players[besitzer].Cooldown[pos] = INT(Data[a + 14]);

                        for (int t = 0; t < Spielfeld.players[q].Munition[pos].Count(); t++)
                        {
                            list.Add(Spielfeld.players[q].Munition[pos][t].ToString());
                            Spielfeld.players[q].Munition[pos][t] = INT(Data[a + 15]);
                            a++;
                        }

                        Spielfeld.players[besitzer].Size[pos] = (float)Fahrzeugdaten.SCALEP.Wert[di];
                        Spielfeld.players[besitzer].SizeOfCannon[pos] = Fahrzeugdaten.SCALER.Wert[di];

                        if (Path.GetFileName(Hauptfenster.Tausch.Data) == "OldData.dat" && q == Hauptfenster.Tausch.CurrentPlayer)
                        {
                            //if (Spielfeld.players[besitzer].freezed[pos] > 0) Spielfeld.players[besitzer].freezed[pos]--;
                            // Spielfeld.players[besitzer].gift_anwenden(pos);
                        }

                        Spielfeld.players[besitzer].shootingPower = 2f;
                        Spielfeld.players[besitzer].MaxTimeout = 180 * 60;
                    }
                    // Spielfeld.players[besitzer].load_panzerdaten();
                }
            }

            Game1.LadeText("    Karte...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 90;
            // Karte
            {
                int a = FindBegin("Karte", Data); if (a == -1) { Spielfeld = null; return; } a++;
                int anz = Kartengroesse;
                for (int i = 0; i < anz; i++)
                {
                    string[] dat = Data[a + i].Split('-');
                    for (int b = 0; b < dat.Count(); b++) Spielfeld.Spielfeld[i].Add((UInt16)(INT(dat[b])));
                    int c = Spielfeld.Spielfeld[i].Count;
                    if (screen.Y > Kartenhoehe) Spielfeld.Spielfeld[i][c - 1] = (UInt16)(Spielfeld.Spielfeld[i][c - 1] + (int)(screen.Y - Kartenhoehe));
                }
            }

            // Spielumgebung
            Replay.Begin(Spielfeld.players);
            if (HTTP.HTTP.gameid != "")
            {
                if (Replay.REPLAY_VISIBLE.Wert && _Data != "")
                {
                    Replay.Laden(_Data, (Hauptfenster.Tausch.CurrentPlayer + 1) % Spielfeld.players.Count(), false);
                    if (Path.GetFileName(_Data) == "Data.dat")
                    {
                        Replay.Laden(_Data, Hauptfenster.Tausch.CurrentPlayer, true);
                    }
                }
            }
            // if (Spiel.WIND.Wert)Spielfeld.Wind.X = Help.rnd.Next(-60, 60) / 10;
            if (Hauptfenster.Tausch.CurrentPlayer != Spielfeld.CurrentPlayer)
            {
                if (Spiel.SCHUESSE.Wert) Spielfeld.Schuesse = Spielfeld.players[Spielfeld.CurrentPlayer].MaxSchuesse;
                if (Spiel.WIND.Wert) Spielfeld.Wind.X = Help.rnd.Next(-60, 60) / 10;
            }

            Feuer.Initialisieren(Spielfeld.Spielfeld.Count());
            Hauptfenster.Program.Formular.progressBar1.Value = 100;
            Hauptfenster.Program.Formular.progressBar1.Hide();
            Hauptfenster.Program.Formular.label31.Hide();
        }
    }
}