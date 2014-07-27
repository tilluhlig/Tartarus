// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-21-2013
// ***********************************************************************
// <copyright file="Eingabefenster.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse verwaltet das Eingabefenster, welches für spezielle Befehle geöffnet werden kann
    /// </summary>
    public static class Eingabefenster
    {
        #region Fields

        /// <summary>
        ///     diese Liste enthält Befehle, die als "mögliche" Befehle angezeigt werden
        ///     Bsp.: es wird "#Mi" eingegeben, so zeigt die Eingabezeile sowas wie "#Mine" als möglichen Befehl an
        /// </summary>
        public static List<String> able = new List<String>();

        /// <summary>
        ///     eine Liste der möglichen Befehle
        /// </summary>
        public static List<String> Befehle = new List<String>();

        /// <summary>
        ///     das Textfeld der Eingabe
        /// </summary>
        public static Textfeld Eingabe = null;

        /// <summary>
        ///     wurde diese Klasse zum ersten mal aufgerufen, zur Initialisierung
        /// </summary>
        public static bool ErsterAufruf = true;

        /// <summary>
        ///     die aktuelle Position in der History, zum bewegen in der History
        /// </summary>
        public static int hist = 0;

        /// <summary>
        ///     eine Liste der bisherigen Eingaben
        /// </summary>
        public static List<String> History = new List<String>();

        #endregion Fields

        #region Methods

        /// <summary>
        ///     initialisiert die Klasse
        /// </summary>
        public static void Initialisieren()
        {
            if (ErsterAufruf)
            {
                ErsterAufruf = false;
                History.Add("");
                for (int i = 0; i < Var<String>.ALLE.Count; i++) Befehle.Add(Var<String>.ALLE[i].Name + "=");
                for (int i = 0; i < Var<int>.ALLE2.Count; i++) Befehle.Add(Var<int>.ALLE2[i].Name + "=");
                for (int i = 0; i < Var<bool>.ALLE3.Count; i++) Befehle.Add(Var<bool>.ALLE3[i].Name + "=");
                for (int i = 0; i < Var<float>.ALLE4.Count; i++) Befehle.Add(Var<float>.ALLE4[i].Name + "=");

                for (int i = 0; i < Var<String[]>.ALLE5.Count; i++) Befehle.Add(Var<String[]>.ALLE5[i].Name);
                for (int i = 0; i < Var<int[]>.ALLE6.Count; i++) Befehle.Add(Var<int[]>.ALLE6[i].Name);
                for (int i = 0; i < Var<bool[]>.ALLE7.Count; i++) Befehle.Add(Var<bool[]>.ALLE7[i].Name);
                for (int i = 0; i < Var<float[]>.ALLE8.Count; i++) Befehle.Add(Var<float[]>.ALLE8[i].Name);

                for (int i = 0; i < SpezialBefehle.Befehle.Count(); i++) Befehle.Add(SpezialBefehle.Befehle[i]);

                /*
               for (int i = 0; i < Var<String[]>.ALLE5.Count; i++) for (int b = 0; b < Var<String[]>.ALLE5[i].Wert.Count(); b++) Befehle.Add(Var<String[]>.ALLE5[i].Name + "[" + b.ToString() + "]");
                 for (int i = 0; i < Var<int[]>.ALLE6.Count; i++) for (int b = 0; b < Var<int[]>.ALLE6[i].Wert.Count(); b++) Befehle.Add(Var<int[]>.ALLE6[i].Name + "[" + b.ToString() + "]");
                 for (int i = 0; i < Var<bool[]>.ALLE7.Count; i++) for (int b = 0; b < Var<bool[]>.ALLE7[i].Wert.Count(); b++) Befehle.Add(Var<bool[]>.ALLE7[i].Name + "[" + b.ToString() + "]");
                 for (int i = 0; i < Var<float[]>.ALLE8.Count; i++) for (int b = 0; b < Var<float[]>.ALLE8[i].Wert.Count(); b++) Befehle.Add(Var<float[]>.ALLE8[i].Name + "[" + b.ToString() + "]");
                */

                Befehle.Sort();

                Eingabe =
                    new Textfeld(
                        new Vector2(500 - Texturen.font3.MeasureString(("").PadLeft(30, ' ')).X/2,
                            Game1.screenHeight/2 - Texturen.font3.MeasureString((" ")).Y/2), "", 30, 10, 5, 2,
                        Color.Gray, Texturen.font3);
            }
        }

        /// <summary>
        ///     verwaltet für das Eingabefenster die Tastatureingaben
        /// </summary>
        /// <param name="keybState">der letzte Zustand der Tastatur</param>
        /// <param name="keybState2">der neue zustand der Tastatur</param>
        public static void KeyboardKeys(KeyboardState keybState, KeyboardState keybState2)
        {
            if (ErsterAufruf)
            {
                ErsterAufruf = false;
                Initialisieren();
            }

            if (keybState != keybState2 && !Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (!Eingabe.Sichtbar) return;

                if (keybState != keybState2 && Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (Eingabe.input.Length > 1 && able.Count >= 1)
                    {
                        if (Eingabe.input.Substring(0, 1) == "#")
                        {
                            Eingabe.input = "#" + able[0];
                        }
                    }
                }
                else if (keybState != keybState2 && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (hist < History.Count() - 1)
                    {
                        hist++;
                    }
                    Eingabe.input = History[hist];
                }
                else if (keybState != keybState2 && Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (hist > 0)
                    {
                        hist--;
                    }
                    Eingabe.input = History[hist];
                }
            }
            else if (keybState != keybState2 && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (Eingabe.Sichtbar)
                {
                    // ausführen
                    if (Eingabe.input.Length > 1)
                    {
                        if (Game1.Meldungen != null) Game1.Meldungen.addMessage(Eingabe.input);
                        History.Add(Eingabe.input);
                        History.Insert(History.Count - 1, Eingabe.input);
                        hist = History.Count() - 1;
                        if (Eingabe.input.Substring(0, 1) == "#")
                        {
                            Eingabe.input = Eingabe.input.Substring(1, Eingabe.input.Length - 1);
                            String[] data = Eingabe.input.Split('=');
                            // Mod Variable
                            if (data.Count() >= 2)
                            {
                                // Es handelt sich um eine Einstellbare Variable
                                data[0] = data[0].ToUpper();
                                if (Var<bool>.SetFromALLE(data[0], data[1]))
                                {
                                }
                                else if (Var<int>.SetFromALLE(data[0], data[1]))
                                {
                                }
                                else if (Var<String>.SetFromALLE(data[0], data[1]))
                                {
                                }
                                else if (Var<float>.SetFromALLE(data[0], data[1]))
                                {
                                }
                                else if (Var<bool[]>.SetFromALLE(data[0], data[1]))
                                {
                                }
                                else if (Var<int[]>.SetFromALLE(data[0], data[1]))
                                {
                                }
                                else if (Var<String[]>.SetFromALLE(data[0], data[1]))
                                {
                                }
                                else if (Var<float[]>.SetFromALLE(data[0], data[1]))
                                {
                                }
                            }
                            else if (data.Count() == 1)
                            {
                                // Es ist womöglich ein Befehl
                                SpezialBefehle.PrüfeBefehl(data[0]);
                            }
                        }
                    }
                    Eingabe.Zurücksetzen();
                    Eingabe.Sichtbar = false;
                    Eingabe.Ausgewählt = false;
                }
                else
                {
                    Eingabe.Ausgewählt = true;
                    Eingabe.Sichtbar = true;
                }
            }
        }

        /// <summary>
        ///     zeichnet das Eingabefenster auf dem Bildschirm
        /// </summary>
        /// <param name="spriteBatch">eine Zeichenfläche</param>
        public static void ZeichneEingabefenster(SpriteBatch spriteBatch)
        {
            if (Eingabe.Sichtbar)
            {
                Eingabe.ZeichneTextfeld(spriteBatch);

                if (Eingabe.input.Length > 1)
                {
                    if (Eingabe.input.Substring(0, 1) == "#")
                    {
                        String text = Eingabe.input.Substring(1, Eingabe.input.Length - 1);
                        String[] data = text.Split('=');

                        // Mod Variablen erkennen
                        if (data.Count() == 1 && text.Length >= 1)
                        {
                            // welche Befehle kommen in frage?
                            bool found = false;
                            able.Clear();
                            for (int i = 0; i < Befehle.Count; i++)
                            {
                                if (Befehle[i][0] != text[0] && found) break;
                                if (Befehle[i][0] == text[0]) found = true;
                                if (Befehle[i].Length >= text.Length)
                                    if (Befehle[i].Substring(0, text.Length) == text.ToUpper())
                                    {
                                        able.Add(Befehle[i]);
                                    }
                            }

                            int anz = able.Count;
                            if (anz >= 10) anz = 10;
                            int y = Eingabe.height + 2*Eingabe.offsety;
                            var size = (int) Texturen.font2.MeasureString(" ").Y;
                            int height = anz*(size + 1) + 6;

                            if (anz > 0)
                            {
                                spriteBatch.Draw(Textfeld.textbox,
                                    new Rectangle((int) Eingabe.pos.X, (int) (Eingabe.pos.Y + y),
                                        Eingabe.width + 2*Eingabe.offsetx, height),
                                    new Rectangle(0, 0, Textfeld.textbox.Width, Textfeld.textbox.Height), Color.DimGray);

                                for (int i = 0; i < able.Count && i < 10; i++)
                                {
                                    String aus = able[i];
                                    if (aus.Length > Eingabe.Length) aus = aus.Substring(0, Eingabe.Length - 3) + "...";
                                    spriteBatch.DrawString(Texturen.font2, aus,
                                        new Vector2((int) Eingabe.pos.X, (int) (Eingabe.pos.Y + y + 3 + (size + 1)*i)),
                                        Color.Black);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}