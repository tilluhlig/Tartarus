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

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     das Startmenü
    /// </summary>
    internal class Startmenu
    {
        #region vars

        /// <summary>
        ///     die Farbe der selektierten Menüpunkte
        /// </summary>
        private readonly Color selected;

        /// <summary>
        ///     die Farbe für nicht-selektierte Menüpunkte
        /// </summary>
        private readonly Color unselected; //Farbe der aktuell nicht ausgewählten Einträge

        /// <summary>
        ///     die Schrift für Beschriftungen
        /// </summary>
        private SpriteFont font;

        /// <summary>
        ///     eine Liste der Menüpunkte
        /// </summary>
        public Button[] menuItems = new Button[4];

        /// <summary>
        ///     Sichtbarkeit, true = sichtbar, false = unsichtbar
        /// </summary>
        public bool visible = false;

        #endregion vars

        #region Fields

        /// <summary>
        ///     die Fensterbreite
        /// </summary>
        public int screenHeight;

        /// <summary>
        ///     die Fensterhöhe
        /// </summary>
        public int screenWidth;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Constructor des Menüs
        ///     Initialisiert Standardwerte
        /// </summary>
        /// <param name="unselectedColor">die Färbung der nicht gewählten Menüpunkte</param>
        /// <param name="selectedColor">die Färbung des gewählten Menüpunktes</param>
        /// <param name="font">eine Schriftart für die Beschriftung</param>
        /// <param name="Breite">die Fensterbreite</param>
        /// <param name="Hoehe">die Fensterhöhe</param>
        public Startmenu(Color unselectedColor, Color selectedColor, SpriteFont font, int Breite, int Hoehe)
        {
            screenWidth = Breite;
            screenHeight = Hoehe;
            this.font = font;

            unselected = unselectedColor;
            selected = selectedColor;

            var aux = new Vector2((screenWidth - Texturen.Button1.Width)/2, 80);
            menuItems[0] = new Button(Texturen.Button1, aux, "Neues", "Spiel", font);
            menuItems[1] = new Button(Texturen.Button1, aux + new Vector2(0, 80*1), "Spiel", "Laden", font);
            menuItems[2] = new Button(Texturen.Button1, aux + new Vector2(0, 80*2), "Optionen", font);
            menuItems[3] = new Button(Texturen.Button1, aux + new Vector2(0, 80*3), "Spiel", "Verlassen", font);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     übernimmt das Zeichnen des Menüs
        /// </summary>
        /// <param name="spriteBatch">ein Zeichenoberfläche</param>
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

            var aux = new Vector2((screenWidth - Texturen.Button1.Width)/2, 80);

            // davor auffüllen
            for (int i = -2; i < 0; i++)
                spriteBatch.Draw(Texturen.Button1, aux + new Vector2(0, 80*i), unselected*0.5f);

            // danach auffüllen
            int Danach = (int) Math.Ceiling((float) Game1.screenHeight/80) - 4;
            for (int i = menuItems.Length; i < menuItems.Length + Danach; i++)
                spriteBatch.Draw(Texturen.Button1, aux + new Vector2(0, 80*i), unselected*0.5f);
        }

        /// <summary>
        ///     verbirgt das Startmenü
        /// </summary>
        public void hide()
        {
            visible = false;
        }

        /// <summary>
        ///     verwaltet Mausaktionen
        /// </summary>
        /// <param name="mouseState">der Status der Maus</param>
        /// <returns>-1 = kein Menüpunkt gedrückt, sonst ein Menüpunkt gedrückt.</returns>
        public int MouseKeys(MouseState mouseState)
        {
            if (!visible) return -1;
            for (int i = 0; i < menuItems.Length; i++)
                if (menuItems[i].MouseKeys())
                    return i;
            return -1;
        }

        /// <summary>
        ///     macht das Startmenü sichtbar
        /// </summary>
        public void show()
        {
            visible = true;
        }

        #endregion Methods
    }
}