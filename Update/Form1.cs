using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Update
{
    public partial class Form1 : Form
    {
        private String Bezeichner = "";
        private int value = 0;
        private int maxvalue = 0;

        public static String HASH(String Datei)
        {
            BinaryReader daten = new BinaryReader(File.Open(Datei, FileMode.Open, System.IO.FileAccess.Read));
            MemoryStream a = new MemoryStream();

            int pos = 0;
            while (pos < daten.BaseStream.Length && pos < 1024)
            {
                a.WriteByte(daten.ReadByte());
                pos++;
            }
            daten.Close();

            a.Position = 0;

            String q = HASH(a);
            a.Close();
            return q;
        }

        /// <summary>
        /// HASHs the specified daten.
        /// </summary>
        /// <param name="Daten">The daten.</param>
        /// <returns>String.</returns>
        public static String HASH(Stream Daten)
        {
            MD5 hash = MD5.Create();
            char[] g = Encoding.ASCII.GetChars(hash.ComputeHash(Daten));
            String q = "";
            for (int i = 0; i < g.Length; i++)
            {
                int a = g[i];
                q = q + a.ToString();
            }

            //String q = new string(g);
            return q;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label1.Show();
            progressBar1.Show();
            timer2.Enabled = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private static List<String> HttpPostRequest(string url)
        {
            string postData = "";
            List<String> list = new List<String>();

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            try
            {
                Stream requestStream = myHttpWebRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                Stream responseStream = myHttpWebResponse.GetResponseStream();
                list.Add(((HttpWebResponse)myHttpWebResponse).StatusDescription);

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                //string pageContent = myStreamReader.ReadToEnd();
                while (!myStreamReader.EndOfStream)
                    list.Add(myStreamReader.ReadLine());

                myStreamReader.Close();
                responseStream.Close();

                myHttpWebResponse.Close();
            }
            catch (Exception)
            {
                //  list.Add("FEHLER");
                //  list.Add("Keine Verbindung");
                return list;
            }

            return list;
        }

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
                        byte[] buffer = new byte[1024];
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
            String Adress = ""; //tillu.selfhost.me

            if (Path.GetFileName(Application.ExecutablePath).ToUpper() == "UPDATE.EXE" || Path.GetFileName(Application.ExecutablePath).ToUpper() == "SPIEL.EXE")
            {
                if (File.Exists("UpdateBackup.exe")) File.Delete("UpdateBackup.exe");
                File.Copy(Path.GetFileName(Application.ExecutablePath), "UpdateBackup.exe");
                System.Diagnostics.Process Prozess = System.Diagnostics.Process.Start("UpdateBackup.exe", "");
                Application.Exit();
            }

            Bezeichner = "suche Server...";
            value = 0; maxvalue = 0;
            System.Threading.Thread.Sleep(500);

            if (HttpPostRequest("http://tartarus.bplaced.net/Updates/checksum.dat").Count > 0) ///Updates/checksum.dat
            {
                Adress = "tartarus.bplaced.net";
            }
            else
                if (HttpPostRequest("http://tillu.selfhost.me/Updates/checksum.dat").Count > 0) ///Updates/checksum.dat
                {
                    Adress = "tillu.selfhost.me";
                }

            //   HttpWebResponse response;

            /*      if (Adress == "")
                  {
                      try
                      {
                          //  HttpReq.Timeout = 1000;
                           response = (HttpWebResponse)HttpReq.GetResponse();
                          Adress = "tartarus.bplaced.net";
                      }
                      catch (WebException ex)
                      {
                          // response = (HttpWebResponse)ex.Response;
                      }
                  }*/

            /* if (Adress == "")
             {
                // HttpReq.Timeout = 1000;
                HttpReq = (HttpWebRequest)WebRequest.Create("http://192.168.2.106");

                 try
                 {
                     response = (HttpWebResponse)HttpReq.GetResponse();
                     Adress = "192.168.2.106";
                 }
                 catch (WebException ex)
                 {
                    // response = (HttpWebResponse)ex.Response;
                 }
             }*/

            // Bezeichner = Adress;
            //   value = 0; maxvalue = 0;
            // System.Threading.Thread.Sleep(5000);

            if (Adress == "")
            {
                Application.Exit();
            }

            Bezeichner = "...";
            value = 0; maxvalue = 0;

            // Dateien ermitteln
            List<String> Dateien = new List<String>();
            String meins = Application.StartupPath + Path.DirectorySeparatorChar.ToString();

            // alte hashes runterladen
            FileInfo FileInf = new FileInfo("checksum.dat");
            List<String> Data = new List<String>();

            WebClient Webclient1 = new WebClient();
            try
            {
                Webclient1.DownloadFile("http://" + Adress + "/Updates/checksum.dat", "checksum.dat");
            }
            catch
            {
                Bezeichner = "Fehler beim herunterladen der checksum.dat";
                Bezeichner = "http://" + Adress + "/Updates/checksum.dat";
                return;
            }

            try
            {
                if (File.Exists("checksum.dat"))
                {
                    StreamReader dat = new StreamReader("checksum.dat");
                    while (!dat.EndOfStream)
                        Data.Add(dat.ReadLine());
                    dat.Close();
                    File.Delete("checksum.dat");
                }
                else
                    Application.Exit();
            }
            catch
            {
                Bezeichner = "Fehler beim lesen der checksum.dat";
                return;
            }

            // Dateien ermitteln
            for (int i = 0; i < Data.Count; i++)
            {
                Dateien.Add(meins + Data[i].Split('=')[0]);
            }

            // meine Hash werte erstellen
            Bezeichner = "ermittle Hashwerte...";
            value = 0; maxvalue = Dateien.Count;
            List<String> Hash = new List<String>();
            for (int i = 0; i < Dateien.Count; i++)
            {
                if (!File.Exists(Dateien[i]))
                {
                    Hash.Add("");
                    continue;
                }

                Bezeichner = "Hash: " + Path.GetFileName(Dateien[i]);
                if (Path.GetFileName(Dateien[i]) == "Update.exe")
                {
                    File.Copy("Update.exe", "UpdateBackup");
                    Hash.Add(HASH("UpdateBackup"));
                    File.Delete("UpdateBackup");
                }
                else
                    if (Path.GetFileName(Dateien[i]) == "Spiel.exe")
                    {
                        File.Copy("Spiel.exe", "SpielBackup");
                        Hash.Add(HASH("SpielBackup"));
                        File.Delete("SpielBackup");
                    }
                    else
                        if (Path.GetFileName(Dateien[i]) == "ReaderStream.dll")
                        {
                            File.Copy("ReaderStream.dll", "ReaderStreamBackup");
                            Hash.Add(HASH("ReaderStreamBackup"));
                            File.Delete("ReaderStreamBackup");
                        }
                        else
                            Hash.Add(HASH(Dateien[i]));
                value++;
            }

            // Alte Dateinamen wieder normal machen
            for (int i = 0; i < Dateien.Count; i++)
            {
                Dateien[i] = Dateien[i].Substring(meins.Length, Dateien[i].Length - meins.Length);
            }

            // die gesamte Hashliste erstellen
            List<String> Data2 = new List<String>();

            for (int i = 0; i < Dateien.Count; i++)
            {
                Data2.Add(Dateien[i] + "=" + Hash[i]);
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
            Bezeichner = "neue Dateien herunterladen...";
            value = 0; maxvalue = Dateien.Count;
            for (int i = 0; i < Dateien.Count; i++)
            {
                /* if (Path.GetFileName(Dateien[i]) == "Update.exe")
                 {
                     continue;
                 }
                 if (Path.GetFileName(Dateien[i]) != "Spiel.exe")
                 {
                     continue;
                 }*/

                String a = Path.GetDirectoryName(Dateien[i]).Replace('\\', '/'); ;
                FileInfo FileInf2 = new FileInfo(Dateien[i]);
                String path = Path.GetDirectoryName(Dateien[i]);
                if (path != "" && !Directory.Exists(path))
                    Directory.CreateDirectory(path);
                //
                //Webclient1.DownloadFile("http://" + Adress + "/Updates/" + Dateien[i], Dateien[i]);
                DownloadFile("http://" + Adress + "/Updates/" + Dateien[i], Dateien[i]);
                value++;
            }

            Application.Exit();
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
            label1.Left = this.Width / 2 - label1.Width / 2;
        }
    }
}