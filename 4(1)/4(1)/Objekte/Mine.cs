// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-24-2013
// ***********************************************************************
// <copyright file="Mine.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse verwaltet Minen
    /// </summary>
    public class Mine
    {
        #region Fields

        /// <summary>
        ///     die möglichen Texturen der Minen
        /// </summary>
        public static Texture2D[] Bild = new Texture2D[5];

        /// <summary>
        ///     ein Kollisionsobjekt, wird von allen Mineninstanzen genutzt
        /// </summary>
        public static KollisionsObjekt Kollision;

        /// <summary>
        ///     ein Zerstörungsobjekt, wird von allen Mineninstanzen genutzt
        /// </summary>
        public static ZerstörungsObjekt Zerstörung;

        /// <summary>
        ///     nicht aktive Minen, werden in der Berechnung nicht betrachtet
        /// </summary>
        public bool Aktiv = true;

        /// <summary>
        ///     der Energiewert der Mine, je nach Art
        /// </summary>
        public int Energie = 100;

        /// <summary>
        ///     die ID der Mine
        /// </summary>
        public int ID = 0;

        /// <summary>
        ///     die Position der Mine
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        ///     der Anzeigeradius, der nach dem Setzen der Mine sichtbar ist
        /// </summary>
        public int RadiusAnzeige = 60 * 10;

        /// <summary>
        ///     die Skalierung der Textur
        /// </summary>
        public float Skalierung = 1.0f;

        /// <summary>
        ///     die Art/Sorte/Typ der Mine
        /// </summary>
        public int Typ = 0;

        /// <summary>
        ///     die Explosionsverzögerung
        /// </summary>
        public int Verzoegerung = 0;

        /// <summary>
        ///     die Waffenart ID
        /// </summary>
        public int Waffenart = 11;

        #endregion Fields

        #region Privat

        #region DEBUG

#if DEBUG

        /// <summary>
        ///     die Skalierung der Textur
        /// </summary>
        private static float sc = 0.05f;

#else

    /// <summary>
    /// die Skalierung der Textur
    /// </summary>
        private static float sc = 1f;

#endif

        #endregion DEBUG

        /// <summary>
        ///     damit die Mine blinkt, wird ständig die Textur gewechselt, dafür dient dieser Zähler
        /// </summary>
        private int mode;

        #endregion Privat

        #region Constructors

        /// <summary>
        ///     der Minen Konstruktor
        /// </summary>
        /// <param name="_x">die X-Position</param>
        /// <param name="_y">die Y-Position</param>
        /// <param name="_Typ">die Sorte</param>
        /// <param name="_Waffenart">die Waffenart ID</param>
        /// <param name="_ID">die ID der Mine</param>
        public Mine(int _x, int _y, int _Typ, int _Waffenart, int _ID)
        {
            Waffenart = _Waffenart;
            Typ = _Typ;
            Position = new Vector2(_x, _y);
            Skalierung = sc;
            ID = _ID;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Initialisiert die Daten der Minenklasssen
        /// </summary>
        /// <param name="Content">der ContentManager</param>
        public static void Initialisierung(ContentManager Content)
        {
            for (int i = 0; i < Bild.Count(); i++) Bild[i] = Content.Load<Texture2D>("Textures\\Mine" + i);
            Kollision = new KollisionsObjekt(Bild[0], Bild[0].Width, Bild[0].Height, sc, false, false, true,
                new Vector2(0, 0));
            Zerstörung = new ZerstörungsObjekt(Bild[0].Width, Bild[0].Height, sc, false, false, false);
        }

        /// <summary>
        ///     Erstellt ein Minenobjekt aus Text
        /// </summary>
        /// <param name="Text">der Text, welcher das Objekt darstellt</param>
        /// <param name="Objekt">dieses Objekt wird als Grundlage genutzt (ansonsten null)</param>
        /// <param name="_ID">die ID des Objekts, welches erzeugt werden soll</param>
        /// <returns>ein Minenobjekt</returns>
        public static Mine Laden(List<String> Text, Mine Objekt, int _ID)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "MINE");

            Mine temp = Objekt;
            if (temp == null) temp = new Mine(0, 0, 0, 0, _ID);

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.ID = _ID;
            temp.Position = TextLaden.LadeVector2(Liste, "Position", temp.Position);
            temp.Energie = TextLaden.LadeInt(Liste, "Energie", temp.Energie);
            temp.RadiusAnzeige = TextLaden.LadeInt(Liste, "RadiusAnzeige", temp.RadiusAnzeige);
            temp.Skalierung = TextLaden.LadeFloat(Liste, "Skalierung", temp.Skalierung);
            temp.Aktiv = TextLaden.LadeBool(Liste, "Aktiv", temp.Aktiv);
            temp.Typ = TextLaden.LadeInt(Liste, "Typ", temp.Typ);
            temp.Verzoegerung = TextLaden.LadeInt(Liste, "Verzoegerung", temp.Verzoegerung);
            temp.Waffenart = TextLaden.LadeInt(Liste, "Waffenart", temp.Waffenart);
            return temp;
        }

        /// <summary>
        ///     wandelt ein Objekt in Text um (speziell für Editor)
        /// </summary>
        /// <returns>der Text, welcher das Objekt darstellt</returns>
        public List<String> EditorSpeichern()
        {
            var data = new List<String>();
            data.Add("[MINE]");
            data.Add("Position=" + Position);
            // data.Add("Art=" + Art);
            data.Add("Energie=" + Energie);
            data.Add("RadiusAnzeige=" + RadiusAnzeige);
            data.Add("Skalierung=" + Skalierung);
            data.Add("Aktiv=" + Aktiv);
            data.Add("Typ=" + Typ);
            data.Add("Verzoegerung=" + Verzoegerung);
            data.Add("Waffenart=" + Waffenart);
            data.Add("[/MINE]");
            return data;
        }

        /// <summary>
        ///     ermittelt das Bild, welches verwendet werden soll (fürs Blinken)
        /// </summary>
        /// <returns>git die zu verwendende Textur zurück</returns>
        public Texture2D ErmittleBild()
        {
            Texture2D res;
            if (mode % 60 < 10)
            {
                res = Bild[Typ + 1];
            }
            else
            {
                res = Bild[0];
            }
            mode++;
            return res;
        }

        /// <summary>
        ///     Prüft, ob es eine Kollision mit der Mine gab
        /// </summary>
        /// <param name="Incoming_Position">die absolute zu prüfende Position</param>
        /// <returns>true = es gab einen Treffer, false = kein Treffer</returns>
        public bool PrüfeObKollision(Vector2 Incoming_Position)
        {
            if (Kollision == null) return false;
            return Kollision.collision(Incoming_Position, Position);
        }

        /// <summary>
        ///     Wendet eine Explosion auf die Mine an
        /// </summary>
        /// <param name="Explosion">die Position der Explosion</param>
        /// <param name="Energie">der Explosionsradius</param>
        /// <returns>die Anzahl der getroffenen Pixel</returns>
        public int PrüfeObZerstörung(Vector2 Explosion, int Energie)
        {
            if (Zerstörung == null) return 0;
            var tmp = new Texture2D(Bild[0].GraphicsDevice, Bild[0].Width, Bild[0].Height);
            var temp = new Color[Bild[0].Width * Bild[0].Height];
            Bild[0].GetData(temp);
            tmp.SetData(temp);
            return Zerstörung.BerechneZerstörung(tmp, Explosion, Energie, Position);
        }

        /// <summary>
        ///     wandelt ein Objekt in Text um
        /// </summary>
        /// <returns>der Text, welcher das Objekt darstellt</returns>
        public List<String> Speichern()
        {
            var data = new List<String>();
            data.Add("[MINE]");
            data.Add("Position=" + Position);
            data.Add("Energie=" + Energie);
            data.Add("RadiusAnzeige=" + RadiusAnzeige);
            data.Add("Skalierung=" + Skalierung);
            data.Add("Aktiv=" + Aktiv);
            data.Add("Typ=" + Typ);
            data.Add("Verzoegerung=" + Verzoegerung);
            data.Add("Waffenart=" + Waffenart);
            data.Add("[/MINE]");

            return data;
        }

        /// <summary>
        ///     zündet eine Mine
        /// </summary>
        /// <param name="Spielfeld">das Spielfeld</param>
        /// <param name="gameTime">ein Zeitstempel</param>
        /// <param name="Spiel2">ein Spielobjekt</param>
        /// <returns>eine Liste mit Daten zur Neuberechnung der Kartenoberfläche</returns>
        public List<Vector3> ZündeMine(List<UInt16>[] Spielfeld, GameTime gameTime, Spiel Spiel2)
        {
            // Mine zünden
            var list = new List<Vector3>();

            int _Art = Waffenart;

            // Explosion
            Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, Position, (int)Waffendaten.Daten[_Art].Y,
                Waffendaten.Daten[_Art].Z, Waffendaten.Daten[_Art].W, gameTime,
                Waffendaten.Farben[_Art], _Art, 0);

            // Sound
            Spiel2.Karte.explode_missile(Spielfeld, Position, Spiel2.Fenster, _Art);

            // Rauchstelle
            for (int j = -(int)Waffendaten.Daten[_Art].X / 2;
                j < Waffendaten.Daten[_Art].X / 2;
                j += Waffendaten.BrandAbstand[_Art])
            {
                if (Position.X + j < 0 || Position.X + j >= Spielfeld.Length) continue;
                Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListMapSmoke,
                    new Vector2(Position.X + j, Kartenformat.BottomOf(Position)), 4,
                    Waffendaten.Daten[_Art].Z / 10, Waffendaten.Daten[_Art].W * 10, gameTime,
                    Waffendaten.Farben[_Art], _Art, 2);
            }

            var a = new Karte();
            Replay.Explosion(Position, _Art);
            list.AddRange(a.Explode(Spielfeld, (int)Position.X, (int)Position.Y, (int)(Waffendaten.Daten[_Art].X)));
            list.AddRange(Spiel2.Explosionsschäden(gameTime, Position, (int)(Waffendaten.Daten[_Art].X), _Art,
                new[] { -1, -1 }));
            return list;
        }

        #endregion Methods
    }
}