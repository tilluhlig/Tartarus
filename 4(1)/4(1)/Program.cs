using Hauptfenster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Hauptfenster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Windows.Forms;

namespace _4_1_
{
#if WINDOWS || XBOX || LINUX

    /// <summary>
    /// diese Klasse erzeugt das Spiel
    /// </summary>
    public static class Program
    {
        #region Fields

        /// <summary>
        /// ein Zeiger auf das XNA Spielobjekt
        /// </summary>
        public static Game1 game;

        public static System.Windows.Forms.Button bb;

        #endregion Fields

        #region Methods

        public static void KommandozeilenInterpreter(string[] args)
        {
            for (int i = 0; i < args.Count(); i++)
            {
                switch (args[i].ToLower())
                {
                    case "-kartengroesse":
                        Tausch.Kartengroesse = Convert.ToInt32(args[i + 1]);
                        i++;break;

                    case "-zufallskarte":
                        Tausch.StarteSpiel=true;break;

                    case "-mod":
                         Tausch.Mod = args[i + 1];
                        i++;break;

                    case "-editor":
                        Tausch.OeffneEditor = true; break;

                    case "-map":
                        Tausch.Map = args[i + 1];
                        i++;break;

                    case "-laden":
                         Tausch.SpielLaden = true; break;
                }
            }
        }

        /// <summary>
        /// Hier beginnt das Programm
        /// </summary>
        /// <param name="args">eine Liste von übergebenen Parametern</param>
        private static void Main(string[] args)
        {
            Hauptfenster.Program.Formular = new Form1();
            Form1 form = Hauptfenster.Program.Formular;

            form.Show();
            Game1 game = new Game1();//form.getDrawSurface()
            
            form.pp = game;
            Program.game = game;
           // form.timer3.Enabled = true;
            bool fehler = false;
            Thread.Sleep(100);
            Hauptfenster.Program.Formular.Hide();


            Form gameWindowForm = (Form)Form.FromHandle(Program.game.Window.Handle);
            gameWindowForm.BringToFront();
            gameWindowForm.FormBorderStyle = FormBorderStyle.None;
            bb = new System.Windows.Forms.Button();
            bb.Parent = gameWindowForm;
            bb.Location = new System.Drawing.Point(50, 50);
            bb.Text = "abc";
            bb.Hide();

            KommandozeilenInterpreter(args);

#if DEBUG
                game.Run();
#else

            /*try
            {*/
                game.Run();
           /* }
            catch (Exception e)
            {
                StreamWriter myFile = new StreamWriter(Hauptfenster.Tausch.Path + "/Content/Konfiguration/log.txt", true);
                 String data = "";
                  for (int i = 0; i < e.GetBaseException().Data.Count; i++, e.GetBaseException().Data.Values.GetEnumerator().MoveNext(), e.GetBaseException().Data.Keys.GetEnumerator().MoveNext())
                     {
                         data = data + e.GetBaseException().Data.Keys.GetEnumerator().Current + "=" + e.GetBaseException().Data.Values.GetEnumerator().Current + "\n";
                     }

                myFile.Write(
                    DateTime.Now.ToString("G") + "\r\n" +
                    "<Message>\r\n" + e.Message + "\r\n" +
                    "<Source>\r\n" + e.Source + "\r\n" +
                    "<StackTrace>\r\n" + e.StackTrace + "\r\n" +
                    "<Auslöser>\r\n" +
                    "Name: " + e.TargetSite.Name + "\r\n" +
                    "DeclaringType: " + e.TargetSite.DeclaringType.FullName + "\r\n" +
                    "\r\n\r\n"
                    );
                myFile.Close();

                if (File.Exists(Hauptfenster.Tausch.Path + "/Content/Konfiguration/Minesweeper.exe"))
                {
                    System.Diagnostics.Process Prozess = System.Diagnostics.Process.Start(Hauptfenster.Tausch.Path + "/Content/Konfiguration/Minesweeper.exe", "\"      Es ist ein Fehler aufgetreten!      \" \" Solange dieser nicht behoben ist,\n können Sie ja hier eine Runde spielen.\"");
                }
                fehler = true;
            }

            if (!fehler)
            {
                if (Server.isRunning) Server.Shutdown();
                if (Client.isRunning) Client.Shutdown();

                if (Mod.SAVE_ON_EXIT.Wert)
                {
                    // Nur Speichern
                    String Pfad = "";
                    if (HTTP.HTTP.gameid != "" && Hauptfenster.Tausch.SpielAktiv == true)
                    {
                        Pfad = "Content\\Games\\" + HTTP.HTTP.gameid + "\\";

                        HTTP.HTTP.Dir(Pfad);
                        MapWriter.Generieren(Game1.Spiel2);
                        MapWriter.Speichern(Pfad + "Map.dat");
                        Replay.End(Game1.Spiel2.players);
                        Replay.Generieren(true);
                        Replay.Speichern(Pfad + "Data.dat");
                        Game1.Spiel2 = null;
                        List<String> list = HTTP.HTTP.upload(String.Join("\r\n", MapWriter.list.ToArray()), String.Join("\r\n", Replay.list.ToArray()), "0");
                        if (HTTP.HTTP.IsFailure(list))
                        {
                            // Fehler
                        }
                        else
                        {
                            // alle dateien löschen
                            File.Delete(Pfad + "OldMap.dat");
                            File.Delete(Pfad + "OldData.dat");
                        }
                    }
                }
            }*/
#endif
        }

        #endregion Methods
    }

#endif
}