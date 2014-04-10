using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    public static class TextLaden
    {
        public static List<String> ErmittleBereich(List<String> Text, String Bereichsname)
        {
            List<String> Result = new List<String>();

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
                    else
                        if (Text[i].Substring(0, 1) == "[")
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
                else
                    if (found == 0 && ZielGefunden && Text[i] == "[/" + Bereichsname + "]")
                    {
                        Text.RemoveAt(i);
                        i--;
                        return Result;
                    }
                    else
                        if (ZielGefunden)
                        {
                            Result.Add(Text[i]);
                            Text.RemoveAt(i);
                            i--;
                        }
            }

            return Result;
        }

        public static Dictionary<String, String> CreateDictionary(List<String> Text)
        {
            Dictionary<String, String> Liste = new Dictionary<String, String>();
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
                    else
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

        public static float LadeFloat(Dictionary<String, String> Dict, String Name, float Wert)
        {
            if (Dict.ContainsKey(Name))
            {
                float temp = Wert;
                try
                {
                    temp = (float)Convert.ToDouble(Dict[Name]);
                }
                catch (Exception)
                {
                }

                Dict.Remove(Name);
                return temp;
            }
            return Wert;
        }

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
                    temp = new Vector2((float)Convert.ToDouble(zw[0].Substring(2, zw[0].Length - 2)), (float)Convert.ToDouble(zw[1].Substring(2, zw[1].Length - 2))); ;
                }
                catch (Exception)
                {
                }
                Dict.Remove(Name);
                return temp;
            }
            return Wert;
        }
    }
}