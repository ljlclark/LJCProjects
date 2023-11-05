// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramGenDocEdit.cs
using System;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  // The program entry point class.
  /// <include path='items/Program/*' file='../../LJCGenDoc/Common/Program.xml'/>
  internal static class ProgramGenDocEdit
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../LJCGenDoc/Common/Program.xml'/>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new LJCGenDocList());
    }
  }
}
