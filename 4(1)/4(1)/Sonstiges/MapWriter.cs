// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-31-2013
// ***********************************************************************
// <copyright file="MapWriter.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;

namespace _4_1_
{
    /// <summary>
    /// Class MapWriter
    /// </summary>
    public static class MapWriter
    {
        /// <summary>
        /// The convert
        /// </summary>
        public static bool Convert = false;

        /// <summary>
        /// The list
        /// </summary>
        public static List<String> list = new List<String>();

        /// <summary>
        /// Speicherns the specified datei.
        /// </summary>
        /// <param name="Datei">The datei.</param>
        public static void Speichern(String Datei)
        {
            StreamWriter datei = new StreamWriter(Path.ChangeExtension(Datei, ".dat"));
            for (int i = 0; i < list.Count; i++)
                datei.WriteLine(list[i]);
            datei.Close();

            //Kompression.Kompression.Komprimiere(Path.ChangeExtension(Datei, ".dat"), Datei);
            // if (File.Exists(Path.ChangeExtension(Datei, ".dat"))) File.Delete(Path.ChangeExtension(Datei, ".dat"));

            list.Clear();
        }

        /// <summary>
        /// Generierens the specified spielfeld.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <returns>List{String}.</returns>
        public static List<String> Generieren(Spiel Spielfeld)
        {
            list.Clear();
            list.AddRange(Spielfeld.Speichern());
            return list;
        }
    }
}