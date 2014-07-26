using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///
    /// </summary>
    public class KoerperObjekt
    {
        #region Fields

        /// <summary>
        /// die Position des Schwerpunktes (von Links-Oben)
        /// </summary>
        public Vector2 Schwerpunkt = Vector2.Zero;

        /// <summary>
        /// die Masse des Schwerpunktes
        /// </summary>
        public float SchwerpunktMasse = 0f;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Berechnet den Schwerpunkt einer Textur (von Links-Oben)
        /// </summary>
        /// <param name="Bild">eine Texturmaske</param>
        public void BerechneSchwerpunkt(List<int>[] Bild)
        {
            SchwerpunktMasse = 0;
            Schwerpunkt = Vector2.Zero;

            for (int i = 0; i < Bild.Count(); i++)
            {
                int summe = 0;
                for (int b = 0; b < Bild[i].Count; b++)
                {
                    if (b % 2 == 1)
                    {
                        SchwerpunktMasse += Bild[i][b];
                        Schwerpunkt += new Vector2(i, summe + Bild[i][b] / 2f) * Bild[i][b];
                    }
                    summe += Bild[i][b];
                }
            }
            Schwerpunkt /= SchwerpunktMasse;
        }

        /// <summary>
        /// Hängt neue Massepunkte an das Objekt
        /// </summary>
        /// <param name="Bereiche">
        ///     Ist eine Liste von zu aktualisierenden Bereichen.
        ///     x = X-Position, y = Y-Begin, z = Y-Ende
        /// </param>
        public void ErhoeheSchwerpunktmasse(List<Vector3> Bereiche)
        {
            float neueMasse = 0;
            Vector2 neuerPunkt = Vector2.Zero;
            for (int i = 0; i < Bereiche.Count; i++)
            {
                float m = Bereiche[i].Z - Bereiche[i].Y;
                neuerPunkt += new Vector2(Bereiche[i].X, Bereiche[i].Y + m / 2) * m;
                neueMasse += m;
            }
            neuerPunkt /= neueMasse;
            Schwerpunkt += neuerPunkt;
            SchwerpunktMasse += neueMasse;
        }
        
        /// <summary>
        /// Entfernt Masse vom Objekt
        /// </summary>
        /// <param name="Bereiche">
        ///     Ist eine Liste von zu aktualisierenden Bereichen.
        ///     x = X-Position, y = Y-Begin, z = Y-Ende
        /// </param>
        public void VerringereSchwerpunktmasse(List<Vector3> Bereiche)
        {
            float neueMasse = 0;
            Vector2 neuerPunkt = Vector2.Zero;
            for (int i = 0; i < Bereiche.Count; i++)
            {
                float m = Bereiche[i].Z-Bereiche[i].Y;
                neuerPunkt += new Vector2(Bereiche[i].X, Bereiche[i].Y+m/2) * -m;
                    neueMasse += m;
            }
            neuerPunkt /= neueMasse;
            Schwerpunkt += neuerPunkt;
            SchwerpunktMasse -= neueMasse;
        }

        #endregion Methods
    }
}