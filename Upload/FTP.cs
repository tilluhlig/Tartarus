using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Upload
{
    public class Ftp
    {
        #region Properties

        public string Adress { get; set; }

        public string Password { get; set; }

        public string User { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        ///     Initialisiert eine neue Instanz der FTP Helper Klasse
        /// </summary>
        /// <param name="adress">Name oder IP Adresse des Servers</param>
        /// <param name="user">Benutzername</param>
        /// <param name="password">Passwort</param>
        public Ftp(string adress, string user, string password)
        {
            Adress = adress;
            User = user;
            Password = password;
        }

        #endregion Constructor

        #region Events

        public delegate void ReceivedFileListCompleteEventhandler();

        public event ReceivedFileListCompleteEventhandler ReceivedFileListComplete;

        #endregion Events

        #region Methods

        /// <summary>
        ///     Überprüft ob eine Verbindung zum FTP Server besteht
        /// </summary>
        public void CheckConnection()
        {
            try
            {
                WebRequest.DefaultWebProxy = null;
                var ftpWebRequest = (FtpWebRequest) WebRequest.Create(new Uri("ftp://" + Adress + "/"));
                ftpWebRequest.Credentials = new NetworkCredential(User, Password);

                //Als Methode muss ListDirectory gewählt werden!
                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;

                WebResponse webResponse = ftpWebRequest.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Erstellt einen Order auf dem FTP Server in einem beliebigen Unterverzeichnis
        /// </summary>
        /// <param name="remoteFolder">Zielverzeichnis</param>
        /// <param name="folder">Verzeichnisname</param>
        public void CreateFolder(string remoteFolder, string folder)
        {
            try
            {
                var ftpWebRequest =
                    (FtpWebRequest) WebRequest.Create(new Uri("ftp://" + Adress + "/" + remoteFolder + "/" + folder));
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(User, Password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpWebRequest.Proxy = null;
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.UsePassive = false;
                ftpWebRequest.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Erstellt einen Ordner im Root Verzeichnis des FTP Nutzers
        /// </summary>
        /// <param name="folder">Verzeichnisname</param>
        public void CreateFolder(string folder)
        {
            CreateFolder("", folder);
        }

        /// <summary>
        ///     Löscht eine Datei vom FTP Server
        /// </summary>
        /// <param name="remoteFolder">Zielverzeichnis</param>
        /// <param name="fileInfo">Datei</param>
        public void DeleteFile(string remoteFolder, FileInfo fileInfo)
        {
            try
            {
                var ftpWebRequest =
                    (FtpWebRequest)
                        WebRequest.Create(new Uri("ftp://" + Adress + "/" + remoteFolder + "/" + fileInfo.Name));
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.Credentials = new NetworkCredential(User, Password);
                ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                ftpWebRequest.Proxy = null;
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.UsePassive = false;
                ftpWebRequest.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Löscht eine Datei vom FTP Server
        /// </summary>
        /// <param name="fileInfo">Datei</param>
        public void DeleteFile(FileInfo fileInfo)
        {
            DeleteFile("", fileInfo);
        }

        /// <summary>
        ///     Lädt eine Datei vom FTP Server herunter
        /// </summary>
        public void DownloadFile(string remoteFolder, FileInfo file, string destinationFolder, FileInfo destinationFile)
        {
            try
            {
                var webClient = new WebClient();

                webClient.Credentials = new NetworkCredential(User, Password);

                byte[] data = webClient.DownloadData(new Uri("ftp://" + Adress + "/" + remoteFolder + "/" + file.Name));
                //

                FileStream fileStream = File.Create(destinationFolder + @"\" + destinationFile);

                fileStream.Write(data, 0, data.Length);

                fileStream.Close();
            }
            catch (WebException)
            {
                throw;
            }
        }

        /// <summary>
        ///     Lädt eine Datei vom FTP Server herunter
        /// </summary>
        /// <param name="file"></param>
        /// <param name="destinationFolder"></param>
        /// <param name="destinationFile"></param>
        public void DownloadFile(FileInfo file, string destinationFolder, FileInfo destinationFile)
        {
            DownloadFile("", file, destinationFolder, destinationFile);
        }

        public String DownloadFileToStream(string remoteFolder, FileInfo file, string destinationFolder,
            FileInfo destinationFile)
        {
            try
            {
                var webClient = new WebClient();

                webClient.Credentials = new NetworkCredential(User, Password);

                String q = "";
                byte[] temp = webClient.DownloadData(new Uri("ftp://" + Adress + "/" + remoteFolder + "/" + file.Name));
                //
                /*for (int i = 0; i < temp.Count(); i++)
                {
                    char t = BitConverter.ToChar(temp,0);
                }*/
                //  q = System.Text.Encoding.Unicode.GetString(temp).ToCharArray();
                char[] g = Encoding.ASCII.GetChars(temp);
                q = new string(g);
                return q;
                // MemoryStream a = new MemoryStream();
                // FileStream fileStream = File.Create(destinationFolder + @"\" + destinationFile);

                //a.Write(data, 0, data.Length);
                //a.Position = 0;
                //  return a;
                /// // fileStream.Close();
            }
            catch (WebException)
            {
                throw;
            }
        }

        /// <summary>
        ///     Liefert eine Liste von Dateien zurück, die sich in einem bestimmten Verzeichnis auf dem Server befinden
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileList(string remoteFolder)
        {
            var ftpWebRequest = (FtpWebRequest) WebRequest.Create("ftp://" + Adress + "/" + remoteFolder);
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;

            WebResponse webResponse = null;

            ftpWebRequest.Credentials = new NetworkCredential(User, Password);

            try
            {
                webResponse = ftpWebRequest.GetResponse();
            }
            catch (Exception)
            {
                throw;
            }

            var files = new List<string>();

            var streamReader = new StreamReader(webResponse.GetResponseStream());

            while (!streamReader.EndOfStream)
            {
                files.Add(streamReader.ReadLine());
            }

            streamReader.Close();

            webResponse.Close();

            return files;
        }

        /// <summary>
        ///     Liefert eine Liste von Dateien zurück
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileList()
        {
            return GetFileList("");
        }

        /// <summary>
        ///     Lädt Dateien auf einen FTP Server
        /// </summary>
        /// <param name="remoteFolder">Zielverzeichnis</param>
        /// <param name="fileInfo"></param>
        public void UploadFile(string remoteFolder, FileInfo fileInfo)
        {
            try
            {
                /* if (Path.GetFileName(fileInfo.Name) != "checksum.dat")
                 {
                     if (Path.GetFileName(fileInfo.Name) == "Spiel.exe")
                     {
                         File.Copy("Spiel.exe", "SpielBackup");
                         _4_1_.Kompression.Kompression.Komprimiere("SpielBackup", Path.ChangeExtension(fileInfo.Name, ".kd"));
                         fileInfo = new FileInfo(Path.ChangeExtension(fileInfo.Name, ".kd"));
                         File.Delete("SpielBackup");
                     }
                     else
                         if (Path.GetFileName(fileInfo.Name) == "ReaderStream.dll")
                         {
                             File.Copy("ReaderStream.dll", "ReaderStreamBackup");
                             _4_1_.Kompression.Kompression.Komprimiere("ReaderStreamBackup", Path.ChangeExtension(fileInfo.Name, ".kd"));
                             fileInfo = new FileInfo(Path.ChangeExtension(fileInfo.Name, ".kd"));
                             File.Delete("ReaderStreamBackup");
                         }
                         else
                         {
                             _4_1_.Kompression.Kompression.Komprimiere(fileInfo.Name, Path.ChangeExtension(fileInfo.Name, ".kd"));
                             fileInfo = new FileInfo(Path.ChangeExtension(fileInfo.Name, ".kd"));
                         }
                 }*/

                var request =
                    (FtpWebRequest)
                        WebRequest.Create(new Uri("ftp://" + Adress + "/" + remoteFolder + "/" + fileInfo.Name));

                request.Method = WebRequestMethods.Ftp.UploadFile;

                request.Credentials = new NetworkCredential(User, Password);

                Stream ftpStream = request.GetRequestStream();

                FileStream file = File.OpenRead(fileInfo.FullName);

                int length = 1024;
                var buffer = new byte[length];
                int bytesRead = 0;

                do
                {
                    bytesRead = file.Read(buffer, 0, length);
                    ftpStream.Write(buffer, 0, bytesRead);
                } while (bytesRead != 0);

                file.Close();
                ftpStream.Close();

                // if (Path.GetExtension(fileInfo.Name) == ".kd") File.Delete(fileInfo.Name);
            }
            catch (WebException)
            {
                throw;
            }
        }

        /// <summary>
        ///     Lädt Dateien auf einen FTP Server
        /// </summary>
        /// <param name="fileInfo"></param>
        public void UploadFile(FileInfo fileInfo)
        {
            UploadFile("", fileInfo);
        }

        #endregion Methods
    }
}