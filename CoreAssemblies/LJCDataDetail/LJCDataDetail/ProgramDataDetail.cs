// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProgramDataDetail.cs
using System;
using System.Windows.Forms;
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
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // *** Begin *** Testing
      string dataConfigName = "LJCData";
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName)
      };
      var dbDataAccess = dbServiceRef.DbDataAccess;

      // Create test data columns.
      string tableName = "PersonDMTest";
      var dbRequest = ManagerCommon.CreateRequest(RequestType.SchemaOnly
        , tableName, null, dataConfigName, null);
      var dbResult = dbDataAccess.Execute(dbRequest);
      var dataColumns = dbResult.Columns;
      // *** End   *** Testing

      string userID = "-null";
      DataDetailDialog dialog = new DataDetailDialog(userID, dataConfigName
        , tableName)
      {
        LJCDataColumns = dataColumns
      };

      Application.Run(dialog);
      if (DialogResult.OK == dialog.DialogResult)
      {
        ShowResult(dialog.LJCDataColumns, dialog.LJCKeyItems);
      }
    }

    // 
    private static void ShowResult(DbColumns dbColumns
      , KeyItems controlItems)
    {
      string results = "";
      foreach (DbColumn dbColumn in dbColumns)
      {
        var item = controlItems.GetItem(dbColumn);
        if (item != null)
        {
          results += $"{dbColumn.PropertyName}, {item.Description}" +
            $", {dbColumn.Value}\r\n";
        }
        else
        {
          results += $"{dbColumn.PropertyName}, {dbColumn.Value}\r\n";
        }
      }
      MessageBox.Show(results, "Data Values");
    }
  }
}
