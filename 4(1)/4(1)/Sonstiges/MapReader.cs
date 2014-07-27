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
using Hauptfenster;

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse dient dem Laden von gespeicherten Spielen
    /// </summary>
    public class MapReader
    {
        #region Fields

        /// <summary>
        ///     The convert
        /// </summary>
        public static bool Convert = false;

        /// <summary>
        ///     The list
        /// </summary>
        public static List<String> list = new List<String>();

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Loads the map.
        /// </summary>
        /// <param name="game">The game.</param>
        /// <param name="Map">The map.</param>
        /// <param name="_Data">The _ data.</param>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="screen">The screen.</param>
        public static void Laden(Game1 game, String Map)
        {
            Hauptfenster.Program.Formular.progressBar1.Value = 0;
            Hauptfenster.Program.Formular.progressBar1.Show();
            Hauptfenster.Program.Formular.progressBar1.BringToFront();

            Hauptfenster.Program.Formular.label31.Show();
            Hauptfenster.Program.Formular.label31.BringToFront();
            Tausch.SpielAktiv = false;

            // Reset
            // Karte.Reset_Materialien();
            // Fahrzeugdaten.Reset_Tankdata();

            Game1.LadeText("    Tastatur...    ");
            //if (!File.Exists(Map)) { Spielfeld = null; return; }
            // Tastatur laden
            Tastatur.LadeTastaturbelegung("Content\\Konfiguration\\Tastatur.conf");

            Game1.LadeText("    Mod...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 20;
            // Mod laden
            //Mod.LadeModVariablen("Content\\Konfiguration\\" + Hauptfenster.Tausch.Mod);

            Game1.LadeText("    Komponenten...    ");
            Hauptfenster.Program.Formular.progressBar1.Value = 40;
            // Komponenten laden
            //game.loadAllContent();

            var datei = new StreamReader(Map);
            var Data = new List<String>();
            for (; !datei.EndOfStream; ) Data.Add(datei.ReadLine());
            datei.Close();

            // Game1.Spiel2 = null;
            Game1.Ladebildschirmtexte = null;

            // Reset
            // Karte.Reset_Materialien();
            // Fahrzeugdaten.Reset_Tankdata();

            Game1.LadeText("    Spiel erstellen...    ");
            // Spiel erstellen
            //Game1.Spiel2 = new Spiel(10 * 2048, new Vector2(Game1.screenWidth, Game1.screenHeight));
            Nutzloses.AlleEntfernen();
            Game1.Spiel2.Haeuser = new Haus();

            Game1.Spiel2 = Game1.Spiel2.Laden(Data);
            Help.Spielfeld = Game1.Spiel2.Spielfeld;
            Game1.Spiel2.Width = Game1.screenWidth;
            Game1.Spiel2.Height = Game1.screenHeight;
            //Hauptfenster.Program.Formular.progressBar1.Value = 90;

            Game1.LadeText("    Umgebung...    ");
            //// vordergrund = Farbwahl(Texturen.tilltexture);
            //water = Game1.Farbwahl(Texturen.wasser);
            Vordergrund.ErstelleVordergrund();
            /*Fog.CreateFog();
           Mine.Initialisierung(Game1.Content);
           Eingabefenster.Initialisieren();
           Game1.createKasten();
           Feuer.Initialisieren(Game1.Spiel2.Spielfeld.Count());
           if (Spiel.SCHUESSE.Wert) Game1.Spiel2.Schuesse = Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].MaxSchuesse;
           Game1.Spiel2.InitRunde();
           //Mine.init(Content);
           Help.angrabbel_funktion();
           if (Mod.SPIELERMENU_VISIBLE.Wert) Game1.Spielermenu.show();
           Eingabefenster.Eingabe.Anzeigen();
           Matrix globalTransformation = Matrix.CreateScale(0);
           Game1.spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend, null, null, null, null, globalTransformation);
           if (Mod.SPIELERMENU_VISIBLE.Wert) Game1.Spielermenu.Draw(Game1.spriteBatch, Texturen.font3, null, null, null, 0.5f);
           Game1.spriteBatch.Draw(Texturen.mausZeiger, new Vector2(Game1.mouseState.X, Game1.mouseState.Y), null, Color.Orange, 0,
             new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
           Eingabefenster.ZeichneEingabefenster(Game1.spriteBatch);
           Game1.spriteBatch.End();*/
            Eingabefenster.Eingabe.Verstecken();
            if (Mod.SPIELERMENU_VISIBLE.Wert) Game1.Spielermenu.hide();
            Hauptfenster.Program.Formular.progressBar1.Value = 100;
            Hauptfenster.Program.Formular.progressBar1.Hide();
            Hauptfenster.Program.Formular.label31.Hide();

            // Sounds.Lademusik.StopSound(0);

            //Game1.Spiel2 = new Spiel();
            Game1.Ladebildschirmtexte = null;
            Tausch.SpielAktiv = true;
        }

        /// <summary>
        ///     Finds the begin.
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
        ///     FLOATs the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Single.</returns>
        private float FLOAT(String data)
        {
            return (float)(System.Convert.ToDouble(data));
        }

        /// <summary>
        ///     INTs the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.Int32.</returns>
        private int INT(String data)
        {
            return System.Convert.ToInt32(data);
        }

        #endregion Methods
    }
}