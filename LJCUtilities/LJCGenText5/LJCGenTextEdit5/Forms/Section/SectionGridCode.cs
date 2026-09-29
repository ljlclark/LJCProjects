// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SectionGridCode.cs
using LJCControls5;
using LJCGenTextXAL5;
using LJCNetCommon5;
using static LJCGenTextEdit5.EditList;

namespace LJCGenTextEdit5
{
  // Contains the SectionGrid methods.
  internal class SectionGridCode
  {
    #region Parent Object Properties

    // Gets or sets the Parent List reference.
    private EditList EditList { get; set; }

    // Gets or sets the Section Grid reference.
    private LJCDataGrid SectionGrid { get; set; }

    // Gets or sets the Manager reference.
    internal SectionManager SectionManager { get; set; }

    private TemplateTextCode TemplateTextCode { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    internal SectionGridCode(EditList parentList)
    {
      // Initialize property values.
      EditList = parentList;
      EditList.Cursor = Cursors.WaitCursor;

      SectionGrid = EditList.SectionGrid;
      SectionManager = EditList.SectionManager;
      TemplateTextCode = EditList.TemplateTextCode;

      MenuEventHandlers();
      ControlEventHandlers();
      EditList.Cursor = Cursors.Default;
    }

    // Creates the menu event handlers.
    private void MenuEventHandlers()
    {
      EditList.SectionMenuNew.Click += SectionMenuNew_Click;
      EditList.SectionMenuEdit.Click += SectionMenuEdit_Click;
      EditList.SectionMenuDelete.Click += SectionMenuDelete_Click;
      EditList.SectionMenuRefresh.Click += SectionMenuRefresh_Click;
      EditList.SectionMenuCreateData.Click += SectionMenuCreateData_Click;
      EditList.SectionMenuGenerate.Click += SectionMenuGenerate_Click;
      EditList.SectionMenuSave.Click += SectionMenuSave_Click;
      EditList.SectionMenuExit.Click += SectionMenuExit_Click;
      EditList.SectionMenuHelp.Click += SectionMenuHelp_Click;
      EditList.SectionMenuAbout.Click += SectionMenuAbout_Click;
    }

    // Creates the control event handlers.
    private void ControlEventHandlers()
    {
      var grid = SectionGrid;
      EditList.DataXMLButton.Click += DataXMLButton_Click;
      grid.KeyDown += SectionGrid_KeyDown;
      grid.MouseDoubleClick += SectionGrid_MouseDoubleClick;
      grid.MouseDown += SectionGrid_MouseDown;
      grid.SelectionChanged += SectionGrid_SelectionChanged;
    }
    #endregion

    #region Data Methods

    // Retrieves the list rows.
    internal void DataRetrieve()
    {
      EditList.Cursor = Cursors.WaitCursor;
      SectionGrid.LJCRowsClear();

      if (SectionManager != null)
      {
        var records = SectionManager.Load();
        if (LJC.HasListItems(records))
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
    private LJCGridRow? RowAdd(Section dataRecord)
    {
      LJCGridRow retValue = SectionGrid.LJCRowAdd();
      retValue.LJCSetValues(dataRecord);
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
        gridRow.LJCSetValues(dataRecord);
      }
    }
    #endregion

    #region Action Methods

    // Load the DataXML file.
    internal void DataXMLLoad()
    {
      FilePaths filePaths = EditList.mFilePaths;

      string dataXMLPath = filePaths.DataXMLPath;
      string initialFolder = Directory.GetCurrentDirectory();
      if (LJC.HasText(dataXMLPath))
      {
        if (dataXMLPath.StartsWith(".."))
        {
          var value = Path.GetDirectoryName(dataXMLPath);
          if (LJC.HasText(value))
          {
            initialFolder = Path.GetFullPath(value);
          }
        }
        else
        {
          var dataXMLFolder = Path.GetDirectoryName(dataXMLPath);
          if (LJC.HasText(dataXMLFolder))
          {
            initialFolder = Path.Combine(initialFolder, dataXMLFolder);
          }
        }
      }

      string filter = "XML(*.xml)|*.xml|All Files(*.*)|*.*";
      string? fileSpec = FormCommon.SelectFile(filter, initialFolder, "*.xml");
      if (fileSpec != null)
      {
        SectionManager manager = new(fileSpec);
        EditList.SectionManager = manager;
        if (manager != null)
        {
          string fromPath = Directory.GetCurrentDirectory();
          EditList.mFilePaths.DataXMLPath = LJCNetFile.GetRelativePath(fromPath
            , manager.FileSpec);
          EditList.DataXMLTextbox.Text = manager.FileName;
        }
        DataRetrieve();
      }
    }

    // Performs the default list action.
    internal void Default()
    {
      Edit();
    }

    // Displays a detail dialog for a new record.
    internal void New()
    {
      var location = FormPoint.DialogScreenPoint(SectionGrid);
      //var detail = new SectionDetail()
      //{
      //  LJCGenDataManager = EditList.GenDataManager,
      //  LJCLocation = location
      //};
      //detail.LJCChange += SectionDetail_Change;
      //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
      //detail.ShowDialog();
    }

    // Displays a detail dialog to edit an existing record.
    internal void Edit()
    {
      if (SectionGrid.CurrentRow is LJCGridRow row)
      {
        //// Data from items.
        //string name = row.LJCGetCellText("Name");

        //var location = FormPoint.DialogScreenPoint(SectionGrid);
        //var detail = new SectionDetail()
        //{
        //  LJCGenDataManager = EditList.GenDataManager,
        //  LJCLocation = location,
        //  LJCSectionName = name
        //};
        //detail.LJCChange += SectionDetail_Change;
        //detail.LJCLocation = FormPoint.AdjustedLocation(detail, location);
        //detail.ShowDialog();
      }
    }

    // Deletes the selected row.
    internal void Delete()
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

      if (row != null
        && success)
      {
        // Data from items.
        string? name = row.LJCGetCellText("Name");

        if (LJC.HasText(name))
        {
          success = SectionManager.Delete(name);
        }
      }

      if (success)
      {
        success = SectionManager.Save();
      }

      if (success)
      {
        SectionGrid.Rows.Remove(row);
        EditList.TimedChange(Change.Section);
      }
    }

    // Refreshes the list.
    internal void Refresh()
    {
      EditList.Cursor = Cursors.WaitCursor;
      string? name = null;
      if (SectionGrid.CurrentRow is LJCGridRow row)
      {
        // Save the original row.
        name = row.LJCGetCellText("Name");
      }
      DataRetrieve();

      // Select the original row.
      if (LJC.HasText(name))
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
      //var detail = sender as SectionDetail;
      //var record = detail.LJCRecord;
      //if (record != null)
      //{
      //  if (detail.LJCIsUpdate)
      //  {
      //    RowUpdate(record);
      //  }
      //  else
      //  {
      //    var row = RowAdd(record);
      //    SectionGrid.LJCSetCurrentRow(row, true);
      //    EditList.TimedChange(Change.Section);
      //  }
      //}
    }

    // Creates the DataXML data.
    internal void CreateDataFromTable()
    {
      //var detail = new CreateDataDetail();

      //if (DialogResult.OK == detail.ShowDialog())
      //{
      //  string dataConfigName = detail.DataConfigName;
      //  string tableName = detail.TableName;

      //  LJCDataManager dataManager = new LJCDataManager(dataConfigName, tableName);
      //  LJCDataColumns dbColumns = dataManager.DataDefinition;
      //  XMLData xmlData = new XMLData();
      //  string data = xmlData.Create(tableName, dbColumns);

      //  LJCNetFile.CreateFolder("DataXML");
      //  string fileSpec = @"DataXML\GenData.xml";
      //  File.WriteAllText(fileSpec, data);

      //  SectionManager manager = new(fileSpec);
      //  EditList.SectionManager = manager;
      //  if (manager != null)
      //  {
      //    string fromPath = Environment.CurrentDirectory;
      //    string fullSpec = Path.GetFullPath(fileSpec);
      //    EditList.mFilePaths.DataXMLPath = LJCNetFile.GetRelativePath(fromPath
      //      , fullSpec);
      //    EditList.DataXMLTextbox.Text = manager.FileName;
      //  }
      DataRetrieve();
      //}
    }

    // Save the DataXML file.
    internal void DataXMLSave()
    {

      SectionManager manager = EditList.SectionManager;

      if (manager != null)
      {
        var targetFileSpec = EditList.mFilePaths.DataXMLPath;
        string sourceFileName = Path.GetFileName(targetFileSpec);
        string targetFileName = EditList.DataXMLTextbox.Text.Trim();

        var sourcefolder = Path.GetDirectoryName(targetFileSpec);
        if (LJC.HasText(sourcefolder)
          && !sourcefolder.StartsWith(".."))
        {
          sourcefolder = Path.Combine(Directory.GetCurrentDirectory()
            , sourcefolder);
        }

        if (0 != string.Compare(sourceFileName, targetFileName, true))
        {
          targetFileSpec = FormCommon.SaveFile("XML(*.xml)|*.xml", sourcefolder
            , targetFileName);
          if (targetFileSpec != null)
          {
            string fromPath = Directory.GetCurrentDirectory();
            targetFileSpec = LJCNetFile.GetRelativePath(fromPath, targetFileSpec);
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

    #region Action Event Handlers

    // Calls the New method.
    private void SectionMenuNew_Click(object? sender, EventArgs e)
    {
      New();
    }

    // Calls the Edit method.
    private void SectionMenuEdit_Click(object? sender, EventArgs e)
    {
      Edit();
    }

    // Calls the Delete method.
    private void SectionMenuDelete_Click(object? sender, EventArgs e)
    {
      Delete();
    }

    // Calls the Refresh method.
    private void SectionMenuRefresh_Click(object? sender, EventArgs e)
    {
      Refresh();
    }

    // Creates the XML data.
    private void SectionMenuCreateData_Click(object? sender, EventArgs e)
    {
      CreateDataFromTable();
    }

    // Performs the Generate Output function.
    private void SectionMenuGenerate_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.Generate();
    }

    // Performs the Save function.
    private void SectionMenuSave_Click(object? sender, EventArgs e)
    {
      DataXMLSave();
    }

    // Performs the Close function.
    private void SectionMenuExit_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.DoClose();
    }

    // Displays the context sensitive help.
    private void SectionMenuHelp_Click(object? sender, EventArgs e)
    {
      Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
        , @"Data\SectionList.html");
    }

    // Displays the Splash dialog as an about dialog.
    private void SectionMenuAbout_Click(object? sender, EventArgs e)
    {
      TemplateTextCode.About();
    }
    #endregion

    #region Control Event Handlers

    // Performs the Select Data XML file function.
    private void DataXMLButton_Click(object? sender, EventArgs e)
    {
      EditList.SectionGridCode.DataXMLLoad();
    }

    // Handles the form keys.
    private void SectionGrid_KeyDown(object? sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          EditList.SectionGridCode.Default();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(EditList, EditList.LJCHelpFile, HelpNavigator.Topic
            , @"Data\SectionList.html");
          e.Handled = true;
          break;

        case Keys.F5:
          EditList.SectionGridCode.Refresh();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(SectionGrid
              , Control.MousePosition);
            EditList.SectionMenu.Show(position);
            EditList.SectionMenu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            EditList.ReplacementGrid.Select();
          }
          else
          {
            EditList.ItemGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void SectionGrid_MouseDoubleClick(object? sender, MouseEventArgs e)
    {
      if (SectionGrid.LJCGetMouseRow(e) != null)
      {
        EditList.SectionGridCode.Default();
      }
    }

    // Handles the MouseDown event.
    private void SectionGrid_MouseDown(object? sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        SectionGrid.Select();
        if (SectionGrid.LJCIsDifferentRow(e))
        {
          SectionGrid.LJCSetCurrentRow(e);
          EditList.TimedChange(Change.Section);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void SectionGrid_SelectionChanged(object? sender, EventArgs e)
    {
      if (SectionGrid.LJCAllowSelectionChange)
      {
        EditList.TimedChange(Change.Section);
      }
      SectionGrid.LJCAllowSelectionChange = true;
    }
    #endregion
  }
}
