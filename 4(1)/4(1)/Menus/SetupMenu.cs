// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-24-2013
// ***********************************************************************
// <copyright file="SetupMenu.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#region Using Statements

using System.Windows.Forms; // This class exposes WinForms-style key events.

#endregion Using Statements

namespace _4_1_
{
    /// <summary>
    /// Class SetupMenu
    /// </summary>
    public class SetupMenu
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

        //private Vector2[] pos;              //Array mit allen Menüpositionen
        /// <summary>
        /// The unselected
        /// </summary>
        private Color unselected;           //Farbe der aktuell nicht ausgewählten Einträge

        //Farbe des ausgewählten Eintrages

        //Fontdatei des Menüs

        #endregion vars

        /// <summary>
        /// The lokal
        /// </summary>
        public bool lokal = false;

        /// <summary>
        /// The visible
        /// </summary>
        public bool visible = false;

        /// <summary>
        /// The buttons
        /// </summary>
        private List<Button> buttons = new List<Button>();

        //Passwort andern
        /// <summary>
        /// The current passwort
        /// </summary>
        private Textfeld CurrentPasswort;

        /// <summary>
        /// The e mail
        /// </summary>
        private Textfeld eMail;

        /// <summary>
        /// The login button
        /// </summary>
        private Button LoginButton;

        /// <summary>
        /// The new passwort
        /// </summary>
        private Textfeld NewPasswort;

        /// <summary>
        /// The passwort
        /// </summary>
        private Textfeld Passwort;

        /// <summary>
        /// The passwort bestatigen
        /// </summary>
        private Textfeld passwortBestatigen;

        /// <summary>
        /// The passwortbestatigen
        /// </summary>
        private Textfeld Passwortbestatigen;

        /// <summary>
        /// The screen height
        /// </summary>
        private int screenHeight;

        /// <summary>
        /// The screen width
        /// </summary>
        private int screenWidth;

        /// <summary>
        /// The set
        /// </summary>
        private Button set;

        /// <summary>
        /// The set passwort
        /// </summary>
        private Textfeld setPasswort;

        /// <summary>
        /// The set player
        /// </summary>
        private Button SetPlayer;

        //Spieler einloggen
        /// <summary>
        /// The spieler
        /// </summary>
        private Textfeld Spieler;

        //neuer Spieler
        /// <summary>
        /// The spieler name
        /// </summary>
        private Textfeld spielerName;

        /// <summary>
        /// The text boxen
        /// </summary>
        private List<Textfeld> textBoxen = new List<Textfeld>();

        /// <summary>
        /// The tickbox lokal
        /// </summary>
        private TickBox TickboxLokal;

