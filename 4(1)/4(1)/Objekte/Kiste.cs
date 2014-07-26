// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-24-2013
// ***********************************************************************
// <copyright file="Kiste.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse verwaltet Kisten
    /// </summary>
    public class Kiste
    {
        #region Fields

        /// <summary>
        ///     die Kisten Textur
        /// </summary>
        public static Texture2D Bild;

        /// <summary>
        ///     ein Kollisionsobjekt für die Textur
        /// </summary>
        public static KollisionsObjekt Kollision;

        /// <summary>
        ///     ein Zerstörungsobjekt für die Textur
        /// </summary>
        public static ZerstörungsObjekt Zerstörung;

        #endregion Fields

        #region DEBUG

#if DEBUG

        /// <summary>
        ///     Die Skalierung der Kistentexturen
        /// </summary>
        public static float sc = 0.5f;

#else

    /// <summary>
    ///     Die Skalierung der Kistentexturen
    /// </summary>
        public static float sc = 1f;

#endif

        #endregion DEBUG

        /// <summary>
        ///     bestimmt, ob die Kiste in die Berechnungen einbezogen werden soll (true = mit berechnet, false = inaktiv)
        /// </summary>
        public List<bool> aktiv = new List<bool>();

        /// <summary>
        ///     die IDs der Kisten
        /// </summary>
        public List<int> id = new List<int>();

        /// <summary>
        ///     die Position der Kiste
        /// </summary>
        public List<Vector2> pos = new List<Vector2>();

        /// <summary>
        ///     die Inventare der Kisten
        /// </summary>
        public List<Inventar> Rucksack = new List<Inventar>();

        /// <summary>
        ///     eine Explosionsverzögerung
        /// </summary>
        public List<int> verzögerung = new List<int>();

        /// <summary>
        ///     initalisiert die Kistenklasse
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void init(ContentManager Content)
        {
            Bild = Content.Load<Texture2D>("Textures\\Kiste");
            Kollision = new KollisionsObjekt(Bild, Bild.Width, Bild.Height, sc, false, false, true, new Vector2(0, 0));
            Zerstörung = new ZerstörungsObjekt(Bild.Width, Bild.Height, sc, false, false, false);
        }

        /// <summary>
        ///     fügt eine Kiste hinzu
        /// </summary>
        /// <param name="_x">die X-Koordinate</param>
        /// <param name="_y">die Y-Koordinate</param>
        /// <param name="_Inventar">das Inventar der Kiste</param>
        public void AddKiste(int _x, int _y, Inventar _Inventar)
        {
            Rucksack.Add(_Inventar);
            pos.Add(new Vector2(_x, _y));
            id.Add(pos.Count - 1);
            aktiv.Add(true);
            verzögerung.Add(0);
            // anz++;
        }

        /// <summary>
        ///     fügt eine Kiste hinzu
        /// </summary>
        /// <param name="_pos">die Position der Kiste</param>
        /// <param name="_Inventar">das Inventar der Kiste</param>
        public void AddKiste(Vector2 _pos, Inventar _Inventar)
        {
            Rucksack.Add(_Inventar);
            pos.Add(_pos);
            id.Add(pos.Count - 1);
            aktiv.Add(true);
            verzögerung.Add(0);
            //anz++;
        }

        /// <summary>
        ///     entfernt eine Kiste
        /// </summary>
        /// <param name="id2">die id der Kiste</param>
        public void DeleteKiste(int id2)
        {
            Rucksack.RemoveAt(id2);
            pos.RemoveAt(id2);
            id.RemoveAt(id2);
            aktiv.RemoveAt(id2);
            verzögerung.RemoveAt(id2);
        }

        /// <summary>
        ///     wandelt eine Kiste in Text um (speziell für Editor)
        /// </summary>
        /// <param name="i">die ID der Kiste</param>
        /// <returns>die Textdarstellung des Kistenobjekts</returns>
        public List<String> EditorSpeichern(int i)
        {
            var data = new List<String>();
            data.Add("[KISTE]");
            data.Add("aktiv=" + aktiv[i]);
            data.Add("id=" + id[i]);
            data.Add("pos=" + pos[i]);
            data.Add("verzögerung=" + verzögerung[i]);
            data.Add("[/KISTE]");

            return data;
        }

        /// <summary>
        ///     lässt eine Kiste explodieren
        /// </summary>
        /// <param name="id">die ID der Kiste</param>
        /// <param name="Spielfeld">ein Spielfeld</param>
        /// <param name="gameTime">ein Zeitstempel</param>
        /// <param name="Spiel2">ein Spielobjekt</param>
        /// <returns>gibt Daten zur Neuberechnung der Kartenoberfläche zurück</returns>
        public List<Vector3> Explosion(int id, List<UInt16>[] Spielfeld, GameTime gameTime, Spiel Spiel2)
        {
            // Mine zünden
            var list = new List<Vector3>();

            int _Art = 1;

            // Explosion
            Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, pos[id], (int)Waffendaten.Daten[_Art].Y,
                Waffendaten.Daten[_Art].Z, Waffendaten.Daten[_Art].W, gameTime,
                Waffendaten.Farben[_Art], _Art, 0);

            // Sound
            Spiel2.Karte.explode_missile(Spielfeld, pos[id], Spiel2.Fenster, _Art);

            // Rauchstelle
            for (int j = -(int)Waffendaten.Daten[_Art].X / 2;
                j < Waffendaten.Daten[_Art].X / 2;
                j += Waffendaten.BrandAbstand[_Art])
            {
                if (pos[id].X + j < 0 || pos[id].X + j >= Spielfeld.Length) continue;
                Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListMapSmoke,
                    new Vector2(pos[id].X + j, Kartenformat.BottomOf(pos[id])), 4,
                    Waffendaten.Daten[_Art].Z / 10, Waffendaten.Daten[_Art].W * 10, gameTime,
                    Waffendaten.Farben[_Art], _Art, 2);
            }

            var a = new Karte();
            Replay.Explosion(pos[id], _Art);
            list.AddRange(a.Explode(Spielfeld, (int)pos[id].X, (int)pos[id].Y, (int)(Waffendaten.Daten[_Art].X)));
            list.AddRange(Spiel2.Explosionsschäden(gameTime, pos[id], (int)(Waffendaten.Daten[_Art].X), _Art,
                new[] { -1, -1 }));
            return list;
        }

        /// <summary>
        ///     Prüft, ob eine bestimmte Kiste mit einer Position kollidiert
        /// </summary>
        /// <param name="id">die ID der Kiste</param>
        /// <param name="Incoming_Position">die Position, die auf Kollision geprüft werden soll</param>
        /// <returns>true = es gab eine Kollision, false = keine Kollision</returns>
        public bool IsCollision(int id, Vector2 Incoming_Position)
        {
            if (Kollision == null) return false;
            return Kollision.collision(Incoming_Position, pos[id]);
        }

        /// <summary>
        ///     berechnet die Explosionsschäden an einer Kiste
        /// </summary>
        /// <param name="id">die ID der Kiste</param>
        /// <param name="Explosion">die Position der Explosion</param>
        /// <param name="Energie">der Explosionsradius / die Stärke der Explosion</param>
        public int IsExplode(int id, Vector2 Explosion, int Energie)
        {
            if (Zerstörung == null) return 0;
            var tmp = new Texture2D(Bild.GraphicsDevice, Bild.Width, Bild.Height);
            var temp = new Color[Bild.Width * Bild.Height];
            Bild.GetData(temp);
            tmp.SetData(temp);
            return Zerstörung.BerechneZerstörung(tmp, Explosion, Energie, pos[id]);
        }

        /// <summary>
        ///     erstellt ein Kistenobjekt aus Text
        /// </summary>
        /// <param name="Text">der Text, aus dem das Objekt erzeugt werden soll</param>
        /// <param name="i">
        ///     die ID der Kiste/param>
        ///     <param name="Content">ein Content Manager</param>
        public void Laden(List<String> Text, int i, ContentManager Content)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "KISTE");

            int altid = i;
            if (i == -1)
            {
                AddKiste(Vector2.Zero, null);
                i = pos.Count - 1;
            }

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text);
            aktiv[i] = TextLaden.LadeBool(Liste, "aktiv", aktiv[i]);
            id[i] = TextLaden.LadeInt(Liste, "id", id[i]);
            pos[i] = TextLaden.LadeVector2(Liste, "pos", pos[i]);
            verzögerung[i] = TextLaden.LadeInt(Liste, "verzögerung", verzögerung[i]);

            //  if (Rucksack[i] == null)
            Rucksack[i] = Inventar.Laden(Text2, Content, altid == -1 ? new Inventar() : Rucksack[i]);
        }

        /// <summary>
        ///     wandelt alle Kisten in Text um
        /// </summary>
        /// <returns>die Textdarstellung der Kistenobjekte</returns>
        public List<String> Speichern()
        {
            var data = new List<String>();
            for (int i = 0; i < pos.Count; i++)
            {
                data.Add("[KISTE]");
                data.Add("aktiv=" + aktiv[i]);
                data.Add("id=" + id[i]);
                data.Add("pos=" + pos[i]);
                data.Add("verzögerung=" + verzögerung[i]);
                data.AddRange(Rucksack[i].Speichern());
                data.Add("[/KISTE]");
            }

            return data;
        }
    }
}