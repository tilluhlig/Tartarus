using System;
using System.Collections.Generic;
using System.IO;

namespace _4_1_
{
#if WINDOWS || XBOX || LINUX

    public static class Program
    {
        public static Game1 game;

        //public static XnaTextInput.TextInputHandler Keyb;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            /* using (game = new Game1())
             {
                 game.Run();
             }*/

            //Keyb = new XnaTextInput.TextInputHandler((IntPtr)0);
            //Hauptfenster.Form1 form = new Hauptfenster.Form1();
            Hauptfenster.Program.Formular = new Hauptfenster.Form1();
            Hauptfenster.Form1 form = Hauptfenster.Program.Formular;
            //XnaTextInput.TextInputHandler Keyb;
            // Keyb = new XnaTextInput.TextInputHandler((IntPtr)); //
            //Hauptfenster.Program.Formular.KeyPress += new KeyPressEventHandler(this.OnKeyPress);
            // Keyb.KeyPress += new KeyPressEventHandler(this.OnKeyPress);

            form.Show();
            Game1 game = new Game1(form.getDrawSurface());
            form.pp = game;
            Program.game = game;

            // form.KeyPress = game.OnKeyPress;
            //Keyb.KeyPress += game.
            form.timer3.Enabled = true;
            bool fehler = false;

#if DEBUG
            game.Run();
#else
          
            try
            {
                game.Run();
            }
            catch (Exception e)
            {
                StreamWriter myFile = new StreamWriter(Hauptfenster.Tausch.Path + "/Content/Konfiguration/log.txt", true);
                /*  String data = "";
                  for (int i = 0; i < e.GetBaseException().Data.Count; i++, e.GetBaseException().Data.Values.GetEnumerator().MoveNext(), e.GetBaseException().Data.Keys.GetEnumerator().MoveNext())
                     {
                         data = data + e.GetBaseException().Data.Keys.GetEnumerator().Current + "=" + e.GetBaseException().Data.Values.GetEnumerator().Current + "\n";
                     }*/

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
                        MapWriter.Convert = false;
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
            }
#endif
        }
           
    }

#endif
}