using System;
using System.Windows.Forms;

namespace Hauptfenster
{
    public static class Program
    {
        #region Fields

        /// <summary>
        ///     Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        public static Form1 Formular;

        #endregion Fields

        #region Methods

        [STAThread]
        public static void Main()
        {
           // Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Formular = new Form1();
            Application.Run(Formular);
        }

        #endregion Methods
    }
}