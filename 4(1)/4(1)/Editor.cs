using System;
using System.Collections.Generic;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     Diese Klasse verwaltet den Editor
    /// </summary>
    public static class Editor
    {
        #region Fields

        /// <summary>
        ///     regelt, ob ein Gitternetz auf dem Bildschirm angezeigt werden soll (true = ja, false = nein)
        /// </summary>
        public static bool Gitter = false;

        public static int mouseover = -1;

        public static int mouseoverid = -1;

        public static int mouseoverid2 = -1;

        /// <summary>
        ///     das Eingabefenster zum Ändern der Eigenschaften ausgwählter Objekte
        /// </summary>
        public static Textbereich Textfelder;

        /// <summary>
        ///     das Objekt, welches im Textfeld dargestellt wird, hat eine exakte ID (was auch immer es ist)
        /// </summary>
        public static int TextfeldID = 0;

        /// <summary>
        ///     das Objekt, welches im Textfeld dargestellt wird, gehört einem bestimmten Spieler
        /// </summary>
        public static int TextfeldSpieler = 0;

        /// <summary>
        ///     das Objekt, welches im Textfeld dargestellt wird, hat einen bestimmten Typ/Sorte
        /// </summary>
        public static int Textfeldtyp = 0;

        /// <summary>
        ///     ob der Editor sichtbar ist, bzw ob wir uns im Editor befinden
        /// </summary>
        public static bool visible = false;

        /// <summary>
        ///     ob es der erste Zugirff auf die Editorklasse ist
        /// </summary>
        private static bool first = true;

        /// <summary>
        ///     legt fest, ob etwas angeklickt wurde
        /// </summary>
        private static bool geklickt;

        /// <summary>
        ///     der gewählte Spieler
        /// </summary>
        private static int GewaehlterSpieler = -1;

        /// <summary>
        ///     bestimmt, welche der Auswahllisten, ausgwählt wurde
        /// </summary>
        private static int lastchosen = -1;

        /// <summary>
        ///     bestimmt, welches Item der Auswahllisten gewählt wurde
        /// </summary>
        private static int lastchosenid = -1;

        /// <summary>
        ///     Zur Auswahl verschiedener Optionen (beispielsweise das Anzeigen des Gitternetzes)
        /// </summary>
        private static ComboBox2 Optionen;

        #endregion Fields

        #region Objektmodus

        /// <summary>
        ///     die Auswahlliste für Fahrzeuge
        /// </summary>
        private static ComboBox2 Fahrzeuge;

        /// <summary>
        ///     die Auswahlliste für Gebäude/Häuser
        /// </summary>
        private static ComboBox2 Häuser;

        /// <summary>
        ///     die Auswahlliste für nutzlose Objekte
        /// </summary>
        private static ComboBox2 NutzlosesCombo;

        /// <summary>
        ///     die Auswahlliste für Tunnel
        /// </summary>
        private static ComboBox2 TunnelCombo;

        /// <summary>
        ///     die Auswahlliste für Fahrzeuge
        /// </summary>
        private static ComboBox2 Waffen;

        #endregion Objektmodus

        #region Kartenmalmodus

        /// <summary>
        ///     die gewählte Pinseldicke
        /// </summary>
        private static int GewaehltePinseldicke;

        /// <summary>
        ///     die gewählte Pinselfarbe / Materialsorte
        /// </summary>
        private static int GewaehltePinselfarbe = 1;

        /// <summary>
        ///     die gewählte Pinselform
        /// </summary>
        private static int GewaehltePinselform;

        /// <summary>
        ///     die Auswahlliste für die Pinseldicke
        /// </summary>
        private static ComboBox2 Pinseldicke;

        /// <summary>
        ///     die Auswahlliste für die Pinselfarbe / Materialsorte
        /// </summary>
        private static ComboBox2 Pinselfarbe;

        /// <summary>
        ///     die Auswahlliste für die Pinselform
        /// </summary>
        private static ComboBox2 Pinselform;

        #endregion Kartenmalmodus

        /// <summary>
        ///     lässt den Editor zwischen Zeichenmodus und Objektmodus wechseln
        ///     false = Zeichenmodus, true = Objektmodus
        /// </summary>
        private static bool Zeichenmodus;

        /// <summary>
        ///     zeichnet den Editor auf dem Bidlschirm
        /// </summary>
        public static void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Vector2 Fenster,
            int Fensterbreite, int Fensterhoehe, ContentManager Content, Spiel Spiel2)
        {
            #region Init

            if (first)
            {
                Pinselform = new ComboBox2("Pinselform", new[] { "Rund", "Quadratisch" }, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25 - 100 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

                var data4 = new String[Karte.Material.Length];
                int i = 0;
                for (; i < Karte.Material.Length; i++)
                    data4[i] = Karte.MATERIAL_NAME.Wert[i];

                Pinselfarbe = new ComboBox2("Pinselfarbe", data4, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25 - 200 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

                var data5 = new String[15];
                i = 0;
                for (; i < data5.Length; i++)
                    data5[i] = ((i + 1) * 10).ToString();

                Pinseldicke = new ComboBox2("Pinseldicke", data5, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25 - 300 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

                //  List<String> list = new List<String>();
                // list.Add("Gitternetz aus");
                // Optionen = new Minimenu(list, Texturen.font4, graphicsDevice, 100, Color.SteelBlue, Color.Black, Color.Goldenrod);
                Optionen = new ComboBox2("Optionen", new[] { "Gitternetz aus" }, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

                var data = new String[Texturen.baum.Length + Texturen.panzerruine.Length];
                i = 0;
                for (; i < Texturen.baum.Length; i++)
                    data[i] = "Baum" + i;

                for (int b = 0; b < Texturen.panzerruine.Length; b++, i++)
                    data[i] = "Ruine" + b;

                NutzlosesCombo = new ComboBox2("Nutzloses", data, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25 - 100 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

                var data2 = new String[Texturen.haus.Length];
                for (i = 0; i < Texturen.haus.Length; i++)
                    data2[i] = "Haus" + i;

                Häuser = new ComboBox2("Haeuser", data2, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25 - 200 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

                TunnelCombo = new ComboBox2("Tunnel", new[] { "Tunnel" }, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25 - 300 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

                Fahrzeuge = new ComboBox2("Fahrzeuge",
                    new[] { "Artillerie", "Panzer", "Baufahrzeug", "Spaehfahrzeug", "Geschuetz I", "Geschuetz II" }, 100,
                    graphicsDevice, new Vector2(Fensterbreite - 100 - 25 - 400 - 25, 25), Color.Goldenrod, Color.Black,
                    Color.Red, Color.LightGoldenrodYellow, Color.Black, Color.Red);

                var data3 = new String[Texturen.waffenbilder.Length];
                for (i = 0; i < Texturen.waffenbilder.Length; i++)
                    data3[i] = "Waffe" + i;

                Waffen = new ComboBox2("Waffen", data3, 100, graphicsDevice,
                    new Vector2(Fensterbreite - 100 - 25 - 500 - 25, 25), Color.Goldenrod, Color.Black, Color.Red,
                    Color.LightGoldenrodYellow, Color.Black, Color.Red);

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
                    Optionen.Optionen.Inhalt[0] = "Gitternetz aus";
                }
                else
                    Optionen.Optionen.Inhalt[0] = "Gitternetz an ";

                Optionen.Draw(spriteBatch, graphicsDevice, Vector2.Zero);
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
                int X = (int)Fenster.X ;
                int Y = (int)Fenster.Y ;

                for (var i = (int)(X); i < X + Fensterbreite; i += 50)
                {
                    if (i < 0 || i > Game1.Kartengroesse) continue;
                    int b = (int)(Game1.Kartenhoehe - Fenster.Y);
                    Help.DrawLine(spriteBatch, new Vector2(i-X, -Fenster.Y), new Vector2(i-X, b),Color.LightGoldenrodYellow * 0.15f, 2);
                }

                for (var i = (int)(Y); i<Y+Fensterhoehe; i += 50)
                {
                    if (i < 0 || i > Fensterhoehe || i > Game1.Kartenhoehe) continue;
                    int b = (int) (Game1.Kartengroesse - Fenster.X);
                    Help.DrawLine(spriteBatch, new Vector2(-Fenster.X,i-Y), new Vector2(b, i-Y),Color.LightGoldenrodYellow * 0.15f, 2);
                }
            }

            #endregion Gitter

            // Spielerfarben malen
            float scale = Optimierung.Skalierung(0.25f);
            for (int i = 0; i <= Spiel2.players.Length; i++)
            {
                if (i - 1 == GewaehlterSpieler)
                {
                    Help.DrawRectangle(spriteBatch, graphicsDevice,
                        new Rectangle((int)(15 + i * Texturen.LeeresFeld.Width * scale), 15,
                            (int)(Texturen.LeeresFeld.Width * scale), (int)(Texturen.LeeresFeld.Height * scale)),
                        Color.Green, 0.7f);
                }

                spriteBatch.Draw(Texturen.LeeresFeld, new Vector2(15 + i * Texturen.LeeresFeld.Width * scale, 15), null,
                    Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
                var mitte = new Vector2(15 + i * Texturen.LeeresFeld.Width * scale + Texturen.LeeresFeld.Width * scale / 2,
                    15 + Texturen.LeeresFeld.Height * scale / 2);
                Color r = Color.SaddleBrown;
                if (i > 0) r = Spiel2.players[i - 1].Farbe;
                Help.DrawRectangle(spriteBatch, graphicsDevice,
                    new Rectangle((int)(mitte.X - 10), (int)(mitte.Y - 10), 20, 20), r, 0.55f);
            }

            // Zeichenmodusfelder
            spriteBatch.Draw(Texturen.LeeresFeld, new Vector2(15, Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale),
null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
            spriteBatch.Draw(Texturen.Objekte, new Vector2(15, Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale),
                null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
            if (Zeichenmodus)
            {
                Help.DrawRectangle(spriteBatch, graphicsDevice,
                    new Rectangle(15, (int)(Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale),
                        (int)(Texturen.LeeresFeld.Width * scale), (int)(Texturen.LeeresFeld.Height * scale)), Color.Green,
                    0.7f);
            }

            spriteBatch.Draw(Texturen.LeeresFeld,
new Vector2(15 + Texturen.LeeresFeld.Width * scale, Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale),
null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
            spriteBatch.Draw(Texturen.Karte,
                new Vector2(15 + Texturen.LeeresFeld.Width * scale, Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale),
                null, Color.White, 0, new Vector2(0, 0), scale, SpriteEffects.None, 1);
            if (!Zeichenmodus)
            {
                Help.DrawRectangle(spriteBatch, graphicsDevice,
                    new Rectangle((int)(15 + Texturen.LeeresFeld.Width * scale),
                        (int)(Fensterhoehe - 15 - Texturen.LeeresFeld.Height * scale),
                        (int)(Texturen.LeeresFeld.Width * scale), (int)(Texturen.LeeresFeld.Height * scale)), Color.Green,
                    0.7f);
            }

            // Textfeld
            Textfelder.Draw(spriteBatch, graphicsDevice, Vector2.Zero, false,
                new Vector2(-100 + 25, Texturen.LeeresFeld.Height * scale + 25 + 15), Color.LightGoldenrodYellow,
                Color.Black);

            // Welche sind Mouseover???
            int Bildbreite = 150;
            int Bildhoehe = 75;
            var temp2 = new Vector2(Fensterbreite - 100 - 25 - 500 - 25 - Bildbreite - 25, 0);
            Help.DrawRectangle(spriteBatch, graphicsDevice,
                new Rectangle((int)temp2.X - 1, (int)temp2.Y - 1, Bildbreite + 2, Bildhoehe + 2), Color.Black, 1.0f);
            Help.DrawRectangle(spriteBatch, graphicsDevice,
                new Rectangle((int)temp2.X, (int)temp2.Y, Bildbreite, Bildhoehe), Color.Goldenrod, 1.0f);

            if (Zeichenmodus)
            {
                if (Häuser.Optionen.over > -1 || lastchosen == 0)
                {
                    int me = Häuser.Optionen.over > -1 ? Häuser.Optionen.over : lastchosenid;

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.haus[me], Bildbreite, Bildhoehe);
                    spriteBatch.Draw(Texturen.haus[me],
                        new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2),
                            (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    lastchosen = 0;
                    lastchosenid = me;
                }

                if (TunnelCombo.Optionen.over > -1 || lastchosen == 1)
                {
                    int me = TunnelCombo.Optionen.over > -1 ? TunnelCombo.Optionen.over : lastchosenid;

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.tunnel, Bildbreite, Bildhoehe);
                    spriteBatch.Draw(Texturen.tunnel,
                        new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2),
                            (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    lastchosen = 1;
                    lastchosenid = me;
                }

                if (Fahrzeuge.Optionen.over > -1 || lastchosen == 2)
                {
                    int me = Fahrzeuge.Optionen.over > -1 ? Fahrzeuge.Optionen.over : lastchosenid;

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.panzerbutton[me], Bildbreite, Bildhoehe);
                    spriteBatch.Draw(Texturen.panzerbutton[me],
                        new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2),
                            (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);

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
                        spriteBatch.Draw(Texturen.baum[which],
                            new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2),
                                (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    }
                    else if (me < Texturen.baum.Length + Texturen.panzerruine.Length)
                    {
                        int which = me - Texturen.baum.Length;
                        Vector2 maße = Help.AufBereichVerkleinern(Texturen.panzerruine[which], Bildbreite, Bildhoehe);
                        spriteBatch.Draw(Texturen.panzerruine[which],
                            new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2),
                                (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                    }

                    lastchosen = 3;
                    lastchosenid = me;
                }

                if ((Waffen.Optionen.over > -1 && Waffendaten.Verschiessbar[Waffen.Optionen.over] > 0 &&
                     Waffendaten.Verschiessbar[Waffen.Optionen.over] != 5) ||
                    (lastchosen == 4 && Waffendaten.Verschiessbar[lastchosenid] > 0 &&
                     Waffendaten.Verschiessbar[lastchosenid] != 5))
                {
                    int me = Waffen.Optionen.over > -1 ? Waffen.Optionen.over : lastchosenid;

                    Vector2 maße2 = Help.AufBereichVerkleinern(Texturen.waffenbilder[me], 75, 50);
                    spriteBatch.Draw(Texturen.waffenbilder[me],
                        new Rectangle((int)(temp2.X), (int)(temp2.Y), (int)maße2.X, (int)maße2.Y), Color.White);

                    Vector2 maße = Help.AufBereichVerkleinern(Texturen.missle[me], 75, Bildhoehe);
                    spriteBatch.Draw(Texturen.missle[me],
                        new Rectangle((int)(temp2.X + Bildbreite - 75 / 2 - maße.X / 2),
                            (int)(temp2.Y + Bildhoehe / 2 - maße.Y / 2), (int)maße.X, (int)maße.Y), Color.White);
                    lastchosen = 4;
                    lastchosenid = me;
                }
            }
            else if (!Zeichenmodus)
            {
                // Vector2 maße = Help.AufBereichVerkleinern(Texturen.panzerbutton[me], Bildbreite, Bildhoehe);
                //spriteBatch.Draw(Texturen.panzerbutton[me], new Rectangle((int)(temp2.X + (Bildbreite - maße.X) / 2), (int)(temp2.Y + (Bildhoehe - maße.Y) / 2), (int)maße.X, (int)maße.Y), Color.White);
                var pos =
                    new Vector2(
                        (int)
                            (temp2.X + Bildbreite / 2 - Texturen.font.MeasureString(GewaehltePinseldicke.ToString()).X / 2),
                        (int)(temp2.Y + Bildhoehe - Texturen.font.MeasureString(GewaehltePinseldicke.ToString()).Y));
                spriteBatch.DrawString(Texturen.font, ((GewaehltePinseldicke + 1) * 10).ToString(), pos, Color.Black);
                if (GewaehltePinselform == 0)
                {
                    Vector2 MausPosition = Help.GetMousePos();
                    int dicke = ((GewaehltePinseldicke + 1)*10);
                    spriteBatch.Draw(Texturen.kreis,
                        new Rectangle((int)(MausPosition.X - dicke), (int)(MausPosition.Y - dicke), dicke*2, dicke*2),
                        Color.Maroon*0.8f);

                    spriteBatch.Draw(Texturen.kreis,
                        new Rectangle((int)(temp2.X + Bildbreite - 25), (int)(temp2.Y + Bildhoehe - 25), 20, 20),
                        Color.Black);
                }
                else if (GewaehltePinselform == 1)
                {
                    Vector2 MausPosition = Help.GetMousePos();
                    int dicke = ((GewaehltePinseldicke + 1) * 10);

                    Help.DrawRectangle(spriteBatch, graphicsDevice,
                      new Rectangle((int)(MausPosition.X - dicke), (int)(MausPosition.Y - dicke), dicke * 2, dicke * 2),
                      Color.Maroon, 0.8f);

                    Help.DrawRectangle(spriteBatch, graphicsDevice,
                        new Rectangle((int)(temp2.X + Bildbreite - 25), (int)(temp2.Y + Bildhoehe - 25), 20, 20),
                        Color.Black, 1.0f);
                }

                if (Pinselfarbe.Optionen.over == -1)
                {
                    if (Karte.Material[GewaehltePinselfarbe].Farbe)
                    {
                        Help.DrawRectangle(spriteBatch, graphicsDevice,
                            new Rectangle((int)(temp2.X + Bildbreite / 2 - 25), (int)(temp2.Y), 50, 50),
                            Karte.Material[GewaehltePinselfarbe].CFarbe, 1.0f);
                    }
                    else
                    {
                        Vector2 maße = Help.AufBereichVerkleinern(Karte.Material[GewaehltePinselfarbe].Bild, 50, 50);
                        spriteBatch.Draw(Karte.Material[GewaehltePinselfarbe].Bild,
                            new Rectangle((int)(temp2.X + Bildbreite / 2 - 25 + (50 - maße.X) / 2), (int)(temp2.Y),
                                (int)maße.X, (int)maße.Y), Color.White);
                    }
                }
                else if (Pinselfarbe.Optionen.over > -1)
                {
                    if (Karte.Material[Pinselfarbe.Optionen.over].Farbe)
                    {
                        Help.DrawRectangle(spriteBatch, graphicsDevice,
                            new Rectangle((int)(temp2.X + Bildbreite / 2 - 25), (int)(temp2.Y), 50, 50),
                            Karte.Material[Pinselfarbe.Optionen.over].CFarbe, 1.0f);
                    }
                    else
                    {
                        Vector2 maße = Help.AufBereichVerkleinern(Karte.Material[Pinselfarbe.Optionen.over].Bild, 50, 50);
                        spriteBatch.Draw(Karte.Material[Pinselfarbe.Optionen.over].Bild,
                            new Rectangle((int)(temp2.X + Bildbreite / 2 - 25 + (50 - maße.X) / 2), (int)(temp2.Y),
                                (int)maße.X, (int)maße.Y), Color.White);
                    }
                }
            }
        }

        /// <summary>
        ///     macht den Editor unsichtbar
        /// </summary>
        public static void hide()
        {
            if (first)
            {
                Draw(Game1.spriteBatch, Game1.device, Vector2.Zero, Game1.screenWidth, Game1.screenHeight,
                    Game1.ContentAll, Game1.Spiel2);
            }

            visible = false;
            Gitter=false;
            Textfelder.hide();
            Optionen.hide();
            NutzlosesCombo.hide();
            Häuser.hide();
            TunnelCombo.hide();
            Fahrzeuge.hide();
            Waffen.hide();
            Pinselform.hide();
            Pinselfarbe.hide();
            Pinseldicke.hide();
        }

        /// <summary>
        ///     behandelt Mauszeigeraktionen (klicken)
        /// </summary>
        /// <param name="spriteBatch">eine Zeichenfläche</param>
        /// <param name="graphicsDevice">eine Graphics Device</param>
        /// <param name="oldmouseState">ein vorheriger Mauszustand</param>
        /// <param name="Spiel2">ein Spielobjekt</param>
        public static void MouseKeys(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, MouseState oldmouseState,
            Spiel Spiel2)
        {
            if (!visible) return;
            if (first) return;

            if (visible && Spiel2 != null && Zeichenmodus)
            {
                if (Textfelder.MouseKeys(Vector2.Zero))
                {
                    geklickt = true;
                    return;
                }

                if (Help.GetMouseState().LeftButton != oldmouseState.LeftButton &&
                    Help.GetMouseState().LeftButton == ButtonState.Pressed && mouseover > -1)
                {
                    int player = Spiel2.CurrentPlayer;
                    List<String> temp = null;

                    if (mouseover == 6)
                    {
                        temp = Spiel2.players[player].Minen[mouseoverid2].EditorSpeichern();
                        TextfeldID = mouseoverid2;
                    }
                    else if (mouseover == 5)
                    {
                        temp = Spiel2.Kisten.EditorSpeichern(mouseoverid);
                        TextfeldID = mouseoverid;
                    }
                    else if (mouseover == 4)
                    {
                        temp = Spiel2.Bunker.EditorSpeichern(mouseoverid);
                        TextfeldID = mouseoverid;
                    }
                    else if (mouseover == 3)
                    {
                        temp = Spiel2.players[player].EditorSpeichern(mouseoverid2);
                        TextfeldID = mouseoverid2;
                    }
                    else if (mouseover == 2)
                    {
                        temp = Nutzloses.EditorSpeichern(mouseoverid);
                        TextfeldID = mouseoverid;
                    }
                    else if (mouseover == 1)
                    {
                        temp = Spiel2.players[player].TunnelAnlage[mouseoverid2].EditorSpeichern();
                        TextfeldID = mouseoverid2;
                    }
                    else if (mouseover == 0)
                    {
                        temp = Spiel2.Haeuser.EditorSpeichern(mouseoverid);
                        TextfeldID = mouseoverid;
                    }

                    String res = "";
                    for (int c = 0; c < temp.Count; c++)
                    {
                        res = res + temp[c] + (c < temp.Count - 1 ? "\n" : "");
                    }

                    Textfeldtyp = mouseover;
                    TextfeldSpieler = player;
                    Textfelder.originalText = res;
                    Textfelder.Text = Help.ZerhackeTextAufFesteBreite(Textfelder.font, Textfelder.originalText,
                        Textfelder.maxPixelInZeile, true);
                    Textfelder.show();
                }
            }

            if (Zeichenmodus)
            {
                if (Optionen.Optionen.sichtbar)
                    switch (Optionen.Optionen.Interact(Vector2.Zero, false, oldmouseState))
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
                        default:
                            break;
                    }

                Optionen.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);

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

                if (Optionen.Titel.over == -1 && NutzlosesCombo.Titel.over == -1 && Häuser.Titel.over == -1 &&
                    TunnelCombo.Titel.over == -1 && Fahrzeuge.Titel.over == -1 && Waffen.Titel.over == -1 &&
                    Optionen.Optionen.over == -1 && NutzlosesCombo.Optionen.over == -1 && Häuser.Optionen.over == -1 &&
                    TunnelCombo.Optionen.over == -1 && Fahrzeuge.Optionen.over == -1 && Waffen.Optionen.over == -1)
                    MouseKeys2(Spiel2);
            }
            else if (!Zeichenmodus)
            {
                mouseover = -1;

                if (Pinselform.Optionen.sichtbar)
                {
                    int q = Pinselform.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                    if (q != -1) GewaehltePinselform = q;
                }

                if (Pinseldicke.Optionen.sichtbar)
                {
                    int q = Pinseldicke.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                    if (q != -1) GewaehltePinseldicke = q;
                }

                if (Pinselfarbe.Optionen.sichtbar)
                {
                    int q = Pinselfarbe.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                    if (q != -1) GewaehltePinselfarbe = q;
                }

                Pinselfarbe.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                Pinselform.Optionen.Interact(Vector2.Zero, false, oldmouseState);
                Pinseldicke.Optionen.Interact(Vector2.Zero, false, oldmouseState);

                Pinselfarbe.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                Pinselform.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
                Pinseldicke.MouseKeys(graphicsDevice, Vector2.Zero, oldmouseState);
            }


            if (!Textfelder.visible && Help.GetMouseState().LeftButton != oldmouseState.LeftButton &&
                Help.GetMouseState().LeftButton == ButtonState.Pressed)
            {
                float scale = Optimierung.Skalierung(0.25f);
                for (int i = 0; i <= Spiel2.players.Length; i++)
                {
                    var temp = new Vector2(15 + i * Texturen.LeeresFeld.Width * scale, 15);
                    Vector2 maus = Help.GetMousePos();
                    if (maus.X >= temp.X && maus.Y >= temp.Y && maus.X <= temp.X + Texturen.LeeresFeld.Width * scale &&
                        maus.Y <= temp.Y + Texturen.LeeresFeld.Height * scale)
                    {
                        GewaehlterSpieler = i - 1;
                        if (i > 0) Tausch.CurrentPlayer = i - 1;

                        geklickt = true;
                        return;
                    }
                }

                var temp2 = new Vector2(15, Game1.screenHeight - 15 - Texturen.LeeresFeld.Height * scale);
                Vector2 maus2 = Help.GetMousePos();
                if (maus2.X >= temp2.X && maus2.Y >= temp2.Y && maus2.X <= temp2.X + Texturen.LeeresFeld.Width * scale &&
                    maus2.Y <= temp2.Y + Texturen.LeeresFeld.Height * scale)
                {
                    Zeichenmodus = true;
                    lastchosen = -1;
                    lastchosenid = -1;
                    geklickt = true;
                    return;
                }

                temp2 = new Vector2(15 + Texturen.LeeresFeld.Width * scale,
                    Game1.screenHeight - 15 - Texturen.LeeresFeld.Height * scale);
                maus2 = Help.GetMousePos();
                if (maus2.X >= temp2.X && maus2.Y >= temp2.Y && maus2.X <= temp2.X + Texturen.LeeresFeld.Width * scale &&
                    maus2.Y <= temp2.Y + Texturen.LeeresFeld.Height * scale)
                {
                    Zeichenmodus = false;
                    lastchosen = -1;
                    lastchosenid = -1;
                    geklickt = true;
                    return;
                }
            }

            if (geklickt && Help.GetMouseState().LeftButton != oldmouseState.LeftButton &&
                Help.GetMouseState().LeftButton == ButtonState.Released)
            {
                geklickt = false;
            }

            if (!geklickt && Zeichenmodus && Optionen.Titel.over == -1 && NutzlosesCombo.Titel.over == -1 &&
                Häuser.Titel.over == -1 && TunnelCombo.Titel.over == -1 && Fahrzeuge.Titel.over == -1 &&
                Waffen.Titel.over == -1 && Optionen.Optionen.over == -1 && NutzlosesCombo.Optionen.over == -1 &&
                Häuser.Optionen.over == -1 && TunnelCombo.Optionen.over == -1 && Fahrzeuge.Optionen.over == -1 &&
                Waffen.Optionen.over == -1)
            {
                if (!Textfelder.visible && Help.GetMouseState().LeftButton != oldmouseState.LeftButton &&
                    Help.GetMouseState().LeftButton == ButtonState.Pressed)
                {
                    Vector2 Pos = Help.GetMousePos() + Spiel2.Fenster;
                    if (lastchosen == 0)
                    {
                        Vector2 Pos2 = Pos -
                                       new Vector2(
                                           Texturen.haus[lastchosenid].Width / 2 *
                                           Gebäudedaten.SKALIERUNG.Wert[lastchosenid], 0);
                        Spiel2.Haeuser.Add(new Vector2(Pos2.X, Kartenformat.BottomOf(Pos2)), -9999, lastchosenid,
                            GewaehlterSpieler);
                    }
                    else if (lastchosen == 1)
                    {
                        Vector2 Pos2 = Pos - new Vector2(Texturen.tunnel.Width / 2 * Tunnel.TUNNEL_SCALE.Wert);
                        if (Spiel2.PrüfeTunnelbau(Pos))
                            Spiel2.AddTunnel(GewaehlterSpieler, new Vector2(Pos2.X, Kartenformat.BottomOf(Pos2)));
                    }
                    else if (lastchosen == 2)
                    {
                        Spiel2.AddPanzer(GewaehlterSpieler, lastchosenid, 0, false,
                            new Vector2(Pos.X, Kartenformat.BottomOf(Pos)));
                    }
                    else if (lastchosen == 3)
                    {
                        if (lastchosenid < Texturen.baum.Length)
                        {
                            int which = lastchosenid;
                            Nutzloses.Hinzufügen(Texturen.baum[which],
                                new Vector2(Pos.X, Kartenformat.BottomOf(Pos) + 7), 0, false,
                                Baumdata.SKALIERUNG.Wert[which], Baum.BAEUME_KOLLISION, Baum.BAEUME_ZERSTOERUNG);
                        }
                        else if (lastchosenid < Texturen.baum.Length + Texturen.panzerruine.Length)
                        {
                            int which = lastchosenid - Texturen.baum.Length;

                            Nutzloses.Hinzufügen(Texturen.panzerruine[which],
                                new Vector2(Pos.X, Kartenformat.BottomOf(Pos)), 0, false,
                                Fahrzeugdaten.SCALEP.Wert[which], true, true);
                        }
                    }
                    else if (lastchosen == 4)
                    {
                        if (Waffendaten.Verschiessbar[lastchosenid] > 0 && Waffendaten.Verschiessbar[lastchosenid] != 5)
                            if (GewaehlterSpieler >= 0)
                            {
                                if (Waffendaten.Verschiessbar[lastchosenid] == 4)
                                {
                                    Spiel2.AddRakete(GewaehlterSpieler, Pos, Vector2.Zero, 300 * 4, lastchosenid, -1);
                                }
                                else if (Waffendaten.Verschiessbar[lastchosenid] == 2)
                                {
                                    Spiel2.Airstrike(Pos, GewaehlterSpieler);
                                }
                                else if (Waffendaten.Verschiessbar[lastchosenid] == 3)
                                {
                                    Vector2 Pos2 = Pos - new Vector2(Texturen.tunnel.Width / 2 * Tunnel.TUNNEL_SCALE.Wert);

                                    Spiel2.players[GewaehlterSpieler].Minen.Add(new Mine((int)Pos2.X,
                                        Kartenformat.BottomOf(Pos2), (int)(Waffendaten.Daten2[lastchosenid].Z),
                                        lastchosenid, Spiel2.players[GewaehlterSpieler].Minen.Count));
                                }
                                else
                                    Spiel2.AddRakete(GewaehlterSpieler, Pos, Vector2.Zero, 300 * 4, lastchosenid, -1);
                            }
                    }
                }
            }
            else if (!Zeichenmodus && !geklickt && Pinselform.Optionen.over == -1 && Pinselform.Titel.over == -1 &&
                     Pinselfarbe.Optionen.over == -1 && Pinselfarbe.Titel.over == -1 && Pinseldicke.Optionen.over == -1 &&
                     Pinseldicke.Titel.over == -1)
            {
                if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                {
                    Vector2 Pos = Help.GetMousePos() + Spiel2.Fenster;
                    Pos = new Vector2((int)Pos.X, (int)Pos.Y);
                    var list = new List<Vector3>();
                    int dicke = (GewaehltePinseldicke + 1) * 10;

                    if (GewaehltePinselform == 1)
                    {
                        for (int i = -dicke; i < dicke; i++)
                        {
                            list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Pos.Y - dicke),
                                (int)(Pos.Y + dicke), GewaehltePinselfarbe));
                        }
                    }
                    else if (GewaehltePinselform == 0)
                    {
                        int Breite = dicke;

                        double BreiteHochZwei = Math.Pow(Breite, 2);

                        for (int i = -Breite; i < Breite; i++)
                        {
                            if (i + Pos.X < 0 || i + Pos.X >= Spiel2.Spielfeld.Length) continue;

                            double Distanz = Breite;
                            if (i != 0)
                                Distanz = Math.Floor(Math.Sqrt((double) BreiteHochZwei-Math.Pow(i, 2)));

                            if (Distanz == 0) continue;

                            double Anfang = Pos.Y - Distanz;
                            double Ende = Pos.Y + Distanz;

                            if (Anfang < 0) Anfang = 0;
                            if (Ende >= Game1.Kartenhoehe) Ende = Game1.Kartenhoehe - 1;
                            list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Anfang),
                                (int) Ende, GewaehltePinselfarbe));
                        }
                    }

                    Vordergrund.AktualisiereVordergrund(list);
                }
                else if (Help.GetMouseState().RightButton == ButtonState.Pressed)
                {
                    Vector2 Pos = Help.GetMousePos() + Spiel2.Fenster;
                    var list = new List<Vector3>();
                    int dicke = (GewaehltePinseldicke + 1) * 10;

                    if (GewaehltePinselform == 1)
                    {
                        for (int i = -dicke; i < dicke; i++)
                        {
                            list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Pos.Y - dicke),
                                (int)(Pos.Y + dicke), Karte.LUFT));
                        }
                    }
                    else if (GewaehltePinselform == 0)
                    {
                        /*int width = dicke;
                        var aa = (int)(Math.Log((((width) - 0) * Math.PI), Math.E) * Math.Sqrt(width));
                        for (int i = -aa; i < aa; i++)
                        {
                            if (i + Pos.X < 0 || i + Pos.X >= Spiel2.Spielfeld.Length) continue;

                            int dist = i;
                            if (dist < 0) dist = -dist;
                            var add = (int)(Math.Log(((aa - dist) * Math.PI), Math.E) * Math.Sqrt(width));
                            if (add < 0) add = 0;
                            if (add > aa) add = aa;

                            var add2 = (int)(Pos.Y + add);
                            if (add2 > Game1.screenHeight) add2 = Game1.screenHeight;
                            list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Pos.Y - add),
                                add2, Karte.LUFT));
                            //    list.Add(new Vector3((int)(Pos.X + i), (int)(Pos.Y - add), (int)(add2)));
                        }*/
                        int Breite = dicke;

                        double BreiteHochZwei = Math.Pow(Breite, 2);

                        for (int i = -Breite; i < Breite; i++)
                        {
                            if (i + Pos.X < 0 || i + Pos.X >= Spiel2.Spielfeld.Length) continue;

                            double Distanz = Breite;
                            if (i != 0)
                                Distanz = Math.Floor(Math.Sqrt((double)BreiteHochZwei - Math.Pow(i, 2)));

                            if (Distanz == 0) continue;

                            double Anfang = Pos.Y - Distanz;
                            double Ende = Pos.Y + Distanz;

                            if (Anfang < 0) Anfang = 0;
                            if (Ende >= Game1.Kartenhoehe) Ende = Game1.Kartenhoehe - 1;
                            list.AddRange(Kartenformat.SetMaterialFromTo((int)(Pos.X + i), (int)(Anfang),
                                (int)Ende, Karte.LUFT));
                        }
                    }

                    Vordergrund.AktualisiereVordergrund(list);
                }
            }
        }

        /// <summary>
        ///     prüft, ob sich der Mauszeiger über einem auswählbaren Objekt befindet
        /// </summary>
        /// <param name="Spiel2">ein Spielobjekt</param>
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

            if (found == false)
            {
                mouseover = -1;
            }
        }

        /// <summary>
        ///     macht den Editor sichtbar
        /// </summary>
        public static void show(int Fensterbreite)
        {
            if (first)
            {
                Draw(Game1.spriteBatch, Game1.device, Vector2.Zero, Game1.screenWidth, Game1.screenHeight,
                    Game1.ContentAll, Game1.Spiel2);
            }

            visible = true;
            // Optionen.show(new Vector2(Fensterbreite - 100 -25, 0), 0);
            Optionen.show();
            NutzlosesCombo.show();
            Häuser.show();
            TunnelCombo.show();
            Fahrzeuge.show();
            Waffen.show();
            Pinselform.show();
            Pinselfarbe.show();
            Pinseldicke.show();
        }
    }
}