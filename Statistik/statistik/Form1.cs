using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace statistik
{
    public partial class Form1 : Form
    {
        #region Constructors

        public Form1()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods

        private void button1_Click(object sender, EventArgs e)
        {
            var excludeList = new List<String>();
            String confFile = "statistic.conf";

            if (File.Exists(confFile))
            {
                string[] elements = File.ReadAllLines(confFile);
                excludeList.AddRange(elements);
            }
            excludeList.Add("statistic.conf");
            excludeList.Add("/Backup");
            excludeList.Add(".exe");
            excludeList.Add(".pdf");
            excludeList.Add(".pdb");
            excludeList.Add(".suo");
            excludeList.Add(".sln");
            excludeList.Add(".ico");
            excludeList.Add(".dll");
            excludeList.Add(".png");
            excludeList.Add(".jpg");
            excludeList.Add(".jpeg");
            excludeList.Add(".tiff");
            excludeList.Add(".png");
            excludeList.Add(".gif");
            excludeList.Add(".bmp");
            excludeList.Add(".xcf");
            excludeList.Add(".xnb");
            excludeList.Add(".csproj");
            excludeList.Add(".idx");
            excludeList.Add(".reqifz");
            excludeList.Add(".zip");
            excludeList.Add(".rar");
            excludeList.Add(".ogg");
            excludeList.Add(".mp3");
            excludeList.Add(".mp4");
            excludeList.Add(".avi");
            excludeList.Add(".mpeg");
            excludeList.Add(".flv");
            excludeList.Add(".mp2");
            excludeList.Add(".wav");
            excludeList.Add(".cache");
            excludeList.Add(".resources");
            excludeList.Add(".spritefont");
            excludeList.Add("/.git");

            for (int i = 0; i < excludeList.Count; i++)
            {
                if (excludeList[i].Trim() == "")
                {
                    excludeList.RemoveAt(i);
                    i--;
                    continue;
                }
                excludeList[i] = excludeList[i].Replace("\\", "/");
            }

            var Dateien = new List<String>();
            var Verzeichnisse = new List<String>();
            // String meins = Application.StartupPath + Path.DirectorySeparatorChar.ToString();
            Verzeichnisse.Add(Application.StartupPath + Path.DirectorySeparatorChar);

            for (int i = 0; i < Verzeichnisse.Count; i++)
            {
                string[] temp = Directory.GetDirectories(Verzeichnisse[i]);
                for (int b = 0; b < temp.Length; b++)
                {
                    if (Path.GetFileName(temp[b]) == "Backup") continue;
                    Verzeichnisse.Add(temp[b] + Path.DirectorySeparatorChar);
                }

                temp = Directory.GetFiles(Verzeichnisse[i]);
                for (int b = 0; b < temp.Length; b++)
                {
                    if (Path.GetFileName(temp[b]) == "Backup") continue;
                    Dateien.Add(temp[b]);
                }
            }

            var Verboten = new List<String>();
            Verboten.Add("Settings.Designer.cs");
            Verboten.Add("Form1.Designer.cs");
            Verboten.Add("Resources.Designer.cs");
            Verboten.Add("AssemblyInfo.cs");

            int Count = 0;
            int Count2 = 0;
            int Zeichen = 0;
            int Datei = 0;
            comboBox1.Items.Clear();
            for (int i = 0; i < Dateien.Count; i++)
            {
                String Name = Path.GetFileName(Dateien[i]);
                bool foundForbiddenFile = false;
                for (int b=0;b<excludeList.Count;b++){
                    if (Dateien[i].Replace("\\","/").ToLower().Contains(excludeList[b].ToLower()))
                    {
                        foundForbiddenFile = true;
                        break;
                    }
                }

                if (foundForbiddenFile)
                    continue;

                String dat = Dateien[i];
                Datei++;
                int currentLines = 0;
                var datei = new StreamReader(dat);
                for (; !datei.EndOfStream; Count++)
                {
                    String q = datei.ReadLine();
                    String trimmedChars = q.Trim();
                    if (trimmedChars != "")
                    {
                        Count2++;
                        currentLines++;
                        Zeichen += trimmedChars.Length;
                    }
                }
                comboBox1.Items.Add(Path.GetFileName(Dateien[i]) + " -- " + currentLines.ToString());
                datei.Close();
            }
            label3.Text = "Files: " + Datei;
            label2.Text = "Lines: " + Count + "/"+Count2+" (without empty lines)";
            label1.Text = "Characters: " + Zeichen;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var file = new StreamReader("Städte.txt");
            var file2 = new StreamWriter("Orte.txt");
            while (!file.EndOfStream)
            {
                String t = file.ReadLine();
                bool found = false;
                int b = 0;
                for (int i = 0; i < t.Length; i++)
                {
                    if (!found && t.Substring(i, 1) == "\t")
                    {
                        found = true;
                        b = i;
                    }
                    else if (found && t.Substring(i, 1) == "\t")
                    {
                        String result = t.Substring(b + 1, i - b - 2);
                        if (result.Length <= 11) file2.WriteLine(result);
                        break;
                    }
                }
            }
            file.Close();
            file2.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var Dateien = new List<String>();
            var Verzeichnisse = new List<String>();
            String meins = Application.StartupPath + Path.DirectorySeparatorChar;

            Verzeichnisse.Add(Application.StartupPath + Path.DirectorySeparatorChar);
            var Verboten = new List<String>();
            Verboten.Add(".svn");
            Verboten.Add("Backup");
            Verboten.Add("obj");
            Verboten.Add("bin");
            Verboten.Add("ImageMagick");
            Verboten.Add("IrfanView");
            Verboten.Add("NichtBenutzt");
            Verboten.Add("oldBaum");
            Verboten.Add("genutzte");
            Verboten.Add("backup");
            Verboten.Add("oldTextures");

            for (int i = 0; i < Verzeichnisse.Count; i++)
            {
                string[] temp = Directory.GetDirectories(Verzeichnisse[i]);
                for (int b = 0; b < temp.Length; b++)
                {
                    String q = Path.GetFileName(temp[b]);
                    if (Verboten.Contains(q)) continue;
                    Verzeichnisse.Add(temp[b] + Path.DirectorySeparatorChar);
                }

                temp = Directory.GetFiles(Verzeichnisse[i]);
                for (int b = 0; b < temp.Length; b++)
                {
                    Dateien.Add(temp[b]);
                }
            }

            if (Directory.Exists(meins + "Backup")) Directory.Delete(meins + "Backup", true);

            Directory.CreateDirectory(meins + "Backup");

            for (int i = 0; i < Dateien.Count; i++)
            {
                String Ziel = meins + "Backup\\" + Dateien[i].Substring(meins.Length, Dateien[i].Length - meins.Length);
                Directory.CreateDirectory(Path.GetDirectoryName(Ziel));
                File.Copy(Dateien[i], Ziel);
            }
        }

        #endregion Methods

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}