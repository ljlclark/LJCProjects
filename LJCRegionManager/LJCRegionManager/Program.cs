// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using System.Windows.Forms;

namespace LJCRegionManager
{
  // The program entry class.
  /// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
  static class Program
  {
    // The main entry point for the application.
    /// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
    }
  }
}
