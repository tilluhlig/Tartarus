using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace _4_1_
{
    public static class TastaturDeutsch
    {
        #region Fields

        private static readonly Keys[] Map =
        {
            Keys.OemPlus, Keys.OemQuestion, Keys.OemMinus, Keys.OemPeriod,
            Keys.OemComma, Keys.OemOpenBrackets, Keys.OemBackslash,Keys.Back,Keys.Space,Keys.D0,Keys.D1,Keys.D2,Keys.D3,Keys.D4,Keys.D5,Keys.D6,Keys.D7,Keys.D8,Keys.D9,Keys.Enter
        };

        private static readonly char[] MapAltGrChar = { '~', (char)0, (char)0, (char)0, (char)0, '|', '\\', (char)0, (char)0, '}', (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, '{', '[', ']',(char)0 };
        private static readonly char[] MapChar = { '+', '#', '-', '.', ',', '<', (char)0,(char)8,' ','0','1','2','3','4','5','6','7','8','9','\n' };
        private static readonly char[] MapShiftChar = { '*', '\'', '_', ':', ';', '>', '?', (char)8, ' ', '=', '!', '"', '§', '$', '%', '&', '/', '(', ')', '\n' };

        #endregion Fields

        #region Methods

        public delegate void Del(object sender, KeyPressEventArgs e);
        private static List<Del> Funktionen = new List<Del>();

        public static void Clear()
        {
            Funktionen.Clear();
        }

        public static void Add(Del Funktion)
        {
            Funktionen.Add(Funktion);
        }

        private static int[] pressed = new int[256];
        private static int[] down = new int[256];
        private static bool first=true;
        private static int readed = 2;

        public static void OnKeyPress()
        {
            if (first)
            {
                for (int i = 0; i < pressed.Count(); i++)
                {
                    pressed[i] = 0;
                    down[i] = 0;
                }
                first=false;
            }

            KeyboardState State = Keyboard.GetState();

            Keys[] temp = Keyboard.GetState().GetPressedKeys();
            if (temp.Count() == 0) return;

            int shift = (State.IsKeyDown(Keys.LeftShift) || State.IsKeyDown(Keys.RightShift)) ? 1 : 0;
            int alt = (State.IsKeyDown(Keys.LeftAlt)) ? 1 : 0;
            int altGr = (State.IsKeyDown(Keys.RightAlt)) ? 1 : 0;

            for (int i = 0; i < temp.Count(); i++)
            {
                if ((int)temp[i] >= 65 && (int)temp[i] <= 90)
                {
                    if (pressed[(int) (temp[i] + (1 - shift)*32)] == 0)
                    {
                        var Key = new KeyPressEventArgs((char) (temp[i] + (1 - shift)*32));
                        AlleAufrufen(Key);
                    }
                    pressed[(int)(temp[i] + (1 - shift) * 32)] = readed;
                }
             
                else
                {
                    for (int b = 0; b < Map.Count(); b++)
                        if (Map[b] == temp[i])
                        {
                            if (shift == 0 && altGr == 0)
                            {
                                if (MapChar[b] != 0)
                                {
                                    if (pressed[(int) MapChar[b]] == 0)
                                    {
                                        var Key = new KeyPressEventArgs(MapChar[b]);
                                        AlleAufrufen(Key);
                                    }
                                    pressed[(int)MapChar[b]] = readed;
                                }
                            }
                            if (altGr == 1)
                            {
                                if (MapAltGrChar[b] != 0)
                                {
                                    if (pressed[(int) MapAltGrChar[b]] == 0)
                                    {
                                        var Key = new KeyPressEventArgs(MapAltGrChar[b]);
                                        AlleAufrufen(Key);
                                    }
                                    pressed[(int)MapAltGrChar[b]] = readed;
                                }
                            }
                            if (shift == 1)
                            {
                                if (MapShiftChar[b] != 0)
                                {
                                    if (pressed[(int) MapShiftChar[b]] == 0)
                                    {
                                        var Key = new KeyPressEventArgs(MapShiftChar[b]);
                                        AlleAufrufen(Key);
                                    }
                                    pressed[(int)MapShiftChar[b]] = readed;
                                }
                            }
                        }
                }
            }

            for (int i = 0; i < pressed.Count(); i++)
            {
                if (pressed[i] > 0)
                {
                    pressed[i]--;
                    if (down[i] == 0)
                    {
                        down[i] = 30;
                    } else if (down[i] > 1)
                    {
                        down[i]--;
                    } else if (down[i] == 1)
                    {
                        pressed[i] = 0;
                    }
                }
                else down[i] = 0;
            }
        }

        private static void AlleAufrufen(KeyPressEventArgs e)
        {
            for (int i = 0; i < Funktionen.Count; i++)
                Funktionen[i](null, e);
        }

        #endregion Methods
    }
}