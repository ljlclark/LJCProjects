// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SectionGridCode.cs
using LJCDBClientLib;
using LJCGenTextLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.IO;
using System.Windows.Forms;
using static LJCGenTextEdit.EditList;

namespace LJCGenTextEdit
{
  // Contains the SectionGrid methods.
  internal class SectionGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal SectionGridCode(EditList parentList)
    {
      // Initialize property values.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;
      GenDataManager = EditList.GenDataManager;
      SectionGrid = EditList.SectionGrid;
      EditList.Cursor = Cursors.Default;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      SectionGrid.LJCRowsClear();

      if (GenDataManager != null)
      {
        var records = GenDataManager.LoadSections();
        if (NetCommon.HasItems(records))
        {
          foreach (Section record in records)
          {
            if (record != null)
            {
              RowAdd(record);
            }
          }
        }
      }
      EditList.Cursor = Cursors.Default;
      EditList.DoChange(Change.Section);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(Section dataRecord)
    {
      var retValue = SectionGrid.LJCRowAdd();
      retValue.LJCSetValues(SectionGrid, dataRecord);
      return retValue;
    }

    // Selects a row based on the key record values.
    private bool RowSelect(Section dataRecord)
    {
      bool retValue = false;

      if (dataRecord != null)
      {
        EditList.Cursor = Cursors.WaitCursor;
        foreach (LJCGridRow row in SectionGrid.Rows)
        {
          var name = row.LJCGetCellText("Name");
          if (name == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            SectionGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
        EditList.Cursor = Cursors.Default;
      }
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(Section dataRecord)
    {
      if (SectionGrid.CurrentRow is LJCGridRow gridRow)
      {
        gridRow.LJCSetValues(SectionGrid, dataRecord);
      }
    }
    #endregion

    #region Action Methods

    // Load the DataXML file.
    internal void DoDataXMLLoad()
    {
      FilePaths filePaths = EditList.mFilePaths;

      string dataXMLPath = filePaths.DataXMLPath;
      string initialFolder = Directory.GetCurrentDirectory();
      if (NetString.HasValue(dataXMLPath))
      {
        if (dataXMLPath.StartsWith(".."))
        {
          initialFolder = Path.GetDirectoryName(dataXMLPath);
          initialFolder = Path.GetFullPath(initialFolder);
        }
        else
        {
          initialFolder = Path.Combine(initialFolder
          , Path.GetDirectoryName(dataXMLPath));
        }
      }

      string filter = "XML(*.xml)|*.xml|All Files(*.*)|*.*";
      string fileSpec = FormCommon.SelectFile(filter, initialFolder, "*.xml");
      if (fileSpec != null)
      {
        EditList.GenDataManager = new GenDataManager(fileSpec);
        GenDataManager manager = EditList.GenDataManager;
        if (manager != null)
        {
          string fromPath = Directory.GetCurrentDirectory();
          EditList.mFilePaths.DataXMLPath = NetFile.GetRelativePath(fromPath
            , manager.FileSpec);
          EditList.DataXMLTextbox.Text = manager.FileName;
        }
        DataRetrieve();
      }
    }

    // Performs the default list action.
    internal void DoDefault()
    {
      DoEdit();
    }

    // Displays a detail dialog for a new record.
    internal void DoNew()
    {
      var location = FormCommon.GetDialogScreenPoint(SectionGrid);
      var detail = new SectionDetail()
      {
        LJCGenDataManager = EditList.GenDataManager,
        LJCLocation = location
      };
      detail.LJCChange += SectionDetail_Change;
      detail.ShowDialog();
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      if (SectionGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        string name = row.LJCGetCellText("Name");

        var location = FormCommon.GetDialogScreenPoint(SectionGrid);
        var detail = new SectionDetail()
        {
          LJCGenDataManager = EditList.GenDataManager,
          LJCLocation = location,
          LJCSectionName = name
        };
        detail.LJCChange += SectionDetail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      bool success = false;
      var row = SectionGrid.CurrentRow as LJCGridRow;
      if (row != null)
      {
        var title = "Delete Confirmation";
        var message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          success = true;
        }
      }

      if (success)
      {
        // Data from items.
        string name = row.LJCGetCellText("Name");

        success = GenDataManager.DeleteSection(name);
      }

      if (success)
      {
        success = GenDataManager.Save();
      }

      if (success)
      {
        SectionGrid.Rows.Remove(row);
        EditList.TimedChange(Change.Section);
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      string name = null;
      if (SectionGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (NetString.HasValue(name))
      {
        var record = new Section()
        {
          Name = name
        };
        RowSelect(record);
      }
      EditList.Cursor = Cursors.Default;
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    internal void SectionDetail_Change(object sender, EventArgs e)
    {
      var detail = sender as SectionDetail;
      var record = detail.LJCRecord;
      if (record != null)
      {
        if (detail.LJCIsUpdate)
        {
          RowUpdate(record);
        }
        else
        {
          var row = RowAdd(record);
          SectionGrid.LJCSetCurrentRow(row, true);
          EditList.TimedChange(Change.Section);
        }
      }
    }

    // Creates the DataXML data.
    internal void DoCreateDataFromTable()
    {
      var detail = new CreateDataDetail();

      if (DialogResult.OK == detail.ShowDialog())
      {
        string dataConfigName = detail.DataConfigName;
        string tableName = detail.TableName;

        DataManager dataManager = new DataManager(dataConfigName, tableName);
        DbColumns dbColumns = dataManager.DataDefinition;
        XMLData xmlData = new XMLData();
        string data = xmlData.Create(tableName, dbColumns);

        NetFile.CreateFolder("DataXML");
        string fileSpec = @"DataXML\GenData.xml";
        File.WriteAllText(fileSpec, data);

        EditList.GenDataManager = new GenDataManager(fileSpec);
        GenDataManager manager = EditList.GenDataManager;
        if (manager != null)
        {
          string fromPath = Environment.CurrentDirectory;
          string fullSpec = Path.GetFullPath(fileSpec);
          EditList.mFilePaths.DataXMLPath = NetFile.GetRelativePath(fromPath
            , fullSpec);
          EditList.DataXMLTextbox.Text = manager.FileName;
        }
        DataRetrieve();
      }
    }

    // Save the DataXML file.
    internal void DoDataXMLSave()
    {
      string targetFileSpec;

      GenDataManager manager = EditList.GenDataManager;

      if (manager != null)
      {
        targetFileSpec = EditList.mFilePaths.DataXMLPath;
        string sourceFileName = Path.GetFileName(targetFileSpec);
        string targetFileName = EditList.DataXMLTextbox.Text.Trim();

        string sourcefolder = Path.GetDirectoryName(targetFileSpec);
        if (!sourcefolder.StartsWith(".."))
        {
          sourcefolder = Path.Combine(Directory.GetCurrentDirectory(), sourcefolder);
        }

        if (0 != string.Compare(sourceFileName, targetFileName, true))
        {
          targetFileSpec = FormCommon.SaveFile("XML(*.xml)|*.xml", sourcefolder
            , targetFileName);
          if (targetFileSpec != null)
          {
            string fromPath = Directory.GetCurrentDirectory();
            targetFileSpec = NetFile.GetRelativePath(fromPath, targetFileSpec);
          }
        }

        if (targetFileSpec != null)
        {
          string message = $"Save data file '{targetFileSpec}'?";
          if (DialogResult.Yes == MessageBox.Show(message, "Save Confirmation"
            , MessageBoxButtons.YesNo, MessageBoxIcon.Question))
          {
            manager.FileSpec = targetFileSpec;
            manager.Save();
            EditList.mFilePaths.DataXMLPath = targetFileSpec;
          }
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the Manager reference.
    internal GenDataManager GenDataManager { get; set; }

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    // Gets or sets the Section Grid reference.
    private LJCDataGrid SectionGrid { get; set; }
    #endregion
  }
}
