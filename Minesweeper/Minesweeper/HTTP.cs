using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace HTTP
{
    public static class HTTP
    {
        #region Fields

        public static Random rand = new Random();
        public static String Result = "";
        private static String Adresse = "http://tartarus.bplaced.net";

        #endregion Fields

        #region Methods

        public static List<String> Eingeben()
        {
            return Send("eingabe", Result);
        }

        public static List<String> Eingeben(String Text)
        {
            return Send("fehlerberichte", Text);
        }

        public static String Get_Meldung(List<String> list)
        {
            if (list == null) return "";
            if (list.Count < 2) return "";
            if (list[0] == "FEHLER") return list[1];

            if (list[1] != "FEHLER") return list[1];

            if (list.Count < 3) return "";
            if (list[1] == "FEHLER") return list[2];

            return "";
        }

        public static bool IsFailure(List<String> list)
        {
            if (list == null) return true;
            if (list.Count < 2) return true;
            if (list[0] == "FEHLER") return true;
            if (list[1] == "FEHLER") return true;
            return false;
        }

        public static void SetServer(String _Adresse)
        {
            Adresse = _Adresse;
        }

        private static List<String> HttpPostRequest(string url, Dictionary<string, string> postParameters)
        {
            string postData = "";
            var list = new List<String>();

            foreach (string key in postParameters.Keys)
            {
                postData += HttpUtility.UrlEncode(key) + "="
                            + HttpUtility.UrlEncode(postParameters[key]) + "&";
            }

            var myHttpWebRequest = (HttpWebRequest) WebRequest.Create(url);
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            try
            {
                Stream requestStream = myHttpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                var myHttpWebResponse = (HttpWebResponse) myHttpWebRequest.GetResponse();

                Stream responseStream = myHttpWebResponse.GetResponseStream();
                list.Add(myHttpWebResponse.StatusDescription);

                var myStreamReader = new StreamReader(responseStream, Encoding.Default);

                //string pageContent = myStreamReader.ReadToEnd();
                while (!myStreamReader.EndOfStream)
                    list.Add(myStreamReader.ReadLine());

                myStreamReader.Close();
                responseStream.Close();

                myHttpWebResponse.Close();
            }
            catch (Exception)
            {
                list.Add("FEHLER");
                list.Add("Keine Verbindung");
                return list;
            }

            return list;
        }

        private static List<String> Send(String Funktion, String _Result)
        {
            var list = new List<String>();
            var postParameters = new Dictionary<string, string>();

            postParameters.Add("rand", rand.Next(0, Int32.MaxValue).ToString());
            postParameters.Add("Result", _Result);

            list = HttpPostRequest(Adresse + "/" + Funktion + ".php", postParameters);
            return list;
        }

        #endregion Methods
    }
}