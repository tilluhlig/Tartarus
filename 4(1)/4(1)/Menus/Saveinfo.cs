// ***********************************************************************
// Assembly         : 4(1)
// Author           : Till
// Created          : 07-20-2013
//
// Last Modified By : Till
// Last Modified On : 04-17-2013
// ***********************************************************************
// <copyright file="Saveinfo.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace _4_1_
{
    /// <summary>
    ///     Class Saveinfo
    /// </summary>
    public class Saveinfo
    {
        #region Fields

        /// <summary>
        ///     The button
        /// </summary>
        public int button;

        /// <summary>
        ///     The target
        /// </summary>
        public string target;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Saveinfo" /> class.
        /// </summary>
        public Saveinfo()
        {
            button = -1;
            target = "";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Saveinfo" /> class.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="target">The target.</param>
        public Saveinfo(int button, string target)
        {
            this.button = button;
            this.target = target;
        }

        #endregion Constructors
    }
}