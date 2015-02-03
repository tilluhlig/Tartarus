using System;
using System.IO;
using SevenZip;
using SevenZip.Compression.LZMA;
using System;
using System.Collections.Generic;
using System.IO;

namespace _4_1_.Kompression
{
    /// <summary>
    ///     Klasse nutzt LZMA Kompression
    /// </summary>
    public static class Kompression
    {
        #region Methods

        /// <summary>
        ///     Dekomprimiert Stream
        /// </summary>
        /// <param name="Input">Eingabestream</param>
        /// <param name="Output">Ausgabedatei</param>
        public static void Dekomprimiere(String Output, Stream Input)
        {
            Stream inStream = Input;

            FileStream outStream = null;

            string outputName = Output;
            outStream = new FileStream(outputName, FileMode.Create, FileAccess.Write);

            var properties = new byte[5];
            if (inStream.Read(properties, 0, 5) != 5)
                throw (new Exception("input .lzma is too short"));
            var decoder = new Decoder();
            decoder.SetDecoderProperties(properties);

            long outSize = 0;
            for (int i = 0; i < 8; i++)
            {
                int v = inStream.ReadByte();
                if (v < 0)
                    throw (new Exception("Can't Read 1"));
                outSize |= ((long)(byte)v) << (8 * i);
            }
            long compressedSize = inStream.Length - inStream.Position;

            decoder.Code(inStream, outStream, compressedSize, outSize, null);

            outStream.Close();
        }

        public static List<String> Dekomprimiere(Stream Input)
        {
            Stream inStream = Input;
            List<String> Data = new List<String>();
            MemoryStream outStream = null;
            outStream = new MemoryStream();

            var properties = new byte[5];
            if (inStream.Read(properties, 0, 5) != 5)
                throw (new Exception("input .lzma is too short"));
            var decoder = new Decoder();
            decoder.SetDecoderProperties(properties);

            long outSize = 0;
            for (int i = 0; i < 8; i++)
            {
                int v = inStream.ReadByte();
                if (v < 0)
                    throw (new Exception("Can't Read 1"));
                outSize |= ((long)(byte)v) << (8 * i);
            }
            long compressedSize = inStream.Length - inStream.Position;

            decoder.Code(inStream, outStream, compressedSize, outSize, null);

            outStream.Position = 0;
            StreamReader q = new StreamReader(outStream);
            for (; !q.EndOfStream; ) Data.Add(q.ReadLine());
            outStream.Close();
            return Data;
        }

        /// <summary>
        ///     Komprimiert eine Datei
        /// </summary>
        /// <param name="Input">Eingabedatei</param>
        /// <param name="Output">Ausgabedatei</param>
        public static void Komprimiere(String Input, String Output)
        {
            var dat = new ReaderStream.ReaderStream(Input);
            var datei = new MemoryStream();
            while (!dat.EndOfStream)
                datei.WriteByte(dat.ReadByte());

            dat.Close();
            datei.Position = 0;

            Komprimiere(datei, Output);
            datei.Close();
        }

        /// <summary>
        ///     Komprimiert einen Stream
        /// </summary>
        /// <param name="Input">Eingabestream</param>
        /// <param name="Output">Ausgabedatei</param>
        public static void Komprimiere(Stream Input, String Output)
        {
            Stream inStream = Input;

            FileStream outStream = null;

            string outputName = Output;
            outStream = new FileStream(outputName, FileMode.Create, FileAccess.Write);

            Int32 dictionary = 1 << 23;

            Int32 posStateBits = 2;
            Int32 litContextBits = 3; // for normal files
            // UInt32 litContextBits = 0; // for 32-bit data
            Int32 litPosBits = 0;
            // UInt32 litPosBits = 2; // for 32-bit data
            Int32 algorithm = 2;
            Int32 numFastBytes = 128;

            CoderPropID[] propIDs =
            {
                CoderPropID.DictionarySize,
                CoderPropID.PosStateBits,
                CoderPropID.LitContextBits,
                CoderPropID.LitPosBits,
                CoderPropID.Algorithm,
                CoderPropID.NumFastBytes,
                CoderPropID.MatchFinder,
                CoderPropID.EndMarker
            };

            string mf = "bt4";
            object[] properties =
            {
                dictionary,
                posStateBits,
                litContextBits,
                litPosBits,
                algorithm,
                numFastBytes,
                mf,
                false
            };

            var encoder = new Encoder();
            encoder.SetCoderProperties(propIDs, properties);
            encoder.WriteCoderProperties(outStream);
            Int64 fileSize;

            fileSize = inStream.Length;

            for (int i = 0; i < 8; i++)
                outStream.WriteByte((Byte)(fileSize >> (8 * i)));

            encoder.Code(inStream, outStream, -1, -1, null);

            outStream.Close();
        }

        #endregion Methods
    }
}