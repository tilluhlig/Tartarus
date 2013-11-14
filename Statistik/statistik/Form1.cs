using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace statistik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> Dateien = new List<String>();
            List<String> Verzeichnisse = new List<String>();
            // String meins = Application.StartupPath + Path.DirectorySeparatorChar.ToString();
            Verzeichnisse.Add(Application.StartupPath + Path.DirectorySeparatorChar.ToString());

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

            List<String> Verboten = new List<String>();
            Verboten.Add("Settings.Designer.cs");
            Verboten.Add("Form1.Designer.cs");
            Verboten.Add("Resources.Designer.cs");
            Verboten.Add("AssemblyInfo.cs");

            int Count = 0;
            int Zeichen = 0;
            int Datei = 0;
            comboBox1.Items.Clear();
            for (int i = 0; i < Dateien.Count; i++)
            {
                if (Path.GetExtension(Dateien[i]).ToUpper() != ".CS" && Path.GetExtension(Dateien[i]).ToUpper() != ".PHP" && Path.GetExtension(Dateien[i]).ToUpper() != ".CPP" && Path.GetExtension(Dateien[i]).ToUpper() != ".H" && Path.GetExtension(Dateien[i]).ToUpper() != ".ASM") continue;
                String Name = Path.GetFileName(Dateien[i]);
                if (Verboten.Contains(Name)) continue;

                String dat = Dateien[i];
                Datei++;
                comboBox1.Items.Add(Path.GetFileName(Dateien[i]));
                StreamReader datei = new StreamReader(dat);
                for (; !datei.EndOfStream; Count++) { Zeichen += datei.ReadLine().Length; }
                datei.Close();
            }
            label3.Text = "Dateien: " + Datei.ToString();
            label2.Text = "Zeilen: " + Count.ToString();
            label1.Text = "Zeichen: " + Zeichen.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StreamReader file = new StreamReader("Städte.txt");
            StreamWriter file2 = new StreamWriter("Orte.txt");
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
                    else
                        if (found && t.Substring(i, 1) == "\t")
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
            List<String> Dateien = new List<String>();
            List<String> Verzeichnisse = new List<String>();
            String meins = Application.StartupPath + Path.DirectorySeparatorChar.ToString();

            Verzeichnisse.Add(Application.StartupPath + Path.DirectorySeparatorChar.ToString());
            List<String> Verboten = new List<String>();
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
    }
}