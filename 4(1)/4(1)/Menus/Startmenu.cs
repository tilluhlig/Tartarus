using System;

// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-17-2013
// ***********************************************************************
// <copyright file="Startmenu.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    //ist das allererste Menu
    /// <summary>
    /// Class Startmenu
    /// </summary>
    internal class Startmenu
    {
        #region vars

        /// <summary>
        /// The menu items
        /// </summary>
        public Button[] menuItems = new Button[4];

        /// <summary>
        /// The visible
        /// </summary>
        public bool visible = false;

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
        /// The screen height
        /// </summary>
        public int screenHeight;

        /// <summary>
        /// The screen width
        /// </summary>
        public int screenWidth;

        //    private Texture2D menutexture = Texturen.Pausenmenu;

        //Constructor des Menüs
        //Initialisiert Standardwerte
        /// <summary>
        /// Initializes a new instance of the <see cref="Startmenu"/> class.
        /// </summary>
        /// <param name="unselectedColor">Color of the unselected.</param>
        /// <param name="selectedColor">Color of the selected.</param>
        /// <param name="font">The font.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        public Startmenu(Color unselectedColor, Color selectedColor, SpriteFont font, int w, int h)
        {
            this.screenWidth = w;
            this.screenHeight = h;
            this.font = font;

            this.unselected = unselectedColor;
            this.selected = selectedColor;

            Vector2 aux = new Vector2((screenWidth - Texturen.Button1.Width) / 2, 80);
            menuItems[0] = new Button(Texturen.Button1, aux, "Neues", "Spiel", font);
            menuItems[1] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 1), "Spiel", "Laden", font);
            menuItems[2] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 2), "Optionen", font);
            menuItems[3] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 3), "Spiel", "Verlassen", font);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;
            //Durchläuft alle Menüpunkte und zeichnet den Menüpunkt mit den entsprechenden Eigenschaften
            // spriteBatch.Draw(Texturen.Startmenu, new Vector2(screenWidth / 2 - Texturen.Startmenu.Width / 2, screenHeight / 2 - Texturen.Startmenu.Height / 2),                Color.White);

            /*  int anz = (int)Math.Ceiling((double)((float)Game1.screenHeight / menutexture.Height));
              for (int i = 0; i < anz; i++)
              {
                  spriteBatch.Draw(menutexture, new Vector2(screenWidth / 2 - menutexture.Width / 2, i * menutexture.Height), Color.White);
              }*/

            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Draw(spriteBatch, selected, unselected);
            }

            Vector2 aux = new Vector2((screenWidth - Texturen.Button1.Width) / 2, 80);

            // davor auffüllen
            for (int i = -2; i < 0; i++)
                spriteBatch.Draw(Texturen.Button1, aux + new Vector2(0, 80 * i), unselected * 0.5f);

            // danach auffüllen
            int Danach = (int)Math.Ceiling((float)Game1.screenHeight / 80) - 4;
            for (int i = menuItems.Length; i < menuItems.Length + Danach; i++)
                spriteBatch.Draw(Texturen.Button1, aux + new Vector2(0, 80 * i), unselected * 0.5f);
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
        }

        /// <summary>
        /// Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns>System.Int32.</returns>
        public int MouseKeys(MouseState mouseState)
        {
            if (!visible) return -1;
            for (int i = 0; i < menuItems.Length; i++)
                if (menuItems[i].MouseKeys())
                    return i;
            return -1;
        }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
        }

        //Methode zum Erstellen eines neuen Menüeintrages
        //Übergibt den Namen und Position
    }
}