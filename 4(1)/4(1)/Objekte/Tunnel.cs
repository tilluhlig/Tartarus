using System;

// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-24-2013
// ***********************************************************************
// <copyright file="Tunnel.cs">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    /// Class Tunnel
    /// </summary>
    public class Tunnel
    {
        /// <summary>
        /// The maxhp
        /// </summary>
        public static float Maxhp = 5000;

        #region DEBUG

#if DEBUG

        /// <summary>
        /// Die Skalierung der Tunneltextur
        /// </summary>
        public static float SKALIERUNG = 0.4f;

#else

        /// <summary>
        /// Die Skalierung der Tunneltextur
        /// </summary>
        public static float SKALIERUNG = 1f;

#endif

        #endregion DEBUG

        /// <summary>
        /// The TUNNE l_ SCALE
        /// </summary>
        public static Var<float> TUNNEL_SCALE = new Var<float>("TUNNEL_SCALE", Optimierung.Skalierung(0.4f), ref SKALIERUNG);

        /// <summary>
        /// The bild
        /// </summary>
        public Texture2D Bild;

        /// <summary>
        /// The bild2
        /// </summary>
        //  public Texture2D Bild2;

        /// <summary>
        /// The collision
        /// </summary>
        public KollisionsObjekt Kollision;

        /// <summary>
        /// The collision2
        /// </summary>
        //  public KollisionsObjekt Kollision2;

        /// <summary>
        /// The hp
        /// </summary>
        public float Lebenspunkte;

        /// <summary>
        /// The pos
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The destruction
        /// </summary>
        public ZerstörungsObjekt Zerstörung;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tunnel"/> class.
        /// </summary>
        /// <param name="_pos">The _pos.</param>
        public Tunnel(Vector2 _pos)
        {
            Position = _pos;
            Lebenspunkte = Maxhp;
            AktualisiereTunnelSchaden(0);
            //Color[] temp = new Color[Texturen.tunnel.Width * Texturen.tunnel.Height];
            /*Texturen.tunnel.GetData(temp);
            Bild = new Texture2D(Game1.device, Texturen.tunnel.Width, Texturen.tunnel.Height);
            Bild.SetData(temp);*/

            /*Color[] temp2 = new Color[Texturen.tunnel.Width * Texturen.tunnel.Height];
            Texturen.tunnel.GetData(temp2);
            Bild2 = new Texture2D(Game1.device, Texturen.tunnel.Width, Texturen.tunnel.Height);
            Bild2.SetData(temp2);*/
            Bild = Texturen.tunnel;

            Lade();
            Lade2();
        }

        /// <summary>
        /// Lades the tunnel data.
        /// </summary>
        public static void LadeTunnelDaten()
        {
            SKALIERUNG = TUNNEL_SCALE.Wert;
        }

        /// <summary>
        /// Updates the tunnel schaden.
        /// </summary>
        /// <param name="recievedDamage">The recieved damage.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool AktualisiereTunnelSchaden(int recievedDamage)
        {
            Lebenspunkte -= recievedDamage;
            if (Lebenspunkte <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Lade() // neu
        {
            Kollision = new KollisionsObjekt(Bild, Bild.Width, Bild.Height, SKALIERUNG, false, false, false, new Vector2(0, 0));
            // Kollision2 = new KollisionsObjekt(Bild, Bild.Width, Bild.Height, SKALIERUNG, false, false, false, new Vector2(0, 0));
        }

        /// <summary>
        /// Load2s this instance.
        /// </summary>
        public void Lade2()
        {
            Zerstörung = new ZerstörungsObjekt(Bild.Width, Bild.Height, SKALIERUNG, false, false, false);
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
        /// Determines whether the specified incoming_ position is collision2.
        /// </summary>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <returns><c>true</c> if the specified incoming_ position is collision2; otherwise, <c>false</c>.</returns>
        /* public bool PrüfeObKollision2(Vector2 Incoming_Position)
         {
             if (Kollision2 == null) return false;
             return Kollision2.collision(Incoming_Position, Position);
         }*/

        /// <summary>
        /// Determines whether the specified explosion is explode.
        /// </summary>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        public int PrüfeObZerstörung(Vector2 Explosion, int Energie)
        {
            if (Zerstörung == null) return 0;
            Color[] temp = new Color[Bild.Width * Bild.Height];
            Bild.GetData(temp);
            Texture2D tmp = new Texture2D(Bild.GraphicsDevice, Bild.Width, Bild.Height);
            tmp.SetData(temp);

            return Zerstörung.BerechneZerstörung(tmp, Explosion, Energie, Position);
        }

        public static void ZeichneTunnel(SpriteBatch spriteBatch, Spiel Spiel2) // Zeichnet die Tunnel
        {
            if (Spiel2 == null) return;

            for (int i = 0; i < Spiel2.players.Count(); i++)
                for (int b = 0; b < Spiel2.players[i].TunnelAnlage.Count; b++)
                {
                    List<Tunnel> tunnel = Spiel2.players[i].TunnelAnlage;

                    if (tunnel[b].Position.X + Texturen.tunnel.Width * Tunnel.SKALIERUNG < Spiel2.Fenster.X || Spiel2.Fenster.X + Game1.screenWidth + 10 < tunnel[b].Position.X) continue;
                    int xPos = (int)(tunnel[b].Position.X - (int)Spiel2.Fenster.X);
                    int yPos = (int)(tunnel[b].Position.Y - Spiel2.Fenster.Y); // - (baum[i].Width / 2)

                    // Tunnel malen
                    if (Editor.visible && Editor.mouseover == 1 && Editor.mouseoverid == i && Editor.mouseoverid2 == b)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(tunnel[b].Bild, new Vector2(xPos, yPos), null, Color.Blue, 0, new Vector2(0, Texturen.tunnel.Height), Tunnel.SKALIERUNG, SpriteEffects.None, 1);
                        spriteBatch.End();
                    }
                    else
                    {
                        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                        Texturen.effect.CurrentTechnique.Passes[0].Apply();
                        spriteBatch.Draw(tunnel[b].Bild, new Vector2(xPos, yPos), null, Color.White, 0, new Vector2(0, Texturen.tunnel.Height), Tunnel.SKALIERUNG, SpriteEffects.None, 1);
                        spriteBatch.End();
                    }

                    spriteBatch.Begin();
                    // Fahnen malen
                    float scale2 = Optimierung.Skalierung(0.15f);
                    spriteBatch.Draw(Texturen.fahne, new Vector2(xPos + Texturen.tunnel.Width / 2 * Tunnel.SKALIERUNG, yPos - Texturen.fahne.Height * scale2 - Texturen.tunnel.Height * Tunnel.SKALIERUNG), null, Spiel2.players[i].Farbe, 0, new Vector2(0, 0), scale2, SpriteEffects.None, 1); // haus.Width / 2

                    bool treffer = false;
                    if (Spiel2.players[Spiel2.CurrentPlayer].CurrentTank >= 0)
                        if (tunnel[b].PrüfeObKollision(Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + new Vector2(0, -2)))
                        {
                            treffer = true;
                        }

                    if (treffer)
                    {
                        spriteBatch.Draw(Texturen.tunnelumriss, new Vector2(xPos, yPos), null, Color.White, 0, new Vector2(0, Texturen.tunnel.Height), Tunnel.SKALIERUNG, SpriteEffects.None, 1);
                    }

                    // Lebenslinie Zeichnen
                    if (Haus.HAEUSER_LEBENSLINIE)
                    {
                        float breite = Texturen.tunnel.Width * Tunnel.SKALIERUNG;

                        Rectangle leben = new Rectangle(0, 0, (int)(breite * 1), Texturen.leben.Height / 4);
                        spriteBatch.Draw(Texturen.leben, new Vector2(xPos, tunnel[b].Position.Y - Texturen.tunnel.Height * Tunnel.SKALIERUNG - 10), leben,
                               Color.DarkRed * 0.5f);

                        leben = new Rectangle(0, 0, (int)(breite * ((float)tunnel[b].Lebenspunkte / Tunnel.Maxhp)), Texturen.leben.Height / 4);
                        spriteBatch.Draw(Texturen.leben, new Vector2(xPos, tunnel[b].Position.Y - Texturen.tunnel.Height * Tunnel.SKALIERUNG - 10), leben,
                               Color.Lime * 0.5f);
                    }
                    spriteBatch.End();
                }
        }

        public static Tunnel Laden(List<String> Text, Tunnel Objekt)
        {
            Tunnel temp = Objekt;
            if (temp == null) temp = new Tunnel(Vector2.Zero);
            temp.Bild = Texturen.tunnel;

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "TUNNEL");

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.Lebenspunkte = TextLaden.LadeFloat(Liste, "Lebenspunkte", temp.Lebenspunkte);
            temp.Position = TextLaden.LadeVector2(Liste, "Position", temp.Position);

            if (temp.Kollision == null || temp.Zerstörung == null)
            {
                temp.Kollision = new KollisionsObjekt(temp.Bild, temp.Bild.Width, temp.Bild.Height, SKALIERUNG, false, false, false, new Vector2(0, 0));
                temp.Zerstörung = new ZerstörungsObjekt(temp.Bild.Width, temp.Bild.Height, SKALIERUNG, false, false, false);

                temp.Kollision = KollisionsObjekt.Laden(Text, temp.Kollision);
                temp.Zerstörung = ZerstörungsObjekt.Laden(Text, temp.Zerstörung);
            }

            return temp;
        }

        public List<String> Speichern()
        {
            List<String> data = new List<String>();
            data.Add("[TUNNEL]");
            data.Add("Lebenspunkte=" + Lebenspunkte);
            data.Add("Maxhp=" + Maxhp);
            data.Add("Position=" + Position);
            data.AddRange(Kollision.Speichern());
            data.AddRange(Zerstörung.Speichern());
            data.Add("[/TUNNEL]");

            return data;
        }

        public List<String> EditorSpeichern()
        {
            List<String> data = new List<String>();
            data.Add("[TUNNEL]");
            data.Add("Lebenspunkte=" + Lebenspunkte);
            data.Add("[/TUNNEL]");
            return data;
        }
    }
}