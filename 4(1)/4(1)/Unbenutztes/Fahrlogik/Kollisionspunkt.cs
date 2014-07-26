using Microsoft.Xna.Framework;

namespace _4_1_
{
    public class Kollisionspunkt
    {
        #region Fields

        private readonly Vector2 Position;

        #endregion Fields

        #region Constructors

        public Kollisionspunkt(Vector2 _Position)
        {
            Position = _Position;
        }

        #endregion Constructors

        #region Methods

        public bool IsCollision(float angle, Vector2 Bezugspunkt, Vector2 Anheftpunkt)
        {
            Vector2 pos = Position + Anheftpunkt + Bezugspunkt;

            // Rotation ausgleichen
            pos = Help.RotatePosition(Bezugspunkt, angle, pos);

            return Kartenformat.isSet(pos);
        }

        #endregion Methods

        // die relative Position
    }
}