using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace _4_1_
{
    public static class TastaturDeutsch
    {
        #region Methods

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        #endregion Methods

        #region Fields

        private static readonly Keys[] Map =
        {
            Keys.OemPlus, Keys.OemQuestion, Keys.OemMinus, Keys.OemPeriod,
            Keys.OemComma, Keys.OemOpenBrackets, Keys.OemBackslash,Keys.Back,Keys.Space,Keys.D0,Keys.D1,Keys.D2,Keys.D3,Keys.D4,Keys.D5,Keys.D6,Keys.D7,Keys.D8,Keys.D9,Keys.Enter
        };

        private static readonly Keys[] Map2 =
        {
            Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3,
            Keys.NumPad4, Keys.NumPad5, Keys.NumPad6,Keys.NumPad7,Keys.NumPad8,Keys.NumPad9
        };

        private static readonly char[] Map2Char = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly char[] MapAltGrChar = { '~', (char)0, (char)0, (char)0, (char)0, '|', '\\', (char)0, (char)0, '}', (char)0, (char)0, (char)0, (char)0, (char)0, (char)0, '{', '[', ']', (char)0 };
        private static readonly char[] MapChar = { '+', '#', '-', '.', ',', '<', (char)0, (char)8, ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '\n' };
        private static readonly char[] MapShiftChar = { '*', '\'', '_', ':', ';', '>', '?', (char)8, ' ', '=', '!', '"', '§', '$', '%', '&', '/', '(', ')', '\n' };

        #endregion Fields

        #region Methods

        private static int[] down = new int[256];

        private static bool first = true;

        private static List<Del> Funktionen = new List<Del>();

        private static List<int> oldTreffer = new List<int>();

        private static int[] pressed = new int[256];

        private static int readed = 3;

        public delegate void Del(object sender, KeyPressEventArgs e);

        public static void Add(Del Funktion)
        {
            Funktionen.Add(Funktion);
        }

        public static void Clear()
        {
            Funktionen.Clear();
        }

        public static void OnKeyPress()
        {
            if (first)
            {
                for (int i = 0; i < pressed.Count(); i++)
                {
                    pressed[i] = 0;
                    down[i] = 0;
                }
                first = false;
            }

            for (int i = 0; i < oldTreffer.Count(); i++)
            {
                int KEY = (int)oldTreffer[i];
                if (pressed[KEY] > 0)
                {
                    pressed[KEY]--;
                    if (down[KEY] == 0)
                    {
                        down[KEY] = 30;
                    }
                    else if (down[KEY] > 1)
                    {
                        down[KEY]--;
                    }
                    else if (down[KEY] == 1)
                    {
                        pressed[KEY] = 0;
                    }
                }
                else
                {
                    pressed[KEY] = 0;
                    down[KEY] = 0;
                }
            }

            KeyboardState State = Keyboard.GetState();

            Keys[] temp = Keyboard.GetState().GetPressedKeys();
            if (temp.Count() == 0) return;

            bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            bool NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
            bool ScrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;

            int shift = (State.IsKeyDown(Keys.LeftShift) || State.IsKeyDown(Keys.RightShift) || (CapsLock && !(State.IsKeyDown(Keys.LeftShift) || State.IsKeyDown(Keys.RightShift)))) ? 1 : 0;
            int alt = (State.IsKeyDown(Keys.LeftAlt)) ? 1 : 0;
            int altGr = (State.IsKeyDown(Keys.RightAlt)) ? 1 : 0;
            List<int> treffer = new List<int>();

            for (int i = 0; i < temp.Count(); i++)
            {
                if ((int)temp[i] >= 65 && (int)temp[i] <= 90)
                {
                    if (pressed[(int)(temp[i] + (1 - shift) * 32)] == 0)
                    {
                        var Key = new KeyPressEventArgs((char)(temp[i] + (1 - shift) * 32));
                        AlleAufrufen(Key);
                    }

                    pressed[(int)(temp[i] + (1 - shift) * 32)] = readed;
                    treffer.Add((int)(temp[i] + (1 - shift) * 32));
                }
                else
                {
                    bool found = false;

                    if (!found)
                    {
                        for (int b = 0; b < Map.Count(); b++)
                            if (Map[b] == temp[i])
                            {
                                found = true;
                                if (shift == 0 && altGr == 0)
                                {
                                    if (MapChar[b] != 0)
                                    {
                                        if (pressed[(int)MapChar[b]] == 0)
                                        {
                                            var Key = new KeyPressEventArgs(MapChar[b]);
                                            AlleAufrufen(Key);
                                        }
                                        pressed[(int)MapChar[b]] = readed;
                                        treffer.Add((int)MapChar[b]);
                                    }
                                }
                                if (altGr == 1)
                                {
                                    if (MapAltGrChar[b] != 0)
                                    {
                                        if (pressed[(int)MapAltGrChar[b]] == 0)
                                        {
                                            var Key = new KeyPressEventArgs(MapAltGrChar[b]);
                                            AlleAufrufen(Key);
                                        }
                                        pressed[(int)MapAltGrChar[b]] = readed;
                                        treffer.Add((int)MapAltGrChar[b]);
                                    }
                                }
                                if (shift == 1)
                                {
                                    if (MapShiftChar[b] != 0)
                                    {
                                        if (pressed[(int)MapShiftChar[b]] == 0)
                                        {
                                            var Key = new KeyPressEventArgs(MapShiftChar[b]);
                                            AlleAufrufen(Key);
                                        }
                                        pressed[(int)MapShiftChar[b]] = readed;
                                        treffer.Add((int)MapShiftChar[b]);
                                    }
                                }
                            }
                    }

                    if (!found)
                    {
                        for (int b = 0; b < Map2.Count(); b++)
                            if (Map2[b] == temp[i])
                            {
                                found = true;
                                if (NumLock)
                                {
                                    if (Map2Char[b] != 0)
                                    {
                                        if (pressed[(int)Map2Char[b]] == 0)
                                        {
                                            var Key = new KeyPressEventArgs(Map2Char[b]);
                                            AlleAufrufen(Key);
                                        }
                                        pressed[(int)Map2Char[b]] = readed;
                                        treffer.Add((int)Map2Char[b]);
                                    }
                                }
                            }
                    }
                }
            }

            for (int i = 0; i < treffer.Count(); i++)
            {
                int KEY = (int)treffer[i];
                if (pressed[KEY] > 0)
                {
                    pressed[KEY]--;
                    if (down[KEY] == 0)
                    {
                        down[KEY] = 30;
                    }
                    else if (down[KEY] > 1)
                    {
                        down[KEY]--;
                    }
                    else if (down[KEY] == 1)
                    {
                        pressed[KEY] = 0;
                    }
                }
                else
                {
                    pressed[KEY] = 0;
                    down[KEY] = 0;
                }
            }
            oldTreffer = treffer;
        }

        private static void AlleAufrufen(KeyPressEventArgs e)
        {
            for (int i = 0; i < Funktionen.Count; i++)
                Funktionen[i](null, e);
        }

        #endregion Methods
    }
}