using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    public interface ISpiel
    {
        List<UInt16>[] _Spielfeld { get; }
        void Current_Left();
        void Current_Right();
        bool Current_Rohr_Left();
        bool Current_Rohr_Right();
        void Links(int player, int id);
        int PrüfeAktiveSpieler();
        bool PrüfeBau(Microsoft.Xna.Framework.Vector2 pos, int radius);
        bool PrüfeBunkerbau(Microsoft.Xna.Framework.Vector2 pos);
        bool PrüfeTunnelbau(Microsoft.Xna.Framework.Vector2 pos);
        void Rechts(int player, int id);
    }
}
