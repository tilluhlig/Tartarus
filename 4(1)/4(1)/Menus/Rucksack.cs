// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-31-2013
// ***********************************************************************
// <copyright file="Backpack.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     Diese Klasse stellt Inventare in einem Menü dar
    /// </summary>
    public class Rucksack
    {
        #region Fields

        /// <summary>
        ///     enthält einen Wert für das ausgewählte Item
        /// </summary>
        public int selected = 255;

        /// <summary>
        ///     liefert den Abstand zwischen den Buttons
        /// </summary>
        private readonly Vector2 aux = new Vector2(10, 5);

        /// <summary>
        ///     die Breite des Rucksacks (Darstellung)
        /// </summary>
        private readonly byte height = 1;

        /// <summary>
        ///     die Länge des Rucksacks (Darstellung)
        /// </summary>
        private readonly byte length = 2;

        /// <summary>
        ///     die Position des Menüs
        /// </summary>
        private readonly Vector2 ownPos = new Vector2(0, 0);

        /// <summary>
        ///     die Bildschirmpositionen der Buttons
        /// </summary>
        private Vector2[] buttonPos = new Vector2[0];

        /// <summary>
        ///     die auswählbaren Felder des Menüs
        /// </summary>
        private BoundingBox[] buttons = new BoundingBox[0];

        /// <summary>
        ///     aktuelle Scrollposition
        /// </summary>
        private int scrolled;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// initialisiert ein neues Rucksackobjekt
        /// </summary>
        /// <param name="pos">die Bildschirmposition des Menüs</param>
        /// <param name="length">die Länge des Inventars (Darstellung)</param>
        /// <param name="height">die Breite des Inventars (Darstellung)</param>
        public Rucksack(Vector2 pos, byte length, byte height)
        {
            ownPos = pos;
            this.length = length;
            this.height = height;
            setButtons();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Zeichnet den Rucksack
        /// </summary>
        /// <param name="_Zeichenfläche">eine Zeichenfläche</param>
        /// <param name="Rucksack">ein Inventar, welches dargestellt wird</param>
        /// <param name="clickselected">das ausgewählte Feld wird hier übergeben</param>
        /// <param name="Transparenz">ein Wert für die Transparenz (0.0 bis 1.0)</param>
        public void Draw(SpriteBatch _Zeichenfläche, Inventar Rucksack, ref int clickselected, float Transparenz)
        {
            if (Rucksack == null) return;

            if (scrolled > 0)
            {
                if (selected == 240)
                {
                    _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[length * height], null, Color.Red * Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    _Zeichenfläche.Draw(Texturen.nachOben, buttonPos[length * height], null, Color.Red * Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
                else
                {
                    _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[length * height], null, Color.White * Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    _Zeichenfläche.Draw(Texturen.nachOben, buttonPos[length * height], null, Color.White * Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
            }
            else
            {
                _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[length * height], null, Color.Silver * 0.4f * Transparenz, 0f,
                 Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                _Zeichenfläche.Draw(Texturen.nachOben, buttonPos[length * height], null, Color.Silver * 0.4f * Transparenz, 0f,
                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
            }

            int tre = Rucksack.GibTreibstoffFächer();
            int mun = Rucksack.GibMunitionsFächer();
            int upg = Rucksack.GibUpgradeFächer();
            int kon = Rucksack.GibKonsumierbareFächer();

            List<Vector2> munlist = Rucksack.GibMunitionsliste();
            List<Vector2> upglist = Rucksack.GibtListeUpgrades();
            List<Vector2> konlist = Rucksack.GibListeKonsumierbares();

            int maxscrolls = ((mun + upg + kon + tre) - length) / (length);

            for (byte i = 0; i < height * length; i++)
            {
                // ist es treibstoff?
                if (i + scrolled * length < tre)
                {
                    if (clickselected == i + scrolled * length)
                    {
                        _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White * Transparenz, 0f,
                            Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        _Zeichenfläche.Draw(Texturen.fuel, buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        _Zeichenfläche.Draw(Texturen.rahmen, buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    }
                    else if (selected == i + scrolled * length)
                    {
                        _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.Red * Transparenz, 0f,
                            Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        _Zeichenfläche.Draw(Texturen.fuel, buttonPos[i], null, Color.Red * Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    }
                    else
                    {
                        _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White * Transparenz, 0f,
                            Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        _Zeichenfläche.Draw(Texturen.fuel, buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    }

                    Help.DrawString(_Zeichenfläche, Texturen.font2,
                        Math.Floor(Rucksack.GibTreibstoff() < 0 ? 0 : Rucksack.GibTreibstoff()).ToString(),
                        buttonPos[i] + aux, Color.Gold * Transparenz, Color.Black * Transparenz);
                }
                else // ist es munition?
                    if (i + scrolled * length < mun + tre)
                    {
                        if (clickselected == i + scrolled * length)
                        {
                            _Zeichenfläche.Draw(Texturen.LeeresFeld,
                                buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);

                            _Zeichenfläche.Draw(Texturen.waffenbilder[(int)munlist[i + scrolled * length - tre].X],
                                buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            _Zeichenfläche.Draw(Texturen.rahmen, buttonPos[i], null, Color.White * Transparenz, 0f,
                                Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        }
                        else if (selected == i + scrolled * length)
                        {
                            _Zeichenfläche.Draw(Texturen.LeeresFeld,
                                buttonPos[i], null, Color.Red * Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            _Zeichenfläche.Draw(Texturen.waffenbilder[(int)munlist[i + scrolled * length - tre].X],
                                buttonPos[i], null, Color.Red * Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        }
                        else
                        {
                            _Zeichenfläche.Draw(Texturen.LeeresFeld,
                                buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            _Zeichenfläche.Draw(Texturen.waffenbilder[(int)munlist[i + scrolled * length - tre].X],
                                buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        }

                        Help.DrawString(_Zeichenfläche, Texturen.font2, munlist[i + scrolled * length - tre].Y.ToString(),
                            buttonPos[i] + aux, Color.Gold * Transparenz, Color.Black * Transparenz);
                    }
                    else //ist es ein upgrade?
                        if (i + scrolled * length < mun + upg + tre)
                        {
                            if (clickselected == i + scrolled * length)
                            {
                                _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White * Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                _Zeichenfläche.Draw(
                                    Rucksack.Upgrades[(int)upglist[i + scrolled * length - mun - tre].X].Effekt.Bild,
                                    buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                _Zeichenfläche.Draw(Texturen.rahmen, buttonPos[i], null, Color.White * Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            }
                            else if (selected == i + scrolled * length)
                            {
                                _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.Red * Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                _Zeichenfläche.Draw(
                                    Rucksack.Upgrades[(int)upglist[i + scrolled * length - mun - tre].X].Effekt.Bild,
                                    buttonPos[i], null, Color.Red * Transparenz, 0f, Vector2.Zero,
                                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            }
                            else
                            {
                                _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White * Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                _Zeichenfläche.Draw(
                                    Rucksack.Upgrades[(int)upglist[i + scrolled * length - mun - tre].X].Effekt.Bild,
                                    buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            }
                            Help.DrawString(_Zeichenfläche, Texturen.font2,
                                upglist[i + scrolled * length - mun - tre].Y.ToString(), buttonPos[i] + aux,
                                Color.Gold * Transparenz, Color.Black * Transparenz);
                        }
                        else //es ist konsumierbar
                        {
                            if (i + scrolled * length < mun + upg + kon + tre)
                            {
                                if (clickselected == i + scrolled * length)
                                {
                                    _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White * Transparenz,
                                        0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    _Zeichenfläche.Draw(
                                        Rucksack.Konsumierbares[(int)konlist[i + scrolled * length - mun - upg - tre].X]
                                            .Effekt.Bild, buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                        Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    _Zeichenfläche.Draw(Texturen.rahmen, buttonPos[i], null, Color.White * Transparenz, 0f,
                                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                }
                                else if (selected == i + scrolled * length)
                                {
                                    _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.Red * Transparenz, 0f,
                                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    _Zeichenfläche.Draw(
                                        Rucksack.Konsumierbares[(int)konlist[i + scrolled * length - mun - upg - tre].X]
                                            .Effekt.Bild, buttonPos[i], null, Color.Red * Transparenz, 0f, Vector2.Zero,
                                        Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                }
                                else
                                {
                                    _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White * Transparenz,
                                        0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    _Zeichenfläche.Draw(
                                        Rucksack.Konsumierbares[(int)konlist[i + scrolled * length - mun - upg - tre].X]
                                            .Effekt.Bild, buttonPos[i], null, Color.White * Transparenz, 0f, Vector2.Zero,
                                        Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                }

                                Help.DrawString(_Zeichenfläche, Texturen.font2,
                                    konlist[i + scrolled * length - mun - upg - tre].Y.ToString(), buttonPos[i] + aux,
                                    Color.Gold * Transparenz, Color.Black * Transparenz);
                            }
                        }
            }

            if (scrolled < maxscrolls)
            {
                if (selected == 241)
                {
                    _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[length * height + 1], null, Color.Red * Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    _Zeichenfläche.Draw(Texturen.nachUnten, buttonPos[length * height + 1], null, Color.Red * Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
                else
                {
                    _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[length * height + 1], null, Color.White * Transparenz,
                        0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    _Zeichenfläche.Draw(Texturen.nachUnten, buttonPos[length * height + 1], null, Color.White * Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
            }
            else
            {
                _Zeichenfläche.Draw(Texturen.LeeresFeld, buttonPos[length * height + 1], null, Color.Silver * 0.4f * Transparenz,
                    0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                _Zeichenfläche.Draw(Texturen.nachUnten, buttonPos[length * height + 1], null, Color.Silver * 0.4f * Transparenz,
                    0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
            }
        }

        /// <summary>
        ///     bearbeitet Mauseingaben für das Menü
        /// </summary>
        /// <param name="mouseState">der aktuelle Mausstatus</param>
        /// <param name="clickselected">enthält einen Wert für das ausgewählte Item (ref)</param>
        /// <param name="Rucksack">das geöffnete Inventar</param>
        /// <returns>gibt zuruck, ob ein Item gewählt wurde (0==nein, 1==item gewählt, 2==scrolled)</returns>
        public int mouseKeys(MouseState mouseState, ref int clickselected, Inventar Rucksack)
        {
            if (clickselected > 250) selected = 255;
            int tre = Rucksack.GibTreibstoffFächer();
            int mun = Rucksack.GibMunitionsFächer();
            int upg = Rucksack.GibUpgradeFächer();
            int kon = Rucksack.GibKonsumierbareFächer();
            int maxscrolls = ((mun + upg + kon + tre) - length) / (length);

            if (buttons[length * height].Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                ContainmentType.Contains)
            {
                selected = 240;
                // selected = (byte)(length * height);
                if (mouseState.LeftButton != Help.GetMouseState().LeftButton &&
                    Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    if (scrolled > 0)
                    {
                        scrolled--;
                        return 2;
                    }
            }
            if (buttons[length * height + 1].Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                ContainmentType.Contains)
            {
                selected = 241;
                //selected = (byte)(length * height + 1);
                if (mouseState.LeftButton != Help.GetMouseState().LeftButton &&
                    Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    if (scrolled < maxscrolls)
                    {
                        scrolled++;
                        return 2;
                    }
            }

            for (int i = 0; i < length * height; i++)
                if (buttons[i].Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                    ContainmentType.Contains)
                {
                    //selected = i;
                    selected = i + scrolled * length;
                    if (mouseState.LeftButton != Help.GetMouseState().LeftButton &&
                        Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    {
                        if (clickselected == (i + scrolled * length))
                        {
                        }
                        else
                            clickselected = (i + scrolled * length);
                        return 1;
                    }
                }

            return 0;
        }

        /// <summary>
        ///     erzeugt die Buttons
        /// </summary>
        private void setButtons()
        {
            float dist = 5 / 4;
            buttonPos = new Vector2[length * height + 2];
            buttons = new BoundingBox[length * height + 2];
            {
                for (byte i = 0; i < height; i++)
                    for (byte j = 0; j < length; j++)
                    {
                        buttonPos[i * length + j] = ownPos +
                                                  new Vector2(
                                                      (Texturen.LeeresFeld.Width * Optimierung.Skalierung(0.25f) + +dist) *
                                                      j,
                                                      (Texturen.LeeresFeld.Height * Optimierung.Skalierung(0.25f) + +dist) *
                                                      i);
                        buttons[i * length + j] = new BoundingBox(new Vector3(buttonPos[i * length + j], 0),
                            new Vector3(
                                buttonPos[i * length + j] +
                                new Vector2(Texturen.LeeresFeld.Width, Texturen.LeeresFeld.Height) *
                                Optimierung.Skalierung(0.25f), 0));
                    }
                buttonPos[length * height] = ownPos +
                                           new Vector2(
                                               (Texturen.LeeresFeld.Width * Optimierung.Skalierung(0.25f) + +dist) * length,
                                               0);
                buttons[length * height] = new BoundingBox(new Vector3(buttonPos[height * length], 0),
                    new Vector3(
                        buttonPos[height * length] +
                        new Vector2(Texturen.nachOben.Width, Texturen.nachOben.Height) * Optimierung.Skalierung(0.25f), 0));
                buttonPos[length * height + 1] = buttonPos[length * height] +
                                               new Vector2(0,
                                                   (Texturen.LeeresFeld.Height * Optimierung.Skalierung(0.25f) + +dist) *
                                                   (height - 1));
                buttons[length * height + 1] = new BoundingBox(new Vector3(buttonPos[length * height + 1], 0),
                    new Vector3(
                        buttonPos[length * height + 1] +
                        new Vector2(Texturen.nachOben.Width, Texturen.nachOben.Height) * Optimierung.Skalierung(0.25f), 0));
                // maxscrolls = (byte)((maxAnz - length * height) / length);
            }
        }

        #endregion Methods
    }
}