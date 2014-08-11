using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ReaderStream
{
    /// <summary>
    /// diese Klasse stellt Funktionen zum einlesen einer Datei oder eines Streams bereit
    /// </summary>
    public class ReaderStream
    {
        #region Fields

        /// <summary>
        ///     Gibt an, ob das ende der datei erreicht wurde, true = ende erreicht
        /// </summary>
        public bool EndOfStream = false;

        /// <summary>
        ///     Ignoriert nächstes zu lesendes Zeichen
        /// </summary>
        private bool IgnoriereNächstesZeichen;

        /// <summary>
        ///     Das Lesegerät
        /// </summary>
        private BinaryReader Lesegerät;

        /// <summary>
        ///     Die Leseposition in der Datei
        /// </summary>
        private int Leseposition;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Konstruktor für einen neuen StreamReader
        /// </summary>
        /// <param name="Datei">The datei.</param>
        public ReaderStream(String Datei)
        {
            Lesegerät = new BinaryReader(File.Open(Datei, FileMode.Open, FileAccess.Read));
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Berechnet den HASH-Wert einer angegeben Datei
        /// </summary>
        /// <param name="Datei">der Dateipfad</param>
        /// <returns>ein String mit dem HASH</returns>
        public static String HASH(String Datei)
        {
            var daten = new ReaderStream(Datei);
            var a = new MemoryStream();

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
        ///   Berechnet den HASH-Wert eines Streams
        /// </summary>
        /// <param name="Daten">der Stream</param>
        /// <returns>ein String mit dem HASH</returns>
        public static String HASH(Stream Daten)
        {
            MD5 hash = MD5.Create();
            char[] g = Encoding.ASCII.GetChars(hash.ComputeHash(Daten));
            String q = "";
            for (int i = 0; i < g.Length; i++)
            {
                int a = g[i];
                q = q + a;
            }

            //String q = new string(g);
            return q;
        }

        /// <summary>
        ///     Schließt den Stream und gibt den Speicher frei
        /// </summary>
        public void Close()
        {
            if (Lesegerät != null)
            {
                Lesegerät.Close();
                Lesegerät = null;
            }
        }

        /// <summary>
        /// Liest das nächste Byte aus dem Lesegerät
        /// </summary>
        /// <returns>das gelesene Byte, setzt EndOfStream=true, wenn das Ende erreicht wurde</returns>
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
        ///     Liest die nächste Zeile aus dem Stream
        /// </summary>
        /// <returns>die gelesene Zeile, setzt EndOfStream=true, wenn das Ende erreicht wurde</returns>
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

        #endregion Methods
    }
}