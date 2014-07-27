#region Using Statements

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;

// This class exposes WinForms-style key events.

#endregion Using Statements

namespace _4_1_
{
    /// <summary>
    ///     Macht Kartenbemerkungen möglich
    ///     Bemerkung: Eine font wählen, die gleiche Buchstabenlänge besitzt, sonst
    ///     siehts u.U. nicht schön aus!
    /// </summary>
    public class Notizen
    {
        #region Fields

        /// <summary>
        ///     ein Kollisionsobjekt für alle Notizen
        /// </summary>
        public static KollisionsObjekt Kollision;

        /// <summary>
        ///     die Positionen der Notizen
        /// </summary>
        public List<Vector2> pos = new List<Vector2>();

        /// <summary>
        ///     ob der Schreibmodus für die Notiz aktiv ist
        /// </summary>
        public bool schreibend = false;

        /// <summary>
        ///     die ausgewählte Notiz
        /// </summary>
        public int selected = -1;

        /// <summary>
        ///     die Textfelder der Notizen
        /// </summary>
        public List<Textbereich> Textfelder = new List<Textbereich>();

        /// <summary>
        ///     die Textur für die Markierung auf dem Bildschirm
        /// </summary>
        private static Texture2D fahne;

        /// <summary>
        ///     die Schriftart für alle Notizen
        /// </summary>
        private static SpriteFont font;

        /// <summary>
        ///     die maximale Pixelbreite
        /// </summary>
        private int maxPixelInZeile = 300;

        #endregion Fields

        #region DEBUG

#if DEBUG

        /// <summary>
        ///     die Skalierung der Bildschirmmarkierung
        /// </summary>
        private static float scale = 0.125f;

#else
    /// <summary>
    /// die Skalierung der Bildschirmmarkierung
    /// </summary>
        private static float scale = 1f;
#endif

        #endregion DEBUG

        #region Methods

        /// <summary>
        ///     ladet den Basisbestand rein
        /// </summary>
        public static void LoadContent()
        {
            font = Texturen.font4;
            fahne = Texturen.Notizmarkierung;
            Kollision = new KollisionsObjekt(fahne, fahne.Width, fahne.Height, scale, false, false, true, Vector2.Zero);
        }

        /// <summary>
        ///     fügt eine Notiz in die Liste ein
        /// </summary>
        /// <param name="graphicsDevice">ein GraphicsDevice</param>
        /// <param name="pos">die Position der Notiz</param>
        /// <param name="Content">der Inhalt</param>
        /// <param name="Content2">ein ContentManager</param>
        public void AddNotiz(GraphicsDevice graphicsDevice, Vector2 pos, string Content, ContentManager Content2)
        {
            int place = insert(this.pos, pos.X);

            this.pos.Insert(place, pos);

            Textfelder.Insert(place, new Textbereich(font, maxPixelInZeile, 10, graphicsDevice, Content2));
            Textfelder[place].show();
            Textfelder[place].Text = Help.ZerhackeTextAufFesteBreite(font, Content, maxPixelInZeile, true);
            Textfelder[place].originalText = Content;
        }

        /// <summary>
        ///     entfernt eine Notiz
        /// </summary>
        /// <param name="id">die ID der Notiz</param>
        public void delNotiz(int id)
        {
            selected = -1;
            Textfelder.RemoveAt(id);
            pos.RemoveAt(id);
        }

        /// <summary>
        ///     Zeichnet alle Notizen auf dem Bildschirm
        /// </summary>
        /// <param name="spriteBatch">ein Tastaurevent</param>
        /// <param name="graphicsDevice">ein Tastaurevent</param>
        /// <param name="Fenster">ein Tastaurevent</param>
        /// <param name="found">ob die Maus auf dem Bildschirm bereits über etwas ist</param>
        /// <returns>ob sich die Maus über einem gefundenen Objekt auf dem Bildschirm befindet</returns>
        public bool Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster, bool found)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                if (pos[i].X - Fenster.X + fahne.Width * scale < 0 ||
                    pos[i].X - Fenster.X - fahne.Width * scale > Game1.screenWidth) continue;

                if (schreibend && i == selected)
                {
                    spriteBatch.Draw(fahne, pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale),
                        null, Color.Red, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
                else if (i != selected)
                {
                    spriteBatch.Draw(fahne, pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale),
                        null, Color.Yellow * 0.7f, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
                else
                    spriteBatch.Draw(fahne, pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale),
                        null, Color.DarkGreen, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

                if (Tausch.SpielAktiv)
                    if (!found)
                    {
                        if (Kollision.collision(Help.GetMousePos(), pos[i] - Fenster, false))
                        {
                            spriteBatch.Draw(Texturen.NotizmarkierungUmriss,
                                pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale), null,
                                Color.DarkGreen, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                            found = true;
                        }
                    }
            }

            if (selected != -1)
            {
                Color r = Color.DarkGreen;

                if (schreibend)
                {
                    r = Color.Red;
                }

                Textfelder[selected].Draw(spriteBatch, graphicsDevice, Fenster, found, pos[selected],
                    Color.LightGoldenrodYellow, r);

                // Linie malen
                if (schreibend)
                {
                    Help.DrawLine(spriteBatch, pos[selected] - Fenster,
                        pos[selected] + new Vector2(100 - 7, 0 - 7) - Fenster, Color.Red, 2);
                }
                else
                    Help.DrawLine(spriteBatch, pos[selected] - Fenster,
                        pos[selected] + new Vector2(100 - 7, 0 - 7) - Fenster, Color.DarkGreen, 2);
            }

            KleinesMenu.Draw(spriteBatch, graphicsDevice, Fenster);

            return found;
        }

