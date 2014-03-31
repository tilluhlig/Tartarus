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
    ///     Class Kiste
    /// </summary>
    public class Kiste
    {
        /// <summary>
        ///     The anz
        /// </summary>
        // public static int anz = 0;

        /// <summary>
        ///     The bild
        /// </summary>
        public static Texture2D Bild;

        /// <summary>
        ///     The collision
        /// </summary>
        public static KollisionsObjekt Kollision;

        /// <summary>
        ///     The destruction
        /// </summary>
        public static ZerstörungsObjekt Zerstörung;

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
        ///     The rucksack
        /// </summary>
        public List<Inventar> Rucksack = new List<Inventar>();

        /// <summary>
        ///     The aktiv
        /// </summary>
        public List<bool> aktiv = new List<bool>();

        /// <summary>
        ///     The id
        /// </summary>
        public List<int> id = new List<int>();

        /// <summary>
        ///     The pos
        /// </summary>
        public List<Vector2> pos = new List<Vector2>();

        /// <summary>
        ///     The verzögerung
        /// </summary>
        public List<int> verzögerung = new List<int>();

        /// <summary>
        ///     Inits the specified content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void init(ContentManager Content)
        {
            Bild = Content.Load<Texture2D>("Textures\\Kiste");
            Kollision = new KollisionsObjekt(Bild, Bild.Width, Bild.Height, sc, false, false, true, new Vector2(0, 0));
            Zerstörung = new ZerstörungsObjekt(Bild.Width, Bild.Height, sc, false, false, false);
        }

        /// <summary>
        ///     Adds the kiste.
        /// </summary>
        /// <param name="_x">The _x.</param>
        /// <param name="_y">The _y.</param>
        /// <param name="_Inventar">The _ inventar.</param>
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
        ///     Adds the kiste.
        /// </summary>
        /// <param name="_pos">The _pos.</param>
        /// <param name="_Inventar">The _ inventar.</param>
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
        ///     Deletes the kiste.
        /// </summary>
        /// <param name="id2">The id2.</param>
        public void DeleteKiste(int id2)
        {
            Rucksack.RemoveAt(id2);
            pos.RemoveAt(id2);
            id.RemoveAt(id2);
            aktiv.RemoveAt(id2);
            verzögerung.RemoveAt(id2);
        }

        /// <summary>
        ///     Explosions the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <returns>List{Vector3}.</returns>
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
            for (int j = -(int)Waffendaten.Daten[_Art].X / 2; j < Waffendaten.Daten[_Art].X / 2; j += Waffendaten.BrandAbstand[_Art])
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
            list.AddRange(Spiel2.Explosionsschäden(gameTime, pos[id], (int)(Waffendaten.Daten[_Art].X), _Art, new[] { -1, -1 }));
            return list;
        }

        /// <summary>
        ///     Determines whether the specified id is collision.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <returns><c>true</c> if the specified id is collision; otherwise, <c>false</c>.</returns>
        public bool IsCollision(int id, Vector2 Incoming_Position)
        {
            if (Kollision == null) return false;
            return Kollision.collision(Incoming_Position, pos[id]);
        }

        /// <summary>
        ///     Determines whether the specified id is explode.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        public int IsExplode(int id, Vector2 Explosion, int Energie)
        {
            if (Zerstörung == null) return 0;
            var tmp = new Texture2D(Bild.GraphicsDevice, Bild.Width, Bild.Height);
            var temp = new Color[Bild.Width * Bild.Height];
            Bild.GetData(temp);
            tmp.SetData(temp);
            return Zerstörung.BerechneZerstörung(tmp, Explosion, Energie, pos[id]);
        }

        // TODO ausfüllen
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

        public List<String> Speichern()
        {
            List<String> data = new List<String>();
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

        public List<String> EditorSpeichern(int i)
        {
            List<String> data = new List<String>();
            data.Add("[KISTE]");
            data.Add("aktiv=" + aktiv[i]);
            data.Add("id=" + id[i]);
            data.Add("pos=" + pos[i]);
            data.Add("verzögerung=" + verzögerung[i]);
            data.Add("[/KISTE]");

            return data;
        }
    }
}