using Microsoft.Xna.Framework;

namespace _4_1_
{
    public class Kollisionspunkt
    {
        private Vector2 Position; // die relative Position

        public Kollisionspunkt(Vector2 _Position)
        {
            Position = _Position;
        }

        public bool IsCollision(float angle, Vector2 Bezugspunkt, Vector2 Anheftpunkt)
        {
            Vector2 pos = Position + Anheftpunkt + Bezugspunkt;

            // Rotation ausgleichen
            pos = Help.RotatePosition(Bezugspunkt, angle, pos);

            return Kartenformat.isSet(pos);
        }
    }
}