        /// <summary>
        ///     Erzeugt den Inhalt einer Notiz aus Text
        /// </summary>
        /// <param name="Text">der Text in dem die Notiz definiert ist</param>
        public void Laden(List<String> Text, int i, GraphicsDevice graphicsDevice, ContentManager Content2)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "NOTIZ");

            int altid = i;
            if (i == -1)
            {
                AddNotiz(graphicsDevice, Vector2.Zero, "", Content2);
                i = pos.Count - 1;
            }

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text);
            Textfelder[i].originalText = TextLaden.LadeString(Liste, "originalText", Textfelder[i].originalText);
        }

        /// <summary>
        ///     bearbeitet das anklicken der Notizmarkierung
        /// </summary>
        /// <param name="Fenster">die Verschiebung auf dem Spielfeld (das Fenster)</param>
        /// <param name="graphicsDevice">ein Graphics Device</param>
        /// <param name="oldmouseState">der Mauszustand</param>
        /// <returns>ob sich die Maus über einem gefundenen Objekt auf dem Bildschirm befindet</returns>
        public bool MouseKeys(Vector2 Fenster, GraphicsDevice graphicsDevice, MouseState oldmouseState)
        {
            KleinesMenu.MouseKeys(graphicsDevice, this, oldmouseState);

            for (int i = 0; i < pos.Count; i++)
            {
                if (pos[i].X - Fenster.X + fahne.Width * scale < 0 || pos[i].X - Fenster.X > Game1.screenWidth)
                    continue;

                if (Kollision.collision(Help.GetMousePos(), pos[i] - Fenster, false))
                {
                    if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    {
                        selected = i;
                        schreibend = false;
                        Textfelder[selected].cursor = 0;
                        return false;
                    }
                    if (Help.GetMouseState().RightButton == ButtonState.Pressed)
                    {
                        KleinesMenu.show(Help.GetMousePos() + Fenster, i);
                        selected = -1;
                        schreibend = false;
                        return true;
                    }
                }
            }

            //  if (selected > -1 && Textfelder[selected].MouseKeys(Fenster))
            //return true;
            if (selected > -1)
            {
                //Textfelder[selected].MouseKeys(Fenster);

                if (Textfelder[selected].Scrollbar.InScroller(Fenster)) return true;
            }

            if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
            {
                selected = -1;
                schreibend = false;
            }

            return false;
        }

        /// <summary>
        ///     Überprüft das Klicken auf den Schreibbereich
        /// </summary>
        /// <param name="Fenster">die Verschiebung auf dem Spielfeld (das Fenster)</param>
        /// <param name="graphicsDevice">ein Graphics Device</param>
        /// <returns>ob auf das Schreibfeld der Notiz geklickt wurde</returns>
        public bool Notizbereich_klick(Vector2 Fenster, GraphicsDevice graphicsDevice)
        {
            if (selected <= -1)
            {
                schreibend = false;
                return false;
            }
            if (Help.GetMouseState().LeftButton != ButtonState.Pressed) return false;

            Vector2 Maße = font.MeasureString(("").PadLeft(maxPixelInZeile, 'X'));
            Maße.Y *= Textfelder[selected].Text.Count;
            //  Maße.Y += 14;
            Maße.X = maxPixelInZeile;
            Vector2 temppos = pos[selected] + new Vector2(100, 0);
            if ((temppos + Maße).Y > Game1.screenHeight)
            {
                temppos.Y = Game1.screenHeight - Maße.Y;
            }

            var notizfeld = new BoundingBox(new Vector3(temppos - Fenster, 0), new Vector3(temppos + Maße - Fenster, 0));

            if (notizfeld.Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                ContainmentType.Contains)
            {
                schreibend = true;
                Textfelder[selected].cursor = 0;
                return true;
            }

            if (Textfelder[selected].Scrollbar.InScroller(Fenster)) return false;

            schreibend = false;
            return false;
        }

        /// <summary>
        ///     bearbeitet Tastatureingaben
        /// </summary>
        /// <param name="sender">ein Auslöser</param>
        /// <param name="e">ein Tastaurevent</param>
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (selected != -1 && selected < Textfelder.Count)
            {
                Textfelder[selected].OnKeyPress(sender, e);
            }
        }

        /// <summary>
        ///     Wandelt den Effekt zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> Speichern()
        {
            var data = new List<String>();
            for (int i = 0; i < Textfelder.Count; i++)
            {
                data.Add("[NOTIZ]");
                data.Add("originalText=" + Textfelder[i].originalText);
                data.Add("[/NOTIZ]");
            }

            return data;
        }

        /// <summary>
        ///     bearbeitet weiterhin gedrückte Tasten
        /// </summary>
        /// <param name="keybState">ein Tastaturzustand</param>
        public void TastenEingabe(KeyboardState keybState)
        {
            if (selected != -1 && selected < Textfelder.Count)
            {
                Textfelder[selected].TastenEingabe(keybState);
            }
        }

        /// <summary>
        ///     ordner einer bestimmten X-Position, einem Index der Notizen zu, sodass
        ///     die Notiz eine kleinere X-Position hat.
        ///     wird genutzt, um alle Notizen nach aufsteigendem X Wert zu sortieren
        /// </summary>
        /// <param name="item">eine Liste von Positionen</param>
        /// <param name="value">eine X Position</param>
        /// <returns>der Index des Elements, hinter dem wir das neue platzieren können</returns>
        private int insert(List<Vector2> item, float value)
        {
            for (int i = 0; i < item.Count; i++)
                if (item[i].X > value)
                    return i;
            return item.Count;
        }

        #endregion Methods
    }
}