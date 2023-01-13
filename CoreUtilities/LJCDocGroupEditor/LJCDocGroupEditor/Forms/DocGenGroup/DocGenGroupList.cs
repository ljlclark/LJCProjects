// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DocGenGroupList.cs
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCDocLibDAL;

namespace LJCDocGroupEditor
{
  // The list form.
  /// <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
  public partial class DocGenGroupList : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DocGenGroupList()
    {
      InitializeComponent();

      LJCHelpFile = "FacilityManager.chm";
      LJCHelpPageList = "DocGenGroupList.htm";
      LJCHelpPageDetail = "DocGenGroupDetail.htm";
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void DocGenGroupList_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToScreen();
    }
    #endregion

    #region Data Methods

    #region Group

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DataRetrieveGroup()
    {
      DocGenGroups records;

      DocGenGroupGrid.LJCRowsClear();
      records = mDocGenGroupManager.Load();

      if (records != null && records.Count > 0)
      {
        foreach (DocGenGroup record in records)
        {
          RowAddGroup(record);
        }
      }
      DoChange(Change.DocGenGroup);
    }

    // Adds a grid row and updates it with the record values.
    /// <include path='items/RowAdd/*' file='../../LJCDocLib/Common/List.xml'/>
    private LJCGridRow RowAddGroup(DocGenGroup dataRecord)
    {
      LJCGridRow retValue;

      retValue = DocGenGroupGrid.LJCRowAdd();
      SetStoredValuesGroup(retValue, dataRecord);

      // Sets the row values from a data object.
      DocGenGroupGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    /// <include path='items/RowUpdate/*' file='../../LJCDocLib/Common/List.xml'/>
    private void RowUpdateGroup(DocGenGroup dataRecord)
    {
      if (DocGenGroupGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesGroup(row, dataRecord);
        DocGenGroupGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    /// <include path='items/SetStoredValues/*' file='../../LJCDocLib/Common/List.xml'/>
    private void SetStoredValuesGroup(LJCGridRow row, DocGenGroup dataRecord)
    {
      row.LJCSetString(DocGenGroup.ColumnName, dataRecord.Name);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../LJCDocLib/Common/List.xml'/>
    private bool RowSelectGroup(DocGenGroup dataRecord)
    {
      string rowID;
      bool retValue = false;

      foreach (LJCGridRow row in DocGenGroupGrid.Rows)
      {
        rowID = row.LJCGetString(DocGenGroup.ColumnName);
        if (rowID == dataRecord.Name)
        {
          DocGenGroupGrid.LJCSetCurrentRow(row, true);
          retValue = true;
          break;
        }
      }
      return retValue;
    }
    #endregion

    #region Assembly

    // Retrieves the list rows.
    /// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DataRetrieveAssembly()
    {
      DocGenAssemblies records;

      DocAssemblyGrid.LJCRowsClear();

      // if child grid.
      if (DocGenGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        string groupName = parentRow.LJCGetString(DocGenGroup.ColumnName);
        if (NetString.HasValue(groupName))
        {
          records = mDocGenGroupManager.LoadAssemblies(groupName);

          if (records != null && records.Count > 0)
          {
            foreach (DocGenAssembly record in records)
            {
              RowAddAssembly(record);
            }
          }
          DoChange(Change.DocGenAssembly);
        }
      }
    }

    // Adds a grid row and updates it with the record values.
    /// <include path='items/RowAdd/*' file='../../LJCDocLib/Common/List.xml'/>
    private LJCGridRow RowAddAssembly(DocGenAssembly dataRecord)
    {
      LJCGridRow retValue;

      retValue = DocAssemblyGrid.LJCRowAdd();
      SetStoredValuesAssembly(retValue, dataRecord);

      // Sets the row values from a data object.
      DocAssemblyGrid.LJCRowSetValues(retValue, dataRecord);
      return retValue;
    }

    // Updates the current row with the record values.
    /// <include path='items/RowUpdate/*' file='../../LJCDocLib/Common/List.xml'/>
    private void RowUpdateAssembly(DocGenAssembly dataRecord)
    {
      if (DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        SetStoredValuesAssembly(row, dataRecord);
        DocAssemblyGrid.LJCRowSetValues(row, dataRecord);
      }
    }

    // Sets the row stored values.
    /// <include path='items/SetStoredValues/*' file='../../LJCDocLib/Common/List.xml'/>
    private void SetStoredValuesAssembly(LJCGridRow row
      , DocGenAssembly dataRecord)
    {
      row.LJCSetString(DocGenAssembly.ColumnName, dataRecord.Name);
    }

    // Selects a row based on the key record values.
    /// <include path='items/RowSelect/*' file='../../LJCDocLib/Common/List.xml'/>
    private bool RowSelectAssembly(DocGenAssembly dataRecord)
    {
      string rowID;
      bool retValue = false;

      foreach (LJCGridRow row in DocAssemblyGrid.Rows)
      {
        rowID = row.LJCGetString(DocGenAssembly.ColumnName);
        if (rowID == dataRecord.Name)
        {
          DocAssemblyGrid.LJCSetCurrentRow(row, true);
          retValue = true;
          break;
        }
      }
      return retValue;
    }
    #endregion
    #endregion

    #region Action Methods

    #region DocGenGroup

    // Performs the default list action.
    /// <include path='items/DoDefault/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoDefaultGroup()
    {
      if (LJCIsSelect)
      {
        DoSelectGroup();
      }
      else
      {
        DoEditGroup();
      }
    }

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoNewGroup()
    {
      DocGenGroupDetail detail;

      detail = new DocGenGroupDetail()
      {
        // Data from parent window or list.
        //LJCParentID = parentRow.LJCGetInt(Parent.ColumnID),
        //LJCParentName = parentRow.LJCGetString(Parent.ColumnName),

        LJCHelpFileName = LJCHelpFile,
        LJCHelpPageName = LJCHelpPageDetail,
      };
      detail.DocGenGroupManager.DocGenGroups = mDocGenGroupManager.DocGenGroups;
      detail.Change += new EventHandler<EventArgs>(GroupDetail_Change);
      detail.ShowDialog();
    }

    //  Displays a detail dialog to edit an existing record.
    /// <include path='items/DoEdit/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoEditGroup()
    {
      DocGenGroupDetail detail;

      if (DocGenGroupGrid.CurrentRow is LJCGridRow row)
      {
        detail = new DocGenGroupDetail()
        {
          LJCID = row.LJCGetString(DocGenGroup.ColumnName),

          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = LJCHelpPageDetail,
        };
        detail.DocGenGroupManager.DocGenGroups = mDocGenGroupManager.DocGenGroups;
        detail.Change += new EventHandler<EventArgs>(GroupDetail_Change);
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    /// <include path='items/Detail_Change/*' file='../../LJCDocLib/Common/List.xml'/>
    void GroupDetail_Change(object sender, EventArgs e)
    {
      DocGenGroupDetail detail;

      detail = sender as DocGenGroupDetail;
      if (detail.LJCIsUpdate)
      {
        if (UpdateGroup(detail.LJCRecord))
        {
          mDocGenGroupManager.Save();
          RowUpdateGroup(detail.LJCRecord);
        }
      }
      else
      {
        if (AddGroup(detail.LJCRecord))
        {
          TimedChange(Change.DocGenGroup);
        }
      }
    }

    // Deletes the selected row.
    /// <include path='items/DoDelete/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoDeleteGroup()
    {
      string title;
      string message;

      if (DocGenGroupGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = "Are you sure you want to delete the selected item?";
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          var keyObject = new DocGenGroup()
          {
            Name = row.LJCGetString(DocGenGroup.ColumnName)
          };
          mDocGenGroupManager.DeleteGroup(keyObject);
          //if (NetString.HasValue(mDocGenGroupManager.ErrorText))
          //{
          //	message = "Unable to delete the selected item.\r\n"
          //	+ "There may be attached items or referencing items.";
          //	MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
          //		, MessageBoxIcon.Exclamation);
          //}
          //else
          //{
          if (mDocGenGroupManager.Save())
          {
            DocGenGroupGrid.Rows.Remove(row);
            TimedChange(Change.DocGenGroup);
          }
          //}
        }
      }
    }

    // Refreshes the list.
    /// <include path='items/DoRefresh/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoRefreshGroup()
    {
      DocGenGroup record;
      string id = null;

      if (DocGenGroupGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetString(DocGenGroup.ColumnName);
      }
      DataRetrieveGroup();

      // Select the original row.
      if (NetString.HasValue(id))
      {
        record = new DocGenGroup()
        {
          Name = id
        };
        RowSelectGroup(record);
      }
    }

    // Sets the selected item and returns to the parent form.
    /// <include path='items/DoSelect/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoSelectGroup()
    {
      DocGenGroup record;
      string name;

      LJCSelectedRecord = null;
      if (DocGenGroupGrid.CurrentRow is LJCGridRow row)
      {
        name = row.LJCGetString(DocGenGroup.ColumnName);
        record = mDocGenGroupManager.SearchName(name);
        if (record != null)
        {
          LJCSelectedRecord = record;
        }
      }
      DialogResult = DialogResult.OK;
    }

    // Resequences the groups.
    private void DoSequenceGroups()
    {
      SequenceGroups();
      mDocGenGroupManager.Save();
      DoRefreshGroup();
    }
    #endregion

    #region DocGenAssembly

    // Performs the default list action.
    /// <include path='items/DoDefault/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoDefaultAssembly()
    {
      if (LJCIsSelect)
      {
        DoSelectAssembly();
      }
      else
      {
        DoEditAssembly();
      }
    }

    // Displays a detail dialog for a new record.
    /// <include path='items/DoNew/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoNewAssembly()
    {
      DocGenAssemblyDetail detail;

      if (DocGenGroupGrid.CurrentRow is LJCGridRow parentRow
        && DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        detail = new DocGenAssemblyDetail()
        {
          // Data from parent window or list.
          LJCParentID = parentRow.LJCGetString(DocGenGroup.ColumnName),
          LJCParentName = parentRow.LJCGetString(DocGenGroup.ColumnName),

          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = "DocGenAssemblyDetail.htm"
        };
        detail.DocGenGroupManager.DocGenGroups = mDocGenGroupManager.DocGenGroups;
        detail.Change += new EventHandler<EventArgs>(AssemblyDetail_Change);
        detail.ShowDialog();
      }
    }

    //  Displays a detail dialog to edit an existing record.
    /// <include path='items/DoEdit/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoEditAssembly()
    {
      DocGenAssemblyDetail detail;

      if (DocGenGroupGrid.CurrentRow is LJCGridRow parentRow
        && DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        detail = new DocGenAssemblyDetail()
        {
          LJCID = row.LJCGetString(DocGenAssembly.ColumnName),

          // Data from parent window or list.
          LJCParentID = parentRow.LJCGetString(DocGenGroup.ColumnName),
          LJCParentName = parentRow.LJCGetString(DocGenGroup.ColumnName),

          LJCHelpFileName = LJCHelpFile,
          LJCHelpPageName = "DocGenAssemblyDetail.htm"
        };
        detail.DocGenGroupManager.DocGenGroups = mDocGenGroupManager.DocGenGroups;
        detail.Change += new EventHandler<EventArgs>(AssemblyDetail_Change);
        detail.ShowDialog();
      }
    }

    // Adds new row or updates existing row with changes from the detail dialog.
    /// <include path='items/Detail_Change/*' file='../../LJCDocLib/Common/List.xml'/>
    void AssemblyDetail_Change(object sender, EventArgs e)
    {
      DocGenAssemblyDetail detail;

      detail = sender as DocGenAssemblyDetail;
      if (detail.LJCIsUpdate)
      {
        if (UpdateAssembly(detail.LJCParentName, detail.LJCRecord
          , detail.PreviousName))
        {
          mDocGenGroupManager.Save();
          RowUpdateAssembly(detail.LJCRecord);
        }
      }
      else
      {
        if (AddAssembly(detail.LJCParentName, detail.LJCRecord))
        {
          TimedChange(Change.DocGenAssembly);
        }
      }
    }

    // Deletes the selected row.
    /// <include path='items/DoDelete/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoDeleteAssembly()
    {
      string title;
      string message;

      if (DocGenGroupGrid.CurrentRow is LJCGridRow parentRow
        && DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        title = "Delete Confirmation";
        message = "Are you sure you want to delete the selected item?";
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.Yes)
        {
          string parentName = parentRow.LJCGetString(DocGenGroup.ColumnName);
          var keyObject = new DocGenAssembly()
          {
            Name = row.LJCGetString(DocGenAssembly.ColumnName)
          };
          mDocGenGroupManager.DeleteAssembly(parentName, keyObject);
          //if (NetString.HasValue(mDocGenGroupManager.ErrorText))
          //{
          //	message = "Unable to delete the selected item.\r\n"
          //		+ "There may be attached items or referencing items.";
          //	MessageBox.Show(message, "Delete Error", MessageBoxButtons.OK
          //		, MessageBoxIcon.Exclamation);
          //}
          //else
          //{
          if (mDocGenGroupManager.Save())
          {
            DocAssemblyGrid.Rows.Remove(row);
            TimedChange(Change.DocGenAssembly);
          }
          //}
        }
      }
    }

    // Refreshes the list.
    /// <include path='items/DoRefresh/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoRefreshAssembly()
    {
      DocGenAssembly record;
      string id = null;

      if (DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        id = row.LJCGetString(DocGenAssembly.ColumnName);
      }
      DataRetrieveAssembly();

      // Select the original row.
      if (NetString.HasValue(id))
      {
        record = new DocGenAssembly()
        {
          Name = id
        };
        RowSelectAssembly(record);
      }
    }

    // Sets the selected item and returns to the parent form.
    /// <include path='items/DoSelect/*' file='../../LJCDocLib/Common/List.xml'/>
    private void DoSelectAssembly()
    {
      DocGenAssembly record;

      LJCSelectedAssembly = null;
      if (DocGenGroupGrid.CurrentRow is LJCGridRow parentRow
        && DocAssemblyGrid.CurrentRow is LJCGridRow row)
      {
        var groupName = parentRow.LJCGetString(DocGenGroup.ColumnName);
        var name = row.LJCGetString(DocGenAssembly.ColumnName);
        record = mDocGenGroupManager.SearchNameAssembly(groupName, name);
        if (record != null)
        {
          LJCSelectedAssembly = record;
        }
      }
      DialogResult = DialogResult.OK;
    }

    // Resequences the doc assemblies.
    private void DoSequenceAssemblies()
    {
      if (DocGenGroupGrid.CurrentRow is LJCGridRow parentRow)
      {
        var name = parentRow.LJCGetString(DocGenGroup.ColumnName);
        var group = mDocGenGroupManager.SearchName(name);
        if (group != null)
        {
          SequenceAssemblies(group);
          mDocGenGroupManager.Save();
          DoRefreshAssembly();
        }
      }
    }
    #endregion
    #endregion

    #region Item Change Processing

    // Execute the list and related item functions.
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          ConfigureControls();
          RestoreControlValues();

          // Load first List.
          LoadControlData();
          break;

        case Change.File:
          DataRetrieveGroup();
          break;

        case Change.DocGenGroup:
          DataRetrieveAssembly();
          DocGenGroupGrid.LJCSetLastRow();
          break;

        case Change.DocGenAssembly:
          DocAssemblyGrid.LJCSetLastRow();
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      File,
      DocGenGroup,
      DocGenAssembly
    }

    #region Item Change Support

    // Start the Change processing.
    private void StartChangeProcessing()
    {
      ChangeTimer = new ChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Starts the Timer with the Change value.
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Gets or sets the ChangeTimer object.
    internal ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      if (AutoScaleMode == AutoScaleMode.Font)
      {
        FileCombo.Top -= 1;
        MainSplit.SplitterWidth = 4;
      }
    }

    // Configures the controls and loads the selection control data.
    private bool InitializeControls()
    {
      bool retValue = false;

      // Initialize Class Data.
      Cursor = Cursors.WaitCursor;
      mDocGenGroupManager = new DocGenGroupManager();
      if (mDocGenGroupManager != null)
      {
        retValue = true;
      }

      // Configure controls.
      if (LJCIsSelect)
      {
        // This is a Selection List.
        Text = "Group Selection";
        GroupMenuEdit.ShortcutKeyDisplayString = "";
        GroupMenuEdit.ShortcutKeys = ((Keys)(Keys.Control | Keys.E));
      }
      else
      {
        // This is a display list.
        Text = "Group List";
        GroupMenuSelect.Visible = false;
      }

      // Set initial control values.
      NetFile.CreateFolder("ControlValues");
      mControlValuesFileName = @"ControlValues\DocGenGroup.xml";

      if (retValue)
      {
        SetupGrids();
        StartChangeProcessing();
      }

      Cursor = Cursors.Default;
      return retValue;
    }

    #region Setup Support

    // Loads the initial Control data.
    private void LoadControlData()
    {
      string folder = Directory.GetCurrentDirectory();
      string[] files = Directory.GetFiles(folder, "*.xml");
      foreach (string fileName in files)
      {
        var name = Path.GetFileName(fileName);
        FileCombo.Items.Add(name);
      }
      if (FileCombo.Items.Count > 0)
      {
        FileCombo.SelectedIndex = 0;
      }
    }

    // Setup the data grids.
    private void SetupGrids()
    {
      SetupGridGroup();
      SetupGridAssembly();
    }

    // Setup the DocGenGroup grid.
    private void SetupGridGroup()
    {
      DocGenGroupGrid.BackgroundColor = mBeginColor;

      // Setup default display columns if no columns are defined.
      if (0 == DocGenGroupGrid.Columns.Count)
      {
        // Done this way because there is no database table.
        DocGenGroupGrid.LJCAddColumn(DocGenGroup.ColumnSequence, "Sequence");
        DocGenGroupGrid.LJCAddColumn(DocGenGroup.ColumnName, "Name");
        DocGenGroupGrid.LJCAddColumn(DocGenGroup.ColumnDescription, "Description");
      }
    }

    // Setup the DocAssembly grid.
    private void SetupGridAssembly()
    {
      DocAssemblyGrid.BackgroundColor = mBeginColor;

      // Setup default display columns if no columns are defined.
      if (0 == DocAssemblyGrid.Columns.Count)
      {
        // Done this way because there is no database table.
        DocAssemblyGrid.LJCAddColumn(DocGenAssembly.ColumnSequence, "Sequence");
        DocAssemblyGrid.LJCAddColumn(DocGenAssembly.ColumnName, "Name");
        DocAssemblyGrid.LJCAddColumn(DocGenAssembly.ColumnDescription, "Description");
      }
    }

    // Saves the control values.
    private void SaveControlValues()
    {
      ControlValues controlValues = new ControlValues();

      // Save Grid Column values.
      DocGenGroupGrid.LJCSaveColumnValues(controlValues);
      DocAssemblyGrid.LJCSaveColumnValues(controlValues);

      // Save Splitter values.
      controlValues.Add("DocGenGroupSplit.SplitterDistance", 0, 0, 0
        , MainSplit.SplitterDistance);

      // Save Window values.
      controlValues.Add(Name, Left, Top, Width, Height);

      NetCommon.XmlSerialize(controlValues.GetType(), controlValues, null
        , mControlValuesFileName);
    }

    // Restores the control values.
    private void RestoreControlValues()
    {
      ControlValue controlValue;

      if (File.Exists(mControlValuesFileName))
      {
        ControlValues = NetCommon.XmlDeserialize(typeof(ControlValues)
          , mControlValuesFileName) as ControlValues;

        if (ControlValues != null)
        {
          // Restore Window values.
          controlValue = ControlValues.LJCSearchName(Name);
          if (controlValue != null)
          {
            Left = controlValue.Left;
            Top = controlValue.Top;
            Width = controlValue.Width;
            Height = controlValue.Height;
          }

          // Restore Splitter, Grid and other values.
          FormCommon.RestoreSplitDistance(MainSplit, ControlValues);

          DocGenGroupGrid.LJCRestoreColumnValues(ControlValues);
          DocAssemblyGrid.LJCRestoreColumnValues(ControlValues);
        }
      }
    }

    /// <summary>Gets or sets the ControlValues item.</summary>
    public ControlValues ControlValues { get; set; }
    #endregion
    #endregion

    #region Private Methods

    #region Group

    // Adds the new record.
    // <include path='items/AddDocGenGroup/*' file='Doc/DocGenGroupList.xml'/>
    private bool AddGroup(DocGenGroup record)
    {
      DocGenGroup source;
      bool retValue = true;

      source = mDocGenGroupManager.DocGenGroups.LJCSearchName(record.Name);
      if (source != null)
      {
        retValue = false;
        string title = "Data Entry Error";
        string message = "The record already exists.";
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        mDocGenGroupManager.DocGenGroups.Add(record);
        MoveGroups(source, record);
      }
      return retValue;
    }

    // Updates the record.
    // <include path='items/UpdateDocGenGroup/*' file='Doc/DocGenGroupList.xml'/>
    private bool UpdateGroup(DocGenGroup record)
    {
      bool retValue = true;

      var source = mDocGenGroupManager.SearchName(record.Name);
      if (null == source)
      {
        retValue = false;
        string title = "Data Entry Error";
        string message = "The record was not found.";
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        source.Description = record.Description;
        MoveGroups(source, record);
      }
      return retValue;
    }

    // Move sequences if required.
    private void MoveGroups(DocGenGroup source, DocGenGroup target)
    {
      var isAddedItem = false;

      // Record is to be added.
      if (null == source)
      {
        // Default sequence to target to prevent updates if no groups.
        source = new DocGenGroup()
        {
          Sequence = target.Sequence
        };

        var docGroups = mDocGenGroupManager.DocGenGroups;
        if (docGroups != null && docGroups.Count > 0)
        {
          // Set source to end of groups.
          isAddedItem = true;
          source.Sequence = docGroups.Count;
        }
      }

      if (target.Sequence != source.Sequence)
      {
        if (target.Sequence > source.Sequence)
        {
          MoveGroupsUp(source, target);
        }
        else
        {
          if (target.Sequence < source.Sequence)
          {
            MoveGroupsDown(source, target, isAddedItem);
          }
        }
        SequenceGroups();
        mDocGenGroupManager.Save();
        DataRetrieveGroup();
        RowSelectGroup(target);
      }
    }

    // The target sequence is less than the source sequence.
    // Move sequences >= target and < source down to make room for
    // the new sequence.
    private void MoveGroupsDown(DocGenGroup source, DocGenGroup target
      , bool isAddedItem = false)
    {
      var docGroups = mDocGenGroupManager.DocGenGroups;
      var updateGroups = docGroups.FindAll(x => x.Sequence >= target.Sequence
        && x.Sequence < source.Sequence);
      if (updateGroups != null && updateGroups.Count > 0)
      {
        // Sort in descending order.
        updateGroups.Sort((x, y) => y.Sequence.CompareTo(x.Sequence));
        foreach (DocGenGroup updateGroup in updateGroups)
        {
          // Do not update the sequence for the added item.
          if (false == isAddedItem
            || (isAddedItem && updateGroup.Name != target.Name))
          {
            updateGroup.Sequence++;
          }
        }

        // Update the sequence for the updated item only.
        if (false == isAddedItem)
        {
          source.Sequence = target.Sequence;
        }
      }
    }

    // The target sequence is greater than the source sequence.
    // Move sequences <= target and > source up to make room for
    // the new sequence.
    private void MoveGroupsUp(DocGenGroup source, DocGenGroup target)
    {
      var docGroups = mDocGenGroupManager.DocGenGroups;
      var updateGroups = docGroups.FindAll(x => x.Sequence <= target.Sequence
        && x.Sequence > source.Sequence);
      if (updateGroups != null && updateGroups.Count > 0)
      {
        // Sort in ascending order.
        updateGroups.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
        foreach (DocGenGroup updateGroup in updateGroups)
        {
          updateGroup.Sequence--;
        }
        source.Sequence = target.Sequence;
      }
    }

    // Resequence the groups.
    private void SequenceGroups()
    {
      var groups = mDocGenGroupManager.DocGenGroups;
      if (groups != null && groups.Count > 0)
      {
        // Sort in ascending order.
        groups.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
        int sequence = 1;
        foreach (DocGenGroup group in groups)
        {
          group.Sequence = sequence;
          sequence++;
        }
      }
    }
    #endregion

    #region Assembly

    // Adds the new record.
    // <include path='items/AddDocGenAssembly/*' file='Doc/DocGenGroupList.xml'/>
    private bool AddAssembly(string groupName, DocGenAssembly record)
    {
      bool retValue = true;

      var docGroup = mDocGenGroupManager.DocGenGroups.LJCSearchName(groupName);
      if (null == docGroup)
      {
        retValue = false;
        string title = "Data Entry Error";
        string message = "The DocGroup record was not found.";
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        var source = mDocGenGroupManager.SearchNameAssembly(groupName
          , record.Name);
        if (source != null)
        {
          retValue = false;
          string title = "Data Entry Error";
          string message = "The record already exists.";
          MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
          docGroup.DocGenAssemblies.Add(record);
          MoveAssemblies(docGroup, source, record);
        }
      }
      return retValue;
    }

    // Updates the record.
    // <include path='items/UpdateDocGenAssembly/*' file='Doc/DocGenGroupList.xml'/>
    private bool UpdateAssembly(string groupName, DocGenAssembly record
      , string previousName)
    {
      bool retValue = true;

      var docGroup = mDocGenGroupManager.SearchName(groupName);
      if (null == docGroup)
      {
        retValue = false;
        string title = "Data Entry Error";
        string message = "The DocGroup record was not found.";
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
      else
      {
        var source = mDocGenGroupManager.SearchNameAssembly(groupName
          , previousName);
        if (null == source)
        {
          retValue = false;
          string title = "Data Entry Error";
          string message = "The DocAssembly record was not found.";
          MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
          source.Name = record.Name;
          source.Description = record.Description;
          source.FileSpec = record.FileSpec;
          source.MainImage = record.MainImage;
          MoveAssemblies(docGroup, source, record);
        }
      }
      return retValue;
    }

    // Move sequences if required.
    private void MoveAssemblies(DocGenGroup sourceGroup
      , DocGenAssembly source, DocGenAssembly target)
    {
      var isAddedItem = false;

      // Record is to be added.
      if (null == source)
      {
        // Default sequence to target to prevent updates if no assemblies.
        source = new DocGenAssembly()
        {
          Sequence = target.Sequence
        };

        var docAssemblies = sourceGroup.DocGenAssemblies;
        if (docAssemblies != null && docAssemblies.Count > 0)
        {
          // Set source to end of groups.
          isAddedItem = true;
          source.Sequence = docAssemblies.Count;
        }
      }

      if (target.Sequence != source.Sequence)
      {
        if (target.Sequence > source.Sequence)
        {
          MoveAssembliesUp(sourceGroup, source, target);
        }
        else
        {
          if (target.Sequence < source.Sequence)
          {
            MoveAssembliesDown(sourceGroup, source, target, isAddedItem);
          }
        }
        SequenceAssemblies(sourceGroup);
        mDocGenGroupManager.Save();
        DataRetrieveAssembly();
        RowSelectAssembly(target);
      }
    }

    // The target sequence is less than the source sequence.
    // Move sequences >= target and < source down to make room for
    // the new sequence.
    private void MoveAssembliesDown(DocGenGroup sourceGroup
      , DocGenAssembly source, DocGenAssembly target, bool isAddedItem = false)
    {
      var docAssemblies = sourceGroup.DocGenAssemblies;
      var updateAssemblies = docAssemblies.FindAll(x =>
        x.Sequence >= target.Sequence && x.Sequence < source.Sequence);
      if (updateAssemblies != null && updateAssemblies.Count > 0)
      {
        // Sort in descending order.
        updateAssemblies.Sort((x, y) => y.Sequence.CompareTo(x.Sequence));
        foreach (DocGenAssembly updateAssembly in updateAssemblies)
        {
          // Do not update the sequence for the added item.
          if (false == isAddedItem
            || (isAddedItem && updateAssembly.Name != target.Name))
          {
            updateAssembly.Sequence++;
          }
        }

        // Update the sequence for the updated item only.
        if (false == isAddedItem)
        {
          source.Sequence = target.Sequence;
        }
      }
    }

    // The target sequence is greater than the source sequence.
    // Move sequences <= target and > source up to make room for
    // the new sequence.
    private void MoveAssembliesUp(DocGenGroup sourceGroup
      , DocGenAssembly source, DocGenAssembly target)
    {
      var docAssemblies = sourceGroup.DocGenAssemblies;
      var updateAssemblies = docAssemblies.FindAll(x =>
        x.Sequence <= target.Sequence && x.Sequence > source.Sequence);
      if (updateAssemblies != null && updateAssemblies.Count > 0)
      {
        // Sort in ascending order.
        updateAssemblies.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
        foreach (DocGenAssembly updateAssembly in updateAssemblies)
        {
          updateAssembly.Sequence--;
        }
        source.Sequence = target.Sequence;
      }
    }

    // Resequence the doc assemblies.
    private void SequenceAssemblies(DocGenGroup group)
    {
      if (group != null)
      {
        var docAssemblies = group.DocGenAssemblies;
        if (docAssemblies != null && docAssemblies.Count > 0)
        {
          // Sort in ascending order.
          docAssemblies.Sort((x, y) => x.Sequence.CompareTo(y.Sequence));
          int sequence = 1;
          foreach (DocGenAssembly docAssembly in docAssemblies)
          {
            docAssembly.Sequence = sequence;
            sequence++;
          }
        }
      }
    }
    #endregion

    // Sets the control states based on the current control values.
    /// <include path='items/SetControlState/*' file='../../LJCDocLib/Common/List.xml'/>
    private void SetControlState()
    {
      bool enableNew = true;
      bool enableEdit = DocGenGroupGrid.CurrentRow != null;
      FormCommon.SetMenuState(DocGenGroupMenu, enableNew, enableEdit);
      GroupFileEdit.Enabled = true;
    }
    #endregion

    #region Action Event Handlers

    #region DocGenGroup

    // <summary>Calls the New method.</summary>
    private void GroupMenuNew_Click(object sender, EventArgs e)
    {
      DoNewGroup();
    }

    // <summary>Calls the Edit method.</summary>
    private void GroupMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditGroup();
    }

    // <summary>Calls the Delete method.</summary>
    private void GroupMenuDelete_Click(object sender, EventArgs e)
    {
      DoDeleteGroup();
    }

    // <summary>Calls the Refresh method.</summary>
    private void GroupMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshGroup();
    }

    // <summary>Calls the Select method.</summary>
    private void GroupMenuSelect_Click(object sender, EventArgs e)
    {
      DoSelectGroup();
    }

    // Allows display and edit of text file.
    private void GroupFileEdit_Click(object sender, EventArgs e)
    {
      FormCommon.ShellFile("NotePad.exe");
    }

    // Resequences the groups.
    private void GroupMenuSequence_Click(object sender, EventArgs e)
    {
      DoSequenceGroups();
    }

    // <summary>Performs the Close function.</summary>
    private void GroupMenuClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion

    #region DocGenAssembly

    // <summary>Calls the New method.</summary>
    private void AssemblyMenuNew_Click(object sender, EventArgs e)
    {
      DoNewAssembly();
    }

    // <summary>Calls the Edit method.</summary>
    private void AssemblyMenuEdit_Click(object sender, EventArgs e)
    {
      DoEditAssembly();
    }

    // <summary>Calls the Delete method.</summary>
    private void AssemblyMenuDelete_Click(object sender, EventArgs e)
    {
      DoDeleteAssembly();
    }

    // <summary>Calls the Refresh method.</summary>
    private void AssemblyMenuRefresh_Click(object sender, EventArgs e)
    {
      DoRefreshAssembly();
    }

    // <summary>Calls the Select method.</summary>
    private void AssemblyMenuSelect_Click(object sender, EventArgs e)
    {
      DoSelectAssembly();
    }

    // Resequences the doc assemblies.
    private void DocAssemblyMenuSequence_Click(object sender, EventArgs e)
    {
      DoSequenceAssemblies();
    }
    #endregion
    #endregion

    #region Control Event Handlers

    // Handles the SelectionIndexChanged event.
    private void FileCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      string fileName = FileCombo.Text;
      mDocGenGroupManager = new DocGenGroupManager
      {
        FileName = fileName
      };
      TimedChange(Change.File);
    }

    #region DocGenGroup

    // Handles the form keys.
    private void DocGenGroupGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic, LJCHelpPageList);
          break;

