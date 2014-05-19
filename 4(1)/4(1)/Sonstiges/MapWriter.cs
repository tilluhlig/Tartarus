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
    /// Diese Klasse dient dem serialisieren und speichern eines Spielobjektes
    /// </summary>
    public static class MapWriter
    {
        /// <summary>
        /// in dieser Liste wird nach dem serialisieren des Spielobjektes, die Textdarstellung des Objektes zum Auslösen
        /// des Speichervorganges abgelegt
        /// </summary>
        public static List<String> list = new List<String>();

        /// <summary>
        /// Speichert die zuvor generierten Speicherdaten in eine Datei
        /// </summary>
        /// <param name="Datei">der Pfad+Name der Zieldatei</param>
        public static void Speichern(String Datei)
        {
            StreamWriter datei = new StreamWriter(Path.ChangeExtension(Datei, ".dat"));
            for (int i = 0; i < list.Count; i++)
                datei.WriteLine(list[i]);
            datei.Close();

            Kompression.Kompression.Komprimiere(Path.ChangeExtension(Datei, ".dat"),  Datei);
            // if (File.Exists(Path.ChangeExtension(Datei, ".dat"))) File.Delete(Path.ChangeExtension(Datei, ".dat"));

            list.Clear();
        }

        /// <summary>
        /// Wandelt ein Spielobjekt in Text um
        /// </summary>
        /// <param name="Spielfeld">das zu serialisierende Spielobjekt</param>
        /// <returns>gibt die Textdarstellung zurück</returns>
        public static List<String> Generieren(Spiel Spielfeld)
        {
            list.Clear();
            list.AddRange(Spielfeld.Speichern());
            return list;
        }
    }
}