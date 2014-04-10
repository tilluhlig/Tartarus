using System;
using System.IO;

namespace _4_1_.Kompression
{
    /// <summary>
    /// Klasse nutzt LZMA Kompression
    /// </summary>
    public static class Kompression
    {
        /// <summary>
        /// Dekomprimiert Stream
        /// </summary>
        /// <param name="Input">Eingabestream</param>
        /// <param name="Output">Ausgabedatei</param>
        public static void Dekomprimiere(Stream Input, String Output)
        {
            Stream inStream = Input;

            FileStream outStream = null;

            string outputName = Output;
            outStream = new FileStream(outputName, FileMode.Create, FileAccess.Write);

            byte[] properties = new byte[5];
            if (inStream.Read(properties, 0, 5) != 5)
                throw (new Exception("input .lzma is too short"));
            SevenZip.Compression.LZMA.Decoder decoder = new SevenZip.Compression.LZMA.Decoder();
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

        /// <summary>
        /// Komprimiert eine Datei
        /// </summary>
        /// <param name="Input">Eingabedatei</param>
        /// <param name="Output">Ausgabedatei</param>
        public static void Komprimiere(String Input, String Output)
        {
            ReaderStream.ReaderStream dat = new ReaderStream.ReaderStream(Input);
            MemoryStream datei = new MemoryStream();
            while (!dat.EndOfStream)
                datei.WriteByte(dat.ReadByte());

            dat.Close();
            datei.Position = 0;

            Komprimiere(datei, Output);
            datei.Close();
        }

        /// <summary>
        /// Komprimiert einen Stream
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

            SevenZip.CoderPropID[] propIDs =
				{
					SevenZip.CoderPropID.DictionarySize,
					SevenZip.CoderPropID.PosStateBits,
					SevenZip.CoderPropID.LitContextBits,
					SevenZip.CoderPropID.LitPosBits,
					SevenZip.CoderPropID.Algorithm,
					SevenZip.CoderPropID.NumFastBytes,
					SevenZip.CoderPropID.MatchFinder,
					SevenZip.CoderPropID.EndMarker
				};

            string mf = "bt4";
            object[] properties =
				{
					(Int32)(dictionary),
					(Int32)(posStateBits),
					(Int32)(litContextBits),
					(Int32)(litPosBits),
					(Int32)(algorithm),
					(Int32)(numFastBytes),
					mf,
					false
				};

            SevenZip.Compression.LZMA.Encoder encoder = new SevenZip.Compression.LZMA.Encoder();
            encoder.SetCoderProperties(propIDs, properties);
            encoder.WriteCoderProperties(outStream);
            Int64 fileSize;

            fileSize = inStream.Length;

            for (int i = 0; i < 8; i++)
                outStream.WriteByte((Byte)(fileSize >> (8 * i)));

            encoder.Code(inStream, outStream, -1, -1, null);

            outStream.Close();
        }
    }
}