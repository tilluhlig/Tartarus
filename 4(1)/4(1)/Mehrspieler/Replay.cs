using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _4_1_
{
    public static class Replay
    {
        public static Var<bool> REPLAY_VISIBLE = new Var<bool>("REPLAY_VISIBLE", false);

        public static List<String> Raketen = new List<String>();
        public static List<String> Raketen2 = new List<String>();
        public static List<Spieler> playerA = new List<Spieler>();
        public static List<Spieler> playerB = new List<Spieler>();
        public static List<bool>[] deleted;
        public static List<String> list = new List<String>();

        public static void DrawReplay(SpriteBatch spriteBatch, Spiel Spiel2)
        {
            if (Spiel2 == null || Spiel2.players[0].pos.Count == 0) return;

            {
                for (int i = 0; i < Replay.Raketen2.Count; i += 3)
                {
                    int art = Convert.ToInt32(Replay.Raketen2[i + 2]);
                    int xPos = (int)Convert.ToInt32(Replay.Raketen2[i]) - (int)Spiel2.Fenster.X;// -(int)(Texturen.missle[art].Width * Rakete.Scale[art]) / 2;
                    int yPos = (int)Convert.ToInt32(Replay.Raketen2[i + 1]) - (int)Spiel2.Fenster.Y;// -(int)(Texturen.missle[art].Height * Rakete.Scale[art]) / 2;
                    Color farbe = Color.Lime;
                    int w = Texturen.dot2.Width;
                    float scale = 0.25f;
                    spriteBatch.Draw(Texturen.dot2, new Vector2(xPos - w * scale / 2, yPos - w * scale / 2), null, farbe, 0,
                            new Vector2(0, 0), scale, SpriteEffects.None, 1);
                }
            }

            {
                int i = (Spiel2.CurrentPlayer + 1) % Spiel2.players.Count();

                for (int b = 0; b < Spiel2.players[i].pos.Count; b++)
                {
                    if (Replay.playerA[i].isthere[b] && (Replay.playerA[i].pos[b].X != Replay.playerB[i].pos[b].X || Replay.playerA[i].pos[b].Y != Replay.playerB[i].pos[b].Y))
                    {
                        Vector2 q = new Vector2(Replay.playerA[i].pos[b].X - Spiel2.Fenster.X, Replay.playerA[i].pos[b].Y - Spiel2.Fenster.Y);
                        Vector2 q2 = new Vector2(Replay.playerB[i].pos[b].X - Spiel2.Fenster.X, Replay.playerB[i].pos[b].Y - Spiel2.Fenster.Y);
                        Help.DrawLine(Game1.spriteBatch, q, q2, Color.Lime, 1);
                        spriteBatch.Draw(Texturen.Punkt, new Vector2(q2.X - 2, q2.Y - 2), null, Color.Lime, 0, Vector2.Zero, 4.0f, SpriteEffects.None, 1);

                        int xPos = (int)Replay.playerA[i].pos[b].X - (int)Spiel2.Fenster.X;
                        int yPos = (int)Replay.playerA[i].pos[b].Y - (int)Spiel2.Fenster.Y;
                        float auxfliphelper = MathHelper.ToRadians(180);
                        Vector2 cannonOrigin = new Vector2(300, 40);
                        Vector2 cannonOrigin2 = new Vector2(160, 40);
                        int xPos2 = (int)(Math.Sin(Replay.playerA[i].vehikleAngle[b]) * -25);

                        if (Replay.playerA[i].overreach[b])
                        {
                            spriteBatch.Draw(Texturen.panzerrohrumriss2[Replay.playerA[i].KindofTank[b]],
                                                               new Vector2(xPos - xPos2 + Texturen.RohrPos[Replay.playerA[i].KindofTank[b]].X, yPos - Texturen.RohrPos[Replay.playerA[i].KindofTank[b]].Y),
                                                               null, Color.White, Replay.playerA[i].Angle[b] - auxfliphelper + Replay.playerA[i].vehikleAngle[b],
                                                               Texturen.CannonOrigin[Replay.playerA[i].KindofTank[b]][1], Replay.playerA[i].SizeOfCannon[b], SpriteEffects.FlipHorizontally, 1); //

                            spriteBatch.Draw(Texturen.panzerumriss2[Replay.playerA[i].KindofTank[b]], new Vector2(xPos, yPos), null, Color.White, Replay.playerA[i].vehikleAngle[b],
                                new Vector2(Texturen.panzerumriss2[Replay.playerA[i].KindofTank[b]].Width / 2, Texturen.panzerumriss2[Replay.playerA[i].KindofTank[b]].Height), Replay.playerA[i].Size[b], SpriteEffects.FlipHorizontally, 0);
                        }
                        else
                        {
                            spriteBatch.Draw(Texturen.panzerrohrumriss2[Replay.playerA[i].KindofTank[b]],
                                                                new Vector2(xPos - xPos2 - Texturen.RohrPos[Replay.playerA[i].KindofTank[b]].X, yPos - Texturen.RohrPos[Replay.playerA[i].KindofTank[b]].Y),
                                                                null, Color.White, Replay.playerA[i].Angle[b] + Replay.playerA[i].vehikleAngle[b],
                                                                Texturen.CannonOrigin[Replay.playerA[i].KindofTank[b]][0], Replay.playerA[i].SizeOfCannon[b],
                                                                SpriteEffects.None, 1);

                            spriteBatch.Draw(Texturen.panzerumriss2[Replay.playerA[i].KindofTank[b]], new Vector2(xPos, yPos), null, Color.White, Replay.playerA[i].vehikleAngle[b],
                                new Vector2(Texturen.panzerumriss2[Replay.playerA[i].KindofTank[b]].Width / 2, Texturen.panzerumriss2[Replay.playerA[i].KindofTank[b]].Height), Replay.playerA[i].Size[b], SpriteEffects.None, 0);
                        }
                    }
                }
            }
        }

        // speichern, was passiert ist
        public static void Begin(Spieler[] _player)
        {
            // Situation der Karte vorher
            playerA.Clear();
            playerB.Clear();
            Raketen.Clear();
            deleted = new List<bool>[_player.Count()];
            for (int i = 0; i < _player.Count(); i++)
            {
                deleted[i] = new List<bool>();
                for (int b = 0; b < _player[i].pos.Count; b++)
                    deleted[i].Add(false);
            }

            for (int i = 0; i < _player.Count(); i++)
            {
                playerA.Add(new Spieler());
                playerB.Add(new Spieler());
                playerA[i].Angle.AddRange(_player[i].Angle);
                playerA[i].isthere.AddRange(_player[i].isthere);
                playerA[i].overreach.AddRange(_player[i].overreach);
                playerA[i].pos.AddRange(_player[i].pos);
                playerA[i].vehikleAngle.AddRange(_player[i].vehikleAngle);
                playerA[i].KindofTank.AddRange(_player[i].KindofTank);
                playerA[i].Size.AddRange(_player[i].Size);
                playerA[i].SizeOfCannon.AddRange(_player[i].SizeOfCannon);

                playerB[i].Angle.AddRange(_player[i].Angle);
                playerB[i].isthere.AddRange(_player[i].isthere);
                playerB[i].overreach.AddRange(_player[i].overreach);
                playerB[i].pos.AddRange(_player[i].pos);
                playerB[i].vehikleAngle.AddRange(_player[i].vehikleAngle);
                playerB[i].KindofTank.AddRange(_player[i].KindofTank);
                playerB[i].Size.AddRange(_player[i].Size);
                playerB[i].SizeOfCannon.AddRange(_player[i].SizeOfCannon);
            }
        }

        public static void End(Spieler[] _player)
        {
            // Situation der Karte danach
            for (int i = 0; i < playerA.Count(); i++)
            {
                int id = 0;
                for (int b = 0; b < playerA[i].pos.Count; b++)
                {
                    if (deleted[i][b]) continue;
                    playerB[i].Angle[b] = _player[i].Angle[id];
                    playerB[i].isthere[b] = _player[i].isthere[id];
                    playerB[i].overreach[b] = _player[i].overreach[id];
                    playerB[i].pos[b] = _player[i].pos[id];
                    playerB[i].vehikleAngle[b] = _player[i].vehikleAngle[id];
                    playerB[i].KindofTank[b] = _player[i].KindofTank[id];
                    id++;
                }
            }
        }

        public static void DeletedPlayer(Spieler[] _player, int player, int tank)
        {
            int id = 0;
            int p = 0;
            for (; id < deleted[player].Count; id++)
                if (!deleted[player][id])
                {
                    if (p == tank) break;
                    p++;
                }

            if (id >= deleted[player].Count) return;
            playerB[player].Angle[id] = _player[player].Angle[tank];
            playerB[player].isthere[id] = _player[player].isthere[tank];
            playerB[player].overreach[id] = _player[player].overreach[tank];
            playerB[player].pos[id] = _player[player].pos[tank];
            playerB[player].vehikleAngle[id] = _player[player].vehikleAngle[tank];
            playerB[player].KindofTank[id] = _player[player].KindofTank[tank];

            deleted[player][id] = true;
        }

        public static void Explosion(Vector2 pos, int Raketensorte)
        {
            // Explosion aufnehmen
            Raketen.Add(((int)pos.X).ToString());
            Raketen.Add(((int)pos.Y).ToString());
            Raketen.Add(Raketensorte.ToString());
        }

        public static void Speichern(String Datei)
        {
            StreamWriter datei = new StreamWriter(Datei);
            for (int i = 0; i < list.Count; i++)
                datei.WriteLine(list[i]);
            datei.Close();
        }

        private static int FindBegin(String Text, List<String> Data)
        {
            for (int i = 0; i < Data.Count; i++)
                if (Data[i] == Text)
                    return i;
            return -1;
        }

        private static int INT(String data)
        {
            return System.Convert.ToInt32(data);
        }

        private static float FLOAT(String data)
        {
            return (float)(System.Convert.ToDouble(data));
        }

        public static void Laden(String Datei, int player, bool you)
        {
            if (!File.Exists(Datei)) return;
            StreamReader datei = new StreamReader(Datei);
            List<String> Data = new List<String>();
            for (; !datei.EndOfStream; ) Data.Add(datei.ReadLine());

            // Begin
            {
                int a = FindBegin("<Begin>", Data); if (a == -1) { return; } a++;
                int anz = INT(Data[a]); a++;
                for (int i = 0; i < anz; i++)
                {
                    int anz2 = INT(Data[a]); a++;
                    for (int b = 0; b < anz2; b++, a += 7)
                    {
                        if (i != player || b >= playerA[i].Angle.Count) continue;
                        playerA[i].Angle[b] = FLOAT(Data[a]);
                        playerA[i].isthere[b] = Convert.ToBoolean(Data[a + 1]);
                        playerA[i].overreach[b] = Convert.ToBoolean(Data[a + 2]);
                        playerA[i].pos[b] = new Vector2(FLOAT(Data[a + 3]), FLOAT(Data[a + 4]));
                        playerA[i].vehikleAngle[b] = FLOAT(Data[a + 5]);
                        playerA[i].KindofTank[b] = INT(Data[a + 6]);
                        playerA[i].Size[b] = (float)Fahrzeugdaten.SCALEP.Wert[playerA[i].KindofTank[b]];
                        playerA[i].SizeOfCannon[b] = Fahrzeugdaten.SCALER.Wert[playerA[i].KindofTank[b]];

                        playerA[i].shootingPower = 2f;
                        playerA[i].MaxTimeout = 180 * 60;
                    }
                }
            }

            // End
            {
                int a = FindBegin("<End>", Data); if (a == -1) { return; } a++;
                int anz = INT(Data[a]); a++;
                for (int i = 0; i < anz; i++)
                {
                    int anz2 = INT(Data[a]); a++;
                    for (int b = 0; b < anz2; b++, a += 2)
                    {
                        if (i != player || b >= playerA[i].pos.Count) continue;
                        /*  playerA[i].Angle[b] = FLOAT(Data[a]);
                          playerA[i].isthere[b] = Convert.ToBoolean(Data[a + 1]);
                          playerA[i].overreach[b] = Convert.ToBoolean(Data[a + 2]);*/
                        playerB[i].pos[b] = new Vector2(FLOAT(Data[a]), FLOAT(Data[a + 1]));
                        /*playerA[i].vehikleAngle[b] = FLOAT(Data[a + 5]);
                        playerA[i].KindofTank[b] = INT(Data[a + 6]);*/
                    }
                }
            }

            // Raketen
            if (you)
            {
                Raketen.Clear();
                int a = FindBegin("<Raketen2>", Data);
                if (a != -1)
                {
                    a++;
                    for (; a < Data.Count; a++)
                    {
                        if (Data[a] == "</Raketen2>") break;
                        Raketen.Add(Data[a]);
                    }
                }
            }

            // Raketen2
            {
                Raketen2.Clear();
                int a = FindBegin("<Raketen>", Data); if (a == -1) { return; } a++;
                for (; a < Data.Count; a++)
                {
                    if (Data[a] == "</Raketen>") break;
                    Raketen2.Add(Data[a]);
                }
            }
        }

        public static List<String> Generieren(bool you)
        {
            list.Clear();
            // Begin
            list.Add("<Begin>");
            list.Add(playerA.Count.ToString());
            for (int i = 0; i < playerA.Count; i++)
            {
                list.Add(playerA[i].pos.Count.ToString());
                for (int b = 0; b < playerA[i].pos.Count; b++)
                {
                    list.Add(playerA[i].Angle[b].ToString());
                    list.Add(playerA[i].isthere[b].ToString());
                    list.Add(playerA[i].overreach[b].ToString());
                    list.Add(((int)playerA[i].pos[b].X).ToString());
                    list.Add(((int)playerA[i].pos[b].Y).ToString());
                    list.Add(playerA[i].vehikleAngle[b].ToString());
                    list.Add(playerA[i].KindofTank[b].ToString());
                }
            }
            list.Add("</Begin>");

            // End
            list.Add("<End>");
            list.Add(playerB.Count.ToString());
            for (int i = 0; i < playerB.Count; i++)
            {
                list.Add(playerB[i].pos.Count.ToString());
                for (int b = 0; b < playerB[i].pos.Count; b++)
                {
                    /*list.Add(playerB[i].Angle[b].ToString());
                    list.Add(playerB[i].isthere[b].ToString());
                    list.Add(playerB[i].overreach[b].ToString());*/
                    list.Add(((int)playerB[i].pos[b].X).ToString());
                    list.Add(((int)playerB[i].pos[b].Y).ToString());
                    /* list.Add(playerB[i].vehikleAngle[b].ToString());
                     list.Add(playerB[i].KindofTank[b].ToString());*/
                }
            }
            list.Add("</End>");

            if (you)
            {
                //Raketen
                list.Add("<Raketen>");
                for (int i = 0; i < Raketen2.Count; i++)
                {
                    list.Add(Raketen2[i]);
                }
                list.Add("</Raketen>");

                //Raketen
                list.Add("<Raketen2>");
                for (int i = 0; i < Raketen.Count; i++)
                {
                    list.Add(Raketen[i]);
                }
                list.Add("</Raketen2>");
            }
            else
            {
                //Raketen
                list.Add("<Raketen>");
                for (int i = 0; i < Raketen.Count; i++)
                {
                    list.Add(Raketen[i]);
                }
                list.Add("</Raketen>");
            }

            return list;
        }
    }
}