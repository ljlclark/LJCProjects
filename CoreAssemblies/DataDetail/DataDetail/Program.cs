// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using System;
using System.Windows.Forms;
using LJCNetCommon;
using LJCTestDataLib;

namespace DataDetail
{
  // The program entry point class.
  /// <include path='items/Program/*' file='../../../CoreUtilities/LJCDocLib/Common/Program.xml'/>
  static class Program
  {
    // The program entry point function.
    /// <include path='items/Main/*' file='../../../CoreUtilities/LJCDocLib/Common/Program.xml'/>
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      string userID = "-null";
      string dataConfigName = "LJCData";
      string tableName = "PersonTest";

      // *** Begin *** - Testing
      //var dbServiceRef = new DbServiceRef()
      //{
      //  DbDataAccess = new DbDataAccess(dataConfigName)
      //};
      //var managers = new DataDetailManagers();
      //managers.SetDBProperties(dbServiceRef, dataConfigName);

      //var configManager = managers.DetailConfigManager;
      //var detailConfig = configManager.RetrieveWithUniqueTable(userID, dataConfigName
      // , tableName);

      //var rowManager = managers.ControlRowManager;
      //var keyColumns = new DbColumns()
      //{
      //  { ControlRow.ColumnAllowDisplay, 1 }
      //};
      //rowManager.Delete(keyColumns);

      //var columnManager = managers.ControlColumnManager;
      //if (detailConfig != null)
      //{
      //  keyColumns = new DbColumns()
      //  {
      //    { ControlColumn.ColumnDetailConfigID, detailConfig.ID }
      //  };
      //  columnManager.Delete(keyColumns);
      //}

      //keyColumns = configManager.GetUniqueTableKey(userID, dataConfigName
      //  , tableName);
      //configManager.Delete(keyColumns);
      // *** End   *** - Testing

      DataDetailDialog dialog = new DataDetailDialog(userID, dataConfigName
        , tableName)
      {
        LJCDataColumns = TestData.GetRecord(),
        LJCKeyItems = TestData.GetKeyItems("FifthValue")
      };

      // Testing
      //dialog.LJCDetailConfig.PageColumnsLimit = 1;
      //dialog.LJCDetailConfig.ColumnRowsLimit = 12;

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
