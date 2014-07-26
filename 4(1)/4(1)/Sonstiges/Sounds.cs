// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-11-2013
// ***********************************************************************
// <copyright file="Sounds.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FMOD;
using Microsoft.Xna.Framework.Content;

namespace _4_1_
{
    public class Soundsystem
    {
        #region Fields

        public List<Channel> Channel = new List<Channel>();

        //private static bool playing = false;
        //private static bool paused = false;
        private static int channelsplaying = 0;

        private static uint lenms = 0;

        // private static FMOD.Sound sound1 = null, sound2 = null, sound3 = null;
        //   private static FMOD.Channel channel = null;
        private static uint ms = 0;

        private static FMOD.System system;
        private readonly List<float> originalfrequenz = new List<float>();
        private bool first = true;
        private float frequenzfaktor = 1.0f;
        private bool loop;
        private Sound Sound;
        private float volume = 1.0f;

        #endregion Fields

        #region Constructors

        public Soundsystem(String Datei, int _InitialChannel)
        {
            Init(Datei, 1.0f, 1.0f, false);
            check_channelid(_InitialChannel - 1);
        }

        public Soundsystem(String Datei)
        {
            Init(Datei, 1.0f, 1.0f, false);
        }

        public Soundsystem(String Datei, float _Volume, float _frequenzfaktor, bool _loop, int _InitialChannel)
        {
            Init(Datei, _Volume, _frequenzfaktor, _loop);
            check_channelid(_InitialChannel - 1);
        }

        public Soundsystem(String Datei, float _Volume, float _frequenzfaktor, bool _loop)
        {
            Init(Datei, _Volume, _frequenzfaktor, _loop);
        }

        #endregion Constructors

        #region Methods

        public void Init(String Datei, float _Volume, float _frequenzfaktor, bool _loop)
        {
            Datei = "Content\\" + Datei;
            if (!File.Exists(Datei))
            {
                Datei = Datei + "";
            }

            volume = _Volume;
            frequenzfaktor = _frequenzfaktor;
            loop = _loop;

            if (first)
            {
                Create();
                first = false;
            }
            //.CREATESAMPLE
            system.createSound(Datei, MODE.SOFTWARE | MODE.LOOP_NORMAL, ref Sound);

            Channel.Add(new Channel());
            originalfrequenz.Add(-1);
            // frequenzfaktor = _frequenzfaktor;
        }

        public bool IsPaused(int _Channel)
        {
            check_channelid(_Channel);

            bool paused = false;
            if (Channel[_Channel] != null) Channel[_Channel].getPaused(ref paused);
            return paused;
        }

        public bool IsPlaying(int _Channel)
        {
            check_channelid(_Channel);

            bool playing = false;
            if (Channel[_Channel] != null) Channel[_Channel].isPlaying(ref playing);
            return playing;
        }

        public void PauseSound(int _Channel)
        {
            check_channelid(_Channel);

            if (Channel[_Channel] != null)
                Channel[_Channel].setPaused(true);
        }

        public void PlaySound(int _Channel)
        {
            PlaySound(loop, _Channel, frequenzfaktor, volume);
        }

        public void PlaySound(bool _loop, int _Channel)
        {
            PlaySound(_loop, _Channel, frequenzfaktor, volume);
        }

        public void PlaySound(bool _loop, int _Channel, float _frequenzfaktor, float _volume)
        {
            check_channelid(_Channel);

            bool playing = false;

            if (Channel[_Channel] != null) Channel[_Channel].isPlaying(ref playing);
            if (!playing)
            {
                Channel temp = null;

                system.playSound(CHANNELINDEX.FREE, Sound, false, ref temp);
                Channel[_Channel] = temp;

                if (originalfrequenz[_Channel] == -1)
                {
                    float wert = 0;
                    temp.getFrequency(ref wert);
                    originalfrequenz[_Channel] = wert;
                }

                Channel[_Channel].setFrequency(originalfrequenz[_Channel] * _frequenzfaktor);
                Channel[_Channel].setVolume(_volume);

                if (_loop)
                {
                    temp.setLoopCount(-1);
                }
                else
                    temp.setLoopCount(0);
            }
        }

