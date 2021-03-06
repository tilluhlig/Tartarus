﻿// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 07-25-2013
// ***********************************************************************
// <copyright file="Spieler.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace _4_1_
{
    /// <summary>
    ///     Class Spieler
    /// </summary>
    public class Spieler : _4_1_.ISpieler
    {
        public Kenngroesse Kenngroesse_Wert = null;

        #region Fields

        /// <summary>
        ///     The GLOBA l_ FUEL
        /// </summary>
        public static Var<bool> GLOBAL_FUEL = new Var<bool>("GLOBAL_FUEL", false);

        /// <summary>
        ///     The PLAYE r_ FUEL
        /// </summary>
        public static Var<bool> PLAYER_FUEL = new Var<bool>("PLAYER_FUEL", false);

        /// <summary>
        ///     The WAFFE n_ COOLDOWN
        /// </summary>
        public static Var<bool> WAFFEN_COOLDOWN = new Var<bool>("WAFFEN_COOLDOWN", false);

        /// <summary>
        ///     The fauxdown
        /// </summary>
        private readonly float fauxdown = MathHelper.ToRadians(0);

        /// <summary>
        ///     The fauxup
        /// </summary>
        private readonly float fauxup = MathHelper.ToRadians(180);

        /// <summary>
        ///     The action points
        /// </summary>
        public int ActionPoints = 999;
        public int _ActionPoints
        {
            get
            {
                return ActionPoints;
            }
        }

        /// <summary>
        ///     The angle
        /// </summary>
        public List<float> Angle = new List<float>();
        public List<float> _Angle
        {
            get
            {
                return Angle;
            }
        }

        /// <summary>
        ///     The cooldown
        /// </summary>
        public List<int> Cooldown = new List<int>();
        public List<int> _Cooldown
        {
            get
            {
                return Cooldown;
            }
        }

        /// <summary>
        ///     The credits
        /// </summary>
        public float Credits = 0;

        /// <summary>
        ///     Das aktuelle Level des Fahrzeugs
        /// </summary>
        public List<int> CurrentLv = new List<int>();
        public List<int> _CurrentLv
        {
            get
            {
                return CurrentLv;
            }
        }

        /// <summary>
        ///     The current weapon
        /// </summary>
        public int CurrentWeapon = 0;
        public int _CurrentWeapon
        {
            get
            {
                return CurrentWeapon;
            }
        }

        /// <summary>
        ///     The effekte
        /// </summary>
        public List<EffectPacket> Effekte = new List<EffectPacket>();
        public List<EffectPacket> _Effekte
        {
            get
            {
                return Effekte;
            }
        }

        //enthält den aktuellen Level des Fahrzeugs
        /// <summary>
        ///     The exp now
        /// </summary>
        public List<int> ExpNow = new List<int>();
        public List<int> _ExpNow
        {
            get
            {
                return ExpNow;
            }
        }

        //gibt an, wieviel Exp das Fahrzeug bereits hat
        //    public List<int> ExpProgress = new List<int>();
        /// <summary>
        ///     The exp progress
        /// </summary>
        /// <summary>
        ///     The farbe
        /// </summary>
        public Color Farbe;

        /// <summary>
        ///     The im tunnel
        /// </summary>
        public bool ImTunnel = false;

        /// <summary>
        ///     The kindof tank
        /// </summary>
        public List<int> KindofTank = new List<int>();
        public List<int> _KindofTank
        {
            get
            {
                return KindofTank;
            }
        }

        /// <summary>
        ///     The collision
        /// </summary>
        public List<KollisionsObjekt> Kollision = new List<KollisionsObjekt>();

        /// <summary>
        ///     The max pixel
        /// </summary>
        public List<int> MaxPixel = new List<int>();

        /// <summary>
        ///     The max schuesse
        /// </summary>
        public int MaxSchuesse;

        /// <summary>
        ///     The max timeout
        /// </summary>
        public int MaxTimeout;

        /// <summary>
        ///     The minen
        /// </summary>
        public List<Mine> Minen = new List<Mine>();
        public List<Mine> _Minen
        {
            get
            {
                return Minen;
            }
        }

        /// <summary>
        ///     The munition
        /// </summary>
        public List<int>[] Munition = new List<int>[21];
        public List<int>[] _Munition
        {
            get
            {
                return Munition;
            }
        }

        public List<String> Namen = new List<String>();
        public List<String> _Namen
        {
            get
            {
                return Namen;
            }
        }

        public void SetzeFahrzeugname(String neuerName, int id)
        {
            if (id < 0 || id >= Namen.Count) return;
            Namen[id] = neuerName;
        }

        public Notizen Notiz = new Notizen();

        /// <summary>
        ///     The rucksack
        /// </summary>
        public List<Inventar> Rucksack = new List<Inventar>();
        public List<Inventar> _Rucksack
        {
            get
            {
                return Rucksack;
            }
        }

        /// <summary>
        ///     The size
        /// </summary>
        public List<float> Size = new List<float>();

        /// <summary>
        ///     The size of cannon
        /// </summary>
        public List<float> SizeOfCannon = new List<float>();

        /// <summary>
        ///     The tunnel anlage
        /// </summary>
        public List<Tunnel> TunnelAnlage = new List<Tunnel>();
        public List<Tunnel> _TunnelAnlage
        {
            get
            {
                return TunnelAnlage;
            }
        }

        /// <summary>
        ///     The destruction
        /// </summary>
        public List<ZerstörungsObjekt> Zerstörung = new List<ZerstörungsObjekt>();

        /// <summary>
        ///     The zielpos
        /// </summary>
        public List<Vector2> Zielpos = new List<Vector2>();
        public List<Vector2> _Zielpos
        {
            get
            {
                return Zielpos;
            }
        }

        /// <summary>
        ///     The _ current tank
        /// </summary>
        private int _CurrentTank; // aktuell gewählter panzer

        /// <summary>
        ///     The fuel remains
        /// </summary>
        public float fuelRemains = 999;

        /// <summary>
        ///     The hp
        /// </summary>
        public List<int> hp = new List<int>();
        public List<int> _hp
        {
            get
            {
                return hp;
            }
        }


        public int id = 0;

        /// <summary>
        ///     The isthere
        /// </summary>
        public List<bool> isthere = new List<bool>();

        /// <summary>
        ///     The logik
        /// </summary>
        public List<Fahrlogik_Object> logik = new List<Fahrlogik_Object>();

        /// <summary>
        ///     The oldpos
        /// </summary>
        public List<Vector2> oldpos = new List<Vector2>();

        /// <summary>
        ///     The overreach
        /// </summary>
        public List<bool> overreach = new List<bool>();
        public List<bool> _overreach
        {
            get
            {
                return overreach;
            }
        }

        /// <summary>
        ///     The pos
        /// </summary>
        public List<Vector2> pos = new List<Vector2>();
        public List<Vector2> _pos
        {
            get
            {
                return pos;
            }
        }

        /// <summary>
        ///     The shooting power
        /// </summary>
        public float shootingPower;

        /// <summary>
        ///     The vehikle angle
        /// </summary>
        public List<float> vehikleAngle = new List<float>();
        public List<float> _vehikleAngle
        {
            get
            {
                return vehikleAngle;
            }
        }

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Spieler" /> class.
        /// </summary>
        public Spieler() // Initialisiert einen neuen Spieler
        {
            // Eigentlich total sinnlos   löschen???
            if (Spiel.TIMEOUT.Wert) MaxTimeout = Spiel.TIMEOUT_SEKUNDEN.Wert * 60; // Maximale Zeit zum feuern
            shootingPower = 2;
            if (Spiel.SCHUESSE.Wert) MaxSchuesse = 1; // Maximale anzahl an Schuessen
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     Gets or sets the current tank.
        /// </summary>
        /// <value>The current tank.</value>
        public int CurrentTank
        {
            get
            {
                int oldcurrent = _CurrentTank;
                if (_CurrentTank == -1 || _CurrentTank >= hp.Count)
                {
                    if (hp.Count >= 1)
                    {
                        if (_CurrentTank == -1)
                        {
                            _CurrentTank = 0;
                        }
                        else if (_CurrentTank >= hp.Count)
                        {
                            _CurrentTank = hp.Count - 1;
                        }
                    }
                }

                if (_CurrentTank >= hp.Count) _CurrentTank = -1;

                if (_CurrentTank > -1)
                {
                    Spiel Spiel2 = Game1.Spiel2;
                    // Waffe prüfen
                    int CurrentWeaponNow2 = CurrentWeapon;
                    int CurrentTankNow2 = _CurrentTank;
                    int CurrentWaeponOld = CurrentWeapon;
                    if (Fahrzeugdaten.ShootableAmmunition[KindofTank[CurrentTankNow2], CurrentWeaponNow2] <= 0)
                    {
                        int begin = 0;
                        CurrentWeaponNow2 = 0;
                        for (; Fahrzeugdaten.ShootableAmmunition[KindofTank[CurrentTankNow2], CurrentWeaponNow2] <= 0; )
                        {
                            CurrentWeaponNow2--;
                            if (CurrentWeaponNow2 < 0) CurrentWeaponNow2 = Waffendaten.Daten.Count() - 1;
                            if (CurrentWeaponNow2 == begin) break;
                        }

                        if (CurrentWeaponNow2 != begin)
                        {
                            // if (Client.isRunning) Client.Send("WAFFE " + CurrentWeaponNow2 + " " + CurrentPlayer);
                            //  if (Server.isRunning) Server.Send("WAFFE " + CurrentWeaponNow2 + " " + CurrentPlayer);
                        }
                        CurrentWeapon = CurrentWeaponNow2;
                    }

                    if (CurrentWaeponOld != CurrentWeaponNow2)
                    {
                        if (CurrentWeaponNow2 == 5) //&& !Spiel2.increaseairstrike
                        {
                            Spiel2.increaseairstrike = true;
                            shootingPower = pos[CurrentTankNow2].X;
                            if (!Spiel2.Moving_Map) Spiel2.SetzeFokus(new Vector2(shootingPower, pos[_CurrentTank].Y));
                        }
                        else
                        //  if (!Spiel2.increaseairstrike)
                        {
                            Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 0;
                            Spiel2.increaseairstrike = false;
                        }

                        if (Mod.SPIELERMENU_VISIBLE.Wert)
                        {
                            // Game1.Spielermenu.CurrentPlayerID = Spiel2.CurrentPlayer;
                            // Game1.Spielermenu.CurrentTankID = Spiel2.players[Spiel2.CurrentPlayer].CurrentTank;
                            // Game1.Spielermenu.switchButtons(Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon, 0);
                        }
                    }
                }
                return _CurrentTank;
            }
            set
            {
                if (value != _CurrentTank)
                    if (Mod.SPIELERMENU_VISIBLE.Wert)
                    Game1.Spielermenu.intrade = false;

                _CurrentTank = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     Draws the player.
        /// </summary>
        /// <param name="tankid">The tankid.</param>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="Angle">The angle.</param>
        /// <param name="vehikleAngle">The vehikle angle.</param>
        /// <param name="scaleP">The scale P.</param>
        /// <param name="scaleR">The scale R.</param>
        /// <param name="Position">The position.</param>
        /// <param name="Fenster">The fenster.</param>
        /// <param name="overreach">if set to <c>true</c> [overreach].</param>
        /// <param name="iscurrent">if set to <c>true</c> [iscurrent].</param>
        /// <param name="faktor">The faktor.</param>
        /// <param name="Transparenz">The transparenz.</param>
        public void DrawPlayer(int tankid, SpriteBatch spriteBatch, float Angle, float vehikleAngle, float scaleP,
            float scaleR, Vector2 Position, Vector2 Fenster, bool overreach, bool iscurrent, float faktor,
            float Transparenz, Color CurrentColor, bool Besitzer)
        {
            scaleP *= faktor;
            scaleR *= faktor;

            int b = tankid;
            Color r = Color.White * Transparenz;
            if (Effekte[b].GetVergiftet()) r = Color.Lime * Transparenz;
            if (Effekte[b].GetEingefroren()) r = Color.Aquamarine * Transparenz;

            Color r2 = Farbe * Transparenz * 0.5f;
            // r2 *= 1f;

            int xPos = (int)Position.X - (int)Fenster.X;
            int yPos = (int)Position.Y - (int)Fenster.Y;
            int Kindof = KindofTank[tankid];

            if (Kindof != 4)
            {
                int id2 = Kindof;
                Vector2 p = Help.RotatePositionOffset(new Vector2(xPos, yPos), vehikleAngle,
                    new Vector2((overreach ? Texturen.RohrPos[KindofTank[b]].X : -Texturen.RohrPos[KindofTank[b]].X),
                        Texturen.RohrPos[KindofTank[b]].Y) * faktor);
                spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(Texturen.panzerrohrindex[id2], p, null, r, Angle + vehikleAngle,
                    Texturen.CannonOrigin[id2][0], scaleR, overreach ? SpriteEffects.FlipVertically : SpriteEffects.None,
                    1); //
                spriteBatch.End();

                if (Mod.AKTUELLER_ON_TANK_VISIBLE.Wert)
                {
                    if (iscurrent)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Texturen.panzerrohrindex[id2], p, null, CurrentColor * Transparenz,
                            Angle + vehikleAngle, Texturen.CannonOrigin[id2][0], scaleR,
                            overreach ? SpriteEffects.FlipVertically : SpriteEffects.None, 1); //
                        spriteBatch.End();
                        //  spriteBatch.Draw(Texturen.panzerrohrumriss[id2], p, null, Color.DarkGoldenrod * Transparenz, Angle + vehikleAngle, Texturen.CannonOrigin[id2][0], scaleR, overreach ? SpriteEffects.FlipVertically : SpriteEffects.None, 1); //
                    }
                }
            }

            spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend);
            //Texturen.effect.CurrentTechnique.Passes[0].Apply();
            for (int i = 0; i < Texturen.Radpositionen[KindofTank[b]].Count(); i++)
            {
                Vector2 p = Help.RotatePositionOffset(new Vector2(xPos, yPos), vehikleAngle,
                    new Vector2(
                        (overreach
                            ? Texturen.Radpositionen[KindofTank[b]][i].X
                            : -Texturen.Radpositionen[KindofTank[b]][i].X), Texturen.Radpositionen[KindofTank[b]][i].Y) *
                    faktor);

                spriteBatch.Draw(Texturen.panzerindexreifen[Kindof], p, null, r, vehikleAngle,
                    new Vector2(Texturen.panzerindexreifen[Kindof].Width / 2, Texturen.panzerindexreifen[Kindof].Height / 2),
                    scaleP, SpriteEffects.None, 0);
            }

            spriteBatch.Draw(Texturen.panzerindex[Kindof], new Vector2(xPos, yPos), null, r, vehikleAngle,
                new Vector2(Texturen.panzerindex[Kindof].Width / 2, Texturen.panzerindex[Kindof].Height), scaleP,
                overreach ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

            spriteBatch.End();
            spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend);
            if (Mod.AKTUELLER_ON_TANK_VISIBLE.Wert)
            {
                if (iscurrent)
                {
                    spriteBatch.Draw(Texturen.panzerumriss[Kindof], new Vector2(xPos, yPos), null,
                        CurrentColor * Transparenz, vehikleAngle,
                        new Vector2(Texturen.panzerumriss[Kindof].Width / 2, Texturen.panzerumriss[Kindof].Height), scaleP,
                        overreach ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);

                    spriteBatch.Draw(Texturen.panzerindex[Kindof], new Vector2(xPos, yPos), null,
                        CurrentColor * Transparenz, vehikleAngle,
                        new Vector2(Texturen.panzerindex[Kindof].Width / 2, Texturen.panzerindex[Kindof].Height), scaleP,
                        overreach ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
                }
            }
            spriteBatch.End();

            if (Kindof == 4)
            {
                int id2 = Kindof;
                Vector2 p = Help.RotatePositionOffset(new Vector2(xPos, yPos), vehikleAngle,
                    new Vector2((overreach ? Texturen.RohrPos[Kindof].X * faktor : -Texturen.RohrPos[Kindof].X * faktor),
                        Texturen.RohrPos[Kindof].Y * faktor));
                spriteBatch.Begin(Game1.SpriteMode, BlendState.AlphaBlend);
                Texturen.effect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.Draw(Texturen.panzerrohrindex[id2], p, null, r, Angle + vehikleAngle,
                    Texturen.CannonOrigin[id2][0], scaleR, overreach ? SpriteEffects.FlipVertically : SpriteEffects.None,
                    1); //
                spriteBatch.End();

                if (Mod.AKTUELLER_ON_TANK_VISIBLE.Wert)
                {
                    if (iscurrent)
                    {
                        spriteBatch.Begin();
                        spriteBatch.Draw(Texturen.panzerrohrindex[id2], p, null, CurrentColor * Transparenz,
                            Angle + vehikleAngle, Texturen.CannonOrigin[id2][0], scaleR,
                            overreach ? SpriteEffects.FlipVertically : SpriteEffects.None, 1); //
                        spriteBatch.End();
                        //    spriteBatch.Draw(Texturen.panzerrohrumriss[id2], p, null, Color.DarkGoldenrod * Transparenz, Angle + vehikleAngle, Texturen.CannonOrigin[id2][0], scaleR, overreach ? SpriteEffects.FlipVertically : SpriteEffects.None, 1); //
                    }
                }
            }

            if (Besitzer)
            {
                spriteBatch.Begin();
                // Level zeichnen
                Vector2[] leveloffset =
                {
                    new Vector2(30, 25), new Vector2(22, 25), new Vector2(35, 32),
                    new Vector2(25, 27), new Vector2(0, 18), new Vector2(10, 20)
                };
                int id = Kindof;
                for (int f = 0; f < CurrentLv[b]; f++)
                {
                    Vector2 p = Help.RotatePositionOffset(new Vector2(xPos, yPos), vehikleAngle,
                        new Vector2((overreach ? leveloffset[id].X : -leveloffset[id].X + 8), leveloffset[id].Y - f * 4) *
                        faktor);
                    spriteBatch.DrawString(Texturen.font2, "^", p, Color.Gold * Transparenz, vehikleAngle, Vector2.Zero,
                        1.0f * faktor, SpriteEffects.None, 0);
                }

                // Spielerfarbe zeichnen
                Vector2[] spieleroffset =
                {
                    new Vector2(20, 25), new Vector2(12, 20), new Vector2(25, 22),
                    new Vector2(15, 22), new Vector2(0, 23), new Vector2(0, 20)
                };
                id = Kindof;
                Vector2 p2 = Help.RotatePositionOffset(new Vector2(xPos, yPos), vehikleAngle,
                    new Vector2((overreach ? spieleroffset[id].X : -spieleroffset[id].X + 8), spieleroffset[id].Y) *
                    faktor);
                spriteBatch.Draw(Texturen.Spielerkennzeichnung, p2, null, r2, vehikleAngle, Vector2.Zero, 0.3f * faktor,
                    SpriteEffects.None, 1);
                spriteBatch.End();
            }
        }

        public List<String> EditorSpeichern(int i)
        {
            var data = new List<String>();
            data.Add("[FAHRZEUG]");
            data.Add("hp=" + hp[i]);
            data.Add("ExpNow=" + ExpNow[i]);
            data.Add("CurrentLv=" + CurrentLv[i]);
            data.Add("Cooldown=" + Cooldown[i]);
            data.Add("isthere=" + isthere[i]);
            data.Add("KindofTank=" + KindofTank[i]);
            data.Add("oldpos=" + oldpos[i]);
            data.Add("overreach=" + overreach[i]);
            data.Add("Namen=" + Namen[i]);
            data.Add("vehikleAngle=" + vehikleAngle[i]);
            data.Add("Zielpos=" + Zielpos[i]);
            data.Add("[/FAHRZEUG]");
            return data;
        }

        /// <summary>
        ///     Deletes the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="explode">if set to <c>true</c> [explode].</param>
        /// <param name="Spiel2">The spiel2.</param>
        /// <param name="gameTime">The game time.</param>
        public void Entfernen(int i, bool explode, Spiel Spiel2, GameTime gameTime)
        {
            if (i >= hp.Count) return;

            if (explode)
                Karte.Explosion_einer_Waffe_zünden_ohne_schaden_sound(Spiel2.Spielfeld, gameTime, Spiel2, 1,
                    Sounds.fahrzeugzerstört, pos[i]);

            hp.RemoveAt(i);
            Angle.RemoveAt(i);
            vehikleAngle.RemoveAt(i);
            isthere.RemoveAt(i);
            overreach.RemoveAt(i);
            //freezed.RemoveAt(i);
            //    electrified.RemoveAt(i);
            Size.RemoveAt(i);
            SizeOfCannon.RemoveAt(i);
            Kenngroesse_Wert.Hinzufügen(pos[i], -Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[i]], 350, Anteil.Konstant, Wachstum.Konstant, false);
            pos.RemoveAt(i);
            oldpos.RemoveAt(i);
            KindofTank.RemoveAt(i);
            Namen.RemoveAt(i);
            ExpNow.RemoveAt(i);
            CurrentLv.RemoveAt(i);
            /*fuelRemains2[i] = 0;
            for (int b = i; b < fuelRemains2.Count() - 1; b++)
                fuelRemains2[b] = fuelRemains2[b + 1];*/

            for (int b = i; b < Munition.Count() - 1; b++)
                Munition[b] = Munition[b+1];

            Cooldown.RemoveAt(i);
            Kollision.RemoveAt(i);
            Zerstörung.RemoveAt(i);
            //   poisoned.RemoveAt(i);
            Rucksack.RemoveAt(i);
            Effekte.RemoveAt(i);
            Zielpos.RemoveAt(i);
            logik.RemoveAt(i);
            //if (CurrentTank >= hp.Count) CurrentTank = 0;
            //   if (CurrentTank >= hp.Count) CurrentTank = -1;
        }

        /// <summary>
        ///     Generate_s the credits.
        /// </summary>
        /// <param name="Haeuser">The haeuser.</param>
        /// <param name="id">The id.</param>
        public float Generate_Credits(Haus Haeuser, int id)
        {
            if (Client.isRunning) return 0;
            // Konstanten Wert addieren
            int Const = 125;
            float Cred = Const;

            // Für besetzte Gebäude addieren
            int anzahl = 0;

            for (int i = 0; i < Haeuser.Lebenspunkte.Count; i++)
            {
                if (Haeuser.BesitzerPunkte[i] >= Allgemein.MinBesitzerPunkte && Haeuser.Lebenspunkte[i] > 0 &&
                    Haeuser.Besitzer[i] == id && Haeuser.HausTyp[i] != (int)Gebäudeart.FABRIK &&
                    Haeuser.HausTyp[i] != (int)Gebäudeart.WAFFENHÄNDLER)
                {
                    anzahl++;
                }
            }

            if (anzahl > 0)
            {
                anzahl++; // korrektur, damit log1.1 (1) = 0 nicht vorkommt
                Cred += (int)Math.Log(anzahl, 1.1f) * 25.0f;
            }

            return Cred;
            //   if (Server.isRunning) Server.Send("CREDITS " + id + " " + Credits);
        }

        public float Generate_ActionPoints(int id)
        {
            if (Client.isRunning) return 0;
            // Konstanten Wert addieren

            float Points = Spiel.ACTION_POINTS_INCREASE_CONST.Wert;

            return Points;
        }


        /// <summary>
        ///     Gets the panzer ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="Liste">The liste.</param>
        /// <returns>System.Int32.</returns>
        public int getPanzerID(int id, List<int> Liste)
        {
            for (int i = 0; i < Liste.Count; i++)
            {
                if (Liste[i] == id) return i;
            }
            return -1;
        }

        public int GibArtillerieAnzahl()
        {
            int anzahl = 0;

            for (int i = 0; i < KindofTank.Count; i++)
            {
                if (hp[i] > 0 && KindofTank[i] == Fahrzeugdaten.ARTILLERIE)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        public int GibBaufahrzeugeAnzahl()
        {
            int anzahl = 0;

            for (int i = 0; i < KindofTank.Count; i++)
            {
                if (hp[i] > 0 && KindofTank[i] == Fahrzeugdaten.BAUFAHRZEUG)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        public int GibBunkerAnzahl(Bunker Bunkeranlagen, int id)
        {
            int anzahl = 0;

            for (int i = 0; i < Bunkeranlagen.Position.Count; i++)
            {
                if (Bunkeranlagen.Lebenspunkte[i] > 0 && Bunkeranlagen.Besitzer[i] == id)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        public int GibFabrikenAnzahl(Haus Haeuser, int id)
        {
            int anzahl = 0;

            for (int i = 0; i < Haeuser.Lebenspunkte.Count; i++)
            {
                if (Haeuser.Lebenspunkte[i] > 0 && Haeuser.Besitzer[i] == id &&
                    Haeuser.HausTyp[i] == (int)Gebäudeart.FABRIK)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        public int GibGeschützAnzahl()
        {
            int anzahl = 0;

            for (int i = 0; i < KindofTank.Count; i++)
            {
                if (hp[i] > 0 && KindofTank[i] == Fahrzeugdaten.GESCHÜTZ)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        public int GibGeschütz2Anzahl()
        {
            int anzahl = 0;

            for (int i = 0; i < KindofTank.Count; i++)
            {
                if (hp[i] > 0 && KindofTank[i] == Fahrzeugdaten.GESCHÜTZ2)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        public int GibHändlerAnzahl(Haus Haeuser, int id)
        {
            int anzahl = 0;

            for (int i = 0; i < Haeuser.Lebenspunkte.Count; i++)
            {
                if (Haeuser.Lebenspunkte[i] > 0 && Haeuser.Besitzer[i] == id &&
                    Haeuser.HausTyp[i] == (int)Gebäudeart.WAFFENHÄNDLER)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        public int GibHäuserAnzahl(Haus Haeuser, int id)
        {
            int anzahl = 0;

            for (int i = 0; i < Haeuser.Lebenspunkte.Count; i++)
            {
                if (Haeuser.Lebenspunkte[i] > 0 && Haeuser.Besitzer[i] == id &&
                    Haeuser.HausTyp[i] != (int)Gebäudeart.FABRIK && Haeuser.HausTyp[i] != (int)Gebäudeart.WAFFENHÄNDLER)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        /// <summary>
        ///     Gets the left tunnel pos.
        /// </summary>
        /// <returns>Vector2.</returns>
        public Vector2 GibLinkenTunnel()
        {
            int init = GibTunnelAnAktuellerPanzerposition();
            if (init == -1) return new Vector2(0, 9999);
            int min = init;
            float abstand = 999999;
            for (int i = 0; i < TunnelAnlage.Count; i++)
            {
                if (i != init && TunnelAnlage[i].Position.X < TunnelAnlage[init].Position.X)
                {
                    float ab = Help.Abstand(TunnelAnlage[init].Position, TunnelAnlage[i].Position);
                    if (ab < abstand)
                    {
                        min = i;
                        abstand = ab;
                    }
                }
            }
            return TunnelAnlage[min].Position + new Vector2(TunnelAnlage[min].Bild.Width / 2, 0);
        }

        public int GibPanzer()
        {
            int anzahl = 0;

            for (int i = 0; i < KindofTank.Count; i++)
            {
                if (hp[i] > 0 && KindofTank[i] == Fahrzeugdaten.PANZER)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        /// <summary>
        ///     Gets the right tunnel pos.
        /// </summary>
        /// <returns>Vector2.</returns>
        public Vector2 GibRechtenTunnel()
        {
            int init = GibTunnelAnAktuellerPanzerposition();
            if (init == -1) return new Vector2(0, 9999);
            int min = init;
            float abstand = 999999;
            for (int i = 0; i < TunnelAnlage.Count; i++)
            {
                if (i != init && TunnelAnlage[i].Position.X > TunnelAnlage[init].Position.X)
                {
                    float ab = Help.Abstand(TunnelAnlage[init].Position, TunnelAnlage[i].Position);
                    if (ab < abstand)
                    {
                        min = i;
                        abstand = ab;
                    }
                }
            }
            return TunnelAnlage[min].Position + new Vector2(TunnelAnlage[min].Bild.Width / 2, 0);
        }

        public int GibScout()
        {
            int anzahl = 0;

            for (int i = 0; i < KindofTank.Count; i++)
            {
                if (hp[i] > 0 && KindofTank[i] == Fahrzeugdaten.SCOUT)
                {
                    anzahl++;
                }
            }
            return anzahl;
        }

        /// <summary>
        ///     Gets the tunnel.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GibTunnelAnAktuellerPanzerposition()
        {
            List<Tunnel> tunnel = TunnelAnlage;
            if (CurrentTank >= 0)
                for (int b = 0; b < TunnelAnlage.Count; b++)
                {
                    if (tunnel[b].PrüfeObKollision(pos[CurrentTank] + new Vector2(0, -5)))
                    {
                        return b;
                    }
                }
            return -1;
        }

        /// <summary>
        ///     Load_panzerdatens the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
        public void LadeKollisionsObjekt(int i)
        {
            Kollision.Add(null);
            int id = KindofTank[i];
            Kollision[i] = new KollisionsObjekt(Texturen.panzerindex[id], Texturen.panzerindex[id].Width,
                Texturen.panzerindex[id].Height, Fahrzeugdaten.SCALEP.Wert[id], true, true, true,
                new Vector2((Texturen.panzerindex[id].Width * Fahrzeugdaten.SCALEP.Wert[id]) / 2, 0));
            MaxPixel.Add(Help.GetPixelAnzahl(Texturen.panzerindex[id]));
        }

        public int AddPanzer(int typ, float _angle, bool _overreach, Vector2 _pos)
        {
            // Spielername holen

            if (Spiel.names == null || Spiel.names.Count == 0)
            {
                if (File.Exists("Content\\Konfiguration\\Namen.txt"))
                {
                    Spiel.names = new List<string>();

                    var datei = new ReaderStream.ReaderStream("Content\\Konfiguration\\Namen.txt");

                    while (!datei.EndOfStream)
                    {
                        Spiel.names.Add(datei.ReadLine());
                    }

                    datei.Close();
                }
            }

            if (pos.Count >= Munition.Count()) return -1;

            if (Spiel.names == null)
            {
                Namen.Add("Joe");
            }
            else
            {
                int q = Help.rnd.Next(0, Spiel.names.Count);
                Namen.Add(Spiel.names[q]);
                Spiel.names.RemoveAt(q);
            }

            int di = typ;
            hp.Add(Fahrzeugdaten._MAXHP.Wert[di]);
            int b = hp.Count - 1;
            Angle.Add(_angle);
            vehikleAngle.Add(0);
            isthere.Add(true);
            overreach.Add(_overreach);
            Size.Add(Fahrzeugdaten.SCALEP.Wert[di]);
            SizeOfCannon.Add(Fahrzeugdaten.SCALER.Wert[di]);
            pos.Add(_pos);
            if (Kenngroesse_Wert==null)
                Kenngroesse_Wert = new Kenngroesse(Spiel.Kartenbreite, Spiel.Kartenhoehe, 100, 100, 0);
            Kenngroesse_Wert.Hinzufügen(pos[b], Fahrzeugdaten._PANZERWERTE.Wert[di], 350, Anteil.Konstant, Wachstum.Konstant, false);
            oldpos.Add(_pos);
            KindofTank.Add(di);
            ExpNow.Add(0);
            CurrentLv.Add(0);
            Cooldown.Add(0);
            Effekte.Add(new EffectPacket());
            Zielpos.Add(new Vector2(0, -9999));
            logik.Add(new Fahrlogik_Object(5));
            for (int d = 0; d < Texturen.Radpositionen[di].Count(); d++)
            {
                logik[logik.Count - 1].Motoren.AddRad(Texturen.Radpositionen[di][d],
                    Texturen.panzerindexreifen[di]);
            }

            Munition[b] = new List<int>();
            Rucksack.Add(new Inventar(15000, Munition[b], 1000));

            LadeKollisionsObjekt(b);
            LadeZerstörungsObjekt(b);

            return Effekte.Count - 1;
        }

        public bool LadenFahrzeug(List<String> Text, int i, ContentManager Content)
        {
            List<String> Text2 = TextLaden.ErmittleBereich(Text, "FAHRZEUG");
            if (Text2.Count == 0) return false;

            int altid = i;
            if (hp.Count <= altid) altid = -1;

            if (altid == -1)
            {
                // hinzufügen
                i = AddPanzer(0, 0, false, Vector2.Zero);
                if (i == -1) return false;
            }

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            hp[i] = TextLaden.LadeInt(Liste, "hp", hp[i]);
            ExpNow[i] = TextLaden.LadeInt(Liste, "ExpNow", ExpNow[i]);
            CurrentLv[i] = TextLaden.LadeInt(Liste, "CurrentLv", CurrentLv[i]);
            Cooldown[i] = TextLaden.LadeInt(Liste, "Cooldown", Cooldown[i]);
            isthere[i] = TextLaden.LadeBool(Liste, "isthere", isthere[i]);
            KindofTank[i] = TextLaden.LadeInt(Liste, "KindofTank", KindofTank[i]);
            //   MaxPixel[i] = TextLaden.LadeInt(Liste, "MaxPixel", MaxPixel[i]);
            oldpos[i] = TextLaden.LadeVector2(Liste, "oldpos", oldpos[i]);
            pos[i] = TextLaden.LadeVector2(Liste, "pos", pos[i]);
            overreach[i] = TextLaden.LadeBool(Liste, "overreach", overreach[i]);
            Namen[i] = TextLaden.LadeString(Liste, "Namen", Namen[i]);
            // Size[i] = TextLaden.LadeInt(Liste, "Size", Size[i]);
            //SizeOfCannon[i] = TextLaden.LadeInt(Liste, "SizeOfCannon", SizeOfCannon[i]);
            vehikleAngle[i] = TextLaden.LadeFloat(Liste, "vehikleAngle", vehikleAngle[i]);
            Angle[i] = TextLaden.LadeFloat(Liste, "Angle", Angle[i]);
            Zielpos[i] = TextLaden.LadeVector2(Liste, "Zielpos", Zielpos[i]);

            Effekte[i] = EffectPacket.Laden(Text2, Content);

            int id = KindofTank[i];
            if (Kenngroesse_Wert == null)
                Kenngroesse_Wert = new Kenngroesse(Spiel.Kartenbreite, Spiel.Kartenhoehe, 100, 100, 0);
            Kenngroesse_Wert.Hinzufügen(pos[i], Fahrzeugdaten._PANZERWERTE.Wert[id], 350, Anteil.Konstant, Wachstum.Konstant, false);

            Kollision[i] = new KollisionsObjekt(Texturen.panzerindex[i], Texturen.panzerindex[i].Width,
                Texturen.panzerindex[i].Height, Fahrzeugdaten.SCALEP.Wert[id], true, true, true, Vector2.Zero);
            Zerstörung[i] = new ZerstörungsObjekt(Texturen.panzerindex[id].Width, Texturen.panzerindex[id].Height,
                Fahrzeugdaten.SCALEP.Wert[id], true, true, true);


            for (int d = 0; d < Texturen.Radpositionen[id].Count(); d++)
            {
                logik[i].Motoren.AddRad(Texturen.Radpositionen[id][d],
                    Texturen.panzerindexreifen[id]);
            }

            Kollision[i] = KollisionsObjekt.Laden(Text2, altid == -1 ? null : Kollision[id]);

            Zerstörung[i] = ZerstörungsObjekt.Laden(Text2, altid == -1 ? null : Zerstörung[id]);

            //if (Rucksack[i] == null) // ?? macht das sinn ???
            Rucksack[i] = Inventar.Laden(Text2, Content, altid == -1 ? new Inventar() : Rucksack[i]);
            Munition[i] = Rucksack[i].Munition;

            return true;
        }

        /// <summary>
        ///     Load2s the specified i.
        /// </summary>
        /// <param name="i">The i.</param>
        public void LadeZerstörungsObjekt(int i)
        {
            Zerstörung.Add(null);
            int id = KindofTank[i];
            Zerstörung[i] = new ZerstörungsObjekt(Texturen.panzerindex[id].Width, Texturen.panzerindex[id].Height,
                Fahrzeugdaten.SCALEP.Wert[id], true, true, true);
        }

        /// <summary>
        ///     Linkses the specified spielfeld.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="id">The id.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Links(List<UInt16>[] Spielfeld, int id) // Linksbewegung
        {
            if (Client.isRunning) return false;
            if (id < 0 || id >= KindofTank.Count) return false;
            if (GLOBAL_FUEL.Wert && fuelRemains < Fahrzeugdaten.VERBRAUCH.Wert[KindofTank[id]]) return false;
            Vector2 OldPos = pos[id];

            overreach[id] = false;
            float ang = MathHelper.ToDegrees(Angle[id]);
            if (ang > 90) Angle[id] = MathHelper.ToRadians(90 - (ang - 90));

            if (ImTunnel && GibTunnelAnAktuellerPanzerposition() >= 0 && ActionPoints >= Allgemein.TunnelAPKosten)
            {
                Vector2 po = GibLinkenTunnel();
                if (po !=
                    TunnelAnlage[GibTunnelAnAktuellerPanzerposition()].Position +
                    new Vector2(TunnelAnlage[GibTunnelAnAktuellerPanzerposition()].Bild.Width / 2, 0))
                {
                    Kenngroesse_Wert.Hinzufügen(pos[id], -Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);
                    pos[id] = po;
                    Kenngroesse_Wert.Hinzufügen(pos[id], Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);
                    ActionPoints -= Allgemein.TunnelAPKosten;
                    return true;
                }
                return false;
            }

            if (vehikleAngle[id] >= MathHelper.ToRadians(Fahrzeugdaten.WINKEL.Wert[KindofTank[id]])) return false;
            if (pos[id].Y - 40 >
                Kartenformat.BottomOf((int)pos[id].X - Fahrzeugdaten.FAHRM.Wert[KindofTank[id]] - 1, (int)pos[id].Y))
                return false;
            var x1 =
                (int)(oldpos[id].X - Effekte[id].GibArbeitsbereich(Fahrzeugdaten.ARBEITSBEREICH.Wert[KindofTank[id]]));
            if (pos[id].X < x1 && Fahrzeugdaten.ARBEITSBEREICHE)
            {
                return false;
            }

            float move = Effekte[id].GibGeschwV(Fahrzeugdaten.GESCHWV.Wert[KindofTank[id]]);
            // if (overreach[id]) move = Effekte[id].GibGeschwR(Fahrzeugdaten.GESCHWR.Wert[KindofTank[id]]);
            if (CurrentWeapon == 8) move /= 4;
            if (move <= 0) return false;
            var g = new Vector2(-move, 0);
            if (Kartenformat.GetMaterial(pos[id].X, pos[id].Y - 2) == Karte.SUMPF) g /= 3;
            if (Kartenformat.GetMaterial(pos[id].X, pos[id].Y - 2) == Karte.WASSER) g /= 1.5f;
            Kenngroesse_Wert.Hinzufügen(pos[id], -Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);
            pos[id] += g;
            pos[id] = new Vector2(Spiel.Position(pos[id].X), pos[id].Y);
            Kenngroesse_Wert.Hinzufügen(pos[id], Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);

            //if (!overreach[id])
            {
                if (GLOBAL_FUEL.Wert)
                    fuelRemains -= Effekte[id].GibTreibstoffverbrauch(Fahrzeugdaten.VERBRAUCH.Wert[KindofTank[id]]);
                if (PLAYER_FUEL.Wert)
                    Rucksack[id].Treibstoff -=
                        Effekte[id].GibTreibstoffverbrauch(Fahrzeugdaten.VERBRAUCH.Wert[KindofTank[id]]);
            }
            /* else*
             {
                 if (Spieler.GLOBAL_FUEL.Wert) fuelRemains -= Effekte[id].GibTreibstoffverbrauch((float)((2.0f - Fahrzeugdaten.GESCHWR.Wert[KindofTank[id]]) * 0.75f));
                 if (Spieler.PLAYER_FUEL.Wert) Rucksack[id].Treibstoff -= Effekte[id].GibTreibstoffverbrauch((float)((2.0f - Fahrzeugdaten.GESCHWR.Wert[KindofTank[id]]) * 0.75f));
             }*/

            return true;
        }

        /// <summary>
        ///     Ordnes the eigene panzer anhand der karte.
        /// </summary>
        /// <returns>List{System.Int32}.</returns>
        public List<int> OrdneEigenePanzerAnhandDerKarte()
        {
            var result = new List<int>();
            for (int i = 0; i < pos.Count; i++)
            {
                for (int b = 0; b <= result.Count; b++)
                {
                    if (b == result.Count)
                    {
                        result.Add(i);
                        break;
                    }
                    if (pos[i].X < pos[result[b]].X)
                    {
                        result.Insert(b, i);
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        ///     Determines whether the specified i is collision.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="Incoming_Position">The incoming_ position.</param>
        /// <returns><c>true</c> if the specified i is collision; otherwise, <c>false</c>.</returns>
        public bool PrüfeObKollision(int i, Vector2 Incoming_Position)
        {
            if (Kollision[i] == null) return false;
            return Kollision[i].collision(Incoming_Position, pos[i], vehikleAngle[i], overreach[i]);
        }

        /// <summary>
        ///     Determines whether the specified i is explode.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="Explosion">The explosion.</param>
        /// <param name="Energie">The energie.</param>
        /// <returns>System.Int32.</returns>
        public int PrüfeObTreffer(int i, Vector2 Explosion, int Energie)
        {
            if (Zerstörung[i] == null) return 0;
            int id = KindofTank[i];
            var temp = new Color[Texturen.panzerindex[id].Width * Texturen.panzerindex[id].Height];
            Texturen.panzerindex[id].GetData(temp);
            var tmp = new Texture2D(Texturen.panzerindex[id].GraphicsDevice, Texturen.panzerindex[id].Width,
                Texturen.panzerindex[id].Height);
            tmp.SetData(temp);
            return Zerstörung[i].BerechneZerstörung(tmp, Explosion, Energie, pos[i], overreach[i], vehikleAngle[i]);
        }

        /// <summary>
        ///     Rechtses the specified spielfeld.
        /// </summary>
        /// <param name="Spielfeld">The spielfeld.</param>
        /// <param name="id">The id.</param>
        /// <param name="Fenster">The fenster.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        public bool Rechts(List<UInt16>[] Spielfeld, int id, Vector2 Fenster)
        {
            // Rechtsbewegung
            if (Client.isRunning) return false;
            if (id < 0 || id >= KindofTank.Count) return false;
            if (GLOBAL_FUEL.Wert && fuelRemains < Fahrzeugdaten.VERBRAUCH.Wert[KindofTank[id]]) return false;
            Vector2 OldPos = pos[id];

            overreach[id] = true;
            float ang = MathHelper.ToDegrees(Angle[id]);
            if (ang < 90) Angle[id] = MathHelper.ToRadians((90 - ang) + 90);

            if (ImTunnel && GibTunnelAnAktuellerPanzerposition() >= 0 && ActionPoints >= Allgemein.TunnelAPKosten)
            {
                Vector2 po = GibRechtenTunnel();
                if (po !=
                    TunnelAnlage[GibTunnelAnAktuellerPanzerposition()].Position +
                    new Vector2(TunnelAnlage[GibTunnelAnAktuellerPanzerposition()].Bild.Width / 2, 0))
                {
                    Kenngroesse_Wert.Hinzufügen(pos[id], -Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);
                    pos[id] = po;
                    Kenngroesse_Wert.Hinzufügen(pos[id], Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);
                    ActionPoints -= Allgemein.TunnelAPKosten;
                    return true;
                }
                return false;
            }

            if (vehikleAngle[id] <= MathHelper.ToRadians(-Fahrzeugdaten.WINKEL.Wert[KindofTank[id]])) return false;
            if (pos[id].Y - 40 >
                Kartenformat.BottomOf((int)pos[id].X + Fahrzeugdaten.FAHRM.Wert[KindofTank[id]] + 1, (int)pos[id].Y))
                return false;

            var x2 =
                (int)(oldpos[id].X + Effekte[id].GibArbeitsbereich(Fahrzeugdaten.ARBEITSBEREICH.Wert[KindofTank[id]]));
            if (pos[id].X > x2 && Fahrzeugdaten.ARBEITSBEREICHE)
            {
                return false;
            }

            float move = Effekte[id].GibGeschwV(Fahrzeugdaten.GESCHWV.Wert[KindofTank[id]]);
            // if (!overreach[id]) move = Effekte[id].GibGeschwR(Fahrzeugdaten.GESCHWR.Wert[KindofTank[id]]);

            if (CurrentWeapon == 8) move /= 4;
            if (move <= 0) return false;
            var g = new Vector2(move, 0);
            if (Kartenformat.GetMaterial(pos[id].X, pos[id].Y - 2) == Karte.SUMPF) g /= 3;
            if (Kartenformat.GetMaterial(pos[id].X, pos[id].Y - 2) == Karte.WASSER) g /= 1.5f;
            Kenngroesse_Wert.Hinzufügen(pos[id], -Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);
            pos[id] += g;
            pos[id] = new Vector2(Spiel.Position(pos[id].X), pos[id].Y);
            Kenngroesse_Wert.Hinzufügen(pos[id], Fahrzeugdaten._PANZERWERTE.Wert[KindofTank[id]], 350, Anteil.Konstant, Wachstum.Konstant, false);
            

            /* if (!overreach[id])
             {
                 if (Spieler.GLOBAL_FUEL.Wert) fuelRemains -= Effekte[id].GibTreibstoffverbrauch((float)((2.0f - (Fahrzeugdaten.GESCHWR.Wert[KindofTank[id]]) * 0.75f)));
                 if (Spieler.PLAYER_FUEL.Wert) Rucksack[id].Treibstoff -= Effekte[id].GibTreibstoffverbrauch((float)((2.0f - Fahrzeugdaten.GESCHWR.Wert[KindofTank[id]]) * 0.75f));
             }
             else*/
            {
                if (GLOBAL_FUEL.Wert)
                    fuelRemains -= Effekte[id].GibTreibstoffverbrauch(Fahrzeugdaten.VERBRAUCH.Wert[KindofTank[id]]);
                if (PLAYER_FUEL.Wert)
                    Rucksack[id].Treibstoff -=
                        Effekte[id].GibTreibstoffverbrauch(Fahrzeugdaten.VERBRAUCH.Wert[KindofTank[id]]);
            }

            return true;
        }

        /// <summary>
        ///     Rohr_s the links.
        /// </summary>
        /// <param name="id">The id.</param>
        public bool Rohr_Links(int id) // Rohr nach Links
        {
            if (Client.isRunning) return false;
            if (id < 0 || id >= KindofTank.Count) return false;
            int typ = KindofTank[id];
            if (Effekte[id].GetElektrisiert()) return false;
            Angle[id] -= 0.01f;

            if (Angle[id] < Fahrzeugdaten.MAXANGLEB.Wert[typ] && overreach[id])
            {
                Angle[id] = Fahrzeugdaten.MAXANGLEB.Wert[typ];
                return false;
                // overreach[id] = false;
                //Angle[id] = Tankdata.MaxAngleA[typ];
            }

            if (Angle[id] < fauxdown)
            {
                Angle[id] = fauxdown;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Rohr_s the rechts.
        /// </summary>
        /// <param name="id">The id.</param>
        public bool Rohr_Rechts(int id) // Rohr nach Rechts
        {
            if (Client.isRunning) return false;
            if (id < 0 || id >= KindofTank.Count) return false;
            int typ = KindofTank[id];
            if (Effekte[id].GetElektrisiert()) return false;
            Angle[id] += 0.01f;

            if (Angle[id] > Fahrzeugdaten.MAXANGLEA.Wert[typ] && !overreach[id])
            {
                Angle[id] = Fahrzeugdaten.MAXANGLEA.Wert[typ];
                // overreach[id] = true;
                // Angle[id] = Tankdata.MaxAngleB[KindofTank[id]];
                return false;
            }

            if (Angle[id] > fauxup)
            {
                Angle[id] = fauxup;
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Rohrspitzes the specified c.
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns>Vector2.</returns>
        public Vector2 Rohrspitze(int c)
        {
            int kind = KindofTank[c];
            int welcher = overreach[c] ? 1 : 0;
            Vector2 posi = pos[c] +
                           new Vector2(welcher == 1 ? -Texturen.RohrPos[kind].X : Texturen.RohrPos[kind].X,
                               -Texturen.RohrPos[kind].Y);
            float temp2 = Texturen.CannonOrigin[kind][0].Y / 2;

            Vector2 p = Help.RotatePositionOffset(posi, Angle[c],
                new Vector2(
                    Texturen.panzerrohrindex[kind].Width -
                    (Texturen.panzerrohrindex[kind].Width - Texturen.CannonOrigin[kind][0].X),
                    welcher == 0 ? temp2 : -temp2));
            p = Help.RotatePosition(pos[c], vehikleAngle[c], p);
            return p;
        }

        public static Spieler Laden(Spiel Spiel2, List<String> Text, Spieler Default)
        {
            Spieler temp = Default;

            if (temp == null)
            {
                temp = new Spieler();
            }

            List<String> Text2 = TextLaden.ErmittleBereich(Text, "SPIELER");

            Dictionary<String, String> Liste = TextLaden.CreateDictionary(Text2);
            temp.id = TextLaden.LadeInt(Liste, "id", temp.id);
            temp.ActionPoints = TextLaden.LadeInt(Liste, "ActionPoints", temp.ActionPoints);
            temp.Credits = TextLaden.LadeFloat(Liste, "Credits", temp.Credits);
            temp.CurrentWeapon = TextLaden.LadeInt(Liste, "CurrentWeapon", temp.CurrentWeapon);
            temp.Farbe = TextLaden.LadeColor(Liste, "Farbe", temp.Farbe);
            temp.fuelRemains = TextLaden.LadeFloat(Liste, "fuelRemains", temp.fuelRemains);
            temp.ImTunnel = TextLaden.LadeBool(Liste, "ImTunnel", temp.ImTunnel);
            temp.MaxSchuesse = TextLaden.LadeInt(Liste, "MaxSchuesse", temp.MaxSchuesse);
            temp.MaxTimeout = TextLaden.LadeInt(Liste, "MaxTimeout", temp.MaxTimeout);
            temp.shootingPower = TextLaden.LadeFloat(Liste, "shootingPower", temp.shootingPower);
            temp._CurrentTank = TextLaden.LadeInt(Liste, "_CurrentTank", temp._CurrentTank);

            bool result=true;
            int i = 0;
            for (; i < temp.hp.Count; i++)
            {
                temp.Entfernen(i, false, Spiel2, new GameTime());
            }

            i = 0;
            do
            {
                result = temp.LadenFahrzeug(Text2, i, Game1.ContentAll);
                i++;
            } while (result);



            // Notizen laden
            for (int b = 0; b < temp.Notiz.pos.Count; b++)
                temp.Notiz.delNotiz(0);

            int anz = Text2.Count;
            do
            {
                anz = Text2.Count;
                temp.Notiz.Laden(Text2, -1, Game1.device, Game1.ContentAll);
            } while (anz != Text2.Count);


            // Minen laden
            temp.Minen.Clear();
            anz = Text2.Count;
            i = 0;
            do
            {
                anz = Text2.Count;
                temp.Minen.Add(Mine.Laden(Text2,null, i));
                i++;
            } while (anz != Text2.Count);


            // Tunnel laden
            temp.TunnelAnlage.Clear();
            anz = Text2.Count;
            i = 0;
            do
            {
                anz = Text2.Count;
                temp.TunnelAnlage.Add(Tunnel.Laden(Text2, null));
                i++;
            } while (anz != Text2.Count);

            return temp;
        }

        public List<String> Speichern()
        {
            var data = new List<String>();
            data.Add("[SPIELER]");
            data.Add("id=" + id);
            data.Add("ActionPoints=" + ActionPoints);
            data.Add("Credits=" + Credits);
            data.Add("CurrentWeapon=" + CurrentWeapon);
            data.Add("Farbe=" + Karte.ToHex(Farbe,true));
            data.Add("fuelRemains=" + fuelRemains);
            data.Add("ImTunnel=" + ImTunnel);
            data.Add("MaxSchuesse=" + MaxSchuesse);
            data.Add("MaxTimeout=" + MaxTimeout);
            data.Add("shootingPower=" + shootingPower);
            data.Add("_CurrentTank=" + _CurrentTank);
            for (int i = 0; i < hp.Count; i++)
            {
                data.Add("[FAHRZEUG]");
                data.Add("hp=" + hp[i]);
                data.Add("ExpNow=" + ExpNow[i]);
                data.Add("CurrentLv=" + CurrentLv[i]);
                data.Add("Cooldown=" + Cooldown[i]);
                data.Add("isthere=" + isthere[i]);
                data.Add("KindofTank=" + KindofTank[i]);
                data.Add("MaxPixel=" + MaxPixel[i]);
                data.Add("oldpos=" + oldpos[i]);
                data.Add("pos=" + pos[i]);
                data.Add("overreach=" + overreach[i]);
                data.Add("Namen=" + Namen[i]);
                data.Add("Size=" + Size[i]);
                data.Add("SizeOfCannon=" + SizeOfCannon[i]);
                data.Add("vehikleAngle=" + vehikleAngle[i]);
                data.Add("Angle=" + Angle[i]);
                data.Add("Zielpos=" + Zielpos[i]);
                data.AddRange(Effekte[i].Speichern());
                data.AddRange(Kollision[i].Speichern());
                data.AddRange(Zerstörung[i].Speichern());
                data.AddRange(Rucksack[i].Speichern());
                data.Add("[/FAHRZEUG]");
            }

            data.AddRange(Notiz.Speichern());

            for (int i = 0; i < Minen.Count; i++)
            {
                data.AddRange(Minen[i].Speichern());
            }

            for (int i = 0; i < TunnelAnlage.Count; i++)
            {
                data.AddRange(TunnelAnlage[i].Speichern());
            }

            data.Add("[/SPIELER]");
            return data;
        }

        /// <summary>
        ///     Tunnel_erstellens the specified pos.
        /// </summary>
        /// <param name="pos">The pos.</param>
        public void TunnelErstellen(Vector2 pos)
        {
            TunnelAnlage.Add(new Tunnel(pos));
        }

        #endregion Methods
    }
}