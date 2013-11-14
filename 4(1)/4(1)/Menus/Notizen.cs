using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

#region Using Statements

using System.Windows.Forms; // This class exposes WinForms-style key events.

#endregion Using Statements

namespace _4_1_
{
    /// <summary>
    /// Macht Kartenbemerkungen möglich
    /// Bemerkung: Eine font wählen, die gleiche Buchstabenlänge besitzt, sonst
    ///            siehts u.U. nicht schön aus!
    /// </summary>
    public class Notizen
    {
        private static Texture2D fahne;
        private static SpriteFont font;
        public static KollisionsObjekt Kollision;
        private int maxPixelInZeile = 300;
        public List<Vector2> pos = new List<Vector2>();
        public int selected = -1;
        public List<Textbereich> Textfelder = new List<Textbereich>();

        //private int cursor = 0;
        //private List<List<string>> Text = new List<List<string>>();
        //private List<String> originalText = new List<String>();

        #region DEBUG

#if DEBUG
        private static float scale = 0.125f;

#else
        private static float scale = 1f;
#endif

        #endregion DEBUG

        // TODO ausfüllen
        /// <summary>
        /// Erzeugt den Inhalt des Effektes aus einem String
        /// </summary>
        /// <param name="Text">der Text in dem der Effekt definiert ist</param>
        public void LadeAusText(String Text)
        {
        }

        /// <summary>
        /// Wandelt den Effekt zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> SpeicherIntText()
        {
            List<String> data = new List<String>();
            data.Add("[NOTIZEN]");
            data.Add("maxPixelInZeile=" + maxPixelInZeile);
            data.Add("selected=" + selected);
            // data.Add("cursor=" + cursor);
            for (int i = 0; i < Textfelder.Count; i++)
            {
                data.Add("[NOTIZ]");
                data.Add("originalText=" + Textfelder[i].originalText);
                data.Add("[/NOTIZ]");
            }
            data.Add("[/NOTIZEN]");

            return data;
        }

        public Notizen(GraphicsDevice graphicsDevice)
        {
            // hier steht nichts
        }

        /// <summary>
        /// ladet den Basisbestand rein
        /// </summary>
        public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager Content, int maxY)
        {
            font = Texturen.font4;
            fahne = Texturen.Notizmarkierung;
            Kollision = new KollisionsObjekt(fahne, fahne.Width, fahne.Height, scale, false, false, true, Vector2.Zero);
        }

        /// <summary>
        /// fügt eine Notiz in die Liste ein
        /// </summary>
        public void AddNotiz(GraphicsDevice graphicsDevice, Vector2 pos, string Content, ContentManager Content2)
        {
            int place = insert(this.pos, pos.X);

            this.pos.Insert(place, pos);

            Textfelder.Insert(place, new Textbereich(font, maxPixelInZeile, 10, graphicsDevice, Content2));
            Textfelder[place].show();
            Textfelder[place].Text = Help.ZerhackeTextAufFesteBreite(font, Content, maxPixelInZeile, true);
            Textfelder[place].originalText = Content;
        }

        public void delNotiz(int id)
        {
            selected = -1;
            // Text.RemoveAt(id);
            Textfelder.RemoveAt(id);
            pos.RemoveAt(id);
        }

        public void TastenEingabe(KeyboardState keybState)
        {
            if (selected != -1 && selected < Textfelder.Count)
            {
                Textfelder[selected].TastenEingabe(keybState);
            }
        }

        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (selected != -1 && selected < Textfelder.Count)
            {
                Textfelder[selected].OnKeyPress(sender, e);
            }
        }

        public bool Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster, bool found)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                if (pos[i].X - Fenster.X + fahne.Width * scale < 0 || pos[i].X - Fenster.X - fahne.Width * scale > Game1.screenWidth) continue;

                if (schreibend && i == selected)
                {
                    spriteBatch.Draw(fahne, pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale), null, Color.Red, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                }
                else
                    if (i != selected)
                    {
                        spriteBatch.Draw(fahne, pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale), null, Color.Yellow * 0.7f, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                    }
                    else
                        spriteBatch.Draw(fahne, pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale), null, Color.DarkGreen, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

                if (Hauptfenster.Tausch.SpielAktiv)
                    if (!found)
                    {
                        if (Kollision.collision(Help.GetMousePos(), pos[i] - Fenster, false))
                        {
                            spriteBatch.Draw(Texturen.NotizmarkierungUmriss, pos[i] - Fenster - new Vector2(fahne.Width * scale / 2, fahne.Height * scale), null, Color.DarkGreen, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
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

                Textfelder[selected].Draw(spriteBatch, graphicsDevice, Fenster, found, pos[selected], Color.LightGoldenrodYellow, r);

                // Linie malen
                if (schreibend)
                {
                    Help.DrawLine(spriteBatch, pos[selected] - Fenster, pos[selected] + new Vector2(100 - 7, 0 - 7) - Fenster, Color.Red, 2);
                }
                else
                    Help.DrawLine(spriteBatch, pos[selected] - Fenster, pos[selected] + new Vector2(100 - 7, 0 - 7) - Fenster, Color.DarkGreen, 2);
            }

            KleinesMenu.Draw(spriteBatch, graphicsDevice, Fenster);

            return found;
        }

        public bool schreibend = false;

        public bool Notizbereich_klick(Vector2 Fenster, GraphicsDevice graphicsDevice)
        {
            if (selected <= -1) { schreibend = false; return false; }
            if (Help.GetMouseState().LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed) return false;

            Vector2 Maße = font.MeasureString(("").PadLeft(maxPixelInZeile, 'X'));
            Maße.Y *= Textfelder[selected].Text.Count;
            //  Maße.Y += 14;
            Maße.X = maxPixelInZeile;
            Vector2 temppos = pos[selected] + new Vector2(100, 0);
            if ((temppos + Maße).Y > Game1.screenHeight)
            {
                temppos.Y = Game1.screenHeight - Maße.Y;
            }

            BoundingBox notizfeld = new BoundingBox(new Vector3(temppos - Fenster, 0), new Vector3(temppos + Maße - Fenster, 0));

            if (notizfeld.Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) == ContainmentType.Contains)
            {
                schreibend = true;
                Textfelder[selected].cursor = 0;
                return true;
            }

            if (Textfelder[selected].Scrollbar.InScroller(Fenster)) return false;

            schreibend = false;
            return false;
        }

        public bool MouseKeys(Vector2 Fenster, GraphicsDevice graphicsDevice, MouseState oldmouseState)
        {
            KleinesMenu.MouseKeys(graphicsDevice, this, oldmouseState);

            for (int i = 0; i < pos.Count; i++)
            {
                if (pos[i].X - Fenster.X + fahne.Width * scale < 0 || pos[i].X - Fenster.X > Game1.screenWidth)
                    continue;

                if (Kollision.collision(Help.GetMousePos(), pos[i] - Fenster, false))
                {
                    if (Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        selected = i; schreibend = false;
                        Textfelder[selected].cursor = 0; return false;
                    }
                    else
                        if (Help.GetMouseState().RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
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

            if (Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                selected = -1;
                schreibend = false;
            }

            return false;
        }

        private int insert(List<Vector2> item, float value)
        {
            for (int i = 0; i < item.Count; i++)
                if (item[i].X > value)
                    return i;
            return item.Count;
        }
    }
}