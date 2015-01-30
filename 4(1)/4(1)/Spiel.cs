// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-25-2013
// ***********************************************************************
// <copyright file="Spiel.cs" company="">
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

namespace _4_1_
{
    /// <summary>
    ///     Class Spiel
    /// </summary>
    public class Spiel
    {
        #region Fields

        /// <summary>
        ///     The ACTIO n_ POINTS
        /// </summary>
        public static Var<bool> ACTION_POINTS = new Var<bool>("ACTION_POINTS", false);

        /// <summary>
        ///     The ACTIO n_ POINTS
        /// </summary>
        public static Var<int> ACTION_POINTS_INCREASE_CONST = new Var<int>("ACTION_POINTS_INCREASE_CONST", 0);

        /// <summary>
        ///     The ACTIO n_ POINTS
        /// </summary>
        public static Var<int> ACTION_POINTS_MAX = new Var<int>("ACTION_POINTS_MAX", 0);

        /// <summary>
        ///     The AIRSTRIKE
        /// </summary>
        public static Var<bool> AIRSTRIKE = new Var<bool>("AIRSTRIKE", false);

        /// <summary>
        ///     The CHEC k_ HAEUSER
        /// </summary>
        public static Var<bool> CHECK_HAEUSER = new Var<bool>("CHECK_HAEUSER", false);

        /// <summary>
        ///     The CHEC k_ RAKETEN
        /// </summary>
        public static Var<bool> CHECK_RAKETEN = new Var<bool>("CHECK_RAKETEN", false);

        /// <summary>
        ///     The CHEC k_ WIND
        /// </summary>
        public static Var<bool> CHECK_WIND = new Var<bool>("CHECK_WIND", false);

        /// <summary>
        ///     The CREDITS
        /// </summary>
        public static Var<bool> CREDITS = new Var<bool>("CREDITS", false);

        /// <summary>
        ///     The EXPLOSION
        /// </summary>
        public static Var<bool> EXPLOSION = new Var<bool>("EXPLOSION", false);

        /// <summary>
        ///     The kartenbreite
        /// </summary>
        public static int Kartenbreite = 0;

        /// <summary>
        ///     The MISSILE
        /// </summary>
        public static Var<bool> MISSILE = new Var<bool>("MISSILE", false);

        /// <summary>
        ///     The MISSIL e_ PLAYE r_ COLO r_ VISIBLE
        /// </summary>
        public static Var<bool> MISSILE_PLAYER_COLOR_VISIBLE = new Var<bool>("MISSILE_PLAYER_COLOR_VISIBLE", false);

        /// <summary>
        ///     The MUNITION
        /// </summary>
        public static Var<bool> MUNITION = new Var<bool>("MUNITION", false);

        /// <summary>
        ///     The POISO n_ ROUND
        /// </summary>
        public static Var<bool> POISON_ROUND = new Var<bool>("POISON_ROUND", false);

        /// <summary>
        ///     The POISO n_ TIME
        /// </summary>
        public static Var<bool> POISON_TIME = new Var<bool>("POISON_TIME", false);

        /// <summary>
        ///     The rand
        /// </summary>
        public static Random rand = new Random();

        /// <summary>
        ///     The SCHUESSE
        /// </summary>
        public static Var<bool> SCHUESSE = new Var<bool>("SCHUESSE", false);

        /// <summary>
        ///     The SMOKE
        /// </summary>
        public static Var<bool> SMOKE = new Var<bool>("SMOKE", false);

        /// <summary>
        ///     The TIMEOUT
        /// </summary>
        public static Var<bool> TIMEOUT = new Var<bool>("TIMEOUT", false);

        /// <summary>
        ///     The TIMEOUT
        /// </summary>
        public static Var<bool> TIMEOUT_VISIBLE = new Var<bool>("TIMEOUT_VISIBLE", false);

        /// <summary>
        ///     The TIMEOUT
        /// </summary>
        public static Var<bool> TIMEOUT_SPIELERWECHSEL = new Var<bool>("TIMEOUT_SPIELERWECHSEL", true);

        /// <summary>
        ///     The TIMEOUT_REDUZIEREN_BEIM_FAHREN
        /// </summary>
        public static Var<int> TIMEOUT_REDUZIEREN_BEIM_FAHREN = new Var<int>("TIMEOUT_REDUZIEREN_BEIM_FAHREN", 5);

        /// <summary>
        ///     The TIMEOUT_SEKUNDEN
        /// </summary>
        public static Var<int> TIMEOUT_SEKUNDEN = new Var<int>("TIMEOUT_SEKUNDEN", 60);

        /// <summary>
        ///     The WIND
        /// </summary>
        public static Var<bool> WIND = new Var<bool>("WIND", false);

        /// <summary>
        ///     The baeume
        /// </summary>
        public Baum Baeume = new Baum();

        /// <summary>
        ///     The bunker
        /// </summary>
        public Bunker Bunker = new Bunker();

        /// <summary>
        ///     The colors
        /// </summary>
        public Color[] Colors;

        /// <summary>
        ///     The current missile
        /// </summary>
        public int CurrentMissile;

        /// <summary>
        ///     The current player
        /// </summary>
        public int CurrentPlayer = 0;

        /// <summary>
        ///     The fenster
        /// </summary>
        public Vector2 Fenster;

        /// <summary>
        ///     The fog colors
        /// </summary>
        public Color[] fogColors;

        /// <summary>
        ///     The foreground
        /// </summary>
        public Texture2D[] foreground;

        //  public List<Vector2> BunkerPos = new List<Vector2>();
        //  public List<int> BunkerHp = new List<int>();
        //   public List<int> BunkerMaxHp = new List<int>();
        /// <summary>
        ///     The foreground colors
        /// </summary>
        public Color[][] foregroundColors;

        /// <summary>
        ///     The haeuser
        /// </summary>
        public Haus Haeuser = new Haus();

        /// <summary>
        ///     The height
        /// </summary>
        public int Height;

        /// <summary>
        ///     The hoehlen
        /// </summary>
        public Höhlenkonfiguration hoehlen = null;

        /// <summary>
        ///     The increaseairstrike
        /// </summary>
        public bool increaseairstrike = false;

        //public int SpielerID = 0; // ??? nur ein Spieler lebend
        /// <summary>
        ///     The increaseshot
        /// </summary>
        public bool increaseshot = false;

        /// <summary>
        ///     The is server
        /// </summary>
        public bool IsServer = true;

        /// <summary>
        ///     The karte
        /// </summary>
        public Karte Karte;

        /// <summary>
        ///     The kisten
        /// </summary>
        public Kiste Kisten = new Kiste();

        /// <summary>
        ///     The leftsidet
        /// </summary>
        public bool leftsidet = false;

        // airstrike
        /// <summary>
        ///     The minimap_visible
        /// </summary>
        public bool minimap_visible = true;

        /// <summary>
        ///     The missile
        /// </summary>
        public Waffen[] Missile;

        // airstrike
        /// <summary>
        ///     The moving_ map
        /// </summary>
        public bool Moving_Map = false;

        //  public Color[] Nebelkreis;
        /// <summary>
        ///     The nebelkreis
        /// </summary>
        /// <summary>
        ///     The next_ fenster
        /// </summary>
        public Vector2 Next_Fenster;

        /// <summary>
        ///     The paused
        /// </summary>
        public bool paused = false;

        //public List<int>[] Nebelkreis;
        /// <summary>
        ///     The players
        /// </summary>
        public Spieler[] players;

        /// <summary>
        ///     The replay_visible2
        /// </summary>
        public bool replay_visible2 = true;

        /// <summary>
        ///     The schuesse
        /// </summary>
        public int Schuesse = 0;

        /// <summary>
        ///     The sieger
        /// </summary>
        public int Sieger = -1;

        /// <summary>
        ///     The spielfeld
        /// </summary>
        public List<UInt16>[] Spielfeld;

        /// <summary>
        ///     The timeout
        /// </summary>
        public int Timeout = 0;

        //   public bool vehikleMoving = false;
        /// <summary>
        ///     The vehikle moving
        /// </summary>
        /// <summary>
        ///     The wait_change
        /// </summary>
        public int wait_change = 0;

        /// <summary>
        ///     The width
        /// </summary>
        public int Width;

        /// <summary>
        ///     The wind
        /// </summary>
        public Vector2 Wind;

        /// <summary>
        ///     The wind timeout
        /// </summary>
        public int WindTimeout = 0;

        /// <summary>
        ///     The incshoot2
        /// </summary>
        private static int incshoot2;

        /// <summary>
        ///     The incshoot3
        /// </summary>
        private static int incshoot3;

