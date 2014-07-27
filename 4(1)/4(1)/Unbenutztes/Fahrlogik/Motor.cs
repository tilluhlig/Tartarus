using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    public class Motor
    {
        #region Fields

        public float Beschleunigung;
        public float Drehzahl;
        public float GeschwindigkeitBeschleunigung;
        public float MaxDrehzahl;
        // Drehzahlbeschleunigung

        public float MaxGeschwindigkeit;
        public List<Vector2> MotorVector = new List<Vector2>();
        public Vector2 MotorVectorOld = new Vector2(0, 0);

        public List<Rad> Raeder = new List<Rad>();

        #endregion Fields

        #region Constructors

        public Motor(int _MaxDrehzahl, int _Beschleunigung, int _Radbedarf, float _MaxGeschwindigkeit,
            float _GeschwindigkeitBeschleunigung)
        {
            MaxDrehzahl = _MaxDrehzahl;
            Beschleunigung = _Beschleunigung;
            //Radbedarf = _Radbedarf;
            MaxGeschwindigkeit = _MaxGeschwindigkeit;
            GeschwindigkeitBeschleunigung = _GeschwindigkeitBeschleunigung;
        }

        #endregion Constructors

        #region Methods

        public void Abschalten()
        {
            Drehzahl = 0;
        }

        public void AddRad(Vector2 pos, Texture2D _Bild)
        {
            Raeder.Add(new Rad(pos, 0.08f, _Bild));
            MotorVector.Add(new Vector2(0, 0));
        }

        public void Beschleunigen()
        {
            if (Drehzahl < MaxDrehzahl)
                Drehzahl += Beschleunigung;

            if (Drehzahl > MaxDrehzahl) Drehzahl = MaxDrehzahl;
        }

        public Vector2 GetAntriebsVector(int rad, float angle, Vector2 Bezugspunkt)
        {
            float Anteil = Drehzahl/MaxDrehzahl;
            // float Beschleunigungsanteil = GetRad(rad,angle,Bezugspunkt) ? 1 : 0; //GetRadAnteil(angle, Bezugspunkt);
            float Bremswert = GetRad(rad, angle, Bezugspunkt) ? Raeder[rad].Reibung : 0;
            //GetBremswert(angle, Bezugspunkt);

            Vector2 result = Raeder[rad].Energie;
            // Raeder[rad].Bremswirkung = 0;
            //    result.Y -= Raeder[rad].Potenzielle;

            if (!GetRad(rad, angle, Bezugspunkt))
            {
                result += new Vector2(0, +0.3f);
            }
            /*else
                if (GetRad(rad, angle, Bezugspunkt) && Raeder[rad].Energie.Y < 3)
                { Raeder[rad].Energie += new Vector2(0, +0.1f); }*/

            //result.Y += Raeder[rad].Potenzielle;

            Vector2 Result2 = Vector2.Zero;
            if (GetRad(rad, angle, Bezugspunkt))
            {
                if (Anteil > 0 && GetRad(rad, angle, Bezugspunkt))
                {
                    Result2.X += MaxGeschwindigkeit*Anteil;
                    //   if (result.X > MaxGeschwindigkeit * Anteil) result.X = MaxGeschwindigkeit * Anteil;
                }
                /* else
                     if (result.X > MaxGeschwindigkeit * Anteil)
                     {
                         result.X -= Bremswert;
                         if (result.X < MaxGeschwindigkeit * Anteil) result.X = MaxGeschwindigkeit * Anteil;
                     }
                     else*/
                if (Anteil == 0 && GetRad(rad, angle, Bezugspunkt) && result.X > 0)
                {
                    Result2.X -= Bremswert;
                    // Bremswirkung
                    // Raeder[rad].Bremswirkung = Raeder[rad].Reibung;
                }
                /*else
                    if (Anteil == 0 && GetRad(rad, angle, Bezugspunkt) && result.X < 0)
                    {
                        Result2.X += Bremswert;
                        // Bremswirkung
                        // Raeder[rad].Bremswirkung = Raeder[rad].Reibung;
                    }*/
            }
            Result2 = Help.RotatePosition(Vector2.Zero, -angle, Result2);

            Raeder[rad].Energie = result + Result2;

            // reflektion
            /* if (GetRadCollision(rad, angle, Bezugspunkt))
                {
                    Vector2 own = Bezugspunkt + Raeder[rad].pos;
                    Vector2 p1 = new Vector2(( new Vector2(-5, 0)).X,Help.BottomOf2(own  + new Vector2(-5, 0)));
                    Vector2 p2 = new Vector2((new Vector2(5, 0)).X, Help.BottomOf2(own + new Vector2(5, 0)));
                    Raeder[rad].Energie = Help.VektorReflexion(Raeder[rad].Energie, p1, p2, 1f);
                }*/

            // MotorVector[rad] = result;
            /* Vector2 temp = MotorVector[rad];
             temp = Help.RotatePosition(Vector2.Zero, angle, temp);*/

            return Raeder[rad].Energie;
        }

        public float GetBremswert(float angle, Vector2 Bezugspunkt)
        {
            float wert = 0;
            for (int i = 0; i < Raeder.Count; i++)
                if (Raeder[i].IsCollision(angle, Bezugspunkt))
                    wert += Raeder[i].Reibung;
            return wert;
        }

        public bool GetRad(int rad, float angle, Vector2 Bezugspunkt)
        {
            //int anz=0;
            //for (int i = 0; i < Raeder.Count; i++)
            if (Raeder[rad].IsKontakt(angle, Bezugspunkt)) return true;
            return false;
            //anz++;
            //  if (anz >= Radbedarf) return 1.0f;
            //  return (float)anz / Radbedarf;
        }

        public bool GetRadCollision(int rad, float angle, Vector2 Bezugspunkt)
        {
            //int anz=0;
            //for (int i = 0; i < Raeder.Count; i++)
            if (Raeder[rad].IsCollision(angle, Bezugspunkt)) return true;
            return false;
            //anz++;
            //  if (anz >= Radbedarf) return 1.0f;
            //  return (float)anz / Radbedarf;
        }

        #endregion Methods

        // public int Radbedarf = 0;
    }
}