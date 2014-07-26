using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public static class Program
    {
        #region Fields

        public static String Fehlertext = "";
        public static String Fehlertext2 = "";

        #endregion Fields

        #region Methods

        /// <summary>
        ///     Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            if (args.Length > 0)
                Fehlertext = args[0];

            if (args.Length > 1)
                Fehlertext2 = args[1];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        #endregion Methods
    }
}