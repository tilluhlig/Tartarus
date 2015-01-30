// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="Trash.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse verwaltet Objekte, die lediglich zerstört werden können,
    ///     jedoch keine weitere Funktionalität aufweisen
    /// </summary>
    public static class Nutzloses
    {
        #region Privat

        /// <summary>
        ///     die Texturen der Objekte
        /// </summary>
        private static readonly List<Texture2D> Bild = new List<Texture2D>();

        /// <summary>
        ///     Gibt an, ob die Textur gespiegeld sein soll
        /// </summary>
        private static readonly List<bool> Gespiegelt = new List<bool>();

        /// <summary>
        ///     die Kollisionsobjekte der Objekte
        /// </summary>
        private static readonly List<KollisionsObjekt> Kollision = new List<KollisionsObjekt>();

        /// <summary>
        ///     die Anzahl der Pixel, die noch vorhanden sind
        /// </summary>
        private static readonly List<int> Pixel = new List<int>();

        /// <summary>
        ///     die Position des Objekts
        /// </summary>
        private static readonly List<Vector2> Position = new List<Vector2>();

        /// <summary>
        ///     die Skalierung der Textur
        /// </summary>
        private static readonly List<float> Skalierung = new List<float>();

        /// <summary>
        ///     die Rotationswinkel der Objekte
        /// </summary>
        private static readonly List<float> Winkel = new List<float>();

        /// <summary>
        ///     die Zerstörungsobjekte
        /// </summary>
        private static readonly List<ZerstörungsObjekt> Zerstörung = new List<ZerstörungsObjekt>();
       /// private static readonly List<Body> Koerper = new List<Body>(); 
        #endregion Privat

        #region Methods

        /// <summary>
        ///     entfernt alle Nutzlosen Objekte
        /// </summary>
        public static void AlleEntfernen()
        {
            Bild.Clear();
            Pixel.Clear();
            Position.Clear();
            Winkel.Clear();
            Gespiegelt.Clear();
            Skalierung.Clear();
            Kollision.Clear();
            Zerstörung.Clear();
        }

        /// <summary>
        ///     Wandelt ein Objekt in Text um (speziell für den Editor)
        /// </summary>
        /// <returns>der Text, welcher das Objekt darstellt</returns>
        public static List<String> EditorSpeichern(int id)
        {
            var data = new List<String>();
            data.Add("[NUTZLOSES]");
            data.Add("Bild=" + Bild[id].Tag);
            data.Add("Position=" + Position[id]);
            data.Add("Skalierung=" + Skalierung[id]);
            data.Add("Gespiegelt=" + Gespiegelt[id]);
            data.Add("Winkel=" + Winkel[id]);
            data.Add("[/NUTZLOSES]");
            return data;
        }

        /// <summary>
        ///     Entfernt ein bestimmtes Objekt
        /// </summary>
        /// <param name="i">die ID des zu entfernenden Objekts</param>
        public static void Entfernen(int i)
        {
            Bild.RemoveAt(i);
            Pixel.RemoveAt(i);
            Position.RemoveAt(i);
            Winkel.RemoveAt(i);
            Gespiegelt.RemoveAt(i);
            Skalierung.RemoveAt(i);
            Kollision.RemoveAt(i);
            Zerstörung.RemoveAt(i);
        }

        /// <summary>
        ///     Gibt die Anzahl an nutzlosen Objekten im Spiel zurück
        /// </summary>
        /// <returns>die Anzahl der nutzlosen Objekte</returns>
        public static int GibAnzahl()
        {
            return Position.Count;
        }

        /// <summary>
        ///     die Position eines bestimmten Objekts
        /// </summary>
        /// <param name="ID">die ID des Objekts, dessen Position abgefragt wird</param>
        /// <returns>die Position des entsprechenden Objekts</returns>
        public static Vector2 GibPosition(int ID)
        {
            ///return Koerper[ID].Position;
            return Position[ID];
        }

        /// <summary>
        ///     Fügt ein neues nutzloes Objekt ins Spiel ein
        /// </summary>
        /// <param name="_Bild">die Textur</param>
        /// <param name="_Position">die Position</param>
        /// <param name="_Winkel">der Rotationswinkel</param>
        /// <param name="_Gespiegelt">true = gespiegelt, false = nicht gespiegelt</param>
        /// <param name="_Skalierung">Skalierung der Textur</param>
        /// <param name="_Kollision">true = Objekt kann Kollision auslösen, false = keine Kollisionen möglich</param>
        /// <param name="_Zerstörung">true = kann Zerstört werden, false = keine Zerstörung</param>
        public static void Hinzufügen(Texture2D _Bild, Vector2 _Position, float _Winkel, bool _Gespiegelt,
            float _Skalierung, bool _Kollision, bool _Zerstörung)
        {
            Position.Add(_Position);
            Winkel.Add(_Winkel);
            Gespiegelt.Add(_Gespiegelt);
            Skalierung.Add(_Skalierung);

            var temp = new Color[_Bild.Width*_Bild.Height];
            _Bild.GetData(temp);
            var tmp = new Texture2D(_Bild.GraphicsDevice, _Bild.Width, _Bild.Height);
            tmp.SetData(temp);
            tmp.Tag = _Bild.Tag;

            Bild.Add(tmp);

            if (_Kollision)
            {
                Kollision.Add(new KollisionsObjekt(_Bild, _Bild.Width, _Bild.Height, _Skalierung, true, true, true,
                    Vector2.Zero));
            }
            Pixel.Add(Help.GetPixelAnzahl(_Bild));

            if (_Zerstörung)
            {
                Zerstörung.Add(new ZerstörungsObjekt(_Bild.Width, _Bild.Height, _Skalierung, true, true, true));
            }

           /* uint[] data = new uint[_Bild.Width * _Bild.Height];
            _Bild.GetData(data);
            Vertices verts = PolygonTools.CreatePolygon( data, _Bild.Width, true);

            List<Vertices>_list = FarseerPhysics.Common.Decomposition.BayazitDecomposer.ConvexPartition(verts);
            Body tempKoerper = BodyFactory.CreateCompoundPolygon(Game1.World, _list, 1);
            tempKoerper.BodyType = BodyType.Static;
            tempKoerper.SleepingAllowed = true;
            tempKoerper.Awake = true;
            tempKoerper.Position = _Position;
            Koerper.Add(tempKoerper);*/

          //  Koerper.Add(pol);
        }

        /// <summary>
        ///     erstellt ein nutzloses Objekt aus Text
        /// </summary>
        /// <param name="Content">ein Content Manager</param>
        /// <param name="Text">der Text, aus dem das Objekt erstellt werden soll</param>
        /// <param name="id">die ID des Objekts, id=1 neu erstellen</param>
        public static void Laden(ContentManager Content, List<String> Text, int id)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "NUTZLOSES");
            if (Text2.Count == 0) return;

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            String Typ = TextLaden.LadeString(Liste, "Bild", (id == -1 ? "Textures\\nichts" : (String) Bild[id].Tag));

            int altid = id;
            if (id == -1)
            {
                Hinzufügen(Content.Load<Texture2D>(Typ), Vector2.Zero, 0, false, 1.0f, true, true);
                id = Position.Count - 1;
            }

            if (((String) Bild[id].Tag) == Typ && altid == id)
            {
                Bild[id] = Content.Load<Texture2D>(Typ);
                Bild[id].Tag = Typ;
                Skalierung[id] = TextLaden.LadeFloat(Liste, "Skalierung", Skalierung[id]);
                Winkel[id] = TextLaden.LadeFloat(Liste, "Winkel", Winkel[id]);
                Position[id] = TextLaden.LadeVector2(Liste, "Position", Position[id]);
                Gespiegelt[id] = TextLaden.LadeBool(Liste, "Gespiegelt", Gespiegelt[id]);
            }
            else
            {
                Bild[id] = Content.Load<Texture2D>(Typ);
                Bild[id].Tag = Typ;
                Skalierung[id] = TextLaden.LadeFloat(Liste, "Skalierung", Skalierung[id]);
                Winkel[id] = TextLaden.LadeFloat(Liste, "Winkel", Winkel[id]);
                Position[id] = TextLaden.LadeVector2(Liste, "Position", Position[id]);
                Gespiegelt[id] = TextLaden.LadeBool(Liste, "Gespiegelt", Gespiegelt[id]);
            }

            Kollision[id] = new KollisionsObjekt(Bild[id], Bild[id].Width, Bild[id].Height, Skalierung[id], true, true,
                true, Vector2.Zero);
            Zerstörung[id] = new ZerstörungsObjekt(Bild[id].Width, Bild[id].Height, Skalierung[id], true, true, true);

            Kollision[id] = KollisionsObjekt.Laden(Text2, altid == -1 ? null : Kollision[id]);

            Zerstörung[id] = ZerstörungsObjekt.Laden(Text2, altid == -1 ? null : Zerstörung[id]);

            Bild[id] = Kollision[id].UseMaskOnTexture2D(Bild[id]);
            Bild[id].Tag = Typ;
            Pixel[id] = Help.GetPixelAnzahl(Bild[id]);
        }

        /// <summary>
        ///     Prüft ob das angegebene Objekt kollidiert
        /// </summary>
        /// <param name="i">die ID des zu prüfenden Objekts</param>
        /// <param name="Incoming_Position">die Position, welche auf Kollision geprüft werden soll</param>
        /// <returns>true = es gab eine Kollision, false = es gab keine Kollision</returns>
        public static bool PrüfeObKollision(int i, Vector2 Incoming_Position)
        {
            if (Kollision[i] == null) return false;
            return Kollision[i].collision(Incoming_Position, Position[i], Winkel[i], Gespiegelt[i]);
        }

        /// <summary>
        ///     wendet eine Explosion auf ein bestimmtes nutzloses Objekt an
        /// </summary>
        /// <param name="i">die ID des Objekts</param>
        /// <param name="Explosion">die Position einer Explosion</param>
        /// <param name="Energie">der Explosionsradius</param>
        /// <returns>die Anzahl der getroffenen Pixel</returns>
        public static int PrüfeObZerstörung(int i, Vector2 Explosion, int Energie)
        {
            if (Zerstörung[i] == null) return 0;
            int z = Zerstörung[i].BerechneZerstörung(Bild[i], Explosion, Energie, Position[i], Gespiegelt[i], Winkel[i]);
            Pixel[i] -= z;
            if (Pixel[i] <= 10)
            {
                Entfernen(i);
            }
            else if (z > 0)
            {
                Kollision[i] = new KollisionsObjekt(Bild[i], Bild[i].Width, Bild[i].Height, Skalierung[i], true, true,
                    true, Vector2.Zero);
            }
            return z;
        }

        /// <summary>
        ///     Wandelt ein Objekt in Text um
        /// </summary>
        /// <returns>der Text, welcher das Objekt darstellt</returns>
        public static List<String> Speichern()
        {
            var data = new List<String>();
            for (int i = 0; i < Bild.Count; i++)
            {
                data.Add("[NUTZLOSES]");
                data.Add("Bild=" + Bild[i].Tag);
                //data.Add("Pixel=" + Pixel[i]);
                data.Add("Position=" + Position[i]);
                data.Add("Skalierung=" + Skalierung[i]);
                data.Add("Gespiegelt=" + Gespiegelt[i]);
                data.Add("Winkel=" + Winkel[i]);
                data.AddRange(Kollision[i].Speichern());
                data.AddRange(Zerstörung[i].Speichern());
                data.Add("[/NUTZLOSES]");
            }

            return data;
        }

        /// <summary>
        ///     Zeichnet alle Nutzlosen Objekte
        /// </summary>
        /// <param name="gameTime">ein Zeitstempel</param>
        /// <param name="spriteBatch">eine Zeichenoberfläche</param>
        /// <param name="Spiel2">ein Spielobjekt</param>
        public static void ZeichneNutzloses(GameTime gameTime, SpriteBatch spriteBatch, Spiel Spiel2)
        {
            if (Spiel2 == null) return;

            for (int i = 0; i < Position.Count; i++)
            {
                float scale = Skalierung[i];
                var xPos = (int)(Nutzloses.GibPosition(i).X - Spiel2.Fenster.X);
                var yPos = (int)(Nutzloses.GibPosition(i).Y - Spiel2.Fenster.Y);

                if (xPos + Bild[i].Width*scale/2 < 0 || xPos - Bild[i].Width*scale/2 > Game1.screenWidth) continue;

                //Game1.SpriteBatchSemaphor.WaitOne();
                spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(Bild[i], new Vector2(xPos - (Bild[i].Width*scale)/2, yPos - Bild[i].Height*scale), null,
                    Color.White, Winkel[i], new Vector2(0, 0), scale,
                    Gespiegelt[i] ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1);
                spriteBatch.End();
              //  Game1.SpriteBatchSemaphor.Release();

                if (Editor.visible && Editor.mouseover == 2 && Editor.mouseoverid == i)
                {
                    //Game1.SpriteBatchSemaphor.WaitOne();
                    spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend);
                    spriteBatch.Draw(Bild[i], new Vector2(xPos - (Bild[i].Width*scale)/2, yPos - Bild[i].Height*scale),
                        null, Color.Blue, Winkel[i], new Vector2(0, 0), scale,
                        Gespiegelt[i] ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1);
                    spriteBatch.End();
                   // Game1.SpriteBatchSemaphor.Release();
                }
            }
        }

        #endregion Methods
    }
}