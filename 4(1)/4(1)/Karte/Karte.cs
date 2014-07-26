// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-27-2013
// ***********************************************************************
// <copyright file="Karte.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using CColor = System.Drawing.Color;
using XColor = Microsoft.Xna.Framework.Color;

//using System.Data;
//using System.Drawing;
//using System.Windows.Forms;
//using Microsoft.Xna.Framework.Graphics.Color;
//using System.Drawing.Color;

namespace _4_1_
{
    /// <summary>
    /// Beinhaltet Funktionen die auf Karten angewendet werden koennen
    /// </summary>
    public class Karte
    {
        #region Fields

        /// <summary>
        /// MOD-Variable, Gibt an, ob die Karte symmetrisch aufgebaut werden soll
        /// </summary>
        public static bool KARTE_SYMMETRISCH;

        /// <summary>
        /// The material
        /// </summary>
        public static Materialien[] Material = new Materialien[16];

        /// <summary>
        /// The MATERIA l_ BLUR
        /// </summary>
        public static Var<bool[]> MATERIAL_BLUR = new Var<bool[]>("MATERIAL_BLUR", new bool[] { false, true, false, false, true, true, true, true, false, false, false, false, false, false, false, false });

        /// <summary>
        /// The MATERIA l_ COLLISION
        /// </summary>
        public static Var<bool[]> MATERIAL_COLLISION = new Var<bool[]>("MATERIAL_COLLISION", new bool[] { false, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false });

        /// <summary>
        /// The MATERIA l_ FOLGEID
        /// </summary>
        public static Var<int[]> MATERIAL_FOLGEID = new Var<int[]>("MATERIAL_FOLGEID", new int[] { 0, 0, 3, 0, 0, 0, 7, 0, 0, 0, 10, 0, 0, 0, 0, 0 });

        /// <summary>
        /// The MATERIA l_ I s_ TEXTUR
        /// </summary>
        public static Var<bool[]> MATERIAL_IS_TEXTUR = new Var<bool[]>("MATERIAL_IS_TEXTUR", new bool[] { false, true, true, true, true, true, true, true, true, true, true, false, false, false, false, false });

        /// <summary>
        /// The MATERIA l_ TEXTUR
        /// </summary>
        public static Var<String[]> MATERIAL_NAME = new Var<String[]>("MATERIAL_NAME", new String[] { "LUFT", "ERDE", "BACKSTEIN1", "BACKSTEIN2", "BETON", "FELS", "GRANIT1", "GRANIT2", "SUMPF", "WASSER", "FEUER", "nichts", "nichts", "nichts", "nichts", "nichts" });

        /// <summary>
        /// The MATERIA l_ TEXTUR
        /// </summary>
        public static Var<String[]> MATERIAL_TEXTUR = new Var<String[]>("MATERIAL_TEXTUR", new String[] { "#00000000", "Material\\foreground8", "Material\\buergersteig_64_2", "Material\\buergersteig_64_22", "Material\\boden10240026_512", "Material\\bergtextur_512_2", "Material\\mosh-erde (11)", "Material\\mosh-erde (11)_2", "Material\\boden10", "Material\\wasser14", "Material\\welle_04", "#00000000", "#00000000", "#00000000", "#00000000", "#00000000" });

        /// <summary>
        /// Gibt an, ob die Gebäude auf der Karte als "Städte" und "Dörfer" angelegt werden sollen
        /// </summary>
        public static bool STAEDTE_UND_DOERFER;

        #endregion Fields

        #region Privat

        /// <summary>
        /// Mod-Variable, Gibt an, ob die Karte symmetrisch aufgebaut werden soll
        /// </summary>
        private static Var<bool> MOD_KARTE_SYMMETRISCH = new Var<bool>("KARTE_SYMMETRISCH", false, ref KARTE_SYMMETRISCH);

        /// <summary>
        /// MOD-Variable, Gibt an, ob die Gebäude auf der Karte als "Städte" und "Dörfer" angelegt werden sollen
        /// </summary>
        private static Var<bool> MOD_STAEDTE_UND_DOERFER = new Var<bool>("STAEDTE_UND_DOERFER", false, ref STAEDTE_UND_DOERFER);

        #endregion Privat

        /// <summary>
        /// The particle farbe
        /// </summary>
        public List<Vector3> particleFarbe = new List<Vector3>();

        /// <summary>
        /// The particle farbe S
        /// </summary>
        public List<Vector3> particleFarbeS = new List<Vector3>();

        /// <summary>
        /// The particle farbe s2
        /// </summary>
        public List<Vector3> particleFarbeS2 = new List<Vector3>();

        /// <summary>
        /// The particle list exp
        /// </summary>
        public List<ParticleData> particleListExp = new List<ParticleData>();

        /// <summary>
        /// The particle list map smoke
        /// </summary>
        public List<ParticleData> particleListMapSmoke = new List<ParticleData>();

        /// <summary>
        /// The particle map smoke data
        /// </summary>
        public List<Vector3> particleMapSmokeData = new List<Vector3>();

        /// <summary>
        /// Der interne Zufallszahlengenerator
        /// </summary>
        private Random rand = new Random();

        // Explosion einer Waffe zünden
        /// <summary>
        /// Explosion_einer_s the waffe_zünden.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <param name="Waffenart">The waffenart.</param>
        /// <param name="pos">The pos.</param>
        public static void Explosion_einer_Waffe_zünden(List<UInt16>[] Spielfeld, GameTime gameTime, Spiel Spiel2, int Waffenart, Vector2 pos)
        {
            List<Vector3> list = new List<Vector3>();

            int _Art = Waffenart;

            // Explosion
            Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, pos, (int)Waffendaten.Daten[_Art].Y,
                           Waffendaten.Daten[_Art].X, Waffendaten.Daten[_Art].W, gameTime,
                           Waffendaten.Farben[_Art], _Art, 0);

            // Sound
            Spiel2.Karte.explode_missile(Spielfeld, pos, Spiel2.Fenster, _Art);

