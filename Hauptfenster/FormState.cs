// ***********************************************************************
// Assembly         : Hauptfenster
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-22-2013
// ***********************************************************************
// <copyright file="FormState.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Windows.Forms;

namespace Hauptfenster
{
    /// <summary>
    /// Class FormState
    /// </summary>
    public static class FormState
    {
        /// <summary>
        /// The win state
        /// </summary>
        private static FormWindowState winState;

        /// <summary>
        /// The BRD style
        /// </summary>
        private static FormBorderStyle brdStyle;

        /// <summary>
        /// The top most
        /// </summary>
        private static bool topMost;

        /// <summary>
        /// The bounds
        /// </summary>
        private static System.Drawing.Rectangle bounds;

        /// <summary>
        /// The back color
        /// </summary>
        private static System.Drawing.Color BackColor;

        /// <summary>
        /// The is maximized
        /// </summary>
        private static bool IsMaximized = false;

        /// <summary>
        /// Maximizes the specified target form.
        /// </summary>
        /// <param name="targetForm">The target form.</param>
        /// <param name="fullscreen">if set to <c>true</c> [fullscreen].</param>
        public static void Maximize(Form targetForm, bool fullscreen)
        {
            if (!IsMaximized)
            {
                IsMaximized = true;
                Save(targetForm);
                targetForm.BackColor = System.Drawing.Color.Black;
                if (fullscreen) targetForm.WindowState = FormWindowState.Maximized;
                if (fullscreen) targetForm.FormBorderStyle = FormBorderStyle.None;
                if (fullscreen) targetForm.TopMost = true;
                if (fullscreen) WinApi.SetWinFullScreen(targetForm.Handle);
            }
        }

        /// <summary>
        /// Saves the specified target form.
        /// </summary>
        /// <param name="targetForm">The target form.</param>
        public static void Save(Form targetForm)
        {
            winState = targetForm.WindowState;
            brdStyle = targetForm.FormBorderStyle;
            topMost = targetForm.TopMost;
            bounds = targetForm.Bounds;
            BackColor = targetForm.BackColor;
        }

        /// <summary>
        /// Restores the specified target form.
        /// </summary>
        /// <param name="targetForm">The target form.</param>
        public static void Restore(Form targetForm)
        {
            targetForm.WindowState = winState;
            targetForm.FormBorderStyle = brdStyle;
            targetForm.TopMost = topMost;
            targetForm.Bounds = bounds;
            IsMaximized = false;
            targetForm.BackColor = BackColor;
        }
    }
}