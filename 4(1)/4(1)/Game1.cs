// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-21-2013
// ***********************************************************************
// <copyright file="Game1.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
//using System.Threading;
using System.Threading.Tasks;
//using Microsoft.VisualBasic;
//using System.Diagnostics;

namespace _4_1_
{
    /// <summary>
    ///     Class Game1
    /// </summary>
    public class Game1 : Game
    {
        #region Fields

        public static ContentManager ContentAll = null;

        /// <summary>
        ///     gibt an, ob der Debug Modus aktiv ist
        /// </summary>
        public static Var<bool> DEBUG_AKTIV = new Var<bool>("DEBUG_AKTIV", false);

        public static List<string> Ladebildschirmtexte = null;

        /// <summary>
        ///     The maus aktiv
        /// </summary>
        public static bool MausAktiv = true;

        /// <summary>
        ///     gibt an, welcher Shader verwendet werden soll
        /// </summary>
        public static Var<int> SHADER = new Var<int>("SHADER", 0);

        /// <summary>
        ///     The spiel2
        /// </summary>
        public static Spiel Spiel2 = null;

        public static SpriteSortMode SpriteMode = SpriteSortMode.Immediate;

        /// <summary>
        ///     Die Kartenbreite in Pixel
        /// </summary>
        public int Kartengroesse = 2048*5;

        /// <summary>
        ///     The menuaufruf
        /// </summary>
        public bool menuaufruf;

        /// <summary>
        ///     The mouse state
        /// </summary>
        public MouseState mouseState;

        /// <summary>
        ///     The spiel aktiv
        /// </summary>
        public bool SpielAktiv = false;

        /// <summary>
        ///     The sp menu fully deployed
        /// </summary>
        public bool SpMenuFullyDeployed = false;

        /// <summary>
        ///     The time
        /// </summary>
        public GameTime Time = new GameTime();

        /// <summary>
        ///     The transparenz
        /// </summary>
        public float Transparenz = 1.0f;

        /// <summary>
        ///     The resultion independent
        /// </summary>
        private const bool resultionIndependent = true;

        /// <summary>
        ///     The line
        /// </summary>
        private static Texture2D line;

        /// <summary>
        ///     The reduzierung
        /// </summary>
        private static int reduzierung;

        /// <summary>
        ///     The reduzierung2
        /// </summary>
        private static int reduzierung2;

        /// <summary>
        ///     The draw surface
        /// </summary>
        private readonly IntPtr drawSurface;

        /// <summary>
        ///     The smoke list
        /// </summary>
        private readonly List<Karte.ParticleData> smokeList = new List<Karte.ParticleData>();

        /// <summary>
        ///     The base screen size
        /// </summary>
        private Vector2 baseScreenSize = Vector2.Zero;

        private int Click;

        private bool Clickbool;
        private int clicktimer;

        private GamePadState currentState;

        private int last;
        private GamePadState oldcurrentState;

        private int qq;

        /// <summary>
        ///     The wait
        /// </summary>
        private int wait;

        /// <summary>
        ///     Maus einschalten oder abschalten
        /// </summary>
        private Color[,] water;

        #endregion Fields

        #region Menus

        /// <summary>
        ///     The meldungen
        /// </summary>
        public static Chatbox Meldungen;

        /// <summary>
        ///     The spielermenu
        /// </summary>
        public static Spielermenu Spielermenu;

        /// <summary>
        ///     The ladenmenu
        /// </summary>
        private Lademenu Ladenmenu;

        /// <summary>
        ///     The pause menu
        /// </summary>
        private Menu pauseMenu;

        /// <summary>
        ///     The set up menu
        /// </summary>
        private SetupMenu SetUpMenu;

        /// <summary>
        ///     The start menu
        /// </summary>
        private Startmenu StartMenu;

        #endregion Menus

        /// <summary>
        ///     The XWERT
        /// </summary>
        private readonly int XWERT = Tausch.screenwidth;

        /// <summary>
        ///     The YWERT
        /// </summary>
        private readonly int YWERT = Tausch.screenheight;

        private Vector2 WolkenPos = Vector2.Zero;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Game1" /> class.
        /// </summary>
        /// <param name="drawSurface">The draw surface.</param>
        public Game1(IntPtr drawSurface) // selbsterklärend
        {
            // Erstelle Spiel

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            // graphics.IsFullScreen = true;  // schaltet fullsceen an
            ContentAll = Content;

            graphics.PreferredBackBufferWidth = Tausch.screenwidth;
            graphics.PreferredBackBufferHeight = Tausch.screenheight;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = true;

            // this.graphics.PreferredBackBufferWidth = 1366;
            // this.graphics.PreferredBackBufferHeight = 768;
           // if (drawSurface!=null)
            this.drawSurface = drawSurface;

            graphics.PreparingDeviceSettings += graphics_PreparingDeviceSettings;
            Control.FromHandle((Window.Handle)).VisibleChanged += Game1_VisibleChanged;
            // this.graphics.IsFullScreen = true;  // schaltet fullsceen an
        }

        public Game1()
        {
            // Erstelle Spiel

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = true;  // schaltet fullsceen an
            ContentAll = Content;

            graphics.PreferredBackBufferWidth = Tausch.screenwidth;
            graphics.PreferredBackBufferHeight = Tausch.screenheight;
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.PreferMultiSampling = true;
            // this.graphics.PreferredBackBufferWidth = 1366;
            // this.graphics.PreferredBackBufferHeight = 768;
            //    this.drawSurface = drawSurface;

            //graphics.PreparingDeviceSettings += graphics_PreparingDeviceSettings;
            // Control.FromHandle((Window.Handle)).VisibleChanged += Game1_VisibleChanged;
        }

    /// <summary>
        ///     Hiermit wird der Text beim Laden eines Vorgangs angezeigt
        /// </summary>
        /// <param name="Text2">The text2.</param>
        public static void LadeText(String Text2)
        {
            if (Ladebildschirmtexte == null || Ladebildschirmtexte.Count == 0)
            {
                if (File.Exists("Content\\Konfiguration\\Ladetexte.txt"))
                {
                    Ladebildschirmtexte = new List<string>();

                    var b = new ReaderStream.ReaderStream("Content\\Konfiguration\\Ladetexte.txt");

                    while (!b.EndOfStream)
                    {
                        Ladebildschirmtexte.Add(b.ReadLine());
                    }

                    b.Close();
                }
            }

            ///Hauptfenster.Program.Formular.label31.BringToFront();
            ///Hauptfenster.Program.Formular.label31.Left = Hauptfenster.Program.Formular.progressBar1.Left;
            ///Hauptfenster.Program.Formular.label31.Text =
            ///    ("").PadRight((int)((Hauptfenster.Program.Formular.progressBar1.Width / 5.4f) + 0.5f), ' ');
           /// Hauptfenster.Program.Formular.label31.Refresh();
           /// 

        if (Text2 == null)
        {
            LadebildschirmText = "";
        }else
        if (Ladebildschirmtexte == null)
            {
                ///Hauptfenster.Program.Formular.label31.Text = Text2;
                LadebildschirmText = Text2;
            }
            else
            {
                int id = randomizer.Next(0, Ladebildschirmtexte.Count);
               /// Hauptfenster.Program.Formular.label31.Text = Ladebildschirmtexte[id];
               /// 
                LadebildschirmText = Ladebildschirmtexte[id];
                Ladebildschirmtexte.RemoveAt(id);
            }

           /// Hauptfenster.Program.Formular.label31.Top = Hauptfenster.Program.Formular.progressBar1.Top -
                                                      ///  Hauptfenster.Program.Formular.label31.Height;
            // + progressBar1.Height
           /// Hauptfenster.Program.Formular.label31.Left = Hauptfenster.Program.Formular.Width / 2 -
                                                       ///  Hauptfenster.Program.Formular.label31.Width / 2;
           /// if (!Hauptfenster.Program.Formular.label31.Visible) Hauptfenster.Program.Formular.label31.Show();
            ///Hauptfenster.Program.Formular.label31.Refresh();
            // System.Threading.Thread.Sleep(1000);
        }
        public static Task LadebildschirmTask = null;
        public static Task LadenTask = null;
        public static Task SpeichernTask = null;

        /// <summary>
        ///     Check_s the datenaustausch.
        /// </summary>
        public void check_Datenaustausch()
        {
            if (Tausch.Output.Count > 0)
            {
                for (int i = 0; i < Tausch.Output.Count; i++)
                {
                    String text = Tausch.Output[i];
                    if (Spiel2.IsServer)
                    {
                        Server.Send("<>" + text);
                    }
                    else
                        Client.Send("<>" + text);
                }
                Tausch.Output.Clear();
            }

            // Hauptfenster.Tausch.screenwidth = Hauptfenster.Form1.ActiveForm.Width;
            //Hauptfenster.Tausch.screenheight = Hauptfenster.Form1.ActiveForm.Height;
            baseScreenSize = new Vector2(Tausch.screenwidth, Tausch.screenheight);

            screenWidth = (int)baseScreenSize.X;
            screenHeight = (int)baseScreenSize.Y;
            Kartengroesse = 2048 * Tausch.Kartengroesse;
            SpielAktiv = Tausch.SpielAktiv;
            if (Spiel2 != null)
            {
                Spiel2.CurrentPlayer = Tausch.CurrentPlayer;
            }

            if (Tausch.SpielLaden)
            {
                Tausch.SpielLaden = false;
                var MapReader = new MapReader();
                ///  MapReader.LoadMap(this, Hauptfenster.Tausch.Map, Hauptfenster.Tausch.Data, ref Spiel2, new Vector2(screenWidth, screenHeight));
                if (Spiel2 != null)
                {
                    Help.Spielfeld = Spiel2.Spielfeld;
                    Spiel2.Width = screenWidth;
                    Spiel2.Height = screenHeight;
                    //// vordergrund = Farbwahl(Texturen.tilltexture);
                    water = Farbwahl(Texturen.wasser);
                    Vordergrund.ErstelleVordergrund();
                    Fog.CreateFog();
                    Mine.Initialisierung(Content);
                    createKasten();
                    Tausch.SpielAktiv = true;
                }
                else
                    Tausch.SpielAktiv = false;
            }
            else if (Tausch.CreateNewGame)
            {

                // Neues Spiel erstellen und hochladen
                Tausch.CreateNewGame = false;

                // Reset
                Karte.Reset_Materialien();
                Fahrzeugdaten.Reset_Tankdata();

                // Mod laden
                Tausch.Mod = "A.conf";
                Mod.LadeModVariablen("Content\\Konfiguration\\" + Tausch.Mod);

                // Komponenten laden
                loadAllContent();

                CreateNeuesSpiel();
                // speichern
                String Pfad = "";
                if (HTTP.HTTP.gameid != "")
                {
                    Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";
                }
                else
                    Pfad = "Content\\Games\\temp\\";
                Help.angrabbel_funktion();

                HTTP.HTTP.Dir(Pfad);
                MapWriter.Generieren(Spiel2);
                Replay.Generieren(false);
                MapWriter.Speichern(Pfad + "Map.dat");
                Replay.Speichern(Pfad + "Data.dat");
                Spiel2 = null;
                List<String> list = HTTP.HTTP.upload(String.Join("\r\n", MapWriter.list.ToArray()),
                    String.Join("\r\n", Replay.list.ToArray()), "0");
                int q = list.Count;
            }
            else if (Tausch.StartServer)
            {
                if (Server.isRunning) Server.Shutdown();
                if (Client.isRunning) Client.Shutdown();
                Tausch.StartServer = false;
                StarteNeuesSpiel();
                Server.StartServer();
                Server.Spiel2 = Spiel2;
                Spiel2.IsServer = true;
                keybState = Keyboard.GetState();
            }
            else if (Tausch.StartClient)
            {
                if (Server.isRunning) Server.Shutdown();
                if (Client.isRunning) Client.Shutdown();
                Tausch.StartClient = false;
                //
                StarteNeuesSpiel();
                //
                Client.Connect(Tausch.ZielIP, 14242);
                Client.Spiel2 = Spiel2;
                Client.game = this;
                Spiel2.IsServer = false;
            }
            else if (Tausch.StarteSpiel)
            {
                LadebildschirmAktiv = true;
                if (Hauptfenster.Form1.ActiveForm != null)
                {
                    Hauptfenster.Form1.ActiveForm.BringToFront();
                }
                    //Hauptfenster.Program.Formular.Hide();

                    Spiel2 = null;
                    Tausch.StarteSpiel = false;
                    StarteNeuesSpiel();

                    if (Server.isRunning)
                    {
                        Thread.Sleep(5000);
                        Server.SendAll();
                    }
                
            }
        }

        /// <summary>
        ///     Erstellt ein neues Spiel
        /// </summary>
        public void CreateNeuesSpiel()
        {
            Spiel2 = new Spiel(Kartengroesse, new Vector2(screenWidth, screenHeight));
            Help.Spielfeld = Spiel2.Spielfeld;
            Spiel2.Width = screenWidth;
            Spiel2.Height = screenHeight;
        }

        /// <summary>
        ///     Zeichnet die Airstrikes
        /// </summary>
        public void DrawAirstrike()
        {
            if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == 5)
            {
                int xPos = (int)Spiel2.players[Spiel2.CurrentPlayer].shootingPower - (int)Spiel2.Fenster.X;
                int yPos = screenHeight / 2 - (int)Spiel2.Fenster.Y;
                spriteBatch.Draw(Texturen.kreuz,
                    new Vector2(xPos, Kartenformat.BottomOf(Spiel2.players[Spiel2.CurrentPlayer].shootingPower, 0)),
                    null, Color.White, 0, new Vector2(35, 35), 1f, SpriteEffects.None, 1);

                // spriteBatch.Draw(Texturen.kreuz, new Vector2(xPos, (int)(Spiel2.Spielfeld[(int)Spiel2.players[Spiel2.CurrentPlayer].shootingPower][0] - Spiel2.Fenster.Y)), null, Color.White, 0, new Vector2(35, 35), 1f, SpriteEffects.None, 1);
            }
        }

        /// <summary>
        ///     Draws the arbeitsbereich.
        /// </summary>
        public void DrawArbeitsbereich()
        {
            if (Spiel2.CurrentPlayer == -1) return;
            int i = Spiel2.CurrentPlayer;
            int b = Spiel2.players[i].CurrentTank;
            if (b <= -1) return;
            int id = Spiel2.players[i].KindofTank[b];
            if (Fahrzeugdaten.ARBEITSBEREICH.Wert[id] == 0) return;
            Vector2 pos = Spiel2.players[i].pos[b];
            Vector2 oldpos = Spiel2.players[i].oldpos[b];
            var x1 = (int)(oldpos.X - Fahrzeugdaten.ARBEITSBEREICH.Wert[id] - Spiel2.Fenster.X);
            var x2 = (int)(oldpos.X + Fahrzeugdaten.ARBEITSBEREICH.Wert[id] - Spiel2.Fenster.X);
            int size = 40;
            if (x1 >= 0 && x1 < Spiel2.Width)
            {
                Help.DrawLine(spriteBatch, new Vector2(x1 + size / 2, 0), new Vector2(x1 + size / 2, Spiel2.Height),
                    Color.Goldenrod * 0.2f, size);
            }
            if (x2 >= 0 && x2 < Spiel2.Width)
            {
                Help.DrawLine(spriteBatch, new Vector2(x2 + size / 2, 0), new Vector2(x2 + size / 2, Spiel2.Height),
                    Color.Goldenrod * 0.2f, size);
            }
        }

        /// <summary>
        ///     Zeichnet die Bunker
        /// </summary>
        public void DrawBunker()
        {
            if (Spiel2 == null) return;

            float scale = Optimierung.Skalierung(0.25f);
            for (int i = 0; i < Spiel2.Bunker.Position.Count; i++)
            {
                int check = -1;
                var breite = (int)(Texturen.bunker[0].Width * scale);
                if (Bunker.BUNKER_KOLLISION)
                {
                    for (int b = 0; b < Spiel2.players.Length; b++)
                    {
                        for (int c = 0; c < Spiel2.players[b].pos.Count; c++)
                        {
                            if (Spiel2.Bunker.PrüfeObKollision(i, Spiel2.players[b].pos[c] - new Vector2(0, 5)))
                            {
                                if (check == -1 || check != Spiel2.CurrentPlayer) check = b;
                                break;
                            }
                        }
                    }
                }

                var xPos = (int)(Spiel2.Bunker.Position[i].X - (int)Spiel2.Fenster.X);
                var yPos = (int)(Spiel2.Bunker.Position[i].Y - Spiel2.Fenster.Y);

                int Bild = 0;
                if (Spiel2.Bunker.Lebenspunkte[i] > 0)
                {
                    if (Spiel2.Bunker.Lebenspunkte[i] / Spiel2.Bunker.MaximaleLebenspunkte[i] <= 0.75f) Bild = 1;
                    if (Spiel2.Bunker.Lebenspunkte[i] / Spiel2.Bunker.MaximaleLebenspunkte[i] <= 0.5f) Bild = 2;
                }
                else
                    Bild = 2;

                /*  if (Bunker.BUNKER_ZERSTOERUNG)
                  {
                      if (check == -1 || check != Spiel2.CurrentPlayer)
                      {
                          spriteBatch.Draw(Spiel2.Bunker.Bild[i], new Vector2(xPos, yPos), null, Color.White, 0, new Vector2(0, Spiel2.Bunker.Bild[i].Height), scale, SpriteEffects.None, 1); // haus.Width / 2
                      }
                      else
                          spriteBatch.Draw(Spiel2.Bunker.Bild2[i], new Vector2(xPos, yPos), null, Color.White, 0, new Vector2(0, Spiel2.Bunker.Bild[i].Height), scale, SpriteEffects.None, 1); // haus.Width / 2
                  }
                  else
                  {*/

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                if (check == -1 || check != Spiel2.CurrentPlayer)
                {
                    spriteBatch.Draw(Texturen.bunker[Bild], new Vector2(xPos, yPos), null, Color.White, 0,
                        new Vector2(0, Texturen.bunker[Bild].Height), scale, SpriteEffects.None, 1); // haus.Width / 2
                }
                else
                    spriteBatch.Draw(Texturen.bunker2[Bild], new Vector2(xPos, yPos), null, Color.White, 0,
                        new Vector2(0, Texturen.bunker[Bild].Height), scale, SpriteEffects.None, 1); // haus.Width / 2
                spriteBatch.End();

                spriteBatch.Begin();
                if (Editor.visible && Editor.mouseover == 4 && Editor.mouseoverid == i)
                {
                    if (check == -1 || check != Spiel2.CurrentPlayer)
                    {
                        spriteBatch.Draw(Texturen.bunker[Bild], new Vector2(xPos, yPos), null, Color.Blue, 0,
                            new Vector2(0, Texturen.bunker[Bild].Height), scale, SpriteEffects.None, 1);
                        // haus.Width / 2
                    }
                    else
                        spriteBatch.Draw(Texturen.bunker2[Bild], new Vector2(xPos, yPos), null, Color.Blue, 0,
                            new Vector2(0, Texturen.bunker[Bild].Height), scale, SpriteEffects.None, 1);
                    // haus.Width / 2
                }
                spriteBatch.End();

                //}
                // Lebenslinie Zeichnen
                spriteBatch.Begin();
                if (Bunker.BUNKER_LEBENSLINIE)
                {
                    float Breite = Texturen.bunker[Bild].Width * scale;

                    var leben = new Rectangle(0, 0, (int)(Breite * 1), Texturen.leben.Height / 4);
                    spriteBatch.Draw(Texturen.leben,
                        new Vector2(xPos, Spiel2.Bunker.Position[i].Y - Texturen.bunker[Bild].Height * scale - 10), leben,
                        Color.DarkRed * 0.5f);

                    if (Spiel2.Bunker.Lebenspunkte[i] > 0)
                    {
                        leben = new Rectangle(0, 0,
                            (int)
                                (Breite * ((float)Spiel2.Bunker.Lebenspunkte[i] / (Spiel2.Bunker.MaximaleLebenspunkte[i]))),
                            Texturen.leben.Height / 4);
                        spriteBatch.Draw(Texturen.leben,
                            new Vector2(xPos, Spiel2.Bunker.Position[i].Y - Texturen.bunker[Bild].Height * scale - 10),
                            leben,
                            Color.Lime * 0.5f);
                    }
                }
                spriteBatch.End();
            }
        }

        /// <summary>
        ///     Zeichnet die Explosionen
        /// </summary>
        public void DrawExplosion()
        {
            for (int i = 0; i < Spiel2.Karte.particleListExp.Count; i++)
            {
                if (!Spiel2.Karte.particleListExp[i].set || !Spiel2.Karte.particleListExp[i].alive) continue;
                Karte.ParticleData particle = Spiel2.Karte.particleListExp[i];
                int xPos = (int)particle.Position.X - (int)Spiel2.Fenster.X;
                int yPos = (int)particle.Position.Y - (int)Spiel2.Fenster.Y;
                spriteBatch.Draw(Texturen.explosion, new Vector2(xPos, yPos), null, particle.ModColor, i,
                    new Vector2(256, 256), particle.Scaling, SpriteEffects.None, 1);
            }
        }

        /// <summary>
        ///     Zeichnet die Kisten
        /// </summary>
        public void DrawKisten()
        {
            if (Spiel2 == null) return;

            for (int b = 0; b < Spiel2.Kisten.aktiv.Count; b++)
            {
                float scale = Kiste.sc;
                var x = (int)Spiel2.Kisten.pos[b].X;
                var y = (int)Spiel2.Kisten.pos[b].Y;

                Texture2D Bild = Kiste.Bild;
                if (x == -1) continue;
                if (x + Bild.Width * scale < Spiel2.Fenster.X || Spiel2.Fenster.X + screenWidth < x - Bild.Width * scale)
                    continue;
                int xPos = x - (int)Spiel2.Fenster.X;
                var yPos = (int)(y - Spiel2.Fenster.Y);

                spriteBatch.Draw(Bild, new Vector2(xPos - (Bild.Width * scale) / 2, yPos - (Bild.Height * scale) / 2), null,
                    Color.White, 0, new Vector2(Bild.Width / 2, Bild.Height / 2), scale, SpriteEffects.None, 1);

                if (Editor.visible && Editor.mouseover == 5 && Editor.mouseoverid == b)
                    spriteBatch.Draw(Bild, new Vector2(xPos - (Bild.Width * scale) / 2, yPos - (Bild.Height * scale) / 2), null,
                        Color.Blue, 0, new Vector2(Bild.Width / 2, Bild.Height / 2), scale, SpriteEffects.None, 1);
            }
        }

