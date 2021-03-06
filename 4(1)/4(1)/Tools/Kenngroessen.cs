﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///  Anrechnungsarten
    /// </summary>
    public enum Anteil
    {
        Konstant = 0,
        Fläche = 1
    }
   
    /// <summary>
    ///  das Wachstumsverhalten des Ereignisses auf die Bereiche, vom Ausgangspunkt des Ereignisses gesehen
    /// </summary>
    public enum Wachstum
    {
        Konstant = 0,
        LinearSteigend = 1,
        LinearFallend = 2,
        QuadratischSteigend = 3,
        QuadratischFallend = 4,
    }
   
    /// <summary>
    /// diese Klasse erlaubt die Definition von Bereichen mit bestimmten Werten
    /// </summary>
    public class Bereich
    {
        #region Fields

        /// <summary>
        ///  die Maße des Bereichs
        /// </summary>
        public Rectangle Feld = new Rectangle(0, 0, 0, 0);

        /// <summary>
        ///  die Position im Gitter
        /// </summary>
        public Vector2 Id = Vector2.Zero;

        /// <summary>
        ///  der Wert des Bereichs
        /// </summary>
        public double Wert = 0;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues Bereichs-Objekt
        /// </summary>
        /// <param name="_Id">die Position des Bereichs</param>
        /// <param name="_Wert">der Wert</param>
        /// <param name="_Feld">die Maße des Bereichs</param>
        public Bereich(Vector2 _Id, double _Wert, Rectangle _Feld)
        {
            Id = _Id;
            Wert = _Wert;
            Feld = _Feld;
        }

        #endregion Constructors
    }

    /// <summary>
    ///  diese Klasse stellt eine Verwaltung von Kenngrößen und Methoden zur Manipulation von Bereichen bereit.
    /// </summary>
    public class Kenngroesse
    {
        #region Fields

        /// <summary>
        /// hier werden die berechneten Werte gespeichert (eine Speicherzelle pro Bereich)
        /// </summary>
        public double[,] Bereiche = null;

        /// <summary>
        /// die Spielfeldbreite
        /// </summary>
        public int Breite = 0;

        /// <summary>
        /// die Breite eines Bereichs
        /// </summary>
        public int Feldbreite = 0;

        /// <summary>
        ///  die Anzahl der Felder (Horizontal)
        /// </summary>
        public int FelderAnzahlHorizontal = 0;

        /// <summary>
        /// die Anzahl der Felder (Vertikal)
        /// </summary>
        public int FelderAnzahlVertikal = 0;

        /// <summary>
        /// die Höhe eines Bereichs
        /// </summary>
        public int Feldhoehe = 0;

        /// <summary>
        /// die Gesamthöhe des Spielfeldes
        /// </summary>
        public int Hoehe = 0;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Erzeugt ein neues Kenngroessen objekt
        /// </summary>
        /// <param name="_Gesamtbreite">die Breite des Spielfeldes in Pixeln</param>
        /// <param name="_Gesamthoehe">die Höhe des Spielfeldes in Pixeln</param>
        /// <param name="_Feldbreite">die Breite eines Feldes</param>
        /// <param name="_Feldhoehe">die Höhe eines Feldes</param>
        /// <param name="_Initialwert">der Anfangswert aller Felder</param>
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

        /// <summary>
        /// Gibt einen Bereich anhand seiner Koordinaten im Gitter zurück
        /// </summary>
        /// <param name="_Id">x und y Koordinaten des Bereichs</param>
        /// <returns>der betroffene Bereich</returns>
        public Bereich GibBereichZuId(Vector2 _Id)
        {
            if (_Id.X < 0 || _Id.X >= FelderAnzahlHorizontal || _Id.Y < 0 || _Id.Y >= FelderAnzahlVertikal) return null;
            return new Bereich(_Id, Bereiche[(int)_Id.X, (int)_Id.Y],
                new Rectangle((int)_Id.X * Feldbreite, (int)_Id.Y * Feldhoehe, Feldbreite, Feldhoehe));
        }

        /// <summary>
        /// Gibt einen Bereich zu einer Position zurück
        /// </summary>
        /// <param name="_Position">der betroffene Bereich</param>
        /// <returns>der betroffene Bereich</returns>
        public Bereich GibBereichZuPosition(Vector2 _Position)
        {
            if (_Position.X < 0 || _Position.X >= Breite || _Position.Y < 0 || _Position.Y >= Hoehe) return null;
            var _Id = new Vector2((int)_Position.X / Feldbreite, (int)_Position.Y / Feldhoehe);
            return new Bereich(_Id, Bereiche[(int)_Id.X, (int)_Id.Y],
                new Rectangle((int)_Id.X * Feldbreite, (int)_Id.Y * Feldhoehe, Feldbreite, Feldhoehe));
        }

        /// <summary>
        /// Fügt einen Wert zu allen Bereichen hinzu, die betroffen sind
        /// </summary>
        /// <param name="_Position">der betroffene Bereich</param>
        /// <param name="_Wert">der Gesamtwert</param>
        /// <param name="_Radius">der Radius<</param>
        /// <param name="_Anteil">die Art der Zurechnung</param>
        /// <param name="_Wachstum">die Art des Wachstums, von _Position ausgehend</param>
        public void Hinzufügen(Vector2 _Position, double _Wert, int _Radius, Anteil _Anteil,
            Wachstum _Wachstum)
        {
            return;
            Hinzufügen(_Position, _Wert, _Radius, _Anteil,
            _Wachstum, false);
        }

        /// <summary>
        /// Fügt einen Wert zu allen Bereichen hinzu, die betroffen sind
        /// </summary>
        /// <param name="_Position">der betroffene Bereich</param>
        /// <param name="_Wert">der Gesamtwert</param>
        /// <param name="_Radius">der Radius<</param>
        /// <param name="_Anteil">die Art der Zurechnung</param>
        /// <param name="_Wachstum">die Art des Wachstums, von _Position ausgehend</param>
        /// <param name="Ausgabe">ob die getroffenen Flächen zurückgegeben werden sollen, true = ja, false = nein</param>
        /// <returns>eine Liste Polygonpunkten, welche vom Radius betroffen sind, sofern Ausgabe=true</returns>
        public List<List<Vector2>> Hinzufügen(Vector2 _Position, double _Wert, int _Radius, Anteil _Anteil,
            Wachstum _Wachstum, bool Ausgabe)
        {
            return new List<List<Vector2>>();
            if (_Anteil == Anteil.Konstant)
            {
                return KonstantenWertHinzufügenEinfach(_Position, _Wert, _Radius,
             _Wachstum, Ausgabe);
            }
            else if (_Anteil == Anteil.Fläche)
            {
                return KonstantenWertHinzufügenAnteilig(_Position, _Wert, _Radius,
            _Wachstum, Ausgabe);
            }
            return null;
        }

        /// <summary>
        ///     Fügt einen Wert zu allen Bereichen hinzu, die betroffen sind (anteilig der Trefferfläche)
        /// </summary>
        /// <param name="_Position">die Position</param>
        /// <param name="_Wert">der Wert</param>
        /// <param name="_Radius">der Umkreis, in welchem der Wert wirkt</param>
        /// <param name="_Wachstum">die Art des Wachstums, von _Position ausgehend</param>
        /// <param name="Ausgabe">ob die getroffenen Flächen zurückgegeben werden sollen, true = ja, false = nein</param>
        /// <returns>eine Liste Polygonpunkten, welche vom Radius betroffen sind, sofern Ausgabe=true</returns>
        private List<List<Vector2>> KonstantenWertHinzufügenAnteilig(Vector2 _Position, double _Wert, int _Radius, Wachstum _Wachstum, bool Ausgabe)
        {
            return new List<List<Vector2>>();
            _Position = new Vector2(((int)_Position.X), ((int)_Position.Y));

            int B = (int)Math.Ceiling((double)_Radius / Feldbreite) + 2;
            var Resultat = new List<List<Vector2>>();

            Bereich aktuell = GibBereichZuPosition(_Position);
            if (aktuell == null) return Resultat;

            for (int i = -B - 1; i <= B + 1; i++)
                for (int b = -B - 1; b <= B + 1; b++)
                {
                    if (aktuell.Id.X + i < 0 || aktuell.Id.X + i >= FelderAnzahlHorizontal) continue;
                    if (aktuell.Id.Y + b < 0 || aktuell.Id.Y + b >= FelderAnzahlVertikal) continue;

                    // den aktuellen Bereich zur Bearbeitung auswählen
                    Bereich Kenn = GibBereichZuId(new Vector2(aktuell.Id.X + i, aktuell.Id.Y + b));
                    if (Kenn == null) continue;

                    var Ecken = new List<Vector2>();
                    var Getroffen = new List<bool>();
                    int Treffer = 0;

                    // die 4 Ecken des Bereichs
                    Ecken.Add(new Vector2(Kenn.Feld.X, Kenn.Feld.Y));
                    Ecken.Add(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y));
                    Ecken.Add(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y + Kenn.Feld.Height));
                    Ecken.Add(new Vector2(Kenn.Feld.X, Kenn.Feld.Y + Kenn.Feld.Height));

                    // ist _Position im Bereich
                    if (Kenn.Feld.X <= _Position.X && Kenn.Feld.X + Kenn.Feld.Width >= _Position.X &&
                        Kenn.Feld.Y <= _Position.Y && Kenn.Feld.Y + Kenn.Feld.Height >= _Position.Y)
                    {
                        if (Kenn.Feld.X <= _Position.X - _Radius)
                            Ecken.Add(_Position - new Vector2(_Radius, 0));

                        if (Kenn.Feld.X + Kenn.Feld.Width >= _Position.X + _Radius)
                            Ecken.Add(_Position + new Vector2(_Radius, 0));

                        if (Kenn.Feld.Y <= _Position.Y - _Radius)
                            Ecken.Add(_Position - new Vector2(0, _Radius));

                        if (Kenn.Feld.Y + Kenn.Feld.Height >= _Position.Y + _Radius)
                            Ecken.Add(_Position + new Vector2(0, _Radius));
                    }

                    // Berechnen, welche Ecken im Kreis sind
                    for (int c = 0; c < Ecken.Count; c++)
                        if (Help.Abstand(Ecken[c], _Position) < _Radius)
                        {
                            Getroffen.Add(true);
                            Treffer++;
                        }
                        else
                            Getroffen.Add(false);

                    // Fälle prüfen
                    if (Treffer == 4)
                    {
                        // alle 4 Ecken befinden sich im Kreis, damit volle Anrechnung
                        Bereiche[(int)Kenn.Id.X, (int)Kenn.Id.Y] += WachstumAnwenden(Kenn, Kenn.Feld.Width * Kenn.Feld.Height, _Position, _Wert, _Radius, _Wachstum);

                        if (Ausgabe)
                            Resultat.Add(Ecken);
                        continue;
                    }
                    // Schnittpunkte des Kreises mit dem Bereich berechnen
                    var Schnittpunkte = new List<Vector2>();
                    var SchnittpunktNach = new List<int>();

                    for (int c = 0; c < Ecken.Count; c++)
                    {
                        int d = 0;
                        int EckenAnzahl = Ecken.Count;
                        float x1 = Ecken[c % EckenAnzahl].X;
                        float x2 = Ecken[(c + 1) % EckenAnzahl].X;
                        float y1 = Ecken[c % EckenAnzahl].Y;
                        float y2 = Ecken[(c + 1) % EckenAnzahl].Y;

                        if (x1 == x2)
                        {
                            float w = (_Position.X - x1);
                            w = (float)Math.Sqrt(_Radius * _Radius -
                                          w * w);

                            Vector2 Punkt = new Vector2(x1,
                                    (float)
                                        (_Position.Y -
                                         w));
                            Vector2 Punkt2 = new Vector2(x1,
                                    (float)
                                        (_Position.Y +
                                         w));

                            if ((y1 <= Punkt.Y && Punkt.Y <= y2) ||
                                (y2 <= Punkt.Y && Punkt.Y <= y1))
                            {
                                Schnittpunkte.Add(Punkt);
                                SchnittpunktNach.Add(c + 1 + d);
                            }

                            if ((y1 <= Punkt2.Y && Punkt2.Y <= y2) ||
                               (y2 <= Punkt2.Y && Punkt2.Y <= y1))
                            {
                                Schnittpunkte.Add(Punkt2);
                                SchnittpunktNach.Add(c + 1 + d);
                            }
                        }
                        else if (y1 == y2)
                        {
                            float w = (_Position.Y - y1);
                            w = (float)Math.Sqrt(_Radius * _Radius -
                                          w * w);

                            Vector2 Punkt =
                                    new Vector2(
                                        (float)
                                            (_Position.X -
                                             w),
                                        y1);

                            Vector2 Punkt2 =
                                    new Vector2(
                                        (float)
                                            (_Position.X +
                                             w),
                                        y1);

                            if ((x1 <= Punkt.X && Punkt.X <= x2) ||
                                (x2 <= Punkt.X && Punkt.X <= x1))
                            {
                                Schnittpunkte.Add(Punkt);
                                SchnittpunktNach.Add(c + 1 + d);
                            }

                            if ((x1 <= Punkt2.X && Punkt2.X <= x2) ||
                               (x2 <= Punkt2.X && Punkt2.X <= x1))
                            {
                                Schnittpunkte.Add(Punkt2);
                                SchnittpunktNach.Add(c + 1 + d);
                            }
                        }
                    }

                    if (Schnittpunkte.Count == 0) continue;

                    // Dreieckspunkt berechnen
                    Vector2 Dreieck = Vector2.Zero;
                    if (Schnittpunkte.Count == 2)
                    {
                        float sx1 = Schnittpunkte[0].X;
                        float sx2 = Schnittpunkte[1].X;
                        float sy1 = Schnittpunkte[0].Y;
                        float sy2 = Schnittpunkte[1].Y;
                        Vector2 M = new Vector2((sx1 + sx2) / 2, (sy1 + sy2) / 2);
                        Vector2 _PositionM = M - _Position;
                        float AbstandM = (_PositionM).Length();
                        _PositionM *= _Radius / AbstandM;
                        _PositionM += _Position;
                        Dreieck = _PositionM;
                        int ins = 1;
                        if (!Getroffen[SchnittpunktNach[0] - 1])
                            ins = 0;

                        Schnittpunkte.Insert(ins, _PositionM);
                        SchnittpunktNach.Insert(ins, SchnittpunktNach[0]);
                    }

                    // Schnittpunkte einfügen
                    for (int c = 0; c < Schnittpunkte.Count; c++)
                    {
                        int pos = SchnittpunktNach[c] + c;
                        Ecken.Insert(pos, Schnittpunkte[c]);
                        Getroffen.Insert(pos, true);
                    }

                    // unnötige Ecken entfernen
                    for (int c = 0; c < Ecken.Count; c++)
                        if (!Getroffen[c])
                        {
                            Getroffen.RemoveAt(c);
                            Ecken.RemoveAt(c);
                            c--;
                        }

                    if (Ausgabe)
                        Resultat.Add(Ecken);

                    Bereiche[(int)Kenn.Id.X, (int)Kenn.Id.Y] += WachstumAnwenden(Kenn, (float)Help.PolygonFlaeche(Ecken), _Position, _Wert, _Radius, _Wachstum);
                }
            return Resultat;
        }

        /// <summary>
        ///     Fügt einen Wert zu allen Bereichen hinzu, die betroffen sind (nicht anteilig der Trefferfläche)
        /// </summary>
        /// <param name="_Position">die Position</param>
        /// <param name="_Wert">der Wert</param>
        /// <param name="_Radius">der Umkreis, in welchem der Wert wirkt</param>
        /// <param name="_Wachstum">die Art des Wachstums, von _Position ausgehend</param>
        /// <param name="Ausgabe">ob die getroffenen Flächen zurückgegeben werden sollen, true = ja, false = nein</param>
        /// <returns>eine Liste Polygonpunkten, welche vom Radius betroffen sind, sofern Ausgabe=true</returns>
        private List<List<Vector2>> KonstantenWertHinzufügenEinfach(Vector2 _Position, double _Wert, int _Radius, Wachstum _Wachstum, bool Ausgabe)
        {
            return new List<List<Vector2>>();
            int B = (int)Math.Ceiling((double)_Radius / Feldbreite) + 2;
            var Resultat = new List<List<Vector2>>();

            Bereich aktuell = GibBereichZuPosition(_Position);
            for (int i = -B - 1; i <= B + 1; i++)
                for (int b = -B - 1; b <= B + 1; b++)
                {
                    if (aktuell.Id.X + i < 0 || aktuell.Id.X + i >= FelderAnzahlHorizontal) continue;
                    if (aktuell.Id.Y + b < 0 || aktuell.Id.Y + b >= FelderAnzahlVertikal) continue;

                    Bereich Kenn = GibBereichZuId(new Vector2(aktuell.Id.X + i, aktuell.Id.Y + b));
                    if (Kenn == null) continue;
                    if (Help.Abstand(new Vector2(Kenn.Feld.X, Kenn.Feld.Y), _Position) <= _Radius ||
                        Help.Abstand(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y), _Position) <= _Radius ||
                        Help.Abstand(new Vector2(Kenn.Feld.X, Kenn.Feld.Y + Kenn.Feld.Height), _Position) <= _Radius ||
                        Help.Abstand(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y + Kenn.Feld.Height),
                            _Position) <= _Radius)
                    {
                        Bereiche[(int)Kenn.Id.X, (int)Kenn.Id.Y] += WachstumAnwenden(Kenn, Kenn.Feld.Width * Kenn.Feld.Height, _Position, _Wert, _Radius, _Wachstum);

                        if (Ausgabe)
                        {
                            var Ecken = new List<Vector2>();
                            Ecken.Add(new Vector2(Kenn.Feld.X, Kenn.Feld.Y));
                            Ecken.Add(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y));
                            Ecken.Add(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y + Kenn.Feld.Height));
                            Ecken.Add(new Vector2(Kenn.Feld.X, Kenn.Feld.Y + Kenn.Feld.Height));
                            Resultat.Add(Ecken);
                        }
                    }
                    else
                        if (Kenn.Feld.X <= _Position.X - _Radius && Kenn.Feld.X + Kenn.Feld.Width >= _Position.X + _Radius && Kenn.Feld.Y <= _Position.Y - _Radius && Kenn.Feld.Y + Kenn.Feld.Height >= _Position.Y + _Radius)
                        {
                            Bereiche[(int)Kenn.Id.X, (int)Kenn.Id.Y] += WachstumAnwenden(Kenn, Kenn.Feld.Width * Kenn.Feld.Height, _Position, _Wert, _Radius, _Wachstum);

                            if (Ausgabe)
                            {
                                var Ecken = new List<Vector2>();
                                Ecken.Add(new Vector2(Kenn.Feld.X, Kenn.Feld.Y));
                                Ecken.Add(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y));
                                Ecken.Add(new Vector2(Kenn.Feld.X + Kenn.Feld.Width, Kenn.Feld.Y + Kenn.Feld.Height));
                                Ecken.Add(new Vector2(Kenn.Feld.X, Kenn.Feld.Y + Kenn.Feld.Height));
                                Resultat.Add(Ecken);
                            }
                        }
                }
            return Resultat;
        }

        /// <summary>
        /// berechnet einen _Wert für einen Bereich, anhand seines Abstandes zu _Position
        /// </summary>
        /// <param name="_Bereich">der betroffene Bereich</param>
        /// <param name="_Flache">die getroffene Fläche</param>
        /// <param name="_Position">die Ausgangsposition</param>
        /// <param name="_Wert">der Gesamtwert</param>
        /// <param name="_Radius">der Radius</param>
        /// <param name="_Wachstum">das Wachstumsverhalten, von _Position ausgehend</param>
        /// <returns>der berechnete Wert</returns>
        private double WachstumAnwenden(Bereich _Bereich, float _Flache, Vector2 _Position, double _Wert, int _Radius, Wachstum _Wachstum)
        {
            return 0;
            if (_Wachstum == Wachstum.Konstant)
                return _Wert;

            float FelderAbstand = Help.Abstand(new Vector2(_Bereich.Feld.Center.X, _Bereich.Feld.Center.Y),
                    _Position);
            if (FelderAbstand > _Radius) FelderAbstand = (float)(_Radius - Math.Sqrt(((_Bereich.Feld.Width / 2) * (_Bereich.Feld.Width / 2)) + ((_Bereich.Feld.Height / 2) * (_Bereich.Feld.Height / 2))));

            double WertAnteil = _Wert * (_Flache / (_Bereich.Feld.Width * _Bereich.Feld.Height));
            if (_Wachstum == Wachstum.LinearFallend)
            {
                return WertAnteil * (1.0d - (double)FelderAbstand / _Radius);
            }

            if (_Wachstum == Wachstum.LinearSteigend)
            {
                return WertAnteil * ((double)FelderAbstand / _Radius);
            }

            if (_Wachstum == Wachstum.QuadratischFallend)
            {
                double faktor = ((double)FelderAbstand / _Radius);
                return WertAnteil * (1 - faktor * faktor);
            }

            if (_Wachstum == Wachstum.QuadratischSteigend)
            {
                double faktor = ((double)FelderAbstand / _Radius);
                return WertAnteil * (faktor * faktor);
            }

            return 0;
        }

        #endregion Methods
    }
}