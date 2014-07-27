using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    public class Rad
    {
        #region Fields

        public List<Kollisionspunkt> Antriebspunkte = new List<Kollisionspunkt>();
        public Texture2D Bild;
        public float Bremswirkung = 0;
        public Stoßdämpfer Dämpfer;
        public Vector2 Energie = Vector2.Zero;
        public List<Kollisionspunkt> Kollisionspunkte = new List<Kollisionspunkt>();
        public float Potenzielle = 0;
        public float Reibung;
        public Vector2 pos; // das ist der mittelpunkt des Rades

        #endregion Fields

        #region Constructors

        public Rad(Vector2 _pos, float _Reibung, Texture2D _Bild)
        {
            pos = _pos;
            Reibung = _Reibung;
            Bild = _Bild;
            AddAntriebspunkt(new Vector2(0, Bild.Height / 2 + 1));
        }

        #endregion Constructors

        #region Methods

        public void AddAntriebspunkt(Vector2 Position) // Die Position ist relativ zum Mittelpunkt des Rades
        {
            Antriebspunkte.Add(new Kollisionspunkt(Position + new Vector2(0, 0)));
            Kollisionspunkte.Add(new Kollisionspunkt(Position + new Vector2(0, -3)));
        }

        public bool IsCollision(float angle, Vector2 Bezugspunkt)
        {
            for (int i = 0; i < Kollisionspunkte.Count; i++)
                if (Kollisionspunkte[i].IsCollision(angle, Bezugspunkt, -pos))
                    return true;
            return false;
        }

        public bool IsKontakt(float angle, Vector2 Bezugspunkt)
        {
            for (int i = 0; i < Antriebspunkte.Count; i++)
                if (Antriebspunkte[i].IsCollision(angle, Bezugspunkt, -pos))
                    return true;
            return false;
        }

        #endregion Methods
    }
}