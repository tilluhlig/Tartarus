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
    /// Class Mine
    /// </summary>
    public class Mine
    {
        /// <summary>
        /// The anz
        /// </summary>
        //public static int Anzahl = 0;

        /// <summary>
        /// The bild
        /// </summary>
        public static Texture2D[] Bild = new Texture2D[5];

        /// <summary>
        /// The collision
        /// </summary>
        public static KollisionsObjekt Kollision;

        /// <summary>
        /// The destruction
        /// </summary>
        public static ZerstörungsObjekt Zerstörung;

        /// <summary>
        /// The aktiv
        /// </summary>
        public bool Aktiv = true;

        /// <summary>
        /// The art
        /// </summary>
        //public int Art = 5;

        /// <summary>
        /// The energie
        /// </summary>
        public int Energie = 100;

        /// <summary>
        /// The id
        /// </summary>
        public int ID = 0;

        /// <summary>
        /// The pos
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        /// The radius anzeige
        /// </summary>
        public int RadiusAnzeige = 60 * 10;

        /// <summary>
        /// The scale
        /// </summary>
        public float Skalierung = 1.0f;

        /// <summary>
        /// The typ
        /// </summary>
        public int Typ = 0;

        /// <summary>
        /// The verzögerung
        /// </summary>
        public int Verzoegerung = 0;

        /// <summary>
        /// The waffenart
        /// </summary>
        public int Waffenart = 11;

        #region Privat

        #region DEBUG

#if DEBUG
        /// <summary>
        /// The sc
        /// </summary>
        private static float sc = 0.05f;
#else

        /// <summary>
        /// The sc
        /// </summary>
        private static float sc = 1f;

#endif

        #endregion DEBUG

        /// <summary>
        /// The mode
        /// </summary>
        private int mode = 0;

        #endregion Privat

        /// <summary>
        /// Initializes a new instance of the <see cref="Mine"/> class.
        /// </summary>
        /// <param name="_x">The _x.</param>
        /// <param name="_y">The _y.</param>
        /// <param name="_Typ">The _ typ.</param>
        /// <param name="_Waffenart">The _ waffenart.</param>
        public Mine(int _x, int _y, int _Typ, int _Waffenart, int _ID)
        {
            Waffenart = _Waffenart;
            Typ = _Typ;
            Position = new Vector2(_x, _y);
            Skalierung = sc;
            ID = _ID;
            // Anzahl++;
        }

        /// <summary>
        /// Inits the specified content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void Initialisierung(ContentManager Content)
        {
            for (int i = 0; i < Bild.Count(); i++) Bild[i] = Content.Load<Texture2D>("Textures\\Mine" + i.ToString());
            Kollision = new KollisionsObjekt(Bild[0], Bild[0].Width, Bild[0].Height, sc, false, false, true, new Vector2(0, 0));
            Zerstörung = new ZerstörungsObjekt(Bild[0].Width, Bild[0].Height, sc, false, false, false);
        }

        /// <summary>
        /// Gets the bild.
        /// </summary>
        /// <returns>Texture2D.</returns>
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
        /// Determines whether the specified incoming_ position is collision.
        /// </summary>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <returns><c>true</c> if the specified incoming_ position is collision; otherwise, <c>false</c>.</returns>
        public bool PrüfeObKollision(Vector2 Incoming_Position)
        {
            if (Kollision == null) return false;
            return Kollision.collision(Incoming_Position, Position);
        }

        /// <summary>
        /// Determines whether the specified explosion is explode.
        /// </summary>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        public int PrüfeObZerstörung(Vector2 Explosion, int Energie)
        {
            if (Zerstörung == null) return 0;
            Texture2D tmp = new Texture2D(Bild[0].GraphicsDevice, Bild[0].Width, Bild[0].Height);
            Color[] temp = new Color[Bild[0].Width * Bild[0].Height];
            Bild[0].GetData(temp);
            tmp.SetData(temp);
            return Zerstörung.BerechneZerstörung(tmp, Explosion, Energie, Position);
        }

        /// <summary>
        /// Explosions the specified spielfeld.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <returns>List{Vector3}.</returns>
        public List<Vector3> ZündeMine(List<UInt16>[] Spielfeld, GameTime gameTime, Spiel Spiel2)
        {
            // Mine zünden
            List<Vector3> list = new List<Vector3>();

            int _Art = Waffenart;

            // Explosion
            Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, Position, (int)Waffendaten.Daten[_Art].Y,
                           Waffendaten.Daten[_Art].Z, Waffendaten.Daten[_Art].W, gameTime,
                           Waffendaten.Farben[_Art], _Art, 0);

            // Sound
            Spiel2.Karte.explode_missile(Spielfeld, Position, Spiel2.Fenster, _Art);

            // Rauchstelle
            for (int j = -(int)Waffendaten.Daten[_Art].X / 2; j < Waffendaten.Daten[_Art].X / 2; j += Waffendaten.BrandAbstand[_Art])
            {
                if (Position.X + j < 0 || Position.X + j >= Spielfeld.Length) continue;
                Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListMapSmoke, new Vector2(Position.X + j, Kartenformat.BottomOf(Position)), 4,
               Waffendaten.Daten[_Art].Z / 10, Waffendaten.Daten[_Art].W * 10, gameTime,
                Waffendaten.Farben[_Art], _Art, 2);
            }

            Karte a = new Karte();
            Replay.Explosion(Position, _Art);
            list.AddRange(a.Explode(Spielfeld, (int)Position.X, (int)Position.Y, (int)(Waffendaten.Daten[_Art].X)));
            list.AddRange(Spiel2.Explosionsschäden(gameTime, Position, (int)(Waffendaten.Daten[_Art].X), _Art, new int[] { -1, -1 }));
            return list;
        }

        public static Mine Laden(List<String> Text, Mine Objekt, int _ID)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "MINE");

            Mine temp = Objekt;
            if (temp == null) temp = new Mine(0, 0, 0, 0, _ID);

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.ID = _ID;
            temp.Position = TextLaden.LadeVector2(Liste, "Position", temp.Position);
            // temp.Art = TextLaden.LadeInt(Liste, "Art", temp.Art);
            temp.Energie = TextLaden.LadeInt(Liste, "Energie", temp.Energie);

            temp.RadiusAnzeige = TextLaden.LadeInt(Liste, "RadiusAnzeige", temp.RadiusAnzeige);
            temp.Skalierung = TextLaden.LadeFloat(Liste, "Skalierung", temp.Skalierung);
            temp.Aktiv = TextLaden.LadeBool(Liste, "Aktiv", temp.Aktiv);
            temp.Typ = TextLaden.LadeInt(Liste, "Typ", temp.Typ);
            temp.Verzoegerung = TextLaden.LadeInt(Liste, "Verzoegerung", temp.Verzoegerung);
            temp.Waffenart = TextLaden.LadeInt(Liste, "Waffenart", temp.Waffenart);
            return temp;
        }

        public List<String> Speichern()
        {
            List<String> data = new List<String>();
            data.Add("[MINE]");
            data.Add("Position=" + Position);
            //  data.Add("Art=" + Art);
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

        public List<String> EditorSpeichern()
        {
            List<String> data = new List<String>();
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
    }
}