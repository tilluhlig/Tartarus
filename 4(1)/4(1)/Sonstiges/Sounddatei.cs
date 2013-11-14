using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace _4_1_
{
    public class Sounddatei
    {
        private byte[] byteArray;
        private int count;
        public DynamicSoundEffectInstance dynamicSound;
        private int position;

        public Sounddatei(String Datei)
        {
            System.IO.Stream waveFileStream = TitleContainer.OpenStream(Datei);
            BinaryReader reader = new BinaryReader(waveFileStream);

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
            dynamicSound.BufferNeeded += new EventHandler<EventArgs>(DynamicSound_BufferNeeded);
            dynamicSound.IsLooped = false;
            // dynamicSound.IsLooped = true;
        }

        public bool IsLooped = false;

        public float Volume
        {
            get
            {
                return dynamicSound.Volume;
            }

            set
            {
                dynamicSound.Volume = value;
            }
        }

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

        public SoundState State
        {
            get
            {
                return dynamicSound.State;
            }
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
    }
}