        /// <summary>
        ///     Draws the leiste.
        /// </summary>
        public void DrawLeiste()
        {
            if (Spiel2.CurrentPlayer != -1)
                if (Spiel2.players[Spiel2.CurrentPlayer].CurrentTank != -1)
                {
                    float scale = Optimierung.Skalierung(0.25f);
                    float scale2 = 0.125f;
                    float scale5 = scale * 0.8f; // TODO wurde geändert, war vorher 0.2f, richtig?
                    int leistenbreite = 5;

                    // Fuel Button
                    if (Mod.FUEL_BUTTON_VISIBLE.Wert)
                    {
                        String fuel = "";
                        if (Spieler.GLOBAL_FUEL.Wert)
                            fuel = Spiel2.players[Spiel2.CurrentPlayer].fuelRemains < 0
                                ? "0000"
                                : ((Math.Floor(Spiel2.players[Spiel2.CurrentPlayer].fuelRemains > 9999
                                    ? 9999
                                    : Spiel2.players[Spiel2.CurrentPlayer].fuelRemains)).ToString()).PadLeft(4, '0');
                        if (Spieler.PLAYER_FUEL.Wert)
                            fuel =
                                Spiel2.players[Spiel2.CurrentPlayer].Rucksack[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].GibTreibstoff() < 0
                                    ? "0000"
                                    : ((Math.Floor(
                                        Spiel2.players[Spiel2.CurrentPlayer].Rucksack[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].GibTreibstoff() > 9999
                                            ? 9999
                                            : Spiel2.players[Spiel2.CurrentPlayer].Rucksack[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].GibTreibstoff()))
                                        .ToString()).PadLeft(4, '0');

                        Vector2 add = Spiel.CREDITS.Wert
                            ? new Vector2(-(Texturen.LeeresFeld.Width * scale5) - 25, 0)
                            : Vector2.Zero;

                        if (Mod.ACTION_BUTTON_VISIBLE.Wert)
                        {
                            spriteBatch.Draw(Texturen.LeeresFeld,
                                new Vector2(screenWidth - 3 - 2 * (Texturen.LeeresFeld.Width * scale5), 5) + add,
                                null, Color.White, 0, new Vector2(0, 0), scale5, SpriteEffects.None, 1);
                            spriteBatch.DrawString(Texturen.font, fuel,
                                new Vector2(screenWidth - 3 - 2 * (Texturen.LeeresFeld.Width * scale5) + 8, 5 + 10) +
                                add, Color.Green);
                        }
                        else
                        {
                            spriteBatch.Draw(Texturen.LeeresFeld,
                                new Vector2(screenWidth - 3 - (Texturen.LeeresFeld.Width * scale5), 5) + add, null,
                                Color.White, 0, new Vector2(0, 0), scale5, SpriteEffects.None, 1);
                            spriteBatch.DrawString(Texturen.font, fuel,
                                new Vector2(screenWidth - 3 - (Texturen.LeeresFeld.Width * scale5) + 8, 5 + 10) +
                                add, Color.Green);
                        }
                    }

                    // Credit Points Button
                    if (Spiel.CREDITS.Wert)
                    {
                        String credits =
                            (((int)Spiel2.players[Spiel2.CurrentPlayer].Credits > 9999
                                ? 9999
                                : Spiel2.players[Spiel2.CurrentPlayer].Credits).ToString()).PadLeft(4, '0');
                        spriteBatch.Draw(Texturen.LeeresFeld,
                            new Vector2(screenWidth - 3 - (Texturen.LeeresFeld.Width * scale5) * 2, 5), null,
                            Color.White, 0, new Vector2(0, 0), scale5, SpriteEffects.None, 1);
                        spriteBatch.DrawString(Texturen.font, credits,
                            new Vector2(screenWidth - 3 - (Texturen.LeeresFeld.Width * scale5) * 2 + 8, 5 + 10),
                            Color.CadetBlue);
                    }

                    // Action Points Button
                    if (Mod.ACTION_BUTTON_VISIBLE.Wert)
                    {
                        String action =
                            ((Spiel2.players[Spiel2.CurrentPlayer].ActionPoints > 9999
                                ? 9999
                                : Spiel2.players[Spiel2.CurrentPlayer].ActionPoints).ToString()).PadLeft(4, '0');
                        spriteBatch.Draw(Texturen.LeeresFeld,
                            new Vector2(screenWidth - 3 - (Texturen.LeeresFeld.Width * scale5), 5), null,
                            Color.White, 0, new Vector2(0, 0), scale5, SpriteEffects.None, 1);
                        spriteBatch.DrawString(Texturen.font, action,
                            new Vector2(screenWidth - 3 - (Texturen.LeeresFeld.Width * scale5) + 8, 5 + 10),
                            Color.Red);
                    }

                    // aktuell ausgewählte Munition
                    if (Mod.AKTUELLE_MUNITION_BUTTON_VISIBLE.Wert)
                    {
                        int id = Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon;
                        int tank = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                        Color qq = Color.White;
                        if (Spiel.ACTION_POINTS.Wert &&
                            Spiel2.players[Spiel2.CurrentPlayer].ActionPoints <
                            Waffendaten.APKosten[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon]) qq = Color.Red;
                        if (Spiel.MUNITION.Wert && Spiel2.players[Spiel2.CurrentPlayer].Munition[tank][id] < 1)
                            qq = Color.Red;

                        spriteBatch.Draw(Texturen.LeeresFeld,
                            new Vector2(Mod.AKTUELLE_MUNITION_BUTTON_X.Wert, Mod.AKTUELLE_MUNITION_BUTTON_Y.Wert), null,
                            qq, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1); // haus.Width / 2
                        spriteBatch.Draw(Texturen.waffenbilder[id],
                            new Vector2(Mod.AKTUELLE_MUNITION_BUTTON_X.Wert, Mod.AKTUELLE_MUNITION_BUTTON_Y.Wert), null,
                            qq, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1); // haus.Width / 2

                        // Akctionskosten
                        String cool = Convert.ToString(Waffendaten.APKosten[id]);
                        cool = cool.PadLeft(5, ' ');
                        spriteBatch.DrawString(Texturen.font2, cool,
                            new Vector2(
                                Mod.AKTUELLE_MUNITION_BUTTON_X.Wert + Texturen.waffenbilder[id].Width * scale -
                                Texturen.font2.MeasureString(cool).X - 5,
                                Mod.AKTUELLE_MUNITION_BUTTON_Y.Wert + Texturen.waffenbilder[id].Height * scale -
                                Texturen.font2.MeasureString(cool).Y - 5), Color.Red * 0.8f);

                        // Munitionsvorrat
                        if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] != 0 &&
                            Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] != 5)
                        {
                            cool = Convert.ToString(Spiel2.players[Spiel2.CurrentPlayer].Munition[tank][id]);
                            //   cool = cool.PadLeft(5, ' ');
                            spriteBatch.DrawString(Texturen.font2, cool,
                                new Vector2(Mod.AKTUELLE_MUNITION_BUTTON_X.Wert + 10,
                                    Mod.AKTUELLE_MUNITION_BUTTON_Y.Wert + 5), Color.PaleGoldenrod);
                        }
                    }

                    Spieler current = Spiel2.players[Spiel2.CurrentPlayer];
                    List<int> list = current.OrdneEigenePanzerAnhandDerKarte();
                    // Panzer die der Spieler besitzt
                    int curr = current.getPanzerID(current.CurrentTank, list) - 2;
                    if (list.Count <= leistenbreite)
                    {
                        curr = 0;
                    }
                    else
                    {
                        if (curr < 0)
                        {
                            curr = 0;
                        }
                        else if (curr + leistenbreite >= list.Count)
                        {
                            curr = list.Count - leistenbreite;
                        }
                    }

                    for (int i = 0; i < list.Count && i < leistenbreite; i++)
                    {
                        int b = list[i + curr];

                        if (Mod.LEISTE_BUTTON_VISIBLE.Wert)
                        {
                            spriteBatch.Draw(Texturen.LeeresFeld,
                                new Vector2(5 + (Texturen.LeeresFeld.Width * scale) * i,
                                    screenHeight - Texturen.LeeresFeld.Height * scale - 5), null, Color.White, 0,
                                new Vector2(0, 0), scale, SpriteEffects.None, 1);
                            spriteBatch.Draw(Texturen.panzerbutton[current.KindofTank[b]],
                                new Vector2(5 + (Texturen.LeeresFeld.Width * scale) * i,
                                    screenHeight - Texturen.LeeresFeld.Height * scale - 5), null, Color.White, 0,
                                new Vector2(0, 0), scale, SpriteEffects.None, 1);

                            Color r = Spiel2.players[Spiel2.CurrentPlayer].Farbe;
                            if (current.Effekte[b].GetEingefroren()) r = Color.Aquamarine;
                            if (current.Effekte[b].GetVergiftet()) r = Color.Lime;
                            r = Color.Gold;
                            r *= 0.8f;
                            spriteBatch.Draw(Texturen.panzerbutton[current.KindofTank[b]],
                                new Vector2(5 + (Texturen.LeeresFeld.Width * scale) * i,
                                    screenHeight - Texturen.LeeresFeld.Height * scale - 5), null, r, 0,
                                new Vector2(0, 0), scale, SpriteEffects.None, 1);
                        }

                        if (Mod.LEISTE_TANK_VISIBLE.Wert)
                        {
                            Color r = current.Farbe;
                            if (current.Effekte[b].GetEingefroren()) r = Color.Aquamarine;
                            if (current.Effekte[b].GetVergiftet()) r = Color.Lime;
                            r = Color.White;

                            /*float scale3 = current.Size[b] * 0.6f;
                            float scale4 = current.SizeOfCannon[b] * 0.6f;
                            int[] offset = { +10, 0, 0, +10,0,0 };
                            // Panzerrohre malen
                            spriteBatch.Draw(Texturen.panzerrohrindex[current.KindofTank[b]], new Vector2(5 + ((float)Texturen.load.Width * scale) * i + (float)Texturen.load.Width * scale / 2 - Texturen.panzerindex[current.KindofTank[b]].Width * scale3 / 2 + Texturen.panzerrohrindex[current.KindofTank[b]].Width * scale4 / 2 + offset[current.KindofTank[b]], screenHeight - (float)Texturen.load.Height * scale - 5 + 20), null, r, 0,
                                Texturen.CannonOrigin[current.KindofTank[b]][0], scale4,
                                SpriteEffects.FlipHorizontally, 1);
                            */
                            // Panzerbilder malen
                            //spriteBatch.Draw(Texturen.panzerindex[current.KindofTank[b]], new Vector2(5 + ((float)Texturen.load.Width * scale) * i + (float)Texturen.load.Width * scale / 2 - Texturen.panzerindex[current.KindofTank[b]].Width * scale3 / 2, screenHeight - (float)Texturen.load.Height * scale - 5 + 13), null, r, 0, new Vector2(0, 0), scale3, SpriteEffects.FlipHorizontally, 0);
                        }

                        // Cooldown
                        if (Spieler.WAFFEN_COOLDOWN.Wert)
                        {
                            int cool = Spiel2.players[Spiel2.CurrentPlayer].Cooldown[list[i]];
                            Color rr = Color.Gold;
                            //if (Spiel2.players[Spiel2.CurrentPlayer].freezed[list[i]] > 0) { cool = Spiel2.players[Spiel2.CurrentPlayer].freezed[list[i]]; rr = Color.Aquamarine; }
                            spriteBatch.DrawString(Texturen.font2, Convert.ToString(cool / 60),
                                new Vector2(5 + (Texturen.LeeresFeld.Width * scale) * i + 10,
                                    screenHeight - Texturen.LeeresFeld.Height * scale - 5 + 31), rr);
                        }

                        // Lebenslinie malen
                        if (Mod.LEISTE_LEBENSLINIE_VISIBLE.Wert)
                        {
                            int anteil = current.Effekte[b].GetHP(current.hp[b]);
                            var ges = (int)(((float)anteil / Fahrzeugdaten._MAXHP.Wert[current.KindofTank[b]]) * 250);
                            var a = new Rectangle(0, 0, ges, Texturen.leben.Height);
                            spriteBatch.Draw(Texturen.leben,
                                new Vector2(10 + (Texturen.LeeresFeld.Width * scale) * i,
                                    screenHeight - Texturen.LeeresFeld.Height * scale - 5 + 5), a, Color.Lime * 0.5f,
                                0, new Vector2(0, 0), 0.25f, SpriteEffects.FlipHorizontally, 1);
                        }
                    }

                    // Current_Tank Pfeil
                    if (Mod.LEISTE_AKTUELL_VISIBLE.Wert)
                    {
                        int curr2 = current.getPanzerID(current.CurrentTank, list);
                        spriteBatch.Draw(Texturen.pfeil,
                            new Vector2(
                                5 + (Texturen.LeeresFeld.Width * scale) * (curr2 - curr) +
                                (Texturen.LeeresFeld.Width * scale) / 2 - (Texturen.pfeil.Width * scale2) / 2,
                                screenHeight - Texturen.LeeresFeld.Height * scale - 5 -
                                Texturen.pfeil.Height * scale2), null, Color.White, 0, new Vector2(0, 0), scale2,
                            SpriteEffects.None, 1);
                    }
                }
        }

        /// <summary>
        ///     Zeichnet die Minen
        /// </summary>
        public void DrawMinen()
        {
            if (Spiel2 == null) return;

            int i = Spiel2.CurrentPlayer;
            for (int b = 0; b < Spiel2.players[i].Minen.Count; b++)
            {
                float scale = Spiel2.players[i].Minen[b].Skalierung;
                var x = (int)Spiel2.players[i].Minen[b].Position.X;
                var y = (int)Spiel2.players[i].Minen[b].Position.Y;

                Texture2D Bild = Spiel2.players[i].Minen[b].ErmittleBild();
                if (x == -1) continue;
                if (x + Bild.Width * scale < Spiel2.Fenster.X || Spiel2.Fenster.X + screenWidth < x - Bild.Width * scale)
                    continue;
                int xPos = x - (int)Spiel2.Fenster.X;
                var yPos = (int)(y - Spiel2.Fenster.Y);

                spriteBatch.Draw(Bild, new Vector2(xPos - (Bild.Width * scale) / 2, yPos), null, Color.White, 0,
                    new Vector2(0, Bild.Height), scale, SpriteEffects.None, 1);

                if (Editor.visible && Editor.mouseover == 6 && Editor.mouseoverid == i && Editor.mouseoverid2 == b)
                    spriteBatch.Draw(Bild, new Vector2(xPos - (Bild.Width * scale) / 2, yPos), null, Color.Blue, 0,
                        new Vector2(0, Bild.Height), scale, SpriteEffects.None, 1);
            }
        }

        /// <summary>
        ///     Zeichnet die Minekreisen
        /// </summary>
        public void DrawMinenKreis()
        {
            if (Spiel2 == null) return;

            int i = Spiel2.CurrentPlayer;
            for (int b = 0; b < Spiel2.players[i].Minen.Count; b++)
            {
                if (Spiel2.players[i].Minen[b].RadiusAnzeige <= 0) continue;
                Spiel2.players[i].Minen[b].RadiusAnzeige--;

                float scale = 1; // Spiel2.players[i].Minen[b].scale;
                var x = (int)Spiel2.players[i].Minen[b].Position.X;
                var y = (int)Spiel2.players[i].Minen[b].Position.Y;

                Texture2D Bild = Texturen.kreis;
                var Energie = (int)Waffendaten.Daten[Spiel2.players[i].Minen[b].Waffenart].X;
                if (x == -1) continue;
                if (x + Bild.Width * scale < Spiel2.Fenster.X || Spiel2.Fenster.X + screenWidth < x - Bild.Width * scale)
                    continue;
                int xPos = x - (int)Spiel2.Fenster.X;
                var yPos = (int)(y - Spiel2.Fenster.Y);
                float faktor = 1.0f;
                if (Spiel2.players[i].Minen[b].RadiusAnzeige <= 300)
                {
                    faktor = Spiel2.players[i].Minen[b].RadiusAnzeige / 300.0f;
                }

                spriteBatch.Draw(Bild, new Rectangle(xPos - Energie, yPos - Energie, Energie * 2, Energie * 2), null,
                    Color.Yellow * 0.75f * faktor, 0, Vector2.Zero, SpriteEffects.None, 1);
            }
        }

      
        /// <summary>
        ///     Zeichnet die Minimap Punkte, also die markierungen, wo sich ein Fahrzeug befindet
        /// </summary>
        public void DrawMinimapDot()
        {
            if (Spiel2 == null) return;
            if (Spiel2.CurrentPlayer == -1) return;
            int screenWidth2 = screenWidth * 4;
            var fensterx = (int)(Spiel2.Fenster.X - screenWidth2 / 2 + screenWidth / 2);
            float fact = 0.15f;
            Color[] playerC = { Color.Red, Color.Aqua };
            // DrawPlayer
            int i = Spiel2.CurrentPlayer;
            for (int b = 0; b < Spiel2.players[i].pos.Count; b++)
            {
                if (!Spiel2.players[i].isthere[b]) continue;
                if (Spiel2.players[i].pos[b].X < fensterx) continue;
                var x = (int)(Spiel2.players[i].pos[b].X - Texturen.dot.Width / 2 - fensterx);
                var y = (int)(Spiel2.players[i].pos[b].Y - Texturen.dot.Height / 2 - Spiel2.Fenster.Y);
                spriteBatch.Draw(Texturen.dot,
                    new Rectangle((int)(screenWidth - screenWidth2 * fact + x * fact - 1),
                        (int)(screenHeight - screenHeight * fact + y * fact - 1), (int)(Texturen.dot.Width * fact + 3),
                        (int)(Texturen.dot.Height * fact + 3)), new Rectangle(0, 0, 10, 10), Color.Black);

                spriteBatch.Draw(Texturen.dot,
                    new Vector2(screenWidth - screenWidth2 * fact + x * fact, screenHeight - screenHeight * fact + y * fact),
                    null, Spiel2.players[i].Farbe, 0.0f, new Vector2(0, 0), fact, SpriteEffects.None, 1);
            }
        }

        /// <summary>
        ///     Zeichnet die Raketen
        /// </summary>
        public void DrawMissle()
        {
            if (Spiel2 == null) return;
            for (int i = 0; i < Spiel2.Missile.Length; i++)
            {
                if (Spiel2.Missile[i].missleShot == false) continue;

                int xPos = (int)Spiel2.Missile[i].misslePosition.X - (int)Spiel2.Fenster.X;
                int yPos = (int)Spiel2.Missile[i].misslePosition.Y - (int)Spiel2.Fenster.Y;
                Color farbe = Color.White;

                if (Spiel.MISSILE_PLAYER_COLOR_VISIBLE.Wert)
                {
                    if (Spiel2.Missile[i].Besitzer[0] != 3 && Spiel2.Missile[i].Besitzer[0] >= 0)
                    // Spieler 3 ist nobody
                    {
                        farbe = Spiel2.players[Spiel2.Missile[i].Besitzer[0]].Farbe;
                    }
                }

                spriteBatch.Draw(Texturen.missle[Spiel2.Missile[i].Art], new Vector2(xPos, yPos), null, farbe,
                    Spiel2.Missile[i].missleAngle,
                    new Vector2(0, Texturen.missle[Spiel2.Missile[i].Art].Height / 2) *
                    Waffendaten.Skalierung[Spiel2.Missile[i].Art], Waffendaten.Skalierung[Spiel2.Missile[i].Art],
                    SpriteEffects.None, 1); // 190, 40

                if (Spiel2.Missile[i].Art == 5) //  || Spiel2.Missile[i].Art == 6 || Spiel2.Missile[i].Art == 7
                {
                    xPos = (int)Spiel2.Missile[i].misslePosition.X - (int)Spiel2.Fenster.X;
                    // spriteBatch.Draw(Texturen.kreuz, new Vector2(xPos, (int)(Spiel2.Spielfeld[(int)Spiel2.Missile[i].misslePosition.X] - Spiel2.Fenster.Y)), null, Color.Blue, 0, new Vector2(35 * 0.5f, 35 * 0.5f), 0.5f, SpriteEffects.None, 1);
                    spriteBatch.Draw(Texturen.kreuz,
                        new Vector2(xPos,
                            (int)(Kartenformat.BottomOf(Spiel2.Missile[i].misslePosition) - Spiel2.Fenster.Y)), null,
                        Color.Blue, 0, new Vector2(35 * 0.5f, 35 * 0.5f), 0.5f, SpriteEffects.None, 1);
                }

                if (Mod.MISSILE_STRICH_VISIBLE.Wert)
                    if (Spiel2.Missile[i].misslePosition.Y < 0)
                    {
                        xPos = (int)Spiel2.Missile[i].misslePosition.X - (int)Spiel2.Fenster.X;
                        spriteBatch.Draw(Texturen.strich, new Vector2(xPos, 0), null, Color.White, 0, new Vector2(3, 0),
                            0.5f, SpriteEffects.None, 1);
                    }
            }
        }

        /// <summary>
        ///     Draws the mouse.
        /// </summary>
        public void DrawMouse()
        {
            spriteBatch.Draw(Texturen.mausZeiger, Help.GetMousePos(), null, Color.Orange, 0,
                new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
        }

        /// <summary>
        ///     Draws the ortsschilder.
        /// </summary>
        public void DrawOrtsschilder()
        {
            for (int i = 0; i < Haus.Orte.Count; i++)
            {
                float posx = Haus.Orte[i].X + Haus.Orte[i].Y / 2 - Spiel2.Fenster.X;
                if (posx + 100 < 0 || posx >= Spiel2.Width + 100) continue;
                float posy = Haus.Ortemaxheight[i] - 50;
                spriteBatch.Draw(Texturen.Ort, new Vector2(posx - 50, posy), null, Color.White * 0.4f, 0,
                    new Vector2(0, 0), 1f, SpriteEffects.None, 0);
                String Name = Haus.Ortsname[i];
                try
                {
                    Vector2 size = Texturen.font2.MeasureString(Name);
                    spriteBatch.DrawString(Texturen.font2, Name, new Vector2(posx - size.X / 2, posy - size.Y / 2 + 14),
                        Color.Black * 0.7f);
                }
                catch (Exception)
                {
                    // nichts
                }
            }
        }

        /// <summary>
        ///     Zeichnet die Spieler, also die Fahrzeuge
        /// </summary>
        /// <param name="found">if set to <c>true</c> [found].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool DrawPlayers(bool found)
        {
            if (Spiel2 == null || Spiel2.players[0].pos.Count == 0) return false;

            for (int i = 0; i < Spiel2.players.Length; i++)
            {
                for (int b = 0; b < Spiel2.players[i].pos.Count; b++)
                {
                    if (Spiel2.players[i].isthere[b])
                    {
                        spriteBatch.Begin();
                        if (Spiel2.CurrentPlayer == i && Spiel2.players[Spiel2.CurrentPlayer].CurrentTank == b &&
                            Spiel2.players[Spiel2.CurrentPlayer].Effekte[b].GibZielhilfe()>0 &&
                            Spiel2.players[Spiel2.CurrentPlayer].shootingPower >= 2 &&
                            Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 1)
                        {

                            int Zielhilfewert = Spiel2.players[Spiel2.CurrentPlayer].Effekte[b].GibZielhilfe();
                            // Zeichne Flugbahn
                            Vector2 a = Spiel2.players[i].pos[b] - Spiel2.Fenster;
                            Vector2 a2 = Spiel2.players[i].pos[b] - Spiel2.Fenster;
                            a.Y -=
                                (float)
                                    Math.Sin(
                                        Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                        Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75 + 25;
                            a.X -=
                                (float)
                                    Math.Cos(
                                        Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                        Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75;

                            var up = new Vector2(0, -1);
                            Matrix rotMatrix =
                                Matrix.CreateRotationZ(
                                    Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                    Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] - MathHelper.PiOver2);
                            Vector2 c = Vector2.Transform(up, rotMatrix);
                            c *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower /
                                 (float)Math.Log(Spiel2.players[Spiel2.CurrentPlayer].shootingPower, Math.E);
                            getBahn(Waffen.gravity / 10.0f + (Spiel.WIND.Wert ? Spiel2.Wind / 60.0f : Vector2.Zero), c, a,
                                screenHeight, (int)(a2.X - Zielhilfewert), (int)(a2.X + Zielhilfewert), spriteBatch);
                        }

                        Color r = Color.White;
                        if (Spiel2.players[i].Effekte[b].GetVergiftet()) r = Color.Lime;
                        if (Spiel2.players[i].Effekte[b].GetEingefroren()) r = Color.Aquamarine;

                        Color r2 = Spiel2.players[i].Farbe;
                        r2 *= 1f;

                        int xPos = (int)Spiel2.players[i].pos[b].X - (int)Spiel2.Fenster.X;
                        int yPos = (int)Spiel2.players[i].pos[b].Y - (int)Spiel2.Fenster.Y;
                        float auxfliphelper = MathHelper.ToRadians(180);

                        // TODO das ist scheisse, mit festem origin

                        var xPos2 = (int)(Math.Sin(Spiel2.players[i].vehikleAngle[b]) * -25);

                        // Lebenslinie malen
                        if (Mod.LEBENSLINIE_ON_TANK_VISIBLE.Wert)
                        {
                            if (Spiel2.CurrentPlayer == i)
                            {
                                var a = new Rectangle(0, 0, 300, Texturen.leben.Height);
                                Vector2 p = Help.RotatePositionOffset(new Vector2(xPos, yPos),
                                    Spiel2.players[i].vehikleAngle[b],
                                    new Vector2(75 / 2,
                                        Texturen.panzerindex[Spiel2.players[i].KindofTank[b]].Height *
                                        Fahrzeugdaten.SCALEP.Wert[Spiel2.players[i].KindofTank[b]] + 10));
                                spriteBatch.Draw(Texturen.leben, p, a, Color.Red * 0.5f, Spiel2.players[i].vehikleAngle[b],
                                    Vector2.Zero, 0.25f, SpriteEffects.FlipHorizontally, 1);

                                int anteil = Spiel2.players[i].Effekte[b].GetHP(Spiel2.players[i].hp[b]);
                                int max =
                                    Spiel2.players[i].Effekte[b].GetMaxHP(
                                        Fahrzeugdaten._MAXHP.Wert[Spiel2.players[i].KindofTank[b]]);
                                if (anteil > max) anteil = max;
                                var ges = (int)(((float)anteil / max) * 300);
                                a = new Rectangle(0, 0, ges, Texturen.leben.Height);

                                spriteBatch.Draw(Texturen.leben, p, a, Color.Lime * 0.5f,
                                    Spiel2.players[i].vehikleAngle[b],
                                    Vector2.Zero, 0.25f, SpriteEffects.FlipHorizontally, 1);
                            }
                        }

                        // Erfahrungspunktelinie malen
                        if (Spiel2.CurrentPlayer == i && Spiel2.players[i].ExpNow[b] > 0 &&
                            Spiel2.players[i].CurrentLv[b] < 3)
                        {
                            var a = new Rectangle(0, 0, 300, Texturen.leben.Height);
                            Vector2 p = Help.RotatePositionOffset(
                                new Vector2(xPos, yPos + Texturen.leben.Height * 0.25f), Spiel2.players[i].vehikleAngle[b],
                                new Vector2(75 / 2,
                                    Texturen.panzerindex[Spiel2.players[i].KindofTank[b]].Height *
                                    Fahrzeugdaten.SCALEP.Wert[Spiel2.players[i].KindofTank[b]] + 10));

                            //   spriteBatch.Draw(Texturen.leben, p, a, Color.Red * 0.25f, Spiel2.players[i].vehikleAngle[b],
                            // Vector2.Zero, 0.25f, SpriteEffects.FlipHorizontally, 1);

                            int anteil = Spiel2.players[i].ExpNow[b];

                            int max = Fahrzeugdaten.ExpToLvUpVar[Spiel2.players[i].KindofTank[b], 0];
                            if (anteil > max) anteil = max;
                            var ges = (int)(((float)anteil / max) * 300);
                            a = new Rectangle(0, 0, ges, Texturen.leben.Height);

                            spriteBatch.Draw(Texturen.leben, p, a, Color.Blue * 0.6f, Spiel2.players[i].vehikleAngle[b],
                                Vector2.Zero, 0.25f, SpriteEffects.FlipHorizontally, 1);
                        }
                        spriteBatch.End();

                        // Spieler zeichnen
                        bool Besitzer = false;
                        if (i == Spiel2.CurrentPlayer) Besitzer = true;

                        if (Editor.visible && Editor.mouseover == 3 && Editor.mouseoverid == i &&
                            Editor.mouseoverid2 == b)
                        {
                            Spiel2.players[i].DrawPlayer(b, spriteBatch, Spiel2.players[i].Angle[b],
                                Spiel2.players[i].vehikleAngle[b],
                                Fahrzeugdaten.SCALEP.Wert[Spiel2.players[i].KindofTank[b]],
                                Fahrzeugdaten.SCALER.Wert[Spiel2.players[i].KindofTank[b]], Spiel2.players[i].pos[b],
                                Spiel2.Fenster, Spiel2.players[i].overreach[b], true, 1.0f, 1.0f, Color.Blue, Besitzer);
                        }
                        else
                            Spiel2.players[i].DrawPlayer(b, spriteBatch, Spiel2.players[i].Angle[b],
                                Spiel2.players[i].vehikleAngle[b],
                                Fahrzeugdaten.SCALEP.Wert[Spiel2.players[i].KindofTank[b]],
                                Fahrzeugdaten.SCALER.Wert[Spiel2.players[i].KindofTank[b]], Spiel2.players[i].pos[b],
                                Spiel2.Fenster, Spiel2.players[i].overreach[b],
                                (i == Spiel2.CurrentPlayer && b == Spiel2.players[Spiel2.CurrentPlayer].CurrentTank &&
                                 !Editor.visible)
                                    ? true
                                    : false, 1.0f, 1.0f, Color.DarkGoldenrod, Besitzer);

                        spriteBatch.Begin();
                        if (!Editor.visible)
                            if (!found && i == Spiel2.CurrentPlayer)
                            {
                                if (Tausch.SpielAktiv)
                                    if (Spiel2.players[i].PrüfeObKollision(b,
                                        new Vector2(mouseState.X + Spiel2.Fenster.X, mouseState.Y + Spiel2.Fenster.Y)))
                                    {
                                        int Kindof = Spiel2.players[i].KindofTank[b];
                                        spriteBatch.Draw(Texturen.panzerumriss[Kindof], new Vector2(xPos, yPos), null,
                                            Color.OrangeRed, Spiel2.players[i].vehikleAngle[b],
                                            new Vector2(Texturen.panzerumriss[Kindof].Width / 2,
                                                Texturen.panzerumriss[Kindof].Height), Fahrzeugdaten.SCALEP.Wert[Kindof],
                                            Spiel2.players[i].overreach[b]
                                                ? SpriteEffects.FlipHorizontally
                                                : SpriteEffects.None, 0);
                                        found = true;
                                    }
                            }

                        // shootingpower
                        if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] != 4 &&
                            Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] != 5)
                            if (Spiel2.players[i].CurrentWeapon != 5 && Spiel2.players[i].CurrentTank == b &&
                                Spiel2.CurrentPlayer == i && !Spiel2.increaseairstrike &&
                                Spiel2.players[i].shootingPower > 2)
                            {
                                /*  int dicke = 4;
                                  int c = b;
                                  int kind = Spiel2.players[i].KindofTank[c];
                                  int welcher = Spiel2.players[i].overreach[c] ? 1 : 0;
                                  Vector2 kraft = new Vector2(Spiel2.players[i].shootingPower+5, 0);

                                  Vector2 posi = Spiel2.players[i].pos[c] + new Vector2(welcher == 1 ? -Texturen.RohrPos[kind].X : Texturen.RohrPos[kind].X, -Texturen.RohrPos[kind].Y);
                                  float temp2 = Texturen.CannonOrigin[kind][0].Y/2 -dicke/2 ;

                                  Vector2 p = Help.RotatePositionOffset(posi, Spiel2.players[i].Angle[c], new Vector2(Texturen.panzerrohrindex[kind].Width - (Texturen.panzerrohrindex[kind].Width - Texturen.CannonOrigin[kind][0].X), welcher == 0 ? temp2 : temp2)+ new Vector2(5,0));
                                  p = Help.RotatePosition(Spiel2.players[i].pos[c], Spiel2.players[i].vehikleAngle[c], p);

                                  Vector2 p2 = Help.RotatePositionOffset(posi, Spiel2.players[i].Angle[c], new Vector2(Texturen.panzerrohrindex[kind].Width - (Texturen.panzerrohrindex[kind].Width - Texturen.CannonOrigin[kind][0].X), welcher == 0 ? temp2 : temp2) +kraft);
                                  p2 = Help.RotatePosition(Spiel2.players[i].pos[c], Spiel2.players[i].vehikleAngle[c], p2);

                                  p -= Spiel2.Fenster;
                                  p2 -= Spiel2.Fenster;
                                  Help.DrawLine(spriteBatch, p, p2, Color.Goldenrod*0.3f, dicke);
                */
                            }

                        if (Besitzer && Mod.NAME_TANK_VISIBLE.Wert)
                        {
                            try
                            {
                                // Zeichne Namen
                                Vector2 temppos = Spiel2.players[i].pos[b] + new Vector2(0, -100) - Spiel2.Fenster;
                                var add =
                                    new Vector2(
                                        temppos.X - Texturen.font2.MeasureString(Spiel2.players[i].Namen[b]).X / 2,
                                        (temppos.Y < 50 ? 50 : temppos.Y));
                                Help.DrawString(spriteBatch, Texturen.font2, Spiel2.players[i].Namen[b], add,
                                    Spiel2.players[i].Farbe, Color.Black);
                                // spriteBatch.DrawString(Texturen.font2, Spiel2.players[i].Namen[b], add + new Vector2(1, 1), Color.Black);
                                // spriteBatch.DrawString(Texturen.font2, Spiel2.players[i].Namen[b], add, Spiel2.players[i].Farbe);
                            }
                            catch (Exception)
                            {
                                // nichts
                            }
                        }

                        spriteBatch.End();
                    }
                }
            }
            return found;
        }

        /// <summary>
        ///     Draws the players cooldown.
        /// </summary>
        public void DrawPlayersCooldown()
        {
            if (Spiel2 == null || Spiel2.players[0].pos.Count == 0) return;
            for (int i = 0; i < Spiel2.players.Length; i++)
            {
                for (int b = 0; b < Spiel2.players[i].pos.Count; b++)
                {
                    int cool = Spiel2.players[i].Cooldown[b];
                    if (Spiel2.players[i].isthere[b] && cool > 0)
                    {
                        int xPos = (int)Spiel2.players[i].pos[b].X - (int)Spiel2.Fenster.X;
                        int yPos = (int)Spiel2.players[i].pos[b].Y - (int)Spiel2.Fenster.Y;
                        float auxfliphelper = MathHelper.ToRadians(180);
                        var cannonOrigin = new Vector2(300, 40);
                        var cannonOrigin2 = new Vector2(160, 40);
                        var xPos2 = (int)(Math.Sin(Spiel2.players[i].vehikleAngle[b]) * -25);

                        Color r = Color.Goldenrod;
                        if (Spiel2.players[i].Effekte[b].GetEingefroren())
                        {
                            r = Color.Aquamarine;
                        }
                        float correkt = Texturen.panzerindex[Spiel2.players[i].KindofTank[b]].Height *
                                        Spiel2.players[i].Size[b];
                        spriteBatch.DrawString(Texturen.font, Convert.ToString(Math.Ceiling((double)cool / 60)),
                            new Vector2(xPos, yPos - correkt), r);
                    }
                }
            }
        }

        /// <summary>
        ///     Zeichnet den Hintergrund
        /// </summary>
        public void DrawSceneryBackground()
        {
            if (Spiel2 == null) return;
            if (Spiel2.Fenster.X < 0) Spiel2.Fenster.X = 0;
            if (Spiel2.Fenster.Y < 0) Spiel2.Fenster.Y = 0;

            // die geschwindigkeit, mit der sich das Hintergrundbild bewegt (im Vergleich zur Kartenbewegung)
            // bewegungsFaktor > 1 -> Bewegt sich entgegen der Kartenbewegung
            // bewegungsFaktor <= 1 -> Bewegt sich mit der Kartebewegung
            float bewegungsFaktor = .1f;

            int ww = screenWidth;

            int x = (int)(Spiel2.Fenster.X * bewegungsFaktor) % ww - screenWidth;
            int y = (int)(Spiel2.Fenster.Y * bewegungsFaktor) % screenHeight;

            while (x < screenWidth)
            {
                var a = new Rectangle(x, y, ww, screenHeight);
                spriteBatch.Draw(Texturen.activeBackground, a, Color.White);
                x += ww;
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
            float bewegungsFaktor2 = .05f;

            int x2 = (int)(Spiel2.Fenster.X * bewegungsFaktor2) % ww - screenWidth;
            int y2 = (int)(Spiel2.Fenster.Y * bewegungsFaktor2 - screenHeight * .75f) % screenHeight;

            int oldx2 = x2;
            while (y2 < screenHeight)
            {
                while (x2 < screenWidth)
                {
                    var a = new Rectangle(x2, y2, ww, screenHeight);
                    spriteBatch.Draw(Texturen.activeBackground, a, Color.White * .5f);
                    x2 += ww;
                }
                y2 += screenHeight;
                x2 = oldx2;
            }

            float bewegungsFaktor3 = .075f;

            int x3 = (int)((Spiel2.Fenster.X + WolkenPos.X) * bewegungsFaktor3) % ww - screenWidth;
            int y3 = (int)((Spiel2.Fenster.Y + WolkenPos.Y) * bewegungsFaktor3 - screenHeight * .3f) % screenHeight -
                     screenHeight;

            int oldx3 = x3;
            while (y3 < screenHeight)
            {
                while (x3 < screenWidth)
                {
                    var a = new Rectangle(x3, y3, ww, screenHeight);
                    spriteBatch.Draw(Texturen.activeBackground, a, Color.White * .5f);
                    x3 += ww;
                }
                y3 += screenHeight;
                x3 = oldx3;
            }

            float xx = Spiel2.Wind.X;
            if (xx < 1 && xx > 0) xx = 1;
            if (xx > -1 && xx < 0) xx = -1;

            WolkenPos.X += xx * 1.5f;
        }

        /// <summary>
        ///     Draws the smoke.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void DrawSmoke(GameTime gameTime)
        {
            for (int i = 0; i < smokeList.Count; i++)
            {
                if (!smokeList[i].set || !smokeList[i].alive) continue;

                Karte.ParticleData particle = smokeList[i];
                int xPos = (int)particle.Position.X - (int)Spiel2.Fenster.X;
                int yPos = (int)particle.Position.Y - (int)Spiel2.Fenster.Y;
                spriteBatch.Draw(Texturen.smokeTexture, new Vector2(xPos, yPos), null, particle.ModColor, i,
                    new Vector2(256, 256), particle.Scaling, SpriteEffects.None, 1);
            }

            for (int i = 0; i < Spiel2.Karte.particleListMapSmoke.Count; i++)
            {
                if (!Spiel2.Karte.particleListMapSmoke[i].set || !Spiel2.Karte.particleListMapSmoke[i].alive) continue;

                Karte.ParticleData particle = Spiel2.Karte.particleListMapSmoke[i];

                int xPos = (int)particle.Position.X - (int)Spiel2.Fenster.X;
                int yPos = (int)particle.Position.Y - (int)Spiel2.Fenster.Y;
                spriteBatch.Draw(Texturen.smokeTexture, new Vector2(xPos, yPos), null, particle.ModColor, i,
                    new Vector2(256, 256), particle.Scaling, SpriteEffects.None, 1);
            }
        }

        /// <summary>
        ///     Zeichnet den Spielstatustext
        /// </summary>
        public void DrawText()
        {
            if (Spiel2 == null) return;
            if (Spiel2.CurrentPlayer == -1) return;
            Spieler player = Spiel2.players[Spiel2.CurrentPlayer];
            if (Spiel.TIMEOUT.Wert)
            {
                String t = Convert.ToString(Spiel2.Timeout / 60);
                spriteBatch.DrawString(Texturen.font, Spiel2.Timeout > 0 ? t : "---", new Vector2(25, screenHeight - 35),
                    Color.Gold);
            }
            //  spriteBatch.DrawString(Texturen.font, Spiel2.Fenster.X.ToString(), new Vector2(200, 35), Color.Gold);
        }

        /// <summary>
        ///     Zeichnet den Spielendetext
        /// </summary>
        public void DrawTextEnd()
        {
            if (Editor.visible) return;
            if (Spiel2 == null) return;
            if (Spiel2.CurrentPlayer == -1) return;
            String[] bez = { "Rot", "Blau" };
            int Gewinner = Spiel2.Gewinner();
            Spieler player = Spiel2.players[Gewinner];
            spriteBatch.DrawString(Texturen.font, bez[Gewinner] + " hat gewonnen...",
                new Vector2(screenWidth / 2 - 100, 100), Color.Gold);
            spriteBatch.DrawString(Texturen.font, "Esc: Beenden", new Vector2(screenWidth / 2 - 100, 150), Color.Gold);
        }

        /// <summary>
        ///     Zeichnet den Wind
        /// </summary>
        public void DrawWind()
        {
            bool flip = false;
            int anteil;
            if (Spiel2.Wind.X != 0)
            {
                anteil = (int)((double)Texturen.wind.Width / 120 * Spiel2.Wind.X * 20);
            }
            else
                anteil = 0;
            if (anteil < 0)
            {
                anteil = -anteil;
                flip = true;
            }
            anteil = Texturen.wind.Width - anteil;
            Rectangle a;
            a = new Rectangle(anteil, 0, Texturen.wind.Width - anteil, Texturen.wind.Height);

            var xPos = (int)(-Texturen.wind.Width * Optimierung.Skalierung(0.5f));
            int yPos = 65 - 15;
            if (flip)
            {
                xPos =
                    (int)
                        ((double)screenWidth + 2 * xPos +
                         (Texturen.wind.Width * 0.5f / 120) * (120 - ((double)-Spiel2.Wind.X * 20)));

                // TODO Origin geändert von new Vector2(35,35) auf new Vector2(0,0), richtig?
                spriteBatch.Draw(Texturen.wind, new Vector2(xPos, yPos), a, Color.Green, 0, new Vector2(0, 0),
                    Optimierung.Skalierung(0.5f), SpriteEffects.FlipHorizontally, 1);
            }
            else
            {
                xPos = screenWidth + xPos;
                // TODO Origin geändert von new Vector2(35,35) auf new Vector2(0,0), richtig?
                spriteBatch.Draw(Texturen.wind, new Vector2(xPos, yPos), a, Color.Green, 0, new Vector2(0, 0),
                    Optimierung.Skalierung(0.5f), SpriteEffects.None, 1);
            }
        }

        public static int SpielEinblenden = 0;
        public static int SpielEinblendenMax = 0;
        public static int SpielAusblenden = 0;
        public static int SpielAusblendenMax = 0;
        public static float SpielBlend = 0;

        /// <summary>
        ///     Loads all content.
        /// </summary>
        public void loadAllContent()
        {
            TastaturDeutsch.Clear();
            TastaturDeutsch.Add(this.OnKeyPress);
            baseScreenSize = new Vector2(XWERT, YWERT); //new Vector2(1000, 600);
            if (spriteBatch == null)
                spriteBatch = new SpriteBatch(GraphicsDevice);

            //screenWidth = device.PresentationParameters.BackBufferWidth;
            //screenHeight = device.PresentationParameters.BackBufferHeight;
            //resolution independent
            //   Hauptfenster.Tausch.screenwidth = 1024;
            //   Hauptfenster.Tausch.screenheight = 768;

            screenWidth = Tausch.screenwidth; // (int)baseScreenSize.X;
            screenHeight = Tausch.screenheight; // (int)baseScreenSize.Y;

            Nutzloses.AlleEntfernen();
            Fahrzeugdaten.LadePanzerdaten();
            Tunnel.LadeTunnelDaten();
            Texturen.Load(Content);
            Sounds.Load(Content);
            Karte.Lade_Materialien(Content);
            Notizen.LoadContent();

            Meldungen = new Chatbox(new Vector2(15, 60));

            #region LadeMenus

            if (Mod.PAUSEMENU_VISIBLE.Wert)
                pauseMenu = new Menu(Color.Goldenrod, Color.Gold, Texturen.AlgerianFont, screenWidth, screenHeight);
            Ladenmenu = new Lademenu(Color.Goldenrod, Color.Gold, Texturen.AlgerianFont, screenWidth, screenHeight, 1);
            StartMenu = new Startmenu(Color.Goldenrod, Color.Gold, Texturen.AlgerianFont, screenWidth, screenHeight);
            SetUpMenu = new SetupMenu(Color.White, Color.Gold, Texturen.AlgerianFont, screenWidth, screenHeight);
            //Spielermenu = new SpielerMenu(Color.White, Color.White, Texturen.font2, screenWidth, screenHeight);
            if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu = new Spielermenu();
            if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu.LoadContent(Content);
            Help.angrabbel_funktion();

            #endregion LadeMenus

            Spiel2 = null;
            menuaufruf = false;

            water = Farbwahl(Texturen.wasser);
        }

        /// <summary>
        ///     Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Hauptfenster.Program.Formular.pictureBox1.Visible) return;

            if (Spiel2 != null)
                if (Spiel2.players[Spiel2.CurrentPlayer].Notiz.schreibend)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].Notiz.OnKeyPress(sender, e);
                }

            if (Spiel2 != null)
                if (Editor.Textfelder.visible)
                {
                    Editor.Textfelder.OnKeyPress(sender, e);
                }

            if (Mod.EINGABEZEILE_VISIBLE.Wert) Eingabefenster.Eingabe.OnKeyPress(sender, e);
            SetUpMenu.OnKeyPress(sender, e);
            Ladenmenu.OnKeyPress(sender, e);
        }

        Task StarteNeuesSpielTask = null;
        /// <summary>
        ///     Startes the neues spiel.
        /// </summary>
        public void StarteNeuesSpiel()
        {
            Action<object> StarteNeuesSpiel = (object obj) =>
            {
                if (Sounds.Hintergrundmusik != null)
                    Sounds.Hintergrundmusik.StopSound(0);

                Sounds.Lademusik = new Soundsystem("Sounds\\138681__haydensayshi123__battle-march-action-loop.ogg",
                    0.15f,
                    1f, false);
                Sounds.Lademusik.PlaySound(0);

                /*Hauptfenster.Program.Formular.progressBar1.Value = 0;
            Hauptfenster.Program.Formular.progressBar1.Show();
            Hauptfenster.Program.Formular.progressBar1.BringToFront();

            Hauptfenster.Program.Formular.label31.Show();
            Hauptfenster.Program.Formular.label31.BringToFront();*/
                Spiel2 = null;
                Ladebildschirmtexte = null;

                // Reset
                Karte.Reset_Materialien();
                Fahrzeugdaten.Reset_Tankdata();

                LadeText("    Tastatur...    ");
                // Tastatur laden
                Tastatur.LadeTastaturbelegung("Content\\Konfiguration\\Tastatur.conf");
                // Hauptfenster.Program.Formular.progressBar1.Value = 10;

                LadeText("    Mod...    ");
                // Mod laden
                Mod.LadeModVariablen("Content\\Konfiguration\\" + Tausch.Mod);
                //Hauptfenster.Program.Formular.progressBar1.Value = 20;

                LadeText("    Komponenten...    ");
                // Komponenten laden
                loadAllContent();
                //Hauptfenster.Program.Formular.progressBar1.Value = 60;

                LadeText("    Spiel erstellen...    ");
                // Spiel erstellen
                CreateNeuesSpiel();
                //Hauptfenster.Program.Formular.progressBar1.Value = 90;

                LadeText("    Umgebung...    ");
                //// vordergrund = Farbwahl(Texturen.tilltexture);
                water = Farbwahl(Texturen.wasser);
                Vordergrund.ErstelleVordergrund();
                Fog.CreateFog();
                Mine.Initialisierung(Content);
                Eingabefenster.Initialisieren();
                createKasten();
                Feuer.Initialisieren(Spiel2.Spielfeld.Count());
                if (Spiel.SCHUESSE.Wert) Spiel2.Schuesse = Spiel2.players[Spiel2.CurrentPlayer].MaxSchuesse;
                Spiel2.InitRunde();
                //Mine.init(Content);
                Help.angrabbel_funktion();
                if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu.show();
                Eingabefenster.Eingabe.Anzeigen();
                Matrix globalTransformation = Matrix.CreateScale(0);

                SpriteBatchSemaphor.WaitOne();
                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend, null, null, null, null, globalTransformation);
                if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu.Draw(spriteBatch, Texturen.font3, null, null, null, 0.5f);
                spriteBatch.Draw(Texturen.mausZeiger, new Vector2(mouseState.X, mouseState.Y), null, Color.Orange, 0,
                    new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
                Eingabefenster.ZeichneEingabefenster(spriteBatch);
                spriteBatch.End();
                SpriteBatchSemaphor.Release();

                Eingabefenster.Eingabe.Verstecken();
                if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu.hide();
                //Hauptfenster.Program.Formular.progressBar1.Value = 100;
                // Hauptfenster.Program.Formular.progressBar1.Hide();
                // Hauptfenster.Program.Formular.label31.Hide();

                LadeText(null);
                SpielfeldEinblenden(180);

                Sounds.Lademusik.StopSound(0);
                LadebildschirmAktiv = false;
            };

            if (StarteNeuesSpielTask == null || StarteNeuesSpielTask.IsCompleted)
            {
                StarteNeuesSpielTask = new Task(StarteNeuesSpiel, "StarteNeuesSpiel");
                StarteNeuesSpielTask.Start();
            }
        }

        bool _isDirty = true;
        protected override bool BeginDraw()
        {
            if (_isDirty)
            {
                _isDirty = false;
                SpriteBatchSemaphor.WaitOne();
                return base.BeginDraw();
            }
            else
                return false;
        }

        protected override void EndDraw()
        {
                base.EndDraw();
                SpriteBatchSemaphor.Release();
        }

        public static Texture2D LadeHintergrund = null;
        public static Texture2D LadeHintergrundBalken = null;
        private Action<object> Ladebildschirm = (object obj) =>
        {
            /*for (;;)
            {
                if (Game1.Spiel2 == null || true)
                {
                    if (LadeHintergrund == null)
                        LadeHintergrund = Game1.ContentAll.Load<Texture2D>("Textures\\Ladebildschirm");
                    if (Game1.spriteBatch == null)
                        Game1.spriteBatch = new SpriteBatch(Game1.device);

                    Game1.device.Clear(Color.Black);
                    Game1.spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                    Game1.spriteBatch.Draw(LadeHintergrund, new Vector2(0, 0), Color.White);
                    Game1.spriteBatch.End();
                }
                Thread.Sleep(100);
            }*/

        };

        public static int LadebildschirmPosition = 0;
        public static int LadebildschirmPositionMax = 100;
        public static bool LadebildschirmPositionLoop = true;
        public static Semaphore SpriteBatchSemaphor = new Semaphore(1, 1);
        public static bool LadebildschirmAktiv = false;
        public static String LadebildschirmText = "";
        public static bool LadebildschirmRichtung = false;

        /// <summary>
        ///     Reference page contains code sample.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        protected override void Draw(GameTime gameTime) // Ruft alle Draw Methoden auf
        {
            if (Game1.Spiel2 == null || LadebildschirmAktiv)
            {
                SpriteFont Schriftart = Texturen.font;
                if (LadeHintergrund == null)
                    LadeHintergrund = Game1.ContentAll.Load<Texture2D>("Textures\\Ladebildschirm");
                if (LadeHintergrundBalken == null)
                    LadeHintergrundBalken = Game1.ContentAll.Load<Texture2D>("Textures\\Ladebildschirm2");
                if (Game1.spriteBatch == null)
                    Game1.spriteBatch = new SpriteBatch(Game1.device);
                if (Schriftart == null)
                    Schriftart = Game1.ContentAll.Load<SpriteFont>("Fonts\\myfont");

                Vector2 Verschiebung = new Vector2(Game1.screenWidth / 2 - LadeHintergrund.Width / 2, Game1.screenHeight / 2 - LadeHintergrund.Height / 2);

                Vector2 Verschiebung2 = new Vector2(Game1.screenWidth / 2 - LadeHintergrundBalken.Width / 2+3, Game1.screenHeight / 2 - LadeHintergrundBalken.Height / 2);

                // && Game1.device.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal
                //Game1.device.DepthStencilState.DepthBufferEnable
                //if (Game1.device.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal)
                    //Game1.device.Clear(Color.Black);
                    //GraphicsDevice.Clear(Color.Black);
                    GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0F, 0);

                if (LadebildschirmPositionLoop)
                {
                    LadebildschirmPositionMax = 100;
                    if (LadebildschirmPosition > LadebildschirmPositionMax)
                        LadebildschirmPosition = 0;
                }

                // den Ladebalken bestimmen

                double faktor = (double)LadeHintergrundBalken.Width/LadebildschirmPositionMax;

                Rectangle AnfangsBereich=new Rectangle(0,0,0,0);
                if (LadebildschirmPosition < 10)
                {
                    AnfangsBereich = new Rectangle((int) ((float) faktor*0), 0,
                        (int) ((float) faktor*LadebildschirmPosition), LadeHintergrundBalken.Height);
                }
                else
                {
                    int LadebildschirmPosition2 = LadebildschirmPosition - 10;
                    AnfangsBereich = new Rectangle((int) ((float) faktor*LadebildschirmPosition2), 0,
                        (int)
                            ((float) faktor*
                             (LadebildschirmPosition2 > 90 ? (LadebildschirmPositionMax - LadebildschirmPosition2) :
                    10)),
                    LadeHintergrundBalken.Height)
                    ;
                }

                Game1.spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                //Game1.spriteBatch.Draw(LadeHintergrundBalken, new Rectangle((int)((float)Verschiebung2.X + faktor * (LadebildschirmPosition < 10 ? 0 : LadebildschirmPosition-10)), (int)Verschiebung2.Y, AnfangsBereich.Width, AnfangsBereich.Height), AnfangsBereich, Color.White);

                if (!LadebildschirmRichtung)
                {
                    Game1.spriteBatch.Draw(LadeHintergrundBalken,
                        new Rectangle((int) ((float) Verschiebung2.X), (int) Verschiebung2.Y,
                            (int) (LadebildschirmPosition*faktor), LadeHintergrundBalken.Height),
                        new Rectangle(0, 0, (int) (LadebildschirmPosition*faktor), LadeHintergrundBalken.Height),
                        Color.White);
                } else
                    Game1.spriteBatch.Draw(LadeHintergrundBalken,
                        new Rectangle((int)((float)Verschiebung2.X + (LadebildschirmPositionMax-LadebildschirmPosition) * faktor), (int)Verschiebung2.Y,
                            (int)((LadebildschirmPosition) * faktor), LadeHintergrundBalken.Height),
                        new Rectangle((int)((LadebildschirmPositionMax-LadebildschirmPosition) * faktor), 0, (int)((LadebildschirmPosition) * faktor), LadeHintergrundBalken.Height),
                        Color.White);

                Game1.spriteBatch.Draw(LadeHintergrund, new Vector2((int)Verschiebung.X, (int)Verschiebung.Y), Color.White);

                Help.DrawString(Game1.spriteBatch, Schriftart, LadebildschirmText, new Vector2(Game1.screenWidth / 2 - Schriftart.MeasureString(LadebildschirmText).X / 2, Verschiebung.Y + LadeHintergrund.Height), Color.Green,
                    Color.Transparent);
                Game1.spriteBatch.End();

                if (!LadebildschirmRichtung)
                {
                    LadebildschirmPosition += 1;
                    if (LadebildschirmPosition == LadebildschirmPositionMax)
                        LadebildschirmRichtung = true;
                }
                else
                {
                    LadebildschirmPosition -= 1;
                    if (LadebildschirmPosition == 0)
                        LadebildschirmRichtung = false;
                }
                return;
            }

            /*   if (reduzierung2 % 3 == 0 || true)
               {
                   reduzierung2 = 1;*/
            if (Spiel2 == null)
            {
                return;
            }

            Texturen.effect.Parameters["modus"].SetValue(SHADER.Wert);

            // GraphicsDevice.Clear(Color.White);

            //SpriteBatch Zeichenflaeche = new SpriteBatch(GraphicsDevice);
          //  GraphicsDevice.Clear(Color.Black);
            RenderTarget2D rt = new RenderTarget2D(Game1.device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight);
            Game1.device.SetRenderTarget(rt);

            if (SpielBlend > 0)
            {
               // GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0F, 0);


                /* SamplerState sampler = new SamplerState();
            sampler.MaxAnisotropy = 4;
            sampler.Filter = TextureFilter.LinearMipPoint;
            sampler.MaxMipLevel = 0;
            sampler.MipMapLevelOfDetailBias = 0;
            sampler.AddressU = TextureAddressMode.Clamp;
            sampler.AddressV = TextureAddressMode.Clamp;
            sampler.AddressW = TextureAddressMode.Clamp;
            sampler = null;*/

                /*   RasterizerState rasterize = new RasterizerState();
               rasterize.CullMode = CullMode.None;
               rasterize.FillMode = FillMode.Solid;
               rasterize.MultiSampleAntiAlias = true;
               device.RasterizerState = rasterize;*/
                // rasterize = null;

                Vector3 screenScalingFactor;

                float horScaling = device.PresentationParameters.BackBufferWidth/baseScreenSize.X;
                float verScaling = device.PresentationParameters.BackBufferHeight/baseScreenSize.Y;
                /// device.PresentationParameters.PresentationInterval = PresentInterval.Two;
                screenScalingFactor = new Vector3(horScaling, verScaling, 1);

                Matrix globalTransformation = Matrix.CreateScale(screenScalingFactor);



                //  GenerateSceneryFog();

                //  spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, globalTransformation); //SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, globalTransformation

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend, null, null, null, null, globalTransformation); //

                bool mouseover = false;

                bool overnotiz = false;
                if (Tausch.SpielAktiv)
                    for (int i = 0; i < Spiel2.players[Spiel2.CurrentPlayer].Notiz.pos.Count; i++)
                    {
                        if (Notizen.Kollision.collision(Help.GetMousePos(),
                            Spiel2.players[Spiel2.CurrentPlayer].Notiz.pos[i] - Spiel2.Fenster, false))
                        {
                            overnotiz = true;
                            break;
                        }
                    }

                bool overtank = false;

                if (Tausch.SpielAktiv)
                    if (!overnotiz)
                    {
                        int t = Spiel2.CurrentPlayer;
                        for (int b = 0; b < Spiel2.players[t].pos.Count; b++)
                            if (Spiel2.players[t].PrüfeObKollision(b,
                                new Vector2(mouseState.X + Spiel2.Fenster.X, mouseState.Y + Spiel2.Fenster.Y)))
                            {
                                overtank = true;
                                break;
                            }
                    }
                spriteBatch.End();
                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                DrawSceneryBackground();

                spriteBatch.End();

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);

                if (Mod.ORTSSCHILD_VISIBLE.Wert) DrawOrtsschilder();
                Meldungen.Draw(spriteBatch, Texturen.font2);

                spriteBatch.End();
                spriteBatch.Begin(SpriteMode, BlendState.Additive,
                    null, null, null, null, globalTransformation);
                if (Spiel.SMOKE.Wert) DrawSmoke(gameTime);
                spriteBatch.End();

                if (Haus.HAEUSER)
                    mouseover = Haus.ZeichneHäuser(overnotiz || overtank ? true : false, spriteBatch, device, Spiel2);

                // spriteBatch.End();
                Tunnel.ZeichneTunnel(spriteBatch, Spiel2);

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                DrawMinenKreis();
                spriteBatch.End();

                Nutzloses.ZeichneNutzloses(gameTime, spriteBatch, Spiel2);

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend, null, null, null);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                Vordergrund.ZeichneVordergrund();
                spriteBatch.End();
                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);

                // Hinweistext
                float scale5 = Optimierung.Skalierung(0.25f)*0.8f; // TODO wurde geändert von 0.2f
                if (Tastatur.TASTE_ZUG_BEENDEN.Wert && HTTP.HTTP.gameid != "")
                {
                    spriteBatch.DrawString(Texturen.font2, "F9 Zug Beenden",
                        new Vector2(screenWidth - 3 - 2*(Texturen.LeeresFeld.Width*scale5),
                            (Texturen.LeeresFeld.Height*scale5) + 42), Color.Lime);
                }
                if (Tastatur.TASTE_SPEICHERN.Wert)
                    spriteBatch.DrawString(Texturen.font2, "F5 Speichern",
                        new Vector2(screenWidth - 3 - 2*(Texturen.LeeresFeld.Width*scale5),
                            (Texturen.LeeresFeld.Height*scale5) + 54), Color.Lime);

                if (Replay.REPLAY_VISIBLE.Wert && Spiel2.replay_visible2) Replay.DrawReplay(spriteBatch, Spiel2);

                spriteBatch.End();
                mouseover = DrawPlayers(overnotiz ? true : false);

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                if (Spieler.WAFFEN_COOLDOWN.Wert && Mod.COOLDOWN_ON_TANK_VISIBLE.Wert) DrawPlayersCooldown();
                spriteBatch.End();

                if (Bunker.BUNKER) DrawBunker();

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);

                DrawKisten();
                DrawMinen();

                DrawArbeitsbereich();

                if (Spiel.AIRSTRIKE.Wert && Spiel.MISSILE.Wert) DrawAirstrike();
                if (Spiel.MISSILE.Wert) DrawMissle();

                //    DrawSceneryFog();

                if (!Editor.visible) DrawLeiste();
                DrawText();
                if (Spiel.WIND.Wert) DrawWind();

                #region DEBUG

                if (DEBUG_AKTIV.Wert)
                {
                    float fact = 0.15f;
                    for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
                    {
                        int id = Spiel2.Haeuser.HausTyp[i];
                        spriteBatch.Draw(Texturen.dot,
                            new Vector2(Spiel2.Haeuser.Position[i].X - (int) Spiel2.Fenster.X,
                                Spiel2.Haeuser.Position[i].Y - (int) Spiel2.Fenster.Y -
                                Texturen.haus[id].Height*Gebäudedaten.SKALIERUNG.Wert[id]), null, Color.Lime, 0.0f,
                            new Vector2(0, 0), fact, SpriteEffects.None, 1);
                    }

                    for (int i = 0; i < Spiel2.Missile.Count(); i++)
                    {
                        spriteBatch.Draw(Texturen.dot,
                            new Vector2(Spiel2.Missile[i].RaktnSpitze().X - (int) Spiel2.Fenster.X,
                                Spiel2.Missile[i].RaktnSpitze().Y - (int) Spiel2.Fenster.Y), null, Color.OrangeRed, 0.0f,
                            new Vector2(0, 0), fact, SpriteEffects.None, 1);
                    }

                    for (int i = 0; i < Spiel2.players.Length && i < 1; i++)
                    {
                        for (int c = 0; c < Spiel2.players[i].hp.Count && c < 1; c++)
                        {
                            int id = Spiel2.players[i].KindofTank[c];
                            float posx = Spiel2.players[i].pos[c].X;
                            float posy = Spiel2.players[i].pos[c].Y;
                            double winkel = Spiel2.players[i].vehikleAngle[c];
                            float ww = (Texturen.panzerindex[id].Width*Fahrzeugdaten.SCALEP.Wert[id])/2;
                            Vector2 p = KollisionsObjekt.Rotiere(winkel, new Vector3(0, 0, 1), new Vector2(-ww, 0));
                            posx = posx + p.X;
                            posy = posy - p.Y;
                            spriteBatch.Draw(Texturen.dot,
                                new Vector2(posx - (int) Spiel2.Fenster.X,
                                    posy - (int) Spiel2.Fenster.Y -
                                    Texturen.panzerindex[id].Height*Fahrzeugdaten.SCALEP.Wert[id]), null, Color.Lime,
                                0.0f,
                                new Vector2(0, 0), fact, SpriteEffects.None, 1);

                            for (int z = 0; z < Spiel2.Missile.Count(); z++)
                            {
                                Vector2 RakPos = Spiel2.Missile[z].RaktnSpitze();
                                id = Spiel2.players[i].KindofTank[c];
                                posx = Spiel2.players[i].pos[c].X; //Player[i].pos[c].X;
                                posy = Spiel2.players[i].pos[c].Y; //Player[i].pos[c].Y;
                                winkel = Spiel2.players[i].vehikleAngle[c];
                                ww = (Texturen.panzerindex[id].Width*Fahrzeugdaten.SCALEP.Wert[id])/2;
                                p = KollisionsObjekt.Rotiere(winkel, new Vector3(0, 0, 1), new Vector2(ww, 0));
                                p = new Vector2(p.X - ww, p.Y);
                                RakPos = new Vector2(RakPos.X + p.X, RakPos.Y - p.Y);
                                spriteBatch.Draw(Texturen.dot,
                                    new Vector2(RakPos.X - (int) Spiel2.Fenster.X, RakPos.Y - (int) Spiel2.Fenster.Y),
                                    null,
                                    Color.Yellow, 0.0f, new Vector2(0, 0), fact, SpriteEffects.None, 1);
                            }
                        }
                    }

                    // Panzer Rohrspitzen zeichnen
                    for (int i = 0; i < Spiel2.players.Length; i++)
                    {
                        for (int c = 0; c < Spiel2.players[i].hp.Count; c++)
                        {
                            int kind = Spiel2.players[i].KindofTank[c];
                            int welcher = Spiel2.players[i].overreach[c] ? 1 : 0;
                            Vector2 pos = Spiel2.players[i].pos[c] +
                                          new Vector2(
                                              welcher == 1 ? -Texturen.RohrPos[kind].X : Texturen.RohrPos[kind].X,
                                              -Texturen.RohrPos[kind].Y); //Texturen.CannonOrigin[kind][0] -
                            float temp2 = Texturen.CannonOrigin[kind][0].Y/2;

                            Vector2 p = Help.RotatePositionOffset(pos, Spiel2.players[i].Angle[c],
                                new Vector2(
                                    Texturen.panzerrohrindex[kind].Width -
                                    (Texturen.panzerrohrindex[kind].Width - Texturen.CannonOrigin[kind][0].X),
                                    welcher == 0 ? temp2 : -temp2));
                            p = Help.RotatePosition(Spiel2.players[i].pos[c], Spiel2.players[i].vehikleAngle[c], p);

                            spriteBatch.Draw(Texturen.dot, p - Spiel2.Fenster, null, Color.Yellow, 0,
                                new Vector2(12.5f, 12.5f), fact, SpriteEffects.None, 1);

                            int Kindof = Spiel2.players[i].KindofTank[c];
                            for (int d = 0; d < Texturen.Radpositionen[Kindof].Count(); d++)
                            {
                                float vehikleAngle = Spiel2.players[i].vehikleAngle[c];
                                bool overreach = Spiel2.players[i].overreach[c];
                                Vector2 bew = Spiel2.players[i].logik[c].GetBewegungsVektor(d,
                                    Spiel2.players[i].vehikleAngle[c], Spiel2.players[i].pos[c],
                                    Spiel2.players[i].overreach[c]);
                                Vector2 add = bew*10;
                                Vector2 p2 = Help.RotatePositionOffset(Spiel2.players[i].pos[c], vehikleAngle,
                                    new Vector2(
                                        (overreach
                                            ? Texturen.Radpositionen[Kindof][d].X
                                            : -Texturen.Radpositionen[Kindof][d].X), Texturen.Radpositionen[Kindof][d].Y));
                                Help.DrawLine(spriteBatch, p2 - Spiel2.Fenster, p2 - Spiel2.Fenster + add, Color.Red, 2);

                                if (i == Spiel2.CurrentPlayer && c == Spiel2.players[i].CurrentTank)
                                    Help.DrawString(spriteBatch, Texturen.font2, bew.ToString(),
                                        new Vector2(10, 60 + d*9),
                                        Color.DarkGoldenrod, Color.Black);
                                // spriteBatch.Draw(Texturen.panzerindexreifen[Kindof], p, null, Color.White, vehikleAngle,
                                // new Vector2(Texturen.panzerindexreifen[Kindof].Width / 2, Texturen.panzerindexreifen[Kindof].Height / 2), scaleP, SpriteEffects.None, 0);
                            }
                        }
                    }

                    // Zykluslinie
                    var x = (int) (Spiel.Position(0));
                    int y = Kartenformat.BottomOf(x, 0);
                    Help.DrawLine(spriteBatch, new Vector2(Spiel.Position(0 - Spiel2.Fenster.X), 0),
                        new Vector2(Spiel.Position(0 - Spiel2.Fenster.X), y), Color.Red, 2);

                    // Mitte
                    x = Spiel.Kartenbreite/2;
                    Help.DrawLine(spriteBatch, new Vector2(x - Spiel2.Fenster.X, 0),
                        new Vector2(x - Spiel2.Fenster.X, Kartenformat.BottomOf(x, 0)), Color.Blue, 2);
                }

                #endregion DEBUG

                mouseover = Spiel2.players[Spiel2.CurrentPlayer].Notiz.Draw(spriteBatch, GraphicsDevice, Spiel2.Fenster,
                    overnotiz ? false : true);
                Editor.Draw(spriteBatch, GraphicsDevice, Spiel2.Fenster, screenWidth, screenHeight, Content, Spiel2);

                spriteBatch.End();


                spriteBatch.Begin(SpriteMode, BlendState.Additive,
                    null, null, null, null, globalTransformation);
                if (Spiel.EXPLOSION.Wert) DrawExplosion();
                // if (Spiel.SMOKE.Wert) DrawSmoke(gameTime);

                spriteBatch.End();

                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                if (Mod.MINIMAP_VISIBLE.Wert && Tausch.SpielAktiv && Spiel2.minimap_visible) Vordergrund.DrawMinimap();
                if (Mod.MINIMAP_VISIBLE.Wert && Tausch.SpielAktiv && Spiel2.minimap_visible) DrawMinimapDot();

                Kurzmeldung.Zeichnen(spriteBatch, Texturen.font2, (int) Spiel2.Fenster.X,
                    (int) (Spiel2.Fenster.X + screenWidth));
                spriteBatch.End();

                // Zeichne Kenngrößen
                if (Game1.DEBUG_AKTIV.Wert)
                {
                    spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
                    Vector2 Spielerpos = Spiel2.players[Spiel2.CurrentPlayer].pos[
                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]-Spiel2.Fenster;

                    Kenngroesse Kenn = Spiel2.players[Spiel2.CurrentPlayer].Kenngroesse_Wert;
                    if (Kenn != null)
                    {
                        for (int i = (int) Spiel2.Fenster.X/Kenn.Feldbreite;
                            i < Math.Ceiling((Spiel2.Fenster.X + screenWidth)/Kenn.Feldbreite);
                            i++)
                            for (int b = (int) Spiel2.Fenster.Y/Kenn.Feldhoehe;
                                b < Math.Ceiling((Spiel2.Fenster.Y + screenHeight)/Kenn.Feldhoehe);
                                b++)
                            {
                                if (i < 0 || b < 0) continue;
                                if (i >= Kenn.FelderAnzahlHorizontal || b >= Kenn.FelderAnzahlVertikal) continue;

                                Bereich bereich = Kenn.GibBereichZuId(new Vector2(i, b));

                                Color Bereichsfarbe = Color.Transparent;
                                if (bereich.Wert >= 15)
                                    Bereichsfarbe = Color.Green;
                                if (bereich.Wert >= 35)
                                    Bereichsfarbe = Color.Yellow;
                                if (bereich.Wert >= 60)
                                    Bereichsfarbe = Color.Red;

                                /*if (Bereichsfarbe != null)
                                    Help.DrawRectangle(spriteBatch, this.GraphicsDevice,
                                        new Rectangle((int) (bereich.Feld.X - Spiel2.Fenster.X),
                                            (int) (bereich.Feld.Y - Spiel2.Fenster.Y), bereich.Feld.Width,
                                            bereich.Feld.Height), Bereichsfarbe, 0.25f);*/

                                Color Farbe = Color.Yellow;
                                Help.DrawLine(spriteBatch,
                                    new Vector2(bereich.Feld.X - Spiel2.Fenster.X, bereich.Feld.Y - Spiel2.Fenster.Y),
                                    new Vector2(bereich.Feld.X + bereich.Feld.Width - Spiel2.Fenster.X,
                                        bereich.Feld.Y - Spiel2.Fenster.Y), Farbe, 1);
                                Help.DrawLine(spriteBatch,
                                    new Vector2(bereich.Feld.X - Spiel2.Fenster.X, bereich.Feld.Y - Spiel2.Fenster.Y),
                                    new Vector2(bereich.Feld.X - Spiel2.Fenster.X,
                                        bereich.Feld.Y - Spiel2.Fenster.Y + bereich.Feld.Height), Farbe, 1);

                                double Wert = Math.Round(Kenn.Bereiche[i, b],2);
                                Vector2 text = Texturen.font2.MeasureString(Wert.ToString());
                                Help.DrawString(spriteBatch, Texturen.font2, Wert.ToString(),
                                    new Vector2(i*Kenn.Feldbreite - Spiel2.Fenster.X + Kenn.Feldbreite/2 - text.X/2,
                                        b*Kenn.Feldhoehe - Spiel2.Fenster.Y + Kenn.Feldhoehe/2 - text.Y/2),
                                    Farbe, Color.Transparent);
                            }

                        List<List<Vector2>> Flaechen =
                            Kenn.Hinzufügen(
                                Spiel2.players[Spiel2.CurrentPlayer].pos[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank], 0, 350, Anteil.Fläche, Wachstum.LinearFallend, true);

                        for (int i = 0; i < Flaechen.Count; i++)
                            for (int b = 0; b < Flaechen[i].Count; b++)
                            {
                                Help.DrawLine(spriteBatch, Flaechen[i][b%Flaechen[i].Count]-Spiel2.Fenster,
                                    Flaechen[i][(b + 1) % Flaechen[i].Count] - Spiel2.Fenster, Color.Red, 2);
                            }
                    }
                    spriteBatch.End();
                }


                spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);

                int CurrentTank = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;

                if (Mod.SPIELERMENU_VISIBLE.Wert && CurrentTank >= 0)
                    Spielermenu.Draw(spriteBatch, Texturen.font4,
                        Spiel2.players[Spiel2.CurrentPlayer].Effekte[CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].Rucksack[CurrentTank], Spiel2, Transparenz);
                spriteBatch.End();
            }



            Game1.device.SetRenderTarget(null);
            spriteBatch.Begin();
            Help.DrawRectangle(spriteBatch, Game1.device,
                new Rectangle(0, 0, device.PresentationParameters.BackBufferWidth,
                    device.PresentationParameters.BackBufferHeight), Color.Black, 1);
            float Faktor = SpielBlend;

            spriteBatch.Draw(rt, new Rectangle(0, 0, screenWidth, screenHeight), Color.White * Faktor);
            spriteBatch.End();
            rt.Dispose();



            spriteBatch.Begin(SpriteMode, BlendState.AlphaBlend);
            Ladenmenu.Draw(spriteBatch);
            if (Mod.PAUSEMENU_VISIBLE.Wert) pauseMenu.Draw(spriteBatch,SpielBlend);
            StartMenu.Draw(spriteBatch);
            if (Mod.EINGABEZEILE_VISIBLE.Wert) Eingabefenster.ZeichneEingabefenster(spriteBatch);
            BauMenü.Draw(spriteBatch, Spiel2.Fenster, Texturen.font4, Spiel2);
            SetUpMenu.Draw(spriteBatch);
            if (MausAktiv) DrawMouse();

            if (Spiel2.SpielerAktiv() < 2)
            {
                Tausch.SpielAktiv = false;
                DrawTextEnd();
            }
            spriteBatch.End();
            
            //GraphicsDevice.PresentationParameters.PresentationInterval = PresentInterval.Immediate;

            //base.Draw(gameTime);
            // _isDirty = true;
            //base.BeginDraw();
            // GraphicsDevice.Present();
        }

        /// <summary>
        ///     Called after the Game and GraphicsDevice are created, but before LoadContent.  Reference page contains code sample.
        /// </summary>
        protected override void Initialize() // selbsterklärend
        {
            device = graphics.GraphicsDevice;

            base.Initialize();
            /*  int DesiredFrameRate = 30;
          TargetElapsedTime = new TimeSpan(TimeSpan.TicksPerSecond / DesiredFrameRate);
          IsFixedTimeStep = true;*/
        }

        /// <summary>
        ///     Loads the content.
        /// </summary>
        protected override void LoadContent() // lädt alle notwendigen Ressourcen
        {
            // loadAllContent();
        }

        /// <summary>
        ///     Called when graphics resources need to be unloaded. Override this method to unload any game-specific graphics
        ///     resources.
        /// </summary>
        protected override void UnloadContent() // keine Funktion
        {
        }

        public static void SpielfeldEinblenden(int Zeit)
        {
            Game1.SpielEinblenden = Zeit;
            Game1.SpielEinblendenMax = Zeit;
            Game1.SpielAusblenden = 0;
            Game1.SpielAusblendenMax = 0;
        }

        public static void SpielfeldAusblenden(int Zeit)
        {
            Game1.SpielEinblenden = 0;
            Game1.SpielEinblendenMax = 0;
            Game1.SpielAusblenden = Zeit;
            Game1.SpielAusblendenMax = Zeit;
        }

        private double elapsed = 0;
        Task UpdateTask = null;
        private int updateCount = 0;
        /// <summary>
        ///     Ruft alle check..  Methoden auf
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        protected override void Update(GameTime gameTime)
        {
           Time = gameTime;
           check_Datenaustausch();


           if (Time.TotalGameTime.TotalMilliseconds - elapsed >= 50)
           {
               _isDirty = true;
               elapsed = Time.TotalGameTime.TotalMilliseconds;
           }

            if (Spiel2 != null)
            {
                KeyboardKeys();
            }

            Action<object> action = (object obj) =>
            {
                for (;;)
                {
                    if (updateCount > 0)
                    {
                        updateCount--;

                        if (SpielEinblenden > 0)
                            SpielEinblenden--;

                        if (SpielAusblenden > 0)
                            SpielAusblenden--;

                        if (SpielEinblendenMax != 0)
                        {
                            SpielBlend += ((float)1 / SpielEinblendenMax);
                            if (SpielEinblendenMax != 0 && SpielEinblenden == 0)
                                SpielBlend = 1;
                            if (SpielBlend > 1)
                                SpielBlend = 1;
                        }

                        if (SpielAusblendenMax != 0)
                        {
                            SpielBlend -= ((float)1 / SpielAusblendenMax);
                            if (SpielAusblendenMax != 0 && SpielAusblenden == 0)
                                SpielBlend = 0;
                            if (SpielBlend < 0)
                                SpielBlend = 0;
                        }

                        if (!LadebildschirmAktiv)
                        {

                            if (Meldungen != null) Meldungen.Update();

                            if (SpielAktiv == false || Editor.visible)
                            {
                                //if (Sounds.hintergrund2 != null)
                                //Sounds.hintergrund2.Stop();

                                if (Sounds.Hintergrundmusik != null)
                                    Sounds.Hintergrundmusik.StopSound(0);
                            }
                            else
                            {
                                // if (Sounds.hintergrund2 != null && Sounds.hintergrund2.State != SoundState.Playing)
                                // Sounds.hintergrund2.Play();
                                /* if (Sounds.channel != null)
                 {
                     bool playing = false;
                     Sounds.channel.isPlaying(ref playing);
                     if (!playing)
                     {
                         Sounds.system.playSound(FMOD.CHANNELINDEX.FREE, Sounds.sound1, false, ref Sounds.channel);
                     }
                 }*/

                                if (Sounds.Hintergrundmusik != null)
                                    Sounds.Hintergrundmusik.PlaySound(0);
                            }

                            if (Spiel2 == null) continue;

                            // Netzwerk
                            if (Server.isRunning)
                            {
                                Server.Application_Idle(null, null);
                            }

                            if (Spiel2 == null) continue;

                            if (MausAktiv)
                            {
                                MouseKeys();
                                if (Eingabefenster.Eingabe != null)
                                    Eingabefenster.Eingabe.mouseKeys();
                            }

                            if (Spiel2 == null) continue;
                            //  if (Spiel2.paused) pauseMenu.Update(gameTime);
                            if (Spiel2.SpielerAktiv() > 1 && SpielAktiv)
                            {
                                Spiel2.check_shot_increase();
                                Kurzmeldung.Aktualisieren();
                                if (Spiel.MISSILE.Wert) Spiel2.UpdateMissles(Time, smokeList);

                                if (Spiel.CHECK_RAKETEN.Wert) Spiel2.check_Raketen();
                                Spiel2.check_players(Time);
                                if (Spiel.CHECK_WIND.Wert) Spiel2.check_wind();
                                Spiel2.check_Focus();
                                Spiel2.check_playerwinkel();
                                Spiel2.check_Minen(Time);
                                if (Haus.HAEUSER && Spiel.CHECK_HAEUSER.Wert) Spiel2.check_haeuser();
                                if (Spieler.WAFFEN_COOLDOWN.Wert) Spiel2.check_Cooldowns();

                                if (!Client.isRunning)
                                    if (Spiel2.Karte.collisions(Spiel2.Spielfeld, Spiel2.Missile, Spiel2.players,
                                        Spiel2.Haeuser,
                                        Spiel2.Baeume, Spiel2.Bunker, Spiel2.Kisten, Spiel2.Height, Time,
                                        new Vector2(Spiel2.Fenster.X + screenWidth/2, Spiel2.Fenster.Y)))
                                    {
                                        Vordergrund.ErstelleVordergrund();
                                    }

                                Vordergrund.AktualisiereVordergrund(Spiel2.check_verzoegerte(Time));
                                // zündet die Raketen und zeichnet karte daraufhin neu

                                if (Spiel2.Karte.particleListExp.Count > 0)
                                    Spiel2.Karte.UpdateParticles(Spiel2.Karte.particleListExp, Time, 0);

                                if (Spiel2.Karte.particleListMapSmoke.Count > 0)
                                    Spiel2.Karte.UpdateParticles(Spiel2.Karte.particleListMapSmoke, Time, 2);

                                if (smokeList.Count > 0)
                                    Spiel2.Karte.UpdateParticles(smokeList, Time, 1);
                            }

                            if (Mod.SPIELERMENU_VISIBLE.Wert)
                            {
                                // Spielermenu.CurrentPlayerID = Spiel2.CurrentPlayer;
                                // Spielermenu.CurrentTankID = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                            }
                            //base.Update(gameTime);
                            //Draw(gameTime);
                        }
                    }
                    Thread.Sleep(5);
             }
            };

               if (UpdateTask == null)
            {
                UpdateTask = new Task(action, "Update");
                UpdateTask.Start();
            }
                updateCount++;
        }
          

        /// <summary>
        ///     Creates the kasten.
        /// </summary>
        private void createKasten()
        {
            Spiel2.Colors = new Color[screenWidth * screenHeight];
            Texturen.kasten = new Texture2D(GraphicsDevice, screenWidth, screenHeight);

            Color[,] vordergrund = Farbwahl(Karte.Material[1].Bild);
            Color[,] water = Farbwahl(Texturen.wasser);

            Spiel2.Colors = new Color[screenWidth * screenHeight];
            int breite = 5;
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    Spiel2.Colors[x + y * screenWidth] = Color.Transparent;
                    // || (y >= screenHeight - breite && x < screenWidth)|| (y <= breite && x < screenWidth)
                    if ((x <= breite && y < screenHeight) || (x >= screenWidth - breite && y < screenHeight))
                        Spiel2.Colors[x + y * screenWidth] = Color.Gray;
                }
            }
            Texturen.kasten = new Texture2D(device, screenWidth, screenHeight, false, SurfaceFormat.Color);
            Texturen.kasten.SetData(Spiel2.Colors);

            int mas = 25;
            Spiel2.Colors = new Color[mas * mas];
            Texturen.dot = new Texture2D(GraphicsDevice, mas, mas);
            Texturen.dot2 = new Texture2D(GraphicsDevice, mas, mas);

            Spiel2.Colors = new Color[mas * mas];
            for (int x = 0; x < mas; x++)
            {
                for (int y = 0; y < mas; y++)
                {
                    Spiel2.Colors[x + y * mas] = Color.White;
                }
            }
            Texturen.dot = new Texture2D(device, mas, mas, false, SurfaceFormat.Color);
            Texturen.dot.SetData(Spiel2.Colors);

            Spiel2.Colors = new Color[mas * mas];
            for (int x = 0; x < mas; x++)
            {
                for (int y = 0; y < mas; y++)
                {
                    int erx = 0;
                    int ery = 0;
                    if (x < 10) erx++;
                    if (x > 15) erx++;
                    if (y < 10) ery++;
                    if (y > 15) ery++;

                    if (erx == 1 && ery == 1)
                    {
                        Spiel2.Colors[x + y * mas] = Color.Transparent;
                    }
                    else
                        Spiel2.Colors[x + y * mas] = Color.White;
                }
            }
            Texturen.dot2 = new Texture2D(device, mas, mas, false, SurfaceFormat.Color);
            Texturen.dot2.SetData(Spiel2.Colors);

            /*Spiel2.Colors = new Color[screenWidth*2*screenHeight];
            //Texturen.miniback = new Texture2D(this.GraphicsDevice, 2048, screenHeight);

            Spiel2.Colors = new Color[screenWidth*2 * screenHeight];
            for (int x = 0; x < screenWidth*2; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    Spiel2.Colors[x + y * screenWidth*2] = Color.White;
                }
            }
            Texturen.miniback = new Texture2D(device, screenWidth*2, screenHeight, false, SurfaceFormat.Color);
            Texturen.miniback.SetData(Spiel2.Colors);    */
        }

        /// <summary>
        ///     Handles the VisibleChanged event of the Game1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Game1_VisibleChanged(object sender, EventArgs e)
        {
            if (Control.FromHandle((Window.Handle)).Visible)
            {
                Control.FromHandle((Window.Handle)).Visible = false;
            }
        }

        /// <summary>
        ///     Gets the bahn.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="schuss">The schuss.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="screenheight">The screenheight.</param>
        /// <param name="minx">The minx.</param>
        /// <param name="maxx">The maxx.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        private void getBahn(Vector2 g, Vector2 schuss, Vector2 pos, int screenheight, int minx, int maxx,
            SpriteBatch spriteBatch)
        {
            Vector2 temp = pos;
            if (line == null)
            {
                line = new Texture2D(GraphicsDevice, 1, 1);
                line.SetData(new[] { Color.Yellow });
            }

            int summe = 0;
            int maxsumme = 1000;

            bool draw = true;
            while (temp.Y < screenheight && summe <= maxsumme && !Kartenformat.isSet(temp + Spiel2.Fenster))
            {
                var angle = (float)Math.Atan2(schuss.Y, schuss.X);
                if (draw)
                {
                    //if (temp.X < maxx && temp.X > minx)
                    spriteBatch.Draw(line, new Rectangle((int)temp.X, (int)temp.Y, (int)schuss.Length(), 2), null,
                        Color.White, angle, new Vector2(0, 0), SpriteEffects.None, 0);
                    //else return;
                    draw = false;
                    summe += 3 * (int)schuss.Length();
                }
                else
                {
                    draw = true;
                    schuss += g;
                    temp += schuss;
                }
                schuss += g;
                temp += schuss;
            }
        }

        /// <summary>
        ///     Handles the PreparingDeviceSettings event of the graphics control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PreparingDeviceSettingsEventArgs" /> instance containing the event data.</param>
        private void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            e.GraphicsDeviceInformation.PresentationParameters.DeviceWindowHandle = drawSurface;
        }

        /// <summary>
        ///     Tastaturabfragen werden hier bearbeitet
        /// </summary>
        private void KeyboardKeys()
        {
          TastaturDeutsch.OnKeyPress();

           // Eingabe.KeyboardKeys(keybState);
            if (Spiel2 == null) return;

            if (!Tausch.SpielAktiv)
            {
                Spiel2.players[Spiel2.CurrentPlayer].Notiz.schreibend = false;
            }

            if (Spiel2.players[Spiel2.CurrentPlayer].Notiz.schreibend)
            {
                if (keybState == Keyboard.GetState())
                {
                    last++;
                }
                else
                    last = 0;

                if (keybState != Keyboard.GetState() || last >= 15)
                {
                    last = 0;
                    Spiel2.players[Spiel2.CurrentPlayer].Notiz.TastenEingabe(Keyboard.GetState());
                }
                keybState = Keyboard.GetState();
                return;
            }

            if (Mod.EINGABEZEILE_VISIBLE.Wert && Editor.Textfelder != null && !Editor.Textfelder.visible)
                Eingabefenster.KeyboardKeys(keybState, Keyboard.GetState());
            oldcurrentState = currentState;
            if (oldcurrentState == null) oldcurrentState = currentState;
            currentState = GamePad.GetState(PlayerIndex.One);

            // if (keybState != Keyboard.GetState())

            #region Escape

            if (Editor.visible && Editor.Textfelder != null && Editor.Textfelder.visible)
            {
                if (keybState != Keyboard.GetState() || oldcurrentState.Buttons.Start != currentState.Buttons.Start)
                {
                    if ((currentState.IsConnected && currentState.Buttons.Start == ButtonState.Pressed) ||
                        (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Keys.Escape)))
                    {
                        Editor.Textfelder.hide();

                        if (Editor.Textfeldtyp == 6)
                        {
                            Spiel2.players[Editor.TextfeldSpieler].Minen[Editor.TextfeldID] =
                                Mine.Laden(Editor.Textfelder.Text,
                                    Spiel2.players[Editor.TextfeldSpieler].Minen[Editor.TextfeldID],
                                    Spiel2.players[Editor.TextfeldSpieler].Minen[Editor.TextfeldID].ID);
                        }
                        else if (Editor.Textfeldtyp == 5)
                        {
                            Spiel2.Kisten.Laden(Editor.Textfelder.Text, Editor.TextfeldID, Content);
                        }
                        else if (Editor.Textfeldtyp == 4)
                        {
                            Spiel2.Bunker.Laden(Editor.Textfelder.Text, Editor.TextfeldID);
                        }
                        else if (Editor.Textfeldtyp == 3)
                        {
                            Spiel2.players[Editor.TextfeldSpieler].LadenFahrzeug(Editor.Textfelder.Text,
                                Editor.TextfeldID, Content);
                        }
                        else if (Editor.Textfeldtyp == 2)
                        {
                            Nutzloses.Laden(Content, Editor.Textfelder.Text, Editor.TextfeldID);
                        }
                        else if (Editor.Textfeldtyp == 1)
                        {
                            Spiel2.players[Editor.TextfeldSpieler].TunnelAnlage[Editor.TextfeldID] =
                                Tunnel.Laden(Editor.Textfelder.Text,
                                    Spiel2.players[Editor.TextfeldSpieler].TunnelAnlage[Editor.TextfeldID]);
                        }
                        else if (Editor.Textfeldtyp == 0)
                        {
                            Spiel2.Haeuser.Laden(Editor.Textfelder.Text, Editor.TextfeldID);
                        }
                    }
                }
            }

            #region Menüaufruf

            if (!Editor.visible && Mod.PAUSEMENU_VISIBLE.Wert && !StartMenu.visible)
            {
                if (keybState != Keyboard.GetState() || oldcurrentState.Buttons.Start != currentState.Buttons.Start)
                {
                    if ((currentState.IsConnected && currentState.Buttons.Start == ButtonState.Pressed) ||
                        (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Keys.Escape)))
                        if (pauseMenu.visible)
                        {
                            // MausAktiv = false;
                            // if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu.show();
                            pauseMenu.hide();
                            Eingabefenster.Eingabe.Verstecken();
                            SpielfeldEinblenden(60);
                            Tausch.SpielAktiv = true;
                        }
                        else
                        {
                            //   if (Mod.SPIELERMENU_VISIBLE.Wert) { Spielermenu.undeploy(); Spielermenu.hide(); }
                            //   MausAktiv = true;
                            pauseMenu.show();
                            Eingabefenster.Eingabe.Verstecken();
                            SpielfeldAusblenden(60);
                            Tausch.SpielAktiv = false;
                        }
                }
            }

            #endregion Menüaufruf

            #region Online_Escape

            if (!Editor.visible && Mod.ONLINE_ESCAPE_VISIBLE.Wert)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    if (Server.isRunning) Server.Shutdown();
                    if (Client.isRunning) Client.Shutdown();

                    // Nur Speichern
                    String Pfad = "";
                    if (HTTP.HTTP.gameid != "")
                    {
                        Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";
                    }
                    else
                        Pfad = "Content\\Games\\temp\\";

                    HTTP.HTTP.Dir(Pfad);
                    MapWriter.Generieren(Spiel2);
                    MapWriter.Speichern(Pfad + "Map.dat");
                    Replay.End(Spiel2.players);
                    Replay.Generieren(true);
                    Replay.Speichern(Pfad + "Data.dat");
                    // alle dateien löschen
                    File.Delete(Pfad + "OldMap.dat");
                    File.Delete(Pfad + "OldData.dat");
                    Spiel2 = null;
                    if (HTTP.HTTP.gameid != "" && !Hauptfenster.Program.Formular.checkBox2.Checked)
                    {
                        List<String> list = HTTP.HTTP.upload(String.Join("\r\n", MapWriter.list.ToArray()),
                            String.Join("\r\n", Replay.list.ToArray()), "0");
                        if (HTTP.HTTP.IsFailure(list))
                        {
                            // Fehler
                        }
                    }

                    SpielAktiv = false;
                    FormState.Restore(Hauptfenster.Program.Formular);

                    Tausch.SpielAktiv = false;

                    if (HTTP.HTTP.gameid != "")
                    {
                        TabPage[] qq =
                        {
                            Hauptfenster.Program.Formular.tabPage4, Hauptfenster.Program.Formular.tabPage5,
                            Hauptfenster.Program.Formular.tabPage1
                        };
                        Hauptfenster.Program.Formular.Show(qq, Hauptfenster.Program.Formular.tabPage5);
                    }
                    else
                    {
                        TabPage[] qq = { Hauptfenster.Program.Formular.tabPage4, Hauptfenster.Program.Formular.tabPage1 };
                        Hauptfenster.Program.Formular.Show(qq, Hauptfenster.Program.Formular.tabPage1);
                    }

                    Hauptfenster.Program.Formular.tabControl1.Show();
                    Hauptfenster.Program.Formular.pictureBox1.Hide();
                    return;
                }
            }

            #endregion Online_Escape

            #endregion Escape

            if (Mod.EINGABEZEILE_VISIBLE.Wert && Eingabefenster.Eingabe!=null &&Eingabefenster.Eingabe.Sichtbar)
            {
                keybState = Keyboard.GetState();
                return;
            }

            if (Spiel2 != null && keybState != Keyboard.GetState() && Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                if (!Editor.visible)
                {
                    Editor.show(screenWidth);
                    if (pauseMenu.visible) SpielfeldEinblenden(60);
                    pauseMenu.hide();
                    StartMenu.hide();
                    Eingabefenster.Eingabe.Verstecken();
                    Spielermenu.hide();
                }
                else
                    Editor.hide();
            }

            if (Editor.Textfelder != null)
                if (Editor.Textfelder.visible)
                {
                    if (keybState == Keyboard.GetState())
                    {
                        last++;
                    }
                    else
                        last = 0;

                    if (keybState != Keyboard.GetState() || last >= 15)
                    {
                        last = 0;
                        Editor.Textfelder.TastenEingabe(Keyboard.GetState());
                    }
                    keybState = Keyboard.GetState();
                    return;
                }

            if (SpielAktiv == false)
            {
                keybState = Keyboard.GetState();
                return;
            }
            Meldungen.keyboardKeys(keybState);

            #region MausSichtbarkeit

            if (keybState != Keyboard.GetState() || oldcurrentState.Buttons != currentState.Buttons)
                if (Keyboard.GetState().IsKeyDown(Keys.K))
                {
                    if (MausAktiv == false)
                    {
                        MausAktiv = true;
                    }
                    else
                        MausAktiv = false;
                }

            #endregion MausSichtbarkeit

            #region Interaktion

            if ((currentState.IsConnected && currentState.Buttons.Y == ButtonState.Pressed) ||
                (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Keys.I)))
            {
                // Interaktion mit anderen+
                // mit anderem Fahrzeug
                bool found = false;
                int t = Spiel2.CurrentPlayer;
                for (int b = 0; b < Spiel2.players[t].pos.Count; b++)
                {
                    if (b == Spiel2.players[t].CurrentTank) continue;
                    if (Fahrzeugdaten.KANNHANDELN.Wert[Spiel2.players[t].KindofTank[b]] == 0) continue;
                    if (Spiel2.players[t].PrüfeObKollision(b,
                        Spiel2.players[t].pos[Spiel2.players[t].CurrentTank] + new Vector2(0, -5)))
                    {
                        Spielermenu.GetInTrade(Spiel2.players[t].Rucksack[b], new Vector3(0, b, 0));
                        Spielermenu.show();
                        Eingabefenster.Eingabe.Verstecken();
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
                    {
                        if (Spiel2.Haeuser.Lebenspunkte[i] <= 0) continue;
                        if (Spiel2.Haeuser.BesitzerPunkte[i] < Allgemein.MinBesitzerPunkte) continue;
                        if (Spiel2.Haeuser.Besitzer[i] != t) continue;
                        if (Spiel2.Haeuser.HausTyp[i] != Gebäudedaten.FABRIK &&
                            Spiel2.Haeuser.HausTyp[i] != Gebäudedaten.WAFFENHÄNDLER) continue;
                        if (Spiel2.Haeuser.IsCollision2(i,
                            Spiel2.players[t].pos[Spiel2.players[t].CurrentTank] + new Vector2(0, -5)))
                        {
                            // auffüllen
                            Haus.InventarAuffüllen();

                            Spielermenu.GetInTrade(Haus.Rucksack, new Vector3(1, Spiel2.Haeuser.HausTyp[i], 0));
                            Spielermenu.show();
                            Eingabefenster.Eingabe.Verstecken();
                            found = true;
                            break;
                        }
                    }
                }
            }

            #endregion Interaktion

            #region Netzwerk

            /* if (keybState != Keyboard.GetState())
                if (Keyboard.GetState().IsKeyDown(Keys.F7))
                {
                    Server.StartServer();
                    Server.Spiel2 = Spiel2;
                    keybState = Keyboard.GetState();
                }

            if (keybState != Keyboard.GetState())
                if (Keyboard.GetState().IsKeyDown(Keys.F8))
                {
                    Client.Connect("localhost", 14242);
                    Client.Spiel2 = Spiel2;
                    Client.game = this;
                    Spiel2.IsServer = false;
                    keybState = Keyboard.GetState();
                }*/

            #endregion Netzwerk

            #region MovingMap

            if (Spiel2 != null &&
                (keybState != Keyboard.GetState() || oldcurrentState.Buttons.B != currentState.Buttons.B ||
                 currentState.Buttons.Back != oldcurrentState.Buttons.Back))
            {
                if (Tastatur.TASTE_FREIEBEWGUNG.Wert &&
                    (currentState.IsConnected && currentState.Buttons.B == ButtonState.Pressed ||
                     Keyboard.GetState().IsKeyDown(Tastatur.FREIEBEWGUNG.Wert)))
                    if (Spiel2.Moving_Map)
                    {
                        Spiel2.Moving_Map = false;
                    }
                    else
                        Spiel2.Moving_Map = true;

            #endregion MovingMap

                #region Spielermenüaufruf

                if (Mod.SPIELERMENU_VISIBLE.Wert &&
                    (keybState != Keyboard.GetState() || currentState.Buttons.Back != oldcurrentState.Buttons.Back))
                    if (Keyboard.GetState().IsKeyDown(Keys.T))
                    {
                        if (Transparenz == 1.0f)
                        {
                            Transparenz = 0.5f;
                        }
                        else
                            Transparenz = 1.0f;
                    }
                    else if ((!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Keys.Tab)) ||
                             (currentState.IsConnected && currentState.Buttons.Back == ButtonState.Pressed))
                        if (Spielermenu.visible)
                        {
                            Spielermenu.hide();
                            //    MausAktiv = false;
                            // SpMenuFullyDeployed = false;

                            // Spiel2.paused = false;
                            // Spielermenu.hide();
                            // Hauptfenster.Tausch.SpielAktiv = true;
                        }
                        else
                        {
                            Spielermenu.show();
                            Eingabefenster.Eingabe.Verstecken();
                            //  MausAktiv = true;
                            // Spielermenu.show();
                            // Hauptfenster.Tausch.SpielAktiv = false;
                            // SpMenuFullyDeployed = true;w

                            // Spiel2.paused = true;
                        }

                #endregion Spielermenüaufruf
            }

            #region Minimap

            if (keybState != Keyboard.GetState())
            {
                if (Tastatur.TASTE_MINIMAP.Wert && Keyboard.GetState().IsKeyDown(Tastatur.MINIMAP.Wert))
                    if (Spiel2.minimap_visible)
                    {
                        Spiel2.minimap_visible = false;
                    }
                    else
                    {
                        Spiel2.minimap_visible = true;
                    }
            }

            #endregion Minimap

            #region Replay

            if (keybState != Keyboard.GetState())
            {
                if (Tastatur.TASTE_REPLAY.Wert && Keyboard.GetState().IsKeyDown(Tastatur.REPLAY.Wert))
                    if (Spiel2.replay_visible2)
                    {
                        Spiel2.replay_visible2 = false;
                    }
                    else
                    {
                        Spiel2.replay_visible2 = true;
                    }
            }

            #endregion Replay

            #region DEBUG

            if (DEBUG_AKTIV.Wert)
            {
                if (keybState != Keyboard.GetState())
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.K))
                    {
                        Mod.SpeichereModVariablen("Mod.dat", false);
                        Meldungen.addMessage("Mod-Variablen gespeichert...");
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.L))
                    {
                        //  Vector2 pos = Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];
                        //Foreground.Update_CreateForeground(Feuer.Generieren((int) (pos.X-50),(int) (pos.X+50),(int)pos.X,(int)pos.Y,5));

                        /*Spieler tmp = Spiel2.players[Spiel2.CurrentPlayer];
                            int id = tmp.CurrentTank;
                            Spiel2.players[Spiel2.CurrentPlayer].Minen.Add(new Mine((int)tmp.pos[id].X, (int)tmp.pos[id].Y,1));*/
                        Spiel2.next_player();
                        Meldungen.addMessage("Spieler gewechselt...");
                        //  Optimierung.Optimiere_Bäume();
                        //   Optimierung.Optimiere_Häuser();
                    }
                    else if (Spiel2 != null && Keyboard.GetState().IsKeyDown(Keys.F12))
                    {
                        /*  MapReader MapReader = new MapReader();
                                  MapReader.LoadMap(this, "Content\\Games\\temp\\Map.dat", "Content\\Games\\temp\\Data.dat", ref Spiel2, new Vector2(screenWidth, screenHeight));
                                  if (Spiel2 != null)
                                  {
                                      Help.Spielfeld = Spiel2.Spielfeld;
                                      Spiel2.Width = screenWidth;
                                      Spiel2.Height = screenHeight;
                                      //// vordergrund = Farbwahl(Texturen.tilltexture);
                                      water = Farbwahl(Texturen.wasser);
                                      Foreground.CreateForeground();
                                      Fog.CreateFog();
                                      createKasten();
                                      Hauptfenster.Tausch.SpielAktiv = true;
                                  }
                                  else
                                      Hauptfenster.Tausch.SpielAktiv = false;*/
                                    Action<object> SpielSpeichern = (object obj) =>
            {
                  MapWriter.Generieren(Spiel2);
                        MapWriter.Speichern("Spiel.sav");
                Meldungen.addMessage("Speichern...");
                LadebildschirmAktiv = false;
                Game1.SpielfeldEinblenden(60);
};

                         if (SpeichernTask == null || SpeichernTask.IsCompleted)
            {
                                              LadebildschirmAktiv = true;
                SpielfeldAusblenden(60);
                        LadebildschirmText = "Spiel wird gespeichert...";
                SpeichernTask = new Task(SpielSpeichern, "SpielSpeichern");
                SpeichernTask.Start();
            }
                        
                    }
                    else if (Spiel2 != null && Keyboard.GetState().IsKeyDown(Keys.F8))
                    {
                        /*  MapReader MapReader = new MapReader();
                                      MapReader.LoadMap(this, "Content\\Games\\temp\\Map.dat", "Content\\Games\\temp\\Data.dat", ref Spiel2, new Vector2(screenWidth, screenHeight));
                                      if (Spiel2 != null)
                                      {
                                          Help.Spielfeld = Spiel2.Spielfeld;
                                          Spiel2.Width = screenWidth;
                                          Spiel2.Height = screenHeight;
                                          //// vordergrund = Farbwahl(Texturen.tilltexture);
                                          water = Farbwahl(Texturen.wasser);
                                          Foreground.CreateForeground();
                                          Fog.CreateFog();
                                          createKasten();
                                          Hauptfenster.Tausch.SpielAktiv = true;
                                      }
                                      else
                                          Hauptfenster.Tausch.SpielAktiv = false;*/
                        Action<object> SpielLaden = (object obj) =>
                        {
                            MapReader.Laden(this, "Spiel.dat");
                            Meldungen.addMessage("Geladen...");
                            LadebildschirmAktiv = false;
                            Game1.SpielfeldEinblenden(60);
                        };

                        if (LadenTask == null || LadenTask.IsCompleted)
                        {
                            LadebildschirmAktiv = true;
                            Game1.SpielfeldAusblenden(60);
                            LadebildschirmText = "Spiel wird geladen...";
                            LadenTask = new Task(SpielLaden, "SpielLaden");
                            LadenTask.Start();
                        }

                    }
                }

                if (Spiel2 != null && Keyboard.GetState().IsKeyDown(Keys.F11))
                {
                    Spiel2.players[Spiel2.CurrentPlayer].ActionPoints += 999;
                }

                if (Spiel2 != null && Keyboard.GetState().IsKeyDown(Keys.F10))
                {
                    Spiel2.players[Spiel2.CurrentPlayer].fuelRemains += 999;
                    for (int i = 0; i < Spiel2.players[Spiel2.CurrentPlayer].pos.Count(); i++)
                    {
                        Spiel2.players[Spiel2.CurrentPlayer].Rucksack[i].Treibstoff += 999;
                    }
                    //Tastatur.Save("Datei.txt");
                    //  Var<Int32> data = new Var<Int32>("data", 0);
                    // Tastatur.Load("Tastatur.conf");
                    // Mod.Load("v1.0.conf");
                    //  SetUpMenu.show();
                    //  Hauptfenster.Tausch.SpielAktiv = false;
                    //  MausAktiv = true;
                    // Spiel2.paused = true;
                }
            }

            #endregion DEBUG

            #region ZUG_BEENDEN

            if (HTTP.HTTP.gameid != "" && Tastatur.TASTE_ZUG_BEENDEN.Wert && Spiel2 != null &&
                Keyboard.GetState().IsKeyDown(Tastatur.ZUG_BEENDEN.Wert))
            {
                // Hochladen

                Tausch.SpielAktiv = false;
                SpielAktiv = false;
                String Pfad = "";
                if (HTTP.HTTP.gameid != "")
                {
                    Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";
                }
                else
                    Pfad = "Content\\Games\\temp\\";

                HTTP.HTTP.Dir(Pfad);
                MapWriter.Generieren(Spiel2);
                MapWriter.Speichern(Pfad + "Map.dat");
                Replay.End(Spiel2.players);
                Replay.Generieren(false);
                Replay.Speichern(Pfad + "Data.dat");
                File.Delete(Pfad + "OldMap.dat");
                File.Delete(Pfad + "OldData.dat");

                if (!Hauptfenster.Program.Formular.checkBox2.Checked)
                {
                    List<String> list = HTTP.HTTP.upload(String.Join("\r\n", MapWriter.list.ToArray()),
                        String.Join("\r\n", Replay.list.ToArray()), "1");
                    if (HTTP.HTTP.IsFailure(list))
                    {
                        // Fehler
                        Tausch.SpielAktiv = true;
                    }
                    else
                    {
                        // alle dateien löschen
                        File.Delete(Pfad + "Map.dat");
                        File.Delete(Pfad + "Data.dat");
                        File.Delete(Pfad + "GameInfo.dat");

                        Spiel2 = null;
                        //  Hauptfenster.Program.Formular.button4.Show();

                        Hauptfenster.Program.Formular.Width = 909;
                        Hauptfenster.Program.Formular.Height = 611;
                        Hauptfenster.Program.Formular.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                        Tausch.SpielAktiv = false;
                        if (HTTP.HTTP.gameid != "")
                        {
                            TabPage[] qq =
                            {
                                Hauptfenster.Program.Formular.tabPage4,
                                Hauptfenster.Program.Formular.tabPage5, Hauptfenster.Program.Formular.tabPage1
                            };
                            Hauptfenster.Program.Formular.Show(qq, Hauptfenster.Program.Formular.tabPage5);
                        }
                        else
                        {
                            TabPage[] qq =
                            {
                                Hauptfenster.Program.Formular.tabPage4,
                                Hauptfenster.Program.Formular.tabPage1
                            };
                            Hauptfenster.Program.Formular.Show(qq, Hauptfenster.Program.Formular.tabPage1);
                        }

                        FormState.Restore(Hauptfenster.Program.Formular);

                        Hauptfenster.Program.Formular.tabControl1.Show();
                        Hauptfenster.Program.Formular.pictureBox1.Hide();
                        HTTP.HTTP.gameid = "";
                        return;
                    }
                }
            }

            #endregion ZUG_BEENDEN

            #region SPEICHERN

            if (Tastatur.TASTE_SPEICHERN.Wert && Spiel2 != null &&
                Keyboard.GetState().IsKeyDown(Tastatur.SPEICHERN.Wert))
            {
                // Nur Speichern
                String Pfad = "";
                if (HTTP.HTTP.gameid != "")
                {
                    Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";
                }
                else
                    Pfad = "Content\\Games\\temp\\";

                HTTP.HTTP.Dir(Pfad);
                MapWriter.Generieren(Spiel2);
                MapWriter.Speichern(Pfad + "Map.dat");
                Replay.End(Spiel2.players);
                Replay.Generieren(true);
                Replay.Speichern(Pfad + "Data.dat");
                // alle dateien löschen
                File.Delete(Pfad + "OldMap.dat");
                File.Delete(Pfad + "OldData.dat");

                if (HTTP.HTTP.gameid != "" && !Hauptfenster.Program.Formular.checkBox2.Checked)
                {
                    List<String> list = HTTP.HTTP.upload(String.Join("\r\n", MapWriter.list.ToArray()),
                        String.Join("\r\n", Replay.list.ToArray()), "0");
                    if (HTTP.HTTP.IsFailure(list))
                    {
                        // Fehler
                    }
                }
            }

            #endregion SPEICHERN

            #region Sounds

            if (Spiel2.CurrentPlayer >= 0 && Spiel2.CurrentPlayer < Spiel2.players.Count())
            {
                if (Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (Sounds.PanzerMove[i] == null) continue;
                        if (
                            Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] == i) continue;
                        for (int b = 0; b < Sounds.PanzerMove[i].Channel.Count; b++)
                        {
                            Sounds.PanzerMove[i].StopSound(b);
                        }
                    }
                }
            }

            if ((Tastatur.TASTE_BEWEGUNG_LINKS.Wert || Tastatur.TASTE_BEWEGUNG_RECHTS.Wert) && Spiel2 != null &&
                (!Tastatur.TASTE_BEWEGUNG_LINKS.Wert ||
                 (Tastatur.TASTE_BEWEGUNG_LINKS.Wert && Keyboard.GetState().IsKeyUp(Tastatur.BEWEGUNG_LINKS.Wert))) &&
                (!Tastatur.TASTE_BEWEGUNG_RECHTS.Wert ||
                 (Tastatur.TASTE_BEWEGUNG_RECHTS.Wert && Keyboard.GetState().IsKeyUp(Tastatur.BEWEGUNG_RECHTS.Wert))))
            {
                //keybState = Keyboard.GetState();
                if (Spiel2.players[Spiel2.CurrentPlayer].CurrentTank < 0) return;
                if (Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >=
                    Spiel2.players[Spiel2.CurrentPlayer].KindofTank.Count) return;
                // Sounds.PanzerMove2[Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]].Stop();

                if (
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]] != null)
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]].StopSound(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
            }

            #endregion Sounds

            if (Spiel2 != null && Spiel2.SpielerAktiv() > 1 && SpielAktiv)
            {
                #region Tunnel

                int spieler = Spiel2.CurrentPlayer;
                int tank = Spiel2.players[spieler].CurrentTank;
                if (Spiel2 != null &&
                    (currentState.IsConnected && currentState.Buttons.X == ButtonState.Pressed ||
                     (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Keys.LeftShift))))
                {
                    if (Spiel2.players[spieler].GibTunnelAnAktuellerPanzerposition() > -1)
                    {
                        Spiel2.players[spieler].ImTunnel = true;
                    }
                }
                else
                    Spiel2.players[spieler].ImTunnel = false;

                #endregion Tunnel

                #region Rotation

                int soundid = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;

                if (((currentState.IsConnected && currentState.ThumbSticks.Left.Y < 0.5f) ||
                     (!currentState.IsConnected && Keyboard.GetState().IsKeyUp(Tastatur.BEWEGUNG_HOCH.Wert))) &&
                    ((currentState.IsConnected && currentState.ThumbSticks.Left.X > -0.5f) ||
                     (!currentState.IsConnected && Keyboard.GetState().IsKeyUp(Tastatur.BEWEGUNG_RUNTER.Wert))))
                {
                    //  int typ = Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];

                    for (int i = 0; i < Sounds.Panzer_rohr_end.Count(); i++)
                    {
                        if (Sounds.Panzer_rohr_end[i] != null)
                            if (Sounds.Panzer_rohrmode[i] > 0 && !Sounds.Panzer_rohr_end[i].IsPlaying(soundid))
                            {
                                Sounds.Panzer_rohr_loop[i].StopSound(soundid);
                                Sounds.Panzer_rohr_begin[i].StopSound(soundid);

                                Sounds.Panzer_rohr_end[i].PlaySound(soundid);
                                Sounds.Panzer_rohrmode[i] = 0;
                            }
                    }
                }

                if (Tastatur.TASTE_BEWEGUNG_HOCH.Wert &&
                    ((currentState.IsConnected && currentState.ThumbSticks.Left.Y > 0.5f) ||
                     (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_HOCH.Wert))))
                // && !Keyboard.GetState().IsKeyDown(Tastatur.SCHUSS.Wert)
                {
                    if (
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ] == 2 && Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == 9)
                    {
                    }
                    else
                    {
                        // Rohr nach Links bewegen
                        if (Spiel2.IsServer)
                        {
                            if (
                                Spiel2.players[Spiel2.CurrentPlayer].overreach[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank])
                            {
                                Spiel2.Current_Rohr_Right();
                            }
                            else
                                Spiel2.Current_Rohr_Left();
                        }
                        else
                            Client.Send("UP");

                        /*  int typ = Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];

                          if (Sounds.Panzer_rohr_end[typ] != null)
                          {
                              if (Sounds.Panzer_rohr_end[typ].State == SoundState.Playing)
                              {
                                  Sounds.Panzer_rohr_end[typ].Stop();
                                  Sounds.Panzer_rohrmode[typ] = 0;
                              }

                              if (Sounds.Panzer_rohrmode[typ] == 0 && Sounds.Panzer_rohr_begin[typ].State != SoundState.Playing)
                              {
                                  Sounds.Panzer_rohr_begin[typ].Play();
                                  Sounds.Panzer_rohrmode[typ] = 1;
                              }
                              else
                                  if (Sounds.Panzer_rohrmode[typ] == 1 && Sounds.Panzer_rohr_loop[typ].State != SoundState.Playing && Sounds.Panzer_rohr_begin[typ].State != SoundState.Playing)
                                  {
                                      Sounds.Panzer_rohr_loop[typ].Play();
                                      Sounds.Panzer_rohrmode[typ] = 2;
                                  }
                          }*/
                    }
                }

                if (Tastatur.TASTE_BEWEGUNG_RUNTER.Wert &&
                    ((currentState.IsConnected && currentState.ThumbSticks.Left.Y < -0.5f) ||
                     (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RUNTER.Wert))))
                // && !Keyboard.GetState().IsKeyDown(Tastatur.SCHUSS.Wert)
                {
                    if (
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ] == 2 && Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == 9)
                    {
                    }
                    else
                    {
                        // Rohr nach Rechts bewegen
                        if (Spiel2.IsServer)
                        {
                            if (
                                Spiel2.players[Spiel2.CurrentPlayer].overreach[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank])
                            {
                                Spiel2.Current_Rohr_Left();
                            }
                            else
                                Spiel2.Current_Rohr_Right();
                        }
                        else
                            Client.Send("DOWN");

                        /*  int typ = Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];

                          if (Sounds.Panzer_rohr_end[typ] != null)
                          {
                              if (Sounds.Panzer_rohr_end[typ].State == SoundState.Playing)
                              {
                                  Sounds.Panzer_rohr_end[typ].Stop();
                                  Sounds.Panzer_rohrmode[typ] = 0;
                              }

                              if (Sounds.Panzer_rohrmode[typ] == 0 && Sounds.Panzer_rohr_begin[typ].State != SoundState.Playing)
                              {
                                  Sounds.Panzer_rohr_begin[typ].Play();
                                  Sounds.Panzer_rohrmode[typ] = 1;
                              }
                              else
                                  if (Sounds.Panzer_rohrmode[typ] == 1 && Sounds.Panzer_rohr_loop[typ].State != SoundState.Playing && Sounds.Panzer_rohr_begin[typ].State != SoundState.Playing)
                                  {
                                      Sounds.Panzer_rohr_loop[typ].Play();
                                      Sounds.Panzer_rohrmode[typ] = 2;
                                  }
                          }*/
                    }
                }

                #endregion Rotation

                #region Shot

                if (Spiel2.CurrentPlayer >= 0 && Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
                    if ((!Spiel.TIMEOUT.Wert || Spiel2.Timeout > 0) && (!Spiel.SCHUESSE.Wert || Spiel2.Schuesse > 0) &&
                        (!Spieler.WAFFEN_COOLDOWN.Wert ||
                         Spiel2.players[Spiel2.CurrentPlayer].Cooldown[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] <=
                         0) &&
                        (!Spiel.MUNITION.Wert ||
                         Spiel2.players[Spiel2.CurrentPlayer].Munition[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]
                             [Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] > 0) &&
                        (!Spiel.ACTION_POINTS.Wert ||
                         Spiel2.players[Spiel2.CurrentPlayer].ActionPoints >=
                         Waffendaten.APKosten[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon]) &&
                        Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 0)
                    {
                        if (Tastatur.TASTE_SCHUSS.Wert &&
                            ((currentState.IsConnected && currentState.Buttons.A == ButtonState.Pressed) ||
                             (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Tastatur.SCHUSS.Wert))) &&
                            Fahrzeugdaten.ShootableAmmunition[
                                Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                                Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] >= 1)
                        {
                            if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.bunker &&
                                Spiel2.PrüfeBunkerbau(
                                    Spiel2.players[Spiel2.CurrentPlayer].pos[
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]))
                            {
                                Spiel2.increaseshot = true;
                            }
                            else if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.tunnel &&
                                     Spiel2.PrüfeTunnelbau(
                                         Spiel2.players[Spiel2.CurrentPlayer].pos[
                                             Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]))
                            {
                                Spiel2.increaseshot = true;
                            }
                            else if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.geschütz)
                            {
                                Spiel2.increaseshot = true;
                            }
                            else if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.geschütz2)
                            {
                                Spiel2.increaseshot = true;
                            }
                        }

                        if (Tastatur.TASTE_SCHUSS.Wert &&
                            ((currentState.IsConnected && currentState.Buttons.A == ButtonState.Released) ||
                             (!currentState.IsConnected && Keyboard.GetState().IsKeyUp(Tastatur.SCHUSS.Wert))) &&
                            Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 0)
                        {
                            if (Spiel2.increaseshot && (!Spiel.TIMEOUT.Wert || Spiel2.Timeout > 0))
                            {
                                bool abgeschlossen = false;
                                if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.bunker)
                                {
                                    if (
                                        Spiel2.PrüfeBunkerbau(
                                            Spiel2.players[Spiel2.CurrentPlayer].pos[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]))
                                    {
                                        abgeschlossen = true;
                                        Spiel2.AddBunker(Spiel2.CurrentPlayer,
                                            Spiel2.players[Spiel2.CurrentPlayer].pos[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                                    }
                                }
                                else if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.tunnel)
                                {
                                    if (
                                        Spiel2.PrüfeTunnelbau(
                                            Spiel2.players[Spiel2.CurrentPlayer].pos[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]))
                                    {
                                        abgeschlossen = true;
                                        Spiel2.AddTunnel(Spiel2.CurrentPlayer,
                                            Spiel2.players[Spiel2.CurrentPlayer].pos[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] -
                                            new Vector2(Texturen.tunnel.Width / 2 * Tunnel.SKALIERUNG, 0));
                                    }
                                }
                                else if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.geschütz)
                                {
                                    abgeschlossen = true;
                                    Spiel2.AddGeschütz(Spiel2.CurrentPlayer,
                                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                                }
                                else if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == Waffendaten.geschütz2)
                                {
                                    abgeschlossen = true;
                                    Spiel2.AddGeschütz2(Spiel2.CurrentPlayer,
                                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                                }

                                if (abgeschlossen)
                                {
                                    if (Spiel.MUNITION.Wert)
                                        Spiel2.players[Spiel2.CurrentPlayer].Munition[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank][
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon]--;
                                    if (Spieler.WAFFEN_COOLDOWN.Wert)
                                        Spiel2.players[Spiel2.CurrentPlayer].Cooldown[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] =
                                            Waffendaten.APKosten[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon];
                                    if (Spiel.ACTION_POINTS.Wert)
                                        Spiel2.players[Spiel2.CurrentPlayer].ActionPoints -=
                                            Waffendaten.APKosten[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon];
                                    if (Spiel.SCHUESSE.Wert) Spiel2.Schuesse--;
                                }
                                Spiel2.increaseshot = false;
                            }
                        }
                    }
                    else if (Spiel2.CurrentPlayer >= 0 && Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
                        if ((!Spiel.TIMEOUT.Wert || Spiel2.Timeout > 0) && (!Spiel.SCHUESSE.Wert || Spiel2.Schuesse > 0) &&
                            (!Spieler.WAFFEN_COOLDOWN.Wert ||
                             Spiel2.players[Spiel2.CurrentPlayer].Cooldown[
                                 Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] <= 0) &&
                            (!Spiel.MUNITION.Wert ||
                             Spiel2.players[Spiel2.CurrentPlayer].Munition[
                                 Spiel2.players[Spiel2.CurrentPlayer].CurrentTank][
                                     Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] > 0 ||
                             Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 5) &&
                            (!Spiel.ACTION_POINTS.Wert ||
                             Spiel2.players[Spiel2.CurrentPlayer].ActionPoints >=
                             Waffendaten.APKosten[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon]) &&
                            Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] > 0)
                        {
                            if (Tastatur.TASTE_SCHUSS.Wert &&
                                ((currentState.IsConnected && currentState.Buttons.A == ButtonState.Pressed) ||
                                 (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Tastatur.SCHUSS.Wert))) &&
                                Fahrzeugdaten.ShootableAmmunition[
                                    Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] >= 1)
                            {
                                //   keybState = Keyboard.GetState();
                                if (!Spiel2.increaseshot && !Spiel2.increaseairstrike)
                                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower =
                                        Waffendaten.minShootingpower[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon];

                                Spiel2.increaseshot = true;
                                if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 2)
                                {
                                    Spiel2.increaseairstrike = true;
                                }
                            }

                            if (Tastatur.TASTE_SCHUSS.Wert &&
                                ((currentState.IsConnected && currentState.Buttons.A == ButtonState.Released) ||
                                 (!currentState.IsConnected && Keyboard.GetState().IsKeyUp(Tastatur.SCHUSS.Wert)) ||
                                 ((Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 4 ||
                                   Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 5) &&
                                  Spiel2.increaseshot)) &&
                                Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] > 0)
                            {
                                bool ok = true;
                                if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 4)
                                {
                                    if (qq % 4 == 0)
                                    {
                                        qq = 0;
                                    }
                                    else
                                        ok = false;

                                    qq++;
                                }

                                if (Spiel2.increaseshot && (!Spiel.TIMEOUT.Wert || Spiel2.Timeout > 0) && ok)
                                {
                                    // keybState = Keyboard.GetState();
                                    // Vector2 a = Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];
                                    Vector2 a =
                                        Spiel2.players[Spiel2.CurrentPlayer].Rohrspitze(
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);

                                    if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] !=
                                        5)
                                        if (Spiel.MUNITION.Wert)
                                            Spiel2.players[Spiel2.CurrentPlayer].Munition[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank][
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon]--;
                                    if (Spieler.WAFFEN_COOLDOWN.Wert)
                                        Spiel2.players[Spiel2.CurrentPlayer].Cooldown[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] =
                                            Waffendaten.APKosten[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon];
                                    if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] !=
                                        5)
                                        if (Spiel.ACTION_POINTS.Wert)
                                            Spiel2.players[Spiel2.CurrentPlayer].ActionPoints -=
                                                Waffendaten.APKosten[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon];
                                    if (Spiel.SCHUESSE.Wert) Spiel2.Schuesse--;

                                    if (!Spiel2.increaseairstrike)
                                    {
                                        if (
                                            Fahrzeugdaten.ShootableAmmunition[
                                                Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] >= 1 &&
                                            (Spiel2.players[Spiel2.CurrentPlayer].shootingPower > 2 ||
                                             Waffendaten.Verschiessbar[
                                                 Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 3))
                                        {
                                            /*if (Client.isRunning)
                                                {
                                                    Client.Send("SHOT " + Spiel2.players[Spiel2.CurrentPlayer].shootingPower);
                                                }
                                                else
                                                    if (Server.isRunning)
                                                    {
                                                        Server.Send("SHOT " + Spiel2.players[Spiel2.CurrentPlayer].shootingPower);
                                                    }*/

                                            if (
                                                Waffendaten.Verschiessbar[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 3)
                                            {
                                                // Objektezugriffe, werden nicht verschossen, aber durch schuss ausgelöst
                                                Spieler tmp = Spiel2.players[Spiel2.CurrentPlayer];
                                                int id = tmp.CurrentTank;
                                                Spiel2.players[Spiel2.CurrentPlayer].Minen.Add(
                                                    new Mine((int)tmp.pos[id].X, (int)tmp.pos[id].Y,
                                                        (int)
                                                            (Waffendaten.Daten2[
                                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon].Z),
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon,
                                                        Spiel2.players[Spiel2.CurrentPlayer].Minen.Count));
                                            }
                                            else
                                            {
                                                // normale Rakete

                                                // a.Y -= (float)Math.Sin(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75 + 25;
                                                //   a.X -= (float)Math.Cos(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75;
                                                // Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 100;

                                                float angle =
                                                    Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];
                                                var up = new Vector2(0, -1);
                                                if (
                                                    Waffendaten.Verschiessbar[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 4)
                                                {
                                                    float merk = Spiel2.players[Spiel2.CurrentPlayer].shootingPower;
                                                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 10 +
                                                                                                         Spiel.incshoot;
                                                    //Spiel.rand.Next(50, (int) (Spiel2.players[Spiel2.CurrentPlayer].shootingPower+1));
                                                    Matrix rotMatrix =
                                                        Matrix.CreateRotationZ(
                                                            Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                                            Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] -
                                                            MathHelper.PiOver2 +
                                                            MathHelper.ToRadians(Spiel.rand.Next(-8, 9)));
                                                    Vector2 b = Vector2.Transform(up, rotMatrix);
                                                    b *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower /
                                                         (float)
                                                             Math.Log(
                                                                 Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                                                                 Math.E);
                                                    Spiel2.CurrentMissile = Spiel2.AddRakete(Spiel2.CurrentPlayer, a, b,
                                                        10, Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon,
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                                                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower = merk;
                                                }
                                                else if (
                                                    Waffendaten.Verschiessbar[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 5)
                                                {
                                                    // reparieren, erobern
                                                    if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon ==
                                                        Waffendaten.reparieren)
                                                    {
                                                        // Haus Reparieren
                                                        int player = Spiel2.CurrentPlayer;
                                                        int t = Spiel2.players[player].CurrentTank;
                                                        // if (Spiel2.players[player].KindofTank[t] == Fahrzeugdaten.BAUFAHRZEUG)
                                                        // {
                                                        for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
                                                        {
                                                            if (Spiel2.Haeuser.Lebenspunkte[i] >=
                                                                Spiel2.Haeuser.MaximaleLebenspunkte[i]) continue;
                                                            if (Spiel2.Haeuser.IsCollision2(i,
                                                                Spiel2.players[player].pos[t] + new Vector2(0, -2)))
                                                            {
                                                                int rep = Help.RepariereBild(
                                                                    Spiel2.Haeuser.Bild[i],
                                                                    Texturen.haus[Spiel2.Haeuser.HausTyp[i]], 25);
                                                                Spiel2.Haeuser.load(i);
                                                                Spiel2.Haeuser.Lebenspunkte[i] += rep;

                                                                if (rep > 0)
                                                                    if (Spiel.ACTION_POINTS.Wert)
                                                                        Spiel2.players[Spiel2.CurrentPlayer]
                                                                            .ActionPoints -=
                                                                            Waffendaten.APKosten[
                                                                                Spiel2.players[Spiel2.CurrentPlayer]
                                                                                    .CurrentWeapon];

                                                                break;
                                                            }
                                                        }
                                                        //}
                                                    }
                                                    else if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon ==
                                                             Waffendaten.erobern)
                                                    {
                                                        // Haus erobern
                                                        int player = Spiel2.CurrentPlayer;
                                                        int t = Spiel2.players[player].CurrentTank;
                                                        for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
                                                        {
                                                            if (Spiel2.Haeuser.IsCollision2(i,
                                                                Spiel2.players[player].pos[t] +
                                                                new Vector2(0, -2)))
                                                            {
                                                                if (Spiel2.Haeuser.Erobern(i, 10, player))
                                                                    if (Spiel.ACTION_POINTS.Wert)
                                                                        Spiel2.players[Spiel2.CurrentPlayer]
                                                                            .ActionPoints -=
                                                                            Waffendaten.APKosten[
                                                                                Spiel2.players[
                                                                                    Spiel2.CurrentPlayer]
                                                                                    .CurrentWeapon];

                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Matrix rotMatrix =
                                                        Matrix.CreateRotationZ(
                                                            Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                                            Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] -
                                                            MathHelper.PiOver2);
                                                    Vector2 b = Vector2.Transform(up, rotMatrix);
                                                    b *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower /
                                                         (float)
                                                             Math.Log(
                                                                 Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                                                                 Math.E);
                                                    Spiel2.CurrentMissile = Spiel2.AddRakete(Spiel2.CurrentPlayer, a,
                                                        b, 300 * 4, Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon,
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                                                }

                                                // abschuss rauch
                                                //  a = Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];
                                                //a.Y -= 25;
                                                //a.X += 25;
                                                // a.Y -= (float)Math.Sin(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75 + 50;
                                                // a.X -= (float)Math.Cos(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75;

                                                // up = new Vector2(0, -1);
                                                // rotMatrix = Matrix.CreateRotationZ(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] - (float)MathHelper.PiOver2);
                                                // b = Vector2.Transform(up, rotMatrix);
                                                //  b *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower / (float)Math.Log(Spiel2.players[Spiel2.CurrentPlayer].shootingPower, Math.E);
                                                if (
                                                    Waffendaten.Verschiessbar[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 4)
                                                {
                                                    // MG
                                                    Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, a, 4, 5, 150,
                                                        Time, new Vector3(0.7f, 1f, 1.2f), 18, 0);
                                                }
                                                else if (
                                                    Waffendaten.Verschiessbar[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 5)
                                                {
                                                    // reparieren, erobern
                                                }
                                                else
                                                    Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, a, 10,
                                                        35, 650, Time, new Vector3(0.7f, 1f, 1.2f), 18, 0);

                                                if (
                                                    Waffendaten.Verschiessbar[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] != 4 &&
                                                    Waffendaten.Verschiessbar[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] != 5)
                                                    Spiel2.Missile[Spiel2.CurrentMissile].focused = true;
                                                //CurrentMissle
                                                if ((currentState.IsConnected &&
                                                     currentState.Buttons.A == ButtonState.Released) ||
                                                    (!currentState.IsConnected &&
                                                     Keyboard.GetState().IsKeyUp(Tastatur.SCHUSS.Wert)))
                                                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                                            }
                                        }

                                        if ((currentState.IsConnected && currentState.Buttons.A == ButtonState.Released) ||
                                            (!currentState.IsConnected &&
                                             Keyboard.GetState().IsKeyUp(Tastatur.SCHUSS.Wert)))
                                        {
                                            Spiel2.increaseshot = false;
                                        }
                                    }
                                    else
                                    {
                                        // Airstrike abfeuern
                                        if (
                                            Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon
                                                ] == 2)
                                        {
                                            //Spiel2.increaseairstrike = false;
                                            Spiel2.increaseshot = false;
                                            if (
                                                Fahrzeugdaten.ShootableAmmunition[
                                                    Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] >= 1)
                                            {
                                                a.X = Spiel2.players[Spiel2.CurrentPlayer].shootingPower;
                                                Spiel2.Airstrike(a, Spiel2.CurrentPlayer);
                                                //  Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 2;
                                            }
                                        }
                                        else
                                        // Normale Waffe
                                        //     if (Rakete.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 1)
                                        {
                                            // Abstand
                                            a.X = Spiel2.players[Spiel2.CurrentPlayer].shootingPower;
                                            Spiel2.CurrentMissile = Spiel2.AddRakete(Spiel2.CurrentPlayer,
                                                new Vector2(a.X, Spiel.rand.Next(-1100, -200)),
                                                new Vector2(Spiel.rand.Next(-100, 100) / 25, -1), 300 * 4,
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon,
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);

                                            if (Spiel2.CurrentMissile != -1)
                                                Spiel2.Missile[Spiel2.CurrentMissile].focused = true; //CurrentMissle
                                            Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                                            Spiel2.increaseshot = false;
                                        }
                                    }

                                    //               Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                                    if (Spiel.SCHUESSE.Wert && Spiel2.Schuesse <= 0) Spiel2.Timeout = 0;
                                    //ZZ if (Spiel.SCHUESSE.Wert && Spiel2.Schuesse <= 0) Spiel2.next_player();
                                }
                            }
                        }
                        else if (!Spiel2.increaseshot &&
                                 ((currentState.IsConnected && currentState.Buttons.A == ButtonState.Released) ||
                                  (!currentState.IsConnected && Keyboard.GetState().IsKeyUp(Tastatur.SCHUSS.Wert))))
                        {
                            Spiel2.increaseairstrike = false;
                            Spiel2.increaseshot = false;
                            Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                            //    if (Spiel2.CurrentMissile >= 0 && Spiel2.CurrentMissile < Spiel2.Missile.Count()) Spiel2.Missile[Spiel2.CurrentMissile].focused = false;
                        }
            }

                #endregion Shot

            if (keybState != Keyboard.GetState() || currentState.Buttons != oldcurrentState.Buttons ||
                (currentState.Triggers.Left >= 0.1f && oldcurrentState.Triggers.Left < 0.1f) ||
                (currentState.Triggers.Right >= 0.1f && oldcurrentState.Triggers.Right < 0.1f))
            {
                #region Panzerwahl

                Spieler current = Spiel2.players[Spiel2.CurrentPlayer];
                if (Tastatur.TASTE_FAHRZEUGWAHL_LINKS.Wert &&
                    (currentState.IsConnected && currentState.Buttons.LeftShoulder == ButtonState.Pressed ||
                     Keyboard.GetState().IsKeyDown(Tastatur.FAHRZEUGWAHL_LINKS.Wert)) &&
                    current.getPanzerID(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank,
                        current.OrdneEigenePanzerAnhandDerKarte()) > 0)
                {
                    keybState = Keyboard.GetState();
                    List<int> list = current.OrdneEigenePanzerAnhandDerKarte();
                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank =
                        list[current.getPanzerID(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank, list) - 1];
                    int a = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                    if (Spiel2.increaseairstrike)
                    {
                        Spiel2.players[Spiel2.CurrentPlayer].shootingPower =
                            Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].X;
                        if (!Spiel2.Moving_Map)
                            Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                                Spiel2.players[Spiel2.CurrentPlayer].pos[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));
                    }
                    else if (!Spiel2.Moving_Map)
                        Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                            Spiel2.players[Spiel2.CurrentPlayer].pos[
                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));

                    //  if (Client.isRunning) Client.Send("PANZER " + Spiel2.players[Spiel2.CurrentPlayer].CurrentTank + " " + Spiel2.CurrentPlayer);
                    //  if (Server.isRunning) Server.Send("PANZER " + Spiel2.players[Spiel2.CurrentPlayer].CurrentTank + " " + Spiel2.CurrentPlayer);

                    if (Mod.SPIELERMENU_VISIBLE.Wert)
                    {
                        //   if (CurrentWaeponOld != Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon)
                        //Spielermenu.CurrentPlayerID = Spiel2.CurrentPlayer;
                        //Spielermenu.CurrentTankID = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                        //Spielermenu.switchButtons(Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon, 0);
                    }
                }

                if (Tastatur.TASTE_FAHRZEUGWAHL_RECHTS.Wert &&
                    (currentState.IsConnected && currentState.Buttons.RightShoulder == ButtonState.Pressed ||
                     Keyboard.GetState().IsKeyDown(Tastatur.FAHRZEUGWAHL_RECHTS.Wert)) &&
                    current.getPanzerID(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank,
                        current.OrdneEigenePanzerAnhandDerKarte()) < Spiel2.players[Spiel2.CurrentPlayer].pos.Count - 1)
                {
                    keybState = Keyboard.GetState();
                    List<int> list = current.OrdneEigenePanzerAnhandDerKarte();
                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank =
                        list[current.getPanzerID(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank, list) + 1];
                    int a = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                    if (Spiel2.increaseairstrike)
                    {
                        Spiel2.players[Spiel2.CurrentPlayer].shootingPower =
                            Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].X;
                        if (!Spiel2.Moving_Map)
                            Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                                Spiel2.players[Spiel2.CurrentPlayer].pos[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));
                    }
                    else if (!Spiel2.Moving_Map)
                        Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                            Spiel2.players[Spiel2.CurrentPlayer].pos[
                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));

                    //  if (Client.isRunning) Client.Send("PANZER " + Spiel2.players[Spiel2.CurrentPlayer].CurrentTank + " " + Spiel2.CurrentPlayer);
                    //  if (Server.isRunning) Server.Send("PANZER " + Spiel2.players[Spiel2.CurrentPlayer].CurrentTank + " " + Spiel2.CurrentPlayer);

                    if (Mod.SPIELERMENU_VISIBLE.Wert)
                    {
                        // if (CurrentWaeponOld != Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon)
                        //Spielermenu.CurrentPlayerID = Spiel2.CurrentPlayer;
                        //Spielermenu.CurrentTankID = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                        //Spielermenu.switchButtons(Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon, 0);
                    }
                }

                #endregion Panzerwahl

                #region Waffenwahl

                int CurrentTankNow = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                int CurrentWeaponNow = Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon;
                int CurrentWeaponOld = Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon;

                if (Tastatur.TASTE_WAFFENWAHL_LINKS.Wert &&
                    ((currentState.IsConnected && currentState.Triggers.Left >= 0.1f) ||
                     (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Tastatur.WAFFENWAHL_LINKS.Wert))))
                {
                    keybState = Keyboard.GetState();
                    int begin = Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon;
                    CurrentWeaponNow--;
                    if (CurrentWeaponNow < 0) CurrentWeaponNow = Waffendaten.Daten.Count() - 1;
                    /*Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon--;
                    if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon < 0) Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon = Rakete.Daten.Count() - 1;
                    int a = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;*/

                    for (;
                        Fahrzeugdaten.ShootableAmmunition[
                            Spiel2.players[Spiel2.CurrentPlayer].KindofTank[CurrentTankNow], CurrentWeaponNow] <= 0; )
                    {
                        CurrentWeaponNow--;
                        if (CurrentWeaponNow < 0) CurrentWeaponNow = Waffendaten.Daten.Count() - 1;
                        if (CurrentWeaponNow == begin) break;
                    }

                    if (CurrentWeaponNow != begin)
                    {
                        //   if (Client.isRunning) Client.Send("WAFFE " + CurrentWeaponNow + " " + Spiel2.CurrentPlayer);
                        //    if (Server.isRunning) Server.Send("WAFFE " + CurrentWeaponNow + " " + Spiel2.CurrentPlayer);
                    }
                    Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon = CurrentWeaponNow;
                    if (Waffendaten.Verschiessbar[CurrentWeaponNow] == 2)
                    {
                        Spiel2.increaseairstrike = true;
                        Spiel2.players[Spiel2.CurrentPlayer].shootingPower =
                            Spiel2.players[Spiel2.CurrentPlayer].pos[CurrentTankNow].X;
                        if (!Spiel2.Moving_Map)
                            Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                                Spiel2.players[Spiel2.CurrentPlayer].pos[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));
                    }
                    else
                    {
                        Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                        Spiel2.increaseairstrike = false;
                    }
                }

                if (Tastatur.TASTE_WAFFENWAHL_RECHTS.Wert &&
                    ((currentState.IsConnected && currentState.Triggers.Right >= 0.1f) ||
                     (!currentState.IsConnected && Keyboard.GetState().IsKeyDown(Tastatur.WAFFENWAHL_RECHTS.Wert))))
                {
                    keybState = Keyboard.GetState();

                    int begin = CurrentWeaponNow;
                    CurrentWeaponNow++;
                    if (CurrentWeaponNow >= Waffendaten.Daten.Count()) CurrentWeaponNow = 0;
                    /* Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon++;
                     if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon>=Rakete.Daten.Count()) Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon = 0;
                     int a = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;*/
                    for (;
                        Fahrzeugdaten.ShootableAmmunition[
                            Spiel2.players[Spiel2.CurrentPlayer].KindofTank[CurrentTankNow], CurrentWeaponNow] <= 0; )
                    {
                        CurrentWeaponNow++;
                        if (CurrentWeaponNow >= Waffendaten.Daten.Count()) CurrentWeaponNow = 0;
                        if (CurrentWeaponNow == begin) break;
                    }

                    if (CurrentWeaponNow != begin)
                    {
                        //   if (Client.isRunning) Client.Send("WAFFE " + CurrentWeaponNow + " " + Spiel2.CurrentPlayer);
                        //   if (Server.isRunning) Server.Send("WAFFE " + CurrentWeaponNow + " " + Spiel2.CurrentPlayer);
                    }
                    Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon = CurrentWeaponNow;
                    if (Waffendaten.Verschiessbar[CurrentWeaponNow] == 2)
                    {
                        Spiel2.increaseairstrike = true;
                        Spiel2.players[Spiel2.CurrentPlayer].shootingPower =
                            Spiel2.players[Spiel2.CurrentPlayer].pos[CurrentTankNow].X;
                        if (!Spiel2.Moving_Map)
                            Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                                Spiel2.players[Spiel2.CurrentPlayer].pos[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));
                    }
                    else
                    {
                        Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                        Spiel2.increaseairstrike = false;
                    }
                }

                if (Mod.SPIELERMENU_VISIBLE.Wert)
                {
                    //  if (CurrentWeaponOld != Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon)
                    //      Spielermenu.switchButtons(Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon, 0);
                }

                #endregion Waffenwahl
            }

            #region Freier-Modus

            if (Tastatur.TASTE_LINKS.Wert &&
                ((currentState.IsConnected && currentState.ThumbSticks.Right.X < 0.0f) ||
                 Keyboard.GetState().IsKeyDown(Tastatur.LINKS.Wert)))
            {
                if (Mod.FREIE_KARTENBEWEGUNG.Wert && Spiel2.Moving_Map)
                {
                    Spiel2.Set_Focus_X(new Vector2(Spiel2.Next_Fenster.X - 25 + screenWidth / 2, Spiel2.Next_Fenster.Y));
                }
                else if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 2)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower -= 14;
                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower =
                        Spiel.Position(Spiel2.players[Spiel2.CurrentPlayer].shootingPower);
                    //if (Spiel2.players[Spiel2.CurrentPlayer].shootingPower < 0) Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                    //if (Spiel2.players[Spiel2.CurrentPlayer].shootingPower >= Spiel2.Spielfeld.Length - 1) Spiel2.players[Spiel2.CurrentPlayer].shootingPower = Spiel2.Spielfeld.Length - 1;
                    if (!Spiel2.Moving_Map)
                        Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                            Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));
                }
            }

            if (Tastatur.TASTE_RECHTS.Wert &&
                ((currentState.IsConnected && currentState.ThumbSticks.Right.X > 0.0f) ||
                 Keyboard.GetState().IsKeyDown(Tastatur.RECHTS.Wert)))
            {
                if (Mod.FREIE_KARTENBEWEGUNG.Wert && Spiel2.Moving_Map)
                {
                    Spiel2.Set_Focus_X(new Vector2(Spiel2.Next_Fenster.X + 25 + screenWidth / 2, Spiel2.Next_Fenster.Y));
                }
                else if (Waffendaten.Verschiessbar[Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 2)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower += 14;
                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower =
                        Spiel.Position(Spiel2.players[Spiel2.CurrentPlayer].shootingPower);
                    //if (Spiel2.players[Spiel2.CurrentPlayer].shootingPower < 0) Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                    //if (Spiel2.players[Spiel2.CurrentPlayer].shootingPower >= Spiel2.Spielfeld.Length - 1) Spiel2.players[Spiel2.CurrentPlayer].shootingPower = Spiel2.Spielfeld.Length - 1;
                    if (!Spiel2.Moving_Map)
                        Spiel2.Set_Focus(new Vector2(Spiel2.players[Spiel2.CurrentPlayer].shootingPower,
                            Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Y));
                }
            }

            #endregion Freier-Modus

            #region Fahrlogik

            //   if (!Spiel2.Moving_Map)
            //   {
            if (wait <= 0 && Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == 8)
            {
                if (Tastatur.TASTE_BEWEGUNG_RUNTER.Wert && Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RUNTER.Wert) &&
                    Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] ==
                    2)
                {
                    Kartenfunktionen.Graben_Runter(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }

                if (Tastatur.TASTE_BEWEGUNG_LINKS.Wert && Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_LINKS.Wert) &&
                    Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] ==
                    2)
                {
                    Kartenfunktionen.Graben_Links(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }

                if (Tastatur.TASTE_BEWEGUNG_RECHTS.Wert && Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RECHTS.Wert) &&
                    Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] ==
                    2)
                {
                    Kartenfunktionen.Graben_Rechts(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
            }
            else if (wait <= 0 && Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == 9)
            {
                if (Tastatur.TASTE_BEWEGUNG_RUNTER.Wert && Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RUNTER.Wert) &&
                    Tastatur.TASTE_BEWEGUNG_RECHTS.Wert && Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RECHTS.Wert) &&
                    Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] ==
                    2)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].overreach[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] =
                        true;
                    Kartenfunktionen.Bauen_Rechts_Runter(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
                else if (Tastatur.TASTE_BEWEGUNG_RUNTER.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RUNTER.Wert) &&
                         Tastatur.TASTE_BEWEGUNG_LINKS.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_LINKS.Wert) &&
                         Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                             Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                             ] == 2)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].overreach[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]
                        = false;
                    Kartenfunktionen.Bauen_Links_Runter(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
                else if (Tastatur.TASTE_BEWEGUNG_HOCH.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_HOCH.Wert) &&
                         Tastatur.TASTE_BEWEGUNG_RECHTS.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RECHTS.Wert) &&
                         Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                             Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] == 2)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].overreach[
                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] = true;
                    Kartenfunktionen.Bauen_Rechts_Hoch(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
                else if (Tastatur.TASTE_BEWEGUNG_HOCH.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_HOCH.Wert) &&
                         Tastatur.TASTE_BEWEGUNG_LINKS.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_LINKS.Wert) &&
                         Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                             Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] == 2)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].overreach[
                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] = false;
                    Kartenfunktionen.Bauen_Links_Hoch(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
                else if (Tastatur.TASTE_BEWEGUNG_HOCH.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_HOCH.Wert) &&
                         Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                             Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] == 2)
                {
                    Spiel2.players[Spiel2.CurrentPlayer].overreach[
                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] = false;
                    Kartenfunktionen.Bauen_Hoch(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
                else if (Tastatur.TASTE_BEWEGUNG_LINKS.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_LINKS.Wert) &&
                         Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                             Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] == 2)
                {
                    Kartenfunktionen.Bauen_Links(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
                else if (Tastatur.TASTE_BEWEGUNG_RECHTS.Wert &&
                         Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RECHTS.Wert) &&
                         Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                             Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] == 2)
                {
                    Kartenfunktionen.Bauen_Rechts(
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]);
                    wait = 20;
                }
            }
            // }

            if (Tastatur.TASTE_BEWEGUNG_RECHTS.Wert && Keyboard.GetState().IsKeyUp(Tastatur.BEWEGUNG_RECHTS.Wert) &&
                Tastatur.TASTE_BEWEGUNG_LINKS.Wert && Keyboard.GetState().IsKeyUp(Tastatur.BEWEGUNG_LINKS.Wert) &&
                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
            {
                Spiel2.players[Spiel2.CurrentPlayer].logik[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Motoren
                    .Abschalten();
            }

            if (Tastatur.TASTE_BEWEGUNG_LINKS.Wert &&
                ((currentState.IsConnected && currentState.ThumbSticks.Left.X < -0.5f) ||
                 Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_LINKS.Wert)) &&
                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
            {
                // keybState = Keyboard.GetState();
                if (!Spiel2.players[Spiel2.CurrentPlayer].ImTunnel ||
                    (keybState != Keyboard.GetState() || currentState.ThumbSticks.Left.X < -0.5f))
                    if (Spiel2.IsServer)
                    {
                        Spiel2.Current_Left();
                    }
                    else
                        Client.Send("LEFT");

                Spiel2.players[Spiel2.CurrentPlayer].logik[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Motoren
                    .Beschleunigen();

                int dist = 0;
                if (Spiel2.Fenster.X >
                    Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].X)
                {
                    dist =
                        (int)
                            (Spiel2.Fenster.X -
                             Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]
                                 .X);
                }
                else
                    dist =
                        (int)
                            (Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]
                                .X - (Spiel2.Fenster.X));

                float wert =
                    Fahrzeugdaten.VOLUMES.Wert[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]];
                float a = wert - (wert / 2048) * dist;
                if (a < 0) a = 0;

                if (
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]] != null)
                {
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]].ResumeSound(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]].SetVolume(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank, a);
                    //if (Sounds.PanzerMove2[Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]].State != SoundState.Playing) Sounds.PanzerMove2[Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]].Resume();
                }
            }

            if (Tastatur.TASTE_BEWEGUNG_RECHTS.Wert &&
                ((currentState.IsConnected && currentState.ThumbSticks.Left.X > 0.5f) ||
                 Keyboard.GetState().IsKeyDown(Tastatur.BEWEGUNG_RECHTS.Wert)) &&
                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
            {
                //keybState = Keyboard.GetState();
                if (!Spiel2.players[Spiel2.CurrentPlayer].ImTunnel ||
                    (keybState != Keyboard.GetState() || currentState.ThumbSticks.Left.X > 0.5f))
                    if (Spiel2.IsServer)
                    {
                        Spiel2.Current_Right();
                    }
                    else
                        Client.Send("RIGHT");

                Spiel2.players[Spiel2.CurrentPlayer].logik[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].Motoren
                    .Beschleunigen();

                int dist = 0;
                if (Spiel2.Fenster.X >
                    Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank].X)
                {
                    dist =
                        (int)
                            (Spiel2.Fenster.X -
                             Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]
                                 .X);
                }
                else
                    dist =
                        (int)
                            (Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]
                                .X - (Spiel2.Fenster.X));

                float wert =
                    Fahrzeugdaten.VOLUMES.Wert[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]];
                float a = wert - (wert / 2048) * dist;
                if (a < 0) a = 0;
                if (
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]] != null)
                {
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]].ResumeSound(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                    Sounds.PanzerMove[
                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank
                            ]].SetVolume(Spiel2.players[Spiel2.CurrentPlayer].CurrentTank, a);
                }
            }

            if (wait > 0)
                wait--;

            #endregion Fahrlogik

            keybState = Keyboard.GetState();
        }

        /// <summary>
        ///     Hier werden alle Mauseingaben des Nutzers abgefangen
        /// </summary>
        private void MouseKeys()
        {
            int CurrentTank = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
            bool notizrightklicked = false;
            bool notizbereichleftklicked = false;

            if (Editor.visible && Spiel2 != null && Tausch.SpielAktiv)
            {
                Editor.MouseKeys(spriteBatch, device, mouseState, Spiel2);
            }

            if (!Editor.visible && Spiel2 != null && Tausch.SpielAktiv)
            {
                if (!Spielermenu.visible)
                {
                    BauMenü.mouseKeys(Help.GetMouseState(), Spiel2.Fenster, Spiel2);

                    if (!BauMenü.visible)
                    {
                        #region Notizfeld_klick

                        if (Spiel2.players[Spiel2.CurrentPlayer].Notiz.selected > -1)
                        {
                            Spiel2.players[Spiel2.CurrentPlayer].Notiz.Textfelder[
                                Spiel2.players[Spiel2.CurrentPlayer].Notiz.selected].MouseKeys(Spiel2.Fenster);
                        }

                        if (mouseState != Help.GetMouseState())
                            notizbereichleftklicked =
                                Spiel2.players[Spiel2.CurrentPlayer].Notiz.Notizbereich_klick(Spiel2.Fenster,
                                    GraphicsDevice);

                        #endregion Notizfeld_klick

                        #region Doppelklick_Notizen

                        if (!Spiel2.players[Spiel2.CurrentPlayer].Notiz.schreibend)
                        {
                            notizrightklicked = Spiel2.players[Spiel2.CurrentPlayer].Notiz.MouseKeys(Spiel2.Fenster,
                                GraphicsDevice, mouseState);

                            if (Clickbool == false && Click < 2 &&
                                Help.GetMouseState().LeftButton == ButtonState.Pressed)
                            {
                                Clickbool = true;
                            }
                            if (Click == 1)
                            {
                                clicktimer = clicktimer + 1;
                            }
                            if (clicktimer >= 20)
                            {
                                Click = 0;
                                clicktimer = 0;
                            }
                            if (Clickbool && Help.GetMouseState().LeftButton == ButtonState.Released)
                            {
                                Click = Click + 1;
                                Clickbool = false;
                            }

                            if (Click >= 2)
                            {
                                Vector2 temp = Help.GetMousePos() + Spiel2.Fenster;
                                Spiel2.players[Spiel2.CurrentPlayer].Notiz.AddNotiz(GraphicsDevice, temp, "", Content);
                                Click = 0;
                                clicktimer = 0;
                            }
                        }

                        #endregion Doppelklick_Notizen
                    }
                }
            }

            if (!Editor.visible && Mod.SPIELERMENU_VISIBLE.Wert)
                if (CurrentTank >= 0)
                {
                    if (Spielermenu.mouseKeys(mouseState, Spiel2.players[Spiel2.CurrentPlayer].Rucksack[CurrentTank],
                        Spiel2.players[Spiel2.CurrentPlayer].Effekte[CurrentTank]))
                    {
                        mouseState = Help.GetMouseState();
                        return;
                    }

                    if (SetUpMenu.MouseKeys(mouseState, spriteBatch))
                    {
                        mouseState = Help.GetMouseState();
                        return;
                    }

                    #region Pausenmenü

                    if (Mod.PAUSEMENU_VISIBLE.Wert)
                    {
                        switch (pauseMenu.mouseKeys(mouseState))
                        {
                            case 0:
                                {
                                    //neues zufalliges spiel
                                    Spiel2 = null;
                                    StarteNeuesSpiel();
                                    mouseState = Help.GetMouseState();
                                    // MausAktiv = false;
                                    //    if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu.show();
                                    pauseMenu.hide();
                                    Tausch.SpielAktiv = true;
                                    mouseState = Help.GetMouseState();
                                    return;
                                }

                            case 1:
                                {
                                    //lademenu
                                    Ladenmenu.show();
                                    pauseMenu.hide();
                                    Tausch.SpielAktiv = false;
                                    mouseState = Help.GetMouseState();
                                    return;
                                }
                            case 2:
                                {
                                    //optionen anzeigen
                                    mouseState = Help.GetMouseState();
                                    return;
                                }
                            case 3:
                                {
                                    //zuruck
                                    // MausAktiv = false;
                                    // if (Mod.SPIELERMENU_VISIBLE.Wert) Spielermenu.show();
                                    pauseMenu.hide();
                                    SpielfeldEinblenden(60);
                                    Tausch.SpielAktiv = true;
                                    mouseState = Help.GetMouseState();
                                    return;
                                }
                            case 4:
                                {
                                    //zum Startmenu
                                    //MausAktiv = true;
                                    pauseMenu.hide();
                                    StartMenu.show();
                                    Tausch.SpielAktiv = false;
                                    mouseState = Help.GetMouseState();
                                    return;
                                }
                            default:
                                break;
                        }
                    }

                    #endregion Pausenmenü

                    #region Startmenü

                    switch (StartMenu.MouseKeys(mouseState))
                    {
                        case 0:
                            {
                                Spiel2 = null;
                                StarteNeuesSpiel();
                                mouseState = Help.GetMouseState();
                                StartMenu.hide();
                                //  MausAktiv = false;
                                Tausch.SpielAktiv = true;
                                mouseState = Help.GetMouseState();
                                return;
                            }
                        case 1:
                            {
                                //laden
                                mouseState = Help.GetMouseState();
                                return;
                            }
                        case 2:
                            {
                                //optionen anzeigen
                                mouseState = Help.GetMouseState();
                                return;
                            }
                        case 3:
                            {
                                //  MausAktiv = false;
                                Exit();
                                mouseState = Help.GetMouseState();
                                return;
                            }
                        default:
                            break;
                    }

                    #endregion Startmenü

                    /*

            if (pregamemenuaufruf)
            {
                SetUpMenu.MouseKeys(mouseState, spriteBatch);
            }
            */

                    #region Lademenu

                    Saveinfo currentSaveinfo = Ladenmenu.MouseKeys(mouseState);
                    // Ladenmenu.KeyboardKeys(keybState);
                    switch (currentSaveinfo.button)
                    {
                        case 0:
                            {
                                //Spiel Speichen
                                //Mapwriter.WriteMap(currentSaveinfo.target, Spiel2);

                                break;
                            }
                        case 1:
                            {
                                //Spiel laden
                                /*Spiel2 = null;
                                        Mapreader.LoadMap(currentSaveinfo.target, ref Spiel2, new Vector2(this.screenWidth, this.screenHeight));
                                        Help.Spielfeld = Spiel2.Spielfeld;
                                        Spiel2.Width = screenWidth;
                                        Spiel2.Height = screenHeight;
                                        vordergrund = Farbwahl(Texturen.tilltexture);
                                        water = Farbwahl(Texturen.wasser);

                                        CreateForeground();
                                        createKasten();*/
                                Tausch.SpielAktiv = true;
                                // MausAktiv = false;
                                Ladenmenu.hide();
                                break;
                            }
                        case 2:
                            {
                                /*
                                        lademenuaufruf = false;*/
                                Ladenmenu.hide();
                                if (Spiel2 == null) StartMenu.show();
                                else pauseMenu.show();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }

                    #endregion Lademenu

                    if (!Tausch.SpielAktiv)
                    {
                        mouseState = Help.GetMouseState();
                        return;
                    }
                    if (mouseState.LeftButton == Help.GetMouseState().LeftButton &&
                        mouseState.RightButton == Help.GetMouseState().RightButton)
                    {
                        mouseState = Help.GetMouseState();
                        return;
                    }

                    // Etwas auf der Karte anklicken

                    if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    {
                        bool found = false;
                        int c = Spiel2.CurrentPlayer;
                        for (int i = 0; i < Spiel2.players[c].pos.Count; i++)
                        {
                            if (Spiel2.players[c].PrüfeObKollision(i,
                                new Vector2(mouseState.X + Spiel2.Fenster.X, mouseState.Y + Spiel2.Fenster.Y)))
                            {
                                //  Kurzmeldung.Hinzufügen("Panzer gewaehlt!", new Vector2(Help.GetMouseState().X + Spiel2.Fenster.X, Help.GetMouseState().Y + Spiel2.Fenster.Y), Color.Yellow);
                                Spiel2.players[c].CurrentTank = i;
                                int a = Spiel2.players[c].CurrentTank;
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                            for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
                            {
                                if (Spiel2.Haeuser.HausTyp[i] != Gebäudedaten.FABRIK) continue;
                                if (Spiel2.Haeuser.Besitzer[i] != c) continue;
                                if (Spiel2.Haeuser.IsCollision2(i,
                                    new Vector2(mouseState.X + Spiel2.Fenster.X, mouseState.Y + Spiel2.Fenster.Y)))
                                {
                                    // Fabrik gewählt
                                    //Kurzmeldung.Hinzufügen("Fabrik gewaehlt", new Vector2(Help.GetMouseState().X + Spiel2.Fenster.X, Help.GetMouseState().Y + Spiel2.Fenster.Y), Color.Yellow);
                                    //Kurzmeldung.Add("Haus gewählt: " + i.ToString(), new Vector2(500, 300), Color.Yellow);

                                    //BauMenü.Position = new Vector2(Help.GetMouseState().X + Spiel2.Fenster.X, Help.GetMouseState().Y + Spiel2.Fenster.Y);
                                    BauMenü.Hausliste = Spiel2.Haeuser;
                                    BauMenü.HausID = i;
                                    BauMenü.show();
                                    found = true;
                                    break;
                                }
                            }

                        /* if (!found)
                         {
                             if (Spiel2 != null && Hauptfenster.Tausch.SpielAktiv)
                             {
                                 if (!Spielermenu.visible)
                                 {
                                     if (BauMenü.visible)
                                     {
                                         BauMenü.hide();
                                     }
                                 }
                             }
                         }*/
                    }
                }

            if (!Editor.visible && !notizrightklicked && Help.GetMouseState().RightButton == ButtonState.Pressed)
            {
                if (Spiel2.CurrentPlayer >= 0 && Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
                {
                    // Lasse den Panzer fahren
                    Spiel2.players[Spiel2.CurrentPlayer].Zielpos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] =
                        new Vector2(mouseState.X + Spiel2.Next_Fenster.X,
                            Kartenformat.BottomOf(mouseState.X + Spiel2.Next_Fenster.X,
                                mouseState.Y + Spiel2.Next_Fenster.Y));
                }
            }

            mouseState = Help.GetMouseState();
        }

        #region graphics

        /// <summary>
        ///     The device
        /// </summary>
        public static GraphicsDevice device;

        /// <summary>
        ///     The sprite batch
        /// </summary>
        public static SpriteBatch spriteBatch;

        /// <summary>
        ///     The graphics
        /// </summary>
        private readonly GraphicsDeviceManager graphics;

        /// <summary>
        ///     The keyb state
        /// </summary>
        private KeyboardState keybState;

        #endregion graphics

        #region textures

        /// <summary>
        ///     The screen
        /// </summary>
        public static Rectangle screen;

        /// <summary>
        ///     The screen height
        /// </summary>
        public static int screenHeight;

        /// <summary>
        ///     The screen width
        /// </summary>
        public static int screenWidth;

        /// <summary>
        ///     The randomizer
        /// </summary>
        private static readonly Random randomizer = new Random();

        #endregion textures

        #region Farbwahl

        /// <summary>
        ///     Farbwahls the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <returns>Color[][].</returns>
        public static Color[,] Farbwahl(Texture2D texture)
        {
            var colors1D = new Color[texture.Width * texture.Height];
            texture.GetData(colors1D);
            var colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
                for (int y = 0; y < texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * texture.Width];

            return colors2D;
        }

        #endregion Farbwahl
    }
}