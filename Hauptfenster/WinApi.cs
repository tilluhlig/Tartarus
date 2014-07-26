﻿#region Using Statements

using System;
using System.Runtime.InteropServices;

// This class exposes WinForms-style key events.

#endregion Using Statements

namespace Hauptfenster
{
    /// <summary>
    ///     Selected Win API Function Calls
    /// </summary>
    public class WinApi
    {
        #region Fields

        private const int SM_CXSCREEN = 0;

        private const int SM_CYSCREEN = 1;

        private const int SWP_SHOWWINDOW = 64;

        private static readonly IntPtr HWND_TOP = IntPtr.Zero;

        #endregion Fields

        #region Properties

        public static int ScreenX
        {
            get { return GetSystemMetrics(SM_CXSCREEN); }
        }

        // 0×0040
        public static int ScreenY
        {
            get { return GetSystemMetrics(SM_CYSCREEN); }
        }

        #endregion Properties

        #region Methods

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        public static extern void
            SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                int X, int Y, int width, int height, uint flags);

        public static void SetWinFullScreen(IntPtr hwnd)
        {
            SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
        }

        #endregion Methods
    }
}