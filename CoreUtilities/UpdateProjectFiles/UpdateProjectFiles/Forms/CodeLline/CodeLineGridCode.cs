// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeLineGridCode.cs
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  internal class CodeLineGridCode
  {
    #region Constructors

    // <summary>Initializes an object instance.</summary>
    internal CodeLineGridCode(CodeManagerList parentList)
    {
      // Initialize property values.
      parentList.Cursor = Cursors.WaitCursor;
      CodeList = parentList;
      CodeList.Cursor = Cursors.Default;
    }
    #endregion

    #region Action Methods

    // Deletes the selected row.
    internal void DoDelete()
    {
    }

    // Displays a detail dialog to edit a record.
    internal void DoEdit()
    {
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
    }
    #endregion

    #region Properties

    // Gets or sets the Parent List reference.
    private CodeManagerList CodeList { get; set; }
    #endregion
  }
}
