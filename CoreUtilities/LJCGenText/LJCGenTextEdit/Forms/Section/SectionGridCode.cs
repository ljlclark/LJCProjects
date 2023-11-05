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

namespace LJCGenTextEdit
{
  // Contains the SectionGrid methods.
  internal class SectionGridCode
  {
    #region Constructors

    // Initializes an object instance.
    internal SectionGridCode(EditList parent)
    {
      // Set default class data.
      mParent = parent;
      mSectionGrid = mParent.SectionGrid;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../LJCGenDoc/Common/List.xml'/>
    internal void DataRetrieve()
    {
      Sections dataRecords;
      GenDataManager manager = mParent.GenDataManager;

      mParent.Cursor = Cursors.WaitCursor;
      mSectionGrid.LJCRowsClear();

      if (manager != null)
      {
        dataRecords = manager.LoadSections();

        if (dataRecords != null && dataRecords.Count > 0)
        {
          foreach (Section record in dataRecords)
          {
            if (record != null)
            {
              RowAdd(record);
            }
          }
        }
      }
      mParent.Cursor = Cursors.Default;
      mParent.DoChange(EditList.Change.Section);
    }

    // Adds a grid row and updates it with the record values.
    private LJCGridRow RowAdd(Section dataRecord)
    {
      LJCGridRow retValue;

      retValue = mSectionGrid.LJCRowAdd();

      // Sets the row values from a data object.
      retValue.LJCSetValues(mSectionGrid, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    private void RowUpdate(Section dataRecord)
    {
      if (mSectionGrid.CurrentRow is LJCGridRow gridRow)
      {
        gridRow.LJCSetValues(mSectionGrid, dataRecord);
      }
    }

    // Selects a row based on the key record values.
    private bool RowSelect(Section dataRecord)
    {
      string name;
      bool retValue = false;

      if (dataRecord != null)
      {
        foreach (LJCGridRow row in mSectionGrid.Rows)
        {
          name = row.LJCGetCellText("Name");
          if (name == dataRecord.Name)
          {
            // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
            mSectionGrid.LJCSetCurrentRow(row, true);
            retValue = true;
            break;
          }
        }
      }
      return retValue;
    }
    #endregion

    #region Action Methods

    // Load the DataXML file.
    internal void DoDataXMLLoad()
    {
      FilePaths filePaths = mParent.mFilePaths;

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
        mParent.GenDataManager = new GenDataManager(fileSpec);
        GenDataManager manager = mParent.GenDataManager;
        if (manager != null)
        {
          string fromPath = Directory.GetCurrentDirectory();
          mParent.mFilePaths.DataXMLPath = NetFile.GetRelativePath(fromPath
            , manager.FileSpec);
          mParent.DataXMLTextbox.Text = manager.FileName;
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
      SectionDetail detail;

      var grid = mSectionGrid;
      var location = FormCommon.GetDialogScreenPoint(grid);
      detail = new SectionDetail()
      {
        LJCGenDataManager = mParent.GenDataManager,
        LJCLocation = location
      };
      detail.LJCChange += SectionDetail_Change;
      detail.ShowDialog();
    }

    // Displays a detail dialog to edit an existing record.
    internal void DoEdit()
    {
      SectionDetail detail;

      if (mSectionGrid.CurrentRow is LJCGridRow row)
      {
        // Data from items.
        string name = row.LJCGetCellText("Name");

        var grid = mSectionGrid;
        var location = FormCommon.GetDialogScreenPoint(grid);
        detail = new SectionDetail()
        {
          LJCSectionName = name,
          LJCGenDataManager = mParent.GenDataManager,
          LJCLocation = location
        };
        detail.LJCChange += SectionDetail_Change;
        detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void DoDelete()
    {
      string title;
      string message;

      if (mSectionGrid.CurrentRow is LJCGridRow row)
      {
        GenDataManager manager = mParent.GenDataManager;

        title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          // Data from items.
          string name = row.LJCGetCellText("Name");

          manager.DeleteSection(name);
          manager.Save();
          mSectionGrid.Rows.Remove(row);
          mParent.TimedChange(EditList.Change.Section);
        }
      }
    }

    // Refreshes the list.
    internal void DoRefresh()
    {
      Section dataRecord;
      string name = null;

      mParent.Cursor = Cursors.WaitCursor;
      if (mSectionGrid.CurrentRow is LJCGridRow row)
      {
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (NetString.HasValue(name))
      {
        dataRecord = new Section()
        {
          Name = name
        };
        RowSelect(dataRecord);
      }
      mParent.Cursor = Cursors.Default;
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    internal void SectionDetail_Change(object sender, EventArgs e)
    {
      SectionDetail detail;
      Section dataRecord;
      LJCGridRow row;

      detail = sender as SectionDetail;
      dataRecord = detail.LJCRecord;
      if (detail.LJCIsUpdate)
      {
        RowUpdate(dataRecord);
      }
      else
      {
        row = RowAdd(dataRecord);
        mSectionGrid.LJCSetCurrentRow(row, true);
        mParent.TimedChange(EditList.Change.Section);
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

        mParent.GenDataManager = new GenDataManager(fileSpec);
        GenDataManager manager = mParent.GenDataManager;
        if (manager != null)
        {
          string fromPath = Environment.CurrentDirectory;
          string fullSpec = Path.GetFullPath(fileSpec);
          mParent.mFilePaths.DataXMLPath = NetFile.GetRelativePath(fromPath
            , fullSpec);
          mParent.DataXMLTextbox.Text = manager.FileName;
        }
        DataRetrieve();
      }
    }

    // Save the DataXML file.
    internal void DoDataXMLSave()
    {
      string targetFileSpec;

      GenDataManager manager = mParent.GenDataManager;

      if (manager != null)
      {
        targetFileSpec = mParent.mFilePaths.DataXMLPath;
        string sourceFileName = Path.GetFileName(targetFileSpec);
        string targetFileName = mParent.DataXMLTextbox.Text.Trim();

        string sourcefolder = Path.GetDirectoryName(targetFileSpec);
        if (false == sourcefolder.StartsWith(".."))
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
            mParent.mFilePaths.DataXMLPath = targetFileSpec;
          }
        }
      }
    }
    #endregion

    #region Class Data

    private readonly EditList mParent;
    private readonly LJCDataGrid mSectionGrid;
    #endregion
  }
}
