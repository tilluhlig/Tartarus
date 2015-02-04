using System;
namespace _4_1_
{
    public interface ISpieler
    {
        int _ActionPoints { get; }
        System.Collections.Generic.List<float> _Angle { get; }
        System.Collections.Generic.List<int> _Cooldown { get; }
        System.Collections.Generic.List<int> _CurrentLv { get; }
        int _CurrentWeapon { get; }
        System.Collections.Generic.List<EffectPacket> _Effekte { get; }
        System.Collections.Generic.List<int> _ExpNow { get; }
        System.Collections.Generic.List<int> _hp { get; }
        System.Collections.Generic.List<int> _KindofTank { get; }
        System.Collections.Generic.List<Mine> _Minen { get; }
        System.Collections.Generic.List<int>[] _Munition { get; }
        System.Collections.Generic.List<string> _Namen { get; }
        System.Collections.Generic.List<bool> _overreach { get; }
        System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> _pos { get; }
        System.Collections.Generic.List<Inventar> _Rucksack { get; }
        System.Collections.Generic.List<Tunnel> _TunnelAnlage { get; }
        System.Collections.Generic.List<float> _vehikleAngle { get; }
        System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> _Zielpos { get; }
        int CurrentTank { get; set; }
        int getPanzerID(int id, System.Collections.Generic.List<int> Liste);
        int GibArtillerieAnzahl();
        int GibBaufahrzeugeAnzahl();
        int GibBunkerAnzahl(Bunker Bunkeranlagen, int id);
        int GibFabrikenAnzahl(Haus Haeuser, int id);
        int GibGeschützAnzahl();
        int GibGeschütz2Anzahl();
        int GibHändlerAnzahl(Haus Haeuser, int id);
        int GibHäuserAnzahl(Haus Haeuser, int id);
        Microsoft.Xna.Framework.Vector2 GibLinkenTunnel();
        int GibPanzer();
        Microsoft.Xna.Framework.Vector2 GibRechtenTunnel();
        int GibScout();
        int GibTunnelAnAktuellerPanzerposition();
        bool Links(System.Collections.Generic.List<ushort>[] Spielfeld, int id);
        System.Collections.Generic.List<int> OrdneEigenePanzerAnhandDerKarte();
        bool Rechts(System.Collections.Generic.List<ushort>[] Spielfeld, int id, Microsoft.Xna.Framework.Vector2 Fenster);
        bool Rohr_Links(int id);
        bool Rohr_Rechts(int id);
        Microsoft.Xna.Framework.Vector2 Rohrspitze(int c);
        void SetzeFahrzeugname(string neuerName, int id);
    }
}
