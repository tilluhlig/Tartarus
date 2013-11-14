using System;
using System.Threading;
using Lidgren.Network;
using Microsoft.Xna.Framework;

//using SamplesCommon;
namespace _4_1_
{
    public static class Client
    {
        private static NetClient s_client;
        public static bool isRunning;
        public static _4_1_.Spiel Spiel2 = null;
        public static Game1 game = null;

        private static void Setup()
        {
            NetPeerConfiguration config = new NetPeerConfiguration("chat");
            config.AutoFlushSendQueue = false;
            s_client = new NetClient(config);

            s_client.RegisterReceivedCallback(new SendOrPostCallback(GotMessage));
        }

        public static int INT(String data)
        {
            return Convert.ToInt32(data);
        }

        public static double DOUBLE(String data)
        {
            return Convert.ToDouble(data);
        }

        public static float FLOAT(String data)
        {
            return (float)Convert.ToDouble(data);
        }

        public static void GotMessage(object peer)
        {
            NetIncomingMessage im;
            while ((im = s_client.ReadMessage()) != null)
            {
                // handle incoming message
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string text = im.ReadString();
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        NetConnectionStatus status = (NetConnectionStatus)im.ReadByte();

                        string reason = im.ReadString();

                        break;

                    case NetIncomingMessageType.Data:
                        string chat = im.ReadString();
                        string[] message = chat.Split(' ');
                        if (message[0].Length >= 2 && message[0].Substring(0, 2) == "<>")
                        {
                            Hauptfenster.Tausch.Input.Add(message[0].Substring(2, message[0].Length - 2));
                        }

                        if (message[0] == "DELETEALLHAEUSER")
                        {
                            Spiel2.Haeuser.Position.Clear();
                            Spiel2.Haeuser.HausTyp.Clear();
                            Spiel2.Haeuser.Bild.Clear();
                            Spiel2.Haeuser.Lebenspunkte.Clear();
                            Spiel2.Haeuser.Besitzer.Clear();
                            Spiel2.Haeuser.Zerstörung.Clear();
                            Spiel2.Haeuser.BesitzerPunkte.Clear();
                            Spiel2.Haeuser.Kollision.Clear();
                            Spiel2.Haeuser.BesitzerEroberer.Clear();
                        }
                        if (message[0] == "ADDHAUS") Spiel2.Haeuser.Add(new Vector2(FLOAT(message[1]), FLOAT(message[2])), 1000, INT(message[3]), -1);
                        if (message[0] == "TIMEOUT") Spiel2.Timeout = INT(message[1]); //
                        if (message[0] == "CURRENTPLAYER") Spiel2.CurrentPlayer = INT(message[1]);
                        if (message[0] == "CREDITS") Spiel2.players[INT(message[1])].Credits = INT(message[2]);
                        if (message[0] == "WIND") Spiel2.Wind.X = (float)DOUBLE(message[1]);
                        if (message[0] == "POS") { Vector2 a = new Vector2((float)DOUBLE(message[3]), (float)DOUBLE(message[4])); Spiel2.players[INT(message[1])].pos[INT(message[2])] = a; }
                        if (message[0] == "ISTHERE") Spiel2.players[INT(message[1])].isthere[INT(message[2])] = Convert.ToBoolean(message[3]);
                        if (message[0] == "ROHRANGLE") Spiel2.players[INT(message[1])].Angle[INT(message[2])] = (float)Convert.ToDouble(message[3]);
                        if (message[0] == "VEHIKLEANGLE") Spiel2.players[INT(message[1])].vehikleAngle[INT(message[2])] = (float)Convert.ToDouble(message[3]);
                        if (message[0] == "UPDATEKARTE") { Help.Spielfeld = Spiel2.Spielfeld; Vordergrund.ErstelleVordergrund(); }
                        if (message[0] == "KARTE") { Spiel2.Spielfeld[INT(message[1])].Clear(); for (int i = 2; i < message.Length; i++) Spiel2.Spielfeld[INT(message[1])].Add((UInt16)INT(message[i])); }
                        if (message[0] == "OVERREACH") Spiel2.players[INT(message[1])].overreach[INT(message[2])] = Convert.ToBoolean(message[3]);
                        // if (message[0] == "FREEZED") Spiel2.players[INT(message[1])].freezed[INT(message[2])] = INT(message[3]);
                        if (message[0] == "HP") Spiel2.players[INT(message[1])].hp[INT(message[2])] = INT(message[3]);
                        if (message[0] == "HAUSBESITZER") Spiel2.Haeuser.Besitzer[INT(message[1])] = INT(message[2]);
                        if (message[0] == "VERZOEGERUNG")
                        {
                            Waffen Missile = Spiel2.Missile[INT(message[1])];
                            Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, Missile.misslePosition, (int)Waffendaten.Daten[Missile.Art].Y,
                                Waffendaten.Daten[Missile.Art].Z, Waffendaten.Daten[Missile.Art].W, Program.game.Time,
                                Waffendaten.Farben[Missile.Art], Missile.Art, 0);

                            Spiel2.Karte.explode_missile(Spiel2.Spielfeld, Missile.misslePosition, Spiel2.Fenster, Missile.Art);
                            Missile.verzoegerung = INT(message[2]);
                        }

                        if (message[0] == "SHOT")
                        {
                            Vector2 a = Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];
                            Spiel2.players[Spiel2.CurrentPlayer].shootingPower = INT(message[1]);

                            if (!Spiel2.increaseairstrike)
                            {
                                if (Fahrzeugdaten.Shootable[Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank], Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 1)
                                {
                                    a.Y -= (float)Math.Sin(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75 + 25;
                                    a.X -= (float)Math.Cos(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75;

                                    Vector2 up = new Vector2(0, -1);
                                    Matrix rotMatrix = Matrix.CreateRotationZ(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] - (float)MathHelper.PiOver2);
                                    Vector2 b = Vector2.Transform(up, rotMatrix);
                                    b *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower / (float)Math.Log(Spiel2.players[Spiel2.CurrentPlayer].shootingPower, Math.E);

                                    Spiel2.CurrentMissile = Spiel2.AddRakete(Spiel2.CurrentPlayer, a, b, 300 * 4, Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon, Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                                    //Program.game.CurrentMissle

                                    // abschuss rauch
                                    a = Spiel2.players[Spiel2.CurrentPlayer].pos[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];
                                    //a.Y -= 25;
                                    //a.X += 25;
                                    a.Y -= (float)Math.Sin(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75 + 50;
                                    a.X -= (float)Math.Cos(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank]) * 75;

                                    up = new Vector2(0, -1);
                                    rotMatrix = Matrix.CreateRotationZ(Spiel2.players[Spiel2.CurrentPlayer].Angle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] + Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] - (float)MathHelper.PiOver2);
                                    b = Vector2.Transform(up, rotMatrix);
                                    b *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower / (float)Math.Log(Spiel2.players[Spiel2.CurrentPlayer].shootingPower, Math.E);

                                    Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, a, 4, 50, 1800, Program.game.Time, new Vector3(0.7f, 1f, 1.2f), 0, 0);

                                    Spiel2.Missile[Spiel2.CurrentMissile].focused = true; //Program.game.CurrentMissle
                                    // Bsp.: Spiel2.Missile[CurrentMissle].Explosion(Spiel2.Spielfeld, screenHeight);

                                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 2;

                                    Spiel2.Schuesse--;
                                }
                                Spiel2.increaseshot = false;
                                if (Spiel2.Schuesse <= 0) Spiel2.Timeout = 0;
                            }
                            else
                            {
                                // Airstrike abfeuern
                                if (Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon == 5)
                                {
                                    Spiel2.increaseairstrike = false;
                                    Spiel2.increaseshot = false;
                                    if (Fahrzeugdaten.Shootable[Spiel2.players[Spiel2.CurrentPlayer].KindofTank[Spiel2.players[Spiel2.CurrentPlayer].CurrentTank], Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 1)
                                    {
                                        a.X = Spiel2.players[Spiel2.CurrentPlayer].shootingPower;
                                        Spiel2.Airstrike(a, Spiel2.CurrentPlayer);
                                        Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 2;
                                    }
                                }
                                else
                                {
                                    // Abstand
                                    a.X = Spiel2.players[Spiel2.CurrentPlayer].shootingPower;
                                    Spiel2.CurrentMissile = Spiel2.AddRakete(3, new Vector2(a.X, Spiel.rand.Next(-1100, -200)), new Vector2(Spiel.rand.Next(-100, 100) / 25, -1), 300 * 4, Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon, Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                                    if (Spiel2.CurrentMissile != -1) Spiel2.Missile[Spiel2.CurrentMissile].focused = true; //Program.game.CurrentMissle
                                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 2;
                                    Spiel2.increaseairstrike = false;
                                    Spiel2.increaseshot = false;
                                }
                            }
                        }
                        if (message[0] == "PANZER") Spiel2.players[INT(message[2])].CurrentTank = INT(message[1]);
                        if (message[0] == "WAFFE") Spiel2.players[INT(message[2])].CurrentWeapon = INT(message[1]);
                        break;

                    default:
                        //Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes");
                        break;
                }
            }
        }

        // called by the UI
        public static void Connect(string host, int port)
        {
            Setup();
            s_client.Start();
            NetOutgoingMessage hail = s_client.CreateMessage();
            hail.Write("This is the hail message");
            s_client.Connect(host, port, hail);
            isRunning = true;
        }

        // called by the UI
        public static void Shutdown()
        {
            s_client.Disconnect("Requested by user");
            isRunning = false;
        }

        // called by the UI
        public static void Send(string text)
        {
            NetOutgoingMessage om = s_client.CreateMessage(text);
            s_client.SendMessage(om, NetDeliveryMethod.ReliableOrdered);
            //Output("Sending '" + text + "'");
            s_client.FlushSendQueue();
        }
    }
}