        /// <summary>
        ///     auswahl mit Namen
        /// </summary>
        public static List<String> names;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Spiel" /> class.
        /// </summary>
        /// <param name="Kartengroesse">The kartengroesse.</param>
        /// <param name="screen">The screen.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public Spiel(int Kartengroesse, Vector2 screen)
        {
            Init(Kartengroesse, screen);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Spiel" /> class.
        /// </summary>
        public Spiel()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets the incshoot.
        /// </summary>
        /// <value>The incshoot.</value>
        public static int incshoot
        {
            get
            {
                incshoot2++;
                incshoot2 %= 5;
                incshoot3++;
                incshoot3 %= 9;
                return incshoot3 * 10 + incshoot2 * 2;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     INTs the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Int32.</returns>
        public static int INT(String data)
        {
            return Convert.ToInt32(data);
        }

        /// <summary>
        ///     Positions the specified pos.
        /// </summary>
        /// <param name="Pos">The pos.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 Position(Vector2 Pos)
        {
            if (Pos.X < 0) return new Vector2(Kartenbreite + Pos.X, Pos.Y);
            if (Pos.X >= Kartenbreite) return new Vector2(Pos.X - Kartenbreite, Pos.Y);
            return Pos;
        }

        /// <summary>
        ///     Positions the specified pos.
        /// </summary>
        /// <param name="Pos">The pos.</param>
        /// <returns>System.Single.</returns>
        public static float Position(float Pos)
        {
            if (Karte.KARTE_SYMMETRISCH)
            {
                if (Pos < 0) return Kartenbreite + Pos;
                if (Pos >= Kartenbreite) return Pos - Kartenbreite;
            }
            else
            {
                if (Pos < 0) return 0;
                if (Pos >= Kartenbreite) return Kartenbreite-1;
            }

            return Pos;
        }

        /// <summary>
        ///     Adds the bunker.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="pos">The pos.</param>
        public void AddBunker(int Spieler, Vector2 pos)
        {
            var breite = (int)(Texturen.bunker[0].Width * Optimierung.Skalierung(0.25f));
            int id = Spieler;
            Bunker.Hinzufügen(new Vector2(pos.X - breite / 2, pos.Y), id);
        }

        /// <summary>
        ///     Adds the geschütz.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="pos">The pos.</param>
        public void AddGeschütz(int Spieler, Vector2 pos)
        {
            AddPanzer(Spieler, Fahrzeugdaten.GESCHÜTZ, 0, false, pos);
        }

        /// <summary>
        ///     Adds the geschütz2.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="pos">The pos.</param>
        public void AddGeschütz2(int Spieler, Vector2 pos)
        {
            AddPanzer(Spieler, Fahrzeugdaten.GESCHÜTZ2, 0, false, pos);
        }

        /// <summary>
        ///     Adds the panzer.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="typ">The typ.</param>
        /// <param name="_angle">The _angle.</param>
        /// <param name="_overreach">if set to <c>true</c> [_overreach].</param>
        /// <param name="_pos">The _pos.</param>
        public void AddPanzer(int Spieler, int typ, float _angle, bool _overreach, Vector2 _pos)
        {
            // Spielername holen
            if (Spieler < 0 || Spieler >= players.Count()) return;

            if (names == null || names.Count == 0)
            {
                if (File.Exists("Content\\Konfiguration\\Namen.txt"))
                {
                    names = new List<string>();

                    var datei = new ReaderStream.ReaderStream("Content\\Konfiguration\\Namen.txt");

                    while (!datei.EndOfStream)
                    {
                        names.Add(datei.ReadLine());
                    }

                    datei.Close();
                }
            }

            if (players[Spieler].pos.Count >= players[Spieler].Munition.Count()) return;

            if (names == null)
            {
                players[Spieler].Namen.Add("Joe");
            }
            else
            {
                int q = rand.Next(0, names.Count);
                players[Spieler].Namen.Add(names[q]);
                names.RemoveAt(q);
            }

            int di = typ;
            players[Spieler].hp.Add(Fahrzeugdaten._MAXHP.Wert[di]);
            int b = players[Spieler].hp.Count - 1;
            players[Spieler].Angle.Add(_angle);
            players[Spieler].vehikleAngle.Add(0);
            players[Spieler].isthere.Add(true);
            players[Spieler].overreach.Add(_overreach);
            players[Spieler].Size.Add(Fahrzeugdaten.SCALEP.Wert[di]);
            players[Spieler].SizeOfCannon.Add(Fahrzeugdaten.SCALER.Wert[di]);
            players[Spieler].pos.Add(_pos);
            players[Spieler].oldpos.Add(_pos);
            players[Spieler].KindofTank.Add(di);
            players[Spieler].ExpNow.Add(0);
            players[Spieler].CurrentLv.Add(0);
            players[Spieler].Cooldown.Add(0);
            players[Spieler].Effekte.Add(new EffectPacket());
            players[Spieler].Zielpos.Add(new Vector2(0, -9999));
            players[Spieler].logik.Add(new Fahrlogik_Object(5));
            for (int d = 0; d < Texturen.Radpositionen[di].Count(); d++)
            {
                players[Spieler].logik[players[Spieler].logik.Count - 1].Motoren.AddRad(Texturen.Radpositionen[di][d],
                    Texturen.panzerindexreifen[di]);
            }

          players[Spieler].Munition[b] = new List<int>();
            for (int d = 0; d < Waffendaten.Daten.Count(); d++)
          {
              players[Spieler].Munition[b].Add(99);
          }

          players[Spieler].Munition[b][Waffendaten.mg] = 999;

            players[Spieler].Rucksack.Add(new Inventar(15000, players[Spieler].Munition[b], 1000));

            players[Spieler].LadeKollisionsObjekt(b);
            players[Spieler].LadeZerstörungsObjekt(b);

            players[Spieler].Rucksack[b].Hinzufügen(Inventar.Neu(Itemdata.Verteidiung1));
            players[Spieler].Rucksack[b].Hinzufügen(Inventar.Neu(Itemdata.Verbrauch1));
            players[Spieler].Rucksack[b].Hinzufügen(Inventar.Neu(Itemdata.Ziel1));
            players[Spieler].Rucksack[b].Hinzufügen(Inventar.Neu(Itemdata.Lager1));
            players[Spieler].Rucksack[b].Hinzufügen(Inventar.Neu(Itemdata.Arbeitsbereich1));
            players[Spieler].Rucksack[b].Hinzufügen(Inventar.Neu(Itemdata.Heilen));
        }

        /// <summary>
        ///     Adds the rakete.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="Direction">The direction.</param>
        /// <param name="Lebensdauer">The lebensdauer.</param>
        /// <param name="Weapon">The weapon.</param>
        /// <param name="CurrentTank">The current tank.</param>
        /// <returns>System.Int32.</returns>
        public int AddRakete(int Spieler, Vector2 pos, Vector2 Direction, int Lebensdauer, int Weapon, int CurrentTank)
        // Fügt eine Rakete zum Spiel hinzu  // geändert
        {
            Soundsystem[] sh = Sounds.Shots;

            for (int i = 0; i < Missile.Length; i++)
            {
                if (Missile[i].verzoegerung > 0) continue;
                if (Missile[i].Besitzer[0] == -1)
                {
                    int dist;
                    if (Spieler <= 2)
                    {
                        dist = (int)(players[Spieler].pos[players[Spieler].CurrentTank].X - (Fenster.X));
                        if (dist < 0) dist = -dist;
                    }
                    else
                        dist = 0; // ?????

                    //sh[Waffendaten.Abschuesse[Weapon]].Play((float)(1.0d - ((double)dist / Spielfeld.Length)), 0.0f, 0.0f);
                    sh[Waffendaten.Abschuesse[Weapon]].PlaySoundAny(false,
                        (float)(1.0d - ((double)dist / Spielfeld.Length)));
                    Missile[i].Energie = (int)Waffendaten.Daten[Weapon].X;
                    Missile[i].misslePosition = pos;
                    Missile[i].Besitzer[0] = Spieler;
                    Missile[i].Besitzer[1] = CurrentTank;
                    Missile[i].Lebensdauer = Lebensdauer;
                    Missile[i].missleDirection = Direction;
                    Missile[i].missleShot = true;
                    Missile[i].Art = Weapon;
                    Missile[i].verzoegerung = 0;
                    //Missile[i].missleScaling = Rakete.Scale[Weapon];

                    for (int b = 0; b < Missile[i].Last_Position.Length; b++)
                    {
                        Missile[i].Last_Position[b] = new Vector2(-99, 99);
                    }
                    CurrentMissile = i;
                    return i;
                }
            }
            return -1; // Rakete hinzufügen
        }

        /// <summary>
        ///     Adds the tunnel.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="pos">The pos.</param>
        public void AddTunnel(int Spieler, Vector2 pos)
        {
            if (Spieler < 0 || Spieler >= players.Count()) return;
            players[Spieler].TunnelAnlage.Add(new Tunnel(pos));
        }

        /// <summary>
        ///     löst einen Airstrike aus
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="Spieler">The spieler.</param>
        public void Airstrike(Vector2 pos, int Spieler)
        {
            if (Client.isRunning) return;
            var q = new int[9]; // anzahl der raketen, ungerade anzahl benötigt
            for (int i = -(q.Length / 2); i <= (q.Length / 2); i++)
            {
                // Abstand
                q[i + (q.Length / 2)] = AddRakete(Spieler, new Vector2(pos.X + (i * 80), rand.Next(-1100, -200)),
                    new Vector2(rand.Next(-100, 100) / 25, -1), 300 * 4, 5, players[CurrentPlayer].CurrentTank);
            }

            CurrentMissile = q[(q.Length / 2)];
            if (CurrentMissile != -1) Missile[q[(q.Length / 2)]].focused = true;
        }

        /// <summary>
        ///     Prüft die Cooldowns der Waffen
        /// </summary>
        public void check_Cooldowns()
        {
            for (int i = 0; i < players.Length; i++)
            {
                for (int b = 0; b < players[i].pos.Count; b++)
                {
                    if (!players[i].isthere[b]) continue;
                    //if (players[i].freezed[b] > 0) { players[i].freezed[b]--; continue; }
                    if (Spieler.WAFFEN_COOLDOWN.Wert && players[i].Cooldown[b] > 0) players[i].Cooldown[b]--;
                }
            }
        }

        /// <summary>
        ///     prüft ob zielposition des Fensters erreicht, bewegt fenster notfalls
        /// </summary>
        public void check_Focus() 
        {
            if (Next_Fenster.X != Fenster.X)
            {
                var add = (int)(Next_Fenster.X - Fenster.X);
                if (add < 0) add = -add;

                if (!Moving_Map)
                {
                    if (add < 100)
                    {
                        if (add > 14)
                        {
                            add = 14;
                        }
                    }
                    else if (add < 500)
                    {
                        if (add > 14)
                        {
                            add = 25;
                        } // 14,14
                    }
                    else if (add < 1000)
                    {
                        if (add > 20)
                        {
                            add = 35;
                        } // 20,21
                    }
                    else if (add > 25)
                    {
                        add = 50;
                    } // 35,35
                }

                if (Next_Fenster.X > Fenster.X)
                {
                    Fenster.X += add;
                }
                else
                {
                    Fenster.X -= add;
                }
            }


            if (Next_Fenster.Y != Fenster.Y)
            {
                var add = (int)(Next_Fenster.Y - Fenster.Y);
                if (add < 0) add = -add;

                if (!Moving_Map)
                {
                    if (add < 100)
                    {
                        if (add > 14)
                        {
                            add = 14;
                        }
                    }
                    else if (add < 500)
                    {
                        if (add > 14)
                        {
                            add = 25;
                        } // 14,14
                    }
                    else if (add < 1000)
                    {
                        if (add > 20)
                        {
                            add = 35;
                        } // 20,21
                    }
                    else if (add > 25)
                    {
                        add = 50;
                    } // 35,35
                }

                if (Next_Fenster.Y > Fenster.Y)
                {
                    Fenster.Y += add;
                }
                else
                {
                    Fenster.Y -= add;
                }
            }
        }

        /// <summary>
        ///     Check_haeusers this instance.
        /// </summary>
        public void check_haeuser()
        {
            /*
            if (Client.isRunning) return;
            for (int i = 0; i < Haeuser.Haeuser.Count; i++)
            {
                int[] anz = new int[players.Length];
                int breite = (int)(Texturen.bunker[0].Width * 0.2f);
                for (int b = 0; b < players.Length; b++)
                {
                    for (int c = 0; c < players[b].pos.Count; c++)
                    {
                        if (Haeuser.IsCollision(i, players[b].pos[c] - new Vector2(0, 5)))
                        {
                            anz[b]++;
                        }
                    }
                }
                // gezählt
                List<int> max = new List<int>();
                int m = 0;
                for (int b = 0; b < players.Length; b++)
                    if (anz[b] > m) m = anz[b];

                if (m > 0)
                {
                    for (int b = 0; b < players.Length; b++)
                    {
                        if (anz[b] == m) max.Add(b);
                    }
                }

                if (max.Count > 1)
                {
                    Haeuser.Besitzer[i] = -1;
                }
                else
                    if (max.Count == 1)
                    {
                        Haeuser.Besitzer[i] = max[0];
                    }
                /// if (Server.isRunning) Server.Send("HAUSBESITZER " + i + " " + Haeuser.Besitzer[i]);/////////////////////// changed
            }*/
        }

        /// <summary>
        ///     Check_s the minen.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void check_Minen(GameTime gameTime)
        {
            int i = CurrentPlayer;
            //  int b = players[i].CurrentTank;
            for (int c = 0; c < players.Count(); c++)
            {
                if (c == i) continue;
                for (int d = 0; d < players[c].Minen.Count; d++)
                {
                    bool fahrzeugkollision = false;
                    for (int b = 0; b < players[i].pos.Count(); b++)
                    {
                        if (players[c].Minen[d].PrüfeObKollision(players[i].pos[b] + new Vector2(0, -2)))
                        {
                            fahrzeugkollision = true;
                        }
                    }

                    if (players[c].Minen[d].Verzoegerung != 0 && !players[c].Minen[d].Aktiv && !fahrzeugkollision)
                    {
                        // Fahrzeug steht auf Mine
                        Sounds.Mine_klick[rand.Next(0, 2)].PlaySoundAny();
                        players[c].Minen[d].Aktiv = false;
                        players[c].Minen[d].Verzoegerung = 0;

                        /* int id = players[c].Minen[d].ID;
                         Vordergrund.AktualisiereVordergrund(players[c].Minen[d].ZündeMine(Spielfeld, gameTime, this));

                         for (int e = 0; e < players[c].Minen.Count; e++)
                             if (players[c].Minen[e].ID == id)
                             {
                                 players[c].Minen.RemoveAt(e);
                                 break;
                             }*/
                        //    players[c].Minen.RemoveAt(d);
                        d = 0;
                    }

                    if (!players[c].Minen[d].Aktiv) continue;
                    if (fahrzeugkollision)
                    {
                        // Fahrzeug steht auf Mine
                        Sounds.Mine_klick[rand.Next(0, 2)].PlaySoundAny();

                        // Foreground.Update_CreateForeground(players[c].Minen[d].Explosion(Spielfeld,gameTime,this));
                        players[c].Minen[d].Aktiv = false;
                        players[c].Minen[d].Verzoegerung = Allgemein.Minenverzögerung;

                        /* int id = players[c].Minen[d].ID;
                         Vordergrund.AktualisiereVordergrund(players[c].Minen[d].ZündeMine(Spielfeld, gameTime, this));

                         for (int e = 0; e < players[c].Minen.Count; e++)
                             if (players[c].Minen[e].ID == id)
                             {
                                 players[c].Minen.RemoveAt(e);
                                 break;
                             }*/
                        //    players[c].Minen.RemoveAt(d);
                        d = 0;
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     Check_playerses the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void check_players(GameTime gameTime)
        // prüfe timeout der Spieler und ob spieler tot/aus spielfeld gefallen
        {
            //  if (Client.isRunning) return;
            if (TIMEOUT.Wert)
            {
                if (Timeout > 0)
                {
                    Timeout--;
                }
                else
                {
                    if (wait_change == 0 || !Spiel.TIMEOUT_SPIELERWECHSEL.Wert) Spielerwechsel();
                }
                // if (Server.isRunning) Server.Send("TIMEOUT " + Timeout);
            }
            else if (SCHUESSE.Wert)
            {
                if (Schuesse <= 0)
                {
                    if (wait_change == 0 || !Spiel.TIMEOUT_SPIELERWECHSEL.Wert) Spielerwechsel();
                }
                // if (Server.isRunning) Server.Send("TIMEOUT " + Timeout);
            }

            for (int i = 0; i < players.Length; i++)
            {
                for (int b = 0; b < players[i].pos.Count; b++)
                {
                    if (!players[i].isthere[b]) continue;

                    // Feuer
                    if (Feuer.check_Feuer((int)players[i].pos[b].X, (int)players[i].pos[b].Y - 2))
                        players[i].hp[b] -= players[i].Effekte[b].GibFeuerSchaden(10);

                    // automatisches Fahren
                    if (players[i].Zielpos[b].Y != -9999)
                    {
                        if (players[i].Zielpos[b].X > players[i].pos[b].X + 2)
                        {
                            players[i].overreach[b] = true;
                            float ang = MathHelper.ToDegrees(players[i].Angle[b]);
                            if (ang < 90) players[i].Angle[b] = MathHelper.ToRadians((90 - ang) + 90);
                            Rechts(i, b);
                        }
                        else if (players[i].Zielpos[b].X < players[i].pos[b].X - 2)
                        {
                            players[i].overreach[b] = false;
                            float ang = MathHelper.ToDegrees(players[i].Angle[b]);
                            if (ang > 90) players[i].Angle[b] = MathHelper.ToRadians(90 - (ang - 90));
                            Links(i, b);
                        }
                        else
                            players[i].Zielpos[b] = new Vector2(0, -9999);
                    }
                    // automatisches Fahren prüfen

                    float move = Fahrzeugdaten.FALLG.Wert[players[i].KindofTank[b]];
                    ;
                    if (players[i].pos[b].Y > Kartenformat.BottomOf(players[i].pos[b]))
                    {
                        players[i].Kenngroesse_Wert.Hinzufügen(players[i].pos[b], -Fahrzeugdaten._PANZERWERTE.Wert[players[i].KindofTank[b]], 350,Anteil.Fläche,Wachstum.LinearFallend,false);
                        float diff = players[i].pos[b].Y - Kartenformat.BottomOf(players[i].pos[b]);
                        if (diff > move) diff = move;
                        Vector2 a = players[i].pos[b];
                        a.Y -= diff;
                        players[i].pos[b] = a;
                        players[i].Kenngroesse_Wert.Hinzufügen(players[i].pos[b], Fahrzeugdaten._PANZERWERTE.Wert[players[i].KindofTank[b]], 350, Anteil.Fläche, Wachstum.LinearFallend, false);
                        // if (Server.isRunning) Server.Send("POS " + i + " " + b + " " + players[i].pos[b].X + " " + players[i].pos[b].Y);
                    }
                    else if (players[i].pos[b].Y < Kartenformat.BottomOf(players[i].pos[b]))
                    {
                        players[i].Kenngroesse_Wert.Hinzufügen(players[i].pos[b], -Fahrzeugdaten._PANZERWERTE.Wert[players[i].KindofTank[b]], 350, Anteil.Fläche, Wachstum.LinearFallend, false);
                        float diff = Kartenformat.BottomOf(players[i].pos[b]) - players[i].pos[b].Y;
                        if (diff > move) diff = move;
                        Vector2 a = players[i].pos[b];
                        a.Y += diff;
                        players[i].pos[b] = a;
                        players[i].Kenngroesse_Wert.Hinzufügen(players[i].pos[b], Fahrzeugdaten._PANZERWERTE.Wert[players[i].KindofTank[b]], 350, Anteil.Fläche, Wachstum.LinearFallend, false);
                        //if (Server.isRunning)
                        //   Server.Send("POS " + i + " " + b + " " + players[i].pos[b].X + " " + players[i].pos[b].Y);
                    }

                    if (players[i].Effekte[b].GetHP(players[i].hp[b]) <= 0)
                    {
                        players[i].isthere[b] = false;
                        // if (Server.isRunning) Server.Send("ISTHERE " + i + " " + b + " false");
                    }
                    else if (players[i].pos[b].Y >= Height)
                    {
                        Sounds.PanzerMove[Fahrzeugdaten.SINKSOUND.Wert[players[i].KindofTank[b]]].StopSound(b);
                        Sounds.sink[Fahrzeugdaten.SINKSOUND.Wert[players[i].KindofTank[b]]].PlaySoundAny();
                        players[i].isthere[b] = false;
                        if (Server.isRunning) Server.Send("ISTHERE " + i + " " + b + " false");
                    }
                    //    if (players[i].pos[b].Y >= Height) { Sounds.PanzerMove2[Fahrzeugdaten.SINKSOUND.Wert[players[i].KindofTank[b]]].Stop(); Sounds.sink[Fahrzeugdaten.SINKSOUND.Wert[players[i].KindofTank[b]]].Play(); players[i].isthere[b] = false; if (Server.isRunning) Server.Send("ISTHERE " + i + " " + b + " false"); }

                    //berechne Erfahrung
                    if (players[i].CurrentLv[b] < 3)
                    {
                        float auxep = players[i].ExpNow[b];
                        float auxep2 = players[i].KindofTank[b];
                        float abc = players[i].CurrentLv[b];
                        float abcd = Fahrzeugdaten.ExpToLvUpVar[players[i].KindofTank[b], players[i].CurrentLv[b]];

                        players[i].ExpNow[b] = players[i].ExpNow[b] * 100 /
                                               Fahrzeugdaten.ExpToLvUpVar[players[i].KindofTank[b], players[i].CurrentLv[b]
                                                   ];
                        if (players[i].ExpNow[b] >=
                            Fahrzeugdaten.ExpToLvUpVar[players[i].KindofTank[b], players[i].CurrentLv[b]])
                        {
                            players[i].ExpNow[b] -=
                                Fahrzeugdaten.ExpToLvUpVar[players[i].KindofTank[b], players[i].CurrentLv[b]];
                            players[i].CurrentLv[b]++;
                            Sounds.punkteerhalten.StopSound(0);
                            Sounds.levelup.PlaySoundAny();
                            Kurzmeldung.Hinzufügen("+Rang", players[i].pos[b] + new Vector2(0, 15), Color.Green, -300);
                        }
                    }
                    else
                        players[i].ExpNow[b] = 0;

                    if (players[i].isthere[b] == false)
                    {
                        // löschen
                        Replay.DeletedPlayer(players, i, b);
                        players[i].Entfernen(b, true, this, gameTime);
                    }

                    // if (!players[i].isthere[b] && CurrentPlayer == i) next_player();
                }
            }
        }

        /// <summary>
        ///     Check_playerwinkels this instance.
        /// </summary>
        public void check_playerwinkel()
        {
            if (Client.isRunning) return;
            if (Texturen.panzerindex[0] == null) return;
            for (int i = 0; i < players.Length; i++)
            {
                for (int b = 0; b < players[i].pos.Count; b++)
                {
                    if (players[i].pos[b].X < 0 || players[i].pos[b].X > Spielfeld.Length - 2) continue;

                    int Kindof = players[i].KindofTank[b];
                    /*
                    if (Texturen.Radpositionen[Kindof].Count()>=2){
                        Vector2[] Vektoren = new Vector2[2];
                    for (int d = 0; d < Texturen.Radpositionen[Kindof].Count(); d++)
                    {
                        float vehikleAngle = players[i].vehikleAngle[b];
                        bool overreach = players[i].overreach[b];
                        Vector2 add = players[i].logik[b].GetBewegungsVektor(d, players[i].vehikleAngle[b], players[i].pos[b], players[i].overreach[b]);
                        Vektoren[d] = add;
                    }*/

                    // Bewegen
                    /* if (players[i].overreach[b])
                     {
                         Vector2 temp = Vektoren[0];
                         Vektoren[0] = Vektoren[1];
                         Vektoren[1] = temp;
                     }*/

                    /*Vector2 target;

                        // X bewegung
                        float x = Help.GleicherAnteil(Vektoren[0].X,Vektoren[1].X);
                        if (Math.Abs(x) >1)
                        {
                             x = (float) (Math.Log(x, Math.E));
                            if (x>0 && x < 0.1f) x = 0.1f;
                           if (x < 0 && x > -0.1f) x = -0.1f;
                             if (x != float.NaN)
                             {
                                 // float geschv = Tankdata.GeschwV[Kindof];
                                 target = players[i].pos[b] + new Vector2(x, 0);// new Vector2(x > geschv ? geschv : x < -geschv ? -geschv : x, 0);
                                 players[i].pos[b] = target;
                                 players[i].logik[b].Motoren.Raeder[0].Energie -= new Vector2(x, 0);
                                 players[i].logik[b].Motoren.Raeder[1].Energie -= new Vector2(x, 0);
                             }
                        }*/

                    /*  x = Help.RestAnteil(Vektoren[0].X, Vektoren[1].X,0);
                      // x = (float) (Math.Log(x, Math.E));
                      // float geschv = Tankdata.GeschwV[Kindof];
                      target = target + new Vector2(x, 0);// new Vector2(x > geschv ? geschv : x < -geschv ? -geschv : x, 0);
                      players[i].logik[b].Motoren.Raeder[0].Energie -= new Vector2(x, 0);
                      players[i].logik[b].Motoren.Raeder[1].Energie -= new Vector2(x, 0);

                      x = Help.RestAnteil(Vektoren[0].X, Vektoren[1].X, 1);
                      // x = (float) (Math.Log(x, Math.E));
                      // float geschv = Tankdata.GeschwV[Kindof];
                      target = target + new Vector2(x, 0);// new Vector2(x > geschv ? geschv : x < -geschv ? -geschv : x, 0);
                      players[i].pos[b] = target;
                      players[i].logik[b].Motoren.Raeder[0].Energie -= new Vector2(x, 0);
                      players[i].logik[b].Motoren.Raeder[1].Energie -= new Vector2(x, 0);*/

                    /*
                    // Y bewegung
                   float y = Help.GleicherAnteil(Vektoren[0].Y, Vektoren[1].Y);
                 //   geschv = 1.0f;
                   target = players[i].pos[b] + new Vector2(0, y);// new Vector2(0, y > geschv ? geschv : y < -geschv ? -geschv : y);
                    players[i].pos[b] = target;
                    players[i].logik[b].Motoren.Raeder[0].Energie -= new Vector2(0,y);
                    players[i].logik[b].Motoren.Raeder[1].Energie -= new Vector2(0,y);

                    y = Help.RestAnteil(Vektoren[0].Y, Vektoren[1].Y,0);
                    players[i].logik[b].Motoren.Raeder[0].Energie -= new Vector2(0, y);
                  {
                        float ang = Help.PunktDrehenUmBestimmteEntfernung(Vector2.Zero,players[i].logik[b].Motoren.Raeder[0].pos,y);
                        // y korrektur berechnen
                        Vector2 rotpunkt = Help.RotatePosition(Vector2.Zero, players[i].vehikleAngle[b] - ang, players[i].logik[b].Motoren.Raeder[1].pos);

                        double a = Help.VektorBetrag(rotpunkt);
                        double e = Help.VektorBetrag(players[i].logik[b].Motoren.Raeder[1].pos);
                        float ykorrektur = (float)Math.Sqrt(a * a - e * e);
                        if (float.IsNaN(ykorrektur)) ykorrektur = 0;

                      //  players[i].logik[b].Motoren.MotorVectorOld = Help.RotatePosition(players[i].logik[b].Motoren.Raeder[0].pos, -ang, players[i].logik[b].Motoren.Raeder[0].pos + players[i].logik[b].Motoren.MotorVectorOld);
                        players[i].vehikleAngle[b] += ang;
                        players[i].pos[b] += new Vector2(0, ykorrektur);
                    }

                    y = Help.RestAnteil(Vektoren[0].Y, Vektoren[1].Y, 1);
                    players[i].logik[b].Motoren.Raeder[1].Energie -= new Vector2(0, y);
                    {
                        float ang = Help.PunktDrehenUmBestimmteEntfernung(Vector2.Zero, players[i].logik[b].Motoren.Raeder[1].pos, y);
                        // y korrektur berechnen
                        Vector2 rotpunkt = Help.RotatePosition(Vector2.Zero, players[i].vehikleAngle[b] + ang, players[i].logik[b].Motoren.Raeder[1].pos);

                        double a = Help.VektorBetrag(rotpunkt);
                        double e = Help.VektorBetrag(players[i].logik[b].Motoren.Raeder[1].pos);
                        float ykorrektur = (float)Math.Sqrt(a * a - e * e);
                        if (float.IsNaN(ykorrektur)) ykorrektur = 0;

                        //players[i].logik[b].Motoren.MotorVectorOld = Help.RotatePosition(players[i].logik[b].Motoren.Raeder[1].pos, +ang, players[i].logik[b].Motoren.Raeder[1].pos + players[i].logik[b].Motoren.MotorVectorOld);
                        players[i].vehikleAngle[b] -= ang;
                        players[i].pos[b] += new Vector2(0, ykorrektur);
                    }*/

                    /*  float angle = players[i].vehikleAngle[b];
                       Vector2 Bezugspunkt = players[i].pos[b];
                           int rad =0;
                       // reflektion
                       if (players[i].logik[b].Motoren.GetRadCollision(rad, angle, Bezugspunkt))
                       {
                           Vector2 own = Bezugspunkt + players[i].logik[b].Motoren.Raeder[rad].pos;
                           Vector2 p1 = new Vector2((new Vector2(-3, 0)).X, Help.BottomOf2(own + new Vector2(-3, 0)));
                           Vector2 p2 = new Vector2((new Vector2(3, 0)).X, Help.BottomOf2(own + new Vector2(3, 0)));
                           players[i]. = Help.VektorReflexion(Result, p1, p2, 1f);
                       }*/
                    // }

                    var ditt = (int)(Texturen.panzerindex[players[i].KindofTank[b]].Width * (players[i].Size[b]));
                    ditt = Fahrzeugdaten.FAHRM.Wert[players[i].KindofTank[b]];
                    int add = ditt / 2;
                    if ((int)players[i].pos[b].X - ditt + add < 0) ditt = 0;
                    if ((int)players[i].pos[b].X - ditt + add > Spielfeld.Length - 2)
                        ditt = (Spielfeld.Length) - ((int)players[i].pos[b].X - 2 - ditt + add);

                    int playerposX = (int)players[i].pos[b].X + add;
                    var playerposY = (int)players[i].pos[b].Y;
                    int playerposX2;
                    int playerposY2;
                    float summ = 0;
                    int dit = 0;
                    for (int c = 0; c < ditt && playerposX - c + add < Spielfeld.Length; c++)
                    {
                        summ += Kartenformat.BottomOf(playerposX - c + add, playerposY);
                        // Spielfeld[playerposX - c + add];
                        dit++;
                    }
                    summ /= dit;
                    playerposX = (int)players[i].pos[b].X + add;
                    playerposY = (int)players[i].pos[b].Y;
                    playerposX2 = (int)players[i].pos[b].X - ditt + add;
                    playerposY2 = (int)summ;
                    var auxangle =
                        (float)
                            Math.Atan(
                                (double)
                                    (Kartenformat.BottomOf(playerposX2, playerposY2) -
                                     Kartenformat.BottomOf(playerposX, playerposY)) / ((playerposX2) - (playerposX)));
                    // (float)Math.Atan((double)(Spielfeld[playerposX2] - Spielfeld[playerposX]) / ((playerposX2) - (playerposX)));
                    float move = Fahrzeugdaten.FALLW.Wert[players[i].KindofTank[b]];

                    if (players[i].vehikleAngle[b] < auxangle)
                    {
                        float diff = auxangle - players[i].vehikleAngle[b];
                        if (diff > move) diff = move;
                        players[i].vehikleAngle[b] += diff;
                        // if (Server.isRunning) Server.Send("VEHIKLEANGLE " + i + " " + b + " " + players[i].vehikleAngle[b]);
                    }
                    else if (players[i].vehikleAngle[b] > auxangle)
                    {
                        float diff = players[i].vehikleAngle[b] - auxangle;
                        if (diff > move) diff = move;
                        players[i].vehikleAngle[b] -= diff;
                        //  if (Server.isRunning) Server.Send("VEHIKLEANGLE " + i + " " + b + " " + players[i].vehikleAngle[b]);
                    }
                }
            }
        }

        /// <summary>
        ///     Check_s the raketen.
        /// </summary>
        public void check_Raketen() // muss jeden takt aufgerufen werden, verkürzt lebensdauer der raketen
        {
            bool focused_rakete = false;
            for (int i = 0; i < Missile.Length; i++)
            {
                if (Missile[i].verzoegerung > 0) continue;
                Missile[i].check_Rakete(Spielfeld, Height);
                // if ((Missile[i].misslePosition.X < 0 || Missile[i].misslePosition.X >= Spielfeld.Length) && Missile[i].focused && i == CurrentMissile) wait_change = 0;
                // if (Missile[i].misslePosition.X < 0 || Missile[i].misslePosition.X >= Spielfeld.Length) { Missile[i].Delete(); }

                if (Missile[i].focused && CurrentMissile != -1)
                {
                    if (Missile[CurrentMissile].missleShot == false) CurrentMissile = i;
                    if (!Moving_Map) SetzeFokus(Position(Missile[i].misslePosition));
                    focused_rakete = true;
                }
            }
            if (wait_change > 0) wait_change--;
            if (focused_rakete && !Moving_Map)
            {
                SetzeFokus(Position(Missile[CurrentMissile].misslePosition));
                wait_change = (int)Waffendaten.Daten2[Missile[CurrentMissile].Art].X;
            }
            if (!focused_rakete && wait_change == 0 && increaseairstrike == false && !Moving_Map &&
                players[CurrentPlayer].CurrentTank > -1)
                SetzeFokusX(Position(players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank]));
        }

        /// <summary>
        ///     Check_shot_increases this instance.
        /// </summary>
        public void check_shot_increase() // aktualisiert die sshootingpower, wenn leertaste gedrückt
        {
            if (increaseshot)
            {
                if (!increaseairstrike)
                {
                    if (Waffendaten.Verschiessbar[players[CurrentPlayer].CurrentWeapon] == 1)
                    {
                        players[CurrentPlayer].shootingPower++;
                        if (players[CurrentPlayer].shootingPower > 100) players[CurrentPlayer].shootingPower = 100;
                    }
                }
            }
        }

        /// <summary>
        ///     Check_verzoegertes the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <returns>List{Vector3}.</returns>
        public List<Vector3> check_verzoegerte(GameTime gameTime)
        // prüft ob nach dem Aufschlag verzögerte Raketen jetzt zünden dürfen
        {
            var list = new List<Vector3>();
            for (int i = 0; i < Missile.Length; i++)
            {
                if (Missile[i].verzoegerung > 0)
                {
                    Missile[i].verzoegerung--;

                    if (Missile[i].verzoegerung == 0)
                    {
                        // nun darf sie endlich explodieren
                        if (Missile[i].Art != 3 && Missile[i].Art != 2 && !Missile[i].watered)
                        {
                            list.AddRange(Missile[i].Explosion(Spielfeld));
                        }

                        if (!Missile[i].watered)
                        {
                            list.AddRange(Explosionsschäden(gameTime, Missile[i].RaktnSpitze(), Missile[i].Energie,
                                Missile[i].Art, Missile[i].Besitzer));
                        }

                        // hier startet der mapsmoke
                        if (Waffendaten.Verschiessbar[Missile[i].Art] != 4)
                            if (Missile[i].Art != 2 && Missile[i].Art != 3 && !Missile[i].watered)
                            {
                                // Feuerstelle erstellen
                                /*for (int j = -(int)Rakete.Daten[Missile[i].Art].X / 2; j < Rakete.Daten[Missile[i].Art].X / 2; j += Rakete.BrandAbstand[Missile[i].Art])
                                {
                                    if (Missile[i].misslePosition.X + j < 0 || Missile[i].misslePosition.X + j >= Spielfeld.Length) continue;

                                    Karte.AddExplosion(Karte.particleListMapSmoke, new Vector2(Missile[i].RaktnSpitze().X + j, Help.BottomOf2(Missile[i].RaktnSpitze())), 4,
                                   Rakete.Daten[Missile[i].Art].X, Rakete.Daten[Missile[i].Art].W * 10, gameTime,
                                    Microsoft.Xna.Framework.Color.Gray.ToVector3(), Missile[i].Art, 2); //.Rakete.Farben[Missile[i].Art]
                                }*/
                            }
                        int a = Missile[i].Energie;
                        Missile[i].Delete();
                    }
                }
            }

            // Minen berechnen
            for (int b = 0; b < players.Count(); b++)
            {
                for (int c = 0; c < players[b].Minen.Count; c++)
                {
                    if (!players[b].Minen[c].Aktiv && players[b].Minen[c].Verzoegerung <= 0)
                    {
                        int id = players[b].Minen[c].ID;
                        list.AddRange(players[b].Minen[c].ZündeMine(Spielfeld, gameTime, this));

                        for (int e = 0; e < players[b].Minen.Count; e++)
                            if (players[b].Minen[e].ID == id)
                            {
                                players[b].Minen.RemoveAt(e);
                                break;
                            }
                        c = 0;
                    }
                }

                if (b == CurrentPlayer) continue;
                for (int c = 0; c < players[b].Minen.Count; c++)
                {
                    if (players[b].Minen[c].Aktiv) continue;
                    if (players[b].Minen[c].Verzoegerung > 0)
                    {
                        players[b].Minen[c].Verzoegerung--;
                    }
                }
            }

            // Kisten berechnen
            for (int c = 0; c < Kisten.aktiv.Count; c++)
            {
                if (Kisten.aktiv[c]) continue;
                if (Kisten.verzögerung[c] > 0)
                {
                    Kisten.verzögerung[c]--;
                    continue;
                }

                int id = Kisten.id[c];
                list.AddRange(Kisten.Explosion(c, Spielfeld, gameTime, this));

                for (int e = 0; e < Kisten.aktiv.Count; e++)
                    if (Kisten.id[e] == id)
                    {
                        Kisten.DeleteKiste(e);
                        break;
                    }
                c = 0;
            }

            return list;
        }

        /// <summary>
        ///     Check_winds this instance.
        /// </summary>
        public void check_wind() // prüfe Timout des Windes
        {
            if (Client.isRunning) return;
            if (WindTimeout > 0)
            {
                WindTimeout--;
            }
            else
            {
                next_Wind();
            }
        }

        /// <summary>
        ///     Current_s the left.
        /// </summary>
        public void Current_Left() // bewegt den aktuellen Panzer nach links
        {
            if (Mod.SPIELERMENU_VISIBLE.Wert)
            Game1.Spielermenu.intrade = false;

            //  if (Client.isRunning) return;
            if (players[CurrentPlayer].CurrentTank <= -1) return;
            if (Spieler.PLAYER_FUEL.Wert &&
                players[CurrentPlayer].Rucksack[players[CurrentPlayer].CurrentTank].GibTreibstoff() <
                Fahrzeugdaten.VERBRAUCH.Wert[players[CurrentPlayer].KindofTank[players[CurrentPlayer].CurrentTank]])
                return;
            if (TIMEOUT.Wert && Timeout <= 0) return;

            bool message = players[CurrentPlayer].Links(Spielfeld, players[CurrentPlayer].CurrentTank);
            if (message)
            {
                if (TIMEOUT.Wert) Timeout -= TIMEOUT_REDUZIEREN_BEIM_FAHREN.Wert;
                if (!Moving_Map) SetzeFokusX(players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank]);
                //  if (Server.isRunning) Server.Send("POS " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].X + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].Y);
            }
        }

        /// <summary>
        ///     Current_s the right.
        /// </summary>
        public void Current_Right() // bewegt den aktuellen Panzer nach rechts
        {
            if (Mod.SPIELERMENU_VISIBLE.Wert)
            Game1.Spielermenu.intrade = false;

            //if (Client.isRunning) return;
            if (players[CurrentPlayer].CurrentTank <= -1) return;
            if (Spieler.PLAYER_FUEL.Wert &&
                players[CurrentPlayer].Rucksack[players[CurrentPlayer].CurrentTank].GibTreibstoff() <
                Fahrzeugdaten.VERBRAUCH.Wert[players[CurrentPlayer].KindofTank[players[CurrentPlayer].CurrentTank]])
                return;
            if (TIMEOUT.Wert && Timeout <= 0) return;
            bool message = players[CurrentPlayer].Rechts(Spielfeld, players[CurrentPlayer].CurrentTank, Fenster);
            if (message)
            {
                if (TIMEOUT.Wert) Timeout -= TIMEOUT_REDUZIEREN_BEIM_FAHREN.Wert;
                if (!Moving_Map) SetzeFokusX(players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank]);
                // if (Server.isRunning) Server.Send("POS " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].X + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].Y);
            }
        }

        /// <summary>
        ///     Current_s the rohr_ left.
        /// </summary>
        public bool Current_Rohr_Left() // bewegt den aktuellen Panzer nach rechts
        {
            if (players[CurrentPlayer].CurrentTank <= -1) return false;
            bool result = players[CurrentPlayer].Rohr_Rechts(players[CurrentPlayer].CurrentTank);
            if (Server.isRunning)
                Server.Send("ROHRANGLE " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " +
                            players[CurrentPlayer].Angle[players[CurrentPlayer].CurrentTank]);
            if (Server.isRunning)
                Server.Send("OVERREACH " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " +
                            players[CurrentPlayer].overreach[players[CurrentPlayer].CurrentTank]);

            int typ = players[CurrentPlayer].KindofTank[players[CurrentPlayer].CurrentTank];
            int soundid = players[CurrentPlayer].CurrentTank;
            if (result)
            {
                if (Sounds.Panzer_rohr_end[typ] != null)
                {
                    if (Sounds.Panzer_rohr_end[typ].IsPlaying(soundid))
                    {
                        Sounds.Panzer_rohr_end[typ].StopSound(soundid);
                        Sounds.Panzer_rohrmode[typ] = 0;
                    }

                    if (Sounds.Panzer_rohrmode[typ] == 0 && !Sounds.Panzer_rohr_begin[typ].IsPlaying(soundid))
                    {
                        Sounds.Panzer_rohr_begin[typ].PlaySound(soundid);
                        Sounds.Panzer_rohrmode[typ] = 1;
                    }
                    else if (Sounds.Panzer_rohrmode[typ] == 1 && !Sounds.Panzer_rohr_loop[typ].IsPlaying(soundid) &&
                             !Sounds.Panzer_rohr_begin[typ].IsPlaying(soundid))
                    {
                        Sounds.Panzer_rohr_loop[typ].PlaySound(true, soundid);
                        Sounds.Panzer_rohrmode[typ] = 2;
                    }
                }
                return true;
            }
            if (Sounds.Panzer_rohr_end[typ] != null)
                if (Sounds.Panzer_rohrmode[typ] > 0 && !Sounds.Panzer_rohr_end[typ].IsPlaying(soundid))
                {
                    Sounds.Panzer_rohr_loop[typ].StopSound(soundid);
                    Sounds.Panzer_rohr_begin[typ].StopSound(soundid);

                    Sounds.Panzer_rohr_end[typ].PlaySound(soundid);
                    Sounds.Panzer_rohrmode[typ] = 0;
                }

            return false;
        }

        /// <summary>
        ///     Current_s the rohr_ right.
        /// </summary>
        public bool Current_Rohr_Right() // bewegt den aktuellen Panzer nach rechts
        {
            if (players[CurrentPlayer].CurrentTank <= -1) return false;
            bool result = players[CurrentPlayer].Rohr_Links(players[CurrentPlayer].CurrentTank);
            if (Server.isRunning)
                Server.Send("ROHRANGLE " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " +
                            players[CurrentPlayer].Angle[players[CurrentPlayer].CurrentTank]);
            if (Server.isRunning)
                Server.Send("OVERREACH " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " +
                            players[CurrentPlayer].overreach[players[CurrentPlayer].CurrentTank]);

            int typ = players[CurrentPlayer].KindofTank[players[CurrentPlayer].CurrentTank];
            int soundid = players[CurrentPlayer].CurrentTank;

            if (result)
            {
                if (Sounds.Panzer_rohr_end[typ] != null)
                {
                    if (Sounds.Panzer_rohr_end[typ].IsPlaying(soundid))
                    {
                        Sounds.Panzer_rohr_end[typ].StopSound(soundid);
                        Sounds.Panzer_rohrmode[typ] = 0;
                    }

                    if (Sounds.Panzer_rohrmode[typ] == 0 && !Sounds.Panzer_rohr_begin[typ].IsPlaying(soundid))
                    {
                        Sounds.Panzer_rohr_begin[typ].PlaySound(soundid);
                        Sounds.Panzer_rohrmode[typ] = 1;
                    }
                    else if (Sounds.Panzer_rohrmode[typ] == 1 && !Sounds.Panzer_rohr_loop[typ].IsPlaying(soundid) &&
                             !Sounds.Panzer_rohr_begin[typ].IsPlaying(soundid))
                    {
                        Sounds.Panzer_rohr_loop[typ].PlaySound(soundid);
                        Sounds.Panzer_rohrmode[typ] = 2;
                    }
                }
                return true;
            }
            if (Sounds.Panzer_rohr_end[typ] != null)
                if (Sounds.Panzer_rohrmode[typ] > 0 && !Sounds.Panzer_rohr_end[typ].IsPlaying(soundid))
                {
                    Sounds.Panzer_rohr_loop[typ].StopSound(soundid);
                    Sounds.Panzer_rohr_begin[typ].StopSound(soundid);

                    Sounds.Panzer_rohr_end[typ].PlaySound(soundid);
                    Sounds.Panzer_rohrmode[typ] = 0;
                }

            return false;
        }

        /// <summary>
        ///     Deletes the panzer.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="id">The id.</param>
        /// <param name="gameTime">The game time.</param>
        public void DeletePanzer(int Spieler, int id, GameTime gameTime)
        {
            players[Spieler].Entfernen(id, true, this, gameTime);

            int b = Spieler;
            int c = id;
            if (Sounds.PanzerMove[players[b].KindofTank[c]] != null)
                Sounds.PanzerMove[players[b].KindofTank[c]].StopSound(c);

            if (Sounds.Panzer_rohr_begin[players[b].KindofTank[c]] != null)
                Sounds.Panzer_rohr_begin[players[b].KindofTank[c]].StopSound(c);

            if (Sounds.Panzer_rohr_loop[players[b].KindofTank[c]] != null)
                Sounds.Panzer_rohr_loop[players[b].KindofTank[c]].StopSound(c);

            if (Sounds.Panzer_rohr_begin[players[b].KindofTank[c]] != null)
                Sounds.Panzer_rohr_end[players[b].KindofTank[c]].StopSound(c);
        }

        /// <summary>
        ///     Deletes the rakete.
        /// </summary>
        /// <param name="ID">The ID.</param>
        public void DeleteRakete(int ID) // löscht eine angegebene Rakete aus dem Spiel
        {
            // Rakete aus Array löschen
            Missile[ID].Besitzer[0] = -1;
            Missile[ID].missleShot = false;
            Missile[ID].verzoegerung = 0;
        }

        /// <summary>
        ///     Deletes the tunnel.
        /// </summary>
        /// <param name="Spieler">The spieler.</param>
        /// <param name="id">The id.</param>
        public void DeleteTunnel(int Spieler, int id)
        {
            players[Spieler].TunnelAnlage.RemoveAt(id);
        }

        /// <summary>
        ///     Explosionsschädens the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="Energie">The energie.</param>
        /// <param name="Art">The art.</param>
        /// <param name="Besitzer">The besitzer.</param>
        /// <returns>List{Vector3}.</returns>
        public List<Vector3> Explosionsschäden(GameTime gameTime, Vector2 pos, int Energie, int Art, int[] Besitzer)
        {
            var list = new List<Vector3>();

            // Brandherd erstellen
            if (Waffendaten.Brandherd[Art] > 0)
            {
                int brand = Waffendaten.Brandherd[Art];
                list.AddRange(Feuer.Generieren((int)(pos.X - brand), (int)(pos.X + brand), (int)pos.X, (int)pos.Y, 5));
            }

            if ((Art == 2 || Art == 3 || Art == 12 || Art == 13 || Art == 11))
            {
            }
            else
            {
                // Trashschäden berechnen
                for (int b = 0; b < Nutzloses.GibAnzahl(); b++)
                {
                    Nutzloses.PrüfeObZerstörung(b, pos, Energie);
                }
            }

            // Panzer schäden
            for (int b = 0; b < players.Length; b++)
            {
                for (int c = 0; c < players[b].pos.Count; c++)
                {
                    if (!players[b].isthere[c]) continue;
                    var dist =
                        (int)
                            Math.Sqrt(Math.Pow(players[b].pos[c].X - pos.X, 2) +
                                      Math.Pow(players[b].pos[c].Y - pos.Y, 2));
                    if (dist < 0) dist = -dist;
                    var energy = (int)((float)Energie * Waffendaten.Energiefaktor[Art]);
                    if (dist > energy + 200) continue; // wenn zu weit weg

                    // Prüfe ob Panzer im Bunker ist
                    bool isin = false;
                    for (int i = 0; i < Bunker.Position.Count; i++)
                        if (Bunker.PrüfeObKollision(i, players[b].pos[c] + new Vector2(0, -2)))
                        {
                            isin = true;
                            break;
                        }
                    if (isin) continue;

                    // Es muss der Schaden berechnet werden
                    if ((Art == 2) && !players[b].Effekte[c].GetEingefroren())
                    {
                        // rakete ist eine freeze
                        players[b].Effekte[c].Hinzufügen(Effectdata.EINGEFROREN);
                        continue;
                    }
                    else if ((Art == 3 || Art == 12) && !players[b].Effekte[c].GetVergiftet())
                    {
                        // Giftgas
                        players[b].Effekte[c].Hinzufügen(Effectdata.VERGIFTED);
                        continue;
                    }
                    else if ((Art == 13) && !players[b].Effekte[c].GetElektrisiert())
                    {
                        // Elektrisiert
                        players[b].Effekte[c].Hinzufügen(Effectdata.ELEKTRISIERT);
                        continue;
                    }

                    // Schaden berechnen
                    int typ = players[b].KindofTank[c];

                    int found = players[b].PrüfeObTreffer(c, pos, energy);
                    if (found > 0)
                    {
                        float abstand = -1;
                        for (int d = 0; d < Fahrzeugdaten.Messpunkte[players[b].KindofTank[c]].Count(); d++)
                        {
                            Vector2 dat = players[b].pos[c] +
                                          new Vector2(
                                              (float)-Texturen.panzerindex[typ].Width / 2 * Fahrzeugdaten.SCALEP.Wert[typ],
                                              (float)-Texturen.panzerindex[typ].Height * Fahrzeugdaten.SCALEP.Wert[typ]) +
                                          Fahrzeugdaten.Messpunkte[typ][d];
                            if (abstand == -1 || Help.Abstand(pos, dat) < abstand)
                            {
                                abstand = Help.Abstand(pos, dat);
                            }
                        }

                        if (abstand == -1) abstand = 0;

                        float gesamt = players[b].MaxPixel[c];
                        if (abstand > energy) abstand = energy;
                        if (players[b].PrüfeObKollision(c, pos)) abstand = 0;
                        if (found > gesamt) found = (int)gesamt;

                        // int schaden = (int)((float)((float)found / gesamt) * ((float)1 - ((float)abstand / energy)) * Rakete.Zentrumschaden[Art]);

                        var schaden =
                            (int)
                                (((float)((float)found / gesamt) * Waffendaten.Zentrumschaden[Art]) * 0.25f +
                                 (float)((float)1 - ((float)abstand / energy)) * Waffendaten.Zentrumschaden[Art] * 0.75f);
                        if (schaden > 0 && Waffendaten.Verschiessbar[Art] != 4)
                            Kurzmeldung.Hinzufügen("-" + schaden.ToString(),
                                new Vector2(players[b].pos[c].X,
                                    players[b].pos[c].Y -
                                    Texturen.panzerindex[players[b].KindofTank[c]].Height *
                                    Fahrzeugdaten.SCALEP.Wert[players[b].KindofTank[c]]), Color.Yellow);
                        //(players[b].Farbe)
                        players[b].hp[c] -= schaden;
                    }

                    if (players[b].Effekte[c].GetHP(players[b].hp[c]) < 1)
                    {
                        // panzer entfernen
                        Nutzloses.Hinzufügen(Texturen.panzerruine[players[b].KindofTank[c]], players[b].pos[c],
                            players[b].vehikleAngle[c], players[b].overreach[c], players[b].Size[c], true, true);

                        if (Besitzer[0] >= 0 && Besitzer[1] >= 0)
                        {
                            var punkte =
                                (int)
                                    (Fahrzeugdaten.ExpRewarded[players[b].KindofTank[c]] *
                                     ((players[b].CurrentLv[c]) * 0.5 + 1));
                            players[Besitzer[0]].ExpNow[Besitzer[1]] += punkte;
                            Kurzmeldung.Hinzufügen("+" + punkte.ToString(), players[Besitzer[0]].pos[Besitzer[1]],
                                Color.Green, -300);
                            Sounds.punkteerhalten.PlaySound(0);
                        }
                    }
                    //   if (Server.isRunning) Server.Send("HP " + b + " " + c + " " + Player[b].hp[c]);
                }
            }

            if ((Art == 2 || Art == 3 || Art == 12 || Art == 13 || Art == 11))
            {
            }
            else
            {
                // Gebäudeschäden berechnen
                for (int b = 0; b < Haeuser.Lebenspunkte.Count; b++)
                    if (Haus.HAEUSER_ZERSTOERUNG)
                    {
                        int found = Haeuser.IsExplode(b, pos, Energie);
                        if (found > 0)
                        {
                            Haeuser.UpdateHausSchaden(b, found);
                            if (Haus.HAEUSER_KOLLISION) Haeuser.load(b);
                            int id = Haeuser.HausTyp[b];
                            if (found > 0 && Waffendaten.Verschiessbar[Art] != 4)
                                Kurzmeldung.Hinzufügen("-" + found.ToString(),
                                    new Vector2(
                                        Haeuser.Position[b].X + Haeuser.Bild[b].Width / 2 * Gebäudedaten.SKALIERUNG.Wert[id] +
                                        Gebäudedaten.POSITIONX.Wert[id],
                                        Haeuser.Position[b].Y + Gebäudedaten.POSITIONY.Wert[id] -
                                        Haeuser.Bild[b].Height * Gebäudedaten.SKALIERUNG.Wert[id]), Color.Yellow);
                            //(Haeuser.Besitzer[b] == -1 ? Color.Yellow : players[Haeuser.Besitzer[b]].Farbe)
                        }
                    }

                // Tunnelschäden berechnen
                for (int i = 0; i < players.Count(); i++)
                    for (int b = 0; b < players[i].TunnelAnlage.Count; b++)
                        if (Haus.HAEUSER_ZERSTOERUNG)
                        {
                            int found = players[i].TunnelAnlage[b].PrüfeObZerstörung(pos, Energie);
                            if (found > 0)
                            {
                                players[i].TunnelAnlage[b].AktualisiereTunnelSchaden(found);
                                if (Haus.HAEUSER_KOLLISION) players[i].TunnelAnlage[b].Lade();

                                if (found > 0 && Waffendaten.Verschiessbar[Art] != 4)
                                    Kurzmeldung.Hinzufügen("-" + found.ToString(),
                                        new Vector2(
                                            players[i].TunnelAnlage[b].Position.X +
                                            Texturen.tunnel.Width / 2 * Tunnel.SKALIERUNG,
                                            players[i].TunnelAnlage[b].Position.Y -
                                            Texturen.tunnel.Height * Tunnel.SKALIERUNG), Color.Yellow);
                                //(Haeuser.Besitzer[b] == -1 ? Color.Yellow : players[Haeuser.Besitzer[b]].Farbe)
                                if (players[i].TunnelAnlage[b].Lebenspunkte <= 0)
                                {
                                    DeleteTunnel(i, b);
                                }
                            }
                        }

                // Bunkerschäden berechnen
                for (int b = 0; b < Bunker.Position.Count; b++)
                //if (Bunker.BUNKER_ZERSTOERUNG.Wert)
                {
                    int found = Bunker.PrüfeObZerstörung(b, pos, Energie);
                    if (found > 0)
                    {
                        Bunker.UpdateBunkerSchaden(b, found);
                        if (found > 0 && Waffendaten.Verschiessbar[Art] != 4)
                            Kurzmeldung.Hinzufügen("-" + found.ToString(),
                                new Vector2(
                                    Bunker.Position[b].X + Texturen.bunker[0].Width / 2 * Optimierung.Skalierung(0.25f),
                                    Bunker.Position[b].Y - Texturen.bunker[0].Height * Optimierung.Skalierung(0.25f)),
                                Color.Yellow);
                        //(Haeuser.Besitzer[b] == -1 ? Color.Yellow : players[Haeuser.Besitzer[b]].Farbe)
                        //if (Bunker.BUNKER_KOLLISION.Wert) Bunker.load(b);

                        if (Bunker.Lebenspunkte[b] <= 0)
                        {
                            Vector2 po = Bunker.Position[b];
                            Bunker.Entfernen(b);
                            Karte.Explosion_einer_Waffe_zünden(Spielfeld, gameTime, this, 0,
                                po +
                                new Vector2(Texturen.bunker[0].Width / 2 * Optimierung.Skalierung(0.25f),
                                    -Texturen.bunker[0].Height / 2 * Optimierung.Skalierung(0.25f)));
                        }
                    }
                }
            }

            // Minenschäden berechnen
            for (int b = 0; b < players.Count(); b++)
                for (int c = 0; c < players[b].Minen.Count; c++)
                {
                    if (!players[b].Minen[c].Aktiv) continue;
                    int found = players[b].Minen[c].PrüfeObZerstörung(pos, Energie);
                    if (found > 0)
                    {
                        players[b].Minen[c].Aktiv = false;
                        players[b].Minen[c].Verzoegerung = 0;
                        //  Sounds.Mine_klick[rand.Next(0, 2)].PlaySoundAny();
                    }
                }

            // Kistenschäden berechnen
            for (int c = 0; c < Kisten.aktiv.Count; c++)
            {
                if (!Kisten.aktiv[c]) continue;
                int found = Kisten.IsExplode(c, pos, Energie);
                if (found > 0)
                {
                    Kisten.aktiv[c] = false;
                    Kisten.verzögerung[c] = 60;
                }
            }

            return list;
        }

        /// <summary>
        ///     Gewinners this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Gewinner() // gibt die Anzahl der aktiven/lebenden Spieler aus
        {
            if (Editor.visible) return -1;

            for (int i = 0; i < players.Length; i++)
            {
                for (int b = 0; b < players[i].pos.Count; b++)
                {
                    if (players[i].isthere[b]) return i;
                }
            }
            return -1;
        }

        /// <summary>
        ///     Inits the specified kartengroesse.
        /// </summary>
        /// <param name="Kartengroesse">The kartengroesse.</param>
        /// <param name="screen">The screen.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public void Init(int Kartengroesse, Vector2 screen) // Spiel Init -- Aufruf nicht nötig
        {
            bool symmetrisch = Karte.KARTE_SYMMETRISCH;
            Kartenbreite = Kartengroesse;
            increaseshot = false;
            rand = new Random();
            Spielfeld = new List<UInt16>[Kartengroesse];
            for (int i = 0; i < Kartengroesse; i++) Spielfeld[i] = new List<UInt16>();
            Karte = new Karte();
            Help.Spielfeld = Spielfeld;
            hoehlen = new Höhlenkonfiguration();

            //Karte.create_map_staedte_doerfer(Spielfeld, (int)((double)screen.Y * 0.75), (int)((double)screen.Y * 0.5), 30, 175, 50, (int)screen.Y, hoehlen, symmetrisch);
            Karte.create_map(Spielfeld, (int)(screen.Y * 0.75), (int)(screen.Y * 0.5), 30, 50, 50, (int)screen.Y, hoehlen);

            // Spieler erstellen
            players = new Spieler[2];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new Spieler();
            } // Initialenergie

            Missile = new Waffen[100];
            for (int i = 0; i < Missile.Length; i++)
            {
                Missile[i] = new Waffen();
                Missile[i].ID = i;
            }

            Fenster.X = 0;
            Fenster.Y = 0;

            Width = (int)screen.X;
            Height = (int)screen.Y;

            InitialisiereSpieler(symmetrisch);

            if (Haus.HAEUSER)
            {
                if (Karte.STAEDTE_UND_DOERFER)
                {
                    Haeuser.set_Haeuser_staedte_doerfer(Spielfeld, symmetrisch);
                }
                else
                    Haeuser.set_Haeuser(Spielfeld, symmetrisch);
            }

            // TODO hier die Bäume
            if (Baum.BAEUME) Baeume.set_Baeume(Spielfeld, symmetrisch);

            foreground = new Texture2D[(int)Math.Ceiling((double)Kartengroesse / 2048)];
            foregroundColors = new Color[(int)Math.Ceiling((double)Kartengroesse / 2048)][];

            // fogColors = new Color[];
            // Nebelkreis = new Color[(int)Math.Ceiling((double)Kartengroesse / 2048)];

            // Replay
            Replay.Begin(players);
            //Replay.Laden("", (CurrentPlayer + 1) % players.Count(), false);
            //Hauptfenster.Tausch.CurrentPlayer = 1;
            Wind.X = rand.Next(-60, 60) / 10;
            // Wind.Y = rand.Next(-5, 5) / 10;
        }

        /// <summary>
        ///     Initialisiert die Spieler
        /// </summary>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public void InitialisiereSpieler(bool symmetrisch)
        {
            Color[] Spielerfarben = { Color.Red, Color.Blue };
            float[] Angle = { 180, 0 };
            bool[] overreach = { true, false };
            int a = 150;
            int bd = Spielfeld.Length - a;
            var Positionen = new Vector2[players.Length];
            Positionen[0] = new Vector2(a, Spielfeld[a][0]);
            Positionen[1] = new Vector2(bd, Spielfeld[bd][0]);

            int[] PanzerTypen = { 0, 1, 2, 3, 4, 5 };

            if (symmetrisch)
            {
                PanzerTypen = new[] { 0, 2, 3, 1 };
            }
            else
            {
                PanzerTypen = new[] { 0, 1, 2, 3 };
            }

            for (int i = 0; i < players.Length; i++)
            {
                for (int b = 0; b < PanzerTypen.Count(); b++)
                {
                    AddPanzer(i, PanzerTypen[b], MathHelper.ToRadians(Angle[i]), overreach[i], Positionen[i]);
                }

                players[i].shootingPower = 0f;
                players[i].MaxTimeout = TIMEOUT_SEKUNDEN.Wert * 60;
                players[i].Credits = 5000;
                players[i].Farbe = Spielerfarben[i];
                players[i].Kenngroesse_Wert = new Kenngroesse(Kartenbreite, Height, 100, 100, 0);
            }

            SetzePanzer(players, symmetrisch);

            for (int i = 0; i < players.Length; i++)
                for (int b = 0; b < players[i].pos.Count; b++)
                    players[i].Kenngroesse_Wert.Hinzufügen(players[i].pos[b],
                        Fahrzeugdaten._PANZERWERTE.Wert[players[i].KindofTank[b]], 350, Anteil.Fläche, Wachstum.LinearFallend, false);
        }

        /// <summary>
        ///     Inits the runde.
        /// </summary>
        public void InitRunde()
        {
            for (int i = 0; i < players.Length; i++)
            {
                for (int b = 0; b < players[i].pos.Count; b++)
                {
                    if (!players[i].isthere[b]) continue;

                    if (CurrentPlayer == i || !TIMEOUT_SPIELERWECHSEL.Wert)
                    {
                        players[i].oldpos[b] = players[i].pos[b];

                        players[i].Effekte[b].PrüfeEffektdauer();
                    }
                }
            }

            Vordergrund.AktualisiereVordergrund(Feuer.AlleBrandherdeVerkleinern());
            ;
        }

        /// <summary>
        ///     Erzeugt den Inhalt des Spieles aus einem String
        /// </summary>
        /// <param name="Text">der Text in dem das Spiel definiert ist</param>
        public Spiel Laden(List<String> Text)
        {
            Spiel temp = this;

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "SPIEL");

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.Width = TextLaden.LadeInt(Liste, "Width", temp.Width); // korrekt?
            temp.Height = TextLaden.LadeInt(Liste, "Height", temp.Height); // korrekt?
            temp.Schuesse = TextLaden.LadeInt(Liste, "Schuesse", temp.Schuesse);
            temp.WindTimeout = TextLaden.LadeInt(Liste, "WindTimeout", temp.WindTimeout);
            temp.Wind = TextLaden.LadeVector2(Liste, "Wind", temp.Wind);
            temp.Timeout = TextLaden.LadeInt(Liste, "Timeout", temp.Timeout);
            temp.CurrentMissile = TextLaden.LadeInt(Liste, "CurrentMissile", temp.CurrentMissile);
            temp.CurrentPlayer = TextLaden.LadeInt(Liste, "CurrentPlayer", temp.CurrentPlayer);

            if (Karte == null) Karte = new Karte();
            Spielfeld = Karte.Laden(Text2);
            Help.Spielfeld = Spielfeld;

            int anz = Text2.Count;
            do
            {
                anz = Text2.Count;
                Haeuser.Laden(Text2, -1);
            } while (anz != Text2.Count);

            anz = Text2.Count;
            do
            {
                anz = Text2.Count;
                Nutzloses.Laden(Game1.ContentAll, Text2, -1);
            } while (anz != Text2.Count);

            anz = Text2.Count;
            do
            {
                anz = Text2.Count;
                Bunker.Laden(Text2, -1);
            } while (anz != Text2.Count);

            if (Missile==null)Missile = new Waffen[100];

            for (int i = 0; i < Missile.Count(); i++)
            {
                Waffen temp2 = Waffen.Laden(Text2, null);
                if (temp2 != null)
                {
                    temp.Missile[i] = temp2;
                }
                else
                    temp.Missile[i].Delete();
            }

            // Spieler laden
            List<Spieler> temp2play = new List<Spieler>();
            int anz2 = Text2.Count;

            do
            {
                anz2 = Text2.Count;
                Spieler tempPlayer = new Spieler();
                tempPlayer = Spieler.Laden(temp, Text2, null);
                temp2play.Add(tempPlayer);
            } while (anz2 != Text2.Count);
            temp.players = temp2play.ToArray();

            for (int i = 0; i < temp.players.Length; i++)
            {
                temp.players[i].Kenngroesse_Wert = new Kenngroesse(Kartenbreite, temp.Height, 100, 100, 0);
            }

           /* for (int i = 0; i < temp.players.Length; i++)
                for (int b = 0; b < temp.players[i].pos.Count; b++)
                    temp.players[i].Kenngroesse_Wert.Hinzufügen(temp.players[i].pos[b],
                        Fahrzeugdaten._PANZERWERTE.Wert[temp.players[i].KindofTank[b]], 350, Anteil.Fläche, Wachstum.LinearFallend, false);
            */
            return temp;
        }

        /// <summary>
        ///     bewegt den aktuellen Panzer nach links.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="id">The id.</param>
        public void Links(int player, int id)  
        {
            //  if (Client.isRunning) return;
            if (id <= -1) return;
            if (Spieler.PLAYER_FUEL.Wert &&
                players[player].Rucksack[id].GibTreibstoff() <
                Fahrzeugdaten.VERBRAUCH.Wert[players[CurrentPlayer].KindofTank[id]]) return;
            //if (players[CurrentPlayer].freezed[players[CurrentPlayer].CurrentTank] > 0) return;
            if (TIMEOUT.Wert && Timeout <= 0) return;
            bool message = players[player].Links(Spielfeld, id);
            if (message)
            {
                if (TIMEOUT.Wert) Timeout -= TIMEOUT_REDUZIEREN_BEIM_FAHREN.Wert;
                if (!Moving_Map && id == players[player].CurrentTank) SetzeFokusX(players[player].pos[id]);
                //  if (Server.isRunning) Server.Send("POS " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].X + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].Y);
            }
        }

        /// <summary>
        ///     setzte auf nächsten Spieler
        /// </summary>
        public void Spielerwechsel() 
        {
            if (Client.isRunning) return;

            if (TIMEOUT_SPIELERWECHSEL.Wert)
            {
                CurrentPlayer++;
                if (PrüfeAktiveSpieler() > 0)
                {
                    CurrentPlayer %= PrüfeAktiveSpieler();
                    int i = 0;
                    for (; CurrentPlayer >= 0; i++)
                    {
                        bool check = false;
                        for (int b = 0; b < players[i].pos.Count; b++)
                        {
                            if (players[i].isthere[b])
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check) CurrentPlayer--;
                    }
                    i--;
                    Tausch.CurrentPlayer = i;
                    CurrentPlayer = i;

                    if (players[i].CurrentWeapon == 5) players[i].CurrentWeapon = 0;
                    increaseairstrike = false;
                    Moving_Map = false;
                    increaseshot = false;
                    players[i].shootingPower = 0;
                    int a = players[i].CurrentTank;

                    if (CREDITS.Wert)
                        players[CurrentPlayer].Credits += players[CurrentPlayer].Generate_Credits(Haeuser, CurrentPlayer);

                    if (ACTION_POINTS.Wert)
                        players[i].ActionPoints += (int) players[i].Generate_ActionPoints(i);

                     if (ACTION_POINTS_MAX.Wert != 0 && players[i].ActionPoints > ACTION_POINTS_MAX.Wert)
                        players[i].ActionPoints = ACTION_POINTS_MAX.Wert;

                    if (TIMEOUT.Wert) Timeout = players[CurrentPlayer].MaxTimeout;
                    if (SCHUESSE.Wert) Schuesse = players[CurrentPlayer].MaxSchuesse;
                    if (!Moving_Map) SetzeFokusX(players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank]);
                }
                else
                    CurrentPlayer = -1;
            }
            else
            {
                for (int i = 0; i < players.Count(); i++)
                {
                    if (CREDITS.Wert)
                        players[i].Credits += players[i].Generate_Credits(Haeuser, i);

                    if (ACTION_POINTS.Wert)
                        players[i].ActionPoints += (int)players[i].Generate_ActionPoints(i);

                    if (ACTION_POINTS_MAX.Wert != 0 && players[i].ActionPoints > ACTION_POINTS_MAX.Wert)
                        players[i].ActionPoints = ACTION_POINTS_MAX.Wert;

                    if (TIMEOUT.Wert) Timeout = players[i].MaxTimeout;
                    if (SCHUESSE.Wert) Schuesse = players[i].MaxSchuesse;
                }
            }

            InitRunde();
            /* if (Server.isRunning)
             {
                 Server.Send("TIMEOUT " + Timeout);
                 Server.Send("CURRENTPLAYER " + CurrentPlayer);
             }*/
        }

        /// <summary>
        ///     Next_s the wind.
        /// </summary>
        public void next_Wind() // erzeuge neuen Wind
        {
            if (Client.isRunning) return;
            WindTimeout = 15; // rand.Next(30, 60 * 1); // dauer des Windes
            float q = 0;
            q = (rand.Next(-20, 20) / 100.0f); // stärke des Windes
            Wind.X += q;
            if (Wind.X < -6) Wind.X = -6;
            if (Wind.X > 6) Wind.X = 6;

            Wind.Y = 0; // ???
            if (Server.isRunning) Server.Send("WIND " + Wind.X);
        }

        /// <summary>
        ///     Prüfes the bau.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="radius">The radius.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool PrüfeBau(Vector2 pos, int radius)
        {
            pos.Y = Kartenformat.BottomOf(pos.X, pos.Y);
            int breite = radius;

            // Prüfe, ob untergrund flach
            for (int i = (int)pos.X - breite / 2; i < pos.X + breite / 2; i++)
            {
                if (Kartenformat.BottomOf(i, pos.Y) != pos.Y) return false;
            }

            // Prüfe das nichts anderes in der nähe steht
            if (pos.X - radius / 2 > 0 && pos.X + radius / 2 < Spielfeld.Length)
            {
                breite = (int)(Texturen.bunker[0].Width * Optimierung.Skalierung(0.25f));
                for (int i = 0; i < Bunker.Position.Count; i++)
                {
                    if (pos.X - radius / 2 <= Bunker.Position[i].X + breite && pos.X - radius / 2 >= Bunker.Position[i].X)
                    {
                        return false;
                    }
                    if (pos.X + radius / 2 >= Bunker.Position[i].X && pos.X + breite / 2 <= Bunker.Position[i].X + radius)
                    {
                        return false;
                    }
                }

                for (int i = 0; i < Haeuser.Position.Count; i++)
                {
                    breite =
                        (int)(Texturen.haus[Haeuser.HausTyp[i]].Width * Gebäudedaten.SKALIERUNG.Wert[Haeuser.HausTyp[i]]);
                    if (pos.X - radius / 2 <= Haeuser.Position[i].X + breite && pos.X - radius / 2 >= Haeuser.Position[i].X)
                    {
                        return false;
                    }
                    if (pos.X + radius / 2 >= Haeuser.Position[i].X && pos.X + breite / 2 <= Haeuser.Position[i].X + radius)
                    {
                        return false;
                    }
                }

                breite = (int)(Texturen.tunnel.Width * Tunnel.SKALIERUNG);
                for (int i = 0; i < players.Count(); i++)
                    for (int b = 0; b < players[i].TunnelAnlage.Count; b++)
                    {
                        if (pos.X - radius / 2 <= players[i].TunnelAnlage[b].Position.X + breite &&
                            pos.X - radius / 2 >= players[i].TunnelAnlage[b].Position.X)
                        {
                            return false;
                        }
                        if (pos.X + radius / 2 >= players[i].TunnelAnlage[b].Position.X &&
                            pos.X + breite / 2 <= players[i].TunnelAnlage[b].Position.X + radius)
                        {
                            return false;
                        }
                    }
            }
            return true;
        }

        /// <summary>
        ///     Prüfes the bunkerbau.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool PrüfeBunkerbau(Vector2 pos)
        {
            // - new Vector2(Texturen.bunker[0].Width * Optimierung.Skalierung(0.25f)/2,0)
            return PrüfeBau(pos, (int)(Texturen.bunker[0].Width * Optimierung.Skalierung(0.25f)));
        }

        /// <summary>
        ///     Prüfes the tunnelbau.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool PrüfeTunnelbau(Vector2 pos)
        {
            return PrüfeBau(pos, (int)(Texturen.tunnel.Width * Tunnel.SKALIERUNG));
        }

        /// <summary>
        ///     bewegt den aktuellen Panzer nach rechts
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="id">The id.</param>
        public void Rechts(int player, int id)
        {
            //if (Client.isRunning) return;
            if (id <= -1) return;
            if (Spieler.PLAYER_FUEL.Wert &&
                players[player].Rucksack[id].GibTreibstoff() <
                Fahrzeugdaten.VERBRAUCH.Wert[players[CurrentPlayer].KindofTank[id]]) return;
            // if (players[CurrentPlayer].freezed[players[CurrentPlayer].CurrentTank] > 0) return;
            if (TIMEOUT.Wert && Timeout <= 0) return;
            bool message = players[player].Rechts(Spielfeld, id, Fenster);
            if (message)
            {
                if (TIMEOUT.Wert) Timeout -= TIMEOUT_REDUZIEREN_BEIM_FAHREN.Wert;
                if (!Moving_Map && id == players[player].CurrentTank) SetzeFokusX(players[player].pos[id]);
                // if (Server.isRunning) Server.Send("POS " + CurrentPlayer + " " + players[CurrentPlayer].CurrentTank + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].X + " " + players[CurrentPlayer].pos[players[CurrentPlayer].CurrentTank].Y);
            }
        }

        /// <summary>
        ///     setzt die Fenster Position so, dass "pos" im Zentrum ist
        /// </summary>
        /// <param name="pos">The pos.</param>
        public void SetzeFokus(Vector2 pos)
        {
            if (Karte.KARTE_SYMMETRISCH)
            {
                Next_Fenster.X = Position(pos.X - Width/2);
                Next_Fenster.Y = pos.Y - Height / 2;

            }
            else
            {if (Next_Fenster.X < 0) Next_Fenster.X = 0;
                 if (Next_Fenster.X >= Spielfeld.Length - Width) Next_Fenster.X = Spielfeld.Length - Width;
            }
        }

        /// <summary>
        ///     setzt die Fenster Position so, dass "pos" im Zentrum ist
        /// </summary>
        /// <param name="pos">The pos.</param>
        public void SetzeFokusX(Vector2 pos)
        {
            if (Karte.KARTE_SYMMETRISCH)
            {
                Next_Fenster.X = Position(pos.X - Width/2);
                Next_Fenster.Y = pos.Y - Height / 2;
            }
            else
            {
                Next_Fenster.X = pos.X - Width / 2;
               if (Next_Fenster.X < 0) Next_Fenster.X = 0;
                if (Next_Fenster.X >= Spielfeld.Length - Width) Next_Fenster.X = Spielfeld.Length - Width;
            }
        }

        public void FensterBewegenAufPosition(Vector2 pos)
        {
                Next_Fenster.X = Position(pos.X);
                Next_Fenster.Y = pos.Y;
        }

        /*public void check_Feuer()
        {
            for (int i = 0; i < players.Length; i++)
            {
                for (int b = 0; b < players[i].pos.Count; b++)
                {
                    if (!players[i].isthere[b]) continue;

                    if (CurrentPlayer == i)
                    {
                        // Feuer
                        if (players[i].brennend[b] > 0)
                            players[i].hp[b] -= (int)(Tankdata.MaxHP[players[i].KindofTank[b]] * 0.13f);
                    }
                }
            }
        }*/

        /// <summary>
        ///     setzt die Panzer zufällig auf die Karte
        /// </summary>
        /// <param name="players">The players.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public void SetzePanzer(Spieler[] players, bool symmetrisch)
        {
            if (symmetrisch)
            {
                int abstand = 150;
                var start = (int)(Spielfeld.Length / 4 - ((float)players[0].pos.Count / 2 * abstand));
                for (int i = 0; i < players[0].pos.Count; i++)
                {
                    int x = start + i * abstand;
                    players[0].pos[i] = new Vector2(x, Spielfeld[x][0]);
                }

                int middle = Spielfeld.Length / 2;
                for (int i = 0; i < players[1].pos.Count; i++)
                {
                    players[1].pos[i] = new Vector2(middle + (middle - players[0].pos[i].X),
                        Spielfeld[(int)(players[0].pos[i].X)][0]);
                }
            }
            else
            {
                var dat = new int[Spielfeld.Length];
                for (int i = 0; i < dat.Length; i++) dat[i] = -1;
                int max = 0;
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i].pos.Count > max) max = players[i].pos.Count;
                }

