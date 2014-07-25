using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    public class Fahrlogik_Object
    {
        public List<Kollisionspunkt> Kollisionspunkte = new List<Kollisionspunkt>();
        public Vector2 Gewicht;
        public Texture2D Bild;
        public Motor Motoren;

        public Fahrlogik_Object(float _Gewicht)
        {
            // Bild = _Bild;
            Gewicht = new Vector2(0, _Gewicht);
            Motoren = new Motor(10000, 150, 1, 1.5f, 5);
        }

        public bool AnyCollision(float angle, Vector2 Bezugspunkt, bool overreach)
        {
            for (int i = 0; i < Motoren.Raeder.Count; i++)
            {
                if (Motoren.GetRad(i, angle, Bezugspunkt)) return true;
            }
            return false;
        }

        public Vector2 GetBewegungsVektor(int rad, float angle, Vector2 Bezugspunkt, bool overreach)
        {
            // Schwerkraft (am Hang)
            Vector2 Result = Vector2.Zero;//= Gewicht;
            /*  Result = Help.RotatePosition(Vector2.Zero, -angle, Result);
              Result.Y = Math.Abs(Result.X/5);
              Result.X = 0;
              Result = Help.RotatePosition(Vector2.Zero, -angle, Result);*/

            // Motor
            Vector2 motor = Motoren.GetAntriebsVector(rad, angle, Bezugspunkt);
            Vector2 Result2 = overreach ? motor : new Vector2(-motor.X, motor.Y);

            Result += motor;

            // Reibung des Rades nutzen, wenn gegenkraft zu hoch
            /*  if (overreach){
                          if (Result.X < 0)
                          {
                              Result.X += Motoren.Raeder[rad].Reibung*5;
                              if (Result.X > 0) Result.X = 0;
                         }
                  }
              else
                        if (Result.X > 0)
                          {
                              Result.X -= Motoren.Raeder[rad].Reibung*5;
                              if (Result.X < 0) Result.X = 0;
                         }*/

            return Result;
        }
    }
}