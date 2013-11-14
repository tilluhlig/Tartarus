using System;
using System.IO;

namespace _4_1_
{
    /// <summary>
    /// 
    /// </summary>
    public class StreamReader
    {
        /// <summary>
        /// Das Lesegerät
        /// </summary>
        private BinaryReader Lesegerät=null;

        /// <summary>
        /// Gibt an, ob das ende der datei erreicht wurde, true = ende erreicht
        /// </summary>
        public bool EndOfStream = false;

        /// <summary>
        /// Die Leseposition in der Datei
        /// </summary>
        private int Leseposition=0;

        /// <summary>
        /// Ignoriert nächstes zu lesendes Zeichen
        /// </summary>
        private bool IgnoriereNächstesZeichen = false;

        /// <summary>
        /// Konstruktor für einen neuen StreamReader
        /// </summary>
        /// <param name="Datei">The datei.</param>
        public StreamReader(String Datei){
           Lesegerät = new BinaryReader(File.Open(Datei, FileMode.Open,System.IO.FileAccess.Read));
        }

        /// <summary>
        /// Schließt den Stream und gibt den Speicher frei
        /// </summary>
        public void Close(){
            if (Lesegerät!=null){
                Lesegerät.Close();
                Lesegerät=null;
            }
        }

        public byte ReadByte()
        {
          if (Leseposition < Lesegerät.BaseStream.Length)
            {
                Leseposition++;
                    if (Leseposition >= Lesegerät.BaseStream.Length)
                        EndOfStream = true;
                return Lesegerät.ReadByte();
            }

            EndOfStream = true;
            return 0;
        }

        /// <summary>
        /// Liest die nächste Zeile aus dem Stream
        /// </summary>
        /// <returns></returns>
        public String ReadLine(){
            String data="";

            while (Leseposition < Lesegerät.BaseStream.Length)
                    {
                        byte q = Lesegerät.ReadByte();
                if (IgnoriereNächstesZeichen && q=='\n'){
                    IgnoriereNächstesZeichen = false;
                    Leseposition++;
                    continue;
                }

                        if (q != '\n' && q != '\r')
                        {
                            data = data + (char) q;
                        }
                        else
                        {
                            if (q == '\r') IgnoriereNächstesZeichen = true;
                            Leseposition++;
                            return data;
                        }
                        Leseposition++;
                    }

            EndOfStream = true;
            return data;
        }
  
    }
}
