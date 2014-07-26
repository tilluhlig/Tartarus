// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="Bunker.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     Diese Klasse verwaltet Bunker
    /// </summary>
    public class Bunker
    {
        #region Fields

        /// <summary>
        ///     MOD-Variable, Es gibt Bunker im Spiel
        /// </summary>
        public static bool BUNKER;

        /// <summary>
        ///     MOD-Variable, Bunker können Kollidieren
        /// </summary>
        public static bool BUNKER_KOLLISION;

        /// <summary>
        ///     MOD-Variable, Lebenslinie wird angezeigt
        /// </summary>
        public static bool BUNKER_LEBENSLINIE;

        /// <summary>
        ///     MOD-Variable, Bunker können zerstört werden
        /// </summary>
        public static bool BUNKER_ZERSTOERUNG;

        /// <summary>
        ///     Der Besitzer des Bunkers. Wer ist aktuell im Bunker?
        /// </summary>
        public List<int> Besitzer = new List<int>();

        /// <summary>
        ///     Das Kollisionsobjekt des Bunkers
        /// </summary>
        public List<KollisionsObjekt> Kollision = new List<KollisionsObjekt>();

        /// <summary>
        ///     Die aktuellen Lebenspunkte des Bunkers
        /// </summary>
        public List<int> Lebenspunkte = new List<int>();

        /// <summary>
        ///     Die maximalen Lebenspunkte
        /// </summary>
        public List<int> MaximaleLebenspunkte = new List<int>();

        /// <summary>
        ///     Die absolute Position des Bunkers auf dem Spielfeld
        /// </summary>
        public List<Vector2> Position = new List<Vector2>();

        /// <summary>
        ///     Das Zerstörungsobjekt des Bunkers
        /// </summary>
        public List<ZerstörungsObjekt> Zerstörung = new List<ZerstörungsObjekt>();

        #endregion Fields

        #region Privat

        /// <summary>
        ///     MOD-Variable, es gibt Bunker im Spiel
        /// </summary>
        private static Var<bool> MOD_BUNKER = new Var<bool>("BUNKER", false, ref BUNKER);

        /// <summary>
        ///     MOD-Variable, Bunker kann kollidieren
        /// </summary>
        private static Var<bool> MOD_BUNKER_KOLLISION = new Var<bool>("BUNKER_KOLLISION", false, ref BUNKER_KOLLISION);

        /// <summary>
        ///     MOD-Variable, zeigt über dem Bunker eine Lebenslinie an
        /// </summary>
        private static Var<bool> MOD_BUNKER_LEBENSLINIE = new Var<bool>("BUNKER_LEBENSLINIE", false,
            ref BUNKER_LEBENSLINIE);

        /// <summary>
        ///     MOD-Variable, Bunker kann zerstört werden
        /// </summary>
        private static Var<bool> MOD_BUNKER_ZERSTOERUNG = new Var<bool>("BUNKER_ZERSTOERUNG", false,
            ref BUNKER_ZERSTOERUNG);

        #endregion Privat

        #region Methods

        /// <summary>
        ///     Erzeugt Text aus einem Objekt, speziell für den Editor
        /// </summary>
        /// <param name="id">ID des Bunkers</param>
        /// <returns>die Textdarstellung des Bunkers</returns>
        public List<String> EditorSpeichern(int i)
        {
            var data = new List<String>();
            data.Add("[BUNKER]");
            data.Add("Position=" + Position[i]);
            data.Add("Lebenspunkte=" + Lebenspunkte[i]);
            data.Add("Besitzer=" + Besitzer[i]);
            data.Add("[/BUNKER]");
            return data;
        }

        /// <summary>
        ///     Entfernt einen Bunker aus dem Spiel
        /// </summary>
        /// <param name="id">die ID des Bunkers</param>
        public void EntferneBunker(int id)
        {
            Position.RemoveAt(id);
            Lebenspunkte.RemoveAt(id);
            Besitzer.RemoveAt(id);
            if (BUNKER_KOLLISION) Kollision.RemoveAt(id);
            if (BUNKER_ZERSTOERUNG) Zerstörung.RemoveAt(id);
        }

        /// <summary>
        ///     Entfernt den Bunker
        /// </summary>
        /// <param name="id">Die ID des Bunkers</param>
        public void Entfernen(int id)
        {
            Position.RemoveAt(id);
            Lebenspunkte.RemoveAt(id);
            Besitzer.RemoveAt(id);

            if (BUNKER_KOLLISION)
            {
                Kollision.RemoveAt(id);
            }

            if (BUNKER_ZERSTOERUNG || BUNKER_KOLLISION)
            {
                Zerstörung.RemoveAt(id);
            }
        }

        /// <summary>
        ///     Fügt einen Bunker hinzu
        /// </summary>
        /// <param name="_Position">absolute Position auf der Karte</param>
        /// <param name="Besitzer">Spieler ID des Besitzers (-1 = Neutral)</param>
        public void Hinzufügen(Vector2 _Position, int Besitzer) // fügt einen Bunker ein
        {
            MaximaleLebenspunkte.Add(Help.GetPixelAnzahl(Texturen.bunker[0]));
            Position.Add(_Position);
            Lebenspunkte.Add(MaximaleLebenspunkte[Position.Count - 1]);
            this.Besitzer.Add(Besitzer);
            UpdateBunkerSchaden(Position.Count - 1, 0);

            if (BUNKER_KOLLISION)
            {
                Kollision.Add(null);
                LadeKollisionsobjekt(Position.Count - 1);
            }

            if (BUNKER_ZERSTOERUNG || BUNKER_KOLLISION)
            {
                Zerstörung.Add(null);
                LadeZerstörungsobjekt(Position.Count - 1);
            }
        }

        /// <summary>
        ///     Läde das Kollisionsobjekt neu
        /// </summary>
        /// <param name="id">die ID des Bunkers</param>
        public void LadeKollisionsobjekt(int id) // neu
        {
            Kollision[id] = new KollisionsObjekt(Texturen.bunker[0], Texturen.bunker[0].Width, Texturen.bunker[0].Height,
                Optimierung.Skalierung(0.25f), false, false, false, new Vector2(0, 0)); // Baumdata.scale[BunkerTyp[i]]
        }

        /// <summary>
        ///     Erzeugt ein Bunkerobjekt aus Text
        /// </summary>
        /// <param name="Text">der Text aus dem das Objekt erstellt wird</param>
        /// <param name="id">ID des Bunkers, optional, -1 = neu erstellen</param>
        public void Laden(List<String> Text, int id)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "BUNKER");
            if (Text2.Count == 0) return;

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);

            int altid = id;
            if (id == -1)
            {
                Hinzufügen(Vector2.Zero, -1);
                id = Position.Count - 1;
            }

            Lebenspunkte[id] = TextLaden.LadeInt(Liste, "Lebenspunkte", Lebenspunkte[id]);
            Besitzer[id] = TextLaden.LadeInt(Liste, "Besitzer", Besitzer[id]);
            Position[id] = TextLaden.LadeVector2(Liste, "Position", Position[id]);

            LadeKollisionsobjekt(id);
            LadeZerstörungsobjekt(id);
        }

        /// <summary>
        ///     Lädt das Zerstörbarkeitsobjekt
        /// </summary>
        /// <param name="id">die ID des Bunkers</param>
        public void LadeZerstörungsobjekt(int id)
        {
            Zerstörung[id] = new ZerstörungsObjekt(Texturen.bunker[0].Width, Texturen.bunker[0].Height,
                Optimierung.Skalierung(0.25f), false, false, false);
        }

        /// <summary>
        ///     Überprüft, ob es eine Kollision mit diesem Bunker gab
        /// </summary>
        /// <param name="i">ID des Bunkers, der geprüft werden soll</param>
        /// <param name="Incoming_Position">absolute Position des Punktes, der geprüft werden soll</param>
        /// <returns>true = Es gab eine Kollision</returns>
        public bool PrüfeObKollision(int i, Vector2 Incoming_Position)
        {
            if (Kollision[i] == null) return false;
            return Kollision[i].collision(Incoming_Position, Position[i]);
        }

        /// <summary>
        ///     Prüft, ob Teile des Bunkers innerhalb der Explosion liegen
        /// </summary>
        /// <param name="id">ID des Bunkers, der geprüft werden soll</param>
        /// <param name="Explosionspunkt">Mittelpunkt der Explosion</param>
        /// <param name="Explosionsradius">Radius der Explosion</param>
        /// <returns>System.Int32.</returns>
        public int PrüfeObZerstörung(int id, Vector2 Explosionspunkt, int Explosionsradius)
        {
            if (Zerstörung[id] == null) return 0;
            var temp = new Color[Texturen.bunker[0].Width*Texturen.bunker[0].Height];
            Texturen.bunker[0].GetData(temp);
            var tmp = new Texture2D(Texturen.bunker[0].GraphicsDevice, Texturen.bunker[0].Width,
                Texturen.bunker[0].Height);
            tmp.SetData(temp);

            return Zerstörung[id].BerechneZerstörung(tmp, Explosionspunkt, Explosionsradius, Position[id]);
        }

        /// <summary>
        ///     Erzeugt Text aus allen Bunker Objekten
        /// </summary>
        /// <returns>die Textdarstellung des Bunkers</returns>
        public List<String> Speichern()
        {
            var data = new List<String>();
            for (int i = 0; i < Besitzer.Count; i++)
            {
                data.Add("[BUNKER]");
                data.Add("Position=" + Position[i]);
                data.Add("Lebenspunkte=" + Lebenspunkte[i]);
                data.Add("Besitzer=" + Besitzer[i]);
                data.AddRange(Kollision[i].Speichern());
                data.AddRange(Zerstörung[i].Speichern());
                data.Add("[/BUNKER]");
            }
            return data;
        }

        /// <summary>
        ///     Reduziert die Lebenspunkte des Bunkers
        /// </summary>
        /// <param name="id">die ID des Bunkers</param>
        /// <param name="EingehenderSchaden">Wieviele Lebenspunkte sollen subtrahiert werden?</param>
        /// <returns>true = Bunker hat noch mehr als 0 Lebenspunkte</returns>
        public bool UpdateBunkerSchaden(int id, int EingehenderSchaden)
        {
            Lebenspunkte[id] -= EingehenderSchaden;
            if (Lebenspunkte[id] <= 0)
            {
                return false;
            }

            return true;
        }

        #endregion Methods
    }
}