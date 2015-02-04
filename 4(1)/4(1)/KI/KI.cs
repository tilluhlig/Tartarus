using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _4_1_;

namespace _KI
{
    public interface IKI
    {
        void Rechne(ISpiel Spiel, ISpieler Spieler, ISpieler[] Gegner, Vector2 Fenster);
    }

    /// <summary>
    ///     Die Klasse dient der Steuerung von nicht-menschlichen Mitspielern
    /// </summary>
    public abstract class KI
    {
        #region Fields

        /// <summary>
        ///     die ID dieses Spielers im Spiel
        /// </summary>
        public int SpielerID = 0;

        /// <summary>
        ///     die KI-Stärke des Spielers (0-100)
        /// </summary>
        public int Staerke = 0;

        public String Name = "Unbenannt";

        #endregion Fields

        #region Methods

        /// <summary>
        ///     die ID dieses Spielers im Spiel
        /// </summary>
        /// <param name="Spiel2">ein Spielobjekt, auf dem gerechnet werden soll</param>
        public abstract void Rechne(ISpiel Spiel, ISpieler Spieler, ISpieler[] Gegner, Vector2 Fenster);

        #endregion Methods
    }
}
