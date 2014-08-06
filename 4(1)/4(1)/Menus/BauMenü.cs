using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     Dieses Menü wird für Fabrikgebäude verwendet
    /// </summary>
    public static class BauMenü
    {
        #region Fields

        /// <summary>
        ///     Die ID des Gebäudes, dessen Menü geöffnet ist (muss gesetzt werden).
        ///     Es kann nur ein Baumenü gleichzeitig gezeichnet werden.
        /// </summary>
        public static int HausID = -1;

        /// <summary>
        ///     eine Liste von Gebäudeobjekten, welche das Menü nutzen können
        /// </summary>
        public static Haus Hausliste = null;

        /// <summary>
        ///     true = sichtbar/nutzbar, false = unsichtbar/deaktiviert
        /// </summary>
        public static bool visible = false;

        /// <summary>
        ///     zum hinterlegen der Schriftart
        /// </summary>
        private static SpriteFont Schrift;

        #endregion Fields

        #region Methods

        /// <summary>
        ///     zeichnet das Menü
        /// </summary>
        /// <param name="spriteBatch">eine Zeichenfläche</param>
        /// <param name="Fenster">die Fensterverschiebung</param>
        /// <param name="font">eine Schriftart</param>
        /// <param name="Spiel2">ein Spielobjekt</param>
        public static void Draw(SpriteBatch spriteBatch, Vector2 Fenster, SpriteFont font, Spiel Spiel2)
        {
            if (!visible) return;
            Schrift = font;
            float scale = Optimierung.Skalierung(0.25f);

            int typ = Hausliste.HausTyp[HausID];
            Vector2 Position = Hausliste.Position[HausID] +
                               new Vector2(Hausliste.Bild[HausID].Width/2,
                                   -Hausliste.Bild[HausID].Height - Texturen.LeeresFeld.Width*scale - 15);
            if (Position.Y < 10) Position.Y = 10;

            int anz = 1;

            for (int i = 0; i < Fahrzeugdaten.KANNGEBAUTWERDEN.Wert.Length; i++)
            {
                if (Fahrzeugdaten.KANNGEBAUTWERDEN.Wert[i] >= 1)
                {
                    anz++;
                }
            }

            Position.X -= ((Texturen.LeeresFeld.Width*scale + 10)*anz)/2;

            if (-1 == Hausliste.Produktion[HausID])
            {
                spriteBatch.Draw(Texturen.LeeresFeld,
                    Position + new Vector2((Texturen.LeeresFeld.Width*scale + 10)*(0), 0) - Fenster, null,
                    Color.Yellow*5f, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
            }
            else
                spriteBatch.Draw(Texturen.LeeresFeld,
                    Position + new Vector2((Texturen.LeeresFeld.Width*scale + 10)*(0), 0) - Fenster, null, Color.White,
                    0, new Vector2(0, 0), scale, SpriteEffects.None, 1);

            for (int i = 0; i < Fahrzeugdaten.KANNGEBAUTWERDEN.Wert.Length; i++)
            {
                if (Fahrzeugdaten.KANNGEBAUTWERDEN.Wert[i] >= 1)
                {
                    if (i == Hausliste.Produktion[HausID])
                    {
                        spriteBatch.Draw(Texturen.panzerbutton[i],
                            Position + new Vector2((Texturen.LeeresFeld.Width*scale + 10)*(i + 1), 0) - Fenster, null,
                            Color.Yellow*5f, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
                    }
                    else if (Spiel2.players[Spiel2.CurrentPlayer].Credits >= Fahrzeugdaten.PREIS.Wert[i] ||
                             (Hausliste.Produktion[HausID] > -1 &&
                              Spiel2.players[Spiel2.CurrentPlayer].Credits +
                              Fahrzeugdaten.PREIS.Wert[Hausliste.Produktion[HausID]] >= Fahrzeugdaten.PREIS.Wert[i]))
                    {
                        spriteBatch.Draw(Texturen.panzerbutton[i],
                            Position + new Vector2((Texturen.LeeresFeld.Width*scale + 10)*(i + 1), 0) - Fenster,
                            null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
                    }
                    else
                        spriteBatch.Draw(Texturen.panzerbutton[i],
                            Position + new Vector2((Texturen.LeeresFeld.Width*scale + 10)*(i + 1), 0) - Fenster,
                            null, Color.Red, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
                }
            }
        }

        /// <summary>
        ///     macht das Menü unsichtbar
        /// </summary>
        public static void hide()
        {
            visible = false;
        }

        /// <summary>
        ///     wird genutzt, um alle static Inhalte zu erzeugen
        /// </summary>
        /// <param name="Content">ein ContentManager (falls Texturen geladen werden müssen)</param>
        public static void LoadContent(ContentManager Content)
        {
            // leer
        }

        /// <summary>
        ///     behandelt Mausereignisse
        /// </summary>
        /// <param name="mouseState">ein aktueller Mausstatus</param>
        /// <param name="Fenster">die Position des Festers (Spielfeldverschiebung)</param>
        /// <param name="Spiel2">ein Spielobjekt</param>
        /// <returns>true = etwas wurd angeklickt, false = kein Treffer</returns>
        public static bool mouseKeys(MouseState mouseState, Vector2 Fenster, Spiel Spiel2)
        {
            if (!visible) return false;

            float scale = Optimierung.Skalierung(0.25f);

            int typ = Hausliste.HausTyp[HausID];
            Vector2 Position = Hausliste.Position[HausID] +
                               new Vector2(Hausliste.Bild[HausID].Width/2,
                                   -Hausliste.Bild[HausID].Height - Texturen.LeeresFeld.Width*scale - 15);
            if (Position.Y < 10) Position.Y = 10;

            var Data = new List<int>();
            int anz = 1;
            Data.Add(-1);

            for (int i = 0; i < Fahrzeugdaten.KANNGEBAUTWERDEN.Wert.Length; i++)
            {
                if (Fahrzeugdaten.KANNGEBAUTWERDEN.Wert[i] >= 1)
                {
                    Data.Add(i);
                    anz++;
                }
            }

            Position.X -= ((Texturen.LeeresFeld.Width*scale + 10)*anz)/2;

            // wurde etwas angeklickt???
            bool found = false;
            for (int i = 0; i < anz; i++)
            {
                Vector2 Pos = Position + new Vector2((Texturen.LeeresFeld.Width*scale + 10)*(i), 0) - Fenster;

                if (i == 0 || Spiel2.players[Spiel2.CurrentPlayer].Credits >= Fahrzeugdaten.PREIS.Wert[Data[i]] ||
                    (Hausliste.Produktion[HausID] > -1 &&
                     Spiel2.players[Spiel2.CurrentPlayer].Credits +
                     Fahrzeugdaten.PREIS.Wert[Hausliste.Produktion[HausID]] >= Fahrzeugdaten.PREIS.Wert[Data[i]]))
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (mouseState.X >= Pos.X && mouseState.Y >= Pos.Y &&
                            mouseState.X <= Pos.X + Texturen.LeeresFeld.Width*scale + 10 &&
                            mouseState.Y <= Pos.Y + Texturen.LeeresFeld.Height*scale)
                        {
                            if (Hausliste.Produktion[HausID] > -1)
                            {
                                Spiel2.players[Spiel2.CurrentPlayer].Credits +=
                                    Fahrzeugdaten.PREIS.Wert[Hausliste.Produktion[HausID]];
                            }

                            Hausliste.Produktion[HausID] = Data[i];

                            if (i != 0)
                                Spiel2.players[Spiel2.CurrentPlayer].Credits -= Fahrzeugdaten.PREIS.Wert[Data[i]];
                            found = true;
                            return true;
                        }
                    }
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
                if (!found &&
                    !Hausliste.IsCollision2(HausID,
                        new Vector2(mouseState.X + Spiel2.Fenster.X, mouseState.Y + Spiel2.Fenster.Y)))
                {
                    visible = false;
                }

            return false;
        }

        /// <summary>
        ///     macht das Menü sichtbar
        /// </summary>
        public static void show()
        {
            visible = true;
        }

        #endregion Methods
    }
}