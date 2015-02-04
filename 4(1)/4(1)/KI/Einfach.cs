﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _4_1_;
using targeting;

namespace _KI
{
    public class Einfach : KI, IKI
    {
        bool richtung = false;
        int bew = 0;
        int bewl = 0;
        override public void Rechne(ISpiel Spiel, ISpieler Spieler, ISpieler[] Gegner, Vector2 Fenster)
        {
            int current = Spieler.CurrentTank;

            if (bewl < 1000)
            {
                Spieler.Links(Spiel._Spielfeld, current);
                bewl++;
            }

            float angle = Spieler._Angle[current]; 
            bew++;

            if (!richtung)
            {
                Spieler.Rohr_Links(Spieler.CurrentTank);
                if (bew == 40) { richtung = true; bew = 0; }
            }
            else 
                if (richtung)
            {
                Spieler.Rohr_Rechts(Spieler.CurrentTank);
                if (bew == 40) { richtung = false; bew = 0; }
            }

            // finde Gegner
            bool found=false;
            Vector2 Ziel = Vector2.Zero;
            for (int i = 0; i < Gegner.Length; i++)
            {
                if (found)break;
                for (int b = 0; b < Gegner[i]._pos.Count; b++)
            {
                if (Help.Abstand(Gegner[i]._pos[b],Spieler._pos[current])<=1000){
                    Ziel = Gegner[i]._pos[b];
                    found=true;
                    break;
                }
                }
            }

            // targeting
        if (found){
            double v0 = 0, t = 0;
            Vector2 Position = Spieler._pos[current];
            float vehikleAngle = Spieler._vehikleAngle[current];
            //(Waffen.gravity / 10.0f).Y
            targeting.Target.GetPower(angle, -(Ziel.X - Spieler.Rohrspitze(current).X), (Ziel.Y - Spieler.Rohrspitze(current).Y), 0, (Waffen.gravity / 10.0f).Y, !Spieler._overreach[current], ref t, ref v0);
            if (t == 0) v0 = 100;
            if (v0 > 0)
            {
                Vector2 a = Position - Fenster;
                a.Y -=
                    (float)
                        Math.Sin(
                            angle + vehikleAngle) * 75 + 25;
                a.X -=
                    (float)
                        Math.Cos(
                            angle + vehikleAngle) * 75;

                var up = new Vector2(0, -1);
                Matrix rotMatrix =
                    Matrix.CreateRotationZ(
                        angle + vehikleAngle - MathHelper.PiOver2);
                Vector2 c = Vector2.Transform(up, rotMatrix);
                c *= (float) (v0 / (float)Math.Log(v0, Math.E));

                Game1.getBahn((Waffen.gravity / 10.0f), c, Spieler.Rohrspitze(current) - Fenster, 500, 1000, Game1.spriteBatch);

                Game1.Meldungen.addMessage(v0.ToString());
            }
        }
        }

        public Einfach()
        {
            Name = "Till";
        }
    }
}
