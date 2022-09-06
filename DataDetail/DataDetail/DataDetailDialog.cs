// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
using DataDetailDAL;
using LJCDataDetailLib;
using LJCDBClientLib;
using LJCDBDataAccess;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DataDetail
{
  // The DataDetail Dynamic Detail dialog.
  /// <include path='items/DataDetailDialog/*' file='Doc/DataDetailDialog.xml'/>
  public partial class DataDetailDialog : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DataDetailDialogC/*' file='Doc/DataDetailDialog.xml'/>
    //public DataDetailDialog(string dataConfigName, string configRowsFileName = null)
    public DataDetailDialog(string userID, string dataConfigName
      , string tableName)
    {
      InitializeComponent();

      // Initialize property values.
      LJCDataConfigName = dataConfigName;

      LJCDbServiceRef = new DbServiceRef()
      {
        DbDataAccess = new DbDataAccess(dataConfigName)
      };
      mManagers = new DataDetailManagers();
      mManagers.SetDBProperties(LJCDbServiceRef, dataConfigName);

      // Get DetailConfig data.
      var configManager = mManagers.DetailConfigManager;
      LJCDetailConfig = configManager.RetrieveWithUniqueTable(userID
        , dataConfigName, tableName);

      var detailConfig = LJCDetailConfig;
      if (detailConfig != null)
      {
        // Get ControlColumns data.
        var columnManager = mManagers.ControlColumnManager;
        mControlColumns = columnManager.LoadWithParentID(detailConfig.ID);

        // Get ControlRows data into ControlColumns.
        var rowManager = mManagers.ControlRowManager;
        foreach (ControlColumn controlColumn in mControlColumns)
        {
          controlColumn.ControlRows
            = rowManager.LoadWithParentID(controlColumn.ID);
        }
      }
      else
      {
        // Set default values.
        LJCDetailConfig = new DetailConfig()
        {
          Name = $"Detail{tableName}Standard",
          Description = $"{tableName} Detail Standard",
          DataConfigName = dataConfigName,
          TableName = tableName,
          UserID = userID,
          BorderHorizontal = 5,
          BorderVertical = 8,
          CharacterPixels = 6,
          ColumnRowsLimit = 8,
          ControlRowHeight = 21,
          ControlRowSpacing = 5,
          ControlsHeight = 21,
          DataValueCount = 1,
          MaxControlCharacters = 40,
          PageColumnsLimit = 2,
        };
        detailConfig = LJCDetailConfig;

        // Add DetailConfig with default data.
        var addedRecord = configManager.Add(detailConfig);
        if (addedRecord != null)
        {
          detailConfig.ID = addedRecord.ID;
        }
        mControlColumns = new ControlColumns();
      }

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
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

    // Paint the form background.
    /// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
        , EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    /// <include path='items/DataRetrieve/*' file='../../LJCDocLib/Common/Detail.xml'/>
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
            controlName = $"{dbColumn.ColumnName}ComboBox";
            control = SearchControls(controlName);
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
              controlName = $"{dbColumn.ColumnName}CheckBox";
              control = SearchControls(controlName);
              if (control != null)
              {
                dbColumn.Value = ((CheckBox)control).Checked;
              }
              break;

            default:
              // Get value from TextBox.
              controlName = $"{dbColumn.ColumnName}TextBox";
              control = SearchControls(controlName);
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

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      // Contains methods for creating ControlColumns.
      mControlColumnsHelper = new ControlColumnsHelper(mManagers)
      {
        // Share configurations.
        DetailConfig = LJCDetailConfig
      };

      // Contains methods for creating Controls.
      mControlHelper = new ControlHelper();

      var config = LJCDetailConfig;
      if (0 == mControlColumns.Count)
      {
        // Create new configuration.
        config.ControlRowHeight = GetControlRowHeight(config.ControlRowHeight);
        mControlColumns
          = mControlColumnsHelper.CreateNewControlColumns(LJCDataColumns);
        CreateNewData();
      }

      // *** Begin *** Add - 9/6
      config.ControlsWidth
        = mControlColumnsHelper.ControlsWidth(mControlColumns);

      config.ControlsHeight
        = mControlColumnsHelper.ControlsHeight(LJCDataColumns.Count);
      UpdateDetailConfig(mManagers.DetailConfigManager, config);
      // *** End   *** Add - 9/6

      CreateConfigControls();
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

      // Set the MainTabs size to the Controls size plus border values.
      MainTabs.Size = new Size(config.ControlsWidth + tabBorders.Width
        , config.ControlsHeight - 2 + tabBorders.Height);

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
        throw new MissingMemberException(Name, "LJCRecord");
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

    // 
    private void UpdateDetailConfig(DetailConfigManager configManager
      , DetailConfig dataObject)
    {
      if (dataObject.ID > 0)
      {
        var keyColumns = configManager.GetIDKey(dataObject.ID);

        configManager.Update(dataObject, keyColumns);
      }
    }
    #endregion

    #region Process Controls Methods

    // Create the Controls from the configuration.
    private void CreateConfigControls()
    {
      DbColumn dataColumn;

      if (LJCDataColumns != null && LJCDataColumns.Count > 0)
      {
        CreateAdditionalTabPages(mControlColumns.Count);
        int tabIndex = 0;

        // Create all controls in all ControlColumns.
        foreach (ControlColumn controlColumn in mControlColumns)
        {
          int columnIndex = controlColumn.ColumnIndex;
          int tabPageIndex = controlColumn.TabPageIndex;
          TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
          foreach (ControlRow controlRow in controlColumn.ControlRows)
          {
            int rowIndex = controlRow.RowIndex;
            dataColumn = LJCDataColumns.LJCSearchName(controlRow.DataValueName);

            CreateControlRow(controlColumn, dataColumn, controlColumn.LabelsWidth
              , tabPageIndex, columnIndex, rowIndex, ref tabIndex);
          }
        }
        ConfigureFormButtons();
      }
    }

    // Creates the ControlRow and associated controls.
    /// <include path='items/CreateControlRow/*' file='Doc/DataDetailDialog.xml'/>
    private void CreateControlRow(ControlColumn controlColumn, DbColumn dataColumn
      , int labelsWidth, int tabPageIndex, int columnIndex, int rowIndex
      , ref int tabbingIndex)
    {
      ControlRow controlRow;
      TextBox textBox = null;

      // Create new column to prevent changing original sort.
      var searchControlColumn = new ControlColumn(controlColumn);
      controlRow = searchControlColumn.ControlRows.LJCSearchUnique(controlColumn.ID
        , dataColumn.ColumnName);

      var config = LJCDetailConfig;
      string controlRowType = GetControlRowType(dataColumn);
      if (controlRowType != "CheckBox")
      {
        CreateLabel(controlRow, dataColumn, labelsWidth
          , tabPageIndex, columnIndex, rowIndex, tabbingIndex);
        tabbingIndex++;
      }

      if ("SelectList" == controlRowType
        || "StaticKey" == controlRowType
        || "TextBox" == controlRowType)
      {
        textBox = CreateTextBox(controlRow, dataColumn, tabPageIndex
          , columnIndex, rowIndex, tabbingIndex);
        SelectControlIfFirst(columnIndex, rowIndex, textBox);
      }

      if ("SelectList" == controlRowType
        || "StaticKey" == controlRowType)
      {
        string propertyText = GetKeyPropertyText(dataColumn.PropertyName);
        if (NetString.HasValue(propertyText))
        {
          int length = propertyText.Length;

          if (length < 26)
          {
            length += 1;
          }
          textBox.Width = length * config.CharacterPixels;
          textBox.Text = propertyText;
        }
        textBox.ReadOnly = true;
      }

      switch (controlRowType)
      {
        case "CheckBox":
          CheckBox checkBox = CreateCheckBox(controlRow, dataColumn, tabPageIndex
            , columnIndex, rowIndex, tabbingIndex);
          SelectControlIfFirst(columnIndex, rowIndex, checkBox);
          break;

        case "StaticCombo":
          ComboBox comboBox = CreateComboBox(controlRow, dataColumn, tabPageIndex
            , columnIndex, rowIndex, tabbingIndex);
          FillStaticCombo(comboBox, dataColumn.PropertyName);
          SelectControlIfFirst(columnIndex, rowIndex, comboBox);
          SelectStaticComboItem(comboBox, dataColumn);
          break;

        case "SelectList":
          Button button = CreateButton(controlRow, dataColumn, tabPageIndex
            , columnIndex, rowIndex, tabbingIndex);
          AdjustEllipseButton(button, textBox);

          // Get Select ControlRow Width.
          if (controlColumn != null)
          {
            int buttonSpacing = 5;
            int selectRowWidth = controlColumn.Width + config.BorderHorizontal;
            selectRowWidth += controlRow.RowControl.Height + buttonSpacing;
            if (selectRowWidth > controlColumn.Width)
            {
              button.Left -= textBox.Height + buttonSpacing;
              textBox.Width -= textBox.Height + buttonSpacing;
            }
          }

          button.Click += EllipseButton_Click;
          break;
      }
      tabbingIndex++;
    }

    // Creates a ControlRow DB and Collection item.
    private ControlRow CreateControlRowItem(ControlColumn controlColumn
      , DbColumn dataColumn, int rowIndex, int tabbingIndex)
    {
      ControlRow retValue;

      var controlRows = controlColumn.ControlRows;

      // Create and add the ControlRow.
      retValue = new ControlRow()
      {
        ControlColumnID = controlColumn.ID,
        DataValueName = dataColumn.ColumnName,
        RowIndex = rowIndex,
        TabbingIndex = tabbingIndex,
        AllowDisplay = true
      };
      var propertyNames = new List<string>()
      {
        ControlRow.ColumnControlColumnID,
        ControlRow.ColumnDataValueName,
        ControlRow.ColumnRowIndex,
        ControlRow.ColumnTabbingIndex,
        ControlRow.ColumnAllowDisplay
      };

      var rowManager = mManagers.ControlRowManager;
      var addedItem = rowManager.Add(retValue, propertyNames);
      if (addedItem != null)
      {
        retValue.ID = addedItem.ID;
      }

      // Add collection item.
      controlRows.Add(retValue);
      return retValue;
    }

    // Creates the ControlRow DB and Collection data.
    private void CreateNewData()
    {
      if (mControlColumns != null
        && LJCDataColumns != null && LJCDataColumns.Count > 0)
      {
        int tabbingIndex = 0;

        // Create controls in each ControlColumn.
        var config = LJCDetailConfig;
        foreach (ControlColumn controlColumn in mControlColumns)
        {
          // Create Controls
          int columnIndex = controlColumn.ColumnIndex;
          int startDataIndex = config.ColumnRowsLimit * columnIndex;
          int rowCount = controlColumn.RowCount;
          int endDataIndex = startDataIndex + (rowCount - 1);
          for (int dataIndex = startDataIndex; dataIndex < endDataIndex + 1
            ; dataIndex++)
          {
            if (dataIndex < LJCDataColumns.Count)
            {
              int rowIndex = dataIndex - columnIndex * config.ColumnRowsLimit;
              DbColumn dataColumn = LJCDataColumns[dataIndex];

              CreateControlRowItem(controlColumn, dataColumn, rowIndex, tabbingIndex);
              string controlRowType = GetControlRowType(dataColumn);
              if (controlRowType != "CheckBox")
              {
                tabbingIndex++;
              }
              tabbingIndex++;
            }
          }
        }
        ConfigureFormButtons();
      }
    }

    // Gets the default ControlRow height.
    private int GetControlRowHeight(int controlRowHeight)
    {
      int retValue = controlRowHeight;

      ComboBox comboBox = mControlHelper.CreateComboBox("Test", new Point(0, 0));
      if (retValue < comboBox.Height)
      {
        retValue = comboBox.Height;
      }
      return retValue;
    }

    // Gets the ControlRow type name.
    private string GetControlRowType(DbColumn dataColumn)
    {
      string retValue = null;

      var isNext = true;
      KeyItems items;
      if (LJCKeyItems != null)
      {
        items = LJCKeyItems.SearchPropertyName(dataColumn.PropertyName);
        if (items != null && items.Count > 0)
        {
          isNext = false;
          if (1 == items.Count)
          {
            retValue = "StaticKey";
            if (NetString.HasValue(items[0].TableName))
            {
              retValue = "SelectList";
            }
          }
          else
          {
            retValue = "StaticCombo";
          }
        }
      }

      if (isNext)
      {
        retValue = "TextBox";
        if ("boolean" == dataColumn.DataTypeName.ToLower())
        {
          retValue = "CheckBox";
        }
      }
      return retValue;
    }

    // Gets the KeyItem Property text.
    private string GetKeyPropertyText(string propertyName)
    {
      string retValue = null;

      var items = LJCKeyItems.SearchPropertyName(propertyName);
      if (items != null && 1 == items.Count)
      {
        retValue = items[0].Description.Trim();
      }
      return retValue;
    }

    // Gets the Control Column 1 Label Location.
    private Point LabelLocation(int tabPageIndex, int controlColumnIndex
      , int controlRowIndex)
    {
      ControlColumn controlColumn;
      int left;
      int top;
      Point retValue;
      int pageColumnIndex;

      var config = LJCDetailConfig;
      pageColumnIndex = controlColumnIndex - ((tabPageIndex) * config.PageColumnsLimit);

      left = config.BorderHorizontal * (pageColumnIndex + 1);
      for (int index = 0; index < pageColumnIndex; index++)
      {
        controlColumn = mControlColumns[index];
        left += controlColumn.Width;
      }

      top = config.BorderVertical + (config.ControlRowSpacing + config.ControlRowHeight)
        * controlRowIndex + 3;
      retValue = new Point(left, top);
      return retValue;
    }

    // Returns a reference to a Control by name.
    /// <include path='items/SearchControls/*' file='Doc/DataDetailDialog.xml'/>
    private Control SearchControls(string name)
    {
      Control retValue = null;

      Control[] controls = Controls.Find(name, true);
      if (controls.Length > 0)
      {
        retValue = controls[0];
      }
      return retValue;
    }

    // Returns a reference to a Label control by name.
    /// <include path='items/SearchLabel/*' file='Doc/DataDetailDialog.xml'/>
    internal Label SearchLabel(string name)
    {
      Label retValue;

      retValue = SearchControls(name) as Label;
      return retValue;
    }

    // Return a reference to a TextBox control by name.
    /// <include path='items/SearchTextBox/*' file='Doc/DataDetailDialog.xml'/>
    internal TextBox SearchTextBox(string name)
    {
      TextBox retValue;

      retValue = SearchControls(name) as TextBox;
      return retValue;
    }

    // Selects the Control if it is the first one.
    private void SelectControlIfFirst(int columnIndex, int rowIndex
      , Control control)
    {
      if (0 == columnIndex && 0 == rowIndex
        && control != null)
      {
        control.Select();
      }
    }

    // Selects the matching Static Combo item.
    private void SelectStaticComboItem(ComboBox comboBox, DbColumn dataColumn)
    {
      if (dataColumn.Value != null && NetString.HasValue(dataColumn.Value.ToString()))
      {
        int.TryParse(dataColumn.Value.ToString(), out int id);
        if (id > 0)
        {
          foreach (KeyItem keyItem in comboBox.Items)
          {
            if (keyItem.ID == id)
            {
              comboBox.SelectedItem = keyItem;
              break;
            }
          }
        }
      }
    }

    // Sets the Control event handlers.
    private void SetTextBoxControlHandlers(TextBox textBox, DbColumn dataColumn)
    {
      if ("Int16" == dataColumn.DataTypeName
        || "Int32" == dataColumn.DataTypeName
        || "Int64" == dataColumn.DataTypeName)
      {
        textBox.KeyPress += TextBoxNumeric_KeyPress;
      }
    }
    #endregion

    #region Create Controls Methods

    // Adjusts the Ellipse button.
    private void AdjustEllipseButton(Button button, TextBox textBox)
    {
      button.Text = null;
      button.ImageList = this.ButtonImages;
      button.ImageKey = "Ellipse.bmp";
      int adjust = textBox.Height;
      button.Size = new System.Drawing.Size(adjust, adjust);
      button.Top = textBox.Top;
      button.Left += textBox.Width + 4;
    }

    // Completes the controls setup.
    private void ConfigureFormButtons()
    {
      int tabIndex = LJCDataColumns.Count * 2;
      OKButton.TabIndex = tabIndex++;
      FormCancelButton.TabIndex = tabIndex++;
    }

    // The Control Location.
    private Point ControlLocation(int tabPageIndex, int controlColumnIndex
      , int controlRowIndex)
    {
      ControlColumn controlColumn;
      int left;
      int top;
      Point retValue;
      int pageColumnIndex;

      var config = LJCDetailConfig;
      pageColumnIndex = controlColumnIndex - ((tabPageIndex) * config.PageColumnsLimit);

      left = config.BorderHorizontal * (pageColumnIndex + 1);
      for (int index = 0; index < pageColumnIndex + 1; index++)
      {
        controlColumn = mControlColumns[index];
        left += controlColumn.LabelsWidth;
        if (index < pageColumnIndex)
        {
          left += controlColumn.ControlsWidth;
        }
      }

      top = config.BorderVertical + (config.ControlRowSpacing + config.ControlRowHeight)
        * controlRowIndex;
      retValue = new Point(left, top);
      return retValue;
    }

    // Creates the additional TabPages.
    private void CreateAdditionalTabPages(int controlColumnsCount)
    {
      int pagesCount = 1;

      var config = LJCDetailConfig;
      if (controlColumnsCount > config.PageColumnsLimit)
      {
        pagesCount = controlColumnsCount / config.PageColumnsLimit;
        if (controlColumnsCount % config.PageColumnsLimit != 0)
        {
          pagesCount++;
        }
      }

      // Create additions TabPages.
      for (int index = 1; index < pagesCount; index++)
      {
        MainTabs.TabPages.Add($"Page {index + 1}");
      }
    }

    // Creates the Button control.
    private Button CreateButton(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      Button retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}Button";
      string text = dataColumn.Caption;
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      //int width = mControlColumnsHelper.AdjustedWidth(dataColumn);
      retValue = mControlHelper.CreateButton(name, text, location);
      retValue.TabIndex = tabIndex;
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      controlRow.RowControl = retValue;
      return retValue;
    }

    // Creates the CheckBox control.
    private CheckBox CreateCheckBox(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      int width;
      CheckBox retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}CheckBox";
      string text = dataColumn.Caption;
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mControlColumnsHelper.AdjustedWidth(dataColumn);
      retValue = mControlHelper.CreateCheckBox(name, text, location, width);
      retValue.TabIndex = tabIndex;
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      controlRow.RowControl = retValue;
      //mControlRows.SetControlRowWidth(mControlColumns, controlRow);
      return retValue;
    }

    // Creates the TextBox control.
    private ComboBox CreateComboBox(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      int width;
      ComboBox retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}ComboBox";
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mControlColumnsHelper.AdjustedWidth(dataColumn);
      retValue = mControlHelper.CreateComboBox(name, location, width);
      retValue.TabIndex = tabIndex;
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      controlRow.RowControl = retValue;
      //mControlRows.SetControlRowWidth(mControlColumns, controlRow);
      return retValue;
    }

    // Creates the Label control.
    private void CreateLabel(ControlRow controlRow, DbColumn dataColumn, int width
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      Label label;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}Label";
      string text = dataColumn.Caption;
      Point location = LabelLocation(tabPageIndex, columnIndex, rowIndex);
      label = mControlHelper.CreateLabel(name, text, location, width);
      label.TabIndex = tabIndex;
      label.BackColor = BeginColor;
      Controls.Add(label);
      label.Parent = currentTabPage;
      controlRow.RowLabel = label;
      controlRow.TabbingIndex = tabIndex;
    }

    // Creates the TextBox control.
    private TextBox CreateTextBox(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      int width;
      TextBox retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}TextBox";
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mControlColumnsHelper.AdjustedWidth(dataColumn);
      string value = null;
      if (dataColumn.Value != null)
      {
        value = dataColumn.Value.ToString();
      }
      retValue = mControlHelper.CreateTextBox(name, value, location, width);
      retValue.TabIndex = tabIndex;
      if (dataColumn.MaxLength > 0)
      {
        retValue.MaxLength = dataColumn.MaxLength;
      }
      SetTextBoxControlHandlers(retValue, dataColumn);
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      controlRow.RowControl = retValue;
      //mControlRows.SetControlRowWidth(mControlColumns, controlRow);
      return retValue;
    }

    // Populates a Static ComboBox.
    private void FillStaticCombo(ComboBox comboBox, string propertyName)
    {
      List<KeyItem> propertyItems;

      if (LJCKeyItems != null)
      {
        propertyItems = LJCKeyItems.Items.FindAll(x => x.PropertyName == propertyName);
        if (propertyItems != null && propertyItems.Count > 0)
        {
          foreach (KeyItem keyItem in propertyItems)
          {
            comboBox.Items.Add(keyItem);
          }
          comboBox.Width = (propertyItems[0].MaxLength + 3)
            * LJCDetailConfig.CharacterPixels;
        }
      }
    }

    //// Gets the current TabPage.
    //private TabPage GetCurrentPage(ControlColumn controlColumn
    //	, ref int tabPageIndex, int tabIndex)
    //{
    //	TabPage retValue = null;

    //	int index = GetTabPageIndex(controlColumn, tabPageIndex, tabIndex);
    //	if (index > tabPageIndex)
    //	{
    //		tabPageIndex = index;
    //		retValue = MainTabs.TabPages[tabPageIndex];
    //	}
    //	return retValue;
    //}
    #endregion

    #region Control Event Handlers

    // Fires the Change event.
    /// <include path='items/LJCOnChange/*' file='../../LJCDocLib/Common/Detail.xml'/>
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

    // Closes the form without saving the data.
    private void FormCancelButton_Click(object sender, EventArgs e)
    {
      Close();
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
          LJCDataConfigName = LJCDataConfigName,
          LJCDbServiceRef = LJCDbServiceRef,
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

    // Checks the values required for the SelectList window.
    private bool CheckSelectListValues(string buttonName, out KeyItem keyItem)
    {
      int index = 0;
      string message = null;
      bool retValue = true;

      keyItem = null;

      if (null == LJCDbServiceRef)
      {
        retValue = false;
        message = "Missing Data Service Reference.";
      }

      if (retValue)
      {
        if (false == NetString.HasValue(LJCDetailConfig.DataConfigName))
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
    #endregion

    #region KeyEdit Event Handlers

    // Only allows numbers or edit keys.
    internal void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
    }

    // Does not allow spaces.
    private void TextBoxNoSpace_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleSpace(e.KeyChar);
    }

    // Strips blanks from the text value.
    private void TextBoxNoSpace_TextChanged(object sender, EventArgs e)
    {
      if (sender is TextBox textBox)
      {
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = textBox.Text.Trim().Length;
      }
    }
    #endregion

    #region Properties

    // The Configuration Rows.
    /// <include path='items/LJCConfigRows/*' file='Doc/DataDetailDialog.xml'/>
    public DetailConfig LJCDetailConfig
    {
      get { return mDetailConfig; }
      set
      {
        if (value != null)
        {
          mDetailConfig = value;
        }
      }
    }
    private DetailConfig mDetailConfig;

    // Gets a reference to the record object.
    /// <include path='items/LJCDataColumns/*' file='Doc/DataDetailDialog.xml'/>
    public DbColumns LJCDataColumns { get; set; }

    /// <summary>Gets or sets the DataConfig name.</summary>
    public string LJCDataConfigName { get; set; }

    /// <summary>Gets or sets the DbServiceRef value.</summary>
    public DbServiceRef LJCDbServiceRef { get; set; }

    /// <summary>Gets the LJCIsUpdate value.</summary>
    public bool LJCIsUpdate { get; set; }

    // Gets or sets the ControlItems collection.
    /// <include path='items/LJCKeyItems/*' file='Doc/DataDetailDialog.xml'/>
    public KeyItems LJCKeyItems { get; set; }
    #endregion

    #region Private Properties

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    // ControlColumns and ControlRows are the main layout definition objects.
    private ControlColumns mControlColumns;
    private ControlColumnsHelper mControlColumnsHelper;
    private ControlHelper mControlHelper;
    readonly DataDetailManagers mManagers;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
