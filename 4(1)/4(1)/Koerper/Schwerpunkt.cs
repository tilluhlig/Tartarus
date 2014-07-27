using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///     Diese Klasse erlaubt Operationen auf Schwerpunkte
    /// </summary>
    public class Schwerpunkt
    {
        #region Fields

        /// <summary>
        ///     die Position des Schwerpunktes (von Links-Oben)
        /// </summary>
        public Vector2 ObjektSchwerpunkt = Vector2.Zero;

        /// <summary>
        ///     die Masse des Schwerpunktes
        /// </summary>
        public float SchwerpunktMasse = 0f;

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Berechnet den Schwerpunkt einer Textur (von Links-Oben)
        /// </summary>
        /// <param name="Bild">eine Texturmaske</param>
        public void BerechneSchwerpunkt(List<int>[] Bild)
        {
            SchwerpunktMasse = 0;
            ObjektSchwerpunkt = Vector2.Zero;

            for (int i = 0; i < Bild.Count(); i++)
            {
                int summe = 0;
                for (int b = 0; b < Bild[i].Count; b++)
                {
                    if (b%2 == 1)
                    {
                        SchwerpunktMasse += Bild[i][b];
                        ObjektSchwerpunkt += new Vector2(i, summe + Bild[i][b]/2f)*Bild[i][b];
                    }
                    summe += Bild[i][b];
                }
            }
            ObjektSchwerpunkt /= SchwerpunktMasse;
        }

        /// <summary>
        ///     Hängt neue Massepunkte an das Objekt
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
                float m = Bereiche[i].Z - Bereiche[i].Y + 1;
                neuerPunkt += new Vector2(Bereiche[i].X, Bereiche[i].Y + m/2)*m;
                neueMasse += m;
            }
            neuerPunkt /= neueMasse;
            ObjektSchwerpunkt += neuerPunkt;
            SchwerpunktMasse += neueMasse;
        }

        /// <summary>
        ///     Entfernt Masse vom Objekt
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
                float m = Bereiche[i].Z - Bereiche[i].Y + 1;
                m = -m;
                neuerPunkt += new Vector2(Bereiche[i].X, Bereiche[i].Y + -m/2)*m;
                neueMasse += m;
            }
            neuerPunkt /= neueMasse;
            ObjektSchwerpunkt = ObjektSchwerpunkt*SchwerpunktMasse + neuerPunkt*neueMasse;
            SchwerpunktMasse += neueMasse;
            ObjektSchwerpunkt /= SchwerpunktMasse;
        }

        #endregion Methods
    }
}