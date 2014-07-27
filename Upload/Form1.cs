using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Upload
{
    public partial class Form1 : Form
    {
        #region Fields

        private static String Adress = "tartarus.bplaced.net"; //"192.168.2.106";//
        private readonly Ftp system = new Ftp(Adress, "", ""); //tartarus.bplaced.net

        //  private Ftp system = new Ftp(Adress, "", "");//tartarus.bplaced.net
        private String Bezeichner = "";

        private int maxvalue;
        private int value;

        #endregion Fields

        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        public static int DownloadFile(String remoteFilename, String localFilename)
        {
            // Function will return the number of bytes processed
            // to the caller. Initialize to 0 here.
            int bytesProcessed = 0;

            // Assign values to these objects here so that they can
            // be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;

            // Use a try/catch/finally block as both the WebRequest and Stream
            // classes throw exceptions upon error
            try
            {
                // Create a request for the specified remote file name
                WebRequest request = WebRequest.Create(remoteFilename);
                if (request != null)
                {
                    // Send the request to the server and retrieve the
                    // WebResponse object
                    response = request.GetResponse();
                    if (response != null)
                    {
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();

                        // Create the local file
                        localStream = File.Create(localFilename);

                        // Allocate a 1k buffer
                        var buffer = new byte[1024];
                        int bytesRead;

                        // Simple do/while loop to read from stream until
                        // no bytes are returned
                        do
                        {
                            // Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            // Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            // Increment total bytes processed
                            bytesProcessed += bytesRead;
                        } while (bytesRead > 0);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Close the response and streams objects here
                // to make sure they're closed even if an exception
                // is thrown at some point
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
            }

            // Return total bytes processed to caller.
            return bytesProcessed;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Dateien ermitteln
            var Dateien = new List<String>();
            var Verzeichnisse = new List<String>();
            String meins = Application.StartupPath + Path.DirectorySeparatorChar;
            Verzeichnisse.Add(Application.StartupPath + Path.DirectorySeparatorChar);
            var Verboten = new List<String>();
            Verboten.Add("Upload.exe");
            Verboten.Add("Spiel.pdb");
            Verboten.Add("Upload.pdb");
            Verboten.Add("Upload.vshost.exe");
            Verboten.Add("Hauptfenster.pdb");
            Verboten.Add("ReaderStream.pdb");
            Verboten.Add("Spiel.application");
            Verboten.Add("Spiel.exe.config");
            Verboten.Add("Spiel.exe.manifest");
            Verboten.Add("Spiel.vshost.exe");
            Verboten.Add("Hauptfenster.vshost.exe");
            Verboten.Add("UpdateBackup.exe");
            Verboten.Add("Update.pdb");
            Verboten.Add("Editor.pdb");
            Verboten.Add("log.txt");

            for (int i = 0; i < Verzeichnisse.Count; i++)
            {
                string[] temp = Directory.GetDirectories(Verzeichnisse[i]);
                for (int b = 0; b < temp.Count(); b++)
                {
                    Verzeichnisse.Add(temp[b] + Path.DirectorySeparatorChar);
                }

                temp = Directory.GetFiles(Verzeichnisse[i]);
                for (int b = 0; b < temp.Count(); b++)
                {
                    if (!Verboten.Contains(Path.GetFileName(temp[b])))
                        Dateien.Add(temp[b]);
                }
            }

            // meine Hash werte erstellen
            Bezeichner = "ermittle Hashwerte...";
            value = 0;
            maxvalue = Dateien.Count;
            var Hash = new List<String>();
            for (int i = 0; i < Dateien.Count; i++)
            {
                Bezeichner = "Hash: " + Path.GetFileName(Dateien[i]);
                if (Path.GetFileName(Dateien[i]) == "Spiel.exe")
                {
                    File.Copy("Spiel.exe", "SpielBackup");
                    Hash.Add(ReaderStream.ReaderStream.HASH("SpielBackup"));
                    File.Delete("SpielBackup");
                }
                else if (Path.GetFileName(Dateien[i]) == "ReaderStream.dll")
                {
                    File.Copy("ReaderStream.dll", "ReaderStreamBackup");
                    Hash.Add(ReaderStream.ReaderStream.HASH("ReaderStreamBackup"));
                    File.Delete("ReaderStreamBackup");
                }
                else
                    Hash.Add(ReaderStream.ReaderStream.HASH(Dateien[i]));
                value++;
            }

            // alte hashes runterladen
            var FileInf = new FileInfo("checksum.dat");
            var Data = new List<String>();
            DownloadFile("http://" + Adress + "/Updates/checksum.dat", "checksum.dat");

            if (File.Exists("checksum.dat"))
            {
                var dat = new StreamReader("checksum.dat");
                while (!dat.EndOfStream)
                    Data.Add(dat.ReadLine());
                dat.Close();
                if (File.Exists("checksum.dat")) File.Delete("checksum.dat");
            }

            /*if (system.GetFileList("Updates").Contains("Updates/checksum.dat"))
            {
                String stream = system.DownloadFileToStream("Updates", FileInf, Application.StartupPath + Path.DirectorySeparatorChar, FileInf);
                for (int i = 0; i <= stream.Length; i++)
                {
                    if (i == stream.Length)
                    {
                        Data.Add(stream.Substring(0, i));
                    }
                    else
                        if (stream.Substring(i, 2) == "\r\n")
                        {
                            Data.Add(stream.Substring(0, i));
                            stream = stream.Substring(i + 2, stream.Length - i - 2);
                            i = 0;
                        }
                }
            }*/

            // Alte Dateinamen wieder normal machen
            for (int i = 0; i < Dateien.Count; i++)
            {
                Dateien[i] = Dateien[i].Substring(meins.Length, Dateien[i].Length - meins.Length);
            }

            // die gesamte Hashliste erstellen
            Bezeichner = "neue Hashtabelle erstellen...";
            value = 0;
            maxvalue = 0;
            var Data2 = new List<String>();
            var Data3 = new List<String>();
            for (int i = 0; i < Dateien.Count; i++)
            {
                Data2.Add(Dateien[i] + "=" + Hash[i]);
                Data3.Add(Dateien[i] + "=" + Hash[i]);
            }

            // Dateiliste reduzieren
            for (int i = 0; i < Data.Count; i++)
            {
                for (int b = 0; b < Dateien.Count; b++)
                {
                    if (Data[i] == Data2[b])
                    {
                        Data2.RemoveAt(b);
                        Dateien.RemoveAt(b);
                        break;
                    }
                }
            }

            // neue Dateien hochladen
            Bezeichner = "neue Dateien hochladen...";

            value = 0;
            maxvalue = Dateien.Count;
            for (int i = 0; i < Dateien.Count; i++)
            {
                Bezeichner = Dateien[i];
                String a = Path.GetDirectoryName(Dateien[i]).Replace('\\', '/');
                ;
                var FileInf2 = new FileInfo(Dateien[i]);

                // Prüfe Pfad
                String[] ordner = a.Split('/');
                String pfad = "Updates";
                if (ordner.Count() > 0 && ordner[0] != "")
                    for (int b = -1; b < ordner.Count() - 1; b++)
                    {
                        List<String> temp = system.GetFileList(pfad);

                        bool found = false;
                        for (int c = 0; c < temp.Count; c++)
                        {
                            String[] tt2 = temp[c].Split('/');
                            String tt = temp[c];
                            if (tt2.Count() > 1) tt = tt2[1];

                            if (tt == ordner[b + 1])
                            {
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            system.CreateFolder(pfad, ordner[b + 1]);
                        }

                        if (b < ordner.Count())
                            pfad = pfad + "/" + ordner[b + 1];
                    }

                // upload
                system.UploadFile("Updates/" + a, FileInf2);
                value++;
            }

            // die gesamte Hashliste hochladen
            Bezeichner = "neue Hashtabelle hochladen...";
            value = 0;
            maxvalue = 0;
            var datei = new StreamWriter("checksum.dat");
            for (int i = 0; i < Data3.Count; i++)
            {
                datei.WriteLine(Data3[i]);
            }
            datei.Close();

            system.UploadFile("Updates", new FileInfo("checksum.dat"));

            if (File.Exists("checksum.dat")) File.Delete("checksum.dat");

            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = true;
            label1.Show();
            progressBar1.Show();
            backgroundWorker1.RunWorkerAsync();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (maxvalue == 0)
            {
                progressBar1.Hide();
            }
            else
            {
                progressBar1.Maximum = maxvalue;
                progressBar1.Value = value;
                progressBar1.Show();
            }

            label1.Text = Bezeichner;
            label1.Left = Width/2 - label1.Width/2;
        }

        #endregion Methods
    }
}