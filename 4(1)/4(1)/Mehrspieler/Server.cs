using System;
using System.Collections.Generic;
using Hauptfenster;
using Lidgren.Network;
using Microsoft.Xna.Framework;

//using SamplesCommon;

namespace _4_1_
{
    public static class Server
    {
        #region Fields

        public static bool isRunning = false;
        public static Spiel Spiel2 = null;
        private static NetServer s_server;

        #endregion Fields

        #region Methods

        [STAThread]
        public static void Application_Idle(object sender, EventArgs e)
        {
            NetIncomingMessage im;
            while ((im = s_server.ReadMessage()) != null)
            {
                // handle incoming message
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.ErrorMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.VerboseDebugMessage:
                        string text = im.ReadString();
                        // Output(text);
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        var status = (NetConnectionStatus) im.ReadByte();
                        string reason = im.ReadString();
                        // Output(NetUtility.ToHexString(im.SenderConnection.RemoteUniqueIdentifier) + " " + status + ": " + reason);
                        // Neuer hinzugefügt
                        // SendAll(); /////////////////////////// changed
                        UpdateConnectionsList();
                        break;

                    case NetIncomingMessageType.Data:
                        // incoming chat message from a client
                        string chat = im.ReadString();
                        // Zerlege Message
                        string[] message = chat.Split(' ');

                        //  Output("Broadcasting '" + chat + "'");
                        // broadcast this to all connections, except sender
                        List<NetConnection> all = s_server.Connections; // get copy
                        all.Remove(im.SenderConnection);
                        if (message[0].Length >= 2 && message[0].Substring(0, 2) == "<>")
                        {
                            Tausch.Input.Add(message[0].Substring(2, message[0].Length - 2));
                        }

                        if (message[0] == "LEFT") Spiel2.Current_Left();
                        if (message[0] == "RIGHT") Spiel2.Current_Right();
                        if (message[0] == "UP") Spiel2.Current_Rohr_Right();
                        if (message[0] == "DOWN") Spiel2.Current_Rohr_Left();
                        if (message[0] == "PANZER") Spiel2.players[INT(message[2])].CurrentTank = INT(message[1]);
                        if (message[0] == "SHOT")
                        {
                            Spiel2.players[Spiel2.CurrentPlayer].shootingPower = INT(message[1]);
                            Vector2 a =
                                Spiel2.players[Spiel2.CurrentPlayer].pos[
                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];

                            if (!Spiel2.increaseairstrike)
                            {
                                if (
                                    Fahrzeugdaten.ShootableAmmunition[
                                        Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 1)
                                {
                                    a.Y -=
                                        (float)
                                            Math.Sin(
                                                Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                                Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank])*75 + 25;
                                    a.X -=
                                        (float)
                                            Math.Cos(
                                                Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                                Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank])*75;

                                    var up = new Vector2(0, -1);
                                    Matrix rotMatrix =
                                        Matrix.CreateRotationZ(
                                            Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                            Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] -
                                            MathHelper.PiOver2);
                                    Vector2 b = Vector2.Transform(up, rotMatrix);
                                    b *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower/
                                         (float) Math.Log(Spiel2.players[Spiel2.CurrentPlayer].shootingPower, Math.E);

                                    Spiel2.CurrentMissile = Spiel2.AddRakete(Spiel2.CurrentPlayer, a, b, 300*4,
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon,
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                                    //Program.game.CurrentMissle

                                    // abschuss rauch
                                    a =
                                        Spiel2.players[Spiel2.CurrentPlayer].pos[
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentTank];
                                    //a.Y -= 25;
                                    //a.X += 25;
                                    a.Y -=
                                        (float)
                                            Math.Sin(
                                                Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                                Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank])*75 + 50;
                                    a.X -=
                                        (float)
                                            Math.Cos(
                                                Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                                Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                    Spiel2.players[Spiel2.CurrentPlayer].CurrentTank])*75;

                                    up = new Vector2(0, -1);
                                    rotMatrix =
                                        Matrix.CreateRotationZ(
                                            Spiel2.players[Spiel2.CurrentPlayer].Angle[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] +
                                            Spiel2.players[Spiel2.CurrentPlayer].vehikleAngle[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank] -
                                            MathHelper.PiOver2);
                                    b = Vector2.Transform(up, rotMatrix);
                                    b *= Spiel2.players[Spiel2.CurrentPlayer].shootingPower/
                                         (float) Math.Log(Spiel2.players[Spiel2.CurrentPlayer].shootingPower, Math.E);

                                    Spiel2.Karte.AddExplosion(Spiel2.Karte.particleListExp, a, 4, 50, 1800,
                                        Program.game.Time, new Vector3(0.7f, 1f, 1.2f), 0, 0);

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
                                    if (
                                        Fahrzeugdaten.ShootableAmmunition[
                                            Spiel2.players[Spiel2.CurrentPlayer].KindofTank[
                                                Spiel2.players[Spiel2.CurrentPlayer].CurrentTank],
                                            Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon] == 1)
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
                                    Spiel2.CurrentMissile = Spiel2.AddRakete(3,
                                        new Vector2(a.X, Spiel.rand.Next(-1100, -200)),
                                        new Vector2(Spiel.rand.Next(-100, 100)/25, -1), 300*4,
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentWeapon,
                                        Spiel2.players[Spiel2.CurrentPlayer].CurrentTank);
                                    if (Spiel2.CurrentMissile != -1)
                                        Spiel2.Missile[Spiel2.CurrentMissile].focused = true;
                                    //Program.game.CurrentMissle
                                    Spiel2.players[Spiel2.CurrentPlayer].shootingPower = 2;
                                    Spiel2.increaseairstrike = false;
                                    Spiel2.increaseshot = false;
                                }
                            }
                        }
                        if (message[0] == "WAFFE") Spiel2.players[INT(message[2])].CurrentWeapon = INT(message[1]);

