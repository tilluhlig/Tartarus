// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-17-2013
// ***********************************************************************
// <copyright file="LadenSpeichern.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#region Using Statements

using System.Windows.Forms; // This class exposes WinForms-style key events.

#endregion Using Statements

namespace _4_1_
{
    /// <summary>
    /// Class Lademenu
    /// </summary>
    public class Lademenu
    {
        #region vars

        /// <summary>
        /// The font
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// The selected
        /// </summary>
        private Color selected;

        /// <summary>
        /// The unselected
        /// </summary>
        private Color unselected;           //Farbe der aktuell nicht ausgewählten Einträge

        //Farbe des ausgewählten Eintrages

        //Fontdatei des Menüs

        #endregion vars

        /// <summary>
        /// The visible
        /// </summary>
        public bool visible = false;

        /// <summary>
        /// The active string
        /// </summary>
        private int activeString;

        /// <summary>
        /// The data
        /// </summary>
        private List<string> data = new List<string>();

        /// <summary>
        /// The menu items
        /// </summary>
        private Button[] menuItems = new Button[3];

        /// <summary>
        /// The parent directory
        /// </summary>
        private System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo("Content\\Savegames");

        /// <summary>
        /// The screen height
        /// </summary>
        private int screenHeight;

        /// <summary>
        /// The screen width
        /// </summary>
        private int screenWidth;

        /// <summary>
        /// The stringbox
        /// </summary>
        private List<BoundingBox> stringbox = new List<BoundingBox>();

        /// <summary>
        /// The stringspos
        /// </summary>
        private List<Vector2> stringspos = new List<Vector2>();

        /// <summary>
        /// The textbox
        /// </summary>
        private Textfeld textbox;

        //Constructor des Menüs
        //Initialisiert Standardwerte

        /// <summary>
        /// Initializes a new instance of the <see cref="Lademenu"/> class.
        /// </summary>
        /// <param name="unselectedColor">Color of the unselected.</param>
        /// <param name="selectedColor">Color of the selected.</param>
        /// <param name="font">The font.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        /// <param name="scale">The scale.</param>
        public Lademenu(Color unselectedColor, Color selectedColor, SpriteFont font, int w, int h, float scale)
        {
            screenWidth = w;
            screenHeight = h;
            this.font = font;
            unselected = unselectedColor;
            selected = selectedColor;

            menuItems[0] = new Button(Texturen.Button1, new Vector2(100, 100), "Speichern", font);
            menuItems[1] = new Button(Texturen.Button1, new Vector2(100, 180), "Laden", font);
            menuItems[2] = new Button(Texturen.Button1, new Vector2(100, 260), "Zuruck", font);

            textbox = new Textfeld(new Vector2(50, 500), "Klicke um neue Datei zu erstellen");
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;
            //Durchläuft alle Menüpunkte und zeichnet den Menüpunkt mit den entsprechenden Eigenschaften
            spriteBatch.Draw(Texturen.pregamemenu, new Vector2(screenWidth / 2 - Texturen.pregamemenu.Width / 2, screenHeight / 2 - Texturen.pregamemenu.Height / 2),
                Color.White);
            for (int i = 0; i < menuItems.Length; i++)
                menuItems[i].Draw(spriteBatch, selected, unselected);
            for (int i = 0; i < data.Count; i++)
                if (i == activeString)
                    spriteBatch.DrawString(font, data[i], stringspos[i], Color.White);
                else
                    spriteBatch.DrawString(font, data[i], stringspos[i], Color.Black);
            textbox.ZeichneTextfeld(spriteBatch);
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
            textbox.Verstecken();
        }

        /// <summary>
        /// Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns>Saveinfo.</returns>
        public Saveinfo MouseKeys(MouseState mouseState)
        {
            if (!visible) return new Saveinfo();
            int j = 0;

            #region Update Filelist

            foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
            {
                j++;
                if (j > data.Count())
                {
                    data.Add(f.Name);
                    if (j == 1)
                        stringspos.Add(new Vector2(50, 50));
                    else
                        stringspos.Add(stringspos[data.Count - 2] + new Vector2(0, 30));
                    stringbox.Add(new BoundingBox(new Vector3(stringspos[data.Count - 1], 0),
                        new Vector3(stringspos[data.Count - 1].X + 300, stringspos[data.Count - 1].Y + 30, 0)));
                }
                else
                {
                    if (data[j - 1] != f.Name)
                    {
                        if (data[j - 1] != f.Name)
                        {
                            data.Insert(j - 1, f.Name);
                            stringspos.Add(stringspos[data.Count - 2] + new Vector2(0, 30));
                            stringbox.Add(new BoundingBox(new Vector3(stringspos[data.Count - 1], 0),
                                new Vector3(stringspos[data.Count - 1].X + 300, stringspos[data.Count - 1].Y + 30, 0)));
                        }
                    }
                }
            }

            #endregion Update Filelist

            #region GetActiveString

            for (int i = 0; i < stringbox.Count; i++)
                if (stringbox[i].Contains(new Vector3(mouseState.X, mouseState.Y, 0)) == ContainmentType.Contains
                    && Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    if (data[i].EndsWith(".map"))
                    {
                        activeString = i;
                        textbox.Zurücksetzen();
                    }

            #endregion GetActiveString

            if (textbox.mouseKeys()) activeString = -1;

            #region Menueintrage

            if (menuItems[0].MouseKeys())
            {
                if (textbox.input != "")
                {
                    string temp = string.Copy(textbox.input);
                    textbox.Zurücksetzen();
                    return new Saveinfo(0, "Saves//" + textbox.input + ".map");
                }
                else
                {
                    textbox.Zurücksetzen();
                    return new Saveinfo(0, "Saves//" + data[activeString]);
                }
            }
            if (menuItems[1].MouseKeys())
            {
                textbox.Zurücksetzen();
                return new Saveinfo(1, "Saves//" + data[activeString]);
            }
            if (menuItems[2].MouseKeys())
            {
                textbox.Zurücksetzen();
                return new Saveinfo(2, "");
            }

            #endregion Menueintrage

            return new Saveinfo();
        }

        //Methode zum Erstellen eines neuen Menüeintrages
        //Übergibt den Namen und Position
        /// <summary>
        /// Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            textbox.OnKeyPress(sender, e);
        }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
            textbox.Anzeigen();
        }
    }
}