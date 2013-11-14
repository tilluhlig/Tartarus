using System;

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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    /// Class Menu
    /// </summary>
    internal class Menu
    {
        /// <summary>
        /// The menu items
        /// </summary>
        private Button[] menuItems = new Button[5];

        /// <summary>
        /// The screen height
        /// </summary>
        public int screenHeight;

        /// <summary>
        /// The screen width
        /// </summary>
        public int screenWidth;

        /// <summary>
        /// The visible
        /// </summary>
        public bool visible = false;

        //Constructor des Menüs
        //Initialisiert Standardwerte
        /// <summary>
        /// The aux
        /// </summary>
        private Vector2 aux;

        /// <summary>
        /// The menutexture
        /// </summary>
        //   private Texture2D menutexture = Texturen.Pausenmenu;

        /// <summary>
        /// The selected
        /// </summary>
        private Color selected;

        /// <summary>
        /// The unselected
        /// </summary>
        private Color unselected;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="unselectedColor">Color of the unselected.</param>
        /// <param name="selectedColor">Color of the selected.</param>
        /// <param name="font">The font.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        public Menu(Color unselectedColor, Color selectedColor, SpriteFont font, int w, int h)
        {
            this.screenWidth = w;
            this.screenHeight = h;
            this.selected = selectedColor;
            this.unselected = unselectedColor;

            aux = new Vector2((screenWidth - Texturen.Button1.Width) / 2, 90);

            menuItems[0] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 0), "Neues", "Spiel", font);
            menuItems[1] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 1), "Speichern", "Laden", font);
            menuItems[2] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 2), "Optionen", font);
            menuItems[3] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 3), "Zuruck", "zum Spiel", font);
            menuItems[4] = new Button(Texturen.Button1, aux + new Vector2(0, 80 * 4), "Spiel", "beenden", font);
        }

        //Methode um das komplette Menü zu zeichnen
        //Übergibt die Spritebatch und einen Boolschen Wert der abgibt ob sich das Menü außerhalb von
        //spriteBatch.Begin()/.End() befindet
        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!visible) return;
            //Durchläuft alle Menüpunkte und zeichnet den Menüpunkt mit den entsprechenden Eigenschaften
            /*int anz = (int)Math.Ceiling((double)((float)Game1.screenHeight / menutexture.Height));
            for (int i = 0; i < anz; i++)
            {
                spriteBatch.Draw(menutexture, new Vector2(screenWidth / 2 - menutexture.Width / 2, i * menutexture.Height), Color.White);
            }*/

            for (int i = 0; i < menuItems.Length; i++)
            {
                menuItems[i].Draw(spriteBatch, selected, unselected);
                /*
                if (i == curMenuItem)
                {
                    Vector2 p = pos[i];                 //Weist die Position einer temporären Variable zu
                    p.X -= (float)(22 / 2);  //Verschiebt die Position der x-Achse um den Namen mittig anzuzeigen
                    p.Y -= (float)(22 / 2);  //Verschiebt die Position der y-Achse um den Namen mittig anzuzeigen
                    spriteBatch.Draw(menuItems[i],p ,null,selected, 0, new Vector2(0, 0), (float)scale[i], SpriteEffects.None, 0);
                }
                else
                {
                    Vector2 p = pos[i];                 //Weist die Position einer temporären Variable zu
                    p.X -= (float)(22  / 2);  //Verschiebt die Position der x-Achse um den Namen mittig anzuzeigen
                    p.Y -= (float)(22  / 2);  //Verschiebt die Position der y-Achse um den Namen mittig anzuzeigen
                    spriteBatch.Draw(menuItems[i], p, null, unselected, 0, new Vector2(0, 0), (float)scale[i], SpriteEffects.None, 0);
                }*/
            }

            // davor
            for (int i = -2; i < 0; i++)
                spriteBatch.Draw(Texturen.Button1, aux + new Vector2(0, 80 * i), unselected * 0.5f);

            // danach
            int Danach = (int)Math.Ceiling((float)Game1.screenHeight / 80) - 6;
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
        public int mouseKeys(MouseState mouseState)
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

        /*
        //Methode um den nächsten Menüpunkt auszuwählen
        //Nach letztem Menüpunkt zum ersten zurückspringen
        public void SelectNext()
        {
            if (curMenuItem < menuItemCount - 1)
            {
                curMenuItem++;
            }
            else
            {
                curMenuItem = 0;
            }
        }

        //Methode um den letzten Menüpunkt auszuwählen
        //Nach erstem Menüpunkt zum letzten zurückspringen
        public void SelectPrev()
        {
            if (curMenuItem > 0)
            {
                curMenuItem--;
            }
            else
            {
                curMenuItem = menuItemCount - 1;
            }
        }
        */
    }
}