                        if (all.Count > 0)
                        {
                            NetOutgoingMessage om = s_server.CreateMessage();
                            om.Write(chat);
                            s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
                        }
                        break;

                    default:
                        //   Output("Unhandled type: " + im.MessageType + " " + im.LengthBytes + " bytes " + im.DeliveryMethod + "|" + im.SequenceChannel);
                        break;
                }
            }
        }

        public static double DOUBLE(String data)
        {
            return Convert.ToDouble(data);
        }

        public static int INT(String data)
        {
            return Convert.ToInt32(data);
        }

        public static void Send(String chat)
        {
            List<NetConnection> all = s_server.Connections; // get copy
            if (all.Count > 0)
            {
                NetOutgoingMessage om = s_server.CreateMessage();
                om.Write(chat);
                s_server.SendMessage(om, all, NetDeliveryMethod.ReliableOrdered, 0);
            }
        }

        public static void SendAll()
        {
            Send("DELETEALLHAEUSER");
            Send("DELETEALLBAEUME");

            for (int i = 0; i < Spiel2.Spielfeld.Length; i++)
            {
                String add = Convert.ToString(Spiel2.Spielfeld[i][0]);
                for (int b = 1; b < Spiel2.Spielfeld[i].Count; b++)
                {
                    add = add + " " + Spiel2.Spielfeld[i][b];
                }
                Send("KARTE " + i + " " + add);
            }

            Send("UPDATEKARTE");
            for (int i = 0; i < Spiel2.players.Length; i++)
            {
                for (int b = 0; b < Spiel2.players[i].pos.Count; b++)
                {
                    Send("POS " + i + " " + b + " " + Spiel2.players[i].pos[b].X + " " + Spiel2.players[i].pos[b].Y);
                    Send("ROHRANGLE " + i + " " + b + " " + Spiel2.players[i].Angle[b]);
                    Send("VEHIKLEANGLE " + i + " " + b + " " + Spiel2.players[i].vehikleAngle[b]);
                    Send("OVERREACH " + i + " " + b + " " + Spiel2.players[i].overreach[b]);
                    // Server.Send("FREEZED " + i + " " + b + " " + Spiel2.players[i].freezed[b]);
                    Send("HP " + i + " " + b + " " + Spiel2.players[i].hp[b]);
                }
                Send("CREDITS " + i + " " + Spiel2.players[i].Credits);
            }
            Send("CURRENTPLAYER " + Spiel2.CurrentPlayer);

            for (int i = 0; i < Spiel2.Haeuser.Position.Count; i++)
            {
                Send("ADDHAUS " + Spiel2.Haeuser.Position[i].X + " " + Spiel2.Haeuser.Position[i].Y + " " +
                     Spiel2.Haeuser.HausTyp[i]);
            }

            Tausch.Output.Add("<ALL>");
        }

        public static void Setup()
        {
            // set up network
            var config = new NetPeerConfiguration("chat");
            config.MaximumConnections = 100;
            config.Port = 14242;
            s_server = new NetServer(config);
        }

        // called by the UI
        public static void Shutdown()
        {
            s_server.Shutdown("Requested by user");
            isRunning = false;
        }

        public static void StartServer()
        {
            Setup();
            s_server.Start();
            isRunning = true;
        }

        private static void UpdateConnectionsList()
        {
            foreach (NetConnection conn in s_server.Connections)
            {
                string str = NetUtility.ToHexString(conn.RemoteUniqueIdentifier) + " from " + conn.RemoteEndpoint + " [" +
                             conn.Status + "]";
                // s_form.listBox1.Items.Add(str);
            }
        }

        #endregion Methods
    }
}