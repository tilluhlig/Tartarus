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
    ///     Class Backpack
    /// </summary>
    public class Backpack
    {
        #region Fields

        /// <summary>
        ///     The max anz
        /// </summary>
        public byte maxAnz;

        /// <summary>
        ///     The selected
        /// </summary>
        public int selected = 255;

        /// <summary>
        ///     The aux
        /// </summary>
        private readonly Vector2 aux = new Vector2(10, 5);

        /// <summary>
        ///     The height
        /// </summary>
        private readonly byte height = 1;

        /// <summary>
        ///     The length
        /// </summary>
        private readonly byte length = 2;

        /// <summary>
        ///     The own pos
        /// </summary>
        private readonly Vector2 ownPos = new Vector2(0, 0);

        /// <summary>
        ///     The vertical
        /// </summary>
        private readonly bool vertical;

        /// <summary>
        ///     The button pos
        /// </summary>
        private Vector2[] buttonPos = new Vector2[0];

        /// <summary>
        ///     The buttons
        /// </summary>
        private BoundingBox[] buttons = new BoundingBox[0];

        /// <summary>
        ///     The maxscrolls
        /// </summary>
        private int maxscrolls;

        /// <summary>
        ///     The scrolled
        /// </summary>
        private int scrolled;

        /// <summary>
        ///     The scrollvalue
        /// </summary>
        private byte scrollvalue;

        #endregion Fields

        #region Constructors

        //bei allen konstruktoren: maxanz ist vielfaches von length
        //standardrucksack
        /// <summary>
        ///     Initializes a new instance of the <see cref="Backpack" /> class.
        /// </summary>
        /// <param name="pos">The pos.</param>
        /// <param name="maxAnz">The max anz.</param>
        /// <param name="length">The length.</param>
        /// <param name="height">The height.</param>
        public Backpack(Vector2 pos, byte maxAnz, byte length, byte height)
        {
            ownPos = pos;
            this.length = length;
            this.height = height;
            this.maxAnz = maxAnz;
            if (height > length)
            {
                vertical = true;
                scrollvalue = height;
            }
            else scrollvalue = length;
            setButtons();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="Rucksack">The rucksack.</param>
        /// <param name="clickselected">The clickselected.</param>
        /// <param name="Transparenz">The transparenz.</param>
        public void Draw(SpriteBatch spriteBatch, Inventar Rucksack, ref int clickselected, float Transparenz)
        {
            if (Rucksack == null) return;

            if (scrolled > 0)
            {
                if (selected == 240)
                {
                    spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[length*height], null, Color.Red*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    spriteBatch.Draw(Texturen.nachOben, buttonPos[length*height], null, Color.Red*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[length*height], null, Color.White*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    spriteBatch.Draw(Texturen.nachOben, buttonPos[length*height], null, Color.White*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
            }
            else
            {
                spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[length * height], null, Color.Silver * 0.4f * Transparenz, 0f,
                 Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                spriteBatch.Draw(Texturen.nachOben, buttonPos[length*height], null, Color.Silver*0.4f*Transparenz, 0f,
                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
            }

            int tre = Rucksack.GibTreibstoffFächer();
            int mun = Rucksack.GibMunitionsFächer();
            int upg = Rucksack.GibUpgradeFächer();
            int kon = Rucksack.GibKonsumierbareFächer();

            List<Vector2> munlist = Rucksack.GibMunitionsliste();
            List<Vector2> upglist = Rucksack.GibtListeUpgrades();
            List<Vector2> konlist = Rucksack.GibListeKonsumierbares();

            maxscrolls = ((mun + upg + kon + tre) - length)/(length);

            for (byte i = 0; i < height*length; i++)
            {
                // ist es treibstoff?
                if (i + scrolled*length < tre)
                {
                    if (clickselected == i + scrolled*length)
                    {
                        spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White*Transparenz, 0f,
                            Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        spriteBatch.Draw(Texturen.fuel, buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        spriteBatch.Draw(Texturen.rahmen, buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    }
                    else if (selected == i + scrolled*length)
                    {
                        spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.Red*Transparenz, 0f,
                            Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        spriteBatch.Draw(Texturen.fuel, buttonPos[i], null, Color.Red*Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White*Transparenz, 0f,
                            Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        spriteBatch.Draw(Texturen.fuel, buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                            Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    }

                    Help.DrawString(spriteBatch, Texturen.font2,
                        Math.Floor(Rucksack.GibTreibstoff() < 0 ? 0 : Rucksack.GibTreibstoff()).ToString(),
                        buttonPos[i] + aux, Color.Gold*Transparenz, Color.Black*Transparenz);
                }
                else // ist es munition?
                    if (i + scrolled*length < mun + tre)
                    {
                        if (clickselected == i + scrolled*length)
                        {
                            spriteBatch.Draw(Texturen.LeeresFeld,
                                buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);

                            spriteBatch.Draw(Texturen.waffenbilder[(int) munlist[i + scrolled*length - tre].X],
                                buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            spriteBatch.Draw(Texturen.rahmen, buttonPos[i], null, Color.White*Transparenz, 0f,
                                Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        }
                        else if (selected == i + scrolled*length)
                        {
                            spriteBatch.Draw(Texturen.LeeresFeld,
                                buttonPos[i], null, Color.Red*Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            spriteBatch.Draw(Texturen.waffenbilder[(int) munlist[i + scrolled*length - tre].X],
                                buttonPos[i], null, Color.Red*Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        }
                        else
                        {
                            spriteBatch.Draw(Texturen.LeeresFeld,
                                buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            spriteBatch.Draw(Texturen.waffenbilder[(int) munlist[i + scrolled*length - tre].X],
                                buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                        }

                        Help.DrawString(spriteBatch, Texturen.font2, munlist[i + scrolled*length - tre].Y.ToString(),
                            buttonPos[i] + aux, Color.Gold*Transparenz, Color.Black*Transparenz);
                    }
                    else //ist es ein upgrade?
                        if (i + scrolled*length < mun + upg + tre)
                        {
                            if (clickselected == i + scrolled*length)
                            {
                                spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White*Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                spriteBatch.Draw(
                                    Rucksack.Upgrades[(int) upglist[i + scrolled*length - mun - tre].X].Effekt.Bild,
                                    buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                spriteBatch.Draw(Texturen.rahmen, buttonPos[i], null, Color.White*Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            }
                            else if (selected == i + scrolled*length)
                            {
                                spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.Red*Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                spriteBatch.Draw(
                                    Rucksack.Upgrades[(int) upglist[i + scrolled*length - mun - tre].X].Effekt.Bild,
                                    buttonPos[i], null, Color.Red*Transparenz, 0f, Vector2.Zero,
                                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            }
                            else
                            {
                                spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White*Transparenz, 0f,
                                    Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                spriteBatch.Draw(
                                    Rucksack.Upgrades[(int) upglist[i + scrolled*length - mun - tre].X].Effekt.Bild,
                                    buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                            }
                            Help.DrawString(spriteBatch, Texturen.font2,
                                upglist[i + scrolled*length - mun - tre].Y.ToString(), buttonPos[i] + aux,
                                Color.Gold*Transparenz, Color.Black*Transparenz);
                        }
                        else //es ist konsumierbar
                        {
                            if (i + scrolled*length < mun + upg + kon + tre)
                            {
                                if (clickselected == i + scrolled*length)
                                {
                                    spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White*Transparenz,
                                        0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    spriteBatch.Draw(
                                        Rucksack.Konsumierbares[(int) konlist[i + scrolled*length - mun - upg - tre].X]
                                            .Effekt.Bild, buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                        Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    spriteBatch.Draw(Texturen.rahmen, buttonPos[i], null, Color.White*Transparenz, 0f,
                                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                }
                                else if (selected == i + scrolled*length)
                                {
                                    spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.Red*Transparenz, 0f,
                                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    spriteBatch.Draw(
                                        Rucksack.Konsumierbares[(int) konlist[i + scrolled*length - mun - upg - tre].X]
                                            .Effekt.Bild, buttonPos[i], null, Color.Red*Transparenz, 0f, Vector2.Zero,
                                        Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                }
                                else
                                {
                                    spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[i], null, Color.White*Transparenz,
                                        0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                    spriteBatch.Draw(
                                        Rucksack.Konsumierbares[(int) konlist[i + scrolled*length - mun - upg - tre].X]
                                            .Effekt.Bild, buttonPos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                                        Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                                }

                                Help.DrawString(spriteBatch, Texturen.font2,
                                    konlist[i + scrolled*length - mun - upg - tre].Y.ToString(), buttonPos[i] + aux,
                                    Color.Gold*Transparenz, Color.Black*Transparenz);
                            }
                        }
            }

            if (scrolled < maxscrolls)
            {
                if (selected == 241)
                {
                    spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[length*height + 1], null, Color.Red*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    spriteBatch.Draw(Texturen.nachUnten, buttonPos[length*height + 1], null, Color.Red*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[length*height + 1], null, Color.White*Transparenz,
                        0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                    spriteBatch.Draw(Texturen.nachUnten, buttonPos[length*height + 1], null, Color.White*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                }
            }
            else
            {
                spriteBatch.Draw(Texturen.LeeresFeld, buttonPos[length * height + 1], null, Color.Silver * 0.4f * Transparenz,
                    0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
            spriteBatch.Draw(Texturen.nachUnten, buttonPos[length*height + 1], null, Color.Silver*0.4f*Transparenz,
                0f, Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
        }
    }

        //gibt zuruck, welches item gewahlt wurde
        /// <summary>
        ///     Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="clickselected">The clickselected.</param>
        /// <param name="Rucksack">The rucksack.</param>
        /// <returns>System.Int32.</returns>
        public int mouseKeys(MouseState mouseState, ref int clickselected, Inventar Rucksack)
        {
            if (clickselected > 250) selected = 255;
            int tre = Rucksack.GibTreibstoffFächer();
            int mun = Rucksack.GibMunitionsFächer();
            int upg = Rucksack.GibUpgradeFächer();
            int kon = Rucksack.GibKonsumierbareFächer();
            maxscrolls = ((mun + upg + kon + tre) - length) / (length);

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
        ///     Sets the buttons.
        /// </summary>
        private void setButtons()
        {
            float dist = 5 / 4;
            buttonPos = new Vector2[length * height + 2];
            buttons = new BoundingBox[length * height + 2];
            if (vertical)
            {
                for (byte i = 0; i < length; i++)
                    for (byte j = 0; j < height; j++)
                    {
                        buttonPos[i * height + j] = ownPos +
                                                  new Vector2(
                                                      (Texturen.LeeresFeld.Width * Optimierung.Skalierung(0.25f)) * i,
                                                      (Texturen.LeeresFeld.Height * Optimierung.Skalierung(0.25f) + +dist) *
                                                      j);
                        buttons[i * height + j] = new BoundingBox(new Vector3(buttonPos[i * height + j], 0),
                            new Vector3(
                                buttonPos[i * height + j] +
                                new Vector2(Texturen.LeeresFeld.Width * Optimierung.Skalierung(0.25f),
                                    Texturen.LeeresFeld.Height * Optimierung.Skalierung(0.25f)), 0));
                    }
                buttonPos[length * height] = ownPos +
                                           new Vector2(0,
                                               (Texturen.LeeresFeld.Height * Optimierung.Skalierung(0.25f) + dist) * height);
                buttonPos[length * height + 1] = buttonPos[length * height] +
                                               new Vector2(
                                                   (Texturen.LeeresFeld.Width * Optimierung.Skalierung(0.25f) + +dist) *
                                                   (length - 1), 0);
                buttons[length * height] = new BoundingBox(new Vector3(buttonPos[height * length], 0),
                    new Vector3(
                        buttonPos[height * length] +
                        new Vector2(Texturen.nachOben.Width * Optimierung.Skalierung(0.25f),
                            Texturen.nachOben.Height * Optimierung.Skalierung(0.25f)), 0));
                buttons[length * height + 1] = new BoundingBox(new Vector3(buttonPos[length * height + 1], 0),
                    new Vector3(
                        buttonPos[length * height + 1] +
                        new Vector2(Texturen.nachOben.Width * Optimierung.Skalierung(0.25f),
                            Texturen.nachOben.Height * Optimierung.Skalierung(0.25f)), 0));
                // maxscrolls = (byte)((maxAnz - length * height) / height);
            }
            else
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

        //gibt zuruck, ob das item reingelegt werden konnte
    }
}