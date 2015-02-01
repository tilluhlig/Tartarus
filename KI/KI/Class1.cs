using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KI
{
    public interface IKI
    {
        int Aufruf(int Wuerfel);
        void OnFailure();
    }

   abstract public class KI
    {
       public void Funktion()
       {

       }

    }
}