            // Rauchstelle
            /* for (int j = -(int)Rakete.Daten[_Art].X / 2; j < Rakete.Daten[_Art].X / 2; j += Rakete.BrandAbstand[_Art])
             {
                 if (pos.X + j < 0 || pos.X + j >= Spielfeld.Length) continue;
                 Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListMapSmoke, new Vector2(pos.X + j, Help.BottomOf2(pos)), 4,
                Rakete.Daten[_Art].Z / 10, Rakete.Daten[_Art].W * 10, gameTime,
                 Microsoft.Xna.Framework.Color.Gray.ToVector3(), _Art, 2); //.Farben[_Art]
             }*/

            Karte a = new Karte();
            Replay.Explosion(pos, _Art);
            list.AddRange(a.Explode(Spielfeld, (int)pos.X, (int)pos.Y, (int)(Waffendaten.Daten[_Art].X)));
            list.AddRange(Spiel2.Explosionsschäden(gameTime, pos, (int)(Waffendaten.Daten[_Art].X), _Art, new int[] { -1, -1 }));
            Vordergrund.AktualisiereVordergrund(list);
        }

        // Explosion einer Waffe zünden
        /// <summary>
        /// Explosion_einer_s the waffe_zünden_ohne_schaden.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <param name="Waffenart">The waffenart.</param>
        /// <param name="pos">The pos.</param>
        public static void Explosion_einer_Waffe_zünden_ohne_schaden(List<UInt16>[] Spielfeld, GameTime gameTime, Spiel Spiel2, int Waffenart, Vector2 pos)
        {
            int _Art = Waffenart;

            // Explosion
            Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, pos, (int)Waffendaten.Daten[_Art].Y,
                           Waffendaten.Daten[_Art].X, Waffendaten.Daten[_Art].W, gameTime,
                           Waffendaten.Farben[_Art], _Art, 0);

            // Sound
            Spiel2.Karte.explode_missile(Spielfeld, pos, Spiel2.Fenster, _Art);

