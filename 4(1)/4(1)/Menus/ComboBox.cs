// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-17-2013
// ***********************************************************************
// <copyright file="ComboBox.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    /// Class ComboBox
    /// </summary>
    internal class ComboBox
    {
        #region Vars

        /// <summary>
        /// The current item
        /// </summary>
        public int currentItem;

        /// <summary>
        /// The is deployed
        /// </summary>
        public bool isDeployed;

        /// <summary>
        /// The own pos
        /// </summary>
        public Vector2 ownPos;

        /// <summary>
        /// All items
        /// </summary>
        private Texture2D allItems;

        /// <summary>
        /// The drawing color
        /// </summary>
        private Color DrawingColor;

        /// <summary>
        /// The font
        /// </summary>
        private SpriteFont font;

        /// <summary>
        /// The itembox
        /// </summary>
        private BoundingBox[] itembox;

        /// <summary>
        /// The itembox oben unten
        /// </summary>
        private BoundingBox[] itemboxObenUnten;

        /// <summary>
        /// The item names
        /// </summary>
        private string[] itemNames;

        /// <summary>
        /// The items pos
        /// </summary>
        private Vector2[] itemsPos;

        /// <summary>
        /// The klotzchen box
        /// </summary>
        private BoundingBox klotzchenBox;

        /// <summary>
        /// The klotzchen current pos
        /// </summary>
        private Vector2 klotzchenCurrentPos;

        /// <summary>
        /// The max klotzchen pos
        /// </summary>
        private float maxKlotzchenPos;

        /// <summary>
        /// The min klotzchen pos
        /// </summary>
        private float minKlotzchenPos;

        /// <summary>
        /// The oben unten pos
        /// </summary>
        private Vector2[] obenUntenPos;

        /// <summary>
        /// The scale
        /// </summary>
        private float scale;

        /// <summary>
        /// The selected
        /// </summary>
        private Color selected;

        /// <summary>
        /// The selected item
        /// </summary>
        private int selectedItem;

        /// <summary>
        /// The single item
        /// </summary>
        private Texture2D singleItem;

        /// <summary>
        /// The unselected
        /// </summary>
        private Color unselected;

        /// <summary>
        /// The upper item
        /// </summary>
        private int upperItem;

        /// <summary>
        /// The visible
        /// </summary>
        private bool visible = false;

        #endregion Vars

        /// <summary>
        /// The mouse pos
        /// </summary>
        private Vector3 MousePos = Vector3.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        /// <param name="itemNames">The item names.</param>
        /// <param name="selected">The selected.</param>
        /// <param name="unselected">The unselected.</param>
        /// <param name="font">The font.</param>
        /// <param name="ownPos">The own pos.</param>
        /// <param name="singleItem">The single item.</param>
        /// <param name="allItems">All items.</param>
        /// <param name="scale">The scale.</param>
        public ComboBox(string[] itemNames, Color selected, Color unselected, SpriteFont font, Vector2 ownPos,
            Texture2D singleItem, Texture2D allItems, float scale)
        {
            this.itemNames = itemNames;
            this.selected = selected;
            this.unselected = unselected;
            this.font = font;
            this.singleItem = singleItem;
            this.allItems = allItems;
            this.scale = scale;
            this.ownPos = ownPos;

            obenUntenPos = new Vector2[2];
            obenUntenPos[0] = ownPos + new Vector2(singleItem.Width * scale, 0);
            obenUntenPos[1] = obenUntenPos[0] + new Vector2(0, allItems.Height * scale - Texturen.nachOben.Height);
            minKlotzchenPos = ownPos.Y + Texturen.nachUnten.Height;
            maxKlotzchenPos = minKlotzchenPos + Texturen.Comboboxbalken.Height - Texturen.klotzchen.Height;
            klotzchenCurrentPos = new Vector2(obenUntenPos[0].X, obenUntenPos[0].Y + Texturen.nachOben.Height);
            itemboxObenUnten = new BoundingBox[2];
            for (int i = 0; i < 2; i++)
                itemboxObenUnten[i] = new BoundingBox(new Vector3(obenUntenPos[i], 0), new Vector3(obenUntenPos[i].X + Texturen.nachOben.Width, obenUntenPos[i].Y + Texturen.nachOben.Height, 0));

            itemsPos = new Vector2[itemNames.Length];
            //itemsPos[0] = (new Vector2(ownPos.X + 40f, ownPos.Y + singleItem.Height * scale / 2 - 13));

            for (int i = 0; i < itemNames.Length; i++)
            {
                itemsPos[i] = ownPos + new Vector2((singleItem.Width * scale - font.MeasureString(itemNames[i]).X) / 2, (singleItem.Height * scale - font.MeasureString(itemNames[i]).Y) / 2);
            }

            klotzchenBox = new BoundingBox(new Vector3(klotzchenCurrentPos, 0), new Vector3(klotzchenCurrentPos.X + Texturen.klotzchen.Width, klotzchenCurrentPos.Y + Texturen.klotzchen.Height, 0));
            itembox = new BoundingBox[4];
            for (int i = 0; i < 4; i++)
                itembox[i] = new BoundingBox(new Vector3(ownPos.X, ownPos.Y + singleItem.Height * scale * i, 0),
                    new Vector3(ownPos.X + scale * singleItem.Width, ownPos.Y + singleItem.Height * scale * (i + 1), 0));
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="itemEnabled">The item enabled.</param>
        public void Draw(SpriteBatch spriteBatch, bool[] itemEnabled)
        {
            if (!visible) return;
            if (isDeployed)
            {
                spriteBatch.Draw(allItems, ownPos, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
                if (upperItem + 3 < itemNames.Length)
                {
                    currentItem = upperItem + selectedItem;
                    for (int i = 0; i < 4; i++)
                    {
                        if (!itemEnabled[upperItem + i]) DrawingColor = Color.Gray;
                        else
                        {
                            if (i == selectedItem) DrawingColor = selected;
                            else DrawingColor = unselected;
                        }
                        spriteBatch.DrawString(font, itemNames[i + upperItem], itemsPos[upperItem + i] + new Vector2(0, singleItem.Height * scale * i), DrawingColor);
                    }
                }

                spriteBatch.Draw(Texturen.nachOben, obenUntenPos[0], Color.White);
                spriteBatch.Draw(Texturen.nachUnten, obenUntenPos[1], Color.White);
                spriteBatch.Draw(Texturen.Comboboxbalken, obenUntenPos[0] + new Vector2(0, Texturen.nachOben.Height), Color.White);
                spriteBatch.Draw(Texturen.klotzchen, klotzchenCurrentPos, Color.White);
            }
            else
            {
                spriteBatch.Draw(singleItem, ownPos, null, Color.White
                    , 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
                if (!itemEnabled[currentItem])
                    spriteBatch.DrawString(font, itemNames[currentItem], itemsPos[currentItem], Color.Gray);
                else
                    spriteBatch.DrawString(font, itemNames[currentItem], itemsPos[currentItem], unselected);
            }
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
        /// <param name="spriteBatch">The sprite batch.</param>
        public void MouseKeys(MouseState mouseState, SpriteBatch spriteBatch)
        {
            if (!visible) return;
            MousePos.X = mouseState.X;
            MousePos.Y = mouseState.Y;
            if (isDeployed)
            {
                if (mouseState != Help.GetMouseState())
                {
                    if (itemboxObenUnten[0].Contains(MousePos)
                        == ContainmentType.Contains && Help.GetMouseState().LeftButton == ButtonState.Pressed && upperItem > 0)
                    {
                        klotzchenCurrentPos.Y -= Texturen.Comboboxbalken.Height / (itemNames.Length - itembox.Length);
                        if (klotzchenCurrentPos.Y < minKlotzchenPos)
                            klotzchenCurrentPos.Y = minKlotzchenPos;
                        upperItem--;
                    }

                    if (itemboxObenUnten[1].Contains(MousePos)
                        == ContainmentType.Contains && Help.GetMouseState().LeftButton == ButtonState.Pressed && upperItem + 4 < itemNames.Count())
                    {
                        klotzchenCurrentPos.Y += Texturen.Comboboxbalken.Height / (itemNames.Length - itembox.Length);
                        if (klotzchenCurrentPos.Y > maxKlotzchenPos)
                            klotzchenCurrentPos.Y = maxKlotzchenPos;
                        upperItem++;
                    }

                    if (klotzchenBox.Contains(MousePos) == ContainmentType.Contains)
                    {
                        if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                        {
                            klotzchenCurrentPos.Y = Help.GetMouseState().Y;
                            if (klotzchenCurrentPos.Y > maxKlotzchenPos)
                                klotzchenCurrentPos.Y = maxKlotzchenPos;
                            if (klotzchenCurrentPos.Y < minKlotzchenPos)
                                klotzchenCurrentPos.Y = minKlotzchenPos;
                            klotzchenBox.Min.Y = klotzchenCurrentPos.Y;
                            klotzchenBox.Max.Y = klotzchenCurrentPos.Y + Texturen.klotzchen.Height;

                            upperItem = (int)(((itemNames.Count() - 4) * (klotzchenCurrentPos.Y - minKlotzchenPos)) / (maxKlotzchenPos - minKlotzchenPos));
                        }
                    }
                    for (int i = 0; i < itembox.Length; i++)
                    {
                        if (itembox[i].Contains(MousePos)
                            == ContainmentType.Contains)
                        {
                            selectedItem = i;
                            if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                                isDeployed = false;
                        }
                    }

                    if (Help.GetMouseState().RightButton == ButtonState.Pressed)
                        isDeployed = false;
                }
            }
            else
                if (itembox[0].Contains(MousePos)
                    == ContainmentType.Contains)
                    if (mouseState != Help.GetMouseState() && Help.GetMouseState().LeftButton == ButtonState.Pressed)
                        isDeployed = true;
            klotzchenBox.Min.Y = klotzchenCurrentPos.Y;
            klotzchenBox.Max.Y = klotzchenCurrentPos.Y + Texturen.klotzchen.Height;
        }

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
        }
    }
}