using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    public static class Editor
    {
        public static bool visible = false;
        public static bool Gitter = false;
        private static bool first = true;

        private static ComboBox2 Opt;

        public static Textbereich Textfelder;
        public static int Textfeldtyp = 0;
        public static int TextfeldSpieler = 0;
        public static int TextfeldID = 0;
        private static int chosenplayer = -1;

        // Objektmodus
        private static ComboBox2 NutzlosesCombo;

        private static ComboBox2 Häuser;
        private static ComboBox2 TunnelCombo;
        private static ComboBox2 Fahrzeuge;
        private static ComboBox2 Waffen;

        // Kartenmalmodus
        private static ComboBox2 Pinselform;

        private static ComboBox2 Pinselfarbe;
        private static ComboBox2 Pinseldicke;

        private static int Pinselform2 = 0;
        private static int Pinselfarbe2 = 1;
        private static int Pinseldicke2 = 0;

        private static int lastchosen = -1;
        private static int lastchosenid = -1;

        private static bool Zeichenmodus = false; // false = Karte, true = Objekte

        public static void show(int Fensterbreite)
        {
            visible = true;
            // Optionen.show(new Vector2(Fensterbreite - 100 -25, 0), 0);
            Opt.show();
            NutzlosesCombo.show();
            Häuser.show();
            TunnelCombo.show();
            Fahrzeuge.show();
            Waffen.show();
            Pinselform.show();
            Pinselfarbe.show();
            Pinseldicke.show();
        }

        public static void hide()
        {
            visible = false;
            // Optionen.hide();
            Opt.hide();
            NutzlosesCombo.hide();
            Häuser.hide();
            TunnelCombo.hide();
            Fahrzeuge.hide();
            Waffen.hide();
            Pinselform.hide();
            Pinselfarbe.hide();
            Pinseldicke.hide();
        }

        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster, int Fensterbreite, int Fensterhoehe, ContentManager Content, Spiel Spiel2)
        {
            #region Init

            if (first)
            {
                Pinselform = new ComboBox2("Pinselform", new String[] { "Rund", "Quadratisch" }, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 100 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                String[] data4 = new String[Karte.Material.Length];
                int i = 0;
                for (; i < Karte.Material.Length; i++)
                    data4[i] = Karte.MATERIAL_NAME.Wert[i];

                Pinselfarbe = new ComboBox2("Pinselfarbe", data4, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 200 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                String[] data5 = new String[15];
                i = 0;
                for (; i < data5.Length; i++)
                    data5[i] = ((i + 1) * 10).ToString();

                Pinseldicke = new ComboBox2("Pinseldicke", data5, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 300 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                //  List<String> list = new List<String>();
                // list.Add("Gitternetz aus");
                // Optionen = new Minimenu(list, Texturen.font4, graphicsDevice, 100, Color.SteelBlue, Color.Black, Color.Goldenrod);
                Opt = new ComboBox2("Optionen", new String[] { "Gitternetz aus" }, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                String[] data = new String[Texturen.baum.Length + Texturen.panzerruine.Length];
                i = 0;
                for (; i < Texturen.baum.Length; i++)
                    data[i] = "Baum" + i;

                for (int b = 0; b < Texturen.panzerruine.Length; b++, i++)
                    data[i] = "Ruine" + b;

                NutzlosesCombo = new ComboBox2("Nutzloses", data, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 100 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                String[] data2 = new String[Texturen.haus.Length];
                for (i = 0; i < Texturen.haus.Length; i++)
                    data2[i] = "Haus" + i;

                Häuser = new ComboBox2("Haeuser", data2, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 200 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                TunnelCombo = new ComboBox2("Tunnel", new String[] { "Tunnel" }, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 300 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                Fahrzeuge = new ComboBox2("Fahrzeuge", new String[] { "Artillerie", "Panzer", "Baufahrzeug", "Spaehfahrzeug", "Geschuetz I", "Geschuetz II" }, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 400 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                String[] data3 = new String[Texturen.waffenbilder.Length];
                for (i = 0; i < Texturen.waffenbilder.Length; i++)
                    data3[i] = "Waffe" + i;

                Waffen = new ComboBox2("Waffen", data3, 100, graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 500 - 25, 0), Color.Goldenrod, Color.Black, Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                // show(Fensterbreite);

                Textfelder = new Textbereich(Texturen.font, 500, 20, graphicsDevice, Content);
                //Textfelder.show();
                Textfelder.Text = Help.ZerhackeTextAufFesteBreite(Texturen.font, "", 500, true);
                Textfelder.originalText = "";

                first = false;
            }

            #endregion Init

            if (!visible) return;

            if (Zeichenmodus)
            {
                if (Gitter)
                {
                    Opt.Optionen.Inhalt[0] = "Gitternetz aus";
                }
                else
                    Opt.Optionen.Inhalt[0] = "Gitternetz an ";

                Opt.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
                NutzlosesCombo.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
                Häuser.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
                TunnelCombo.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
                Fahrzeuge.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
                Waffen.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
            }
            else if (!Zeichenmodus)
            {
                Pinselform.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
                Pinselfarbe.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
                Pinseldicke.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
            }

            #region Gitter

            if (Gitter)
            {
                Fenster.X = Fenster.X % Fensterbreite;
                Fenster.Y = Fenster.Y % Fensterhoehe;

                for (int i = (int)(-Fenster.X); i < Fensterbreite; i += 50)
                {
                    if (i < 0) continue;
                    Help.DrawLine(spriteBatch, new Vector2(i, 0), new Vector2(i, Fensterhoehe), Color.LightGoldenrodYellow * 0.15f, 2);
                }

                for (int i = (int)(-Fenster.Y); i < Fensterhoehe; i += 50)
                {
                    if (i < 0) continue;
                    Help.DrawLine(spriteBatch, new Vector2(0, i), new Vector2(Fensterbreite, i), Color.LightGoldenrodYellow * 0.15f, 2);
                }
            }

            #endregion Gitter

            // Spielerfarben malen
            float scale = Optimierung.Skalierung(0.25f);
            for (int i = 0; i <= Spiel2.players.Length; i++)
            {
                if (i - 1 == chosenplayer)
                {
                    Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(15 + i * Texturen.LeeresFeld.Width * scale), 15, (int)(Texturen.LeeresFeld.Width * scale), (int)(Texturen.LeeresFeld.Height * scale)), Color.Green, 0.7f);
                }

                spriteBatch.Draw(Texturen.LeeresFeld, new Vector2(15 + i * Texturen.LeeresFeld.Width * scale, 15), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
                Vector2 mitte = new Vector2(15 + i * Texturen.LeeresFeld.Width * scale + Texturen.LeeresFeld.Width * scale / 2, 15 + Texturen.LeeresFeld.Height * scale / 2);
                Color r = Color.SaddleBrown;
                if (i > 0) r = Spiel2.players[i - 1].Farbe;
                Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(mitte.X - 10), (int)(mitte.Y - 10), 20, 20), r, 0.55f);
            }

            // Zeichenmodusfelder
            if (Zeichenmodus)
                Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(15), (int)(Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale), (int)(Texturen.LeeresFeld.Width * scale), (int)(Texturen.LeeresFeld.Height * scale)), Color.Green, 0.7f);
            spriteBatch.Draw(Texturen.Objekte, new Vector2(15, Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);

            if (!Zeichenmodus)
                Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(15 + Texturen.LeeresFeld.Width * scale), (int)(Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale), (int)(Texturen.LeeresFeld.Width * scale), (int)(Texturen.LeeresFeld.Height * scale)), Color.Green, 0.7f);
            spriteBatch.Draw(Texturen.Karte, new Vector2(15 + Texturen.LeeresFeld.Width * scale, Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale), null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);

            // Textfeld
            Textfelder.Draw(spriteBatch, graphicsDevice, Vector2.Zero, false, new Vector2(-100 + 25, Texturen.LeeresFeld.Height * scale + 25 + 15), Color.LightGoldenrodYellow, Color.Black);

            // Welche sind Mouseover???
            int Bildbreite = 150;
            int Bildhoehe = 75;
            Vector2 temp2 = new Vector2(Fensterbreite - 100 - 25 - 500 - 25 - Bildbreite - 25, 0);
            Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)temp2.X - 1, (int)temp2.Y - 1, Bildbreite + 2, Bildhoehe + 2), Color.Black, 1.0f);
            Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)temp2.X, (int)temp2.Y, Bildbreite, Bildhoehe), Color.Goldenrod, 1.0f);

            if (Zeichenmodus)
            {
                if (Häuser.Optionen.over > -1 || lastchosen == 0)
                {
                    int me = Häuser.Optionen.over > -1 ? Häuser.Optionen.over : lastchosenid;

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.haus[me], Bildbreite, Bildhoehe);
                    spriteBatch.Draw(Texturen.haus[me], new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    lastchosen = 0;
                    lastchosenid = me;
                }

                if (TunnelCombo.Optionen.over > -1 || lastchosen == 1)
                {
                    int me = TunnelCombo.Optionen.over > -1 ? TunnelCombo.Optionen.over : lastchosenid;

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.tunnel, Bildbreite, Bildhoehe);
                    spriteBatch.Draw(Texturen.tunnel, new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    lastchosen = 1;
                    lastchosenid = me;
                }

                if (Fahrzeuge.Optionen.over > -1 || lastchosen == 2)
                {
                    int me = Fahrzeuge.Optionen.over > -1 ? Fahrzeuge.Optionen.over : lastchosenid;

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.panzerbutton[me], Bildbreite, Bildhoehe);
                    spriteBatch.Draw(Texturen.panzerbutton[me], new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);

                    lastchosen = 2;
                    lastchosenid = me;
                }

                if (NutzlosesCombo.Optionen.over > -1 || lastchosen == 3)
                {
                    int me = NutzlosesCombo.Optionen.over > -1 ? NutzlosesCombo.Optionen.over : lastchosenid;

                    if (me < Texturen.baum.Length)
                    {
                        int which = me;
                        Vector2 maße = Help.AufBereichVerkleinern(Texturen.baum[which], Bildbreite, Bildhoehe);
                        spriteBatch.Draw(Texturen.baum[which], new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    }
                    else
                        if (me < Texturen.baum.Length + Texturen.panzerruine.Length)
                        {
                            int which = me - Texturen.baum.Length;
                            Vector2 maße = Help.AufBereichVerkleinern(Texturen.panzerruine[which], Bildbreite, Bildhoehe);
                            spriteBatch.Draw(Texturen.panzerruine[which], new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                        }

                    lastchosen = 3;
                    lastchosenid = me;
                }

                if ((Waffen.Optionen.over > -1 && Waffendaten.Verschiessbar[Waffen.Optionen.over] > 0 && Waffendaten.Verschiessbar[Waffen.Optionen.over] != 5) || (lastchosen == 4 && Waffendaten.Verschiessbar[lastchosenid] > 0 && Waffendaten.Verschiessbar[lastchosenid] != 5))
                {
                    int me = Waffen.Optionen.over > -1 ? Waffen.Optionen.over : lastchosenid;

                    Vector2 maße2 = Help.AufBereichVerkleinern(Texturen.waffenbilder[me], 75, 50);
                    spriteBatch.Draw(Texturen.waffenbilder[me], new Rectangle((int)(temp2.X), (int)(temp2.Y), (int)maße2.X, (int)maße2.Y), Color.White);

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.missle[me], Bildbreite, Bildhoehe);
                    spriteBatch.Draw(Texturen.missle[me], new Rectangle((int)(temp2.X + Bildbreite / 2 + (Bildbreite / 2 - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    lastchosen = 4;
                    lastchosenid = me;
                }
            }
            else
                if (!Zeichenmodus)
                {
                    // Vector2 maße = Help.AufBereichVerkleinern(Texturen.panzerbutton[me], Bildbreite, Bildhoehe);
                    //spriteBatch.Draw(Texturen.panzerbutton[me], new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    Vector2 pos = new Vector2((int)(temp2.X + Bildbreite / 2 - Texturen.font.MeasureString(Pinseldicke2.ToString()).X / 2), (int)(temp2.Y + Bildhoehe - Texturen.font.MeasureString(Pinseldicke2.ToString()).Y));
                    spriteBatch.DrawString(Texturen.font, ((Pinseldicke2 + 1) * 10).ToString(), pos, Color.Black);
                    if (Pinselform2 == 0)
                    {
                        spriteBatch.Draw(Texturen.kreis, new Rectangle((int)(temp2.X + Bildbreite - 25), (int)(temp2.Y + Bildhoehe - 25), 20, 20), Color.Black);
                    }
                    else
                        if (Pinselform2 == 1)
                        {
                            Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(temp2.X + Bildbreite - 25), (int)(temp2.Y + Bildhoehe - 25), 20, 20), Color.Black, 1.0f);
                        }

                    if (Pinselfarbe.Optionen.over == -1)
                    {
                        if (Karte.Material[Pinselfarbe2].Farbe)
                        {
                            Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(temp2.X + Bildbreite / 2 - 25), (int)(temp2.Y), 50, 50), Karte.Material[Pinselfarbe2].CFarbe, 1.0f);
                        }
                        else
                        {
                            Vector2 maße = Help.AufBereichVerkleinern(Karte.Material[Pinselfarbe2].Bild, 50, 50);
                            spriteBatch.Draw(Karte.Material[Pinselfarbe2].Bild, new Rectangle((int)(temp2.X + Bildbreite / 2 - 25 + (50 - maße.X) / 2), (int)(temp2.Y), (int)maße.X, (int)maße.Y), Color.White);
                        }
                    }
                    else
                        if (Pinselfarbe.Optionen.over > -1)
                        {
                            if (Karte.Material[Pinselfarbe.Optionen.over].Farbe)
                            {
                                Help.DrawRectangle(spriteBatch, graphicsDevice, new Rectangle((int)(temp2.X + Bildbreite / 2 - 25), (int)(temp2.Y), 50, 50), Karte.Material[Pinselfarbe.Optionen.over].CFarbe, 1.0f);
                            }
                            else
                            {
                                Vector2 maße = Help.AufBereichVerkleinern(Karte.Material[Pinselfarbe.Optionen.over].Bild, 50, 50);
                                spriteBatch.Draw(Karte.Material[Pinselfarbe.Optionen.over].Bild, new Rectangle((int)(temp2.X + Bildbreite / 2 - 25 + (50 - maße.X) / 2), (int)(temp2.Y), (int)maße.X, (int)maße.Y), Color.White);
                            }
                        }
                }

            return;
        }

        private static bool geklickt = false;

        public static int mouseover = -1;
        public static int mouseoverid = -1;
        public static int mouseoverid2 = -1;

        public static void MouseKeys2(Spiel Spiel2)
        {
            bool found = false;
            //   if (chosenplayer < 0) { found = true; mouseover = -1; }

            if (!found)
            {
                for (int i = 0; i < Spiel2.players.Length; i++)
                {
                    for (int b = 0; b < Spiel2.players[i].Minen.Count; b++)
                        if (Spiel2.players[i].Minen[b].PrüfeObKollision(Help.GetMousePos() + Spiel2.Fenster))
                        {
                            mouseover = 6;
                            mouseoverid = i;
                            mouseoverid2 = b;
                            found = true;
                            break;
                        }
                }
            }

            if (!found)
            {
                for (int i = 0; i < Spiel2.Kisten.aktiv.Count; i++)
                {
                    if (Spiel2.Kisten.IsCollision(i, Help.GetMousePos() + Spiel2.Fenster))
                    {
                        mouseover = 5;
                        mouseoverid = i;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int i = 0; i < Spiel2.Bunker.Position.Count; i++)
                {
                    if (Spiel2.Bunker.PrüfeObKollision(i, Help.GetMousePos() + Spiel2.Fenster))
                    {
                        mouseover = 4;
                        mouseoverid = i;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int i = 0; i < Spiel2.players.Length; i++)
                {
                    for (int b = 0; b < Spiel2.players[i].pos.Count; b++)
                        if (Spiel2.players[i].PrüfeObKollision(b, Help.GetMousePos() + Spiel2.Fenster))
                        {
                            mouseover = 3;
                            mouseoverid = i;
                            mouseoverid2 = b;
                            found = true;
                            break;
                        }
                }
            }

            if (!found)
            {
                for (int i = 0; i < Nutzloses.GibAnzahl(); i++)
                {
                    if (Nutzloses.PrüfeObKollision(i, Help.GetMousePos() + Spiel2.Fenster))
                    {
                        mouseover = 2;
                        mouseoverid = i;
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                for (int i = 0; i < Spiel2.players.Length; i++)
                {
                    for (int b = 0; b < Spiel2.players[i].TunnelAnlage.Count; b++)
                    {
                        if (Spiel2.players[i].TunnelAnlage[b].PrüfeObKollision(Help.GetMousePos() + Spiel2.Fenster))
                        {
                            mouseover = 1;
                            mouseoverid = i;
                            mouseoverid2 = b;
                            found = true;
                            break;
                        }
                    }
                }
            }

            if (!found)
            {
                for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
                {
                    if (Spiel2.Haeuser.IsCollision2(i, Help.GetMousePos() + Spiel2.Fenster))
                    {
                        mouseover = 0;
                        mouseoverid = i;
                        found = true;
                        break;
                    }
                }
            }

            if (found == false) { mouseover = -1; }
        }

        public static void MouseKeys(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, MouseState oldmouseState, Spiel Spiel2, int Fensterbreite)
        {
            if (!visible) return;
            if (first) return;

            if (Editor.visible && Spiel2 != null)
            {
                if (Help.GetMouseState().LeftButton != oldmouseState.LeftButton && Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && Editor.mouseover > -1)
                {
                    int player = Spiel2.CurrentPlayer;
                    List<String> temp = null;

                    if (Editor.mouseover == 6)
                    {
                        temp = Spiel2.players[player].Minen[Editor.mouseoverid2].EditorSpeichern();
                        Editor.TextfeldID = Editor.mouseoverid2;
                    }
                    else
                        if (Editor.mouseover == 5)
                        {
                            temp = Spiel2.Kisten.EditorSpeichern(Editor.mouseoverid);
                            Editor.TextfeldID = Editor.mouseoverid;
                        }
                        else
                            if (Editor.mouseover == 4)
                            {
                                temp = Spiel2.Bunker.EditorSpeichern(Editor.mouseoverid);
                                Editor.TextfeldID = Editor.mouseoverid;
                            }
                            else
                                if (Editor.mouseover == 3)
                                {
                                    temp = Spiel2.players[player].EditorSpeichern(Editor.mouseoverid2);
                                    Editor.TextfeldID = Editor.mouseoverid2;
                                }
                                else
                                    if (Editor.mouseover == 2)
                                    {
                                        temp = Nutzloses.EditorSpeichern(Editor.mouseoverid);
                                        Editor.TextfeldID = Editor.mouseoverid;
                                    }
                                    else
                                        if (Editor.mouseover == 1)
                                        {
                                            temp = Spiel2.players[player].TunnelAnlage[Editor.mouseoverid2].EditorSpeichern();
                                            Editor.TextfeldID = Editor.mouseoverid2;
                                        }
                                        else
                                            if (Editor.mouseover == 0)
                                            {
                                                temp = Spiel2.Haeuser.EditorSpeichern(Editor.mouseoverid);
                                                Editor.TextfeldID = Editor.mouseoverid;
                                            }

                    String res = "";
                    for (int c = 0; c < temp.Count; c++)
                    {
                        res = res + temp[c] + (c < temp.Count - 1 ? "\n" : "");
                    }

                    Editor.Textfeldtyp = Editor.mouseover;
                    Editor.TextfeldSpieler = player;
                    Editor.Textfelder.originalText = res;
                    Editor.Textfelder.Text = Help.ZerhackeTextAufFesteBreite(Editor.Textfelder.font, Editor.Textfelder.originalText, Editor.Textfelder.maxPixelInZeile, true);
                    Editor.Textfelder.show();
                }
            }

            if (Zeichenmodus)
            {
                if (Opt.Optionen.sichtbar)
                    switch (Opt.Optionen.Interact(Vector2.Zero, false, oldmouseState))
                    {
                        case 0:
                            {
                                if (Gitter)
                                {
                                    Gitter = false;
                                }
                                else
                                    Gitter = true;

                                //return;
                                break;
                            }
                        case 1:
                            {
                                break;
                                // return;
                            }
                        default: break;
                    }

                Opt.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);

                NutzlosesCombo.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                Häuser.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                TunnelCombo.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                Fahrzeuge.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                Waffen.Optionen.Interact(Vector2.Zero, false, oldmouseState);

                NutzlosesCombo.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                Häuser.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                TunnelCombo.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                Fahrzeuge.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                Waffen.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);

                if (Opt.Titel.over == -1 && NutzlosesCombo.Titel.over == -1 && Häuser.Titel.over == -1 && TunnelCombo.Titel.over == -1 && Fahrzeuge.Titel.over == -1 && Waffen.Titel.over == -1 && Opt.Optionen.over == -1 && NutzlosesCombo.Optionen.over == -1 && Häuser.Optionen.over == -1 && TunnelCombo.Optionen.over == -1 && Fahrzeuge.Optionen.over == -1 && Waffen.Optionen.over == -1)
                    MouseKeys2(Spiel2);
            }
            else
                if (!Zeichenmodus)
                {
                    mouseover = -1;

                    if (Pinselform.Optionen.sichtbar)
                    {
                        int q = Pinselform.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                        if (q != -1) Pinselform2 = q;
                    }

                    if (Pinseldicke.Optionen.sichtbar)
                    {
                        int q = Pinseldicke.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                        if (q != -1) Pinseldicke2 = q;
                    }

                    if (Pinselfarbe.Optionen.sichtbar)
                    {
                        int q = Pinselfarbe.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                        if (q != -1) Pinselfarbe2 = q;
                    }

                    Pinselfarbe.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                    Pinselform.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                    Pinseldicke.Optionen.Interact(Vector2.Zero, false, oldmouseState);

                    Pinselfarbe.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                    Pinselform.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                    Pinseldicke.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                }

            if (Textfelder.MouseKeys(Vector2.Zero)) return;

            if (!Textfelder.visible && Help.GetMouseState().LeftButton != oldmouseState.LeftButton && Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                float scale = Optimierung.Skalierung(0.25f);
                for (int i = 0; i <= Spiel2.players.Length; i++)
                {
                    Vector2 temp = new Vector2(15 + i * Texturen.LeeresFeld.Width * scale, 15);
                    Vector2 maus = Help.GetMousePos();
                    if (maus.X >= temp.X && maus.Y >= temp.Y && maus.X <= temp.X + Texturen.LeeresFeld.Width * scale && maus.Y <= temp.Y + Texturen.LeeresFeld.Height * scale)
                    {
                        chosenplayer = i - 1;
                        if (i > 0) Hauptfenster.Tausch.CurrentPlayer = i - 1;

                        geklickt = true;
                        return;
                    }
                }

                Vector2 temp2 = new Vector2(15, Game1.screenHeight - 15 - Texturen.LeeresFeld.Height * scale);
                Vector2 maus2 = Help.GetMousePos();
                if (maus2.X >= temp2.X && maus2.Y >= temp2.Y && maus2.X <= temp2.X + Texturen.LeeresFeld.Width * scale && maus2.Y <= temp2.Y + Texturen.LeeresFeld.Height * scale)
                {
                    Zeichenmodus = true;
                    lastchosen = -1;
                    lastchosenid = -1;
                    geklickt = true;
                    return;
                }

                temp2 = new Vector2(15 + Texturen.LeeresFeld.Width * scale, Game1.screenHeight - 15 - Texturen.LeeresFeld.Height * scale);
                maus2 = Help.GetMousePos();
                if (maus2.X >= temp2.X && maus2.Y >= temp2.Y && maus2.X <= temp2.X + Texturen.LeeresFeld.Width * scale && maus2.Y <= temp2.Y + Texturen.LeeresFeld.Height * scale)
                {
                    Zeichenmodus = false;
                    lastchosen = -1;
                    lastchosenid = -1;
                    geklickt = true;
                    return;
                }
            }

            if (geklickt && Help.GetMouseState().LeftButton != oldmouseState.LeftButton && Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                geklickt = false;
            }

            if (!geklickt && Zeichenmodus && Opt.Titel.over == -1 && NutzlosesCombo.Titel.over == -1 && Häuser.Titel.over == -1 && TunnelCombo.Titel.over == -1 && Fahrzeuge.Titel.over == -1 && Waffen.Titel.over == -1 && Opt.Optionen.over == -1 && NutzlosesCombo.Optionen.over == -1 && Häuser.Optionen.over == -1 && TunnelCombo.Optionen.over == -1 && Fahrzeuge.Optionen.over == -1 && Waffen.Optionen.over == -1)
            {
                if (!Textfelder.visible && Help.GetMouseState().LeftButton != oldmouseState.LeftButton && Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    Vector2 Pos = Help.GetMousePos() + Spiel2.Fenster;
                    if (lastchosen == 0)
                    {
                        Vector2 Pos2 = Pos - new Vector2(Texturen.haus[lastchosenid].Width / 2 * Gebäudedaten.SKALIERUNG.Wert[lastchosenid], 0);
                        Spiel2.Haeuser.Add(new Vector2(Pos2.X, Kartenformat.BottomOf(Pos2)), -9999, lastchosenid, chosenplayer);
                    }
                    else
                        if (lastchosen == 1)
                        {
                            Vector2 Pos2 = Pos - new Vector2(Texturen.tunnel.Width / 2 * Tunnel.TUNNEL_SCALE.Wert);
                            if (Spiel2.PrüfeTunnelbau(Pos))
                                Spiel2.AddTunnel(chosenplayer, new Vector2(Pos2.X, Kartenformat.BottomOf(Pos2)));
                        }
                        else
                            if (lastchosen == 2)
                            {
                                Spiel2.AddPanzer(chosenplayer, lastchosenid, 0, false, new Vector2(Pos.X, Kartenformat.BottomOf(Pos)));
                            }
                            else
                                if (lastchosen == 3)
                                {
                                    if (lastchosenid < Texturen.baum.Length)
                                    {
                                        int which = lastchosenid;
                                        Nutzloses.Hinzufügen(Texturen.baum[which], new Vector2(Pos.X, Kartenformat.BottomOf(Pos) + 7), 0, false, Baumdata.SKALIERUNG.Wert[which], Baum.BAEUME_KOLLISION, Baum.BAEUME_ZERSTOERUNG);
                                    }
                                    else
                                        if (lastchosenid < Texturen.baum.Length + Texturen.panzerruine.Length)
                                        {
                                            int which = lastchosenid - Texturen.baum.Length;

                                            Nutzloses.Hinzufügen(Texturen.panzerruine[which], new Vector2(Pos.X, Kartenformat.BottomOf(Pos)), 0, false, Fahrzeugdaten.SCALEP.Wert[which], true, true);
                                        }
                                }
                                else
                                    if (lastchosen == 4)
                                    {
                                        if (Waffendaten.Verschiessbar[lastchosenid] > 0 && Waffendaten.Verschiessbar[lastchosenid] != 5)
                                            if (chosenplayer >= 0)
                                            {
                                                if (Waffendaten.Verschiessbar[lastchosenid] == 4)
                                                {
                                                    Spiel2.AddRakete(chosenplayer, Pos, Vector2.Zero, 300 * 4, lastchosenid, -1);
                                                }
                                                else
                                                    if (Waffendaten.Verschiessbar[lastchosenid] == 2)
                                                    {
                                                        Spiel2.Airstrike(Pos, chosenplayer);
                                                    }
                                                    else
                                                        if (Waffendaten.Verschiessbar[lastchosenid] == 3)
                                                        {
                                                            Vector2 Pos2 = Pos - new Vector2(Texturen.tunnel.Width / 2 * Tunnel.TUNNEL_SCALE.Wert);

                                                            Spiel2.players[chosenplayer].Minen.Add(new Mine((int)Pos2.X, (int)Kartenformat.BottomOf(Pos2), (int)(Waffendaten.Daten2[lastchosenid].Z), lastchosenid, Spiel2.players[chosenplayer].Minen.Count));
                                                        }
                                                        else
                                                            Spiel2.AddRakete(chosenplayer, Pos, Vector2.Zero, 300 * 4, lastchosenid, -1);
                                            }
                                    }
                }
            }
            else
                if (!Zeichenmodus && !geklickt && Pinselform.Optionen.over == -1 && Pinselform.Titel.over == -1 && Pinselfarbe.Optionen.over == -1 && Pinselfarbe.Titel.over == -1 && Pinseldicke.Optionen.over == -1 && Pinseldicke.Titel.over == -1)
                {
                    if (Help.GetMouseState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        Vector2 Pos = Help.GetMousePos() + Spiel2.Fenster;
                        Pos = new Vector2((int)Pos.X, (int)Pos.Y);
                        List<Vector3> list = new List<Vector3>();
                        int dicke = (Pinseldicke2 + 1) * 10;

                        if (Pinselform2 == 1)
                        {
                            for (int i = -dicke; i < dicke; i++)
                            {
                                list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Pos.Y - dicke), (int)(Pos.Y + dicke), Pinselfarbe2));
                            }
                        }
                        else
                            if (Pinselform2 == 0)
                            {
                                int width = dicke;
                                int aa = (int)((double)Math.Log((((width) - 0) * Math.PI), Math.E) * Math.Sqrt(width));
                                for (int i = -aa; i < aa; i++)
                                {
                                    if (i + Pos.X < 0 || i + Pos.X >= Spiel2.Spielfeld.Length) continue;

                                    int dist = i; if (dist < 0) dist = -dist;
                                    int add = (int)((double)Math.Log(((float)(aa - dist) * Math.PI), Math.E) * Math.Sqrt(width));
                                    if (add < 0) add = 0;
                                    if (add > aa) add = aa;

                                    int add2 = (int)(Pos.Y + add);
                                    if (add2 > Game1.screenHeight) add2 = Game1.screenHeight;
                                    list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Pos.Y - add), (int)(add2), Pinselfarbe2));
                                    //list.Add(new Vector3((int)(Pos.X + i), (int)(Pos.Y - add), (int)(add2)));
                                }
                            }

                        Vordergrund.AktualisiereVordergrund(list);
                    }
                    else
                        if (Help.GetMouseState().RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                        {
                            Vector2 Pos = Help.GetMousePos() + Spiel2.Fenster;
                            List<Vector3> list = new List<Vector3>();
                            int dicke = (Pinseldicke2 + 1) * 10;

                            if (Pinselform2 == 1)
                            {
                                for (int i = -dicke; i < dicke; i++)
                                {
                                    list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Pos.Y - dicke), (int)(Pos.Y + dicke), Karte.LUFT));
                                }
                            }
                            else
                                if (Pinselform2 == 0)
                                {
                                    int width = dicke;
                                    int aa = (int)((double)Math.Log((((width) - 0) * Math.PI), Math.E) * Math.Sqrt(width));
                                    for (int i = -aa; i < aa; i++)
                                    {
                                        if (i + Pos.X < 0 || i + Pos.X >= Spiel2.Spielfeld.Length) continue;

                                        int dist = i; if (dist < 0) dist = -dist;
                                        int add = (int)((double)Math.Log(((float)(aa - dist) * Math.PI), Math.E) * Math.Sqrt(width));
                                        if (add < 0) add = 0;
                                        if (add > aa) add = aa;

                                        int add2 = (int)(Pos.Y + add);
                                        if (add2 > Game1.screenHeight) add2 = Game1.screenHeight;
                                        list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Pos.Y - add), (int)(add2), Karte.LUFT));
                                        //    list.Add(new Vector3((int)(Pos.X + i), (int)(Pos.Y - add), (int)(add2)));
                                    }
                                }

                            Vordergrund.AktualisiereVordergrund(list);
                        }
                }
        }
    }
}