            // Rauchstelle
            /* for (int j = -(int)Rakete.Daten[_Art].X / 2; j < Rakete.Daten[_Art].X / 2; j += Rakete.BrandAbstand[_Art])
             {
                 if (pos.X + j < 0 || pos.X + j >= Spielfeld.Length) continue;
                 Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListMapSmoke, new Vector2(pos.X + j, Help.BottomOf2(pos)), 4,
                Rakete.Daten[_Art].Z / 10, Rakete.Daten[_Art].W * 10, gameTime,
                 Rakete.Farben[_Art], _Art, 2);
             }*/
        }

        public static void Explosion_einer_Waffe_zünden_ohne_schaden_sound(List<UInt16>[] Spielfeld, GameTime gameTime, Spiel Spiel2, int Waffenart, Soundsystem Sound, Vector2 pos)
        {
            int _Art = Waffenart;

            // Explosion
            Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, pos, (int)Waffendaten.Daten[_Art].Y,
                           Waffendaten.Daten[_Art].X, Waffendaten.Daten[_Art].W, gameTime,
                           Waffendaten.Farben[_Art], _Art, 0);

            Sound.PlaySoundAny();

            // Sound
            // Spiel2.Karte.explode_missile(Spielfeld, pos, Spiel2.Fenster, _Art);

            // Rauchstelle
            /* for (int j = -(int)Rakete.Daten[_Art].X / 2; j < Rakete.Daten[_Art].X / 2; j += Rakete.BrandAbstand[_Art])
             {
                 if (pos.X + j < 0 || pos.X + j >= Spielfeld.Length) continue;
                 Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListMapSmoke, new Vector2(pos.X + j, Help.BottomOf2(pos)), 4,
                Rakete.Daten[_Art].Z / 10, Rakete.Daten[_Art].W * 10, gameTime,
                 Rakete.Farben[_Art], _Art, 2);
             }*/
        }

        /// <summary>
        /// Froms the name.
        /// </summary>
        /// <param name="colorName">Name of the color.</param>
        /// <returns>Microsoft.Xna.Framework.Color.</returns>
        public static Microsoft.Xna.Framework.Color FromName(string colorName)
        {
            CColor clrColor = Hauptfenster.Form1.Colors.FromName(colorName);
            return new XColor(clrColor.R, clrColor.G, clrColor.B, clrColor.A);
        }

        /// <summary>
        /// Lade_s the materialien.
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void Lade_Materialien(ContentManager Content)
        {
            for (int i = 0; i < Material.Count(); i++)
            {
                Material[i] = new Materialien(Content, MATERIAL_IS_TEXTUR.Wert[i] ? MATERIAL_TEXTUR.Wert[i] : "", MATERIAL_IS_TEXTUR.Wert[i] ? false : true, MATERIAL_IS_TEXTUR.Wert[i] ? Microsoft.Xna.Framework.Color.Transparent : FromName(MATERIAL_TEXTUR.Wert[i]), MATERIAL_FOLGEID.Wert[i], 1.0f, MATERIAL_BLUR.Wert[i], MATERIAL_COLLISION.Wert[i]);
            }
        }

        /// <summary>
        /// Reset_s the materialien.
        /// </summary>
        public static void Reset_Materialien()
        {
        }

        /// <summary>
        /// To the name.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>String.</returns>
        public static String ToName(Microsoft.Xna.Framework.Color Name)
        {
            XColor xColor = Name;
            CColor clrColor = CColor.FromArgb(xColor.R, xColor.G, xColor.B, xColor.A);
            return Hauptfenster.Form1.Colors.ToName(clrColor);
        }

        /// <summary>
        /// Adds the explosion.
        /// </summary>
        /// <param name="particleList">The particle list.</param>
        /// <param name="explosionPos">The explosion pos.</param>
        /// <param name="numberOfParticles">The number of particles.</param>
        /// <param name="size">The size.</param>
        /// <param name="maxAge">The max age.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Farben">The farben.</param>
        /// <param name="Weapon">The weapon.</param>
        /// <param name="id">The id.</param>
        public void AddExplosion(List<ParticleData> particleList, Vector2 explosionPos, int numberOfParticles, float size, float maxAge, GameTime gameTime, Vector3 Farben, int Weapon, int id)
        {
            //Weapon;
            for (int i = 0; i < numberOfParticles; i++)
                AddExplosionParticle(particleList, explosionPos, size, maxAge, gameTime, Farben, Weapon, id);
        }

        /// <summary>
        /// Collisionses the specified spielfeld.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="Missile">The missile.</param>
        /// <param name="Player">The player.</param>
        /// <param name="Haeuser">The haeuser.</param>
        /// <param name="Baeume">The baeume.</param>
        /// <param name="Bunker">The bunker.</param>
        /// <param name="Kisten">The kisten.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Fenster">The fenster.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool collisions(List<UInt16>[] Spielfeld, Waffen[] Missile, Spieler[] Player, Haus Haeuser, Baum Baeume, Bunker Bunker, Kiste Kisten, int screenHeight, GameTime gameTime, Vector2 Fenster) // Prüft Kollisionen
        {
            // soll prüfen, ob es Kollisionen gab und diese behandeln

            // alle Raketentreffer prüfen
            bool check = false;
            for (int i = 0; i < Missile.Length && !Client.isRunning; i++)
            {
                if (Missile[i].verzoegerung > 0) continue; //  Rakete hat bereits etwas getroffen
                if (!Missile[i].missleShot) continue; // Rakete noch nicht abgefeuert
                if ((int)Missile[i].misslePosition.X < 0 || (int)Missile[i].misslePosition.X >= Spielfeld.Length) continue; // Rakete ausserhalb des Spielfelds
                bool Treffer = false;

                // Haus treffer prüfen
                Vector2 RakPos = new Vector2(Missile[i].RaktnSpitze().X, Missile[i].RaktnSpitze().Y);
                if (Haus.HAEUSER_KOLLISION)
                {
                    for (int b = 0; b < Haeuser.Position.Count; b++)
                    {
                        if (Haeuser.IsCollision(b, RakPos))
                        {
                            Treffer = true;
                            break;
                        }
                    }
                }

                // Tunnel treffer prüfen
                if (Haus.HAEUSER_KOLLISION)
                {
                    for (int c = 0; c < Player.Count(); c++)
                        for (int b = 0; b < Player[c].TunnelAnlage.Count; b++)
                        {
                            if (Player[c].TunnelAnlage[b].PrüfeObKollision(RakPos))
                            {
                                Treffer = true;
                                break;
                            }
                        }
                }

                // Bunker treffer prüfen
                if (Bunker.BUNKER_KOLLISION && !Client.isRunning && !Treffer)
                {
                    for (int b = 0; b < Bunker.Besitzer.Count; b++)
                    {
                        if (Bunker.PrüfeObKollision(b, RakPos))
                        {
                            Treffer = true;
                            // Bunker.UpdateBunkerSchaden(b, Missile[i].Energie);
                            break;
                        }
                    }
                }

                // Trash treffer prüfen
                if (!Client.isRunning && !Treffer)
                {
                    for (int b = 0; b < Nutzloses.GibAnzahl(); b++)
                    {
                        if (Nutzloses.PrüfeObKollision(b, RakPos))
                        {
                            Treffer = true;
                            break;
                        }
                    }
                }

                // Panzer treffer prüfen
                if (!Client.isRunning && !Treffer)
                    for (int b = 0; b < Player.Length; b++)
                    {
                        for (int c = 0; c < Player[b].pos.Count; c++)
                        {
                            if (Player[b].PrüfeObKollision(c, RakPos))
                            {
                                if (Waffendaten.Verschiessbar[Missile[i].Art] != 4)
                                {
                                    Sounds.armorhit[rand.Next(0, Sounds.armorhit.Count())].PlaySoundAny();
                                }
                                else
                                {
                                    //  Sounds.armorhit2.Play();
                                }
                                Treffer = true;
                                break;
                            }
                        }
                    }

                // Kiste treffer prüfen
                if (!Client.isRunning)
                    for (int b = 0; b < Kisten.aktiv.Count; b++)
                    {
                        if (!Kisten.aktiv[b]) continue;
                        if (Kisten.IsCollision(b, RakPos))
                        {
                            AddExplosion(particleListMapSmoke, new Vector2(Kisten.pos[b].X, Kartenformat.BottomOf(Kisten.pos[b]) - 50), 10, 50, 1000, gameTime, Waffendaten.Farben[0], 0, 2);

                            //if (Baum.BAEUME_DELETE_ON_HIT.Wert) Baeume.Delete(b, gameTime);
                            Treffer = true;
                            break;
                        }
                    }

                // Bodentreffer prüfen
                if (!Client.isRunning && !Treffer)
                    if (Kartenformat.isSet(Missile[i].RaktnSpitze().X, Missile[i].RaktnSpitze().Y) || Missile[i].RaktnSpitze().Y >= screenHeight) Treffer = true;

                // gab es einen Treffer?
                if (Treffer)
                {
                    // Raketenexplosion zünden
                    if (Missile[i].RaktnSpitze().Y <= screenHeight - 10) // nur zünden, wenn rakete nicht ausserhalb des spielfeldes
                    {
                        AddExplosion(particleListExp, Missile[i].RaktnSpitze(), (int)(Waffendaten.Daten[Missile[i].Art].Y),
                            Waffendaten.Daten[Missile[i].Art].X, Waffendaten.Daten[Missile[i].Art].W, gameTime,
                            Waffendaten.Farben[Missile[i].Art], Missile[i].Art, 0);

                        /*  AddExplosion(particleListExp, Missile[i].RaktnSpitze(), (int)Rakete.Daten[Missile[i].Art].Y/5,
                              Rakete.Daten[Missile[i].Art].X, Rakete.Daten[Missile[i].Art].W*2.5f, gameTime,
                              Rakete.Farben[Missile[i].Art]*15, Missile[i].Art, 0);*/

                        explode_missile(Spielfeld, Missile[i].RaktnSpitze(), Fenster, Missile[i].Art);

                        Missile[i].verzoegerung = (int)Waffendaten.Daten2[Missile[i].Art].Y;
                        if (Server.isRunning) Server.Send("VERZOEGERUNG " + i + " " + Missile[i].verzoegerung);
                    }
                    else
                    {
                        Missile[i].watered = true; Sounds.waterboom[Waffendaten.Waterboom[Missile[i].Art]].PlaySoundAny();
                        Missile[i].Delete();
                    }

                    check = false;
                }
            }
            return check;
        }

        /// <summary>
        /// Create_maps the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="height">The height.</param>
        /// <param name="start">The start.</param>
        /// <param name="minmove">The minmove.</param>
        /// <param name="maxmove">The maxmove.</param>
        /// <param name="minheight">The minheight.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="hoehle">The hoehle.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public void create_map(List<UInt16>[] array, int height, int start, int minmove, int maxmove, int minheight, int screenHeight, Höhlenkonfiguration hoehle) // erstellt eine neue Karte
        {
            if (MOD_STAEDTE_UND_DOERFER.Wert)
            {
                create_map_staedte_doerfer(array, height, start, minmove, maxmove, minheight, screenHeight, hoehle);
                return;
            }

            bool symmetrisch = MOD_KARTE_SYMMETRISCH.Wert;
            int move = 0;
            int m = 50;
            int pos = 0;
            int max = 0;
            int lastmove = 1;
            int dist = 0;
            Random rnd = new Random();
            array[0].Add((UInt16)(start + Kartenformat.SortenFaktor(LUFT)));
            array[0].Add((UInt16)(screenHeight - start + Kartenformat.SortenFaktor(ERDE)));
            for (int i = 1; i < (symmetrisch ? array.Length / 2 : array.Length); i++)
            {
                if (m <= 0)
                {
                    lastmove = move;
                    for (; ; )
                    {
                        move = rnd.Next(-1, 2);
                        if (!((lastmove == -1 && move == 1) || (lastmove == 1 && move == -1))) break;
                    }

                    m = rnd.Next(minmove, maxmove);
                    pos = m;
                    dist = 0;
                    max = m;
                }

                if (array[i - 1][0] + move >= height - 1)
                {
                    move = 0;
                }
                else
                    if (array[i - 1][0] + move <= minheight)
                    {
                        move = 0;
                    }

                if (i + 50 >= (symmetrisch ? array.Length / 2 : array.Length)) { move = 0; m = 25; }

                if (move == 0)
                {
                    array[i].Add(array[i - 1][0]);
                    array[i].Add(array[i - 1][1]);
                }
                else
                    if (move == 1)
                    {
                        if (dist <= 0)
                        {
                            pos--;
                            dist = (int)Math.Log(max - m + 1, Math.E);
                            array[i].Add((UInt16)(array[i - 1][0] + move + Kartenformat.SortenFaktor(LUFT)));
                            array[i].Add((UInt16)(screenHeight - (array[i - 1][0] + move) + Kartenformat.SortenFaktor(ERDE)));
                        }
                        else
                        {
                            array[i].Add((UInt16)(array[i - 1][0] + Kartenformat.SortenFaktor(LUFT)));
                            array[i].Add((UInt16)(screenHeight - array[i - 1][0] + Kartenformat.SortenFaktor(ERDE)));
                        }
                    }
                    else
                        if (move == -1)
                        {
                            if (dist <= 0)
                            {
                                pos--;
                                dist = (int)Math.Log(m, Math.E);
                                array[i].Add((UInt16)(array[i - 1][0] + move + Kartenformat.SortenFaktor(LUFT)));
                                array[i].Add((UInt16)(screenHeight - (array[i - 1][0] + move) + Kartenformat.SortenFaktor(ERDE)));
                            }
                            else
                            {
                                array[i].Add((UInt16)(array[i - 1][0] + Kartenformat.SortenFaktor(LUFT)));
                                array[i].Add((UInt16)(screenHeight - array[i - 1][0] + Kartenformat.SortenFaktor(ERDE)));
                            }
                        }

                m--;
                dist--;
            }

            // umformen
            for (int i = 0; i < array.Length; i++)
                for (int b = 0; b < array[i].Count; b++)
                    array[i][b] = (UInt16)(screenHeight - Kartenformat.Laenge(array[i][b]) + Kartenformat.SortenFaktor(Kartenformat.Material(array[i][b])));

            List<UInt16>[] arraysymm = new List<UInt16>[array.Length / 2];
            if (symmetrisch)
            {
                for (int i = 0; i < arraysymm.Length; i++)
                {
                    arraysymm[i] = new List<UInt16>();
                    arraysymm[i].AddRange(array[i]);
                }
            }

            // Höhlen
            Höhlenkonfiguration.Generate(hoehle, (symmetrisch ? arraysymm : array), null);

            /*// Felsen
            CaveConf Felsen = new CaveConf();
            Felsen.setAllgemein(450, 150, 50, 25, 200, 150, Karte.FELS);
            Felsen.setA(5, 30, 0, 8, 40, 0.3f);
            Felsen.setB(15, 50, 0, 8, 50, 0.3f);
            Felsen.setC(15, 50, -40, -40, 40, 0.2f);
            CaveConf.Generate(Felsen, array);*/

            // Granit
            Höhlenkonfiguration Granit = new Höhlenkonfiguration();
            Granit.setAllgemein(450, 150, 50, 25, 200, 150, Karte.GRANIT1, false);
            Granit.setA(5, 30, 0, 8, 40, 0.3f);
            Granit.setB(15, 50, 0, 8, 50, 0.3f);
            Granit.setC(15, 50, -40, -40, 40, 0.05f);
            Höhlenkonfiguration.Generate(Granit, (symmetrisch ? arraysymm : array), null);

            // Sumpf
            Höhlenkonfiguration Sumpf = new Höhlenkonfiguration();
            Sumpf.setAllgemein(1000, 50, 0, 35, 200, 450, Karte.SUMPF, true);
            Sumpf.setA(5, 30, 0, 0, 0, 0.3f);
            Sumpf.setB(30, 50, 0, 5, 10, 0.5f);
            Sumpf.setC(15, 50, -40, -40, 10, 0.05f);
            Höhlenkonfiguration.Generate(Sumpf, (symmetrisch ? arraysymm : array), null);

            // Wasser
            Sumpf.setAllgemein(1000, 50, 0, 35, 200, 450, Karte.WASSER, true);
            Höhlenkonfiguration.Generate(Sumpf, (symmetrisch ? arraysymm : array), null);

            if (symmetrisch)
            {
                for (int i = 0; i < arraysymm.Length; i++)
                {
                    array[i].Clear();
                    array[i].AddRange(arraysymm[i]);
                }

                int b = 0;
                for (int i = arraysymm.Length - 1; i >= 0; i--, b++)
                {
                    array[arraysymm.Length + b].Clear();
                    array[arraysymm.Length + b].AddRange(arraysymm[i]);
                    int a = array[arraysymm.Length + b][0];
                    int c = a + a + 2;
                    int d = a + c;
                }
            }

            //array = Help.Spielfeld;
            // Generate_MAP_v2(array);
        }

        /// <summary>
        /// Create_map_staedte_doerfers the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="height">The height.</param>
        /// <param name="start">The start.</param>
        /// <param name="minmove">The minmove.</param>
        /// <param name="maxmove">The maxmove.</param>
        /// <param name="minheight">The minheight.</param>
        /// <param name="screenHeight">Height of the screen.</param>
        /// <param name="hoehle">The hoehle.</param>
        /// <param name="symmetrisch">if set to <c>true</c> [symmetrisch].</param>
        public void create_map_staedte_doerfer(List<UInt16>[] array, int height, int start, int minmove, int maxmove, int minheight, int screenHeight, Höhlenkonfiguration hoehle) // erstellt eine neue Karte
        {
            bool symmetrisch = MOD_KARTE_SYMMETRISCH.Wert;

            #region Orte_generieren_und_einlesen

            Haus.generate_staedte_doerfer(array, symmetrisch);
            Haus.Orte.Clear();
            Haus.Ortemaxheight.Clear();
            bool dorf = true;
            int dor = 0;
            int sta = 0;
            for (int i = 0; i < Haus.Doerfer.Count + Haus.Staedte.Count; i++)
            {
                if (dorf)
                {
                    if (dor >= Haus.Doerfer.Count()) break;
                    int ma = Haus.Doerfer[dor].Count();
                    Haus.Orte.Add(new Vector2(Haus.Doerfer[dor][ma - 2], Haus.Doerfer[dor][ma - 1]));
                    dorf = false;
                    dor++;
                }
                else
                {
                    if (sta >= Haus.Staedte.Count()) break;
                    int ma = Haus.Staedte[sta].Count();
                    Haus.Orte.Add(new Vector2(Haus.Staedte[sta][ma - 2], Haus.Staedte[sta][ma - 1]));
                    dorf = true;
                    sta++;
                }
            }

            #endregion Orte_generieren_und_einlesen

            int ort = 0;
            Random rnd = new Random();
            int move = 0;
            int m = 50;
            int pos = 0;
            int max = 0;
            int lastmove = 1;
            int dist = 0;
            array[0].Add((UInt16)(start + Kartenformat.SortenFaktor(LUFT)));
            array[0].Add((UInt16)(screenHeight - start + Kartenformat.SortenFaktor(ERDE)));
            for (int i = 1; i < (symmetrisch ? array.Length / 2 : array.Length); i++)
            {
                if (i == Haus.Orte[ort].X)
                {
                    lastmove = move;
                    m = (int)Haus.Orte[ort].Y;
                    pos = m;
                    dist = 0;
                    max = m;
                    move = 0;
                    if (ort < Haus.Orte.Count - 1) ort++;
                }

                if (m <= 0)
                {
                    lastmove = move;
                    for (; ; )
                    {
                        move = rnd.Next(-1, 2);
                        if (!((lastmove == -1 && move == 1) || (lastmove == 1 && move == -1))) break;
                    }

                    m = rnd.Next(minmove, maxmove);
                    pos = m;
                    dist = 0;
                    max = m;
                }

                if (array[i - 1][0] + move >= height - 1)
                {
                    move = 0;
                }
                else
                    if (array[i - 1][0] + move <= minheight)
                    {
                        move = 0;
                    }

                if (i + 50 >= (symmetrisch ? array.Length / 2 : array.Length)) { move = 0; m = 25; }

                if (move == 0)
                {
                    array[i].Add(array[i - 1][0]);
                    array[i].Add(array[i - 1][1]);
                }
                else
                    if (move == 1)
                    {
                        if (dist <= 0)
                        {
                            pos--;
                            dist = (int)Math.Log(max - m + 1, Math.E);
                            array[i].Add((UInt16)(array[i - 1][0] + move + Kartenformat.SortenFaktor(LUFT)));
                            array[i].Add((UInt16)(screenHeight - (array[i - 1][0] + move) + Kartenformat.SortenFaktor(ERDE)));
                        }
                        else
                        {
                            array[i].Add((UInt16)(array[i - 1][0] + Kartenformat.SortenFaktor(LUFT)));
                            array[i].Add((UInt16)(screenHeight - array[i - 1][0] + Kartenformat.SortenFaktor(ERDE)));
                        }
                    }
                    else
                        if (move == -1)
                        {
                            if (dist <= 0)
                            {
                                pos--;
                                dist = (int)Math.Log(m, Math.E);
                                array[i].Add((UInt16)(array[i - 1][0] + move + Kartenformat.SortenFaktor(LUFT)));
                                array[i].Add((UInt16)(screenHeight - (array[i - 1][0] + move) + Kartenformat.SortenFaktor(ERDE)));
                            }
                            else
                            {
                                array[i].Add((UInt16)(array[i - 1][0] + Kartenformat.SortenFaktor(LUFT)));
                                array[i].Add((UInt16)(screenHeight - array[i - 1][0] + Kartenformat.SortenFaktor(ERDE)));
                            }
                        }

                m--;
                dist--;
            }

            // umformen (damit richtig dargestellt)
            for (int i = 0; i < array.Length; i++)
                for (int b = 0; b < array[i].Count; b++)
                    array[i][b] = (UInt16)(screenHeight - Kartenformat.Laenge(array[i][b]) + Kartenformat.SortenFaktor(Kartenformat.Material(array[i][b])));

            // bei symmetrischer Karte, wird hier die Karte verdoppelt
            List<UInt16>[] arraysymm = new List<UInt16>[array.Length / 2];
            if (symmetrisch)
            {
                for (int i = 0; i < arraysymm.Length; i++)
                {
                    arraysymm[i] = new List<UInt16>();
                    arraysymm[i].AddRange(array[i]);
                }
            }

            // Höhlen
            Höhlenkonfiguration.Generate(hoehle, (symmetrisch ? arraysymm : array), Haus.Orte);

            /*// Felsen
            CaveConf Felsen = new CaveConf();
            Felsen.setAllgemein(450, 150, 50, 25, 200, 150, Karte.FELS);
            Felsen.setA(5, 30, 0, 8, 40, 0.3f);
            Felsen.setB(15, 50, 0, 8, 50, 0.3f);
            Felsen.setC(15, 50, -40, -40, 40, 0.2f);
            CaveConf.Generate(Felsen, array);*/

            // Granit
            Höhlenkonfiguration Granit = new Höhlenkonfiguration();
            Granit.setAllgemein(450, 150, 50, 25, 200, 150, Karte.GRANIT1, false);
            Granit.setA(5, 30, 0, 8, 40, 0.3f);
            Granit.setB(15, 50, 0, 8, 50, 0.3f);
            Granit.setC(15, 50, -40, -40, 40, 0.05f);
            Höhlenkonfiguration.Generate(Granit, (symmetrisch ? arraysymm : array), Haus.Orte);

            // Sumpf
            Höhlenkonfiguration Sumpf = new Höhlenkonfiguration();
            Sumpf.setAllgemein(1000, 50, 0, 35, 200, 450, Karte.SUMPF, true);
            Sumpf.setA(5, 30, 0, 0, 0, 0.3f);
            Sumpf.setB(30, 50, 0, 5, 10, 0.5f);
            Sumpf.setC(15, 50, -40, -40, 10, 0.05f);
            Höhlenkonfiguration.Generate(Sumpf, (symmetrisch ? arraysymm : array), Haus.Orte);

            // Wasser
            Sumpf.setAllgemein(1000, 50, 0, 35, 200, 450, Karte.WASSER, true);
            Höhlenkonfiguration.Generate(Sumpf, (symmetrisch ? arraysymm : array), Haus.Orte);

            if (symmetrisch)
            {
                for (int i = 0; i < arraysymm.Length; i++)
                {
                    array[i].Clear();
                    array[i].AddRange(arraysymm[i]);
                }

                int b = 0;
                for (int i = arraysymm.Length - 1; i >= 0; i--, b++)
                {
                    array[arraysymm.Length + b].Clear();
                    array[arraysymm.Length + b].AddRange(arraysymm[i]);
                    // int a = array[arraysymm.Length + b][0];
                    // int c = a + a + 2;
                    // int d = a + c;
                }
            }
        }

        /// <summary>
        /// Explodes the specified daten.
        /// </summary>
        /// <param name="Daten">The daten.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <returns>List{Vector3}.</returns>
        public List<Vector3> Explode(List<UInt16>[] Daten, int x, int y, int width) // errechnet zerstörungen der Karte
        {
            List<Vector3> list = new List<Vector3>();
            int aa = (int)((double)Math.Log((((width) - 0) * Math.PI), Math.E) * Math.Sqrt(width));
            for (int i = -aa; i < aa; i++)
            {
                if (i + x < 0 || i + x >= Daten.Length) continue;

                int dist = i; if (dist < 0) dist = -dist;
                int add = (int)((double)Math.Log((((aa) - dist) * Math.PI), Math.E) * Math.Sqrt(width));
                int add2 = y + add;
                if (add2 > Game1.screenHeight) add2 = Game1.screenHeight;
                list.AddRange(Kartenformat.DeleteFromTo(x + i, (int)(y - add), (int)(add2)));
            }

            for (int i = 0; i < list.Count; i++) list[i] = new Vector3(list[i].X, list[i].Y - 5, list[i].Z + 5);
            if (list.Count >= 1)
            {
                Vector3 temp0 = list[0];
                Vector3 templast = list[list.Count - 1];
                for (int i = 0; i < 5; i++) list.Insert(0, new Vector3(temp0.X - i, temp0.Y, temp0.Z));
                for (int i = 0; i < 5; i++) list.Insert(list.Count - 1, new Vector3(templast.X + i, templast.Y, templast.Z));
            }
            return list;
        }

        /// <summary>
        /// Explode_missiles the specified spielfeld.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="pos">The pos.</param>
        /// <param name="Fenster">The fenster.</param>
        /// <param name="Art">The art.</param>
        public void explode_missile(List<UInt16>[] Spielfeld, Vector2 pos, Vector2 Fenster, int Art)  // Spielt den Explosionssound ab
        {
            if (Waffendaten.Explosionen[Art] >= 0)
            {
                Soundsystem[] expo = Sounds.Explosion;
                int dist = (int)(pos.X - (Fenster.X)); if (dist < 0) dist = -dist;
                expo[Waffendaten.Explosionen[Art]].PlaySoundAny(false, ((float)(1.0d - ((double)dist / Spielfeld.Length))));
            }
        }

        /// <summary>
        /// Find_dead_particles the specified particle list.
        /// </summary>
        /// <param name="particleList">The particle list.</param>
        /// <param name="id">The id.</param>
        /// <returns>System.Int32.</returns>
        public int find_dead_particle(List<ParticleData> particleList, int id)
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                if (particleList[i].alive == false)
                {
                    // gefunden
                    int found = -1;
                    for (int b = particleList.Count - 1; b > i; b--)
                    {
                        if (particleList[b].alive == true)
                        {
                            found = b;
                            break;
                        }
                    }

                    if (found != -1)
                    {
                        if (found + 1 < particleList.Count && particleList.Count - (found) - 1 > 0)
                        {
                            particleList.RemoveRange(found + 1, particleList.Count - (found) - 1);
                            switch (id)
                            {
                                case 0: { particleFarbe.RemoveRange(found + 1, particleList.Count - (found) - 1); break; }
                                case 1: { particleFarbeS.RemoveRange(found + 1, particleList.Count - (found) - 1); break; }
                                case 2:
                                    {
                                        particleFarbeS2.RemoveRange(found + 1, particleList.Count - (found) - 1);
                                        particleMapSmokeData.RemoveRange(found + 1, particleList.Count - (found) - 1);
                                        break;
                                    }
                            }
                        }
                    }
                    return i;
                }
            }
            return -1;
        }

        // TODO ausfüllen
        /// <summary>
        /// Erzeugt den Inhalt des Effektes aus einem String
        /// </summary>
        /// <param name="Text">der Text in dem der Effekt definiert ist</param>
        public List<UInt16>[] Laden(List<String> Text)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "KARTE");
            if (Text2.Count == 0) return null;

            List<UInt16>[] temp = new List<UInt16>[Text2.Count];

            for (int b = 0; b < Text2.Count; b++)
            {
                temp[b] = new List<UInt16>();
                String[] q = Text2[b].Split('-');
                for (int i = 0; i < q.Length; i++)
                    temp[b].Add(Convert.ToUInt16(q[i]));
            }

            return temp;
        }

        /// <summary>
        /// Wandelt den Effekt zum Speichern in einen Text um
        /// </summary>
        /// <returns>Gibt den zu speichernden Text zurück</returns>
        public List<String> Speichern(List<UInt16>[] Spielfeld)
        {
            List<String> data = new List<String>();
            data.Add("[KARTE]");
            for (int i = 0; i < Spielfeld.Length; i++)
            {
                String add = "";
                for (int j = 0; j < Spielfeld[i].Count; j++)
                {
                    // if (j == 0 && Spielfeld[i][j] < Kartenformat.SortenFaktor(1)) continue;

                    add = add + (Spielfeld[i][j]).ToString();
                    if (j + 1 < Spielfeld[i].Count)
                        add = add + ('-');
                }
                //if (i + 1 < Spielfeld.Spielfeld.Length)
                data.Add(add);
            }

            data.Add("[/KARTE]");

            return data;
        }

        /// <summary>
        /// Updates the particles.
        /// </summary>
        /// <param name="particleList">The particle list.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="id">The id.</param>
        public void UpdateParticles(List<ParticleData> particleList, GameTime gameTime, int id) // geändert
        {
            float now = (float)gameTime.TotalGameTime.TotalMilliseconds;
            for (int i = particleList.Count - 1; i >= 0; i--)
            {
                ParticleData particle = particleList[i];
                if (!particle.alive) continue;
                float timeAlive = now - particle.BirthTime;

                if (timeAlive > particle.MaxAge)
                {
                    switch (id)
                    {
                        case 2:
                            {
                                if (particleMapSmokeData[i].Z > 0)
                                {
                                    particleMapSmokeData[i] = new Vector3(particleMapSmokeData[i].X, Kartenformat.BottomOf(particleMapSmokeData[i].X, particleMapSmokeData[i].Y), particleMapSmokeData[i].Z - 1);
                                    int idet = particle.art;
                                    particle.set = false;
                                    int maxAge = (int)((float)((float)Waffendaten.Daten[0].W * 8)); // /20 *(20-fakt)

                                    particle.OrginalPosition = new Vector2(particleMapSmokeData[i].X, particleMapSmokeData[i].Y);
                                    particle.Position = particle.OrginalPosition;

                                    particle.BirthTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
                                    if (particleMapSmokeData[i].Z >= 0)
                                    {
                                        particle.MaxAge = (float)(maxAge - ((float)rand.Next(0, (int)((float)maxAge * 0.2))));
                                    }
                                    else
                                        particle.MaxAge = (float)(maxAge - ((float)rand.Next(0, (int)((float)maxAge * 0.6))));

                                    particle.Scaling = 0.25f;
                                    particle.ModColor = Microsoft.Xna.Framework.Color.White;
                                    particle.alive = true;
                                    float particleDistance = (float)rand.NextDouble() * Waffendaten.Daten[0].Z;
                                    Vector2 displacement = new Vector2(particleDistance, 0);
                                    float angle = 0;
                                    angle = MathHelper.ToRadians(rand.Next(225, 315));
                                    displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(angle));

                                    particle.Direction = displacement * 2.0f;
                                    particle.Accelaration = -particle.Direction;
                                    particleList[i] = particle;
                                }
                                else
                                {
                                    particle.alive = false;
                                    particleList[i] = particle;
                                }

                                break;
                            }
                    }
                    if (id != 2) { particle.alive = false; particleList[i] = particle; }
                }
                else
                {
                    float relAge = timeAlive / particle.MaxAge;
                    particle.Position = 0.5f * particle.Accelaration * relAge * relAge + particle.Direction * relAge + particle.OrginalPosition;

                    float invAge = 1.0f - relAge;
                    Vector2 positionFromCenter = particle.Position - particle.OrginalPosition;
                    float distance = positionFromCenter.Length();
                    particle.set = true;
                    switch (id)
                    {
                        case 0:
                            {
                                particle.ModColor = new Microsoft.Xna.Framework.Color(new Vector4((float)(invAge * particleFarbe[i].X),
                                    (float)(invAge * particleFarbe[i].Y), (float)(invAge * particleFarbe[i].Z), 10f * invAge));
                                particle.Scaling = (Waffendaten.Explosionscale[particle.art] + distance) / 200.0f;
                                break;
                            }
                        case 1:
                            {
                                particle.ModColor = new Microsoft.Xna.Framework.Color(new Vector4(Waffendaten.Daten4[particle.art].X * invAge, Waffendaten.Daten4[particle.art].Y * invAge, Waffendaten.Daten4[particle.art].Z * invAge, Waffendaten.Daten3[particle.art].W * invAge));
                                particle.Scaling = (2.0f + distance) / 200.0f;
                                break;
                            }
                        case 2:
                            {
                                particle.ModColor = new Microsoft.Xna.Framework.Color(new Vector4(0.5f * invAge, 0.5f * invAge, 0.5f * invAge, 8f * invAge));
                                particle.Scaling = (2.0f + distance) / 200.0f;
                                break;
                            }
                    }

                    particleList[i] = particle;
                }
            }
        }

        /// <summary>
        /// Adds the explosion particle.
        /// </summary>
        /// <param name="particleList">The particle list.</param>
        /// <param name="explosionPos">The explosion pos.</param>
        /// <param name="explosionSize">Size of the explosion.</param>
        /// <param name="maxAge">The max age.</param>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Farben">The farben.</param>
        /// <param name="Weapon">The weapon.</param>
        /// <param name="id">The id.</param>
        private void AddExplosionParticle(List<ParticleData> particleList, Vector2 explosionPos, float explosionSize, float maxAge, GameTime gameTime, Vector3 Farben, int Weapon, int id) // geändert
        {
            ParticleData particle = new ParticleData();
            particle.OrginalPosition = explosionPos;
            particle.Position = particle.OrginalPosition;
            particle.BirthTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            if (id == 2)
            {
                // maxAge *= 0.5f;
                particle.MaxAge = (float)(maxAge - ((float)rand.Next(0, (int)((float)maxAge * 0.9))));
            }
            else
                particle.MaxAge = maxAge;

            particle.art = Weapon;
            particle.set = false;
            particle.Scaling = 0.25f;
            particle.ModColor = Microsoft.Xna.Framework.Color.White;

            float particleDistance = (float)rand.NextDouble() * explosionSize;
            Vector2 displacement = new Vector2(particleDistance, 0);
            float angle = 0;

            switch (id)
            {
                case 0: { angle = MathHelper.ToRadians(rand.Next(360)); break; }
                case 1: { angle = MathHelper.ToRadians(rand.Next(360)); break; }
                case 2: { angle = MathHelper.ToRadians(rand.Next(225, 315)); break; }
            }
            displacement = Vector2.Transform(displacement, Matrix.CreateRotationZ(angle));

            particle.Direction = displacement * 2.0f;
            particle.Accelaration = -particle.Direction;
            particle.alive = true;
            int position = find_dead_particle(particleList, id);
            if (position <= 0)
            {
                particleList.Add(particle);

                switch (id)
                {
                    case 0: { particleFarbe.Add(Farben); break; }
                    case 1: { particleFarbeS.Add(Farben); break; }
                    case 2: { particleFarbeS2.Add(Farben); particleMapSmokeData.Add(new Vector3(explosionPos.X, explosionPos.Y, 7)); break; }
                }
            }
            else
            {
                particleList[position] = particle;
                switch (id)
                {
                    case 0: { particleFarbe[position] = Farben; break; }
                    case 1: { particleFarbeS[position] = Farben; break; }
                    case 2: { particleFarbeS2[position] = Farben; particleMapSmokeData[position] = new Vector3(explosionPos.X, explosionPos.Y, 7); break; }
                }
            }
        }

        /// <summary>
        /// Enthält Sammlung von Werten von einen Partikel
        /// </summary>
        public struct ParticleData
        {
            #region Fields

            /// <summary>
            /// Beschleunigung des Partikels (x- und y Richtung)
            /// </summary>
            public Vector2 Accelaration;

            /// <summary>
            /// The alive
            /// </summary>
            public bool alive;

            /// <summary>
            /// enthält die munitionsart ID, 0 bis ...
            /// </summary>
            public int art;

            /// <summary>
            /// wann Partikel erschaffen wurde
            /// </summary>
            public float BirthTime;

            /// <summary>
            /// Die Bewegungsrichtung des Partikels
            /// </summary>
            public Vector2 Direction;

            /// <summary>
            /// nach welcher Dauer muss der Partikel entfernt werden
            /// </summary>
            public float MaxAge;

            /// <summary>
            /// ???
            /// </summary>
            public Microsoft.Xna.Framework.Color ModColor;

            /// <summary>
            /// wo wurde der Partikel erschaffen
            /// </summary>
            public Vector2 OrginalPosition;

            /// <summary>
            /// Die Position des Partikels
            /// </summary>
            public Vector2 Position;

            /// <summary>
            /// Die Skalierung des Partikels
            /// </summary>
            public float Scaling;

            /// <summary>
            /// The set
            /// </summary>
            public bool set;

            #endregion Fields
        }

        // speichert die farbwerte der particle für explosion

        // speichert die farbwerte der particle für raketensmoke

        // speichert die farbwerte der particle für mapsmoke

        #region Materialnummern

        /// <summary>
        ///  ID des Material Backstein1 (Brücke)
        /// </summary>
        public static int BACKSTEIN1 = 2;

        /// <summary>
        ///  ID des Material Backstein2 (getroffene Brücke)
        /// </summary>
        public static int BACKSTEIN2 = 3;

        /// <summary>
        ///  ID des Material Beton
        /// </summary>
        public static int BETON = 4;

        /// <summary>
        ///  ID des Material Erde
        /// </summary>
        public static int ERDE = 1;

        /// <summary>
        ///  ID des Material Fels
        /// </summary>
        public static int FELS = 5;

        /// <summary>
        ///  ID des Material Feuer (Material für Brandherde)
        /// </summary>
        public static int FEUER = 10;

        /// <summary>
        ///  ID des Material Granit
        /// </summary>
        public static int GRANIT1 = 6;

        /// <summary>
        ///  ID des Material Granit (getroffen)
        /// </summary>
        public static int GRANIT2 = 7;

        /// <summary>
        /// ID des Material Luft
        /// </summary>
        public static int LUFT = 0;

        /// <summary>
        ///  ID des Material Sumpf
        /// </summary>
        public static int SUMPF = 8;

        /// <summary>
        ///  ID des Material Wasser
        /// </summary>
        public static int WASSER = 9;

        #endregion Materialnummern

        /*Bsp.
         int[] Daten;
         Daten = new int[1000];
         Karte a = new Karte();
         a.create_map(Daten, 150, 50, 30, 100, 25,pictureBox1.Height);
         int X = 1; int Y=1; // Koordinaten
         a.Explode(Daten,X, Y, 20, pictureBox1.Height);*/
        /*Bsp.
          int[] Daten;
          Daten = new int[1000];
          Karte a = new Karte();
          a.create_map(Daten, 150, 50, 30, 100, 25, screenHeight); */
    }
}