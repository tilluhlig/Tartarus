// ***********************************************************************
// Assembly         : Hauptfenster
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-04-2013
// ***********************************************************************
// <copyright file="HTTP.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace HTTP
{
    /// <summary>
    ///     Class HTTP
    /// </summary>
    public static class HTTP
    {
        #region Fields

        /// <summary>
        ///     The gameid
        /// </summary>
        public static String gameid = "";

        /// <summary>
        ///     The name
        /// </summary>
        public static String name = "";

        /// <summary>
        ///     The ogames
        /// </summary>
        public static List<String> ogames = new List<String>();

        /// <summary>
        ///     The passwd
        /// </summary>
        public static String passwd = "";

        /// <summary>
        ///     The rand
        /// </summary>
        public static Random rand = new Random();

        /// <summary>
        ///     The sgames
        /// </summary>
        public static List<String> sgames = new List<String>();

        /// <summary>
        ///     The adresse
        /// </summary>
        private static String Adresse = "http://tillu.selfhost.me";

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Create_games the specified version.
        /// </summary>
        /// <param name="version">The version.</param>
        /// <returns>List{String}.</returns>
        public static List<String> create_game(String version)
        {
            return create_game(name, passwd, version);
        }

        /// <summary>
        ///     Create_games the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="version">The version.</param>
        /// <returns>List{String}.</returns>
        public static List<String> create_game(String name, String passwd, String version)
        {
            return Send7(name, passwd, version, "create_game");
        }

        /// <summary>
        ///     Create_players the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>List{String}.</returns>
        public static List<String> create_player(String email)
        {
            return create_player(name, passwd, email);
        }

        /// <summary>
        ///     Create_players the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="email">The email.</param>
        /// <returns>List{String}.</returns>
        public static List<String> create_player(String name, String passwd, String email)
        {
            return Send6(name, passwd, email, "create_player");
        }

        /// <summary>
        ///     Delete_games this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> delete_game()
        {
            return delete_game(name, passwd, gameid);
        }

        /// <summary>
        ///     Delete_games the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <returns>List{String}.</returns>
        public static List<String> delete_game(String name, String passwd, String gameid)
        {
            return Send3(name, passwd, gameid, "delete_game");
        }

        /// <summary>
        ///     Delete_players this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> delete_player()
        {
            return delete_player(name, passwd);
        }

        /// <summary>
        ///     Delete_players the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <returns>List{String}.</returns>
        public static List<String> delete_player(String name, String passwd)
        {
            return Send4(name, passwd, "delete_player");
        }

        /// <summary>
        ///     Dirs the specified text.
        /// </summary>
        /// <param name="Text">The text.</param>
        public static void Dir(String Text)
        {
            if (!Directory.Exists(Text))
                Directory.CreateDirectory(Text);
        }

        /// <summary>
        ///     Get_datas this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> get_data()
        {
            return get_data(name, passwd, gameid);
        }

        /// <summary>
        ///     Get_datas the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <returns>List{String}.</returns>
        public static List<String> get_data(String name, String passwd, String gameid)
        {
            return Send3(name, passwd, gameid, "get_data");
        }

        /// <summary>
        ///     Get_gameinfoes this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> get_gameinfo()
        {
            return get_gameinfo(name, passwd, gameid);
        }

        /// <summary>
        ///     Get_gameinfoes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <returns>List{String}.</returns>
        public static List<String> get_gameinfo(String name, String passwd, String gameid)
        {
            return Send3(name, passwd, gameid, "get_gameinfo");
        }

        /// <summary>
        ///     Get_gameses this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> get_games()
        {
            return get_games(name, passwd);
        }

        /// <summary>
        ///     Get_gameses the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <returns>List{String}.</returns>
        public static List<String> get_games(String name, String passwd)
        {
            return Send4(name, passwd, "get_games");
        }

        /// <summary>
        ///     Get_maps this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> get_map()
        {
            return get_map(name, passwd, gameid);
        }

        /// <summary>
        ///     Get_maps the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <returns>List{String}.</returns>
        public static List<String> get_map(String name, String passwd, String gameid)
        {
            return Send3(name, passwd, gameid, "get_map");
        }

        /// <summary>
        ///     Get_s the meldung.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>String.</returns>
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

        /// <summary>
        ///     Get_ogameses this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> get_ogames()
        {
            return get_ogames(name, passwd);
        }

        /// <summary>
        ///     Get_ogameses the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <returns>List{String}.</returns>
        public static List<String> get_ogames(String name, String passwd)
        {
            return Send4(name, passwd, "get_ogames");
        }

        /// <summary>
        ///     Get_sgameses this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> get_sgames()
        {
            return get_sgames(name, passwd);
        }

        /// <summary>
        ///     Get_sgameses the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <returns>List{String}.</returns>
        public static List<String> get_sgames(String name, String passwd)
        {
            return Send4(name, passwd, "get_sgames");
        }

        /// <summary>
        ///     Determines whether the specified list is failure.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if the specified list is failure; otherwise, <c>false</c>.</returns>
        public static bool IsFailure(List<String> list)
        {
            if (list == null) return true;
            if (list.Count < 2) return true;
            if (list[0] == "FEHLER") return true;
            if (list[1] == "FEHLER") return true;
            return false;
        }

        /// <summary>
        ///     Join_games this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> join_game()
        {
            return join_game(name, passwd, gameid);
        }

        /// <summary>
        ///     Join_games the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <returns>List{String}.</returns>
        public static List<String> join_game(String name, String passwd, String gameid)
        {
            return Send3(name, passwd, gameid, "join_game");
        }

        /// <summary>
        ///     Logins this instance.
        /// </summary>
        /// <returns>List{String}.</returns>
        public static List<String> login()
        {
            return login(name, passwd);
        }

        /// <summary>
        ///     Logins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <returns>List{String}.</returns>
        public static List<String> login(String name, String passwd)
        {
            return Send4(name, passwd, "login");
        }

        /// <summary>
        ///     Set_passwds the specified newpasswd.
        /// </summary>
        /// <param name="newpasswd">The newpasswd.</param>
        /// <returns>List{String}.</returns>
        public static List<String> set_passwd(String newpasswd)
        {
            return set_passwd(name, passwd, newpasswd);
        }

        /// <summary>
        ///     Set_passwds the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="newpasswd">The newpasswd.</param>
        /// <returns>List{String}.</returns>
        public static List<String> set_passwd(String name, String passwd, String newpasswd)
        {
            return Send2(name, passwd, newpasswd, "set_passwd");
        }

        /// <summary>
        ///     Sets the local.
        /// </summary>
        public static void SetLocal()
        {
            Adresse = "http://192.168.2.106";
        }

        /// <summary>
        ///     Sets the server.
        /// </summary>
        /// <param name="_Adresse">The _ adresse.</param>
        public static void SetServer(String _Adresse)
        {
            Adresse = _Adresse;
        }

        /// <summary>
        ///     Unsets the local.
        /// </summary>
        public static void UnsetLocal()
        {
            Adresse = "http://tillu.selfhost.me";
        }

        /// <summary>
        ///     Uploads the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="data">The data.</param>
        /// <param name="next">The next.</param>
        /// <returns>List{String}.</returns>
        public static List<String> upload(String map, String data, String next)
        {
            return upload(name, passwd, gameid, map, data, next);
        }

        /// <summary>
        ///     Uploads the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <param name="map">The map.</param>
        /// <param name="data">The data.</param>
        /// <param name="next">The next.</param>
        /// <returns>List{String}.</returns>
        public static List<String> upload(String name, String passwd, String gameid, String map, String data,
            String next)
        {
            return Send5(name, passwd, gameid, map, data, next, "upload");
        }

        /// <summary>
        ///     HTTPs the post request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="postParameters">The post parameters.</param>
        /// <returns>List{String}.</returns>
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

        /// <summary>
        ///     Sends the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="newpasswd">The newpasswd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <param name="map">The map.</param>
        /// <param name="data">The data.</param>
        /// <param name="Funktion">The funktion.</param>
        /// <param name="email">The email.</param>
        /// <param name="version">The version.</param>
        /// <param name="next">The next.</param>
        /// <returns>List{String}.</returns>
        private static List<String> Send(String name, String passwd, String newpasswd, String gameid, String map,
            String data, String Funktion, String email, String version, String next)
        {
            var list = new List<String>();
            var postParameters = new Dictionary<string, string>();

            postParameters.Add("rand", rand.Next(0, Int32.MaxValue).ToString());
            postParameters.Add("name", name);
            postParameters.Add("passwd", passwd);
            postParameters.Add("email", email);
            postParameters.Add("newpasswd", newpasswd);
            postParameters.Add("gameid", gameid);
            postParameters.Add("map", map);
            postParameters.Add("data", data);
            postParameters.Add("version", version);
            postParameters.Add("next", next);

            list = HttpPostRequest(Adresse + "/" + Funktion + ".php", postParameters);
            return list;
        }

        /// <summary>
        ///     Send2s the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="newpasswd">The newpasswd.</param>
        /// <param name="Funktion">The funktion.</param>
        /// <returns>List{String}.</returns>
        private static List<String> Send2(String name, String passwd, String newpasswd, String Funktion)
        {
            return Send(name, passwd, newpasswd, "", "", "", Funktion, "", "", "");
        }

        /// <summary>
        ///     Send3s the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <param name="Funktion">The funktion.</param>
        /// <returns>List{String}.</returns>
        private static List<String> Send3(String name, String passwd, String gameid, String Funktion)
        {
            return Send(name, passwd, "", gameid, "", "", Funktion, "", "", "");
        }

        /// <summary>
        ///     Send4s the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="Funktion">The funktion.</param>
        /// <returns>List{String}.</returns>
        private static List<String> Send4(String name, String passwd, String Funktion)
        {
            return Send(name, passwd, "", "", "", "", Funktion, "", "", "");
        }

        /// <summary>
        ///     Send5s the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="gameid">The gameid.</param>
        /// <param name="map">The map.</param>
        /// <param name="data">The data.</param>
        /// <param name="next">The next.</param>
        /// <param name="Funktion">The funktion.</param>
        /// <returns>List{String}.</returns>
        private static List<String> Send5(String name, String passwd, String gameid, String map, String data,
            String next, String Funktion)
        {
            return Send(name, passwd, "", gameid, map, data, Funktion, "", "", next);
        }

        /// <summary>
        ///     Send6s the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="email">The email.</param>
        /// <param name="Funktion">The funktion.</param>
        /// <returns>List{String}.</returns>
        private static List<String> Send6(String name, String passwd, String email, String Funktion)
        {
            return Send(name, passwd, "", "", "", "", Funktion, email, "", "");
        }

        /// <summary>
        ///     Send7s the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="passwd">The passwd.</param>
        /// <param name="version">The version.</param>
        /// <param name="Funktion">The funktion.</param>
        /// <returns>List{String}.</returns>
        private static List<String> Send7(String name, String passwd, String version, String Funktion)
        {
            return Send(name, passwd, "", "", "", "", Funktion, "", version, "");
        }

        #endregion Methods
    }
}