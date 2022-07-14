// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// Program.cs
using System;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  // The program entry point class.
  /// <include path='items/Program/*' file='../../LJCDocLib/Common/Program.xml'/>
  static class Program
  {
    // The program entry point function.
    /// <include path='items/Main2/*' file='../../LJCDocLib/Common/Program.xml'/>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new EditList());
    }
  }
}