        public void PlaySoundAny()
        {
            PlaySoundAny(loop, frequenzfaktor, volume);
        }

        public void PlaySoundAny(bool _loop)
        {
            PlaySoundAny(_loop, frequenzfaktor, volume);
        }

        public void PlaySoundAny(bool _loop, float _volume)
        {
            PlaySoundAny(_loop, frequenzfaktor, volume * _volume);
        }

        public void PlaySoundAny(bool _loop, float _frequenzfaktor, float _volume)
        {
            for (int i = 0; i < Channel.Count(); i++)
            {
                bool playing = false;
                if (Channel[i] != null) Channel[i].isPlaying(ref playing);

                if (!playing)
                {
                    Channel temp = null;
                    system.playSound(CHANNELINDEX.FREE, Sound, false, ref temp);
                    Channel[i] = temp;

                    if (originalfrequenz[i] == -1)
                    {
                        float wert = 0;
                        temp.getFrequency(ref wert);
                        originalfrequenz[i] = wert;
                    }

                    Channel[i].setFrequency(originalfrequenz[i] * _frequenzfaktor);
                    Channel[i].setVolume(_volume);

                    if (_loop)
                    {
                        Channel[i].setLoopCount(-1);
                    }
                    else
                        Channel[i].setLoopCount(0);

                    break;
                }
            }
        }

        public void ResumeSound(int _Channel)
        {
            check_channelid(_Channel);

            bool paused = true;
            if (Channel[_Channel] != null)
            {
                Channel[_Channel].getPaused(ref paused);

                if (paused)
                    Channel[_Channel].setPaused(false);
            }

            if (Channel[_Channel] == null || !paused)
            {
                PlaySound(_Channel);
            }
        }

        public void SetVolume(int _Channel, float wert)
        {
            check_channelid(_Channel);

            if (Channel[_Channel] != null)
            {
                Channel[_Channel].setVolume(wert);
            }
        }

        public void StopSound(int _Channel)
        {
            check_channelid(_Channel);

            if (Channel[_Channel] != null)
                Channel[_Channel].stop();
        }

        private static void Create()
        {
            uint version = 0;
            Factory.System_Create(ref system);
            system.getVersion(ref version);
            if (version < VERSION.number)
            {
            }

            system.init(32, INITFLAGS.NORMAL, (IntPtr)null);

            //system.createSound("Content\\Sounds\\battle1_ambient.ogg", FMOD.MODE.HARDWARE, ref sound1);
            //  system.playSound(FMOD.CHANNELINDEX.FREE, sound1, false, ref channel);
            // Sounds.channel.setVolume(4.0f);
            //Sounds.channel.setFrequency(15025);
        }

        private void check_channelid(int _Channel)
        {
            while (Channel.Count <= _Channel)
            {
                Channel.Add(new Channel());
                originalfrequenz.Add(-1);
            }
        }

        #endregion Methods
    }

    /// <summary>
    ///     Class Sounds
    /// </summary>
    internal static class Sounds
    {
        #region Fields

        /// <summary>
        ///     The armorhit
        /// </summary>
        public static Soundsystem[] armorhit = new Soundsystem[2];

        public static Soundsystem erobert;
        public static Soundsystem[] Explosion = new Soundsystem[10];
        public static Soundsystem fahrzeugzerstört;

        public static Soundsystem Hintergrundmusik;

        public static Soundsystem Lademusik;

        public static Soundsystem levelup;

        public static Soundsystem[] Mine_klick = new Soundsystem[2];

        /// <summary>
        ///     The panzer move
        /// </summary>
        public static Soundsystem[] Panzer_rohr_begin = new Soundsystem[6];

        public static Soundsystem[] Panzer_rohr_end = new Soundsystem[6];

        public static Soundsystem[] Panzer_rohr_loop = new Soundsystem[6];

        public static int[] Panzer_rohrmode = new int[6];

        /// <summary>
        ///     The panzer move
        /// </summary>
        public static Soundsystem[] PanzerMove = new Soundsystem[6];

        public static Soundsystem punkteerhalten;

        public static Soundsystem[] Shots = new Soundsystem[10];

