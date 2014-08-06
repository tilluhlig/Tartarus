#region Using Statements

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

// This class exposes WinForms-style key events.

#endregion Using Statements

namespace _4_1_
{
    /// <summary>
    /// diese Klasse ermöglicht das Erstellen von Textbereichen
    /// </summary>
    public class Textbereich
    {
        #region Fields

        /// <summary>
        ///     ein Scrollbarobjekt, für lange Texte
        /// </summary>
        public Scroller Scrollbar = null;

        /// <summary>
        ///     der zerlegte Originaltext (erfolgt automatisch aus dem Originaltext)
        /// </summary>
        public List<string> Text = new List<string>();

        /// <summary>
        ///     die Cursorposition im Text
        /// </summary>
        public int cursor = 0;

        /// <summary>
        ///     die verwendete Schriftart
        /// </summary>
        public SpriteFont font;

        /// <summary>
        ///     die maximale Anzahl von Pixeln pro Zeile, welche mit Text gefüllt werden
        /// </summary>
        public int maxPixelInZeile = 300;

        /// <summary>
        ///     die maximale Anzahl an Textzeilen (danach wird ein Scrollbalken angezeigt)
        /// </summary>
        public int maxZeilen = 10;

        /// <summary>
        ///     der Text, welcher im Textfeld dargestellt wird, mit Zeilenumbrüchen(\n)
        /// </summary>
        public String originalText = "";

        /// <summary>
        ///     die Position des Textbereichs
        /// </summary>
        private Vector2 pos;

        /// <summary>
        ///     Sichtbar? true = ja, false = nein
        /// </summary>
        public bool visible = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     initialisiert einen Textbereich
        /// </summary>
        /// <param name="font">eine Schriftart</param>
        /// <param name="maxPixelInZeile">die maximale Anzahl von Pixeln pro Zeile, welche mit Text gefüllt werden</param>
        /// <param name="maxZeilen">die maximale Anzahl an Textzeilen (danach wird ein Scrollbalken angezeigt)</param>
        /// <param name="graphicsDevice">ein Bildschirmobjekt</param>
        /// <param name="Content">ein ContentManager</param>
        public Textbereich(SpriteFont font, int maxPixelInZeile, int maxZeilen, GraphicsDevice graphicsDevice,
            ContentManager Content)
        {
            this.font = font;
            this.maxPixelInZeile = maxPixelInZeile;
            this.maxZeilen = maxZeilen;

            Scrollbar = new Scroller(40, (int) (font.MeasureString("a").Y*maxZeilen + 14), Vector2.Zero, maxZeilen, 1,
                true, graphicsDevice, Content, Color.LightGoldenrodYellow, Color.Black);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Zeichnet den Textbereich
        /// </summary>
        /// <param name="spriteBatch">eine Zeichenfläche</param>
        /// <param name="graphicsDevice">ein Bildschirmobjekt</param>
        /// <param name="Fenster">die Spielfeldverschiebng</param>
        /// <param name="found">ob die Maus bereits über einem Objekt ist</param>
        /// <param name="pos">die Position</param>
        /// <param name="Feldfarbe">die Farbe des Textfeldes</param>
        /// <param name="Randfarbe">dei Farbe vom Rand</param>
        /// <returns></returns>
        public bool Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster, bool found,
            Vector2 pos, Color Feldfarbe, Color Randfarbe)
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

            Help.DrawRectangle(spriteBatch, graphicsDevice,
                new Rectangle((int) (temppos.X - 7 - Fenster.X), (int) (temppos.Y - 7 - Fenster.Y), (int) (Maße.X + 14),
                    (int) (Maße.Y + 14)), Randfarbe, 1f);

            Help.DrawRectangle(spriteBatch, graphicsDevice,
                new Rectangle((int) (temppos.X - 5 - Fenster.X), (int) (temppos.Y - 5 - Fenster.Y), (int) (Maße.X + 10),
                    (int) (Maße.Y + 10)), Feldfarbe, 1f);

            // Text zeichnen

            int b = 0;
            for (int i = Scrollbar.oberstes; i < Text.Count && b < maxZeilen; i++, b++)
            {
                String draw = Text[i];
                if (draw.Length >= 1 && draw[draw.Length - 1] == '\n') draw = draw.Substring(0, draw.Length - 1);
                spriteBatch.DrawString(font, draw,
                    temppos + new Vector2(0, font.MeasureString("a").Y*(i - Scrollbar.oberstes)) - Fenster, Color.Black);
            }

