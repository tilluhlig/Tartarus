using Microsoft.Xna.Framework;

namespace _4_1_
{
    /// <summary>
    ///     das sind die IDs der möglichen Kollisionsarten
    /// </summary>
    public enum Kollisionsart
    {
        BUNKER = 6,
        FABRIK = 2,
        FAHRZEUG = 7,
        GELAENDE = 0,
        GESCHUETZ = 8,
        HAUS = 1,
        KISTE = 5,
        TRASH = 4,
        WAFFENHAENDLER = 3
    }

    /// <summary>
    ///     Die Klasse beschreibt eine Kollision, wo und unter welchen Umständen
    /// </summary>
    public class Kollision
    {

    #region Fields

        /// <summary>
        ///     die Art der Kollision
        /// </summary>
        public int Art = 0;

        /// <summary>
        ///     wem das getroffene gehört
        ///     -1 = niemand
        ///     >=0 = ein Spieler
        /// </summary>
        public int Besitzer = 0;

        /// <summary>
        ///     welche ID besitzt das getroffene im entsprechenden Zusammenhang
        ///     Bsp.: Art = Fahrzeug, Sorte = Artillerie, Besitzer = 0, ObjektID = 5 <- das Fahrzeug mit der ID 5, des Spielers
        /// </summary>
        public int ObjektID = 0;

        /// <summary>
        ///     die Position der Kollision
        /// </summary>
        public Vector2 Position = Vector2.Zero;

        /// <summary>
        ///     welche Sorte der getroffenen Art ist es genau
        ///     Bsp.: Art = Fahrzeug, Sorte = Artillerie
        ///     wird durch einzelne Klassen selbst definiert, welcher Wert, welche Bedeutung hat
        /// </summary>
        public int Sorte = 0;

        /// <summary>
        ///     gab es einen Treffer?
        ///     zur schnellen Prüfung
        /// </summary>
        public bool Treffer = false;

        /// <summary>
        ///     ist das getroffene Zerstörbar?
        /// </summary>
        public bool Zerstoerbar = true;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     einfacher Konstruktor
        /// </summary>
        public Kollision()
        {
        }

        /// <summary>
        ///     umfangreicher Konstruktor
        /// </summary>
        /// <param name="_Position">die Position der Kollision</param>
        /// <param name="_Art">die Art der Kollision, welche Obergruppe wurde getroffen</param>
        /// <param name="_Sorte">die Sorte des getroffenen</param>
        /// <param name="_ObjektID">die eigentliche ID des getroffenen</param>
        /// <param name="_Besitzer">wem gehört das getroffene</param>
        /// <param name="_Treffer">gab es eine Kollision?</param>
        /// <param name="_Zerstoerbar">ist das getroffene Zerstörbar?</param>
        public Kollision(Vector2 _Position, int _Art, int _Sorte, int _ObjektID, int _Besitzer, bool _Treffer,
            bool _Zerstoerbar)
        {
            Position = _Position;
            Art = _Art;
            Sorte = _Sorte;
            ObjektID = _ObjektID;
            Besitzer = _Besitzer;
            Treffer = _Treffer;
            Zerstoerbar = _Zerstoerbar;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        ///     Konstruktor für alle Werte
        /// </summary>
        /// <param name="_Position">die Position, welche auf eine Kollision geprüft werden soll</param>
        /// <returns>ein Kollisionsobjekt, welches beschreibt, ob es an dieser Stelle einen Treffer gab</returns>
        public static Kollision pruefen(Vector2 Position)
        {
            return new Kollision(Position, 0, 0, 0, -1, false, true);
        }

        #endregion Methods
    }
}