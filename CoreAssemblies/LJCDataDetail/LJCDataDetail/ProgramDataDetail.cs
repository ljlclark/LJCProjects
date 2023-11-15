// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramDataDetail.cs
using System;
using System.Windows.Forms;
using LJCDataDetailDAL;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCDBMessage;
using LJCNetCommon;

namespace LJCDataDetail
{
  // The program entry point class.
  /// <include path='items/Program/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
  static class ProgramDataDetail
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../../CoreUtilities/LJCGenDoc/Common/Program.xml'/>
    [STAThread]
    private static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      if (args.Length < 1)
      {
        MessageBox.Show("Syntax: LJCDataDetail tableName"
          , "Missing Argument", MessageBoxButtons.OK
          , MessageBoxIcon.Error);
      }
      else
      {
        string userID = "-null";
        string tableName = args[0];
        DataDetailDialog dialog = new DataDetailDialog(userID, tableName);

        // Set data values.
        var data = dialog.LJCDataColumns;
        data.LJCSetValue("Id", 3);
        data.LJCSetValue("FirstName", "First");
        data.LJCSetValue("LastName", "Last");
        data.LJCSetValue("CodeType_Id", 6);
        data.LJCSetValue("PrincipleFlag", 1);

        Application.Run(dialog);
        if (DialogResult.OK == dialog.DialogResult)
        {
          ShowResult(dialog.LJCDataColumns, dialog.LJCKeyItems);
        }
      }
    }

    // 
    private static void ShowResult(DbColumns dbColumns
      , KeyItems controlItems)
    {
      string results = "";
      foreach (DbColumn dbColumn in dbColumns)
      {
        KeyItem item = null;
        if (controlItems != null)
        {
          item = controlItems.GetItem(dbColumn);
        }
        if (item != null)
        {
          results += $"{dbColumn.PropertyName}, {item.Description}" +
            $", ({dbColumn.Value})\r\n";
        }
        else
        {
          results += $"{dbColumn.PropertyName}, ({dbColumn.Value})\r\n";
        }
      }
      MessageBox.Show(results, "Data Values");
    }
  }
}
