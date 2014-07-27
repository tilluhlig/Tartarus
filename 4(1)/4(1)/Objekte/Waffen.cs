// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-01-2013
// ***********************************************************************
// <copyright file="Rakete.cs" company="">
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
    ///     Diese Klasse beinhaltet Waffen und alles andere Nutzbare für Fahrzeuge (Bunker bauen, Geschütze bauen etc.)
    /// </summary>
    public class Waffen
    {
        #region Fields

        /// <summary>
        ///     ein Faktor zur Anpassung der Absinkgeschwindigkeit des Geschosses
        /// </summary>
        public static Vector2 gravity = new Vector2(0, 1);

        /// <summary>
        ///     die Sorte des Geschosses
        /// </summary>
        public int Art = 0;

        /// <summary>
        ///     Wer hat das Geschoss abgefeuert ([0] = SpielerId, [1] = FahrzeugId)
        /// </summary>
        public int[] Besitzer = new int[2];

        /// <summary>
        ///     Explosionsenergie
        /// </summary>
        public int Energie;

        /// <summary>
        ///     die ID des Objektes
        /// </summary>
        public int ID;

        /// <summary>
        ///     die Position des Geschosses im letzten Berechnungsschritt
        /// </summary>
        public Vector2[] Last_Position;

        /// <summary>
        ///     die Lebensdauer des Geschosses (wieviel ist noch übrig?)
        /// </summary>
        public int Lebensdauer;

        /// <summary>
        ///     ist der Fokus auf diesem Geschoss?
        /// </summary>
        public bool focused = false;

        /// <summary>
        ///     Winkel
        /// </summary>
        public float missleAngle;

        /// <summary>
        ///     Richtung der Bewegung
        /// </summary>
        public Vector2 missleDirection;

        /// <summary>
        ///     die Position
        /// </summary>
        public Vector2 misslePosition;

        /// <summary>
        ///     The missle shot
        /// </summary>
        public bool missleShot;

        /// <summary>
        ///     Explosionsverzögerung (60 = 1s)
        /// </summary>
        public int verzoegerung = 0;

        /// <summary>
        ///     ist das Geschoss im Wasser gelandet? 
        /// </summary>
        public bool watered = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initialisiert ein Geschoss
        /// </summary>
        public Waffen()
        {
            misslePosition.X = -1;
            misslePosition.Y = -1;
            Energie = 0;
            Lebensdauer = 0;
            Besitzer[0] = -1;
            ID = 0;
            missleShot = false;
            Last_Position = new Vector2[15];
            for (int i = 0; i < Last_Position.Length; i++)
            {
                Last_Position[i] = new Vector2(-99, -99);
            }
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Erzeugt den Inhalt der Waffe aus einem String
        /// </summary>
        /// <param name="Text">der Text in dem der Effekt definiert ist</param>
        public static Waffen Laden(List<String> Text, int id)
        {
            return null;
        }

        /// <summary>
        ///     prüft Lebensdauer eines Geschosses um fehlern vorzubeugen
        /// </summary>
        /// <param name="Spielfeld">ein Spielfeld</param>
        /// <param name="MaxHeight">die Spielfeldhöhe</param>
        public void check_Rakete(List<UInt16>[] Spielfeld, int MaxHeight)
        {
            if (Besitzer[0] != -1)
            {
                if (Lebensdauer > 0)
                {
                    Lebensdauer--;
                }
                else
                {
                    Explosion(Spielfeld);
                    Delete();
                }
            }
        }

        /// <summary>
        ///     entfernt ein Geschoss
        /// </summary>
        public void Delete()
        {
            Besitzer[0] = -1;
            missleShot = false;
            focused = false;
            verzoegerung = 0;
        }

        /// <summary>
        ///     Zündet ein Geschoss
        /// </summary>
        /// <param name="Spielfeld">ein Spielfeld</param>
        /// <returns>eine Liste mit zerstörten Bereichen des Spielfeldes</returns>
        public List<Vector3> Explosion(List<UInt16>[] Spielfeld)
        {
            // Rakete zünden
            var a = new Karte();
            Replay.Explosion(RaktnSpitze(), Art);
            return a.Explode(Spielfeld, (int) RaktnSpitze().X, (int) RaktnSpitze().Y, Energie);
        }

        /// <summary>
        ///     gibt die Spitze der Rakete zurück
        /// </summary>
        /// <returns>Position der Raketenspitze</returns>
        public Vector2 RaktnSpitze()
        {
            int MissleLength = Texturen.missle[Art].Width;
            return
                new Vector2((float) (misslePosition.X + MissleLength*Waffendaten.Skalierung[Art]*Math.Cos(missleAngle)),
                    (float) (misslePosition.Y + MissleLength*Waffendaten.Skalierung[Art]*Math.Sin(missleAngle)));
        }

        /// <summary>
        ///     Wandelt die Waffe zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> Speichern()
        {
            var data = new List<String>();
            data.Add("[WAFFE]");
            data.Add("Energie=" + Energie);
            data.Add("Art=" + Art);
            data.Add("verzoegerung=" + verzoegerung);
            data.Add("watered=" + watered);
            data.Add("Lebensdauer=" + Lebensdauer);
            data.Add("ID=" + ID);
            data.Add("missleShot=" + missleShot);
            data.Add("misslePosition=" + misslePosition);
            data.Add("missleDirection=" + missleDirection);
            data.Add("missleAngle=" + missleAngle);
            //     data.Add("Last_Position=" + Last_Position);
            data.Add("focused=" + focused);
            data.Add("Besitzer[0]=" + Besitzer[0]);
            data.Add("Besitzer[1]=" + Besitzer[1]);
            data.Add("[/WAFFE]");
            return data;
        }

        /// <summary>
        ///     Aktualisiert ein Geschoss (berechnung der Bewegung)
        /// </summary>
        /// <param name="Wind">der Wind</param>
        public void UpdateMissle(Vector2 Wind)
        {
            for (int i = 0; i < Last_Position.Length - 1; i++)
                Last_Position[i] = Last_Position[i + 1];

            if (missleShot)
            {
                Wind.Y = 0;
                missleDirection += gravity/10.0f + (Spiel.WIND.Wert ? Wind/60.0f : Vector2.Zero);
                missleAngle = (float) Math.Atan2(missleDirection.Y, missleDirection.X);
                misslePosition += missleDirection;
                misslePosition = Spiel.Position(misslePosition);
                Last_Position[Last_Position.Length - 1] = misslePosition;
            }
            else
                Last_Position[Last_Position.Length - 1] = new Vector2(-99, -99);
        }

        #endregion Methods
    }
}