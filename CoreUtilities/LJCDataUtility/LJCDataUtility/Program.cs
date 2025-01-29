// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
using System;
using System.Windows.Forms;

namespace LJCDataUtility
{
  internal static class Program
  {
    // The main entry point for the application.
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new DataUtilityList());
    }
  }
}
