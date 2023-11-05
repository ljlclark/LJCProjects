// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCRegionManager\Program.cs
using System;
using System.Windows.Forms;

namespace LJCRegionManager
{
  // The program entry class.
  /// <include path='items/Program/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
  static class Program
  {
    // The main entry point for the application.
    /// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
    }
  }
}
