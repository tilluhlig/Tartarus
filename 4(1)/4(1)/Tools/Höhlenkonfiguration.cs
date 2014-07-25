// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-24-2013
// ***********************************************************************
// <copyright file="CaveConf.cs" company="">
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
    /// Class Höhlenkonfiguration
    /// </summary>
    public class Höhlenkonfiguration
    {
        #region Privat

        #region Allgemein

        /// <summary>
        /// The beginy
        /// </summary>
        private int beginy = 50;

        /// <summary>
        /// The dist between caves
        /// </summary>
        private int DistBetweenCaves = 150;

        /// <summary>
        /// The liquid
        /// </summary>
        private bool liquid = false;

        /// <summary>
        /// The loch
        /// </summary>
        private int loch = 450;

        /// <summary>
        /// The material
        /// </summary>
        private int Material = Karte.LUFT;

        /// <summary>
        /// The maxbreite
        /// </summary>
        private int maxbreite = 750;

        /// <summary>
        /// The minbreite
        /// </summary>
        private int minbreite = 250;

        /// <summary>
        /// The minhigh
        /// </summary>
        private int minhigh = 150;

        #endregion Allgemein

        #region A

        /// <summary>
        /// The end A
        /// </summary>
        private float endA = 0.3f;

        /// <summary>
        /// The high A
        /// </summary>
        private int highA = 50;

        /// <summary>
        /// The low A
        /// </summary>
        private int lowA = 8;

        /// <summary>
        /// The lowest A
        /// </summary>
        private int lowestA = 0;

        /// <summary>
        /// The maxm A
        /// </summary>
        private int maxmA = 30;

        /// <summary>
        /// The minm A
        /// </summary>
        private int minmA = 5;

        #endregion A

        #region B

        /// <summary>
        /// The end B
        /// </summary>
        private float endB = 0.3f;

        /// <summary>
        /// The high B
        /// </summary>
        private int highB = 80;

        /// <summary>
        /// The low B
        /// </summary>
        private int lowB = 8;

        /// <summary>
        /// The lowest B
        /// </summary>
        private int lowestB = 0;

        /// <summary>
        /// The maxm B
        /// </summary>
        private int maxmB = 50;

        /// <summary>
        /// The minm B
        /// </summary>
        private int minmB = 15;

        #endregion B

        #region C

        /// <summary>
        /// The end C
        /// </summary>
        private float endC = 0.2f;

        /// <summary>
        /// The high C
        /// </summary>
        private int highC = 40;

        /// <summary>
        /// The low C
        /// </summary>
        private int lowC = -40;

        /// <summary>
        /// The lowest C
        /// </summary>
        private int lowestC = -40;

        /// <summary>
        /// The maxm C
        /// </summary>
        private int maxmC = 50;

        /// <summary>
        /// The minm C
        /// </summary>
        private int minmC = 15;

        #endregion C

        /// <summary>
        /// Prüft ob eine x-Koordinate in einem gesperrten Bereich liegt
        /// </summary>
        /// <param name="x">die x-Koordinate</param>
        /// <param name="Sperrgebiete">Eine Liste aus Sperrgebieten x = x-Anfang, y = Breite des gesperrten Bereichs</param>
        /// <returns>true = x liegt in einem solchen Bereich, false = x liegt nicht in einem gesperrten Bereich</returns>
        private static bool IsGesperrt(int x, List<Vector2> Sperrgebiete)
        {
            if (Sperrgebiete == null) return false;
            for (int i = 0; i < Sperrgebiete.Count; i++)
            {
                if (x >= Sperrgebiete[i].X && x <= Sperrgebiete[i].X + Sperrgebiete[i].Y)
                    return true;
            }
            return false;
        }

        #endregion Privat

        /// <summary>
        /// Erzeugt "Löcher" im Boden, anhand der übergebenen Definition
        /// </summary>
        /// <param name="hoehle">Die Höhlendefinition</param>
        /// <param name="array">ein Spielfeld im Kartenformat</param>
        /// <param name="Sperrgebiete">Eine Liste aus Sperrgebieten x = x-Anfang, y = Breite des gesperrten Bereichs</param>
        public static void Generate(Höhlenkonfiguration hoehle, List<UInt16>[] array, List<Vector2> Sperrgebiete)
        {
            // löcher in den Boden machen
            //Help.Spielfeld = array;
            int loch = hoehle.loch; // 450

            bool machen = false;
            int wertA = 0;
            int wertB = 0;
            int wertC = 0;

            int y = 0; // y position (mittelpunkt der höhle)
            int breite = 0; // Breite der Höhle
            int maxbreite = 0; // temp

            int moveA = 0;
            int mA = 0;
            int posA = 0;
            int maxA = 0;
            int lastmoveA = 0;
            int distA = 0;

            int moveB = 0;
            int mB = 0;
            int posB = 0;
            int maxB = 0;
            int lastmoveB = 0;
            int distB = 0;

            int moveC = 0;
            int mC = 0;
            int posC = 0;
            int maxC = 0;
            int lastmoveC = 0;
            int distC = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (!machen)
                {
                    if (Kartenformat.Laenge(array[i][1]) >= hoehle.minhigh) // 150
                    {
                        machen = Help.rnd.Next(0, loch) == 1 ? true : false;
                        if (machen)
                        {
                            y = Kartenformat.Laenge(array[i][0]) + (!hoehle.liquid ? Help.rnd.Next(hoehle.beginy, (int)(Kartenformat.Laenge(array[i][1]))) : hoehle.beginy); // 50
                            breite = Help.rnd.Next(hoehle.minbreite, hoehle.maxbreite); //250,750
                            maxbreite = breite;
                            moveA = 0;
                            mA = 0;
                            posA = 0;
                            maxA = 0;
                            lastmoveA = 0;
                            distA = 0;

                            moveB = 0;
                            mB = 0;
                            posB = 0;
                            maxB = 0;
                            lastmoveB = 0;
                            distB = 0;

                            moveC = 0;
                            mC = 0;
                            posC = 0;
                            maxC = 0;
                            lastmoveC = 0;
                            distC = 0;

                            wertA = 0;
                            wertB = 0;
                            wertC = 0;
                        }
                    }
                }
                else
                {
                    #region WERTA

                    //wertA += rnd.Next(-1, 2);
                    if (mA <= 0)
                    {
                        lastmoveA = moveA;
                        for (; ; )
                        {
                            moveA = Help.rnd.Next(-1, 2);
                            if (moveA == 0) continue;
                            if (!((lastmoveA == -1 && moveA == 1) || (lastmoveA == 1 && moveA == -1))) break;
                        }

                        mA = Help.rnd.Next(hoehle.minmA, hoehle.maxmA); // 5,30
                        posA = mA;
                        distA = 0;
                        maxA = mA;
                    }

                    if (maxbreite * hoehle.endA >= breite) moveA = -1;

                    if (wertA >= hoehle.highA) // 50
                    {
                        moveA = -1;
                    }
                    else
                        if (wertA <= hoehle.lowA && maxbreite * hoehle.endA < breite) // 8 // 0.3f
                        {
                            moveA = 1;
                        }
                        else
                            if (wertA <= hoehle.lowestA)
                            {
                                moveA = 1;
                            }

                    if (moveA == 1)
                    {
                        if (distA <= 0)
                        {
                            posA--;
                            distA = (int)Math.Log(maxA - mA + 1, Math.E);
                        }
                        else
                            wertA++;
                    }
                    else
                        if (moveA == -1)
                        {
                            if (distA <= 0)
                            {
                                posA--;
                                distA = (int)Math.Log(mA, Math.E);
                            }
                            else
                                wertA--;
                        }

                    mA--;
                    distA--;

                    #endregion WERTA

                    #region WERTB

                    if (mB <= 0)
                    {
                        lastmoveB = moveB;
                        for (; ; )
                        {
                            moveB = Help.rnd.Next(-1, 2);
                            if (!((lastmoveB == -1 && moveB == 1) || (lastmoveB == 1 && moveB == -1))) break;
                        }

                        mB = Help.rnd.Next(hoehle.minmB, hoehle.maxmB); // 15,50
                        posB = mB;
                        distB = 0;
                        maxB = mB;
                    }

                    if (maxbreite * hoehle.endB >= breite) moveB = -1;

                    if (wertB >= hoehle.highB) // 80
                    {
                        moveB = -1;
                    }
                    else
                        if (wertB <= hoehle.lowB && maxbreite * hoehle.endB < breite) // 8
                        {
                            moveB = 1;
                        }
                        else
                            if (wertB <= hoehle.lowestB)
                            {
                                moveB = 1;
                            }

                    if (moveB == 1)
                    {
                        if (distB <= 0)
                        {
                            posB--;

                            distB = (int)Math.Log(mB, Math.E);
                        }
                        else
                            wertB++;
                    }
                    else
                        if (moveB == -1)
                        {
                            if (distB <= 0)
                            {
                                posB--;
                                distB = (int)Math.Log(maxB - mB + 1, Math.E);
                            }
                            else
                                wertB--;
                        }

                    mB--;
                    distB--;

                    #endregion WERTB

                    #region WERTC

                    if (mC <= 0)
                    {
                        lastmoveC = moveC;
                        for (; ; )
                        {
                            moveC = Help.rnd.Next(-1, 2);
                            if (!((lastmoveC == -1 && moveC == 1) || (lastmoveC == 1 && moveC == -1))) break;
                        }

                        mC = Help.rnd.Next(hoehle.minmC, hoehle.maxmC);
                        posC = mC;
                        distC = 0;
                        maxC = mC;
                    }

                    if (maxbreite * hoehle.endC >= breite) moveC = -1;

                    if (wertC >= hoehle.highC)
                    {
                        moveC = -1;
                    }
                    else
                        if (wertC <= hoehle.lowC && maxbreite * hoehle.endC < breite)
                        {
                            moveC = 1;
                        }
                        else
                            if (wertC <= hoehle.lowestC)
                            {
                                moveC = 1;
                            }

                    if (moveC == 1)
                    {
                        if (distC <= 0)
                        {
                            posC--;
                            distC = (int)Math.Log(maxC - mC + 1, Math.E);
                        }
                        else
                            wertC++;
                    }
                    else
                        if (moveC == -1)
                        {
                            if (distC <= 0)
                            {
                                posC--;
                                distC = (int)Math.Log(mC, Math.E);
                            }
                            else
                                wertC--;
                        }

                    mC--;
                    distC--;

                    #endregion WERTC

                    //Help.DeleteFromTo2(array,i, y - wertA + wertC, y + wertB + wertC);
                    if (!IsGesperrt(i, Sperrgebiete))
                    {
                        if (hoehle.liquid)
                        {
                            int a = Kartenformat.BottomOf(array, i, y - wertA + wertC);
                            int b = y + wertB + wertC;
                            if (a < b)
                            {
                                Kartenformat.SetMaterialFromTo(array, i, a, b, hoehle.Material);
                            }
                        }
                        else
                            Kartenformat.SetMaterialFromTo(array, i, y - wertA + wertC, y + wertB + wertC, hoehle.Material);
                    }
                    else
                    {
                        if (hoehle.liquid)
                        {
                        }
                        else
                        {
                            int yps = y - wertA + wertC;
                            int yps2 = y + wertB + wertC;
                            int a = Kartenformat.BottomOf(array, i, yps);
                            if (yps < a + 10) yps = a + 10;
                            if (yps < yps2) Kartenformat.SetMaterialFromTo(array, i, yps, yps2, hoehle.Material);
                        }
                    }

                    if (maxbreite * ((hoehle.endA + hoehle.endB) / 2) >= breite && wertA == 0 && wertB == 0) { breite = 0; }

                    if (breite > 0)
                    {
                        breite--;
                    }
                    else
                    {
                        machen = false;
                        i += hoehle.DistBetweenCaves;
                    }
                }
            }
        }

        /// <summary>
        /// Setzt die "A" Werte für die Definition
        /// </summary>
        /// <param name="_minmA">The _minm A.</param>
        /// <param name="_maxmA">The _maxm A.</param>
        /// <param name="_lowestA">The _lowest A.</param>
        /// <param name="_lowA">The _low A.</param>
        /// <param name="_highA">The _high A.</param>
        /// <param name="_endA">The _end A.</param>
        public void setA(int _minmA, int _maxmA, int _lowestA, int _lowA, int _highA, float _endA)
        {
            minmA = _minmA;
            maxmA = _maxmA;
            lowestA = _lowestA;
            lowA = _lowA;
            highA = _highA;
            endA = _endA;
        }

        /// <summary>
        /// Setzt "Allgemeine" Werte für die Definition
        /// </summary>
        /// <param name="_loch">The _loch.</param>
        /// <param name="_minhigh">The _minhigh.</param>
        /// <param name="_beginy">The _beginy.</param>
        /// <param name="_minbreite">The _minbreite.</param>
        /// <param name="_maxbreite">The _maxbreite.</param>
        /// <param name="_DistBeetweenCaves">The _ dist beetween caves.</param>
        /// <param name="_Material">The _ material.</param>
        /// <param name="_liquid">if set to <c>true</c> [_liquid].</param>
        public void setAllgemein(int _loch, int _minhigh, int _beginy, int _minbreite, int _maxbreite, int _DistBeetweenCaves, int _Material, bool _liquid)
        {
            liquid = _liquid;
            loch = _loch;
            minhigh = _minhigh;
            beginy = _beginy;
            minbreite = _minbreite;
            maxbreite = _maxbreite;
            DistBetweenCaves = _DistBeetweenCaves;
            Material = _Material;
        }

        /// <summary>
        /// Setzt die "B" Werte für die Definition
        /// </summary>
        /// <param name="_minmB">The _minm B.</param>
        /// <param name="_maxmB">The _maxm B.</param>
        /// <param name="_lowestB">The _lowest B.</param>
        /// <param name="_lowB">The _low B.</param>
        /// <param name="_highB">The _high B.</param>
        /// <param name="_endB">The _end B.</param>
        public void setB(int _minmB, int _maxmB, int _lowestB, int _lowB, int _highB, float _endB)
        {
            minmB = _minmB;
            maxmB = _maxmB;
            lowestB = _lowestB;
            lowB = _lowB;
            highB = _highB;
            endB = _endB;
        }

        /// <summary>
        /// Setzt die "C" Werte für die Definition
        /// </summary>
        /// <param name="_minmC">The _minm C.</param>
        /// <param name="_maxmC">The _maxm C.</param>
        /// <param name="_lowestC">The _lowest C.</param>
        /// <param name="_lowC">The _low C.</param>
        /// <param name="_highC">The _high C.</param>
        /// <param name="_endC">The _end C.</param>
        public void setC(int _minmC, int _maxmC, int _lowestC, int _lowC, int _highC, float _endC)
        {
            minmC = _minmC;
            maxmC = _maxmC;
            lowestC = _lowestC;
            lowC = _lowC;
            highC = _highC;
            endC = _endC;
        }
    }
}