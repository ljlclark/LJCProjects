// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataDetailDialog.cs
using LJCDataDetailDAL;
using LJCDataDetailLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCDataDetail
{
  // The DataDetail Dynamic Detail dialog.
  /// <include path='items/DataDetailDialog/*' file='Doc/ProjectDataDetailDialog.xml'/>
  public partial class DataDetailDialog : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataDetailDialogC/*' file='Doc/DataDetailDialog.xml'/>
    public DataDetailDialog(string userID, string tableName)
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      mDataDetailData = new DataDetailData(tableName, userID);
      ControlDetail = mDataDetailData.GetControlDetail();
      mDataDetailCode = new DataDetailCode()
      {
        // Share configuration.
        ControlDetail = ControlDetail,
        DataDetailData = mDataDetailData
      };
      mControlCode = new ControlCode();

      SourceTabIndex = -1;
      SourceColumnIndex = -1;
      SourceRowIndex = -1;

      // Set default class data.
      // Set DAL config before using anywhere in the program.
      var configValues = ValuesDataDetail.Instance;
      var settings = configValues.StandardSettings;
      Text += $" - {settings.DataConfigName}";
      BeginColor = settings.BeginColor;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void DataDetailDialog_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      CenterToScreen();

      // Use timer to configure controls and retrieve data after form is loaded.
      mTimer.Start();
    }

    // Closes the form without saving the data.
    private void FormCancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    // <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    private void DataRetrieve()
    {
      CenterToScreen();
    }

    // Updates the data columns with the current control values.
    private void SetRecordValues()
    {
      Control control;
      string controlName;

      foreach (DbColumn dbColumn in LJCDataColumns)
      {
        bool isCombo = false;

        // If a KeyItem is found, get value from Static ComboBox.
        KeyItems items = null;
        if (LJCKeyItems != null)
        {
          items = LJCKeyItems.SearchPropertyName(dbColumn.PropertyName);
          if (items != null && items.Count > 1)
          {
            isCombo = true;
            // ToDo: Change to use PropertyName?
            controlName = ControlName(dbColumn.ColumnName, "ComboBox");
            control = GetControlWithName(controlName);
            if (control != null)
            {
              KeyItem searchItem;
              searchItem = ((ComboBox)control).SelectedItem as KeyItem;
              if (searchItem != null)
              {
                dbColumn.Value = searchItem.ID;
              }
            }
          }
        }

        if (false == isCombo)
        {
          switch (dbColumn.DataTypeName.ToLower())
          {
            case "boolean":
              // Get value from CheckBox.
              // ToDo: Change to use PropertyName?
              controlName = ControlName(dbColumn.ColumnName, "CheckBox");
              control = GetControlWithName(controlName);
              if (control != null)
              {
                dbColumn.Value = ((CheckBox)control).Checked;
              }
              break;

            default:
              // Get value from TextBox.
              // ToDo: Change to use PropertyName?
              controlName = ControlName(dbColumn.ColumnName, "TextBox");
              control = GetControlWithName(controlName);
              if (control != null)
              {
                if (items != null && 1 == items.Count)
                {
                  // Get StaticCombo or StaticKey ID.
                  dbColumn.Value = items[0].ID;
                }
                else
                {
                  dbColumn.Value = control.Text.Trim();
                }
              }
              break;
          }
        }
      }
    }
    #endregion

    #region Action Methods

    // Add a TabPage.
    private void DoTabAdd()
    {
      // Data from items.
      int newTabIndex = MainTabs.TabCount;

      var detail = new TabDetail(mDataDetailData)
      {
        LJCParentID = ControlDetail.ID,
        LJCParentName = ControlDetail.Name,
        LJCSetIndex = newTabIndex,
      };
      detail.LJCChange += Detail_LJCChange;
      detail.ShowDialog();
    }

    // Edit Tab Title
    private void DoTabEdit()
    {
      // Data from items.
      var tabIndex = CurrentTabPageIndex();
      if (tabIndex >= 0)
      {
        var manager = mDataDetailData.Managers.ControlTabManager;
        var controlTab = manager.RetrieveWithUnique(ControlDetail.ID
          , tabIndex);
        long id = controlTab.ID;

        var detail = new TabDetail(mDataDetailData)
        {
          LJCID = id,
          LJCParentID = ControlDetail.ID,
          LJCParentName = ControlDetail.Name,
        };
        detail.LJCChange += Detail_LJCChange;
        detail.ShowDialog();
      }
    }

    // Deletes the selected tab.
    private void DoTabDelete()
    {
      string message;
      bool success = true;

      var deleteTitle = "Delete Error";
      var deleteMessage = FormCommon.DeleteError;

      var tabIndex = CurrentTabPageIndex();
      if (tabIndex < 0)
      {
        success = false;
      }

      var manager = mDataDetailData.Managers.ControlTabManager;
      ControlTab controlTab = null;
      if (success)
      {
        controlTab = manager.RetrieveWithUnique(ControlDetail.ID
          , tabIndex);
        if (null == controlTab)
        {
          success = false;
        }
      }

      if (success)
      {
        var title = "Delete Confirmation";
        message = FormCommon.DeleteConfirm;
        if (MessageBox.Show(message, title, MessageBoxButtons.YesNo
          , MessageBoxIcon.Question) == DialogResult.No)
        {
          success = false;
        }
      }

      if (success && null == controlTab.ControlColumns)
      {
        success = false;
        MessageBox.Show(deleteMessage, deleteTitle, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }

      if (success)
      {
        if (tabIndex != MainTabs.TabPages.Count - 1)
        {
          int sourceIndex = tabIndex;
          tabIndex = MainTabs.TabPages.Count;
          MainTabs.LJCMoveTabPage(sourceIndex, tabIndex);
        }
      }

      if (success)
      {
        var keyColumns = manager.GetIDKey(controlTab.ID);
        manager.Delete(keyColumns);
        if (0 == manager.AffectedCount)
        {
          success = false;
          MessageBox.Show(deleteMessage, deleteTitle, MessageBoxButtons.OK
            , MessageBoxIcon.Exclamation);
        }
      }

      if (success)
      {
        MainTabs.TabPages.RemoveAt(tabIndex);
      }
    }

    // Updates tab with changes from the detail dialog.
    private void Detail_LJCChange(object sender, EventArgs e)
    {
      var detail = sender as TabDetail;
      if (detail.LJCRecord != null)
      {
        var dataRecord = detail.LJCRecord;
        if (detail.LJCIsUpdate)
        {
          var tabPage = MainTabs.SelectedTab;
          if (detail.LJCTabOriginalIndex != dataRecord.TabIndex)
          {
            MainTabs.LJCMoveTabPage(detail.LJCTabOriginalIndex
              , dataRecord.TabIndex);
          }
          tabPage.Text = dataRecord.Caption;
        }
        else
        {
          var tabPage = new TabPage(dataRecord.Caption)
          {
            BackColor = BeginColor
          };
          tabPage.MouseMove += Page_MouseMove;
          MainTabs.TabPages.Insert(dataRecord.TabIndex, tabPage);
          ControlDetail.ControlTabItems.Add(dataRecord.ID
            , dataRecord.ControlDetailID, dataRecord.TabIndex);
        }
      }
    }

    // Selects the copy/move row.
    private void DoSelect(string type)
    {
      CopyAction = type;
      SourceTabIndex = CurrentTabIndex;
      SourceColumnIndex = CurrentColumnIndex;
      SourceRowIndex = CurrentRowIndex;
      SetStatusText();
    }

    // Pastes the selected row.
    private void DoPaste()
    {
      if (CopyAction != null)
      {
        var config = ControlDetail;

        var targetTabIndex = CurrentTabIndex;
        var targetColumnIndex = CurrentColumnIndex;
        var targetRowIndex = CurrentRowIndex;

        var targetTab = config.ControlTabItems[targetTabIndex];

        var targetColumn = targetTab.ControlColumns[targetColumnIndex];
        if (targetColumn.ControlRows.Count >= config.ColumnRowsLimit)
        {
          InsertBlankRow(targetTabIndex, targetColumnIndex
            , targetRowIndex);
        }

        var label = GetLabel(SourceTabIndex, SourceColumnIndex
          , SourceRowIndex);
        Point location = LabelLocation(targetTabIndex, targetColumnIndex
          , targetRowIndex);
        label.Parent = MainTabs.TabPages[targetTabIndex];
        label.Location = location;

        var control = GetControl(SourceTabIndex, SourceColumnIndex
          , SourceRowIndex);
        location = ControlLocation(targetTabIndex, targetColumnIndex
          , targetRowIndex);
        control.Parent = MainTabs.TabPages[targetTabIndex];
        control.Location = location;

        if ("Move" == CopyAction)
        {
          ClearSelectAction();
        }
      }
      SetStatusText();
    }
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      var config = ControlDetail;
      var controlTabItems = config.ControlTabItems;
      if (false == NetCommon.HasItems(controlTabItems))
      {
        // Create new configuration.
        config.ControlRowHeight = ControlRowHeight(config.ControlRowHeight);
        mDataDetailCode.NewControlData(LJCDataColumns, LJCKeyItems);
      }

      // Always calculate width and height?
      config.ContentWidth = mDataDetailCode.ContentWidth();
      config.ContentHeight
        = mDataDetailCode.ContentHeight(config.ColumnRowCount);
      mDataDetailData.UpdateControlDetail(config);

      CreateControls();
      MainTabs.Width = ClientSize.Width;
      foreach (TabPage page in MainTabs.TabPages)
      {
        page.BackColor = BeginColor;
      }
      BackColor = BeginColor;

      // Get the MainTabs border values.
      TabPage tabPage = MainTabs.TabPages[0];
      Size tabBorders = new Size(MainTabs.Width - tabPage.Width
        , MainTabs.Height - tabPage.Height);

      // Get Client height border value.
      int clientBorderHeight = ClientSize.Height - MainTabs.Height;

      // Set the MainTabs size to the Content size plus border values.
      MainTabs.Size = new Size(config.ContentWidth + tabBorders.Width
        , config.ContentHeight - 2 + tabBorders.Height);

      // Set the ClientSize to the MainTabs plus the border values.
      // Add some extra above the buttons.
      ClientSize = new Size(MainTabs.Width - 1, MainTabs.Height
        + clientBorderHeight + 2);
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      if (null == LJCDataColumns || 0 == LJCDataColumns.Count)
      {
        throw new MissingMemberException(Name, "LJCDataColumns");
      }

      // Let form finish loading then Configure controls.
      mTimer = new Timer()
      {
        Interval = 50
      };
      mTimer.Tick += Timer_Tick;
    }

    // The timer event handler.
    private void Timer_Tick(object sender, EventArgs e)
    {
      mTimer.Stop();
      ConfigureControls();
      DataRetrieve();
    }
    private Timer mTimer;
    #endregion

    #region Private Methods

    // Clear the copy action.
    private void ClearSelectAction()
    {
      SourceTabIndex = -1;
      SourceColumnIndex = -1;
      SourceRowIndex = -1;
      CopyAction = null;
      SetStatusText();
    }

    // Gets the Current TabIndex.
    private int CurrentTabPageIndex()
    {
      int retValue = -1;

      if (MainTabs.SelectedTab != null)
      {
        var tabPage = MainTabs.SelectedTab;
        retValue = MainTabs.LJCGetTabPageIndex(tabPage);
      }
      return retValue;
    }

    // Checks the values required for the SelectList window.
    private bool CheckSelectListValues(string buttonName, out KeyItem keyItem)
    {
      int index = 0;
      string message = null;
      bool retValue = true;

      keyItem = null;

      if (null == mDataDetailData.DbServiceRef)
      {
        retValue = false;
        message = "Missing Data Service Reference.";
      }

      if (retValue)
      {
        if (false == NetString.HasValue(ControlDetail.DataConfigName))
        {
          retValue = false;
          message = "Missing Data Config Name.";
        }
      }

      if (retValue)
      {
        index = buttonName.IndexOf("Button");
        if (index < 0)
        {
          retValue = false;
          message = "Sender did not contain the suffix 'Button'.";
        }
      }

      if (retValue)
      {
        string name = buttonName.Substring(0, index);
        var keyItems = LJCKeyItems.SearchPropertyName(name);
        if (keyItems != null && 1 == keyItems.Count)
        {
          keyItem = keyItems[0];

          message = "";
          if (false == NetString.HasValue(keyItem.TableName))
          {
            retValue = false;
            message += "Conrol Item is missing TableName.\r\n";
          }

          if (false == NetString.HasValue(keyItem.PrimaryKeyName))
          {
            retValue = false;
            message += "Conrol Item is missing PrimaryKeyName.\r\n";
          }

          if (keyItem.ID < 1)
          {
            retValue = false;
            message += "Conrol Item is missing ID.";
          }
        }
      }

      if (NetString.HasValue(message))
      {
        string title = "Display SelectList Error";
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      return retValue;
    }

    // Gets the KeyItem Property text.
    private string KeyPropertyText(string propertyName)
    {
      string retValue = null;

      var items = LJCKeyItems.SearchPropertyName(propertyName);
      if (items != null && 1 == items.Count)
      {
        retValue = items[0].Description.Trim();
      }
      return retValue;
    }

    // Inserts a blank row.
    private void InsertBlankRow(int targetTabIndex, int targetColumnIndex
      , int targetRowIndex)
    {
      var config = ControlDetail;

      // Get source ControlTab.
      var targetControlTab = config.ControlTabItems[targetTabIndex];
      var targetControlColumn = targetControlTab.ControlColumns[targetColumnIndex];

      // Extend form size for one more control row.
      int insertHeight = config.ControlRowHeight + config.ControlRowSpacing;
      MainTabs.Height += config.ControlRowHeight + config.ControlRowSpacing;
      ClientSize = new Size(ClientSize.Width, ClientSize.Height + insertHeight);

      Control control;
      int count = targetControlColumn.ControlRows.Count;
      for (int index = targetRowIndex; index < count; index++)
      {
        var controlRow = targetControlColumn.ControlRows[index];
        var dataValueName = controlRow.DataValueName;

        // Get label.
        var controlName = ControlName(dataValueName, "Label");
        control = GetControlWithName(controlName);
        if (control != null)
        {
          control.Top += insertHeight;
        }

        // Get control.
        control = GetControlWithProperty(dataValueName);
        if (control != null)
        {
          control.Top += insertHeight;
        }
      }
    }

    // Sets the Status text.
    private void SetStatusText(string prefix = null)
    {
      var text = "";
      if (prefix != null)
      {
        text += prefix;
      }
      text += $"{CurrentTabIndex}:{CurrentColumnIndex}:{CurrentRowIndex}";
      if (SourceTabIndex >= 0
        || SourceColumnIndex >= 0
        || SourceRowIndex >= 0)
      {
        text += $" - {CopyAction} ";
      }
      if (SourceTabIndex >= 0)
      {
        text += $"{SourceTabIndex}";
      }
      if (SourceColumnIndex >= 0)
      {
        text += $":{SourceColumnIndex}";
      }
      if (SourceRowIndex >= 0)
      {
        text += $":{SourceRowIndex}";
      }
      StatusLabel.Text = text;
    }
    #endregion

    #region Action Event Handlers

    // Add Tab
    private void DetailTabAdd_Click(object sender, EventArgs e)
    {
      DoTabAdd();
    }

    // Edit Tab Title
    private void DetailTabEdit_Click(object sender, EventArgs e)
    {
      DoTabEdit();
    }

    // Delete Tab
    private void DetailTabDelete_Click(object sender, EventArgs e)
    {
      DoTabDelete();
    }

    // Select the "Move" row.
    private void DetailSelectMoveRow_Click(object sender, EventArgs e)
    {
      DoSelect("Move");
    }

    // Pastes the selected row.
    private void DetailPasteRow_Click(object sender, EventArgs e)
    {
      DoPaste();
    }

    // Clears the "Select" action.
    private void DetailClearSelect_Click(object sender, EventArgs e)
    {
      ClearSelectAction();
    }
    #endregion

    #region Control Event Handlers

    #region DataDetailDialog

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
    protected void LJCOnChange()
    {
      LJCChange?.Invoke(this, new EventArgs());
    }

    // Saves the data and closes the form.
    private void OKButton_Click(object sender, EventArgs e)
    {
      SetRecordValues();
      LJCOnChange();

      DialogResult = DialogResult.OK;
      Close();
    }
    #endregion

    #region MainTabs Event Handlers

    // Sets the current Column and Row index.
    private void Control_MouseMove(object sender, MouseEventArgs e)
    {
      Control control = sender as Control;
      int x = e.X + control.Left;
      int y = e.Y + control.Top;

      CurrentTabIndex = CurrentTabPageIndex();
      CurrentColumnIndex = mDataDetailCode.GetColumnIndex(CurrentTabIndex, x);
      CurrentRowIndex = mDataDetailCode.GetRowIndex(y);
      SetStatusText();
    }

    // Displays the selection list window.
    private void EllipseButton_Click(object sender, EventArgs e)
    {
      Button button;
      bool success;

      button = sender as Button;
      //success = CheckSelectListValues(LJCDbServiceRef, button.Name
      //	, out KeyItem keyItem);
      success = CheckSelectListValues(button.Name, out KeyItem keyItem);

      if (success)
      {
        SelectList selectList = new SelectList()
        {
          LJCDataConfigName = ControlDetail.DataConfigName,
          LJCDbServiceRef = mDataDetailData.DbServiceRef,
          LJCID = (int)keyItem.ID,
          LJCPrimaryKeyName = keyItem.PrimaryKeyName,
          LJCTableName = keyItem.TableName
        };
        if (DialogResult.OK == selectList.ShowDialog())
        {
          keyItem.ID = selectList.LJCID;
        }
      }
    }

    // Handles the MouseDown event.
    private void MainTabs_MouseDown(object sender, MouseEventArgs e)
    {
      var tabPage = MainTabs.LJCGetTabPage(e.X, e.Y);
      if (tabPage != null)
      {
        MainTabs.SelectedTab = tabPage;
      }
    }

    // Sets the current Column and Row index.
    private void Page_MouseMove(object sender, MouseEventArgs e)
    {
      if (mDataDetailCode != null)
      {
        CurrentTabIndex = CurrentTabPageIndex();
        CurrentColumnIndex = mDataDetailCode.GetColumnIndex(CurrentTabIndex, e.X);
        CurrentRowIndex = mDataDetailCode.GetRowIndex(e.Y);
        SetStatusText();
      }
    }
    #endregion
    #endregion

    #region KeyEdit Event Handlers

    // Only allows numbers or edit keys.
    private void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
    }
    #endregion

    #region Properties

    // Gets a reference to the record object.
    /// <include path='items/LJCDataColumns/*' file='Doc/DataDetailDialog.xml'/>
    public DbColumns LJCDataColumns { get; set; }

    /// <summary>Gets the LJCIsUpdate value.</summary>
    public bool LJCIsUpdate { get; set; }

    // Gets or sets the ControlItems collection.
    /// <include path='items/LJCKeyItems/*' file='Doc/DataDetailDialog.xml'/>
    public KeyItems LJCKeyItems { get; set; }
    #endregion

    #region Private Properties

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // The configuration data.
    private ControlDetail ControlDetail
    {
      get { return mControlDetail; }
      set
      {
        if (value != null)
        {
          mControlDetail = value;
        }
      }
    }
    private ControlDetail mControlDetail;

    // The Copy action text.
    private string CopyAction { get; set; }

    // The current mouse cursor ControlColumn index.
    private int CurrentColumnIndex { get; set; }

    // The current mouse cursor ControlRow index.
    private int CurrentRowIndex { get; set; }

    // The current mouse cursor ControlTab index.
    private int CurrentTabIndex { get; set; }

    // The source ControlColumn index.
    private int SourceColumnIndex { get; set; }

    // The source ControlRow index.
    private int SourceRowIndex { get; set; }

    // The source ControlTab index.
    private int SourceTabIndex { get; set; }
    #endregion

    #region Class Data

    // Related Code Classes
    private readonly DataDetailData mDataDetailData;
    private readonly DataDetailCode mDataDetailCode;
    private readonly ControlCode mControlCode;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
