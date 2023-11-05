// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCregionForm\Program.cs
using System;
using System.Windows.Forms;

namespace LJCRegionForm
{
  // The program entry class.
  /// <include path='items/Program/*' file='Doc/ProjectRegionForm.xml'/>
  static class Program
  {
    // The main entry point for the application.
    /// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new RegionTabForm());
    }
  }
}