                for (int i = 0; i < max; i++)
                {
                    for (int b = 0; b < players.Length; b++)
                    {
                        bool check = false;
                        for (; check == false; )
                        {
                            // setzte nächsten Panzer
                            int q = rand.Next(50, dat.Length - 50);
                            bool find = false;
                            for (int c = q - 250; c < q + 250; c++)
                            {
                                if (c < 0) continue;
                                if (c >= dat.Length) continue;
                                if (dat[c] >= 0)
                                {
                                    find = true;
                                    break;
                                }
                            }

                            if (!find)
                            {
                                if (players[b].pos.Count >= i + 1)
                                {
                                    dat[q] = 1;

                                    players[b].Kenngroesse_Wert.Hinzufügen(players[b].pos[i], -Fahrzeugdaten._PANZERWERTE.Wert[players[b].KindofTank[i]], 350, Anteil.Fläche, Wachstum.LinearFallend, false);
                                    players[b].pos[i] = new Vector2(q, Spielfeld[q][0]);
                                    players[b].Kenngroesse_Wert.Hinzufügen(players[b].pos[i], Fahrzeugdaten._PANZERWERTE.Wert[players[b].KindofTank[i]], 350, Anteil.Fläche, Wachstum.LinearFallend, false);
                                }
                                check = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Wandelt das Spiel zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> Speichern()
        {
            var list = new List<String>();
            // Kartendaten speichern
            list.Add("[SPIEL]");
            list.Add("Width=" + Width);
            list.Add("Height=" + Height);
            list.Add("Schuesse=" + Schuesse);
            list.Add("WindTimeout=" + WindTimeout);
            list.Add("Wind=" + Wind);
            list.Add("Timeout=" + Timeout);
            list.Add("CurrentMissile=" + CurrentMissile);
            list.Add("CurrentPlayer=" + CurrentPlayer);

            // Haeuser speichern
            {
                list.AddRange(Haeuser.Speichern());
            }

            // Bunker speichern
            {
                list.AddRange(Bunker.Speichern());
            }

            // Nutzloses speichern
            {
                list.AddRange(Nutzloses.Speichern());
            }

            // Kisten speichern
            {
                 list.AddRange(Kisten.Speichern());
             }

            // Spieler speichern
            {
                 for (int i = 0; i < players.Count(); i++)
                 {
                     players[i].id = i;
                     list.AddRange(players[i].Speichern());
                 }
             }

            // Waffen speichern
            {
                for (int i = 0; i < Missile.Count(); i++)
                {
                    list.AddRange(Missile[i].Speichern());
                }
            }

            // Karte speichern
            {
                list.AddRange(Karte.Speichern(Spielfeld));
            }

            list.Add("[/SPIEL]");

            /*
        public Color[] Colors;
        public int CurrentMissile;
        public int CurrentPlayer = 0;
        public Vector2 Fenster;
        public Color[] fogColors;
        public Texture2D[] foreground;
        public Color[][] foregroundColors;
        public int Height;
        public Höhlenkonfiguration hoehlen = null;
        public bool increaseairstrike = false;
        public bool increaseshot = false;
        public bool IsServer = true;
        public bool leftsidet = false;
        public bool minimap_visible = true;
        public bool Moving_Map = false;
        public Vector2 Next_Fenster;
        public bool paused = false;
        public bool replay_visible2 = true;
        public int Schuesse = 0;
        public int Sieger = -1;
        public int Timeout = 0;
        public int wait_change = 0;
        public int Width;
        public Vector2 Wind;
        public int WindTimeout = 0;
        private static int incshoot2 = 0;
        private static int incshoot3 = 0;
        private List<String> names;*/
            return list;
        }

        /// <summary>
        ///     gibt die Anzahl der aktiven/lebenden Spieler aus
        /// </summary>
        /// <returns>gibt die Anzahl der aktiven/lebenden Spieler aus</returns>
        public int PrüfeAktiveSpieler()
        {
            if (Editor.visible) return players.Length;

            int sum = 0;
            for (int i = 0; i < players.Length; i++)
            {
                bool check = false;
                for (int b = 0; b < players[i].pos.Count; b++)
                {
                    if (players[i].isthere[b])
                    {
                        check = true;
                        break;
                    }
                }

                if (check) sum++;
            }
            return sum;
        }

        /// <summary>
        ///     Updates the missles.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="smokeList">The smoke list.</param>
        public void AktualisiereGeschosse(GameTime gameTime, List<Karte.ParticleData> smokeList) // geändert
        {
            for (int i = 0; i < Missile.Length; i++)
            {
                if (Missile[i].verzoegerung > 0) continue;
                Missile[i].UpdateMissle(Wind);
                var notset = new Vector2(-99, -99);
                if (!Missile[i].missleShot && Missile[i].Last_Position[0] == notset) continue;
                for (int c = 0; c < Missile[i].Last_Position.Length; c++)
                {
                    if (Missile[i].Last_Position[c] == notset) continue;
                    // Rauch starten
                }
                Karte.AddExplosion(smokeList, Missile[i].Last_Position[Missile[i].Last_Position.Length - 1],
                    (int)Waffendaten.Daten3[Missile[i].Art].X, Waffendaten.Daten3[Missile[i].Art].Y,
                    Waffendaten.Daten3[Missile[i].Art].Z, gameTime, Waffendaten.Farben[Missile[i].Art], Missile[i].Art,
                    1);
            }
        }

        #endregion Methods

        /*   public static int Position(int Pos)
           {
               return (int) Position((float) Pos);
           }*/
    }
}