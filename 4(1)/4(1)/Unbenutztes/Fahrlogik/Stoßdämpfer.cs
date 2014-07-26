namespace _4_1_
{
    public class Stoßdämpfer
    {
        #region Fields

        public float Haerte;
        public int Laenge;
        public int MaxLaenge;

        #endregion Fields

        #region Constructors

        public Stoßdämpfer(int _MaxLaenge, float _Haerte)
        {
            MaxLaenge = _MaxLaenge;
            Laenge = MaxLaenge;
            Haerte = _Haerte;
        }

        #endregion Constructors
    }
}