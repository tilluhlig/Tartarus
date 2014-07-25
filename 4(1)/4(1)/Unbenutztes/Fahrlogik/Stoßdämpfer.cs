namespace _4_1_
{
    public class Stoßdämpfer
    {
        public int Laenge;
        public int MaxLaenge;
        public float Haerte;

        public Stoßdämpfer(int _MaxLaenge, float _Haerte)
        {
            MaxLaenge = _MaxLaenge;
            Laenge = MaxLaenge;
            Haerte = _Haerte;
        }
    }
}