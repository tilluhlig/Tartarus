using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///     Ermöglicht das Nutzen von Kraftvektoren
    /// </summary>
    public class Kraft
    {
        #region Fields

        /// <summary>
        ///     die Position an welcher der Vektor wirkt (von Links-Oben)
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        ///     der Vektor des Kraftvektors
        /// </summary>
        public Vector2 Wert = Vector2.Zero;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Erzeugt eine Kraft
        /// </summary>
        /// <param name="_Wert">der Kraftvektor</param>
        /// <param name="_Position">die Position</param>
        public Kraft(Vector2 _Wert, Vector2 _Position)
        {
            Wert = _Wert;
            Position = _Position;
        }

        /// <summary>
        ///     Erzeugt eine Kraft
        /// </summary>
        /// <param name="_PositionA">der Ausgangspunkt der Kraft</param>
        /// <param name="_PositionB">die Position, an welcher der Vektor wirkt</param>
        /// <param name="Laenge">die Länge der Kraft</param>
        public Kraft(Vector2 _PositionA, Vector2 _PositionB, float Laenge)
        {
            float tempLaenge = (_PositionB - _PositionA).Length();
            Vector2 tempVector = _PositionB - _PositionA;
            Wert = new Vector2(Laenge*tempVector.X/tempLaenge, Laenge*tempVector.Y/tempLaenge);
            Position = _PositionB;
        }

        #endregion Constructors
    }
}