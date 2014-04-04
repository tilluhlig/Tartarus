using System;

// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-04-2013
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
    /// Diese Klasse verwaltet Tunnelanlagen
    /// </summary>
    public class Tunnel
    {
        /// <summary>
        /// die maximale Anzahl an Lebenspunkten, die ein Tunnel haben kann
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
        /// MOD-Variable, die skalierung der Tunneltextur
        /// </summary>
        public static Var<float> TUNNEL_SCALE = new Var<float>("TUNNEL_SCALE", Optimierung.Skalierung(0.4f), ref SKALIERUNG);

        /// <summary>
        /// die Textur des Tunnels
        /// </summary>
        public Texture2D Bild;

        /// <summary>
        /// das Kollisionsobjekt des Tunnels
        /// </summary>
        public KollisionsObjekt Kollision;

        /// <summary>
        /// die aktuellen Lebenspunkte des Tunnels
        /// </summary>
        public float Lebenspunkte;

        /// <summary>
        /// die aboslute Position des Tunnels (von der Mitte des Tunnels ausgehend)
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// das Zerstörungsobjekt des Tunnels
        /// </summary>
        public ZerstörungsObjekt Zerstörung;

        /// <summary>
        /// der Konstruktur
        /// </summary>
        /// <param name="_pos">die Position des Tunnels</param>
        public Tunnel(Vector2 _pos)
        {
            Position = _pos;
            Lebenspunkte = Maxhp;
            AktualisiereTunnelSchaden(0);
            Bild = Texturen.tunnel;

            Lade();
            Lade2();
        }

        /// <summary>
        /// initialisiert die Daten für die Tunnelklasse
        /// </summary>
        public static void LadeTunnelDaten()
        {
            SKALIERUNG = TUNNEL_SCALE.Wert;
        }

        /// <summary>
        /// Wendet Schadenspunkte auf den Tunnel an
        /// </summary>
        /// <param name="recievedDamage">der Schaden, der dem Tunnel zugerechnet werden soll</param>
        /// <returns>true = Tunnel hat noch mehr als 0 Lebenspunkte, false = Tunnel ist zerstört</returns>
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
        /// Lädt das Kollisionsobjekt neu
        /// </summary>
        public void Lade()
        {
            Kollision = new KollisionsObjekt(Bild, Bild.Width, Bild.Height, SKALIERUNG, false, false, false, new Vector2(0, 0));
        }

        /// <summary>
        /// Lädt das Zerstörungsobjekt neu
        /// </summary>
        public void Lade2()
        {
            Zerstörung = new ZerstörungsObjekt(Bild.Width, Bild.Height, SKALIERUNG, false, false, false);
        }

        /// <summary>
        /// Prüft, ob der Tunnel getroffen wurde
        /// </summary>
        /// <param name="Incoming_Position">die absolute Position, die geprüft werden soll</param>
        /// <returns>true = getroffen, false = nicht getroffen</returns>
        public bool PrüfeObKollision(Vector2 Incoming_Position)
        {
            if (Kollision == null) return false;
            return Kollision.collision(Incoming_Position, Position);
        }

        /// <summary>
        /// Berechnet die Zerstörung des Tunnels 
        /// </summary>
        /// <param name="Explosion">die Position der Explosion</param>
        /// <param name="Energie">die Explosionsstärke (Radius)</param>
        /// <returns>die Anzahl getroffener Pixel</returns>
        public int PrüfeObZerstörung(Vector2 Explosion, int Energie)
        {
            if (Zerstörung == null) return 0;
            Color[] temp = new Color[Bild.Width * Bild.Height];
            Bild.GetData(temp);
            Texture2D tmp = new Texture2D(Bild.GraphicsDevice, Bild.Width, Bild.Height);
            tmp.SetData(temp);

            return Zerstörung.BerechneZerstörung(tmp, Explosion, Energie, Position);
        }

        /// <summary>
        /// Zeichnet alle Tunnelanlagen
        /// </summary>
        /// <param name="spriteBatch">eine Zeichenfläche</param>
        /// <param name="Spiel2">das Spielobjekt, welches genutzt wird</param>
        public static void ZeichneTunnel(SpriteBatch spriteBatch, Spiel Spiel2)
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

        /// <summary>
        /// Erstellt ein Tunnelobjekt aus Text
        /// </summary>
        /// <param name="Text">der Text, aus dem das Objekt erzeugt werden soll</param>
        /// <param name="Objekt">dieses Tunnelobjekt soll als Grundlage genommen werden</param>
        /// <returns>das erstellte Tunnelobjekt</returns>
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

        /// <summary>
        /// Wandelt ein Tunnelobjekt in Text um
        /// </summary>
        /// <returns>die Textdarstellung des Objekts</returns>
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

        /// <summary>
        /// Wandelt ein Tunnelobjekt in Text um (speziell für den Editor)
        /// </summary>
        /// <returns>die Textdarstellung des Objekts</returns>
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