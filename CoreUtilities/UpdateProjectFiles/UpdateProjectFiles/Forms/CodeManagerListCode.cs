// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeManagerListCode.cs
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  internal partial class CodeManagerList : Form
  {

    // Setup the grid code references.
    private void SetupGridCode()
    {
      mCodeLineGridCode = new CodeLineGridCode(this);
      mCodeGroupGridCode = new CodeGroupGridCode(this);
      mSolutionGridCode = new SolutionGridCode(this);
      mProjectGridCode = new ProjectGridCode(this);
      mProjectFileGridCode = new ProjectFileGridCode(this);
    }

    internal CodeLineGridCode mCodeLineGridCode;
    internal CodeGroupGridCode mCodeGroupGridCode;
    internal SolutionGridCode mSolutionGridCode;
    internal ProjectGridCode mProjectGridCode;
    internal ProjectFileGridCode mProjectFileGridCode;
  }
}
