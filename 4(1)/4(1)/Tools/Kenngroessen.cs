using System;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    public class Bereich
    {
        #region Fields

        public Rectangle Feld = new Rectangle(0, 0, 0, 0);
        public Vector2 Id = Vector2.Zero;
        public double Wert = 0;

        #endregion Fields

        #region Constructors

        public Bereich(Vector2 _Id, double _Wert, Rectangle _Feld)
        {
            Id = _Id;
            Wert = _Wert;
            Feld = _Feld;
        }

        #endregion Constructors
    }

    public class Kenngroesse
    {
        #region Fields

        public double[,] Bereiche = null;
        public int Breite = 0;
        public int Feldbreite = 0;
        public int FelderAnzahlHorizontal = 0;
        public int FelderAnzahlVertikal = 0;
        public int Feldhoehe = 0;
        public int Hoehe = 0;

        #endregion Fields

        #region Constructors

        public Kenngroesse(int _Gesamtbreite, int _Gesamthoehe, int _Feldbreite, int _Feldhoehe, double _Initialwert)
        {
            Breite = _Gesamtbreite;
            Hoehe = _Gesamthoehe;
            Feldbreite = _Feldbreite;
            Feldhoehe = _Feldhoehe;

            FelderAnzahlHorizontal = (int)Math.Ceiling((double)_Gesamtbreite / _Feldbreite);
            FelderAnzahlVertikal = (int)Math.Ceiling((double)_Gesamthoehe / _Feldhoehe);

            Bereiche = new double[FelderAnzahlHorizontal, FelderAnzahlVertikal];
            for (int i = 0; i < FelderAnzahlHorizontal; i++)
                for (int b = 0; b < FelderAnzahlVertikal; b++)
                    Bereiche[i, b] = _Initialwert;
        }

        #endregion Constructors

        #region Methods

        public Bereich GibBereichZuId(Vector2 _Id)
        {
            if (_Id.X < 0 || _Id.X >= FelderAnzahlHorizontal || _Id.Y < 0 || _Id.Y >= FelderAnzahlVertikal) return null;
            return new Bereich(_Id, Bereiche[(int)_Id.X, (int)_Id.Y], new Rectangle((int)_Id.X * Feldbreite, (int)_Id.Y * Feldhoehe, Feldbreite, Feldhoehe));
        }

        public Bereich GibBereichZuPosition(Vector2 _Position)
        {
            if (_Position.X < 0 || _Position.X >= Breite || _Position.Y < 0 || _Position.Y >= Hoehe) return null;
            Vector2 _Id = new Vector2((int)_Position.X / Feldbreite, (int)_Position.Y / Feldhoehe);
            return new Bereich(_Id, Bereiche[(int)_Id.X, (int)_Id.Y], new Rectangle((int)_Id.X * Feldbreite, (int)_Id.Y * Feldhoehe, Feldbreite, Feldhoehe));
        }

        /// <summary>
        /// Fügt einen Wert zu allen Bereichen hinzu, die betroffen sind
        /// </summary>
        /// <param name="_Position">die Position</param>
        /// <param name="_Wert">der Wert</param>
        /// <param name="_Radius">der Umkreis, in welchem der Wert wirkt</param>
        public void StatischenWertHinzufügen(Vector2 _Position, double _Wert, int _Radius)
        {
            int B = (int)Math.Ceiling((double)_Radius / Feldbreite) + 2;

            Bereich aktuell = GibBereichZuPosition(_Position);
            for (int i = -B - 1; i <= B + 1; i++)
                for (int b = -B - 1; b <= B + 1; b++)
                {
                    //if (aktuell.Id.X + i < 0 || aktuell.Id.X + i >= FelderAnzahlHorizontal) continue;
                    //if (aktuell.Id.Y + b < 0 || aktuell.Id.Y + b >= FelderAnzahlVertikal) continue;

                    Bereich Kenn = GibBereichZuId(new Vector2(aktuell.Id.X + i, aktuell.Id.Y + b));
                    if (Kenn == null) continue;
                     if (Help.Abstand(new Vector2(Kenn.Feld.X, Kenn.Feld.Y), _Position) <= _Radius|| Help.Abstand(new Vector2(Kenn.Feld.X + Kenn.Feld.Width,Kenn.Feld.Y), _Position) <= _Radius || Help.Abstand(new Vector2(Kenn.Feld.X,Kenn.Feld.Y + Kenn.Feld.Height), _Position) <= _Radius || Help.Abstand(new Vector2(Kenn.Feld.X+Kenn.Feld.Width,Kenn.Feld.Y+Kenn.Feld.Height), _Position) <= _Radius) //
                   /* if (Kenn.Feld.X >= _Position.X - _Radius)
                        if (Kenn.Feld.Y >= _Position.Y - _Radius)
                            if (Kenn.Feld.X <= _Position.X + _Radius)
                                if (Kenn.Feld.Y <= _Position.Y + _Radius) */
                                {
                                    Bereiche[(int)Kenn.Id.X, (int)Kenn.Id.Y] += _Wert;
                                }
                }
        }

        #endregion Methods
    }
}