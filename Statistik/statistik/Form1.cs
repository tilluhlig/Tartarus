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

            myBaseName = Path.GetFileNameWithoutExtension(Application.ExecutablePath);
        }

        #endregion Constructors

        #region Methods

        public int amountOfLines = 0;
        public int amountOfLinesWithoutEmptyLines = 0;

        public bool computeComposition = false;
        public int images = 0;
        public int modelFiles = 0;
        public int codeFiles = 0;
        public int otherFiles = 0;
        public String languages = "";

        public String myBaseName = "";

        public void button1_Click(object sender, EventArgs e)
        {
            var excludeList = new List<String>();
            String confFile = myBaseName + ".conf";
            List<String> availableLanguages = new List<string>();

            if (File.Exists(confFile))
            {
                string[] elements = File.ReadAllLines(confFile);
                excludeList.AddRange(elements);
            }
            excludeList.Add(confFile);

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
            var Ausgabe = new List<Ergebnis>();
            for (int i = 0; i < Dateien.Count; i++)
            {
                String Name = Path.GetFileName(Dateien[i]);
                bool foundForbiddenFile = false;
                for (int b = 0; b < excludeList.Count; b++) {
                    if (Dateien[i].Replace("\\", "/").ToLower().Contains(excludeList[b].ToLower()))
                    {
                        foundForbiddenFile = true;
                        break;
                    }
                }

                if (foundForbiddenFile)
                    continue;

                String dat = Dateien[i];

                // Projektzusammensetzung berechnen
                if (this.computeComposition)
                {
                    String fileExtension = Path.GetExtension(dat).ToLower();
                    String[] imageFilesList = { ".png",".jpg",".tiff",".jpeg",".gif",".bmp" };
                    String[] codefilesList = { ".c", ".h", ".cpp", ".cs", ".cmd", ".bat", ".tex", ".py", ".m", ".java", ".asm"};
                    String[] modelFilesList = { ".mdl", ".slx"};

                    if (Array.IndexOf(imageFilesList, fileExtension) >= 0) { images++; }
                    else
                    if (Array.IndexOf(codefilesList, fileExtension) >= 0) { codeFiles++; }
                    else
                    if (Array.IndexOf(modelFilesList, fileExtension) >= 0) { modelFiles++; }
                    else
                    { otherFiles++; }

                    String[] languageCSharp = { ".cs" };
                    String[] languageC = { ".c" };
                    String[] languageCpp = { ".cpp" };
                    String[] languageLatex= { ".tex" };
                    String[] languageMatlab = { ".m", ".mdl", ".slx" };
                    String[] languageAssembler = { ".asm" };
                    String[] languageBatch = { ".bat", ".cmd" };
                    String[] languagePython = { ".py" };
                    String[] languageJava = {".java" };
                    String[] languageSql = { ".sql" };
                    String[] languageXml = { ".xml" };
                    String[] languageXmlSchema = { ".xsd" };
                    String[] languageJson = { ".json" };
                    String[] languageReqif = { ".reqif", "reqifz" };

                    if (Array.IndexOf(languageCSharp, fileExtension) >= 0) availableLanguages.Add("C#");
                    if (Array.IndexOf(languageC, fileExtension) >= 0) availableLanguages.Add("C");
                    if (Array.IndexOf(languageCpp, fileExtension) >= 0) availableLanguages.Add("C++");
                    if (Array.IndexOf(languageLatex, fileExtension) >= 0) availableLanguages.Add("Latex");
                    if (Array.IndexOf(languageMatlab, fileExtension) >= 0) availableLanguages.Add("Matlab");
                    if (Array.IndexOf(languageAssembler, fileExtension) >= 0) availableLanguages.Add("Assembler");
                    if (Array.IndexOf(languageBatch, fileExtension) >= 0) availableLanguages.Add("Batch");
                    if (Array.IndexOf(languagePython, fileExtension) >= 0) availableLanguages.Add("Python");
                    if (Array.IndexOf(languageJava, fileExtension) >= 0) availableLanguages.Add("Java");
                    if (Array.IndexOf(languageSql, fileExtension) >= 0) availableLanguages.Add("Sql");
                    if (Array.IndexOf(languageXml, fileExtension) >= 0) availableLanguages.Add("Xml");
                    if (Array.IndexOf(languageXmlSchema, fileExtension) >= 0) availableLanguages.Add("Xml-Schema");
                    if (Array.IndexOf(languageJson, fileExtension) >= 0) availableLanguages.Add("Json");
                    if (Array.IndexOf(languageReqif, fileExtension) >= 0) availableLanguages.Add("Reqif");
                }

                if (!this.computeComposition)
                {
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

                    Ergebnis neuesErgebnis = new Ergebnis(Path.GetFileName(Dateien[i]), currentLines);
                    Ausgabe.Add(neuesErgebnis);
                    datei.Close();
                }
            }

            if (this.checkBox1.Checked)
            {
                compareByExtension cbe = new compareByExtension();
                Ausgabe.Sort(cbe);
            }
            else
            {
                compareBySize cbs = new compareBySize();
                Ausgabe.Sort(cbs);
            }

            for (int i = 0; i < Ausgabe.Count; i++) {
                comboBox1.Items.Add(Ausgabe[i].Dateiname + " -- " + Ausgabe[i].Zeilen.ToString());
            }
            label3.Text = "Files: " + Datei;
            label2.Text = "Lines: " + Count + "/"+Count2+" (without empty lines)";
            label1.Text = "Characters: " + Zeichen;

            amountOfLines = Count;
            amountOfLinesWithoutEmptyLines = Count2;

            if (computeComposition)
            {
                var unique_items = new HashSet<string>(availableLanguages);
                this.languages = String.Join(", ", unique_items);
            }
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

    public class Ergebnis
    {
        public String Dateiname;
        public int Zeilen;

        public Ergebnis(String Dateiname, int Zeilen)
        {
            this.Dateiname = Dateiname;
            this.Zeilen = Zeilen;
        }
    }

    public class compareBySize : IComparer<Ergebnis>
    {
        public int Compare(Ergebnis x, Ergebnis y)
        {
            return y.Zeilen.CompareTo(x.Zeilen);
        }
    }

    public class compareByExtension : IComparer<Ergebnis>
    {
        public int Compare(Ergebnis x, Ergebnis y)
        {
            String xExt = Path.GetExtension(x.Dateiname);
            String yExt = Path.GetExtension(y.Dateiname);
            return xExt.CompareTo(yExt);
        }
    }
}