// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ViewComboControl.cs
using LJCViewEditor;

namespace LJCDBViewControls
{
  public class ViewCommon
  {
    // Displays the ViewBuilder to edit the current view.
    public static void DoViewEdit(ViewInfo viewInfo
      , string configFileName = null)
    {
      var viewEditor = new ViewEditorList(viewInfo.TableName)
      {
        StartupViewDataID = viewInfo.DataID,
        ConfigFileName = configFileName
      };
      viewEditor.ShowDialog();
    }
  }
}
