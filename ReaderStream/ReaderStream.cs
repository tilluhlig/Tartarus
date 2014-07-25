using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ReaderStream
{
    public class ReaderStream
    {
        public static String HASH(String Datei)
        {
            ReaderStream daten = new ReaderStream(Datei);
            MemoryStream a = new MemoryStream();

            int pos = 0;
            while (!daten.EndOfStream && pos < 1024)
            {
                a.WriteByte(daten.ReadByte());
                pos++;
            }
            daten.Close();

            a.Position = 0;

            String q = HASH(a);
            a.Close();
            return q;
        }

        /// <summary>
        /// HASHs the specified daten.
        /// </summary>
        /// <param name="Daten">The daten.</param>
        /// <returns>String.</returns>
        public static String HASH(Stream Daten)
        {
            MD5 hash = MD5.Create();
            char[] g = Encoding.ASCII.GetChars(hash.ComputeHash(Daten));
            String q = "";
            for (int i = 0; i < g.Length; i++)
            {
                int a = g[i];
                q = q + a.ToString();
            }

            //String q = new string(g);
            return q;
        }

        /// <summary>
        /// Das Lesegerät
        /// </summary>
        private BinaryReader Lesegerät = null;

        /// <summary>
        /// Gibt an, ob das ende der datei erreicht wurde, true = ende erreicht
        /// </summary>
        public bool EndOfStream = false;

        /// <summary>
        /// Die Leseposition in der Datei
        /// </summary>
        private int Leseposition = 0;

        /// <summary>
        /// Ignoriert nächstes zu lesendes Zeichen
        /// </summary>
        private bool IgnoriereNächstesZeichen = false;

        /// <summary>
        /// Konstruktor für einen neuen StreamReader
        /// </summary>
        /// <param name="Datei">The datei.</param>
        public ReaderStream(String Datei)
        {
            Lesegerät = new BinaryReader(File.Open(Datei, FileMode.Open, System.IO.FileAccess.Read));
        }

        /// <summary>
        /// Schließt den Stream und gibt den Speicher frei
        /// </summary>
        public void Close()
        {
            if (Lesegerät != null)
            {
                Lesegerät.Close();
                Lesegerät = null;
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
        public String ReadLine()
        {
            String data = "";

            while (Leseposition < Lesegerät.BaseStream.Length)
            {
                byte q = Lesegerät.ReadByte();
                if (IgnoriereNächstesZeichen && q == '\n')
                {
                    IgnoriereNächstesZeichen = false;
                    Leseposition++;
                    continue;
                }

                if (q != '\n' && q != '\r')
                {
                    data = data + (char)q;
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