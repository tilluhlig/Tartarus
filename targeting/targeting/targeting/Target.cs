using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace targeting
{
    public static class Target
    {
        //Berechnet Schusskraft zu den gegebenen werten, mit der man den gegner treffen würde
        //betrachtet werden nur winkel zwischen 0 und 90 grad! das Fahrzeug soll also in richtung des ziels guggen!
        //
        /// <summary>
        /// Berechnet Schusskraft 
        /// Referenztypen, da mehrere Ausgaben
        /// </summary>
        /// <param name="angle">Der Abschusswinkel</param>
        /// <param name="dX">Relativer Abstand zum Ziel (X-Koordinate)</param>
        /// <param name="dY">Relativer Abstand zum Ziel (Y-Koordinate)</param>
        /// <param name="a"> Beschleunigung durch Wind </param>
        /// <param name="g">Beschleunigung durch Gravitation</param>
        /// <param name="nach_rechts">Gibt an, in welche Richtung der Schuss fällt</param>
        /// <param name="t">Flugdauer, entsteht als Beiprodukt (Ausgabe)</param>
        /// <param name="v0">Schussstärke (Ausgabe)</param>
        public static void GetPower(double angle, float dX, float dY, float a, float g, bool nach_rechts, ref double t, ref double v0)
        {
            if (!nach_rechts)
                a = -a;
            if (angle != 0 && angle != 2)
            {/*
                double temp1 = (dY * Math.Sin(angle) * 2);
                double temp2 = (dX * Math.Cos(angle) * (a + g));*/
                double temp1 = dX * Math.Sin(angle) - dY * Math.Cos(angle);
                if (temp1 < 10 && temp1 > -10)
                {
                    t = 0; return;
                }
                double temp2 = a / 2 * Math.Sin(angle) + g / 2 * Math.Cos(angle);
                t = temp1 / temp2;
                if (t < 0)
                {
                    v0 = -1;
                    return;
                }
                t = Math.Sqrt(t);
                v0 = (dX - (a / 2) * t * t) / (t * Math.Cos(angle));
                return;
            }
            if (angle == 0)
            {
                //achtung! die y-achse ist verkehrt!
                t = Math.Sqrt(-(dY * 2) / g);
                v0 = (dX - a / 2 * t * t) / t;
            }
            else
            {
                t = Math.Sqrt((dX * 2) / a);
                v0 = (dY + g / 2 * t * t) / t;
            }
        }

    }
}
