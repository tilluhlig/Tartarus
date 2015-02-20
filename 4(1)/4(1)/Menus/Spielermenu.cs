// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 05-31-2013
// ***********************************************************************
// <copyright file="Spielermenu.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _4_1_
{
    /// <summary>
    ///     Class Spielermenu
    /// </summary>
    public class Spielermenu
    {
        #region Fields

        /// <summary>
        ///     Die art des handelspartners x = {0==Fahrzeug, 1==Gebäude}, y = data
        /// </summary>
        public static Vector3 handelspartner;

        /// <summary>
        ///     The spielermenu
        /// </summary>
        public static Texture2D spielermenu;

        /// <summary>
        ///     The spielermenutextbox
        /// </summary>
        public static Texture2D spielermenutextbox;

        /// <summary>
        ///     The tempbox
        /// </summary>
        public static BoundingBox[] tempbox = new BoundingBox[9];

        /// <summary>
        ///     The temppos
        /// </summary>
        public static Vector2[] temppos = new Vector2[9];

        /// <summary>
        ///     The aux
        /// </summary>
        private static string aux = "";

        /// <summary>
        ///     The clickselected
        /// </summary>
        private static int clickselected = 255;

        /// <summary>
        ///     The clickselected2
        /// </summary>
        private static int clickselected2 = 255;

        //letzten 16 stück für die 9 felder bzw tank reserviert
        /// <summary>
        ///     The focused
        /// </summary>
        private static int focused;

        /// <summary>
        ///     The own pos
        /// </summary>
        private static Vector2 ownPos;

        /// <summary>
        ///     The position
        /// </summary>
        private static Vector2 Position;

        /// <summary>
        ///     The stringpos
        /// </summary>
        private static Vector2 stringpos;

        /// <summary>
        ///     The tankbox
        /// </summary>
        private static BoundingBox tankbox;

        /// <summary>
        ///     The textbox
        /// </summary>
        private static BoundingBox textbox;

        /// <summary>
        ///     The textboxpos
        /// </summary>
        private static Vector2 textboxpos;

        private SpriteFont Schrift;

        private Inventar Tauschrucksack;

        /// <summary>
        ///     The back pack
        /// </summary>
        public Rucksack backPack;

        /// <summary>
        ///     The intrade
        /// </summary>
        public bool intrade = false;

        /// <summary>
        ///     The temp
        /// </summary>
        private int temp = -1;

        /// <summary>
        ///     The tradebackpack
        /// </summary>
        private Rucksack tradebackpack;

        /// <summary>
        ///     The visible
        /// </summary>
        public bool visible = false;

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        /// <param name="Effekte">The effekte.</param>
        /// <param name="Rucksack">The rucksack.</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <param name="Transparenz">The transparenz.</param>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font, EffectPacket Effekte, Inventar Rucksack, Spiel Spiel2,
            float Transparenz)
        {
            if (!visible) return;
            Schrift = font;
            spriteBatch.Draw(spielermenu, ownPos, null, Color.White*Transparenz, 0, Vector2.Zero,
                Optimierung.Skalierung(0.5f), SpriteEffects.None, 0);
            spriteBatch.Draw(spielermenutextbox, textboxpos, null, Color.White*Transparenz, 0, Vector2.Zero,
                Optimierung.Skalierung(0.5f), SpriteEffects.None, 0);
            if (intrade)
                spriteBatch.Draw(spielermenu, ownPos + new Vector2(500, 0), null, Color.White*Transparenz, 0,
                    Vector2.Zero, Optimierung.Skalierung(0.5f), SpriteEffects.None, 0);

            // Spieler zeichnen
            if (Spiel2 != null)
            {
                int i2 = Spiel2.CurrentPlayer;
                int b2 = Spiel2.players[i2].CurrentTank;
                int kindof = Spiel2.players[i2].KindofTank[b2];
                float scaleP = Fahrzeugdaten.SCALEP.Wert[kindof];
                float scaleR = Fahrzeugdaten.SCALER.Wert[kindof];
                spriteBatch.End();
                Spiel2.players[i2].DrawPlayer(b2, spriteBatch, MathHelper.ToRadians(175), MathHelper.ToRadians(0),
                    scaleP, scaleR,
                    new Vector2(tankbox.Min.X, tankbox.Min.Y) +
                    new Vector2(104, 50 + Texturen.panzerindex[kindof].Height*scaleP/2), new Vector2(0, 0), true, false,
                    1.5f, 0.5f, Color.White, true);
                spriteBatch.Begin();
            }

            // Spieler2 zeichnen
            if (intrade && Spiel2 != null)
            {
                if (handelspartner.X == 0)
                {
                    int i2 = Spiel2.CurrentPlayer;
                    var b2 = (int) handelspartner.Y;
                    int kindof = Spiel2.players[i2].KindofTank[b2];
                    float scaleP = Fahrzeugdaten.SCALEP.Wert[kindof];
                    float scaleR = Fahrzeugdaten.SCALER.Wert[kindof];
                    spriteBatch.End();
                    Spiel2.players[i2].DrawPlayer(b2, spriteBatch, MathHelper.ToRadians(175), MathHelper.ToRadians(0),
                        scaleP, scaleR,
                        new Vector2(tankbox.Min.X, tankbox.Min.Y) +
                        new Vector2(605, 50 + Texturen.panzerindex[kindof].Height*scaleP/2), new Vector2(0, 0), true,
                        false, 1.5f, 0.5f, Color.White, true);
                    spriteBatch.Begin();
                }
                else if (handelspartner.X == 1)
                {
                    var id = (int) handelspartner.Y;
                    spriteBatch.Draw(Texturen.haus[id],
                        new Vector2(tankbox.Min.X, tankbox.Min.Y) +
                        new Vector2(605 - Texturen.haus[id].Width*Gebäudedaten.SKALIERUNG.Wert[id]*0.5f/2,
                            50 - Texturen.haus[id].Height*Gebäudedaten.SKALIERUNG.Wert[id]*0.5f/2),
                        new Rectangle(0, 0, Texturen.haus[id].Width, Texturen.haus[id].Height), Color.White, 0,
                        Vector2.Zero, Gebäudedaten.SKALIERUNG.Wert[id]*0.5f, SpriteEffects.None, 0);
                }
            }

            backPack.Draw(spriteBatch, Rucksack, ref clickselected, Transparenz);

            if (Effekte == null || Rucksack == null) return;
            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(Texturen.LeeresFeld, temppos[i], null, Color.White*Transparenz, 0f, Vector2.Zero,
                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                if (i < Effekte.Konsumierbares.Count)
                    spriteBatch.Draw(Effekte.Konsumierbares[i].Bild, temppos[i], null, Color.White*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
            }

            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(Texturen.LeeresFeld, temppos[i + 3], null, Color.White*Transparenz, 0f, Vector2.Zero,
                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                if (i < Effekte.Status.Count)
                    spriteBatch.Draw(Effekte.Status[i].Bild, temppos[i + 3], null, Color.White*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
            }

            for (int i = 0; i < 3; i++)
            {
                spriteBatch.Draw(Texturen.LeeresFeld, temppos[i + 6], null, Color.White*Transparenz, 0f, Vector2.Zero,
                    Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
                if (i < Effekte.Upgrades.Count)
                    spriteBatch.Draw(Effekte.Upgrades[i].Bild, temppos[i + 6], null, Color.White*Transparenz, 0f,
                        Vector2.Zero, Optimierung.Skalierung(0.25f), SpriteEffects.None, 0f);
            }

            if (intrade)
            {
                tradebackpack.Draw(spriteBatch, Tauschrucksack, ref clickselected2, Transparenz);
            }

            if (focused == 0)
            {
                if (clickselected > 250)
                    switch (clickselected)
                    {
                            /*  case 255:
                          {
                              //gebe panzerdaten aus
                              spriteBatch.DrawString(font, "asdasdasd", stringpos, Color.Red * Transparenz); //> fahrzeugbox gewahlt!

                              return;
                          }
                      case 254:
                          {
                              //gebe panzerdaten aus
                              spriteBatch.DrawString(font, " ", stringpos, Color.Red * Transparenz); //> fahrzeugbox gewahlt!
                              return;
                          }*/
                        case 253:
                        {
                            spriteBatch.DrawString(font, aux, stringpos, Color.Red*Transparenz);

                            return;
                        }
                        default:
                        {
                            int fahrzeuge = Spiel2.players[Spiel2.CurrentPlayer].GibArtillerieAnzahl() +
                                            Spiel2.players[Spiel2.CurrentPlayer].GibPanzer() +
                                            Spiel2.players[Spiel2.CurrentPlayer].GibBaufahrzeugeAnzahl() +
                                            Spiel2.players[Spiel2.CurrentPlayer].GibScout();
                            int geschütze = Spiel2.players[Spiel2.CurrentPlayer].GibGeschützAnzahl() +
                                            Spiel2.players[Spiel2.CurrentPlayer].GibGeschütz2Anzahl();

                            aux = "Credits/Runde: " +
                                  Spiel2.players[Spiel2.CurrentPlayer].Generate_Credits(Spiel2.Haeuser,
                                      Spiel2.CurrentPlayer) + "\n" +
                                  "Haeuser: " +
                                  Spiel2.players[Spiel2.CurrentPlayer].GibHäuserAnzahl(Spiel2.Haeuser, Spiel2.CurrentPlayer) +
                                  "\n" +
                                  "Fabriken: " +
                                  Spiel2.players[Spiel2.CurrentPlayer].GibFabrikenAnzahl(Spiel2.Haeuser, Spiel2.CurrentPlayer) +
                                  "\n" +
                                  "Haendler: " +
                                  Spiel2.players[Spiel2.CurrentPlayer].GibHändlerAnzahl(Spiel2.Haeuser, Spiel2.CurrentPlayer) +
                                  "\n" +
                                  "Tunnel: " + Spiel2.players[Spiel2.CurrentPlayer].TunnelAnlage.Count + "/" +
                                  Allgemein.MaxTunnel + "\n" +
                                  "\n" +
                                  "Fahrzeuge: " + fahrzeuge + "/" + Allgemein.MaxFahrzeug + "\n" +
                                  "Artillerie: " + Spiel2.players[Spiel2.CurrentPlayer].GibArtillerieAnzahl() + "\n" +
                                  "Panzer: " + Spiel2.players[Spiel2.CurrentPlayer].GibPanzer() + "\n" +
                                  "Baufahrzeug: " + Spiel2.players[Spiel2.CurrentPlayer].GibBaufahrzeugeAnzahl() + "\n" +
                                  "Spaeher: " + Spiel2.players[Spiel2.CurrentPlayer].GibScout() + "\n" +
                                  "\n" +
                                  "Geschuetze: " + geschütze + "/" + Allgemein.MaxGeschuetze + "\n" +
                                  "Geschuetz I: " + Spiel2.players[Spiel2.CurrentPlayer].GibGeschützAnzahl() + "\n" +
                                  "Geschuetz II: " + Spiel2.players[Spiel2.CurrentPlayer].GibGeschütz2Anzahl() + "\n";
                            spriteBatch.DrawString(font, aux, stringpos, Color.Red*Transparenz);

                            aux = "\n\n\n\n" +
                                  "Bunker: " +
                                  Spiel2.players[Spiel2.CurrentPlayer].GibBunkerAnzahl(Spiel2.Bunker, Spiel2.CurrentPlayer) +
                                  "/" + Allgemein.MaxBunker + "\n";
                            spriteBatch.DrawString(font, aux, stringpos + new Vector2(140, 0), Color.Red*Transparenz);

                            return;
                        }
                    }

                if (clickselected > 239)
                    switch ((clickselected - 240)/3)
                    {
                        case 0:
                        {
                            if (clickselected - 240 < Effekte.Konsumierbares.Count)
                                spriteBatch.DrawString(font,
                                    "> Konsumitem " + Effekte.Konsumierbares[clickselected - 240].Name + "\n" +
                                    Effekte.Konsumierbares[clickselected - 240].Beschreibungstext(), stringpos,
                                    Color.Red*Transparenz);
                            return;
                        }
                            ;
                        case 1:
                        {
                            if (clickselected - 243 < Effekte.Status.Count)
                                spriteBatch.DrawString(font,
                                    "> Status " + Effekte.Status[clickselected - 243].Name + "\n" +
                                    Effekte.Status[clickselected - 243].Beschreibungstext(), stringpos,
                                    Color.Red*Transparenz);
                            return;
                        }
                            ;
                        case 2:
                        {
                            if (clickselected - 246 < Effekte.Upgrades.Count)
                                spriteBatch.DrawString(font,
                                    "> Upgrade " + Effekte.Upgrades[clickselected - 246].Name + "\n" +
                                    Effekte.Upgrades[clickselected - 246].Beschreibungstext(), stringpos,
                                    Color.Red*Transparenz);
                            return;
                        }
                            ;
                        default:
                        {
                            spriteBatch.DrawString(font, "> FEHLER", stringpos, Color.Red*Transparenz);
                            return;
                        }
                    }
            }

            if (focused == 1)
            {
                if (clickselected2 > 250)
                    switch (clickselected2)
                    {
                        case 253:
                        {
                            spriteBatch.DrawString(font, aux, stringpos, Color.Red*Transparenz);
                            return;
                        }
                        default:
                        {
                            aux = "";
                            spriteBatch.DrawString(font, aux, stringpos, Color.Red*Transparenz);
                            return;
                        }
                    }
            }

            spriteBatch.DrawString(font, aux, stringpos, Color.Red*Transparenz);
        }

        /// <summary>
        ///     Exits the trade.
        /// </summary>
        public void ExitTrade()
        {
            focused = 0;
            clickselected2 = 255;
            intrade = false;
        }

        /// <summary>
        ///     Gets the in trade.
        /// </summary>
        /// <param name="remoteitems">The remoteitems.</param>
        /// <param name="remoteanzahl">The remoteanzahl.</param>
        public void GetInTrade(Inventar RemoteRucksack, Vector3 Partner)
        {
            handelspartner = Partner;
            Tauschrucksack = RemoteRucksack;
            intrade = true;
        }

        /// <summary>
        ///     Hides this instance.
        /// </summary>
        public void hide()
        {
            visible = false;
        }

        /// <summary>
        ///     Loads the content.
        /// </summary>
        /// <param name="Content">The content.</param>
        public void LoadContent(ContentManager Content)
        {
            Texturen.Bilddateien.Add("spielermenu", 0.5f);
            spielermenu = Content.Load<Texture2D>("Textures\\spielermenu");

            //Texturen.Bilddateien.Add("spielermenuok", 0.5f);
            //spielermenuok = Content.Load<Texture2D>("Textures\\spielermenuok");

            Texturen.Bilddateien.Add("spielermenutextbox", 0.5f);
            spielermenutextbox = Content.Load<Texture2D>("Textures\\spielermenutextbox");

            Position = new Vector2(Game1.screenWidth/2 - spielermenu.Width/2*Optimierung.Skalierung(0.5f), 0);

            ownPos = new Vector2(0, 0) + Position - new Vector2(150, 0);
            textboxpos = ownPos + new Vector2(220, 6);
            tankbox = new BoundingBox(new Vector3(7 + ownPos.X, 7 + ownPos.Y, 0),
                new Vector3(210 + ownPos.X, 100 + ownPos.Y, 0));
            textbox = new BoundingBox(new Vector3(textboxpos, 0),
                new Vector3(
                    textboxpos +
                    new Vector2(spielermenutextbox.Width, spielermenutextbox.Height)*Optimierung.Skalierung(0.5f), 0));
            stringpos = textboxpos + new Vector2(10, 6);

            backPack = new Rucksack(ownPos + new Vector2(3, 268), 6, 2);
            tradebackpack = new Rucksack(ownPos + new Vector2(505, 105), 3, 4);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    temppos[i*3 + j] = new Vector2(3, 105) + ownPos +
                                       new Vector2((Texturen.LeeresFeld.Width*Optimierung.Skalierung(0.25f) + 5/4)*j,
                                           (Texturen.LeeresFeld.Height*Optimierung.Skalierung(0.25f))*i);
                    tempbox[i*3 + j] = new BoundingBox(new Vector3(temppos[i*3 + j], 0),
                        new Vector3(
                            temppos[i*3 + j] +
                            new Vector2(Texturen.LeeresFeld.Width*Optimierung.Skalierung(0.25f),
                                Texturen.LeeresFeld.Height*Optimierung.Skalierung(0.25f)), 0));
                }
        }

        /// <summary>
        ///     Mouses the keys.
        /// </summary>
        /// <param name="mouseState">State of the mouse.</param>
        /// <param name="Rucksack">The rucksack.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool mouseKeys(MouseState mouseState, Inventar Rucksack, EffectPacket Effekte)
        {
            if (!visible) return false;

            if (focused == 0)
            {
                int tre = Rucksack.GibTreibstoffFächer();
                int mun = Rucksack.GibMunitionsFächer();
                int upg = Rucksack.GibUpgradeFächer();
                int kon = Rucksack.GibKonsumierbareFächer();

                List<Vector2> munlist = Rucksack.GibMunitionsliste();
                List<Vector2> upglist = Rucksack.GibtListeUpgrades();
                List<Vector2> konlist = Rucksack.GibListeKonsumierbares();

                if (clickselected < tre)
                    aux = " ";
                else if (clickselected < mun + tre)
                {
                    var which = (int) munlist[clickselected - tre].X;
                    if (Schrift != null)
                        aux =
                            Help.ListeZuString(Help.ZerhackeTextAufFesteBreite(Schrift, Waffendaten.GibWaffentext(which),
                                257, false));

                    aux = aux + "\n" +
                          "Maximalschaden: " + Waffendaten.Zentrumschaden[which] + "\n" +
                          "Radius: " + (Waffendaten.Daten[which].X*Waffendaten.Energiefaktor[which]) + "\n" +
                          "AP Verbrauch: " + Waffendaten.APKosten[which];
                }
                else if (clickselected < mun + upg + tre)
                {
                    var temp = (int) upglist[clickselected - mun - tre].X;
                    aux = "> Upgrade " + Rucksack.Upgrades[temp].Name + "\n" +
                          Rucksack.Upgrades[temp].Effekt.Beschreibungstext();
                }
                else if (clickselected < mun + upg + kon + tre)
                {
                    var temp = (int) konlist[clickselected - mun - upg - tre].X;
                    aux = "> Konsumitem " + Rucksack.Konsumierbares[temp].Name + "\n" +
                          Rucksack.Konsumierbares[temp].Effekt.Beschreibungstext();
                }
            }

            if (focused == 1)
            {
                int tre2 = Rucksack.GibTreibstoffFächer();
                int mun2 = Rucksack.GibMunitionsFächer();
                int upg2 = Rucksack.GibUpgradeFächer();
                int kon2 = Rucksack.GibKonsumierbareFächer();

                List<Vector2> munlist2 = Rucksack.GibMunitionsliste();
                List<Vector2> upglist2 = Rucksack.GibtListeUpgrades();
                List<Vector2> konlist2 = Rucksack.GibListeKonsumierbares();
                if (clickselected2 < tre2)
                    aux = ""; //Treibstoff
                else if (clickselected2 < mun2 + tre2)
                {
                    var which = (int) munlist2[clickselected2 - tre2].X;
                    if (Schrift != null)
                        aux =
                            Help.ListeZuString(Help.ZerhackeTextAufFesteBreite(Schrift, Waffendaten.GibWaffentext(which),
                                257, false));
                    aux = aux + "\n" +
                          "Maximalschaden: " + Waffendaten.Zentrumschaden[which] + "\n" +
                          "Radius: " + (Waffendaten.Daten[which].X*Waffendaten.Energiefaktor[which]) + "\n" +
                          "AP Verbrauch: " + Waffendaten.APKosten[which];
                }
                else if (clickselected2 < mun2 + upg2 + tre2)
                {
                    var temp = (int) upglist2[clickselected2 - mun2 - tre2].X;
                    aux = "> Upgrade " + Rucksack.Upgrades[temp].Name + "\n" +
                          Rucksack.Upgrades[temp].Effekt.Beschreibungstext();
                }
                else if (clickselected2 < mun2 + upg2 + kon2 + tre2)
                {
                    var temp = (int) konlist2[clickselected2 - mun2 - upg2 - tre2].X;
                    aux = "> Konsumitem " + Rucksack.Konsumierbares[temp].Name + "\n" +
                          Rucksack.Konsumierbares[temp].Effekt.Beschreibungstext();
                }
            }

            for (byte i = 0; i < tempbox.Length; i++)
                if (tempbox[i].Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                    ContainmentType.Contains)
                    if (Help.GetMouseState().LeftButton == ButtonState.Pressed)
                    {
                        focused = 0;
                        clickselected2 = 255;
                        clickselected = (byte) (240 + i);
                        return true;
                    }

            if (tankbox.Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                ContainmentType.Contains
                && Help.GetMouseState().LeftButton == ButtonState.Pressed)
            {
                focused = 0;
                clickselected2 = 255;
                clickselected = 254;
                return true;
            }

            if (textbox.Contains(new Vector3(Help.GetMouseState().X, Help.GetMouseState().Y, 0)) ==
                ContainmentType.Contains
                && Help.GetMouseState().LeftButton == ButtonState.Pressed)
            {
                focused = 0;
                clickselected2 = 255;
                clickselected = 255;
                return true;
            }

            int bb = clickselected;
            int stat = backPack.mouseKeys(mouseState, ref clickselected, Rucksack);
            if (stat == 1)
            {
                //übergabe von clickselected
                focused = 0;
                clickselected2 = 255;
                if (clickselected == bb)
                {
                    clickselected = 253;

                    int tre = Rucksack.GibTreibstoffFächer();
                    int mun = Rucksack.GibMunitionsFächer();
                    int upg = Rucksack.GibUpgradeFächer();
                    int kon = Rucksack.GibKonsumierbareFächer();

                    List<Vector2> munlist = Rucksack.GibMunitionsliste();
                    List<Vector2> upglist = Rucksack.GibtListeUpgrades();
                    List<Vector2> konlist = Rucksack.GibListeKonsumierbares();
                    int selected = backPack.selected;

                    if (!intrade)
                    {
                        if (selected < tre)
                        {
                            // wozu sollte man treibstoff auswählen?
                        }
                        else if (selected < mun + tre)
                        {
                            // munition ausgewählt
                            var which = (int) munlist[selected - tre].X;
                            if (
                                Fahrzeugdaten.ShootableAmmunition[
                                    Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].KindofTank[
                                        Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].CurrentTank], which] > 0)
                            {
                                Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].CurrentWeapon = which;
                            }
                        }
                        else if (selected < mun + tre + upg)
                        {
                            var which = (int) upglist[selected - tre - mun].X;
                            // Upgrade gewählt
                            Rucksack.BenutzenUpgrade(Effekte, which);
                            backPack.selected = 255;
                            clickselected = 255;
                        }
                        else if (selected < mun + tre + upg + kon)
                        {
                            var which = (int) konlist[selected - tre - mun - upg].X;
                            // Konsumitem gewählt
                            Rucksack.BenutzenKonsumierbares(Effekte, which);
                            backPack.selected = 255;
                            clickselected = 255;
                        }
                    }
                    else
                    {
                        // hier wird gehandelt, von Spieler zu Handelspartner

                        if (selected < tre)
                        {
                            // Treibstoff schicken
                            float schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? 500 : 100;

                            float result = Rucksack.EntnehmenTreibstoff(schritt);
                            float transferiert = result;
                            result = Tauschrucksack.HinzufügenTreibstoff(result);
                            transferiert -= result;
                            Rucksack.HinzufügenTreibstoff(result);

                            if (handelspartner.X == 1)
                                Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits += transferiert*
                                                                                            Allgemein.TreibstoffPreis;
                        }
                        else if (selected < mun + tre)
                        {
                            // munition schicken
                            var which = (int) munlist[selected - tre].X;

                            int schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift)
                                ? Waffendaten.Fachgröße[which]
                                : Waffendaten.Austauschgröße[which];
                            if (munlist[selected - tre].Y < schritt) schritt = (int) munlist[selected - tre].Y;

                            int result = Rucksack.EntnehmenMunition(which, schritt);
                            float transferiert = result;
                            result = Tauschrucksack.HinzufügenMunition(which, result);
                            transferiert -= result;
                            Rucksack.HinzufügenMunition(which, result);

                            if (handelspartner.X == 1)
                                Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits += transferiert*
                                                                                            Waffendaten.Preis[which];
                        }
                        else if (selected < mun + tre + upg)
                        {
                            var which = (int) upglist[selected - tre - mun].X;
                            // Upgrade schicken
                            int schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? Rucksack.Fachgroesse : 1;
                            if (upglist[selected - tre - mun].Y < schritt)
                                schritt = (int) upglist[selected - tre - mun].Y;

                            Item temp = Inventar.Neu(Rucksack.Upgrades[which]);
                            int result = Rucksack.EntnehmenUpgrade(which, schritt);
                            temp.Anzahl = result;
                            result = Tauschrucksack.Hinzufügen(temp);
                            temp.Anzahl = result;
                            Rucksack.Hinzufügen(temp);
                        }
                        else if (selected < mun + tre + upg + kon)
                        {
                            var which = (int) konlist[selected - tre - mun - upg].X;
                            // Konsumitem schicken
                            int schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? Rucksack.Fachgroesse : 1;
                            if (konlist[selected - tre - mun - upg].Y < schritt)
                                schritt = (int) konlist[selected - tre - mun - upg].Y;
                            Item temp = Inventar.Neu(Rucksack.Konsumierbares[which]);
                            int result = Rucksack.EntnehmenUpgrade(which, schritt);
                            temp.Anzahl = result;
                            result = Tauschrucksack.Hinzufügen(temp);
                            temp.Anzahl = result;
                            Rucksack.Hinzufügen(temp);
                        }
                    }
                }
                return true;
            }
            if (stat == 2) return true;

            if (intrade)
            {
                int bb2 = clickselected2;
                int stat2 = tradebackpack.mouseKeys(mouseState, ref clickselected2, Tauschrucksack);
                if (stat2 == 1)
                {
                    //übergabe von clickselected2
                    focused = 1;
                    clickselected = 255;
                    if (clickselected2 == bb2)
                    {
                        // hier wird gehandelt, von Handelspartner zu Spieler
                        clickselected2 = 253;
                        int selected = tradebackpack.selected;
                        int tre = Tauschrucksack.GibTreibstoffFächer();
                        int mun = Tauschrucksack.GibMunitionsFächer();
                        int upg = Tauschrucksack.GibUpgradeFächer();
                        int kon = Tauschrucksack.GibKonsumierbareFächer();

                        List<Vector2> munlist = Tauschrucksack.GibMunitionsliste();
                        List<Vector2> upglist = Tauschrucksack.GibtListeUpgrades();
                        List<Vector2> konlist = Tauschrucksack.GibListeKonsumierbares();

                        if (selected < tre)
                        {
                            // Treibstoff schicken
                            float schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? 500 : 100;
                            if (Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits <
                                schritt*Allgemein.TreibstoffPreis)
                                schritt = Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits/
                                          Allgemein.TreibstoffPreis;

                            float result = Tauschrucksack.EntnehmenTreibstoff(schritt);
                            float transferiert = result;
                            result = Rucksack.HinzufügenTreibstoff(result);
                            transferiert -= result;
                            Tauschrucksack.HinzufügenTreibstoff(result);

                            if (handelspartner.X == 1)
                                Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits -= transferiert*
                                                                                            Allgemein.TreibstoffPreis;
                        }
                        else if (selected < mun + tre)
                        {
                            // munition schicken
                            var which = (int) munlist[selected - tre].X;

                            // Treibstoff schicken
                            int schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift)
                                ? Waffendaten.Fachgröße[which]
                                : Waffendaten.Austauschgröße[which];
                            if (munlist[selected - tre].Y < schritt) schritt = (int) munlist[selected - tre].Y;

                            if (Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits <
                                schritt*Waffendaten.Preis[which])
                                schritt =
                                    (int)
                                        (Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits/
                                         Waffendaten.Preis[which]);

                            int result = Tauschrucksack.EntnehmenMunition(which, schritt);
                            float transferiert = result;
                            result = Rucksack.HinzufügenMunition(which, result);
                            transferiert -= result;
                            Tauschrucksack.HinzufügenMunition(which, result);

                            if (handelspartner.X == 1)
                                Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits -= transferiert*
                                                                                            Waffendaten.Preis[which];
                        }
                        else if (selected < mun + tre + upg)
                        {
                            var which = (int) upglist[selected - tre - mun].X;
                            // Upgrade schicken
                            int schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? Rucksack.Fachgroesse : 1;
                            if (upglist[selected - tre - mun].Y < schritt)
                                schritt = (int) upglist[selected - tre - mun].Y;

                            Item temp = Inventar.Neu(Tauschrucksack.Upgrades[which]);

                            if (Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits < schritt*temp.Preis)
                                schritt = (int) (Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits/temp.Preis);

                            int result = Tauschrucksack.EntnehmenUpgrade(which, schritt);
                            float transferiert = result;
                            temp.Anzahl = result;
                            result = Rucksack.Hinzufügen(temp);
                            transferiert -= result;
                            temp.Anzahl = result;
                            Tauschrucksack.Hinzufügen(temp);

                            if (handelspartner.X == 1)
                                Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits -= transferiert*temp.Preis;
                        }
                        else if (selected < mun + tre + upg + kon)
                        {
                            var which = (int) konlist[selected - tre - mun - upg].X;
                            // Konsumitem schicken
                            int schritt = Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? Rucksack.Fachgroesse : 1;
                            if (konlist[selected - tre - mun - upg].Y < schritt)
                                schritt = (int) konlist[selected - tre - mun - upg].Y;

                            Item temp = Inventar.Neu(Tauschrucksack.Konsumierbares[which]);

                            if (Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits < schritt*temp.Preis)
                                schritt = (int) (Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits/temp.Preis);

                            int result = Tauschrucksack.EntnehmenUpgrade(which, schritt);
                            float transferiert = result;
                            temp.Anzahl = result;
                            result = Rucksack.Hinzufügen(temp);
                            transferiert -= result;
                            temp.Anzahl = result;
                            Tauschrucksack.Hinzufügen(temp);

                            if (handelspartner.X == 1)
                                Game1.Spiel2.players[Game1.Spiel2.CurrentPlayer].Credits -= transferiert*temp.Preis;
                        }
                    }
                    return true;
                }
                if (stat2 == 2)
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Shows this instance.
        /// </summary>
        public void show()
        {
            visible = true;
        }

        #endregion Methods
    }
}