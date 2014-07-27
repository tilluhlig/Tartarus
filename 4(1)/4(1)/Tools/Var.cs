// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-22-2013
//
// Last Modified By : Till
// Last Modified On : 08-02-2013
// ***********************************************************************
// <copyright file="Var.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace _4_1_
{
    /// <summary>
    ///     Class Data
    /// </summary>
    public static class Data
    {
        #region Fields

        /// <summary>
        ///     The list
        /// </summary>
        public static List<String> list = new List<String>();

        #endregion Fields
    }

    /// <summary>
    ///     Class Var
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public unsafe class Var<T>
    {
        #region Fields

        /// <summary>
        ///     The ALLE
        /// </summary>
        public static List<Var<String>> ALLE = new List<Var<String>>();

        /// <summary>
        ///     The ALL e2
        /// </summary>
        public static List<Var<int>> ALLE2 = new List<Var<int>>();

        /// <summary>
        ///     The ALL e3
        /// </summary>
        public static List<Var<bool>> ALLE3 = new List<Var<bool>>();

        /// <summary>
        ///     The ALL e4
        /// </summary>
        public static List<Var<float>> ALLE4 = new List<Var<float>>();

        /// <summary>
        ///     The ALL e5
        /// </summary>
        public static List<Var<String[]>> ALLE5 = new List<Var<String[]>>();

        /// <summary>
        ///     The ALL e6
        /// </summary>
        public static List<Var<int[]>> ALLE6 = new List<Var<int[]>>();

        /// <summary>
        ///     The ALL e7
        /// </summary>
        public static List<Var<bool[]>> ALLE7 = new List<Var<bool[]>>();

        /// <summary>
        ///     The ALL e8
        /// </summary>
        public static List<Var<float[]>> ALLE8 = new List<Var<float[]>>();

        /// <summary>
        ///     The link B
        /// </summary>
        private readonly bool* LinkB;

        private readonly float* LinkF;

        /// <summary>
        ///     The link I
        /// </summary>
        private readonly int* LinkI;

        /// <summary>
        ///     Enthält einen eindeutigen Bezeichner der Variablen, wird
        ///     auch in den .conf Dateien zum Belegen der Variablen genutztsdg
        /// </summary>
        public String Name;

        /// <summary>
        ///     The wert
        /// </summary>
        public T Wert;

        /// <summary>
        ///     The old wert
        /// </summary>
        public T oldWert;

        #endregion Fields

        #region Constructors

        public Var(String _Name, T Default, ref float _Link)
        {
            Init(_Name, Default);

            fixed (float* a = &_Link)
            {
                LinkF = a;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Var{T}" /> class.
        /// </summary>
        /// <param name="_Name">Name of the _.</param>
        /// <param name="Default">The default.</param>
        /// <param name="_Link">if set to <c>true</c> [_ link].</param>
        public Var(String _Name, T Default, ref bool _Link)
        {
            Init(_Name, Default);

            fixed (bool* a = &_Link)
            {
                LinkB = a;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Var{T}" /> class.
        /// </summary>
        /// <param name="_Name">Name of the _.</param>
        /// <param name="Default">The default.</param>
        /// <param name="_Link">The _ link.</param>
        public Var(String _Name, T Default, ref int _Link)
        {
            Init(_Name, Default);

            fixed (int* a = &_Link)
            {
                LinkI = a;
            }
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Var{T}" /> class.
        /// </summary>
        /// <param name="_Name">Name of the _.</param>
        /// <param name="Default">The default.</param>
        public Var(String _Name, T Default)
        {
            Init(_Name, Default);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Adds the ALLE.
        /// </summary>
        /// <param name="Object">The object.</param>
        public static void AddALLE(Var<T> Object)
        {
            if (Object.GetType() == typeof(Var<Int32>))
            {
                ALLE2.Add((Var<int>)(object)Object);
            }
            else if (Object.GetType() == typeof(Var<bool>))
            {
                ALLE3.Add((Var<bool>)(object)Object);
            }
            else if (Object.GetType() == typeof(Var<String>))
            {
                ALLE.Add((Var<String>)(object)Object);
            }
            else if (Object.GetType() == typeof(Var<float>))
            {
                ALLE4.Add((Var<float>)(object)Object);
            }
            else if (Object.GetType() == typeof(Var<Int32[]>))
            {
                ALLE6.Add((Var<int[]>)(object)Object);
            }
            else if (Object.GetType() == typeof(Var<bool[]>))
            {
                ALLE7.Add((Var<bool[]>)(object)Object);
            }
            else if (Object.GetType() == typeof(Var<String[]>))
            {
                ALLE5.Add((Var<String[]>)(object)Object);
            }
            else if (Object.GetType() == typeof(Var<float[]>))
            {
                ALLE8.Add((Var<float[]>)(object)Object);
            }
        }

        /// <summary>
        ///     Determines whether the specified expression is numeric.
        /// </summary>
        /// <param name="Expression">The expression.</param>
        /// <returns><c>true</c> if the specified expression is numeric; otherwise, <c>false</c>.</returns>
        public static Boolean IsNumeric(Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal ||
                Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch
            {
            } // just dismiss errors but return false
            return false;
        }

        /// <summary>
        ///     Liest eine Konfigurationsdatei ein
        /// </summary>
        /// <param name="_Datei">Name der Datei</param>
        public static void Open(String _Datei)
        {
            Data.list.Clear();
            var Datei = new StreamReader(_Datei);
            for (; !Datei.EndOfStream; )
            {
                String q = Datei.ReadLine();
                if (q.Length >= 1)
                    if (q[0] != '/' && q[0] != '#') Data.list.Add(q);
            }
            Datei.Close();
        }

        /// <summary>
        ///     Sets from ALLE.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="_Wert">The _ wert.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetFromALLE(String Name, T _Wert)
        {
            if (_Wert == null) return false;
            if (_Wert.GetType() == typeof(Int32) || _Wert.GetType() == typeof(bool) ||
                _Wert.GetType() == typeof(String) || _Wert.GetType() == typeof(float))
            {
                for (int i = 0; i < ALLE.Count; i++)
                    if (ALLE[i].Name == Name)
                    {
                        ALLE[i].Wert = _Wert.ToString();
                        return true;
                    }

                for (int i = 0; i < ALLE2.Count; i++)
                    if (ALLE2[i].Name == Name)
                    {
                        if (IsNumeric(_Wert))
                            ALLE2[i].Wert = Convert.ToInt32(_Wert);
                        return true;
                    }

                for (int i = 0; i < ALLE4.Count; i++)
                    if (ALLE4[i].Name == Name)
                    {
                        if (IsNumeric(_Wert))
                            ALLE4[i].Wert = (float)Convert.ToDouble(_Wert);
                        return true;
                    }

                try
                {
                    bool ww = Convert.ToBoolean(_Wert);
                    for (int i = 0; i < ALLE3.Count; i++)
                        if (ALLE3[i].Name == Name)
                        {
                            ALLE3[i].Wert = ww;
                            return true;
                        }
                }
                catch (FormatException)
                {
                    // kein Bool
                }
            }

            return false;
        }

        /// <summary>
        ///     Sets from ALLE.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <param name="_Wert">The _ wert.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public static bool SetFromALLE(String Name, String _Wert)
        {
            if (Name.IndexOf("[") < 0)
            {
                for (int i = 0; i < ALLE.Count; i++)
                    if (ALLE[i].Name == Name)
                    {
                        ALLE[i].Wert = _Wert;
                        return true;
                    }

                for (int i = 0; i < ALLE2.Count; i++)
                    if (ALLE2[i].Name == Name)
                    {
                        if (IsNumeric(_Wert))
                            ALLE2[i].Wert = Convert.ToInt32(_Wert);
                        return true;
                    }

                for (int i = 0; i < ALLE4.Count; i++)
                    if (ALLE4[i].Name == Name)
                    {
                        if (IsNumeric(_Wert))
                            ALLE4[i].Wert = (float)Convert.ToDouble(_Wert);
                        return true;
                    }

                try
                {
                    bool ww = Convert.ToBoolean(_Wert);
                    for (int i = 0; i < ALLE3.Count; i++)
                        if (ALLE3[i].Name == Name)
                        {
                            ALLE3[i].Wert = ww;
                            return true;
                        }
                }
                catch (FormatException)
                {
                    // kein Bool
                }
            }
            else
            {
                int id = Convert.ToInt32(Name.Split('[')[1].TrimEnd(']').Trim());
                Name = Name.Split('[')[0];
                for (int i = 0; i < ALLE5.Count; i++)
                    if (ALLE5[i].Name == Name)
                    {
                        ALLE5[i].Wert[id] = _Wert;
                        return true;
                    }

                for (int i = 0; i < ALLE6.Count; i++)
                    if (ALLE6[i].Name == Name)
                    {
                        ALLE6[i].Wert[id] = Convert.ToInt32(_Wert);
                        return true;
                    }

                for (int i = 0; i < ALLE8.Count; i++)
                    if (ALLE8[i].Name == Name)
                    {
                        ALLE8[i].Wert[id] = Convert.ToInt32(_Wert);
                        return true;
                    }

                try
                {
                    bool ww = Convert.ToBoolean(_Wert);
                    for (int i = 0; i < ALLE7.Count; i++)
                        if (ALLE7[i].Name == Name)
                        {
                            ALLE7[i].Wert[id] = ww;
                            return true;
                        }
                }
                catch (FormatException)
                {
                    // kein Bool
                }
            }

            return false;
        }

        /// <summary>
        ///     Loads this instance.
        /// </summary>
        public void Load()
        {
            // Reset
            SetFromALLE(Name, oldWert);

            // neu laden
            if (Wert.GetType() == typeof(Int32) || Wert.GetType() == typeof(bool) || Wert.GetType() == typeof(String) ||
                Wert.GetType() == typeof(float))
            {
                for (int i = 0; i < Data.list.Count; i++)
                {
                    String[] data = Data.list[i].Split('=');
                    if (data[0].Trim().ToUpper() == Name)
                    {
                        if (Wert.GetType() == typeof(Int32))
                        {
                            Wert = (T)(object)(Convert.ToInt32(data[1].Trim()));
                        }
                        else if (Wert.GetType() == typeof(Keys))
                        {
                            var qq = new KeysConverter();
                            Wert = (T)qq.ConvertFromString(data[1].Trim());
                            String q = Wert.ToString();
                        }
                        else if (Wert.GetType() == typeof(bool))
                        {
                            try
                            {
                                Wert = (T)(object)(Convert.ToBoolean(data[1].Trim()));
                            }
                            catch (FormatException)
                            {
                            }
                        }
                        else if (Wert.GetType() == typeof(String))
                        {
                            Wert = (T)(object)data[1].Trim();
                        }
                        break;
                    }
                }
            }
            else
            {
                if (Wert.GetType() == typeof(Int32[]))
                {
                    for (int i = 0; i < ((Int32[])(object)Wert).Count(); i++)
                    {
                        ((Int32[])(object)Wert)[i] = ((Int32[])(object)oldWert)[i];

                        String search = Name + "[" + i + "]";
                        for (int b = 0; b < Data.list.Count; b++)
                        {
                            String[] data = Data.list[b].Split('=');
                            if (data[0].Trim().ToUpper() == search)
                            {
                                ((Int32[])(object)Wert)[i] = (Convert.ToInt32(data[1].Trim()));
                            }
                        }
                    }
                }
                else if (Wert.GetType() == typeof(bool[]))
                {
                    for (int i = 0; i < ((bool[])(object)Wert).Count(); i++)
                    {
                        ((bool[])(object)Wert)[i] = ((bool[])(object)oldWert)[i];

                        String search = Name + "[" + i + "]";
                        for (int b = 0; b < Data.list.Count; b++)
                        {
                            String[] data = Data.list[b].Split('=');
                            if (data[0].Trim().ToUpper() == search)
                            {
                                ((bool[])(object)Wert)[i] = (Convert.ToBoolean(data[1].Trim()));
                            }
                        }
                    }
                }
                else if (Wert.GetType() == typeof(String[]))
                {
                    for (int i = 0; i < ((String[])(object)Wert).Count(); i++)
                    {
                        ((String[])(object)Wert)[i] = ((String[])(object)oldWert)[i];

                        String search = Name + "[" + i + "]";
                        for (int b = 0; b < Data.list.Count; b++)
                        {
                            String[] data = Data.list[b].Split('=');
                            if (data[0].Trim().ToUpper() == search)
                            {
                                ((String[])(object)Wert)[i] = data[1].Trim();
                            }
                        }
                    }
                }
                else if (Wert.GetType() == typeof(float[]))
                {
                    for (int i = 0; i < ((float[])(object)Wert).Count(); i++)
                    {
                        ((float[])(object)Wert)[i] = ((float[])(object)oldWert)[i];

                        String search = Name + "[" + i + "]";
                        for (int b = 0; b < Data.list.Count; b++)
                        {
                            String[] data = Data.list[b].Split('=');
                            if (data[0].Trim().ToUpper() == search)
                            {
                                ((float[])(object)Wert)[i] = (float)Convert.ToDouble(data[1].Trim());
                            }
                        }
                    }
                }
            }

            if (LinkB != null)
            {
                *LinkB = (bool)(object)Wert;
            }
            else if (LinkI != null)
            {
                *LinkI = (int)(object)Wert;
            }
            else if (LinkF != null)
            {
                *LinkF = (float)(object)Wert;
            }
        }

        /// <summary>
        ///     Speichert die Variable als Zuweisung = Wert
        /// </summary>
        public String Save()
        {
            for (int i = 0; i < ALLE.Count; i++)
                if (ALLE[i].Name == Name)
                {
                    return Name + " = " + Wert;
                }

            for (int i = 0; i < ALLE2.Count; i++)
                if (ALLE2[i].Name == Name)
                {
                    return Name + " = " + Wert;
                }

            for (int i = 0; i < ALLE3.Count; i++)
                if (ALLE3[i].Name == Name)
                {
                    return Name + " = " + Wert;
                }

            for (int i = 0; i < ALLE4.Count; i++)
                if (ALLE4[i].Name == Name)
                {
                    return Name + " = " + Wert;
                }

            for (int i = 0; i < ALLE5.Count; i++)
                if (ALLE5[i].Name == Name)
                {
                    String result = "";
                    for (int b = 0; b < ALLE5[i].Wert.Length; b++)
                        result += ALLE5[i].Name + "[" + b + "] = " + ALLE5[i].Wert[b] +
                                  (b < ALLE5[i].Wert.Length - 1 ? "\n" : "");
                    return result;
                }

            for (int i = 0; i < ALLE6.Count; i++)
                if (ALLE6[i].Name == Name)
                {
                    String result = "";
                    for (int b = 0; b < ALLE6[i].Wert.Length; b++)
                        result += ALLE6[i].Name + "[" + b + "] = " + ALLE6[i].Wert[b] +
                                  (b < ALLE6[i].Wert.Length - 1 ? "\n" : "");
                    return result;
                }

            for (int i = 0; i < ALLE7.Count; i++)
                if (ALLE7[i].Name == Name)
                {
                    String result = "";
                    for (int b = 0; b < ALLE7[i].Wert.Length; b++)
                        result += ALLE7[i].Name + "[" + b + "] = " + ALLE7[i].Wert[b] +
                                  (b < ALLE7[i].Wert.Length - 1 ? "\n" : "");
                    return result;
                }

            for (int i = 0; i < ALLE8.Count; i++)
                if (ALLE8[i].Name == Name)
                {
                    String result = "";
                    for (int b = 0; b < ALLE8[i].Wert.Length; b++)
                        result += ALLE8[i].Name + "[" + b + "] = " + ALLE8[i].Wert[b] +
                                  (b < ALLE8[i].Wert.Length - 1 ? "\n" : "");
                    return result;
                }

            return "";

            /*// neu laden
            if (Wert.GetType() == typeof(Int32) || Wert.GetType() == typeof(bool) || Wert.GetType() == typeof(String) || Wert.GetType() == typeof(float))
            {
                for (int i = 0; i < Data.list.Count; i++)
                {
                    String[] data = Data.list[i].Split('=');
                    if (data[0].Trim().ToUpper() == Name)
                    {
                        if (Wert.GetType() == typeof(Int32))
                        {
                            Wert = (T)(object)(Convert.ToInt32(data[1].Trim()));
                        }
                        else
                            if (Wert.GetType() == typeof(Keys))
                            {
                                System.Windows.Forms.KeysConverter qq = new System.Windows.Forms.KeysConverter();
                                Wert = (T)(object)(qq.ConvertFromString(data[1].Trim()));
                                String q = Wert.ToString();
                            }
                            else
                                if (Wert.GetType() == typeof(bool))
                                {
                                    try
                                    {
                                        Wert = (T)(object)(Convert.ToBoolean(data[1].Trim()));
                                    }
                                    catch (FormatException) { }
                                }
                                else
                                    if (Wert.GetType() == typeof(String))
                                    {
                                        Wert = (T)(object)data[1].Trim();
                                    }
                        break;
                    }
                }
            }
            else
            {
                if (Wert.GetType() == typeof(Int32[]))
                {
                    for (int i = 0; i < ((Int32[])(object)Wert).Count(); i++)
                    {
                        ((Int32[])(object)Wert)[i] = ((Int32[])(object)oldWert)[i];

                        String search = Name + "[" + i.ToString() + "]";
                        for (int b = 0; b < Data.list.Count; b++)
                        {
                            String[] data = Data.list[b].Split('=');
                            if (data[0].Trim().ToUpper() == search)
                            {
                                ((Int32[])(object)Wert)[i] = (Convert.ToInt32(data[1].Trim()));
                            }
                        }
                    }
                }
                else
                    if (Wert.GetType() == typeof(bool[]))
                    {
                        for (int i = 0; i < ((bool[])(object)Wert).Count(); i++)
                        {
                            ((bool[])(object)Wert)[i] = ((bool[])(object)oldWert)[i];

                            String search = Name + "[" + i.ToString() + "]";
                            for (int b = 0; b < Data.list.Count; b++)
                            {
                                String[] data = Data.list[b].Split('=');
                                if (data[0].Trim().ToUpper() == search)
                                {
                                    ((bool[])(object)Wert)[i] = (Convert.ToBoolean(data[1].Trim()));
                                }
                            }
                        }
                    }
                    else
                        if (Wert.GetType() == typeof(String[]))
                        {
                            for (int i = 0; i < ((String[])(object)Wert).Count(); i++)
                            {
                                ((String[])(object)Wert)[i] = ((String[])(object)oldWert)[i];

                                String search = Name + "[" + i.ToString() + "]";
                                for (int b = 0; b < Data.list.Count; b++)
                                {
                                    String[] data = Data.list[b].Split('=');
                                    if (data[0].Trim().ToUpper() == search)
                                    {
                                        ((String[])(object)Wert)[i] = data[1].Trim();
                                    }
                                }
                            }
                        }
                        else
                            if (Wert.GetType() == typeof(float[]))
                            {
                                for (int i = 0; i < ((float[])(object)Wert).Count(); i++)
                                {
                                    ((float[])(object)Wert)[i] = ((float[])(object)oldWert)[i];

                                    String search = Name + "[" + i.ToString() + "]";
                                    for (int b = 0; b < Data.list.Count; b++)
                                    {
                                        String[] data = Data.list[b].Split('=');
                                        if (data[0].Trim().ToUpper() == search)
                                        {
                                            ((float[])(object)Wert)[i] = (float)Convert.ToDouble(data[1].Trim());
                                        }
                                    }
                                }
                            }
            }

            unsafe
            {
                if (LinkB != null)
                {
                    *LinkB = (bool)(object)Wert;
                }
                else
                    if (LinkI != null)
                    {
                        *LinkI = (int)(object)Wert;
                    }
                    else
                        if (LinkF != null)
                        {
                            *LinkF = (float)(object)Wert;
                        }
            }*/
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Var{T}" /> class.
        /// </summary>
        /// <param name="_Name">Name of the _.</param>
        /// <param name="Default">The default.</param>
        private void Init(String _Name, T Default)
        {
            Name = _Name;
            Wert = Default;
            if (Wert.GetType() == typeof(Int32) || Wert.GetType() == typeof(bool) || Wert.GetType() == typeof(String) ||
                Wert.GetType() == typeof(float))
            {
                oldWert = Default;
            }
            else
            {
                if (typeof(T) == typeof(Int32[]))
                {
                    oldWert = (T)(object)new Int32[((Int32[])(object)Wert).Count()];
                    for (int i = 0; i < ((Int32[])(object)Wert).Count(); i++)
                    {
                        ((Int32[])(object)oldWert)[i] = ((Int32[])(object)Wert)[i];
                    }
                }
                else if (typeof(T) == typeof(bool[]))
                {
                    oldWert = (T)(object)new bool[((bool[])(object)Wert).Count()];
                    for (int i = 0; i < ((bool[])(object)Wert).Count(); i++)
                    {
                        ((bool[])(object)oldWert)[i] = ((bool[])(object)Wert)[i];
                    }
                }
                else if (typeof(T) == typeof(float[]))
                {
                    oldWert = (T)(object)new float[((float[])(object)Wert).Count()];
                    for (int i = 0; i < ((float[])(object)Wert).Count(); i++)
                    {
                        ((float[])(object)oldWert)[i] = ((float[])(object)Wert)[i];
                    }
                }
                else if (typeof(T) == typeof(String[]))
                {
                    int anz = ((String[])(object)Wert).Count();
                    oldWert = (T)(object)new String[anz];
                    for (int i = 0; i < ((String[])(object)Wert).Count(); i++)
                    {
                        ((String[])(object)oldWert)[i] = new String(((String[])(object)Wert)[i].ToCharArray());
                    }
                }
            }

            AddALLE(this);
        }

        #endregion Methods

        /*public static object GetFromALLE(String Name)
        {
            for (int i = 0; i < ALLE.Count; i++)
                if (ALLE[i].Name == Name)
                {
                    return (object)ALLE[i];
                }

            for (int i = 0; i < ALLE2.Count; i++)
                if (ALLE2[i].Name == Name)
                {
                    return (object)ALLE2[i];
                }

            for (int i = 0; i < ALLE3.Count; i++)
                if (ALLE3[i].Name == Name)
                {
                    return (object)ALLE3[i];
                }

                    for (int i = 0; i < ALLE5.Count; i++)
                if (ALLE5[i].Name == Name)
                {
                    return (object)ALLE5[i];
                }

            for (int i = 0; i < ALLE6.Count; i++)
                if (ALLE6[i].Name == Name)
                {
                    return (object)ALLE6[i];
                }

            for (int i = 0; i < ALLE7.Count; i++)
                if (ALLE7[i].Name == Name)
                {
                    return (object)ALLE7[i];
                }

            return null;
        }
        */
    }
}