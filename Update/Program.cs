using System;
using System.Windows.Forms;

namespace Update
{
    internal static class Program
    {
        #region Methods

        /// <summary>
        ///     Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        #endregion Methods
    }
}