// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-24-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="Kartenformat.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    /// Diese Klasse stellt Funktionen zur Verwaltung des Kartenformats zur verfügung
    /// </summary>
    public static class Kartenformat
    {
        /// <summary>
        /// Diese Konstante wird für Berechnungen innerhalb des Kartenformats benötigt (gibt größe der Abschnitte an)
        /// </summary>
        private static int MapFaktor = 4096;

        /// <summary>
        /// Gibt die nächste y Koordinate aus, an der es zu einer Kollision kommt (sucht nach unten, wenn in Material, dann nach oben), nutzt Spielfeld
        /// </summary>
        /// <param name="x">x Koordiante</param>
        /// <param name="y">y Koordiante</param>
        /// <returns>gibt die gesuchte y Koordinate zurück</returns>
        public static int BottomOf(float x, float y)
        {
            return BottomOf(Help.Spielfeld, x, y);
        }

        /// <summary>
        /// Gibt die nächste y Koordinate aus, an der es zu einer Kollision kommt (sucht nach unten, wenn in Material: dann nach oben), nutzt Spielfeld
        /// </summary>
        /// <param name="pos">Die zu prüfende Position</param>
        /// <returns>gibt die gesuchte y Koordinate zurück</returns>
        public static int BottomOf(Vector2 pos)
        {
            return BottomOf(pos.X, pos.Y);
        }

        /// <summary>
        /// Gibt die nächste y Koordinate aus, an der es zu einer Kollision kommt (sucht nach unten, wenn in Material, dann nach oben)
        /// </summary>
        /// <param name="array">das Spielfeld im Kartenformat</param>
        /// <param name="x">x Koordiante</param>
        /// <param name="y">y Koordiante</param>
        /// <returns>gibt die gesuchte y Koordinate zurück</returns>
        public static int BottomOf(List<UInt16>[] array, float x, float y)
        {
            int sum = 0;
            int last = 0;
            x = Spiel.Position(x);

            for (int i = 0; i < array[(int)x].Count; i++)
            {
                if (!Karte.Material[Material(array[(int)x][i])].Kollision) { last = 0; } else last += Laenge(array[(int)x][i]);
                sum += Laenge(array[(int)x][i]);
                if (y < sum)
                {
                    if (!Karte.Material[Material(array[(int)x][i])].Kollision) //Spielfeld[(int)x][i] < Karte.MapFaktor
                    {
                        if (i < array[(int)x].Count - 1 && Karte.Material[Material(array[(int)x][i + 1])].Kollision)
                        {
                            return sum;
                        }
                    }
                    else
                    {
                        return sum - last;
                    }
                }
            }
            return sum;
        }

        /// <summary>
        /// Löscht einen Abschnitt und setzt dabei die nächste Materialsorte
        /// </summary>
        /// <param name="array">das Array mit den Kartendaten</param>
        /// <param name="x">x Koordiante</param>
        /// <param name="y1">y1 Koordiante</param>
        /// <param name="y2">y2 Koordiante</param>
        /// <returns>Gibt die Bereiche zurück, die neu gezeichnet werden müssen</returns>
        public static List<Vector3> DeleteFromTo(List<UInt16>[] array, int x, int y1, int y2)
        {
            return SetMaterialFromTo(array, x, y1, y2, -1);
        }

        /// <summary>
        /// Löscht einen Abschnitt und setzt dabei die nächste Materialsorte (nutzt Spielfeld)
        /// </summary>
        /// <param name="x">x Koordiante</param>
        /// <param name="y1">y1 Koordiante</param>
        /// <param name="y2">y2 Koordiante</param>
        /// <returns>Gibt die Bereiche zurück, die neu gezeichnet werden müssen</returns>
        public static List<Vector3> DeleteFromTo(int x, int y1, int y2)
        {
            return DeleteFromTo(Help.Spielfeld, x, y1, y2);
        }

        /// <summary>
        /// Gibt das Material an der Position zurück (nutzt Spielfeld)
        /// </summary>
        /// <param name="Pos">Die Position</param>
        /// <returns>Gibt die ID des Materials zurück</returns>
        public static int GetMaterial(Vector2 Pos)
        {
            return GetMaterial(Help.Spielfeld, Pos.X, Pos.Y);
        }

        /// <summary>
        /// Gibt das Material an der Position zurück (nutzt Spielfeld)
        /// </summary>
        /// <param name="x">die x-Koordinate</param>
        /// <param name="y">die y-Koordinate</param>
        /// <returns>Gibt die ID des Materials zurück</returns>
        public static int GetMaterial(float x, float y)
        {
            return GetMaterial(Help.Spielfeld, x, y);
        }

        /// <summary>
        /// Gibt das Material an der Position zurück
        /// </summary>
        /// <param name="array">Spielfeld im Kartenformat</param>
        /// <param name="x">die x-Koordinate</param>
        /// <param name="y">die y-Koordinate</param>
        /// <returns>Gibt die ID des Materials zurück</returns>
        public static int GetMaterial(List<UInt16>[] array, float x, float y)
        {
            int x2 = (int)Spiel.Position(x);
            int sum = 0;
            for (int i = 0; i < array[x2].Count; i++)
            {
                sum += Laenge(array[x2][i]);
                if ((int)y < sum)
                {
                    return Material(array[x2][i]);
                }
            }
            return Karte.WASSER;
        }

        /// <summary>
        /// Prüft, ob eine Koordinate eine Kollision verursacht (ist gesetzt?)
        /// </summary>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        /// <returns>true = Position x,y verursacht Kollision</returns>
        public static bool isSet(float x, float y)
        {
            return isSet(Help.Spielfeld, x, y);
        }

        /// <summary>
        /// Prüft, ob eine Koordinate eine Kollision verursacht (ist gesetzt?)
        /// </summary>
        /// <param name="array">das Array im kartenformat</param>
        /// <param name="x">x Koordinate</param>
        /// <param name="y">y Koordinate</param>
        /// <returns>true = Position x,y verursacht Kollision</returns>
        public static bool isSet(List<UInt16>[] array, float x, float y)
        {
            x = Spiel.Position(x);
            int sum = 0;
            for (int i = 0; i < array[(int)x].Count; i++)
            {
                sum += Laenge(array[(int)x][i]);
                if ((int)y < sum)
                {
                    if (!Karte.Material[Material(array[(int)x][i])].Kollision) return false;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Prüft, ob eine Koordinate eine Kollision verursacht (ist gesetzt?), nutzt Spielfeld
        /// </summary>
        /// <param name="pos">Die Position die geprüft werden soll</param>
        /// <returns>true = Position x,y verursacht Kollision</returns>
        public static bool isSet(Vector2 pos)
        {
            return isSet(Help.Spielfeld, pos.X, pos.Y);
        }

        /// <summary>
        /// Berechnet aus einem Wert des Kartenformats die Länge des Abschnitts
        /// </summary>
        /// <param name="Zahl">der Wert</param>
        /// <returns>gibt die Länge zurück</returns>
        public static int Laenge(UInt16 Zahl)
        {
            return Zahl & 4095;
        }

        /// <summary>
        ///  Berechnet aus einem Wert des Kartenformats die Länge des Abschnitts
        /// </summary>
        /// <param name="Zahl">der Wert</param>
        /// <returns>gibt die Länge zurück</returns>
        public static int Material(UInt16 Zahl)
        {
            return Zahl >> 12;
        }

        /// <summary>
        /// Setzt einen Abschnitt auf eine Materialsorte (nutzt Spielfeld)
        /// </summary>
        /// <param name="x">x Koordiante</param>
        /// <param name="y1">y1 Koordiante</param>
        /// <param name="y2">y2 Koordiante</param>
        /// <param name="MaterialSorte">das zu setzende Material</param>
        /// <returns>Gibt die Bereiche zurück, die neu gezeichnet werden müssen</returns>
        public static List<Vector3> SetMaterialFromTo(int x, int y1, int y2, int MaterialSorte)
        {
            return SetMaterialFromTo(Help.Spielfeld, x, y1, y2, MaterialSorte);
        }

        /// <summary>
        /// Setzt einen Abschnitt auf eine Materialsorte
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="x">x Koordiante</param>
        /// <param name="y1">y1 Koordiante</param>
        /// <param name="y2">y2 Koordiante</param>
        /// <param name="MaterialSorte">das zu setzende Material</param>
        /// <returns>Gibt die Bereiche zurück, die neu gezeichnet werden müssen</returns>
        public static List<Vector3> SetMaterialFromTo(List<UInt16>[] array, int x, int y1, int y2, int MaterialSorte)
        {
            List<Vector3> list = new List<Vector3>();
            if (x >= array.Length) return list;
            if (x < 0) return list;
            if (y1 > y2) { int temp = y1; y1 = y2; y2 = temp; }
            if (y1 < 0) y1 = 0;
            if (y2 > Game1.screenHeight) y2 = Game1.screenHeight;

            int sum = 0;
            int last = 0;
            int sum1 = 0; int sum2 = 0; int last1 = 0; int last2 = 0;
            int a = -1;
            int b = -1;
            for (int i = 0; i < array[x].Count; i++)
            {
                last = sum;
                sum += Laenge(array[x][i]);

                if (a == -1 && y1 <= sum)
                {
                    a = i;
                    sum1 = sum;
                    last1 = last;
                }

                if (b == -1 && y2 <= sum)
                {
                    b = i;
                    sum2 = sum;
                    last2 = last;
                    break;
                }
            }

            if (a != -1 && b != -1)
            {
                float fakt = MaterialSorte * MapFaktor;

                // entferne
                if (a == b) // befinden sich im selben Bereich
                {
                    if (Material(array[x][a]) == MaterialSorte || (MaterialSorte == -1 && Material(array[x][a]) == Karte.LUFT)) // ist bereits das Material
                    {
                        // brauche nichts ändern
                        return list;
                    }
                    else
                    {     // ist anderes Material
                        float fakt2 = Material(array[x][a]) * MapFaktor;
                        fakt = (MaterialSorte == -1 ? Karte.Material[Material(array[x][a])].FolgeID : MaterialSorte) * MapFaktor;
                        array[x].Insert(a + 1, (UInt16)((sum2 - y2) + fakt2)); b++;
                        array[x].Insert(a + 1, (UInt16)((y2 - y1) + fakt));
                        array[x].Insert(a + 1, (UInt16)((y1 - last1) + fakt2)); b++;
                        array[x].RemoveAt(a);

                        list.Add(new Vector3(x, y1, y2));
                    }
                }
                else
                {
                    // a bearbeiten
                    int anz = sum1 - y1;
                    int old = sum1;

                    fakt = (MaterialSorte == -1 ? Karte.Material[Material(array[x][a])].FolgeID : MaterialSorte) * MapFaktor;
                    if ((MaterialSorte == -1 && Material(array[x][a]) != Karte.Material[Material(array[x][a])].FolgeID) || MaterialSorte != Material(array[x][a])) list.Add(new Vector3(x, y1, sum1));
                    array[x][a] = (UInt16)(array[x][a] - anz);
                    array[x].Insert(a + 1, (UInt16)(anz + fakt)); b++;

                    // b bearbeiten
                    anz = y2 - last2;
                    fakt = (MaterialSorte == -1 ? Karte.Material[Material(array[x][b])].FolgeID : MaterialSorte) * MapFaktor;
                    array[x].Insert(b, (UInt16)(anz + fakt));
                    array[x][b + 1] = (UInt16)(array[x][b + 1] - anz); b++;
                    if ((MaterialSorte == -1 && Material(array[x][b]) != Karte.Material[Material(array[x][b])].FolgeID) || MaterialSorte != Material(array[x][b])) list.Add(new Vector3(x, last2, y2));

                    int summe = old;
                    bool change = false;
                    if ((MaterialSorte == -1 && Material(array[x][a + 2]) != Karte.Material[Material(array[x][a + 2])].FolgeID) || MaterialSorte != Material(array[x][a + 2])) change = true;
                    for (int i = a + 2; i < b - 1; i++)
                    {
                        summe += Laenge(array[x][i]);
                        fakt = (MaterialSorte == -1 ? Karte.Material[Material(array[x][i])].FolgeID : MaterialSorte) * MapFaktor;
                        array[x][i] = (UInt16)((Laenge(array[x][i]) + fakt));
                    }
                    if (change) list.Add(new Vector3(x, old, summe)); //if (change)
                    //  list.Add(new Vector3(x, y1, y2));
                }
            }

            // gleiche zusammenfügen
            if (a > 0) a--;
            if (a < 0) a = 0;
            for (int i = a; i <= b && i < array[x].Count - 1; i++)
            {
                int sorteA = Material(array[x][i]);
                int sorteB = Material(array[x][i + 1]);
                if (sorteA == sorteB)
                {
                    array[x][i] += (UInt16)(Laenge(array[x][i + 1]));
                    array[x].RemoveAt(i + 1);
                    i--;
                    b--;
                }
            }

            return list;
        }

        /// <summary>
        /// Berechent aus einer Material-ID den Sortenfaktor (array[i] = 5 * SortenFaktor(Stein))
        /// </summary>
        /// <param name="sorte">die ID des Materials</param>
        /// <returns>gibt den Faktor zurück</returns>
        public static int SortenFaktor(int sorte)
        {
            return sorte * MapFaktor;
        }
    }
}