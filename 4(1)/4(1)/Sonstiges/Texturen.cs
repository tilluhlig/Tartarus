// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 06-25-2013
// ***********************************************************************
// <copyright file="Texturen.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    /// <summary>
    ///     Class Texturen
    /// </summary>
    public static class Texturen
    {
        #region Fields

        public static Dictionary<String, float> Bilddateien = new Dictionary<String, float>();

        public static Effect effect;

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Baeumes the skalierung.
        /// </summary>
        public static void BaeumeSkalierung()
        {
            /* if (Baumdata.oldscale == null)
             {
                 Baumdata.oldscale = new float[Baumdata.SKALIERUNG.Count()];
                 Baumdata.SKALIERUNG.CopyTo(Baumdata.oldscale, 0);
             }
             else
             {
                 Baumdata.SKALIERUNG = new float[Baumdata.oldscale.Count()];
                 Baumdata.oldscale.CopyTo(Baumdata.SKALIERUNG, 0);
             }*/

            for (int i = 0; i < baum.Count(); i++)
            {
                #if DEBUG
                           /*     if (Baumdata.SKALIERUNG.Wert[i] == 1.0f) continue;
                                var rt = new RenderTarget2D(Game1.device, (int)(baum[i].Width * Baumdata.SKALIERUNG.Wert[i]),
                                    (int)(baum[i].Height * Baumdata.SKALIERUNG.Wert[i]));
                               
                   Game1.SpriteBatchSemaphor.WaitOne();
                  Game1.device.SetRenderTarget(rt);
                                var rect = new Rectangle(0, 0, (int)(baum[i].Width * Baumdata.SKALIERUNG.Wert[i]),
                                    (int)(baum[i].Height * Baumdata.SKALIERUNG.Wert[i]));
                  
                 
                                Game1.spriteBatch.Begin();
                                Game1.device.Clear(Color.Transparent);
                                Game1.spriteBatch.Draw(baum[i], rect, Color.White);
                                Game1.spriteBatch.End();
                  
                  
                                Game1.device.SetRenderTarget(null);
                  Game1.SpriteBatchSemaphor.Release();
                  
                                rt.Tag = baum[i].Tag;
                                baum[i] = rt;*/
#else
                Baumdata.SKALIERUNG.Wert[i] = 1.0f;
                #endif
                ///   
            }
        }

        /// <summary>
        ///     Panzers the skalierung.
        /// </summary>
        public static void FahrzeugSkalierung()
        {
            for (int i = 0; i < panzerindex.Count(); i++)
            {
#if DEBUG
                if (Fahrzeugdaten.SCALEP.Wert[i] == 1.0f) continue;
                var rt = new RenderTarget2D(Game1.device, (int)(panzerindex[i].Width * Fahrzeugdaten.SCALEP.Wert[i]),
                    (int)(panzerindex[i].Height * Fahrzeugdaten.SCALEP.Wert[i]));
               
                Game1.SpriteBatchSemaphor.WaitOne();
                Game1.device.SetRenderTarget(rt);
                var rect = new Rectangle(0, 0, (int)(panzerindex[i].Width * Fahrzeugdaten.SCALEP.Wert[i]),
                    (int)(panzerindex[i].Height * Fahrzeugdaten.SCALEP.Wert[i]));

                
                Game1.spriteBatch.Begin();
                Game1.device.Clear(Color.Transparent);
                Game1.spriteBatch.Draw(panzerindex[i], rect, Color.White);
                Game1.spriteBatch.End();
                

                Game1.device.SetRenderTarget(null);
                Game1.SpriteBatchSemaphor.Release();

                rt.Tag = panzerindex[i].Tag;
                panzerindex[i] = rt;

                rt = new RenderTarget2D(Game1.device, (int)(panzerindexreifen[i].Width * Fahrzeugdaten.SCALEP.Wert[i]),
                    (int)(panzerindexreifen[i].Height * Fahrzeugdaten.SCALEP.Wert[i]));

                Game1.SpriteBatchSemaphor.WaitOne();
                Game1.device.SetRenderTarget(rt);
                rect = new Rectangle(0, 0, (int)(panzerindexreifen[i].Width * Fahrzeugdaten.SCALEP.Wert[i]),
                    (int)(panzerindexreifen[i].Height * Fahrzeugdaten.SCALEP.Wert[i]));

                
                Game1.spriteBatch.Begin();
                Game1.device.Clear(Color.Transparent);
                Game1.spriteBatch.Draw(panzerindexreifen[i], rect, Color.White);
                Game1.spriteBatch.End();
                

                Game1.device.SetRenderTarget(null);
                Game1.SpriteBatchSemaphor.Release();

                rt.Tag = panzerindexreifen[i].Tag;
                panzerindexreifen[i] = rt;

                rt = new RenderTarget2D(Game1.device, (int)(panzerruine[i].Width * Fahrzeugdaten.SCALEP.Wert[i]),
                    (int)(panzerruine[i].Height * Fahrzeugdaten.SCALEP.Wert[i]));
                
                Game1.SpriteBatchSemaphor.WaitOne();
                Game1.device.SetRenderTarget(rt);
                rect = new Rectangle(0, 0, (int)(panzerruine[i].Width * Fahrzeugdaten.SCALEP.Wert[i]),
                    (int)(panzerruine[i].Height * Fahrzeugdaten.SCALEP.Wert[i]));

                
                Game1.spriteBatch.Begin();
                Game1.device.Clear(Color.Transparent);
                Game1.spriteBatch.Draw(panzerruine[i], rect, Color.White);
                Game1.spriteBatch.End();
                

                Game1.device.SetRenderTarget(null);
                Game1.SpriteBatchSemaphor.Release();

                rt.Tag = panzerruine[i].Tag;
                panzerruine[i] = rt;
#endif
                Fahrzeugdaten.SCALEP.Wert[i] = 1.0f;
            }

            for (int i = 0; i < panzerrohrindex.Count(); i++)
            {
#if DEBUG
                if ((int)(panzerrohrindex[i].Width * Fahrzeugdaten.SCALER.Wert[i]) > 0 &&
                    (int)(panzerrohrindex[i].Height * Fahrzeugdaten.SCALER.Wert[i]) > 0)
                {
                    var rt = new RenderTarget2D(Game1.device,
                        (int)(panzerrohrindex[i].Width * Fahrzeugdaten.SCALER.Wert[i]),
                        (int)(panzerrohrindex[i].Height * Fahrzeugdaten.SCALER.Wert[i]));

                    Game1.SpriteBatchSemaphor.WaitOne();
                    Game1.device.SetRenderTarget(rt);
                    var rect = new Rectangle(0, 0, (int)(panzerrohrindex[i].Width * Fahrzeugdaten.SCALER.Wert[i]),
                        (int)(panzerrohrindex[i].Height * Fahrzeugdaten.SCALER.Wert[i]));

                    
                    Game1.spriteBatch.Begin();
                    Game1.device.Clear(Color.Transparent);
                    Game1.spriteBatch.Draw(panzerrohrindex[i], rect, Color.White);
                    Game1.spriteBatch.End();
                   

                    Game1.device.SetRenderTarget(null);
                    Game1.SpriteBatchSemaphor.Release();

                    panzerrohrindex[i] = rt;
#endif
                    Fahrzeugdaten.SCALER.Wert[i] = 1.0f;
#if DEBUG
                }
#endif
            }
        }

        public static Texture2D FromFile(String Datei)
        {
            Texture2D fileTexture;
            using (var fileStream = new FileStream(Datei, FileMode.Open))
            {
                fileTexture = Texture2D.FromStream(Game1.device, fileStream);
            }
            return fileTexture;
        }

        #endregion Methods

        #region Texturdeklarationen

        /// <summary>
        ///     das Logo des Spiels
        /// </summary>
        public static Texture2D tartarus;

        /// <summary>
        ///     The active background
        /// </summary>
        public static Texture2D activeBackground;

        /// <summary>
        ///     The algerian font
        /// </summary>
        public static SpriteFont AlgerianFont;

        /// <summary>
        ///     The background1
        /// </summary>
        public static Texture2D background1;

        /// <summary>
        ///     The baum
        /// </summary>
        public static Texture2D[] baum = new Texture2D[26];

        /// <summary>
        ///     The bunker
        /// </summary>
        public static Texture2D[] bunker = new Texture2D[3];

        /// <summary>
        ///     The bunker2
        /// </summary>
        public static Texture2D[] bunker2 = new Texture2D[3];

        /// <summary>
        ///     The button1
        /// </summary>
        public static Texture2D Button1;

        /// <summary>
        ///     The cannon origin
        /// </summary>
        public static Vector2[][] CannonOrigin = new Vector2[6][];

        /// <summary>
        ///     The comboboxbalken
        /// </summary>
        ///public static Texture2D Comboboxbalken;

        /// <summary>
        ///     The dot
        /// </summary>
        public static Texture2D dot;

        /// <summary>
        ///     The dot2
        /// </summary>
        public static Texture2D dot2;

        /// <summary>
        ///     The exp
        /// </summary>
        ///public static Texture2D Exp;

        /// <summary>
        ///     The explosion
        /// </summary>
        public static Texture2D explosion;

        /// <summary>
        ///     The fahne
        /// </summary>
        public static Texture2D fahne;

        /// <summary>
        ///     The font
        /// </summary>
        public static SpriteFont font = null;

        /// <summary>
        ///     The font2
        /// </summary>
        public static SpriteFont font2=null;

        /// <summary>
        ///     The font3
        /// </summary>
        public static SpriteFont font3 = null;

        public static SpriteFont font4 = null;

        /// <summary>
        ///     The freeze
        /// </summary>
        /// public static Texture2D freeze;

        /// <summary>
        ///     The fuel
        /// </summary>
        public static Texture2D fuel;

        /// <summary>
        ///     The geld
        /// </summary>
        ///public static Texture2D Geld;

        /// <summary>
        ///     The haus
        /// </summary>
        public static Texture2D[] haus = new Texture2D[18];

        /// <summary>
        ///     The hausbutton
        /// </summary>
        public static Texture2D hausbutton;

        /// <summary>
        ///     The hausumriss
        /// </summary>
        public static Texture2D[] hausumriss = new Texture2D[18];

        /// <summary>
        ///     The hp status
        /// </summary>
        ///public static Texture2D HpStatus;

        /// <summary>
        ///     The spielerkennzeichnung
        /// </summary>
        public static Texture2D Karte;

        /// <summary>
        ///     The kasten
        /// </summary>
        public static Texture2D kasten;

        /// <summary>
        ///     The klotzchen
        /// </summary>
        ///public static Texture2D klotzchen;

        /// <summary>
        ///     The kreis
        /// </summary>
        public static Texture2D kreis;

        /// <summary>
        ///     The kreuz
        /// </summary>
        public static Texture2D kreuz;

        /// <summary>
        ///     The leben
        /// </summary>
        public static Texture2D leben;

        /// <summary>
        ///     The load
        /// </summary>
        public static Texture2D LeeresFeld;

        /// <summary>
        ///     The maus zeiger
        /// </summary>
        public static Texture2D mausZeiger;

        /// <summary>
        ///     The miniback
        /// </summary>
        public static Texture2D miniback;

        /// <summary>
        ///     The missle
        /// </summary>
        public static Texture2D[] missle = new Texture2D[21];

        /// <summary>
        ///     The nach oben
        /// </summary>
        public static Texture2D nachOben;

        /// <summary>
        ///     The nach unten
        /// </summary>
        public static Texture2D nachUnten;

        /// <summary>
        ///     The nichts
        /// </summary>
        public static Texture2D nichts;

        public static Texture2D Notizmarkierung;

        public static Texture2D NotizmarkierungUmriss;

        /// <summary>
        ///     The spielerkennzeichnung
        /// </summary>
        public static Texture2D Objekte;

        /// <summary>
        ///     The ort
        /// </summary>
        public static Texture2D Ort;

        /// <summary>
        ///     The panzerbutton
        /// </summary>
        public static Texture2D[] panzerbutton = new Texture2D[6];

        /// <summary>
        ///     The panzerindex
        /// </summary>
        public static Texture2D[] panzerindex = new Texture2D[6];

        /// <summary>
        ///     The panzerindexreifen
        /// </summary>
        public static Texture2D[] panzerindexreifen = new Texture2D[6];

        /// <summary>
        ///     The panzerrohrindex
        /// </summary>
        public static Texture2D[] panzerrohrindex = new Texture2D[6];

        /// <summary>
        ///     The panzerrohrumriss
        /// </summary>
        public static Texture2D[] panzerrohrumriss = new Texture2D[6];

        /// <summary>
        ///     The panzerrohrumriss2
        /// </summary>
        public static Texture2D[] panzerrohrumriss2 = new Texture2D[6];

        // public static Texture2D nebel;
        // public static Texture2D nebelkreis;
        /// <summary>
        ///     The panzerruine
        /// </summary>
        public static Texture2D[] panzerruine = new Texture2D[6];

        /// <summary>
        ///     The panzerumriss
        /// </summary>
        public static Texture2D[] panzerumriss = new Texture2D[6];

        /// <summary>
        ///     The panzerumriss2
        /// </summary>
        public static Texture2D[] panzerumriss2 = new Texture2D[6];

        /// <summary>
        ///     The panzerumriss2reifen
        /// </summary>
        public static Texture2D[] panzerumriss2reifen = new Texture2D[6];

        /// <summary>
        ///     The panzerumrissreifen
        /// </summary>
        public static Texture2D[] panzerumrissreifen = new Texture2D[6];

        /// <summary>
        ///     The pfeil
        /// </summary>
        public static Texture2D pfeil;

        //public static Texture2D leer;
        /// <summary>
        ///     The leer
        /// </summary>
        /// <summary>
        ///     The pregamemenu
        /// </summary>
        ///public static Texture2D pregamemenu;

        /// <summary>
        ///     The punkt
        /// </summary>
        public static Texture2D Punkt;

        /// <summary>
        ///     The radpositionen
        /// </summary>
        public static Vector2[][] Radpositionen = new Vector2[6][];

        /// <summary>
        ///     The rahmen
        /// </summary>
        public static Texture2D rahmen;

        /// <summary>
        ///     The rohr pos
        /// </summary>
        public static Vector2[] RohrPos = new Vector2[6];

        /// <summary>
        ///     The rohr pos2
        /// </summary>
        public static Vector2[] RohrPos2 = new Vector2[6];

        /// <summary>
        ///     The shootingpower
        /// </summary>
        public static Texture2D shootingpower;

        /// <summary>
        ///     The smoke texture
        /// </summary>
        public static Texture2D smokeTexture;

        /// <summary>
        ///     The spielerkennzeichnung
        /// </summary>
        public static Texture2D Spielerkennzeichnung;

        /// <summary>
        ///     The strich
        /// </summary>
        public static Texture2D strich;

        /// <summary>
        ///     The tank pos
        /// </summary>
        public static Vector2[] TankPos = new Vector2[6];

        /// <summary>
        ///     The tickbox off
        /// </summary>
        public static Texture2D tickboxOff;

        /// <summary>
        ///     The tickbox on
        /// </summary>
        public static Texture2D tickboxOn;

        // public static Texture2D Spielermenu;
        /// <summary>
        ///     The spielermenu
        /// </summary>
        /// <summary>
        ///     The tunnel
        /// </summary>
        public static Texture2D tunnel;

        //  public static Texture2D Startmenu;
        //public static Texture2D Pausenmenu;
        /// <summary>
        ///     The startmenu
        /// </summary>
        /// <summary>
        ///     The pausenmenu
        /// </summary>
        /// <summary>
        ///     The tunnelumriss
        /// </summary>
        public static Texture2D tunnelumriss;

        /// <summary>
        ///     The waffenbilder
        /// </summary>
        public static Texture2D[] waffenbilder = new Texture2D[21];

        /// <summary>
        ///     The wasser
        /// </summary>
        public static Texture2D wasser;

        /// <summary>
        ///     The wind
        /// </summary>
        public static Texture2D wind;

        #endregion Texturdeklarationen

        /// <summary>
        ///     Haeusers the skalierung.
        /// </summary>
        public static void HaeuserSkalierung()
        {
            /*  if (Gebäudedaten.oldscale == null)
              {
                  Gebäudedaten.oldscale = new float[Gebäudedaten.SKALIERUNG.Count()];
                  Gebäudedaten.SKALIERUNG.CopyTo(Gebäudedaten.oldscale, 0);
              }
              else
              {
                  Gebäudedaten.SKALIERUNG = new float[Gebäudedaten.oldscale.Count()];
                  Gebäudedaten.oldscale.CopyTo(Gebäudedaten.SKALIERUNG, 0);
              }*/

            for (int i = 0; i < haus.Count(); i++)
            {
                #if DEBUG
                         /*       if (Gebäudedaten.SKALIERUNG.Wert[i] == 1.0f) continue;
                                var rt = new RenderTarget2D(Game1.device, (int)(haus[i].Width * Gebäudedaten.SKALIERUNG.Wert[i]),
                                    (int)(haus[i].Height * Gebäudedaten.SKALIERUNG.Wert[i]));
                              
                  Game1.SpriteBatchSemaphor.WaitOne();
                  Game1.device.SetRenderTarget(rt);
                                var rect = new Rectangle(0, 0, (int)(haus[i].Width * Gebäudedaten.SKALIERUNG.Wert[i]),
                                    (int)(haus[i].Height * Gebäudedaten.SKALIERUNG.Wert[i]));
                  
                  
                                Game1.spriteBatch.Begin();
                                Game1.device.Clear(Color.Transparent);
                                Game1.spriteBatch.Draw(haus[i], rect, Color.White);
                                Game1.spriteBatch.End();
                  
                  
                                Game1.device.SetRenderTarget(null);
                  Game1.SpriteBatchSemaphor.Release();
                 
                                rt.Tag = haus[i].Tag;
                                haus[i] = rt;*/
#else
                Gebäudedaten.SKALIERUNG.Wert[i] = 1.0f;
                #endif
                ///
            }

            #if DEBUG
                    /*    var rt2 = new RenderTarget2D(Game1.device, (int)(tunnel.Width * Tunnel.SKALIERUNG),
                            (int)(tunnel.Height * Tunnel.SKALIERUNG));
                      
              Game1.SpriteBatchSemaphor.WaitOne();
              Game1.device.SetRenderTarget(rt2);
                        var rect2 = new Rectangle(0, 0, (int)(tunnel.Width * Tunnel.SKALIERUNG),
                            (int)(tunnel.Height * Tunnel.SKALIERUNG));
              
              
                        Game1.spriteBatch.Begin();
                        Game1.device.Clear(Color.Transparent);
                        Game1.spriteBatch.Draw(tunnel, rect2, Color.White);
                        Game1.spriteBatch.End();
              
              
                        Game1.device.SetRenderTarget(null);
              Game1.SpriteBatchSemaphor.Release();
             
                        rt2.Tag = tunnel.Tag;
                        tunnel = rt2;*/
#else
            Tunnel.SKALIERUNG = 1.0f;
            #endif
        }

        /// <summary>
        ///     Loads the specified content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public static void Load(ContentManager Content) // lädt alle Texturen
        {
            Bilddateien.Clear();

            effect = Content.Load<Effect>("Shader2");

            #region Panzerwerte

            // Radpositionen
            Radpositionen[0] = new Vector2[0];
            Radpositionen[1] = new Vector2[0];
            Radpositionen[2] = new Vector2[0];
            Radpositionen[3] = new[] { new Vector2(-18, 6), new Vector2(16, 6) };
            Radpositionen[4] = new Vector2[0];
            Radpositionen[5] = new Vector2[0];

            //bestimmt Rohrposition bei Panzer auf der Karte
            RohrPos[0] = new Vector2(5, 25);
            RohrPos[1] = new Vector2(-15, 22);
            RohrPos[2] = Vector2.Zero;
            RohrPos[3] = new Vector2(0, 29);
            RohrPos[4] = new Vector2(0, 15);
            RohrPos[5] = new Vector2(0, 15);
            //RohrPos[3] = new Vector2(0, 25);

            //für Spielermenü: Panzerposition
            TankPos[0] = new Vector2(30, 16);
            TankPos[1] = new Vector2(20, 16);
            TankPos[2] = new Vector2(30, 30);
            TankPos[3] = new Vector2(30, 30);
            TankPos[4] = new Vector2(30, 16);
            TankPos[5] = new Vector2(20, 16);

            //für Spielermenü: Panzerrohrposition
            RohrPos2[0] = new Vector2(5, 15);
            //RohrPos2[1] = new Vector2(40, 25);
            RohrPos2[1] = new Vector2(40, 25);
            RohrPos2[2] = Vector2.Zero;
            RohrPos2[3] = new Vector2(-5, 20);

            for (int i = 0; i < CannonOrigin.Count(); i++)
                CannonOrigin[i] = new Vector2[2];
            CannonOrigin[0][0] = new Vector2(250, 25);
            CannonOrigin[0][1] = new Vector2(-250, 25);

            //CannonOrigin[1][0] = new Vector2(186, 14);
            //CannonOrigin[1][1] = new Vector2(-168, 14);

            CannonOrigin[1][0] = new Vector2(186, 14);
            CannonOrigin[1][1] = new Vector2(-168, 14);

            CannonOrigin[2][0] = new Vector2(163, 60);
            CannonOrigin[2][1] = new Vector2(-163, 60);

            CannonOrigin[3][0] = new Vector2(172, 27);
            CannonOrigin[3][1] = new Vector2(-172, 27);

            CannonOrigin[4][0] = new Vector2(75, 16);
            CannonOrigin[4][1] = new Vector2(26, 16);

            CannonOrigin[5][0] = new Vector2(80, 10);
            CannonOrigin[5][1] = new Vector2(9, 10);
            for (int i = 0; i < CannonOrigin.Count(); i++)
            {
                CannonOrigin[i][0] *= Fahrzeugdaten.SCALER.Wert[i];
                CannonOrigin[i][1] *= Fahrzeugdaten.SCALER.Wert[i];
            }

            #endregion Panzerwerte

            #region Loading Content

            Punkt = new Texture2D(Game1.device, 1, 1);
            var tt = new Color[1];
            tt[0] = Color.White;

            Game1.SpriteBatchSemaphor.WaitOne();
            Punkt.SetData(tt);
            Game1.SpriteBatchSemaphor.Release();

            Bilddateien.Add("Tartarus", 1.0f);
            tartarus = Content.Load<Texture2D>("Textures\\Tartarus");

            Bilddateien.Add("powerup", 0.8f);
            shootingpower = Content.Load<Texture2D>("Textures\\powerup"); // neues

            Bilddateien.Add("leer", 0.25f);
            LeeresFeld = Content.Load<Texture2D>("Textures\\leer");

            //freeze = Content.Load<Texture2D>("Textures\\freeze");

            Bilddateien.Add("alt_oben", 0.25f);
            nachOben = Content.Load<Texture2D>("Textures\\alt_oben");

            Bilddateien.Add("markierung", 0.125f);
            Notizmarkierung = Content.Load<Texture2D>("Textures\\markierung");

            NotizmarkierungUmriss = Umriss.Generieren(Notizmarkierung, Color.White, 2);

            Bilddateien.Add("alt_unten", 0.25f);
            nachUnten = Content.Load<Texture2D>("Textures\\alt_unten");

            Bilddateien.Add("rahmen", 0.25f);
            rahmen = Content.Load<Texture2D>("Textures\\rahmen");

            Bilddateien.Add("Bauen", 0.25f);
            Karte = Content.Load<Texture2D>("Textures\\Bauen");

            Bilddateien.Add("Objekte", 0.25f);
            Objekte = Content.Load<Texture2D>("Textures\\Objekte");

            Ort = Content.Load<Texture2D>("Textures\\Ort");

            tickboxOn = Content.Load<Texture2D>("Textures\\tickboxOn");
            tickboxOff = Content.Load<Texture2D>("Textures\\tickboxOff");
            //Comboboxbalken = Content.Load<Texture2D>("Textures\\Comboboxbalken");
            ///klotzchen = Content.Load<Texture2D>("Textures\\klotzchen");
            // Startmenu = Content.Load<Texture2D>("Textures\\Startmenu4"); ;
            ///pregamemenu = Content.Load<Texture2D>("Textures\\pregamemenu");
            hausbutton = Content.Load<Texture2D>("Textures\\hausbutton");
            Button1 = Content.Load<Texture2D>("Textures\\Button1");
            // Pausenmenu = Content.Load<Texture2D>("Textures\\Pausenmenu");
            nichts = Content.Load<Texture2D>("Textures\\nichts");
            Spielerkennzeichnung = Content.Load<Texture2D>("Textures\\Spieler");
            kreis = Content.Load<Texture2D>("Textures\\kreis");
            //Spielermenu = Mod.SPIELERMENU_VISIBLE.Wert ? Content.Load<Texture2D>("Textures\\spielermenu3") : null;

            //  nebel = Content.Load<Texture2D>("Textures\\nebel");
            //  nebelkreis = Content.Load<Texture2D>("Textures\\nebelkreis");

            pfeil = Content.Load<Texture2D>("Textures\\pfeil");

            Bilddateien.Add("standardmisslebutton", 0.25f);
            waffenbilder[0] = Content.Load<Texture2D>("Textures\\standardmisslebutton");

            Bilddateien.Add("bigmisslebutton", 0.25f);
            waffenbilder[1] = Content.Load<Texture2D>("Textures\\bigmisslebutton");

            Bilddateien.Add("cryomisslebutton", 0.25f);
            waffenbilder[2] = Content.Load<Texture2D>("Textures\\cryomisslebutton");

            Bilddateien.Add("poisonmisslebutton", 0.25f);
            waffenbilder[3] = Content.Load<Texture2D>("Textures\\poisonmisslebutton");

            Bilddateien.Add("nukemisslebutton", 0.25f);
            waffenbilder[4] = Content.Load<Texture2D>("Textures\\nukemisslebutton");

            Bilddateien.Add("airstrikebutton", 0.25f);
            waffenbilder[5] = Content.Load<Texture2D>("Textures\\airstrikebutton");

            Bilddateien.Add("geschossbutton", 0.25f);
            waffenbilder[6] = Content.Load<Texture2D>("Textures\\geschossbutton");

            Bilddateien.Add("geschoss2button", 0.25f);
            waffenbilder[7] = Content.Load<Texture2D>("Textures\\geschoss2button");

            Bilddateien.Add("baubutton", 0.25f);
            waffenbilder[8] = Content.Load<Texture2D>("Textures\\baubutton");

            Bilddateien.Add("baubutton2", 0.25f);
            waffenbilder[9] = Content.Load<Texture2D>("Textures\\baubutton2");

            Bilddateien.Add("MineRotButton", 0.25f);
            waffenbilder[10] = Content.Load<Texture2D>("Textures\\MineRotButton");

            Bilddateien.Add("MineGelbButton", 0.25f);
            waffenbilder[11] = Content.Load<Texture2D>("Textures\\MineGelbButton");

            Bilddateien.Add("MineGruenButton", 0.25f);
            waffenbilder[12] = Content.Load<Texture2D>("Textures\\MineGruenButton");

            Bilddateien.Add("MineBlauButton", 0.25f);
            waffenbilder[13] = Content.Load<Texture2D>("Textures\\MineBlauButton");

            Bilddateien.Add("BunkerButton", 0.25f);
            waffenbilder[14] = Content.Load<Texture2D>("Textures\\BunkerButton");

            Bilddateien.Add("TunnelButton", 0.25f);
            waffenbilder[15] = Content.Load<Texture2D>("Textures\\TunnelButton");

            Bilddateien.Add("GeschuetzButton", 0.25f);
            waffenbilder[16] = Content.Load<Texture2D>("Textures\\GeschuetzButton");

            Bilddateien.Add("Geschuetz2Button", 0.25f);
            waffenbilder[17] = Content.Load<Texture2D>("Textures\\Geschuetz2Button");

            Bilddateien.Add("mgbutton", 0.25f);
            waffenbilder[18] = Content.Load<Texture2D>("Textures\\mgbutton");

            Bilddateien.Add("Reparieren", 0.25f);
            waffenbilder[19] = Content.Load<Texture2D>("Textures\\Reparieren");

            Bilddateien.Add("Erobern", 0.25f);
            waffenbilder[20] = Content.Load<Texture2D>("Textures\\Erobern");

            panzerindex[0] = Content.Load<Texture2D>("Textures\\Artillerie2");
            panzerindex[1] = Content.Load<Texture2D>("Textures\\Panzer2");
            panzerindex[2] = Content.Load<Texture2D>("Textures\\Baufahrzeug2");
            panzerindex[3] = Content.Load<Texture2D>("Textures\\Scout2");
            panzerindex[4] = Content.Load<Texture2D>("Textures\\Geschuetz");
            panzerindex[5] = Content.Load<Texture2D>("Textures\\Geschuetz2");

            panzerruine[0] = Content.Load<Texture2D>("Textures\\ArtillerieRuine");
            panzerruine[0].Tag = "Textures\\ArtillerieRuine";

            panzerruine[1] = Content.Load<Texture2D>("Textures\\PanzerRuine");
            panzerruine[1].Tag = "Textures\\PanzerRuine";

            panzerruine[2] = Content.Load<Texture2D>("Textures\\BaufahrzeugRuine");
            panzerruine[2].Tag = "Textures\\BaufahrzeugRuine";

            panzerruine[3] = Content.Load<Texture2D>("Textures\\ScoutRuine");
            panzerruine[3].Tag = "Textures\\ScoutRuine";

            panzerruine[4] = Content.Load<Texture2D>("Textures\\Geschuetz");
            panzerruine[4].Tag = "Textures\\Geschuetz";

            panzerruine[5] = Content.Load<Texture2D>("Textures\\Geschuetz2");
            panzerruine[5].Tag = "Textures\\Geschuetz2";

            panzerindexreifen[0] = Content.Load<Texture2D>("Textures\\ScoutReifen");
            panzerindexreifen[1] = Content.Load<Texture2D>("Textures\\ScoutReifen");
            panzerindexreifen[2] = Content.Load<Texture2D>("Textures\\ScoutReifen");
            panzerindexreifen[3] = Content.Load<Texture2D>("Textures\\ScoutReifen");
            panzerindexreifen[4] = Content.Load<Texture2D>("Textures\\ScoutReifen");
            panzerindexreifen[5] = Content.Load<Texture2D>("Textures\\ScoutReifen");

            panzerrohrindex[0] = Content.Load<Texture2D>("Textures\\Artillerie2Rohr");
            panzerrohrindex[1] = Content.Load<Texture2D>("Textures\\Panzer2Rohr");
            panzerrohrindex[2] = Content.Load<Texture2D>("Textures\\nichts");
            panzerrohrindex[3] = Content.Load<Texture2D>("Textures\\Scout2Rohr");
            panzerrohrindex[4] = Content.Load<Texture2D>("Textures\\GeschuetzRohr");
            panzerrohrindex[5] = Content.Load<Texture2D>("Textures\\Geschuetz2Rohr");

            Bilddateien.Add("ArtillerieButton", 0.25f);
            panzerbutton[0] = Content.Load<Texture2D>("Textures\\ArtillerieButton");

            Bilddateien.Add("PanzerButton", 0.25f);
            panzerbutton[1] = Content.Load<Texture2D>("Textures\\PanzerButton");

            Bilddateien.Add("BaufahrzeugButton", 0.25f);
            panzerbutton[2] = Content.Load<Texture2D>("Textures\\BaufahrzeugButton");

            Bilddateien.Add("ScoutButton", 0.25f);
            panzerbutton[3] = Content.Load<Texture2D>("Textures\\ScoutButton");

            //Bilddateien.Add("GeschützButton", 0.25f); // nicht nötig
            panzerbutton[4] = Content.Load<Texture2D>("Textures\\GeschuetzButton");

            // Bilddateien.Add("Geschütz2Button", 0.25f); // nicht nötig
            panzerbutton[5] = Content.Load<Texture2D>("Textures\\Geschuetz2Button");

            Bilddateien.Add("Bunker1", 0.25f);
            bunker[0] = Content.Load<Texture2D>("Textures\\bunker1");

            Bilddateien.Add("Bunker2", 0.25f);
            bunker[1] = Content.Load<Texture2D>("Textures\\bunker2");

            Bilddateien.Add("Bunker4", 0.25f);
            bunker[2] = Content.Load<Texture2D>("Textures\\bunker4");

            Bilddateien.Add("Bunker1-2", 0.25f);
            bunker2[0] = Content.Load<Texture2D>("Textures\\bunker1-2");

            Bilddateien.Add("Bunker2-2", 0.25f);
            bunker2[1] = Content.Load<Texture2D>("Textures\\bunker2-2");

            Bilddateien.Add("Bunker4-2", 0.25f);
            bunker2[2] = Content.Load<Texture2D>("Textures\\bunker4-2");

            FahrzeugSkalierung();

            // Umrisse generieren
            for (int i = 0; i < panzerindex.Count(); i++)
                panzerumriss[i] = Umriss.Generieren(panzerindex[i], Color.White, 1);
            for (int i = 0; i < panzerindex.Count(); i++)
                panzerumriss2[i] = Umriss.Generieren(panzerindex[i], Color.Lime, 3);
            for (int i = 0; i < panzerindex.Count(); i++)
                panzerumrissreifen[i] = Umriss.Generieren(panzerindexreifen[i], Color.White, 1);
            for (int i = 0; i < panzerindex.Count(); i++)
                panzerumriss2reifen[i] = Umriss.Generieren(panzerindexreifen[i], Color.Lime, 3);
            for (int i = 0; i < panzerrohrindex.Count(); i++)
                panzerrohrumriss[i] = Umriss.Generieren(panzerrohrindex[i], Color.White, 1);
            for (int i = 0; i < panzerrohrindex.Count(); i++)
                panzerrohrumriss2[i] = Umriss.Generieren(panzerrohrindex[i], Color.Lime, 3);

            for (int i = 0; i < panzerindex.Count(); i++)
            {
                Fahrzeugdaten.Messpunkte[i] = Help.GetMesspunkte(panzerindex[i]);
            }

            #region Schriften

            AlgerianFont = Content.Load<SpriteFont>("Fonts\\AlgerianFont");
            font = Content.Load<SpriteFont>("Fonts\\myfont");
            font2 = Content.Load<SpriteFont>("Fonts\\a4");
            font3 = Content.Load<SpriteFont>("Fonts\\a2");
            font4 = Content.Load<SpriteFont>("Fonts\\a4");

            #endregion Schriften

            Bilddateien.Add("Effekte\\Heilen", 0.25f);
            Bilddateien.Add("Effekte\\Arbeitsbereich_I", 0.25f);
            Bilddateien.Add("Effekte\\Arbeitsbereich_II", 0.25f);
            Bilddateien.Add("Effekte\\Arbeitsbereich_III", 0.25f);
            Bilddateien.Add("Effekte\\Eingefroren", 0.25f);
            Bilddateien.Add("Effekte\\Elektrisiert", 0.25f);
            Bilddateien.Add("Effekte\\Lager_I", 0.25f);
            Bilddateien.Add("Effekte\\Lager_II", 0.25f);
            Bilddateien.Add("Effekte\\Lager_III", 0.25f);
            Bilddateien.Add("Effekte\\Schild_I", 0.25f);
            Bilddateien.Add("Effekte\\Schild_II", 0.25f);
            Bilddateien.Add("Effekte\\Schild_III", 0.25f);
            Bilddateien.Add("Effekte\\Verbrauch_I", 0.25f);
            Bilddateien.Add("Effekte\\Verbrauch_II", 0.25f);
            Bilddateien.Add("Effekte\\Verbrauch_III", 0.25f);
            Bilddateien.Add("Effekte\\Vergiftet", 0.25f);
            Bilddateien.Add("Effekte\\Zielen_I", 0.25f);
            Bilddateien.Add("Effekte\\Zielen_II", 0.25f);
            Bilddateien.Add("Effekte\\Zielen_III", 0.25f);
            Bilddateien.Add("Effekte\\Tarn", 0.25f);

            Bilddateien.Add("Mine0", 0.05f);
            missle[0] = Content.Load<Texture2D>("Textures\\missle");
            missle[1] = Content.Load<Texture2D>("Textures\\bigmissle");
            missle[2] = Content.Load<Texture2D>("Textures\\cryomissle");
            missle[3] = Content.Load<Texture2D>("Textures\\poisonrocket1");
            missle[4] = Content.Load<Texture2D>("Textures\\nuke");
            missle[5] = Content.Load<Texture2D>("Textures\\missle");
            missle[6] = Content.Load<Texture2D>("Textures\\geschoss");
            missle[7] = Content.Load<Texture2D>("Textures\\geschoss2");
            missle[8] = Content.Load<Texture2D>("Textures\\nichts");
            missle[9] = Content.Load<Texture2D>("Textures\\nichts");
            missle[10] = Content.Load<Texture2D>("Textures\\Mine1");
            missle[11] = Content.Load<Texture2D>("Textures\\Mine2");
            missle[12] = Content.Load<Texture2D>("Textures\\Mine3");
            missle[13] = Content.Load<Texture2D>("Textures\\Mine4");
            missle[14] = Content.Load<Texture2D>("Textures\\nichts");
            missle[15] = Content.Load<Texture2D>("Textures\\nichts");
            missle[16] = Content.Load<Texture2D>("Textures\\nichts");
            missle[17] = Content.Load<Texture2D>("Textures\\nichts");
            missle[18] = Content.Load<Texture2D>("Textures\\geschoss");
            missle[19] = Content.Load<Texture2D>("Textures\\nichts");
            missle[20] = Content.Load<Texture2D>("Textures\\nichts");

            smokeTexture = Content.Load<Texture2D>("Textures\\smokeTexture");
            explosion = Content.Load<Texture2D>("Textures\\explosion");

            /// Hier keine Release Skalierung
            kreuz = Content.Load<Texture2D>("Textures\\Kreuz");

            strich = Content.Load<Texture2D>("Textures\\strich");

            Bilddateien.Add("Wind", 0.5f);
            wind = Spiel.WIND.Wert ? Content.Load<Texture2D>("Textures\\wind") : null;

            leben = Content.Load<Texture2D>("Textures\\Leben");

            Bilddateien.Add("Fahne", 0.15f);
            fahne = Content.Load<Texture2D>("Textures\\Fahne");

            /// Hier keine Release Skalierung
            Bilddateien.Add("Tunnel", Tunnel.TUNNEL_SCALE.oldWert);
            tunnel = Content.Load<Texture2D>("Textures\\Tunnel");

            if (Haus.HAEUSER)
            {
                haus[0] = Content.Load<Texture2D>("Textures\\Haus");
                haus[0].Tag = "Textures\\Haus";

                haus[1] = Content.Load<Texture2D>("Textures\\Haus2");
                haus[1].Tag = "Textures\\Haus2";

                haus[2] = Content.Load<Texture2D>("Textures\\Haus3");
                haus[2].Tag = "Textures\\Haus3";

                haus[3] = Content.Load<Texture2D>("Textures\\Haus4");
                haus[3].Tag = "Textures\\Haus4";

                haus[4] = Content.Load<Texture2D>("Textures\\Haus5");
                haus[4].Tag = "Textures\\Haus5";

                haus[5] = Content.Load<Texture2D>("Textures\\Haus6");
                haus[5].Tag = "Textures\\Haus6";

                haus[6] = Content.Load<Texture2D>("Textures\\Haus7");
                haus[6].Tag = "Textures\\Haus7";

                haus[7] = Content.Load<Texture2D>("Textures\\Haus8");
                haus[7].Tag = "Textures\\Haus8";

                haus[8] = Content.Load<Texture2D>("Textures\\Haus9");
                haus[8].Tag = "Textures\\Haus9";

                haus[9] = Content.Load<Texture2D>("Textures\\Haus10");
                haus[9].Tag = "Textures\\Haus10";

                haus[10] = Content.Load<Texture2D>("Textures\\Haus11");
                haus[10].Tag = "Textures\\Haus11";

                haus[11] = Content.Load<Texture2D>("Textures\\Haus12");
                haus[11].Tag = "Textures\\Haus12";

                haus[12] = Content.Load<Texture2D>("Textures\\Haus13");
                haus[12].Tag = "Textures\\Haus13";

                haus[13] = Content.Load<Texture2D>("Textures\\Haus14");
                haus[13].Tag = "Textures\\Haus14";

                haus[14] = Content.Load<Texture2D>("Textures\\Haus15");
                haus[14].Tag = "Textures\\Haus15";

                haus[15] = Content.Load<Texture2D>("Textures\\Haus16");
                haus[15].Tag = "Textures\\Haus16";

                haus[16] = Content.Load<Texture2D>("Textures\\Haus17");
                haus[16].Tag = "Textures\\Haus17";

                haus[17] = Content.Load<Texture2D>("Textures\\Haus18");
                haus[17].Tag = "Textures\\Haus18";

                HaeuserSkalierung();

                for (int i = 0; i < haus.Count(); i++)
                {
                    hausumriss[i] = Umriss.Generieren(haus[i], Color.White, 2);
                }
                tunnelumriss = Umriss.Generieren(tunnel, Color.Lime, 2);
            }
            else
                for (int i = 0; i < haus.Count(); i++) haus[i] = null;

            if (Baum.BAEUME)
            {
                baum[0] = Content.Load<Texture2D>("Textures\\Baum");
                baum[0].Tag = "Textures\\Baum";

                baum[1] = Content.Load<Texture2D>("Textures\\Baum2");
                baum[1].Tag = "Textures\\Baum2";

                baum[2] = Content.Load<Texture2D>("Textures\\Baum3");
                baum[2].Tag = "Textures\\Baum3";

                baum[3] = Content.Load<Texture2D>("Textures\\Baum4");
                baum[3].Tag = "Textures\\Baum4";

                baum[4] = Content.Load<Texture2D>("Textures\\Baum5");
                baum[4].Tag = "Textures\\Baum5";

                baum[5] = Content.Load<Texture2D>("Textures\\Baum6");
                baum[5].Tag = "Textures\\Baum6";

                baum[6] = Content.Load<Texture2D>("Textures\\Baum7");
                baum[6].Tag = "Textures\\Baum7";

                baum[7] = Content.Load<Texture2D>("Textures\\Baum8");
                baum[7].Tag = "Textures\\Baum8";

                baum[8] = Content.Load<Texture2D>("Textures\\Baum9");
                baum[8].Tag = "Textures\\Baum9";

                baum[9] = Content.Load<Texture2D>("Textures\\a_tree04");
                baum[9].Tag = "Textures\\a_tree04";

                baum[10] = Content.Load<Texture2D>("Textures\\a_tree05");
                baum[10].Tag = "Textures\\a_tree05";

                baum[11] = Content.Load<Texture2D>("Textures\\a_tree06");
                baum[11].Tag = "Textures\\a_tree06";

                baum[12] = Content.Load<Texture2D>("Textures\\a_tree08");
                baum[12].Tag = "Textures\\a_tree08";

                baum[13] = Content.Load<Texture2D>("Textures\\a_tree12");
                baum[13].Tag = "Textures\\a_tree12";

                baum[14] = Content.Load<Texture2D>("Textures\\a_tree13");
                baum[14].Tag = "Textures\\a_tree13";

                baum[15] = Content.Load<Texture2D>("Textures\\a_tree22");
                baum[15].Tag = "Textures\\a_tree22";

                baum[16] = Content.Load<Texture2D>("Textures\\Palm_01");
                baum[16].Tag = "Textures\\Palm_01";

                baum[17] = Content.Load<Texture2D>("Textures\\Palm_02");
                baum[17].Tag = "Textures\\Palm_02";

                baum[18] = Content.Load<Texture2D>("Textures\\Palm_03");
                baum[18].Tag = "Textures\\Palm_03";

                baum[19] = Content.Load<Texture2D>("Textures\\Palm_04");
                baum[19].Tag = "Textures\\Palm_04";

                baum[20] = Content.Load<Texture2D>("Textures\\Palm_05");
                baum[20].Tag = "Textures\\Palm_05";

                baum[21] = Content.Load<Texture2D>("Textures\\Palm_06");
                baum[21].Tag = "Textures\\Palm_06";

                baum[22] = Content.Load<Texture2D>("Textures\\Palm_07");
                baum[22].Tag = "Textures\\Palm_07";

                baum[23] = Content.Load<Texture2D>("Textures\\Palm_08");
                baum[23].Tag = "Textures\\Palm_08";

                baum[24] = Content.Load<Texture2D>("Textures\\Palm_p01");
                baum[24].Tag = "Textures\\Palm_p01";

                baum[25] = Content.Load<Texture2D>("Textures\\Palm_p02");
                baum[25].Tag = "Textures\\Palm_p02";

                baum[24] = Content.Load<Texture2D>("Textures\\Palm_p03");
                baum[24].Tag = "Textures\\Palm_p03";

                baum[25] = Content.Load<Texture2D>("Textures\\Palm_p04");
                baum[25].Tag = "Textures\\Palm_p04";

                BaeumeSkalierung();
            }
            else
                for (int i = 0; i < baum.Count(); i++) baum[i] = null;

            ///Bilddateien.Add("geldbutton", 0.25f);
            ///Geld = Content.Load<Texture2D>("Textures\\geldbutton");

            //Bilddateien.Add("lvupbutton", 0.25f);
            ///Exp = Content.Load<Texture2D>("Textures\\lvupbutton");

            ///Bilddateien.Add("hpstatusbutton", 0.25f);
            ///HpStatus = Content.Load<Texture2D>("Textures\\hpstatusbutton");

            Bilddateien.Add("fuel", 0.25f);
            fuel = Content.Load<Texture2D>("Textures\\fuel");

            #endregion Loading Content

            #region Grounds

            wasser = FromFile("Content\\Textures\\Material\\wasser14.jpg");
            background1 = FromFile("Content\\Textures\\background2.jpg");
            activeBackground = background1;
            mausZeiger = Content.Load<Texture2D>("Textures\\mauszeiger");
            Textfeld.textbox = Content.Load<Texture2D>("Textures\\textbox");

            #endregion Grounds
        }

        // TODO da steht nichts drin
        /// <summary>
        ///     Materials the skalierung.
        /// </summary>
        public static void MaterialSkalierung()
        {
            /*for (int i = 0; i < haus.Count(); i++)
            {
                RenderTarget2D rt = new RenderTarget2D(Game1.device, (int)(haus[i].Width * Hausdata.scale[i]), (int)(haus[i].Height * Hausdata.scale[i]));
               
              Game1.SpriteBatchSemaphor.WaitOne(); 
              Game1.device.SetRenderTarget(rt);
                Rectangle rect = new Rectangle(0, 0, (int)(haus[i].Width * Hausdata.scale[i]), (int)(haus[i].Height * Hausdata.scale[i]));
                
                
                Game1.spriteBatch.Begin();
                Game1.device.Clear(Color.Transparent);
                Game1.spriteBatch.Draw(haus[i], rect, Color.White);
                
              Game1.SpriteBatchSemaphor.Release();
              
                Game1.device.SetRenderTarget(null);
              Game1.spriteBatch.End();
                haus[i] = rt;
                Hausdata.scale[i] = 1.0f;
            }*/
        }
    }
}