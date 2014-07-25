using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

#region Using Statements

using System.Windows.Forms; // This class exposes WinForms-style key events.

#endregion Using Statements

namespace _4_1_
{
    /// <summary>
    ///
    /// </summary>
    public class Textbereich
    {
        /// <summary>
        /// The cursor
        /// </summary>
        public int cursor = 0;

        /// <summary>
        /// The font
        /// </summary>
        public SpriteFont font;

        /// <summary>
        /// The maximum pixel information zeile
        /// </summary>
        public int maxPixelInZeile = 300;

        /// <summary>
        /// The maximum zeilen
        /// </summary>
        public int maxZeilen = 10;

        /// <summary>
        /// The original text
        /// </summary>
        public String originalText = "";

        /// <summary>
        /// The scrollbar
        /// </summary>
        public Scroller Scrollbar = null;

        /// <summary>
        /// The text
        /// </summary>
        public List<string> Text = new List<string>();

        /// <summary>
        /// The visible
        /// </summary>
        public bool visible = false;

        /// <summary>
        /// The position
        /// </summary>
        private Vector2 pos;

        /// <summary>
        /// Initializes a new instance of the <see cref="Textbereich"/> class.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="maxPixelInZeile">The maximum pixel information zeile.</param>
        /// <param name="maxZeilen">The maximum zeilen.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="Content">The content.</param>
        public Textbereich(SpriteFont font, int maxPixelInZeile, int maxZeilen, GraphicsDevice graphicsDevice, ContentManager Content)
        {
            this.font = font;
            this.maxPixelInZeile = maxPixelInZeile;
            this.maxZeilen = maxZeilen;

            Scrollbar = new Scroller(40, (int)(font.MeasureString("a").Y * maxZeilen + 14), Vector2.Zero, maxZeilen, 1, true, graphicsDevice, Content, Color.LightGoldenrodYellow, Color.Black);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="Fenster">The fenster.</param>
        /// <param name="found">if set to <c>true</c> [found].</param>
        /// <param name="pos">The position.</param>
        /// <param name="Feldfarbe">The feldfarbe.</param>
        /// <param name="Randfarbe">The randfarbe.</param>
        /// <returns></returns>
        public bool Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster, bool found, Vector2 pos, Color Feldfarbe, Color Randfarbe)
        {
            if (!visible) return false;

            this.pos = pos;
            Vector2 Maße = font.MeasureString(("").PadLeft(maxPixelInZeile, 'X'));
            int faktor = Text.Count;
            if (faktor > maxZeilen) faktor = maxZeilen;

            Maße.Y *= faktor;
            Maße.X = maxPixelInZeile;

            Vector2 temppos = pos + new Vector2(100, 0);
            if ((temppos + Maße).Y > Game1.screenHeight)
            {
                temppos.Y = Game1.screenHeight - Maße.Y;
            }

            Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(temppos.X - 7 - Fenster.X), (int)(temppos.Y - 7 - Fenster.Y), (int)(Maße.X + 14), (int)(Maße.Y + 14)), Randfarbe, 1f);

            Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(temppos.X - 5 - Fenster.X), (int)(temppos.Y - 5 - Fenster.Y), (int)(Maße.X + 10), (int)(Maße.Y + 10)), Feldfarbe, 1f);

            // Text zeichnen

            int b = 0;
            for (int i = Scrollbar.oberstes; i < Text.Count && b < maxZeilen; i++, b++)
            {
                String draw = Text[i];
                if (draw.Length >= 1 && draw[draw.Length - 1] == '\n') draw = draw.Substring(0, draw.Length - 1);
                spriteBatch.DrawString(font, draw, temppos + new Vector2(0, font.MeasureString("a").Y * (i - Scrollbar.oberstes)) - Fenster, Color.Black);
            }

            // Cursor zeichnen
            int x = 0;
            int y = 0;
            int position = 0;
            String cursortext = "";
            for (int i = 0; i < Text.Count; i++)
            {
                position += Text[i].Length;

                if (cursor <= position || (Text[i].Length > 0 && Text[i][Text[i].Length - 1] == '\n' && cursor <= position))
                {
                    if (Text[i].Length > 0 && cursor == position && Text[i][Text[i].Length - 1] == '\n') //
                    {
                        y = i + 1;
                        x = 0;
                        cursortext = Text[i].Substring(0, x);
                    }
                    else
                    {
                        y = i;
                        x = Text[i].Length - (position - cursor);
                        cursortext = Text[i].Substring(0, x);
                    }
                    break;
                }
            }

            if (y - Scrollbar.oberstes >= 0 && y - Scrollbar.oberstes < maxZeilen)
                spriteBatch.DrawString(font, "_", temppos + new Vector2(font.MeasureString(cursortext).X, font.MeasureString("a").Y * (y - Scrollbar.oberstes)) - Fenster, Color.Red, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 1);

            Scrollbar.UpdatePos(temppos + new Vector2(Maße.X + 7, -7));
            Scrollbar.updateScroller(Text.Count, graphicsDevice);
            Scrollbar.Draw(spriteBatch, Fenster, graphicsDevice);

            return found;
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
            Scrollbar.hide();
        }

        /// <summary>
        /// Mouses the keys.
        /// </summary>
        /// <param name="Fenster">The fenster.</param>
        /// <returns></returns>
        public bool MouseKeys(Vector2 Fenster)
        {
            if (!visible) return false;
            BoundingBox a = new BoundingBox(new Vector3(pos, 0), new Vector3(pos + new Vector2(maxPixelInZeile, maxZeilen * font.MeasureString("a").Y), 0));
            if (a.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) == ContainmentType.Contains)
            {
                return true;
            }

            return Scrollbar.MouseKeys(Fenster);
        }

        /// <summary>
        /// Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!visible) return;

            if (((e.KeyChar < 32 && e.KeyChar != 8) || e.KeyChar > 126)) return;

            if (e.KeyChar == 8 && originalText.Length != 0 && cursor > 0)
            {
                originalText = originalText.Substring(0, cursor - 1) + originalText.Substring(cursor, originalText.Length - cursor);
                cursor--;
            }
            else
                if (e.KeyChar >= 32 && e.KeyChar <= 126)
                {
                    originalText = originalText.Substring(0, cursor) + e.KeyChar.ToString() + originalText.Substring(cursor, originalText.Length - cursor);
                    cursor++;
                }

            Text = Help.ZerhackeTextAufFesteBreite(font, originalText, maxPixelInZeile, true);
        }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
            Scrollbar.show();
        }

        /// <summary>
        /// Tastens the eingabe.
        /// </summary>
        /// <param name="keybState">State of the keyb.</param>
        public void TastenEingabe(KeyboardState keybState)
        {
            if (!visible) return;

            bool gedrückt = true;
            if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Delete) && cursor < originalText.Length)
            {
                originalText = originalText.Substring(0, cursor) + originalText.Substring(cursor + 1, originalText.Length - cursor - 1);
                Text = Help.ZerhackeTextAufFesteBreite(font, originalText, maxPixelInZeile, true);
            }
            else
                if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                {
                    originalText = originalText.Substring(0, cursor) + "\n" + originalText.Substring(cursor, originalText.Length - cursor);
                    cursor++;
                    Text = Help.ZerhackeTextAufFesteBreite(font, originalText, maxPixelInZeile, true);
                }
                else
                    if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) && cursor < originalText.Length)
                    {
                        cursor++;
                    }
                    else
                        if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left) && cursor > 0)
                        {
                            cursor--;
                        }
                        else
                            if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) && cursor < originalText.Length)
                            {
                                int x = 0;
                                int position = 0;
                                for (int i = 0; i < Text.Count; i++)
                                {
                                    position += Text[i].Length;
                                    if (cursor < position)
                                    {
                                        x = Text[i].Length - (position - cursor);
                                        if (i == Text.Count - 1)
                                        {
                                        }
                                        else
                                            if (x >= Text[i + 1].Length)
                                            {
                                                cursor = position + Text[i + 1].Length - 1;
                                            }
                                            else
                                            {
                                                cursor = position + x;
                                            }

                                        break;
                                    }
                                }
                            }
                            else
                                if (keybState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up))
                                {
                                    int x = 0;
                                    int position = 0;
                                    for (int i = 0; i < Text.Count; i++)
                                    {
                                        position += Text[i].Length;
                                        if (cursor < position || (i == Text.Count - 1 && cursor == position))
                                        {
                                            x = Text[i].Length - (position - cursor);
                                            if (i == 0)
                                            {
                                            }
                                            else
                                                if (x >= Text[i - 1].Length)
                                                {
                                                    cursor = position - Text[i].Length - 1;
                                                }
                                                else
                                                {
                                                    cursor = position + x - Text[i].Length - Text[i - 1].Length;
                                                }

                                            break;
                                        }
                                    }
                                }
                                else
                                    gedrückt = false;

            if (gedrückt)
            {
                // Cursor berechnen
                int y = 0;
                int position = 0;
                for (int i = 0; i < Text.Count; i++)
                {
                    position += Text[i].Length;

                    if (cursor <= position || (Text[i].Length > 0 && Text[i][Text[i].Length - 1] == '\n' && cursor <= position))
                    {
                        if (Text[i].Length > 0 && cursor == position && Text[i][Text[i].Length - 1] == '\n') //
                        {
                            y = i + 1;
                        }
                        else
                        {
                            y = i;
                        }
                        break;
                    }
                }

                if (y < Scrollbar.oberstes) { Scrollbar.oberstes = y; }
                if (Scrollbar.oberstes + Scrollbar.maxItems - 1 < y) { Scrollbar.oberstes = y - Scrollbar.maxItems + 1; }
            }
        }
    }
}