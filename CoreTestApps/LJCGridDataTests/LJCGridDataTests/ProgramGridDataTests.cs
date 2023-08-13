// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// GridDataTest/Program.cs
using System;
using System.Windows.Forms;

namespace LJCGridDataTests
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());
        }
    }
}