        //
        //Constructor des Menüs
        //Initialisiert Standardwerte
        /// <summary>
        /// Initializes a new instance of the <see cref="SetupMenu"/> class.
        /// </summary>
        /// <param name="unselectedColor">Color of the unselected.</param>
        /// <param name="selectedColor">Color of the selected.</param>
        /// <param name="_font">The _font.</param>
        /// <param name="w">The w.</param>
        /// <param name="h">The h.</param>
        public SetupMenu(Color unselectedColor, Color selectedColor, SpriteFont _font, int w, int h)
        {
            screenWidth = w;
            screenHeight = h;
            font = _font;
            unselected = unselectedColor;
            selected = selectedColor;
            /*
            //Spielerlogin
            Spieler = new TextBox(new Vector2(50, 100), "Spieler:");
            Passwort = new TextBox(new Vector2(50, 125), "Passwort:");
            LoginButton = new Button(Texturen.hausbutton, new Vector2(75, 150), "Login", font);
            //Passwort andern
            CurrentPasswort = new TextBox(new Vector2(50, 200), "Altes Passwort:");
            NewPasswort = new TextBox(new Vector2(50, 225), "Neues Passwort:");
            Passwortbestatigen = new TextBox(new Vector2(50, 250), "Paswort bestatigen:");
            set = new Button(Texturen.hausbutton, new Vector2(75, 275), "Setzen", font);
            //neuer Spieler
            spielerName = new TextBox(new Vector2(50, 325), "Name:");
            setPasswort = new TextBox(new Vector2(50, 350), "Passwort:");
            passwortBestatigen = new TextBox(new Vector2(50, 375), "Passwort bestatigen:");
            eMail = new TextBox(new Vector2(50,400),"e-Mail:");
            SetPlayer = new Button(Texturen.hausbutton, new Vector2(75, 425), "Erstellen", font);
            */
            textBoxen.Add(new Textfeld(new Vector2(50, 100), "Spieler:"));
            textBoxen.Add(new Textfeld(new Vector2(50, 130), "Passwort:", true));
            textBoxen.Add(new Textfeld(new Vector2(50, 190), "Altes Passwort:", true));
            textBoxen.Add(new Textfeld(new Vector2(50, 220), "Neues Passwort:", true));
            textBoxen.Add(new Textfeld(new Vector2(50, 250), "Paswort bestatigen:", true));
            textBoxen.Add(new Textfeld(new Vector2(50, 310), "Name:"));
            textBoxen.Add(new Textfeld(new Vector2(50, 340), "Passwort:", true));
            textBoxen.Add(new Textfeld(new Vector2(50, 370), "Passwort bestatigen:", true));
            textBoxen.Add(new Textfeld(new Vector2(50, 400), "e-Mail:"));

            buttons.Add(new Button(Texturen.hausbutton, new Vector2(300, 100), "Login", font));
            buttons.Add(new Button(Texturen.hausbutton, new Vector2(300, 190), "Setzen", font));
            buttons.Add(new Button(Texturen.hausbutton, new Vector2(300, 310), "Erstellen", font));

            TickboxLokal = new TickBox(Color.Gold, Color.White, new Vector2(300, 20), 0.7f, false);
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

            //zeichne Einloggenmenu
            spriteBatch.DrawString(Texturen.AlgerianFont, "Einloggen", new Vector2(50, 75), Color.Black);

            spriteBatch.DrawString(Texturen.AlgerianFont, "Passwort andern", new Vector2(50, 160), Color.Black);

            spriteBatch.DrawString(Texturen.AlgerianFont, "Neuer Spieler", new Vector2(50, 280), Color.Black);

            spriteBatch.DrawString(font, "Lokal:", new Vector2(50, 25), Color.Black);
            for (int i = 0; i < textBoxen.Count; i++)
                textBoxen[i].ZeichneTextfeld(spriteBatch);
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Draw(spriteBatch, Color.Goldenrod, Color.White);

            TickboxLokal.Draw(spriteBatch);
        }

        /// <summary>
        /// Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
            for (int i = 0; i < textBoxen.Count; i++) textBoxen[i].Verstecken();
            for (int i = 0; i < buttons.Count; i++) buttons[i].hide();
            TickboxLokal.Verstecken();
        }

        //Methode zum Erstellen eines neuen Menüeintrages
        //Übergibt den Namen und Position
        /// <summary>
        /// Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool MouseKeys(MouseState mouseState, SpriteBatch spriteBatch)
        {
            if (!visible) return false;
            //noch zu fullen
            lokal = TickboxLokal.MouseKeys(mouseState);
            for (int i = 0; i < textBoxen.Count; i++)
                textBoxen[i].mouseKeys();

            for (int i = 0; i < buttons.Count; i++)
                buttons[i].MouseKeys();

            return false;
        }

        /// <summary>
        /// Called when [key press].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!visible) return;
            for (int i = 0; i < textBoxen.Count; i++)
                textBoxen[i].OnKeyPress(sender, e);
        }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
            for (int i = 0; i < textBoxen.Count; i++) textBoxen[i].Anzeigen();
            for (int i = 0; i < buttons.Count; i++) buttons[i].show();
            TickboxLokal.Anzeigen();
        }
    }
}