using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace statistik
{
    internal static class Program
    {
        #region Methods

        /// <summary>
        ///     Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                if (args[1] == "--linestofile")
                {
                    // die berechnete Zeilenanzahl in eine Datei schreiben
                    Form1 statisticApp = new Form1();
                    statisticApp.button1_Click(null, null);
                    int lines = statisticApp.amountOfLines;
                    File.WriteAllText(statisticApp.myBaseName + ".dat", "Zeilen: " + lines.ToString());
                }
                else if (args[1] == "--computecomposition")
                {
                    // die Projetzusammensetzung in eine Datei schreiben
                    Form1 statisticApp = new Form1();
                    statisticApp.computeComposition = true;
                    statisticApp.button1_Click(null, null);
                    List<String> res = new List<String>();

                    if (statisticApp.languages != "")
                    {
                        res.Add("Sprachen: " + statisticApp.languages);
                    }

                    if (statisticApp.codeFiles > 0)
                    {
                        res.Add("Code-Dateien: " + statisticApp.codeFiles);
                    }

                    if (statisticApp.modelFiles > 0)
                    {
                        res.Add("Modelldateien: " + statisticApp.modelFiles);
                    }

                    if (statisticApp.images > 0)
                    {
                        res.Add("Bilder: " + statisticApp.images);
                    }

                    if (statisticApp.otherFiles > 0)
                    {
                        res.Add("Sonstige: " + statisticApp.otherFiles);
                    }

                    File.WriteAllText(statisticApp.myBaseName + ".dat", String.Join("\n",res));
                }
            }
            else
            {
                Application.Run(new Form1());
            }
        }

        #endregion Methods
    }
}