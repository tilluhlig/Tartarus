using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///     diese Klasse dient dem Erstellen und Handhaben von Textlisten (siehe Serialisierung)
    /// </summary>
    public static class TextLaden
    {
        #region Methods

        /// <summary>
        ///     erzeugt ein Dictionary aus einer Textliste, dabei werden Unterobjekte ignoriert
        /// </summary>
        /// <param name="Text">die Textliste</param>
        /// <returns>ein Dictionary mit den Paaren</returns>
        public static Dictionary<String, String> CreateDictionary(List<String> Text)
        {
            var Liste = new Dictionary<String, String>();
            int found = 0;
            for (int i = 0; i < Text.Count; i++)
            {
                if (Text[i].Length >= 2)
                {
                    if (Text[i].Substring(0, 2) == "[/")
                    {
                        found--;
                        continue;
                    }
                    if (Text[i].Substring(0, 1) == "[")
                    {
                        found++;
                    }
                }

                if (found == 0)
                {
                    String[] temp = Text[i].Split('=');
                    if (temp.Length > 1)
                        try
                        {
                            Liste.Add(temp[0], temp[1]);
                        }
                        catch (Exception)
                        {
                        }

                    Text.RemoveAt(i);
                    i--;
                }
            }

            return Liste;
        }

        /// <summary>
        ///     Ermittelt ein Unterobjekt einer Textliste, mit einem bestimmten Namen und entfernt diesen aus der original Liste
        /// </summary>
        /// <param name="Text">die Textliste</param>
        /// <param name="Bereichsname">der Name des Bereichs/Unterobjekts</param>
        /// <returns>einen Teil der Textliste, welcher den benannten Bereich enthält</returns>
        public static List<String> ErmittleBereich(List<String> Text, String Bereichsname)
        {
            var Result = new List<String>();

            bool ZielGefunden = false;

            int found = 0;
            for (int i = 0; i < Text.Count; i++)
            {
                Text[i] = Text[i].TrimEnd('\n');

                if (Text[i].Length >= 2)
                {
                    if (Text[i].Substring(0, 2) == "[/")
                    {
                        found--;
                    }
                    else if (Text[i].Substring(0, 1) == "[")
                    {
                        found++;
                    }
                }

                if (found == 1 && Text[i] == "[" + Bereichsname + "]")
                {
                    ZielGefunden = true;
                    Text.RemoveAt(i);
                    i--;
                }
                else if (found == 0 && ZielGefunden && Text[i] == "[/" + Bereichsname + "]")
                {
                    Text.RemoveAt(i);
                    i--;
                    return Result;
                }
                else if (ZielGefunden)
                {
                    Result.Add(Text[i]);
                    Text.RemoveAt(i);
                    i--;
                }
            }

            return Result;
        }

        /// <summary>
        ///     Lädt eine Bool-Variable mit entsprechendem Namen
        /// </summary>
        /// <param name="Dict">das Dictionary, welches durchsucht werden soll</param>
        /// <param name="Name">der Name des Eintrages</param>
        /// <param name="Wert">ein default-Wert, wenn der Eintrag nicht existiert</param>
        /// <returns>den Wert des Eintrages oder den Default-Wert</returns>
        public static bool LadeBool(Dictionary<String, String> Dict, String Name, bool Wert)
        {
            if (Dict.ContainsKey(Name))
            {
                bool temp = Wert;
                try
                {
                    temp = Convert.ToBoolean(Dict[Name]);
                }
                catch (Exception)
                {
                }
                Dict.Remove(Name);
                return temp;
            }
            return Wert;
        }

        /// <summary>
        ///     Lädt eine Float-Variable mit entsprechendem Namen
        /// </summary>
        /// <param name="Dict">das Dictionary, welches durchsucht werden soll</param>
        /// <param name="Name">der Name des Eintrages</param>
        /// <param name="Wert">ein default-Wert, wenn der Eintrag nicht existiert</param>
        /// <returns>den Wert des Eintrages oder den Default-Wert</returns>
        public static float LadeFloat(Dictionary<String, String> Dict, String Name, float Wert)
        {
            if (Dict.ContainsKey(Name))
            {
                float temp = Wert;
                try
                {
                    temp = (float) Convert.ToDouble(Dict[Name]);
                }
                catch (Exception)
                {
                }

                Dict.Remove(Name);
                return temp;
            }
            return Wert;
        }

        /// <summary>
        ///     Lädt eine Int-Variable mit entsprechendem Namen
        /// </summary>
        /// <param name="Dict">das Dictionary, welches durchsucht werden soll</param>
        /// <param name="Name">der Name des Eintrages</param>
        /// <param name="Wert">ein default-Wert, wenn der Eintrag nicht existiert</param>
        /// <returns>den Wert des Eintrages oder den Default-Wert</returns>
        public static int LadeInt(Dictionary<String, String> Dict, String Name, int Wert)
        {
            if (Dict.ContainsKey(Name))
            {
                int temp = Wert;
                try
                {
                    temp = Convert.ToInt32(Dict[Name]);
                }
                catch (Exception)
                {
                }

                Dict.Remove(Name);
                return temp;
            }
            return Wert;
        }

        /// <summary>
        ///     Lädt eine String-Variable mit entsprechendem Namen
        /// </summary>
        /// <param name="Dict">das Dictionary, welches durchsucht werden soll</param>
        /// <param name="Name">der Name des Eintrages</param>
        /// <param name="Wert">ein default-Wert, wenn der Eintrag nicht existiert</param>
        /// <returns>den Wert des Eintrages oder den Default-Wert</returns>
        public static String LadeString(Dictionary<String, String> Dict, String Name, String Wert)
        {
            if (Dict.ContainsKey(Name))
            {
                String temp = Wert;
                try
                {
                    temp = Dict[Name];
                }
                catch (Exception)
                {
                }
                Dict.Remove(Name);
                return temp;
            }
            return Wert;
        }

        /// <summary>
        ///     Lädt eine Vector2-Variable mit entsprechendem Namen
        /// </summary>
        /// <param name="Dict">das Dictionary, welches durchsucht werden soll</param>
        /// <param name="Name">der Name des Eintrages</param>
        /// <param name="Wert">ein default-Wert, wenn der Eintrag nicht existiert</param>
        /// <returns>den Wert des Eintrages oder den Default-Wert</returns>
        public static Vector2 LadeVector2(Dictionary<String, String> Dict, String Name, Vector2 Wert)
        {
            if (Dict.ContainsKey(Name))
            {
                Vector2 temp = Wert;
                try
                {
                    String zwischen = Dict[Name];
                    zwischen = zwischen.TrimStart('{');
                    zwischen = zwischen.TrimEnd('}');
                    String[] zw = zwischen.Split(' ');
                    temp = new Vector2((float) Convert.ToDouble(zw[0].Substring(2, zw[0].Length - 2)),
                        (float) Convert.ToDouble(zw[1].Substring(2, zw[1].Length - 2)));
                    ;
                }
                catch (Exception)
                {
                }
                Dict.Remove(Name);
                return temp;
            }
            return Wert;
        }

        #endregion Methods
    }
}