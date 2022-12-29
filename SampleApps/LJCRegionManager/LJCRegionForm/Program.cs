// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using System.Windows.Forms;

namespace LJCRegionForm
{
  // The program entry class.
  /// <include path='items/Program/*' file='Doc/ProjectRegionForm.xml'/>
  static class Program
  {
    // The main entry point for the application.
    /// <include path='items/Main/*' file='../../../CoreUtilities/LJCDocLib/Common/Program.xml'/>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new RegionTabForm());
    }
  }
}
