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
    /// Diese Klasse beinhaltet Waffen und alles andere Nutzbare für Fahrzeuge (Bunker bauen, Geschütze bauen etc.)
    /// </summary>
    public class Waffen
    {
        // Werte der Munition
        /// <summary>
        /// The energie
        /// </summary>
        public int Energie; // kraterbreite

        /// <summary>
        /// The art
        /// </summary>
        public int Art = 0;

        /// <summary>
        /// The verzoegerung
        /// </summary>
        public int verzoegerung = 0;

        /// <summary>
        /// The watered
        /// </summary>
        public bool watered = false;

        /// <summary>
        /// The lebensdauer
        /// </summary>
        public int Lebensdauer;

        /// <summary>
        /// The besitzer
        /// </summary>
        public int[] Besitzer = new int[2]; // wer hat gefeuert?

        /// <summary>
        /// The ID
        /// </summary>
        public int ID;

        /// <summary>
        /// The missle shot
        /// </summary>
        public bool missleShot;

        /// <summary>
        /// The missle position
        /// </summary>
        public Vector2 misslePosition;

        /// <summary>
        /// The missle direction
        /// </summary>
        public Vector2 missleDirection;

        /// <summary>
        /// The gravity
        /// </summary>
        public static Vector2 gravity = new Vector2(0, 1);

        /// <summary>
        /// The missle angle
        /// </summary>
        public float missleAngle;

        /// <summary>
        /// The last_ position
        /// </summary>
        public Vector2[] Last_Position;

        /// <summary>
        /// The focused
        /// </summary>
        public bool focused = false;

        /// <summary>
        /// Raktns the spitze.
        /// </summary>
        /// <returns>Vector2.</returns>
        public Vector2 RaktnSpitze()
        {
            int MissleLength = Texturen.missle[Art].Width;
            return new Vector2((float)(misslePosition.X + MissleLength * Waffendaten.Skalierung[Art] * Math.Cos(missleAngle)),
                (float)(misslePosition.Y + MissleLength * Waffendaten.Skalierung[Art] * Math.Sin(missleAngle)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Waffen"/> class.
        /// </summary>
        public Waffen() // initialisiert eine neue Rakete
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

        /// <summary>
        /// Check_s the rakete.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="MaxHeight">Height of the max.</param>
        public void check_Rakete(List<UInt16>[] Spielfeld, int MaxHeight) // prüft Lebensdauer einer Rakete um fehlern vorzubeugen
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
        /// Explosions the specified spielfeld.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <returns>List{Vector3}.</returns>
        public List<Vector3> Explosion(List<UInt16>[] Spielfeld)
        {
            // Rakete zünden
            Karte a = new Karte();
            Replay.Explosion(RaktnSpitze(), Art);
            return a.Explode(Spielfeld, (int)RaktnSpitze().X, (int)RaktnSpitze().Y, Energie);
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void Delete() // entfernt eine Rakete
        {
            Besitzer[0] = -1;
            missleShot = false;
            focused = false;
            verzoegerung = 0;
        }

        /// <summary>
        /// Updates the missle.
        /// </summary>
        /// <param name="Wind">The wind.</param>
        public void UpdateMissle(Vector2 Wind) // aktualisiert die Bewegung der Raketen
        {
            for (int i = 0; i < Last_Position.Length - 1; i++)
                Last_Position[i] = Last_Position[i + 1];

            if (missleShot)
            {
                Wind.Y = 0;
                missleDirection += gravity / 10.0f + (Spiel.WIND.Wert ? Wind / 60.0f : Vector2.Zero);
                missleAngle = (float)Math.Atan2(missleDirection.Y, missleDirection.X);
                misslePosition += missleDirection;
                misslePosition = Spiel.Position(misslePosition);
                Last_Position[Last_Position.Length - 1] = misslePosition;
            }
            else
                Last_Position[Last_Position.Length - 1] = new Vector2(-99, -99);
        }

        // TODO ausfüllen
        /// <summary>
        /// Erzeugt den Inhalt der Waffe aus einem String
        /// </summary>
        /// <param name="Text">der Text in dem der Effekt definiert ist</param>
        public void LadeAusText(String Text)
        {
        }

        /// <summary>
        /// Wandelt die Waffe zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> Speichern()
        {
            List<String> data = new List<String>();
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
    }
}