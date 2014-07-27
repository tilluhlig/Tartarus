// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-17-2013
// ***********************************************************************
// <copyright file="DesignHelperTanks.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     Class DesignHelperTanks
    /// </summary>
    public class DesignHelperTanks
    {
        #region Fields

        /// <summary>
        ///     The scale box
        /// </summary>
        private readonly Textfeld scaleBox = new Textfeld(Vector2.Zero, "Geben Sie eine Zahl zwischen 0 und 1 an");

        /// <summary>
        ///     The writer
        /// </summary>
        private readonly StreamWriter writer = new StreamWriter(@"C:\myfile.txt");

        /// <summary>
        ///     The textur
        /// </summary>
        public List<Texture2D> Textur = new List<Texture2D>();

        /// <summary>
        ///     The texturbox
        /// </summary>
        public List<BoundingBox> Texturbox = new List<BoundingBox>();

        /// <summary>
        ///     The advanced scaling
        /// </summary>
        private bool advancedScaling;

        /// <summary>
        ///     The aux pos
        /// </summary>
        public List<Vector2> auxPos = new List<Vector2>();

        /// <summary>
        ///     The focus set at
        /// </summary>
        public int focusSetAt = -1;

        /// <summary>
        ///     The is enabled
        /// </summary>
        public bool isEnabled = false;

        /// <summary>
        ///     The marked for scale
        /// </summary>
        public int markedForScale = 0;

        /// <summary>
        ///     The mouse origin
        /// </summary>
        public Vector2 mouseOrigin;

        /// <summary>
        ///     The names
        /// </summary>
        public List<string> names = new List<string>();

        /// <summary>
        ///     The pos
        /// </summary>
        public List<Vector2> pos = new List<Vector2>();

        /// <summary>
        ///     The pos depends from
        /// </summary>
        public List<int> posDependsFrom = new List<int>();

        /// <summary>
        ///     The scales
        /// </summary>
        public List<float> scales = new List<float>();

        /// <summary>
        ///     The temp pos
        /// </summary>
        public Vector2 tempPos = Vector2.Zero;

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = Textur.Count - 1; i > -1; i--)
            {
                if (i == focusSetAt)
                    spriteBatch.Draw(Textur[i], tempPos, null, Color.Red, 0f, Vector2.Zero, scales[i],
                        SpriteEffects.None, 0);
                else if (i == markedForScale)
                    spriteBatch.Draw(Textur[i], pos[i], null, Color.Green, 0f, Vector2.Zero, scales[i],
                        SpriteEffects.None, 0);
                else

                    spriteBatch.Draw(Textur[i], pos[i], null, Color.White, 0f, Vector2.Zero, scales[i],
                        SpriteEffects.None, 0);
            }
            if (advancedScaling)
                scaleBox.ZeichneTextfeld(spriteBatch);
        }

        /// <summary>
        ///     Keyboards the keys.
        /// </summary>
        /// <param name="keybstate">The keybstate.</param>
        public void KeyboardKeys(KeyboardState keybstate)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                if (advancedScaling)
                    advancedScaling = false;
                else
                    advancedScaling = true;
            if (advancedScaling)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    advancedScaling = false;
                    scales[markedForScale] = float.Parse(scaleBox.input);
                    UpdateBoundingBox(markedForScale);
                }
                //scaleBox.KeyboardKeys(keybstate);
            }
        }

        /// <summary>
        ///     Mouses the keys.
        /// </summary>
        public void MouseKeys()
        {
            if (focusSetAt != -1)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Released)
                {
                    pos[focusSetAt] += new Vector2(Help.GetMouseState().X, Help.GetMouseState().Y) - mouseOrigin;
                    UpdateBoundingBox(focusSetAt);
                    focusSetAt = -1;
                }
                else
                    tempPos = pos[focusSetAt] + new Vector2(Help.GetMouseState().X, Help.GetMouseState().Y) -
                              mouseOrigin;
            }
            else
            {
                for (int i = 0; i < Textur.Count; i++)
                {
                    if (Texturbox[i].Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                        ContainmentType.Contains
                        && Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    {
                        mouseOrigin = new Vector2(Help.GetMouseState().X, Help.GetMouseState().Y);
                        focusSetAt = i;
                        break;
                    }
                }
            }
            if (advancedScaling)
            {
                for (int i = 0; i < Textur.Count; i++)
                {
                    if (Texturbox[i].Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                        ContainmentType.Contains
                        && Help.GetMouseState().RightButton == ButtonState.Pressed)
                        markedForScale = i;
                }
                scaleBox.mouseKeys();
            }
        }

        /// <summary>
        ///     Prints the and remove.
        /// </summary>
        public void PrintAndRemove()
        {
            for (int i = 0; i < names.Count; i++)
            {
                if (posDependsFrom[i] != -1)
                    writer.WriteLine(names[i] + " (" + pos[i].X + "," + pos[i].Y + ") depending on " +
                                     auxPos[posDependsFrom[i]].X + auxPos[posDependsFrom[i]].Y + " with Scale " +
                                     scales[0]);
                else
                    //writer.WriteLine(names[i] + " (" + pos[i].X + "," + pos[i].Y + ") with Scale " + scales[i]);
                    writer.WriteLine("test");
            }
            writer.Close();
            reset();
        }

        /// <summary>
        ///     Sets the new item.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="textur">The textur.</param>
        /// <param name="Box">The box.</param>
        /// <param name="position">The position.</param>
        /// <param name="dependsFrompos">The depends frompos.</param>
        /// <param name="scale">The scale.</param>
        public void setNewItem(string name, Texture2D textur, BoundingBox Box, Vector2 position, Vector2 dependsFrompos,
            float scale)
        {
            names.Add(name);
            Textur.Add(textur);
            Texturbox.Add(Box);
            pos.Add(position);
            if (dependsFrompos == new Vector2(-1, -1))
                posDependsFrom.Add(-1);
            else
            {
                if (auxPos.Contains(dependsFrompos))
                {
                    posDependsFrom.Add(auxPos.BinarySearch(dependsFrompos));
                }
                else
                {
                    auxPos.Add(dependsFrompos);
                    posDependsFrom.Add(auxPos.Count - 1);
                }
            }
            scales.Add(scale);
        }

        /// <summary>
        ///     Updates the bounding box.
        /// </summary>
        /// <param name="at">At.</param>
        public void UpdateBoundingBox(int at)
        {
            Texturbox[at] = new BoundingBox(new Vector3(pos[at], 0),
                new Vector3(pos[at].X + Textur[at].Width * scales[at], pos[at].Y + Textur[at].Height * scales[at], 0));
        }

        /// <summary>
        ///     Resets this instance.
        /// </summary>
        private void reset()
        {
            Textur = new List<Texture2D>();

            Texturbox = new List<BoundingBox>();

            pos = new List<Vector2>();

            posDependsFrom = new List<int>();

            scales = new List<float>();

            auxPos = new List<Vector2>();
        }

        #endregion Methods
    }
}