        case Keys.F5:
          DoRefreshGroup();
          e.Handled = true;
          break;

        case Keys.Enter:
          DoDefaultGroup();
          e.Handled = true;
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            DocAssemblyGrid.Select();
          }
          else
          {
            DocAssemblyGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void DocGenGroupGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && DocGenGroupGrid.LJCIsDifferentRow(e))
      {
        DocGenGroupGrid.LJCSetCurrentRow(e);
        TimedChange(Change.DocGenGroup);
      }
    }

    // Handles the SelectionChanged event.
    private void DocGenGroupGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (DocGenGroupGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.DocGenGroup);
      }
      DocGenGroupGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void DocGenGroupGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (DocGenGroupGrid.LJCGetMouseRow(e) != null)
      {
        DoDefaultGroup();
      }
    }
    #endregion

    #region DocGenAssembly

    // Handles the form keys.
    private void DocGenAssemblyGrid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic, LJCHelpPageList);
          break;

        case Keys.F5:
          DoRefreshAssembly();
          e.Handled = true;
          break;

        case Keys.Enter:
          DoDefaultAssembly();
          e.Handled = true;
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            DocGenGroupGrid.Select();
          }
          else
          {
            DocGenGroupGrid.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDown event.
    private void DocGenAssemblyGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right
        && DocAssemblyGrid.LJCIsDifferentRow(e))
      {
        DocAssemblyGrid.LJCSetCurrentRow(e);
        TimedChange(Change.DocGenAssembly);
      }
    }

    // Handles the SelectionChanged event.
    private void DocGenAssemblyGrid_SelectionChanged(object sender, EventArgs e)
    {
      if (DocAssemblyGrid.LJCAllowSelectionChange)
      {
        TimedChange(Change.DocGenAssembly);
      }
      DocAssemblyGrid.LJCAllowSelectionChange = true;
    }

    // Handles the MouseDoubleClick event.
    private void DocGenAssemblyGrid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      if (DocAssemblyGrid.LJCGetMouseRow(e) != null)
      {
        DoDefaultAssembly();
      }
    }
    #endregion
    #endregion

    #region Public Properties

    /// <summary>Gets or sets the parent ID value.</summary>
    public int LJCParentID { get; set; }

    /// <summary>Gets or sets the LJCParentName value.</summary>
    public string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    /// <summary>Gets or sets the LJCIsSelect value.</summary>
    public bool LJCIsSelect { get; set; }

    /// <summary>Gets a reference to the selected record.</summary>
    public DocGenGroup LJCSelectedRecord { get; private set; }

    /// <summary>Gets a reference to the selected assembly record.</summary>
    public DocGenAssembly LJCSelectedAssembly { get; private set; }

    /// <summary>The help file name.</summary>
    public string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;

    /// <summary>The List help page name.</summary>
    public string LJCHelpPageList
    {
      get { return mHelpPageList; }
      set { mHelpPageList = NetString.InitString(value); }
    }
    private string mHelpPageList;

    /// <summary>The Detail help page name.</summary>
    public string LJCHelpPageDetail
    {
      get { return mHelpPageDetail; }
      set { mHelpPageDetail = NetString.InitString(value); }
    }
    private string mHelpPageDetail;
    #endregion

    #region Class Data

    private readonly Color mBeginColor = Color.AliceBlue;
    private string mControlValuesFileName;
    private DocGenGroupManager mDocGenGroupManager;
    #endregion
  }
}
