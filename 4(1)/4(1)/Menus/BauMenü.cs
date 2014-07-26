using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    public static class BauMenü
    {
        #region Fields

        public static int HausID = -1;

        public static Haus Hausliste = null;

        /// <summary>
        ///     The visible
        /// </summary>
        public static bool visible = false;

        /// <summary>
        ///     The own pos
        /// </summary>
        //  public static Vector2 Position = Vector2.Zero;
        private static SpriteFont Schrift;

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        /// <param name="Effekte">The effekte.</param>
        /// <param name="Rucksack">The rucksack.</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <param name="Transparenz">The transparenz.</param>
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
        ///     Hides this instance.
        /// </summary>
        public static void hide()
        {
            visible = false;
        }

        /// <summary>
        ///     Loads the content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void LoadContent(ContentManager Content)
        {
        }

        /// <summary>
        ///     Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="Rucksack">The rucksack.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
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
        ///     Shows this instance.
        /// </summary>
        public static void show()
        {
            visible = true;
        }

        #endregion Methods
    }
}