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
        #region Methods

        /// <summary>
        ///     Lädt ein Spiel aus einer Datei
        /// </summary>
        /// <param name="game">das Spielobjekt</param>
        /// <param name="Map">der Pfad zum gespeicherten Spiel</param>
        public static void Laden(Game1 game, String Map)
        {
            //Hauptfenster.Program.Formular.progressBar1.Value = 0;
           // Hauptfenster.Program.Formular.progressBar1.Show();
            //Hauptfenster.Program.Formular.progressBar1.BringToFront();

           // Hauptfenster.Program.Formular.label31.Show();
           // Hauptfenster.Program.Formular.label31.BringToFront();
            Tausch.SpielAktiv = false;

            // Reset
            Karte.Reset_Materialien();
            Fahrzeugdaten.Reset_Tankdata();

            Game1.LadeText("    Tastatur...    ");
            //if (!File.Exists(Map)) { Spielfeld = null; return; }
            // Tastatur laden
            Tastatur.LadeTastaturbelegung("Content\\Konfiguration\\Tastatur.conf");

            Game1.LadeText("    Mod...    ");
            ///Hauptfenster.Program.Formular.progressBar1.Value = 20;
            // Mod laden
            Mod.LadeModVariablen("Content\\Konfiguration\\" + Tausch.Mod);

            Game1.LadeText("    Komponenten...    ");
            ///Hauptfenster.Program.Formular.progressBar1.Value = 40;
            // Komponenten laden
            game.loadAllContent();

            if (!File.Exists(Map))
            {
                Game1.Ladebildschirmtexte = null;
                return;
            }

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

            Game1.Spiel2 = new Spiel();
            bool symmetrisch = Karte.KARTE_SYMMETRISCH;
            Spiel.Kartenbreite = Game1.Kartengroesse;
            Game1.Spiel2.increaseshot = false;
            Spiel.rand = new Random();
            Game1.Spiel2.Spielfeld = new List<UInt16>[Spiel.Kartenbreite];
            for (int i = 0; i < Spiel.Kartenbreite; i++) Game1.Spiel2.Spielfeld[i] = new List<UInt16>();
            Game1.Spiel2.Karte = new Karte();
            Help.Spielfeld = Game1.Spiel2.Spielfeld;

            Game1.Spiel2.Haeuser = new Haus();
            Game1.Spiel2 = Game1.Spiel2.Laden(Data);
            Data.Clear();
            Help.Spielfeld = Game1.Spiel2.Spielfeld;
            Game1.Spiel2.Width = Game1.screenWidth;
            Game1.Spiel2.Height = Game1.screenHeight;
            //Hauptfenster.Program.Formular.progressBar1.Value = 90;

            Game1.LadeText("    Umgebung...    ");
            Game1.water = Game1.Farbwahl(Texturen.wasser);
            Vordergrund.ErstelleVordergrund();
            Fog.CreateFog();
            Mine.Initialisierung(Game1.ContentAll);
            Eingabefenster.Initialisieren();
            Game1.createKasten();
            Feuer.Initialisieren(Game1.Spiel2.Spielfeld.Length);
            ///if (Spiel.SCHUESSE.Wert) Spiel2.Schuesse = Spiel2.players[Spiel2.CurrentPlayer].MaxSchuesse;
           // Game1.Spiel2.InitRunde();
            //Mine.init(Game1.ContentAll);
            Help.angrabbel_funktion();
            if (Mod.SPIELERMENU_VISIBLE.Wert) Game1.Spielermenu.show();
            Eingabefenster.Eingabe.Verstecken();
            if (Mod.SPIELERMENU_VISIBLE.Wert) Game1.Spielermenu.hide();
            // Sounds.Lademusik.StopSound(0);

            //Game1.Spiel2 = new Spiel();
            Game1.Ladebildschirmtexte = null;
            Tausch.SpielAktiv = true;
        }

        #endregion Methods
    }
}