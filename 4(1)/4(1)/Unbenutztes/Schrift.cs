using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace _4_1_
{
    public static class Schrift
    {
        #region Fields

        // Some private holders of font information we are loading
        private static IntPtr m_fh = IntPtr.Zero;

        private static PrivateFontCollection m_pfc;

        #endregion Fields

        #region Methods

        /////////////////////////////////////
        //
        // The GetSpecialFont procedure takes a size and
        // create a font of that size using the hardcoded
        // special font name it knows about.
        //
        /////////////////////////////////////
        public static Font GetSpecialFont(String Schriftdatei, float size)
        {
            Font fnt = null;

            if (null == m_pfc)
            {
                // First load the font as a memory stream
                Stream stmFont = Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    Schriftdatei);

                if (null != stmFont)
                {
                    //
                    // GDI+ wants a pointer to memory, GDI wants the memory.
                    // We will make them both happy.
                    //

                    // First read the font into a buffer
                    var rgbyt = new Byte[stmFont.Length];
                    stmFont.Read(rgbyt, 0, rgbyt.Length);

                    // Then do the unmanaged font (Windows 2000 and later)
                    // The reason this works is that GDI+ will create a font object for
                    // controls like the RichTextBox and this call will make sure that GDI
                    // recognizes the font name, later.
                    uint cFonts;
                    AddFontMemResourceEx(rgbyt, rgbyt.Length, IntPtr.Zero, out cFonts);

                    // Now do the managed font
                    IntPtr pbyt = Marshal.AllocCoTaskMem(rgbyt.Length);
                    if (null != pbyt)
                    {
                        Marshal.Copy(rgbyt, 0, pbyt, rgbyt.Length);
                        m_pfc = new PrivateFontCollection();
                        m_pfc.AddMemoryFont(pbyt, rgbyt.Length);
                        Marshal.FreeCoTaskMem(pbyt);
                    }
                }
            }

            if (m_pfc.Families.Length > 0)
            {
                // Handy how one of the Font constructors takes a
                // FontFamily object, huh? :-)
                fnt = new Font(m_pfc.Families[0], size);
            }

            return fnt;
        }

        // Cleanup of a private font (Win2000 and later)
        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern bool RemoveFontMemResourceEx(IntPtr fh);

        // Adding a private font (Win2000 and later)
        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern IntPtr AddFontMemResourceEx(byte[] pbFont, int cbFont, IntPtr pdv, out uint pcFonts);

        #endregion Methods
    }
}