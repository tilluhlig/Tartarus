namespace _4_1_
{
    // Last Modified On : 01.04.14 by Anton
    public static class Allgemein
    {
        #region Fields

        /// <summary>
        ///     MOD-Variable, maximale Anzahl BesitzerPunkten, die man bei der Eroberung eines Gebäudes erreichen kann
        ///     umso mehr "Besitz" man an einem Gebäude hat, umso länger dauert die Eroberung durch einen fremden Spieler
        /// </summary>
        public static int MaxBesitzerPunkte;

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Bunkern, pro Spieler
        /// </summary>
        public static int MaxBunker;

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Fahrzeugen, pro Spieler
        /// </summary>
        public static int MaxFahrzeug;

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Geschütztürmen, pro Spieler
        /// </summary>
        public static int MaxGeschuetze;

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Tunneln, pro Spieler
        /// </summary>
        public static int MaxTunnel;

        /// <summary>
        ///     MOD-Variable, regelt den Anteil der "Eroberung" an einem Gebäude, damit man das Haus noch im eigenen Bestitz hat
        ///     sinken die BesitzerPunkte durch eine fremde Eroberung unter diesen Wert, wird das Gebäude neutral
        /// </summary>
        public static int MinBesitzerPunkte;

        /// <summary>
        ///     MOD-Variable, wielange wird eine Minenexplosion verzögert
        /// </summary>
        public static int Minenverzögerung;

        /// <summary>
        ///     MOD-Variable, wieviel Kostet der Kauf einer Einheit Treibstoff
        /// </summary>
        public static float TreibstoffPreis;

        /// <summary>
        ///     MOD-Variable, wieviel kostet die Nutzung eines Tunnels
        /// </summary>
        public static int TunnelAPKosten;

        #endregion Fields

        #region Privat

        /// <summary>
        ///     MOD-Variable, maximale Anzahl BesitzerPunkten, die man bei der Eroberung eines Gebäudes erreichen kann
        ///     umso mehr "Besitz" man an einem Gebäude hat, umso länger dauert die Eroberung durch einen fremden Spieler
        /// </summary>
        private static Var<int> MOD_MaxBesitzerPunkte = new Var<int>("MAXBESITZERPUNKTE", 1000, ref MaxBesitzerPunkte);

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Bunkern, pro Spieler
        /// </summary>
        private static Var<int> MOD_MaxBunker = new Var<int>("MAXBUNKER", 5, ref MaxBunker);

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Fahrzeugen, pro Spieler
        /// </summary>
        private static Var<int> MOD_MaxFahrzeug = new Var<int>("MAXFAHRZEUG", 20, ref MaxFahrzeug);

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Geschütztürmen, pro Spieler
        /// </summary>
        private static Var<int> MOD_MaxGeschuetze = new Var<int>("MAXGESCHUETZE", 10, ref MaxGeschuetze);

        /// <summary>
        ///     MOD-Variable, maximale Anzahl an Tunneln, pro Spieler
        /// </summary>
        private static Var<int> MOD_MaxTunnel = new Var<int>("MAXTUNNEL", 5, ref MaxTunnel);

        /// <summary>
        ///     MOD-Variable, regelt den Anteil der "Eroberung" an einem Gebäude, damit man das Haus noch im eigenen Bestitz hat
        ///     sinken die BesitzerPunkte durch eine fremde Eroberung unter diesen Wert, wird das Gebäude neutral
        /// </summary>
        private static Var<int> MOD_MinBesitzerPunkte = new Var<int>("MINBESITZERPUNKTE", 250, ref MinBesitzerPunkte);

        /// <summary>
        ///     MOD-Variable, wielange wird eine Minenexplosion verzögert
        /// </summary>
        private static Var<int> MOD_Minenverzögerung = new Var<int>("MINENVERZOEGERUNG", 60*60*5, ref Minenverzögerung);

        /// <summary>
        ///     MOD-Variable, wieviel Kostet der Kauf einer Einheit Treibstoff
        /// </summary>
        private static Var<float> MOD_TreibstoffPreis = new Var<float>("TREIBSTOFFPREIS", 0.1f, ref TreibstoffPreis);

        /// <summary>
        ///     MOD-Variable, wieviel kostet die Nutzung eines Tunnels
        /// </summary>
        private static Var<int> MOD_TunnelAPKosten = new Var<int>("TUNNELAPKOSTEN", 25, ref TunnelAPKosten);

        #endregion Privat
    }
}