            // Cursor zeichnen
            int x = 0;
            int y = 0;
            int position = 0;
            String cursortext = "";
            for (int i = 0; i < Text.Count; i++)
            {
                position += Text[i].Length;

                if (cursor <= position ||
                    (Text[i].Length > 0 && Text[i][Text[i].Length - 1] == '\n' && cursor <= position))
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
                spriteBatch.DrawString(font, "_",
                    temppos +
                    new Vector2(font.MeasureString(cursortext).X, font.MeasureString("a").Y*(y - Scrollbar.oberstes)) -
                    Fenster, Color.Red, 0, Vector2.Zero, 1.1f, SpriteEffects.None, 1);

            Scrollbar.UpdatePos(temppos + new Vector2(Maße.X + 7, -7));
            Scrollbar.updateScroller(Text.Count, graphicsDevice);
            Scrollbar.Draw(spriteBatch, Fenster, graphicsDevice);

            return found;
        }

        /// <summary>
        ///     versteckt den Textbereich
        /// </summary>
        public void hide()
        {
            visible = false;
            Scrollbar.hide();
        }

        /// <summary>
        ///     behandelt Mausaktionen
        /// </summary>
        /// <param name="Fenster">die Verschiebung auf dem Spielfeld</param>
        /// <returns></returns>
        public bool MouseKeys(Vector2 Fenster)
        {
            if (!visible) return false;
            var a = new BoundingBox(new Vector3(pos, 0),
                new Vector3(pos + new Vector2(maxPixelInZeile, maxZeilen*font.MeasureString("a").Y), 0));
            if (a.Contains(new Vector3(Help.GetMouseState().X + Fenster.X, Help.GetMouseState().Y + Fenster.Y, 0)) ==
                ContainmentType.Contains)
            {
                return true;
            }

            return Scrollbar.MouseKeys(Fenster);
        }

        /// <summary>
        ///     behandelt Tastatureingaben 
        /// </summary>
        /// <param name="sender">der Auslöser (normalerweise null)</param>
        /// <param name="e">das Tastaturevent bzw. die gedrückte Taste</param>
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!visible) return;

            if (((e.KeyChar < 32 && e.KeyChar != 8) || e.KeyChar > 126)) return;

            if (e.KeyChar == 8 && originalText.Length != 0 && cursor > 0)
            {
                originalText = originalText.Substring(0, cursor - 1) +
                               originalText.Substring(cursor, originalText.Length - cursor);
                cursor--;
            }
            else if (e.KeyChar >= 32 && e.KeyChar <= 126)
            {
                originalText = originalText.Substring(0, cursor) + e.KeyChar +
                               originalText.Substring(cursor, originalText.Length - cursor);
                cursor++;
            }

            Text = Help.ZerhackeTextAufFesteBreite(font, originalText, maxPixelInZeile, true);
        }

        /// <summary>
        ///     macht den Textbereich sichtbar
        /// </summary>
        public void show()
        {
            visible = true;
            Scrollbar.show();
        }

        /// <summary>
        ///     behandelt Tastatureingaben
        /// </summary>
        /// <param name="keybState">der Tastaturstatus</param>
        public void TastenEingabe(KeyboardState keybState)
        {
            if (!visible) return;

            bool gedrückt = true;
            if (keybState.IsKeyDown(Keys.Delete) && cursor < originalText.Length)
            {
                originalText = originalText.Substring(0, cursor) +
                               originalText.Substring(cursor + 1, originalText.Length - cursor - 1);
                Text = Help.ZerhackeTextAufFesteBreite(font, originalText, maxPixelInZeile, true);
            }
            else if (keybState.IsKeyDown(Keys.Enter))
            {
                originalText = originalText.Substring(0, cursor) + "\n" +
                               originalText.Substring(cursor, originalText.Length - cursor);
                cursor++;
                Text = Help.ZerhackeTextAufFesteBreite(font, originalText, maxPixelInZeile, true);
            }
            else if (keybState.IsKeyDown(Keys.Right) && cursor < originalText.Length)
            {
                cursor++;
            }
            else if (keybState.IsKeyDown(Keys.Left) && cursor > 0)
            {
                cursor--;
            }
            else if (keybState.IsKeyDown(Keys.Down) && cursor < originalText.Length)
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
                        else if (x >= Text[i + 1].Length)
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
            else if (keybState.IsKeyDown(Keys.Up))
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
                        else if (x >= Text[i - 1].Length)
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

                    if (cursor <= position ||
                        (Text[i].Length > 0 && Text[i][Text[i].Length - 1] == '\n' && cursor <= position))
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

                if (y < Scrollbar.oberstes)
                {
                    Scrollbar.oberstes = y;
                }
                if (Scrollbar.oberstes + Scrollbar.maxItems - 1 < y)
                {
                    Scrollbar.oberstes = y - Scrollbar.maxItems + 1;
                }
            }
        }

        #endregion Methods
    }
}