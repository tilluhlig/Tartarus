using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace _4_1_
{
    public class Sounddatei
    {
        #region Fields

        public DynamicSoundEffectInstance dynamicSound;
        public bool IsLooped = false;
        private readonly byte[] byteArray;
        private readonly int count;
        private int position;

        #endregion Fields

        #region Constructors

        public Sounddatei(String Datei)
        {
            Stream waveFileStream = TitleContainer.OpenStream(Datei);
            var reader = new BinaryReader(waveFileStream);

            int chunkID = reader.ReadInt32();
            int fileSize = reader.ReadInt32();
            int riffType = reader.ReadInt32();
            int fmtID = reader.ReadInt32();
            int fmtSize = reader.ReadInt32();
            int fmtCode = reader.ReadInt16();
            int channels = reader.ReadInt16();
            int sampleRate = reader.ReadInt32();
            int fmtAvgBPS = reader.ReadInt32();
            int fmtBlockAlign = reader.ReadInt16();
            int bitDepth = reader.ReadInt16();

            if (fmtSize == 18)
            {
                // Read any extra values
                int fmtExtraSize = reader.ReadInt16();
                reader.ReadBytes(fmtExtraSize);
            }

            int dataID = reader.ReadInt32();
            int dataSize = reader.ReadInt32();

            byteArray = reader.ReadBytes(dataSize);

            dynamicSound = new DynamicSoundEffectInstance(sampleRate, (AudioChannels)channels);
            count = dynamicSound.GetSampleSizeInBytes(TimeSpan.FromMilliseconds(100));
            dynamicSound.BufferNeeded += DynamicSound_BufferNeeded;
            dynamicSound.IsLooped = false;
            // dynamicSound.IsLooped = true;
        }

        #endregion Constructors

        #region Properties

        public SoundState State
        {
            get { return dynamicSound.State; }
        }

        public float Volume
        {
            get { return dynamicSound.Volume; }

            set { dynamicSound.Volume = value; }
        }

        #endregion Properties

        #region Methods

        public void Pause()
        {
            dynamicSound.Pause();
        }

        public void Play()
        {
            dynamicSound.Play();
        }

        public void Resume()
        {
            dynamicSound.Resume();
        }

        public void Stop(bool sofort)
        {
            dynamicSound.Stop(sofort);
        }

        public void Stop()
        {
            dynamicSound.Stop();
        }

        private void DynamicSound_BufferNeeded(object sender, EventArgs e)
        {
            dynamicSound.SubmitBuffer(byteArray, position, count / 2);
            dynamicSound.SubmitBuffer(byteArray, position + count / 2, count / 2);

            position += count;
            if (position + count > byteArray.Length)
            {
                position = 0;
            }
        }

        #endregion Methods
    }
}