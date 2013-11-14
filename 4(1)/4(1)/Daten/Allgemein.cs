namespace _4_1_
{
    public static class Allgemein
    {
        public static int MaxBesitzerPunkte;
        public static int MaxBunker;
        public static int MaxFahrzeug;
        public static int MaxGeschuetze;
        public static int MaxTunnel;
        public static int MinBesitzerPunkte;
        public static int Minenverzögerung;
        public static float TreibstoffPreis;
        public static int TunnelAPKosten;

        #region Privat

        private static Var<int> MOD_MaxBesitzerPunkte = new Var<int>("MAXBESITZERPUNKTE", 1000, ref MaxBesitzerPunkte);
        private static Var<int> MOD_MaxBunker = new Var<int>("MAXBUNKER", 5, ref MaxBunker);
        private static Var<int> MOD_MaxFahrzeug = new Var<int>("MAXFAHRZEUG", 20, ref MaxFahrzeug);
        private static Var<int> MOD_MaxGeschuetze = new Var<int>("MAXGESCHUETZE", 10, ref MaxGeschuetze);
        private static Var<int> MOD_MaxTunnel = new Var<int>("MAXTUNNEÖ", 5, ref MaxTunnel);
        private static Var<int> MOD_MinBesitzerPunkte = new Var<int>("MINBESITZERPUNKTE", 250, ref MinBesitzerPunkte);
        private static Var<int> MOD_Minenverzögerung = new Var<int>("MINENVERZÖGERUNG", 60 * 60 * 5, ref Minenverzögerung);
        private static Var<float> MOD_TreibstoffPreis = new Var<float>("TREIBSTOFFPREIS", 0.1f, ref TreibstoffPreis);
        private static Var<int> MOD_TunnelAPKosten = new Var<int>("TunnelAPKosten", 25, ref TunnelAPKosten);

        #endregion Privat
    }
}