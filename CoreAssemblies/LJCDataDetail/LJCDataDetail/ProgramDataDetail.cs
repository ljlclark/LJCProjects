// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// Program.cs
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
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      string userID = "-null";
      string dataConfigName = "LJCData";
      string tableName = "PersonDMTest";

      // *** Begin *** - Testing
      var dbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName)
      };
      var managers = new DataDetailManagers();
      managers.SetDBProperties(dbServiceRef, dataConfigName);
      var dbDataAccess = dbServiceRef.DbDataAccess;

      //var rowManager = managers.ControlRowManager;
      //var keyColumns = new DbColumns()
      //{
      //  { ControlRow.ColumnAllowDisplay, 1 }
      //};
      //rowManager.Delete(keyColumns);

      //var columnManager = managers.ControlColumnManager;
      //if (controlDetail != null)
      //{
      //  keyColumns = new DbColumns()
      //  {
      //    { ControlColumn.ColumnID, controlDetail.ID }
      //  };
      //  columnManager.Delete(keyColumns);
      //}

      //var controlDetailManager = managers.ControlDetailManager;
      //var controlDetail = controlDetailManager.RetrieveWithUniqueTable(userID, dataConfigName
      // , tableName);
      //keyColumns = controlDetailManager.GetUniqueTableKey(userID, dataConfigName
      //  , tableName);
      //controlDetailManager.Delete(keyColumns);
      // *** End   *** - Testing

      var dbRequest = ManagerCommon.CreateRequest(RequestType.SchemaOnly
        , tableName, null, dataConfigName, null);
      var dbResult = dbDataAccess.Execute(dbRequest);
      var dataColumns = dbResult.Columns;

      DataDetailDialog dialog = new DataDetailDialog(userID, dataConfigName
        , tableName)
      {
        LJCDataColumns = dataColumns
        //LJCKeyItems = TestData.GetKeyItems("FifthValue")
      };

      // Testing
      //controlDetail.PageColumnsLimit = 1;
      //controlDetail.ColumnRowsLimit = 12;

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