        /// <summary>
        ///     The sink
        /// </summary>
        public static Soundsystem[] sink = new Soundsystem[2];

        /// <summary>
        ///     The waterboom
        /// </summary>
        public static Soundsystem[] waterboom = new Soundsystem[3];

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Loads the specified content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void Load(ContentManager Content) // lädt alle Sounds
        {
            String Typ = ".ogg";

            Mine_klick[0] = new Soundsystem("Sounds\\mineclick1" + Typ, 2);
            Mine_klick[1] = new Soundsystem("Sounds\\mineclick2" + Typ, 2);

            Hintergrundmusik = new Soundsystem("Sounds\\War song.ogg", 0.45f, 1f, true);
            Hintergrundmusik.PlaySound(0);

            armorhit[0] = new Soundsystem("Sounds\\67617__qubodup__metal-crash-collision" + Typ, 0.75f, 1.0f, false, 5);
            armorhit[1] = new Soundsystem("Sounds\\emag" + Typ, 0.75f, 1.0f, false, 5);

            levelup = new Soundsystem("Sounds\\spell3" + Typ, 1f, 1.0f, false, 2);
            punkteerhalten = new Soundsystem("Sounds\\upshort2" + Typ, 1f, 1.0f, false, 1);
            erobert = new Soundsystem("Sounds\\points" + Typ, 0.75f, 0.75f, false, 1);

            //  airexp = new Soundsystem("Sounds\\airexp" + Typ, 5);
            //   airshot = new Soundsystem("Sounds\\airshot" + Typ, 5);

            //   freezeexp = new Soundsystem("Sounds\\boom6" + Typ, 5);
            //  freezeshot = new Soundsystem("Sounds\\freezeshot" + Typ, 5);

            //  bigexp = new Soundsystem("Sounds\\boom1" + Typ, 5);
            // bigshot = new Soundsystem("Sounds\\67515__qubodup__howitzer-gun-shot-6-no-echo" + Typ, 5);

            //  smokeexp = new Soundsystem("Sounds\\smokeexp" + Typ, 5);
            //smokeshot = new Soundsystem("Sounds\\smokeshot" + Typ, 5);

            // nukeexp = new Soundsystem("Sounds\\nukeexp" + Typ, 5);

            fahrzeugzerstört = new Soundsystem("Sounds\\fahrzeugzerstoert1" + Typ, 3);

            //  shot1 = new Soundsystem("Sounds\\67515__qubodup__howitzer-gun-shot-6-no-echo" + Typ, 5);
            // mgshot = new Soundsystem("Sounds\\67586__qubodup__machine-gun-shot-2" + Typ, 0.25f, 1f, false, 150);

            String[] Shots_Dateien =
            {
                "67521__qubodup__m1a1-abrams-canon-shot-2",
                "67519__qubodup__m1a1-abrams-canon-shot-1",
                "161342__qubodup__howitzer-artillery-gun-shot-sound-effect-02",
                "175430__qubodup__excalibur-howitzer-shot",
                "161341__qubodup__howitzer-artillery-gun-shot-sound-effect-03",
                "Slow Whoosh 7",
                "161343__qubodup__howitzer-artillery-gun-shot-sound-effect-01",
                "161343__qubodup__howitzer-artillery-gun-shot-sound-effect-01",
                "67586__qubodup__machine-gun-shot-2"
            };
            float[] Shots_frequenz = { 1, 1, 1, 1, 1, 2f, 1.5f, 1f, 1 };
            float[] Shots_volume = { 1, 1, 1, 0.5f, 1, 0.7f, 0.25f, 0.25f, 0.15f };
            int[] Shots_initial = { 5, 5, 5, 5, 5, 5, 5, 5, 150 };

            for (int i = 0; i < Shots_Dateien.Count(); i++)
            {
                if (Shots_Dateien[i] != "null")
                {
                    Shots[i] = new Soundsystem("Sounds\\" + Shots_Dateien[i] + Typ, Shots_volume[i], Shots_frequenz[i],
                        false, Shots_initial[i]);
                }
            }

            String[] Explosion_Dateien =
            {
                "boom.ogg", // standardmissle = 0; //
                "explode.ogg", // bigstandardmissle = 1; //
                "boom2.ogg", // cryomissle = 2; //
                "boom8.ogg", // poisonmissle = 3; //
                "raketenexplosion.mp3", // nuke = 4; //
                "null", // airstrike = 5; //
                "boom6.ogg", // geschoss = 6; //
                "explodemini.ogg", // geschoss2 = 7; //
                "null"
            }; // mg = 8;
            float[] Explosion_frequenz = { 1f, 1, 0.5f, 1, 0.8f, 1f, 1f, 1f, 1f };
            float[] Explosion_volume = { .8f, .8f, 0.35f, 1, 0.75f, 1f, 1f, 1f, .30f };
            int[] Explosion_initial = { 5, 5, 5, 5, 5, 5, 5, 5, 0 };

            for (int i = 0; i < Explosion_Dateien.Count(); i++)
            {
                if (Explosion_Dateien[i] != "null")
                {
                    Explosion[i] = new Soundsystem("Sounds\\" + Explosion_Dateien[i], Explosion_volume[i],
                        Explosion_frequenz[i], false, Explosion_initial[i]);
                }
            }

            sink[0] = new Soundsystem("Sounds\\sink_000" + Typ, 3);
            sink[1] = new Soundsystem("Sounds\\sink_001" + Typ, 3);
            waterboom[0] = new Soundsystem("Sounds\\water_boom_000" + Typ, 5);
            waterboom[1] = new Soundsystem("Sounds\\water_boom_001" + Typ, 5);
            waterboom[2] = new Soundsystem("Sounds\\water_boom_002" + Typ, 5);

            String[] Dateien =
            {
                "Sounds\\vehicle084", "Sounds\\vehicle077_2", "Sounds\\vehicle069", "Sounds\\vehicle121",
                "null", "null"
            };
            float[] Dateien_volume = { 0.6f, 0.7f, 0.6f, 1f, 0.0f, 0.0f };

            for (int i = 0; i < Dateien.Count(); i++)
            {
                if (Dateien[i] != "null")
                    PanzerMove[i] = new Soundsystem(Dateien[i] + Typ, Fahrzeugdaten.VOLUMES.Wert[i], Dateien_volume[i],
                        true);
            }

            String[] Panzer_begin = { "rohr_begin0", "rohr_begin0", "null", "null", "rohr_begin0", "rohr_begin0" };
            float[] Panzer_begin_frequenz = { 1, 1.5f, 1, 1, 2, 2.5f };
            float[] Panzer_begin_volume = { .75f, 1, .75f, .75f, 1, 1 };

            String[] Panzer_end = { "rohr_end0", "rohr_end0", "null", "null", "rohr_end0", "rohr_end0" };
            float[] Panzer_end_frequenz = { 1, 1.5f, 1, 1, 2, 2.5f };
            float[] Panzer_end_volume = { .1f, .75f, .5f, .5f, .75f, .75f };

            String[] Panzer_loop = { "rohr_loop0", "rohr_loop0", "null", "null", "rohr_loop0", "rohr_loop0" };
            float[] Panzer_loop_frequenz = { 1, 1.5f, 1, 1, 2, 2.5f };
            float[] Panzer_loop_volume = { .75f, 1, .75f, .75f, 1, 1 };

            for (int i = 0; i < Panzer_begin.Count(); i++)
            {
                Panzer_rohrmode[i] = 0;

                if (Panzer_begin[i] != "null")
                {
                    Panzer_rohr_begin[i] = new Soundsystem("Sounds\\" + Panzer_begin[i] + Typ,
                        Panzer_begin_volume[i] * 0.65f, Panzer_begin_frequenz[i], false);
                    Panzer_rohr_end[i] = new Soundsystem("Sounds\\" + Panzer_end[i] + Typ, Panzer_end_volume[i] * 0.65f,
                        Panzer_end_frequenz[i], false);
                    Panzer_rohr_loop[i] = new Soundsystem("Sounds\\" + Panzer_loop[i] + Typ, Panzer_loop_volume[i] * 0.65f,
                        Panzer_loop_frequenz[i], true);
                }
            }
        }

        #endregion Methods
    }
}