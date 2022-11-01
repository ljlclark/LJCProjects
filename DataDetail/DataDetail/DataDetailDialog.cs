// DataDetailDialog.cs
using LJCDataDetailDAL;
using LJCDataDetailLib;
using LJCNetCommon;
using LJCWinFormCommon;
using System;
using System.Collections.Generic;
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
    public DataDetailDialog(string userID, string dataConfigName
      , string tableName)
    {
      InitializeComponent();

      // Initialize property values.
      DataDetailData = new DataDetailData(dataConfigName, tableName, userID);
      ControlDetail = DataDetailData.GetControlDetail();

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
      // Local references.
      var config = ControlDetail;

      // Contains methods for creating Controls.
      mControlCode = new ControlCode();

      // Contains methods for creating ControlColumns.
      mControlColumnsCode = new DataDetailCode()
      {
        // Share configuration.
        ControlDetail = ControlDetail,
        DataDetailData = DataDetailData
      };

      var controlTabItems = config.ControlTabItems;
      if (null == controlTabItems || 0 == controlTabItems.Count)
      {
        // Create new configuration.
        config.ControlRowHeight = ControlRowHeight(config.ControlRowHeight);
        mControlColumnsCode.NewControlData(LJCDataColumns, LJCKeyItems);
      }

      // *** Begin *** Add - 09/06/22
      config.ContentWidth = mControlColumnsCode.ContentWidth();
      config.ContentHeight
        = mControlColumnsCode.ContentHeight(config.ColumnRowsLimit);
      DataDetailData.UpdateControlDetail(config);
      // *** End   *** Add

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

    #region Process Controls Methods

    // Create the Controls from the configuration.
    private void CreateControls()
    {
      DbColumn dataColumn;

      // Local references.
      var config = ControlDetail;

      if (LJCDataColumns != null && LJCDataColumns.Count > 0)
      {
        // Create additional tabs.
        int count = config.ControlTabItems.Count;
        for (int index = 1; index < count; count++)
        {
          var controlTab = config.ControlTabItems[index];
          MainTabs.TabPages.Add(controlTab.Caption);
        }

        int tabIndex = 0;
        foreach (ControlTab controlTab in config.ControlTabItems)
        {
          foreach (ControlColumn controlColumn in controlTab.ControlColumns)
          {
            int tabPageIndex = controlTab.TabIndex;
            int columnIndex = controlColumn.ColumnIndex;
            foreach (ControlRow controlRow in controlColumn.ControlRows)
            {
              int rowIndex = controlRow.RowIndex;
              dataColumn = LJCDataColumns.LJCSearchName(controlRow.DataValueName);

              ControlRow(controlColumn, dataColumn, controlColumn.LabelsWidth
                , tabPageIndex, columnIndex, rowIndex, ref tabIndex);
            }
          }
        }
        ConfigureFormButtons();
      }
    }

    // Creates the ControlRow and associated controls.
    /// <include path='items/ControlRow/*' file='Doc/DataDetailDialog.xml'/>
    private void ControlRow(ControlColumn controlColumn, DbColumn dataColumn
      , int labelsWidth, int tabIndex, int columnIndex, int rowIndex, ref int tabbingIndex)
    {
      ControlRow controlRow;
      TextBox textBox = null;

      // Local references.
      var config = ControlDetail;

      // Create new column to prevent changing original sort.
      var searchControlColumn = new ControlColumn(controlColumn);
      controlRow = searchControlColumn.ControlRows.LJCSearchUnique(controlColumn.ID
        , dataColumn.ColumnName);

      string controlRowType = mControlColumnsCode.ControlRowType(dataColumn
        , LJCKeyItems);
      if (controlRowType != "CheckBox")
      {
        AddLabel(controlRow, dataColumn, labelsWidth, tabIndex, columnIndex
          , rowIndex, tabbingIndex);
        tabbingIndex++;
      }

      if ("SelectList" == controlRowType
        || "StaticKey" == controlRowType
        || "TextBox" == controlRowType)
      {
        textBox = AddTextBox(controlRow, dataColumn, tabIndex, columnIndex
          , rowIndex, tabbingIndex);
        SelectControlIfFirst(columnIndex, rowIndex, textBox);
      }

      if ("SelectList" == controlRowType
        || "StaticKey" == controlRowType)
      {
        string propertyText = KeyPropertyText(dataColumn.PropertyName);
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
          CheckBox checkBox = AddCheckBox(controlRow, dataColumn, tabIndex
            , columnIndex, rowIndex, tabbingIndex);
          SelectControlIfFirst(columnIndex, rowIndex, checkBox);
          break;

        case "StaticCombo":
          ComboBox comboBox = AddComboBox(controlRow, dataColumn, tabIndex
            , columnIndex, rowIndex, tabbingIndex);
          FillStaticCombo(comboBox, dataColumn.PropertyName);
          SelectControlIfFirst(columnIndex, rowIndex, comboBox);
          SelectStaticComboItem(comboBox, dataColumn);
          break;

        case "SelectList":
          Button button = AddButton(controlRow, dataColumn, tabIndex
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

    // Gets the default ControlRow height.
    private int ControlRowHeight(int controlRowHeight)
    {
      int retValue = controlRowHeight;

      ComboBox comboBox = mControlCode.CreateComboBox("Test", new Point(0, 0));
      if (retValue < comboBox.Height)
      {
        retValue = comboBox.Height;
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

    // Gets the Control Column 1 Label Location.
    private Point LabelLocation(int tabIndex, int controlColumnIndex
      , int controlRowIndex)
    {
      ControlColumn controlColumn;
      int left;
      int top;
      Point retValue;

      // Local references.
      var config = ControlDetail;

      left = config.BorderHorizontal * (controlColumnIndex * 1);
      var controlTab = config.ControlTabItems[tabIndex];
      for (int index = 0; index < controlColumnIndex; index++)
      {
        controlColumn = controlTab.ControlColumns[index];
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
    private Point ControlLocation(int tabIndex, int controlColumnIndex
      , int controlRowIndex)
    {
      ControlColumn controlColumn;
      int left;
      int top;
      Point retValue;

      // Local references.
      var config = ControlDetail;

      left = config.BorderHorizontal * (controlColumnIndex + 1);
      var controlTab = config.ControlTabItems[tabIndex];
      for (int index = 0; index < controlColumnIndex + 1; index++)
      {
        controlColumn = controlTab.ControlColumns[index];
        left += controlColumn.LabelsWidth;
        if (index < controlColumnIndex)
        {
          left += controlColumn.ControlsWidth;
        }
      }

      top = config.BorderVertical + (config.ControlRowSpacing + config.ControlRowHeight)
        * controlRowIndex;
      retValue = new Point(left, top);
      return retValue;
    }

    // Creates the Button control.
    private Button AddButton(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      Button retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}Button";
      string text = dataColumn.Caption;
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      //int width = mControlColumnsHelper.AdjustedWidth(dataColumn);
      retValue = mControlCode.CreateButton(name, text, location);
      retValue.TabIndex = tabIndex;
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      controlRow.RowControl = retValue;
      return retValue;
    }

    // Creates the CheckBox control.
    private CheckBox AddCheckBox(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      int width;
      CheckBox retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}CheckBox";
      string text = dataColumn.Caption;
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mControlColumnsCode.AdjustedWidth(dataColumn);
      retValue = mControlCode.CreateCheckBox(name, text, location, width);
      retValue.TabIndex = tabIndex;
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      controlRow.RowControl = retValue;
      //mControlRows.SetControlRowWidth(mControlColumns, controlRow);
      return retValue;
    }

    // Creates the TextBox control.
    private ComboBox AddComboBox(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      int width;
      ComboBox retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}ComboBox";
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mControlColumnsCode.AdjustedWidth(dataColumn);
      retValue = mControlCode.CreateComboBox(name, location, width);
      retValue.TabIndex = tabIndex;
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      controlRow.RowControl = retValue;
      //mControlRows.SetControlRowWidth(mControlColumns, controlRow);
      return retValue;
    }

    // Creates the Label control.
    private void AddLabel(ControlRow controlRow, DbColumn dataColumn, int width
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      Label label;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}Label";
      string text = dataColumn.Caption;
      Point location = LabelLocation(tabPageIndex, columnIndex, rowIndex);
      label = mControlCode.CreateLabel(name, text, location, width);
      label.TabIndex = tabIndex;
      label.BackColor = BeginColor;
      Controls.Add(label);
      label.Parent = currentTabPage;
      controlRow.RowLabel = label;
      controlRow.TabbingIndex = tabIndex;
    }

    // Creates the TextBox control.
    private TextBox AddTextBox(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      int width;
      TextBox retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      var name = $"{dataColumn.ColumnName}TextBox";
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mControlColumnsCode.AdjustedWidth(dataColumn);
      string value = null;
      if (dataColumn.Value != null)
      {
        value = dataColumn.Value.ToString();
      }
      retValue = mControlCode.CreateTextBox(name, value, location, width);
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
            * ControlDetail.CharacterPixels;
        }
      }
    }
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
          LJCDataConfigName = ControlDetail.DataConfigName,
          LJCDbServiceRef = DataDetailData.DbServiceRef,
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

      if (null == DataDetailData.DbServiceRef)
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
    #endregion

    #region KeyEdit Event Handlers

    // Only allows numbers or edit keys.
    internal void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
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

    // Gets or sets ConfigData.
    private DataDetailData DataDetailData { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private DataDetailCode mControlColumnsCode;
    private ControlCode mControlCode;

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;
    #endregion
  }
}
