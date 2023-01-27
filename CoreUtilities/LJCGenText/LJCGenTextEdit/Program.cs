// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
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
    /// <include path='items/Main/*' file='../../LJCDocLib/Common/Program.xml'/>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new EditList());
    }
  }
}
