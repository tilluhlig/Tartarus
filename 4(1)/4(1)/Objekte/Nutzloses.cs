using System;

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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    /// Class Trash
    /// </summary>
    public static class Nutzloses
    {
        #region Privat

        /// <summary>
        /// The bild
        /// </summary>
        private static List<Texture2D> Bild = new List<Texture2D>();

        /// <summary>
        /// The collision
        /// </summary>
        private static List<KollisionsObjekt> Kollision = new List<KollisionsObjekt>();

        /// <summary>
        /// The pixel
        /// </summary>
        private static List<int> Pixel = new List<int>();

        /// <summary>
        /// The pos
        /// </summary>
        private static List<Vector2> Position = new List<Vector2>();

        /// <summary>
        /// The size
        /// </summary>
        private static List<float> Skalierung = new List<float>();

        /// <summary>
        /// The overreach
        /// </summary>
        private static List<bool> Gespiegelt = new List<bool>();

        /// <summary>
        /// The angle
        /// </summary>
        private static List<float> Winkel = new List<float>();

        /// <summary>
        /// The destruction
        /// </summary>
        private static List<ZerstörungsObjekt> Zerstörung = new List<ZerstörungsObjekt>();

        #endregion Privat

        /// <summary>
        /// Clears this instance.
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
        /// Draws the trash.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="Spiel2">The spiel2.</param>
        public static void ZeichneNutzloses(GameTime gameTime, SpriteBatch spriteBatch, Spiel Spiel2)
        {
            if (Spiel2 == null) return;

            for (int i = 0; i < Nutzloses.Position.Count; i++)
            {
                float scale = Nutzloses.Skalierung[i];
                int xPos = (int)(Nutzloses.Position[i].X - Spiel2.Fenster.X);
                int yPos = (int)(Nutzloses.Position[i].Y - Spiel2.Fenster.Y);

                if (xPos + Nutzloses.Bild[i].Width * scale / 2 < 0 || xPos - Nutzloses.Bild[i].Width * scale / 2 > Game1.screenWidth) continue;

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(Nutzloses.Bild[i], new Vector2(xPos - (Nutzloses.Bild[i].Width * scale) / 2, yPos - Nutzloses.Bild[i].Height * scale), null, Color.White, Nutzloses.Winkel[i], new Vector2(0, 0), scale, Nutzloses.Gespiegelt[i] ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1);
                spriteBatch.End();

                if (Editor.visible && Editor.mouseover == 2 && Editor.mouseoverid == i)
                {
                    spriteBatch.Begin();
                    spriteBatch.Draw(Nutzloses.Bild[i], new Vector2(xPos - (Nutzloses.Bild[i].Width * scale) / 2, yPos - Nutzloses.Bild[i].Height * scale), null, Color.Blue, Nutzloses.Winkel[i], new Vector2(0, 0), scale, Nutzloses.Gespiegelt[i] ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 1);
                    spriteBatch.End();
                }
            }
        }

        /// <summary>
        /// Deletes the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
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
        /// Gibs the anzahl.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int GibAnzahl()
        {
            return Position.Count;
        }

        /// <summary>
        /// Gibs the position.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 GibPosition(int ID)
        {
            return Position[ID];
        }

        /// <summary>
        /// Adds the specified _ bild.
        /// </summary>
        /// <param name="_Bild">The _ bild.</param>
        /// <param name="_Position">The _pos.</param>
        /// <param name="_Winkel">The _ angle.</param>
        /// <param name="_Gespiegelt">if set to <c>true</c> [_overreach].</param>
        /// <param name="_Skalierung">Size of the _.</param>
        /// <param name="_Kollision">if set to <c>true</c> [_collision].</param>
        /// <param name="_Zerstörung">if set to <c>true</c> [_destruction].</param>
        public static void Hinzufügen(Texture2D _Bild, Vector2 _Position, float _Winkel, bool _Gespiegelt, float _Skalierung, bool _Kollision, bool _Zerstörung)
        {
            Position.Add(_Position);
            Winkel.Add(_Winkel);
            Gespiegelt.Add(_Gespiegelt);
            Skalierung.Add(_Skalierung);

            Color[] temp = new Color[_Bild.Width * _Bild.Height];
            _Bild.GetData(temp);
            Texture2D tmp = new Texture2D(_Bild.GraphicsDevice, _Bild.Width, _Bild.Height);
            tmp.SetData(temp);
            tmp.Tag = _Bild.Tag;

            Bild.Add(tmp);

            if (_Kollision)
            {
                Kollision.Add(new KollisionsObjekt(_Bild, _Bild.Width, _Bild.Height, _Skalierung, true, true, true, Vector2.Zero));
            }
            Pixel.Add((int)(Help.GetPixelAnzahl(_Bild)));

            if (_Zerstörung)
            {
                Zerstörung.Add(new ZerstörungsObjekt(_Bild.Width, _Bild.Height, _Skalierung, true, true, true));
            }
        }

        /// <summary>
        /// Determines whether the specified i is collision.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <returns><c>true</c> if the specified i is collision; otherwise, <c>false</c>.</returns>
        public static bool PrüfeObKollision(int i, Vector2 Incoming_Position)
        {
            if (Kollision[i] == null) return false;
            return Kollision[i].collision(Incoming_Position, Position[i], Winkel[i], Gespiegelt[i]);
        }

        /// <summary>
        /// Determines whether the specified i is explode.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        /// <returns>System.Int32.</returns>
        public static int PrüfeObZerstörung(int i, Vector2 Explosion, int Energie)
        {
            if (Zerstörung[i] == null) return 0;
            int z = Zerstörung[i].BerechneZerstörung(Bild[i], Explosion, Energie, Position[i], Gespiegelt[i], Winkel[i]);
            Pixel[i] -= z;
            if (Pixel[i] <= 10)
            {
                Entfernen(i);
            }
            else
                if (z > 0)
                {
                    Kollision[i] = new KollisionsObjekt(Bild[i], Bild[i].Width, Bild[i].Height, Skalierung[i], true, true, true, Vector2.Zero);
                }
            return z;
        }

        // TODO ausfüllen
        public static void Laden(ContentManager Content, List<String> Text, int id)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "NUTZLOSES");
            if (Text2.Count == 0) return;

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            String Typ = TextLaden.LadeString(Liste, "Bild", (id == -1 ? "Textures\\nichts" : (String)Bild[id].Tag));

            int altid = id;
            if (id == -1)
            {
                Hinzufügen(Content.Load<Texture2D>(Typ), Vector2.Zero, 0, false, 1.0f, true, true);
                id = Position.Count - 1;
            }

            if (((String)Bild[id].Tag) == Typ && altid == id)
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

            Kollision[id] = new KollisionsObjekt(Bild[id], Bild[id].Width, Bild[id].Height, Skalierung[id], true, true, true, Vector2.Zero);
            Zerstörung[id] = new ZerstörungsObjekt(Bild[id].Width, Bild[id].Height, Skalierung[id], true, true, true);

            Kollision[id] = KollisionsObjekt.Laden(Text2, altid == -1 ? null : Kollision[id]);

            Zerstörung[id] = ZerstörungsObjekt.Laden(Text2, altid == -1 ? null : Zerstörung[id]);

            Bild[id] = Kollision[id].UseMaskOnTexture2D(Bild[id]);
            Bild[id].Tag = Typ;
            Pixel[id] = ((int)(Help.GetPixelAnzahl(Bild[id])));
        }

        public static List<String> Speichern()
        {
            List<String> data = new List<String>();
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

        public static List<String> EditorSpeichern(int id)
        {
            List<String> data = new List<String>();
            data.Add("[NUTZLOSES]");
            data.Add("Bild=" + Bild[id].Tag);
            data.Add("Position=" + Position[id]);
            data.Add("Skalierung=" + Skalierung[id]);
            data.Add("Gespiegelt=" + Gespiegelt[id]);
            data.Add("Winkel=" + Winkel[id]);
            data.Add("[/NUTZLOSES]");
            return data;
        }
    }
}