// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-19-2013
// ***********************************************************************
// <copyright file="Menu.cs" company="">
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
    ///     Class Menu
    /// </summary>
    internal class Menu
    {
        #region Fields

        //Constructor des Menüs
        //Initialisiert Standardwerte
        /// <summary>
        ///     The aux
        /// </summary>
        private readonly Vector2 aux;

        /// <summary>
        ///     The menu items
        /// </summary>
        private readonly Button[] menuItems = new Button[5];

        //   private Texture2D menutexture = Texturen.Pausenmenu;
        /// <summary>
        ///     The menutexture
        /// </summary>
        /// <summary>
        ///     The selected
        /// </summary>
        private readonly Color selected;

        /// <summary>
        ///     The unselected
        /// </summary>
        private readonly Color unselected;

        /// <summary>
        ///     The screen height
        /// </summary>
        public int screenHeight;

        /// <summary>
        ///     The screen width
        /// </summary>
        public int screenWidth;

        /// <summary>
        ///     The visible
        /// </summary>
        public bool visible = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Menu" /> class.
        /// </summary>
        /// <param name="unselectedColor">Color of the unselected.</param>
        /// <param name="selectedColor">Color of the selected.</param>
        /// <param name="font">The font.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        public Menu(Color unselectedColor, Color selectedColor, SpriteFont font, int w, int h)
        {
            screenWidth = w;
            screenHeight = h;
            selected = selectedColor;
            unselected = unselectedColor;

            aux = new Vector2((screenWidth - Texturen.Button1.Width) / 2, 90);

            menuItems[0] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 0), "Neues", "Spiel", font);
            menuItems[1] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 1), "Speichern", "Laden", font);
            menuItems[2] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 2), "Optionen", font);
            menuItems[3] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 3), "Zuruck", "zum Spiel", font);
            menuItems[4] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 4), "Spiel", "beenden", font);
        }

        #endregion Constructors

        #region Methods

        //Methode um das komplette Menü zu zeichnen
        //Übergibt die Spritebatch und einen Boolschen Wert der abgibt ob sich das Menü außerhalb von
        //spriteBatch.Begin()/.End() befindet
        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch, float Transparenz)
        {
            if (!visible) return;
            //Durchläuft alle Menüpunkte und zeichnet den Menüpunkt mit den entsprechenden Eigenschaften

            for (int i = 0; i < menuItems.Length; i++)
                menuItems[i].Draw(spriteBatch, selected * (1 - Transparenz), unselected * (1 - Transparenz));

            // davor
            for (int i = -2; i < 0; i++)
                spriteBatch.Draw(Texturen.Button1, aux + new Vector2(0, 80 * i), unselected * 0.5f * (1 - Transparenz));

            // danach
            int Danach = (int)Math.Ceiling((float)Game1.screenHeight / 80) - 6;
            for (int i = menuItems.Length; i < menuItems.Length + Danach; i++)
                spriteBatch.Draw(Texturen.Button1, aux + new Vector2(0, 80 * i), unselected * 0.5f * (1 - Transparenz));

            Game1.ZeichneLogo(1 - Transparenz);
        }

        /// <summary>
        ///     Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
        }

        /// <summary>
        ///     Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <returns>System.Int32.</returns>
        public int mouseKeys(MouseState mouseState)
        {
            if (!visible) return -1;
            for (int i = 0; i < menuItems.Length; i++)
                if (menuItems[i].MouseKeys())
                    return i;
            return -1;
        }

        /// <summary>
        ///     Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
        }

        #endregion Methods
    }
}