using LJCDataDetailDAL;
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LJCDataDetail
{
  // The DataDetail Dynamic Detail dialog.
  public partial class DataDetailDialog
  {
    #region Process Controls Methods

    // Create the Controls from the configuration.
    private void CreateControls()
    {
      DbColumn dataColumn;

      // Local references.
      var config = ControlDetail;

      if (NetCommon.HasItems(LJCDataColumns))
      {
        // Create additional tabs.
        foreach (ControlTab controlTab in config.ControlTabItems)
        {
          var caption = controlTab.Caption;
          if (0 == controlTab.TabIndex)
          {
            // Initial tab already exists.
            var tabPage = MainTabs.TabPages[0];
            tabPage.Text = caption;
            tabPage.Name = caption;
            tabPage.MouseMove += Page_MouseMove;
          }
          else
          {
            // Add additional tabs.
            MainTabs.TabPages.Add(caption);
            var tabPage = MainTabs.LJCGetTabPage(caption);
            tabPage.Name = caption;
            tabPage.BackColor = BeginColor;
            tabPage.MouseMove += Page_MouseMove;
          }
        }

        // Create ControlRows controls.
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
              dataColumn
                = LJCDataColumns.LJCSearchPropertyName(controlRow.DataValueName);

              ControlRow(controlColumn, dataColumn, controlColumn.LabelsWidth
                , tabPageIndex, columnIndex, rowIndex, ref tabIndex);
            }
          }
        }
        ConfigureFormButtons();
      }
    }

    // Creates the ControlRow and associated controls.
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
      string controlRowType = mDataDetailCode.ControlRowType(dataColumn
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

    // Get the ControlRow label control.
    private Control GetControl(int tabIndex, int columnIndex, int rowIndex)
    {
      Control retValue;

      var controlTab = ControlDetail.ControlTabItems[tabIndex];
      var controlColumn = controlTab.ControlColumns[columnIndex];
      var controlRow = controlColumn.ControlRows[rowIndex];
      var dataValueName = controlRow.DataValueName;
      retValue = GetControlWithProperty(dataValueName);
      return retValue;
    }

    // Returns a reference to a Control by name.
    private Control GetControlWithName(string name)
    {
      Control retValue = null;

      Control[] controls = Controls.Find(name, true);
      if (controls.Length > 0)
      {
        retValue = controls[0];
      }
      return retValue;
    }

    // Get control with property name.
    private Control GetControlWithProperty(string propertyName)
    {
      Control retValue;

      var dataColumn = LJCDataColumns.LJCSearchPropertyName(propertyName);
      string controlRowType = mDataDetailCode.ControlRowType(dataColumn
        , LJCKeyItems);
      string suffix;
      switch (controlRowType.ToLower())
      {
        case "checkbox":
          suffix = "CheckBox";
          var controlName = ControlName(propertyName, suffix);
          retValue = GetControlWithName(controlName) as CheckBox;
          break;

        case "staticcombo":
          suffix = "ComboBox";
          controlName = ControlName(propertyName, suffix);
          retValue = GetControlWithName(controlName) as ComboBox;
          break;

        default:
          suffix = "TextBox";
          controlName = ControlName(propertyName, suffix);
          retValue = GetControlWithName(controlName) as TextBox;
          break;
      }
      return retValue;
    }

    // Get the ControlRow label control.
    private Label GetLabel(int tabIndex, int columnIndex, int rowIndex)
    {
      Label retValue;

      var controlTab = ControlDetail.ControlTabItems[tabIndex];
      var controlColumn = controlTab.ControlColumns[columnIndex];
      var controlRow = controlColumn.ControlRows[rowIndex];
      var dataValueName = controlRow.DataValueName;
      var controlName = ControlName(dataValueName, "Label");
      retValue = GetControlWithName(controlName) as Label;
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
    private void SetControlHandlers(Control control)
    {
      control.MouseMove += Control_MouseMove;
    }

    // Sets the TextBox event handlers.
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

    // Creates the Button control.
    private Button AddButton(ControlRow controlRow, DbColumn dataColumn
      , int tabPageIndex, int columnIndex, int rowIndex, int tabIndex)
    {
      Button retValue;

      TabPage currentTabPage = MainTabs.TabPages[tabPageIndex];
      // ToDo: Change to use PropertyName?
      var name = ControlName(dataColumn.ColumnName, "Button");
      string text = dataColumn.Caption;
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      //int width = mControlColumnsHelper.AdjustedWidth(dataColumn);
      retValue = mControlCode.CreateButton(name, text, location);
      retValue.TabIndex = tabIndex;
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      // ToDo: Not Used?
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
      // ToDo: Change to use PropertyName?
      var name = ControlName(dataColumn.ColumnName, "CheckBox");
      string text = dataColumn.Caption;
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mDataDetailCode.AdjustedWidth(dataColumn);
      retValue = mControlCode.CreateCheckBox(name, text, location, width);
      retValue.TabIndex = tabIndex;
      SetControlHandlers(retValue);
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      // ToDo: Not Used?
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
      // ToDo: Change to use PropertyName?
      var name = ControlName(dataColumn.ColumnName, "ComboBox");
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mDataDetailCode.AdjustedWidth(dataColumn);
      retValue = mControlCode.CreateComboBox(name, location, width);
      retValue.TabIndex = tabIndex;
      SetControlHandlers(retValue);
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      // ToDo: Not Used?
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
      // ToDo: Change to use PropertyName?
      var name = ControlName(dataColumn.ColumnName, "Label");
      string text = dataColumn.Caption;
      Point location = LabelLocation(tabPageIndex, columnIndex, rowIndex);
      label = mControlCode.CreateLabel(name, text, location, width);
      label.TabIndex = tabIndex;
      label.BackColor = BeginColor;
      SetControlHandlers(label);
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
      // ToDo: Change to use PropertyName?
      var name = ControlName(dataColumn.ColumnName, "TextBox");
      Point location = ControlLocation(tabPageIndex, columnIndex, rowIndex);
      width = mDataDetailCode.AdjustedWidth(dataColumn);
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
      SetControlHandlers(retValue);
      SetTextBoxControlHandlers(retValue, dataColumn);
      Controls.Add(retValue);
      retValue.Parent = currentTabPage;
      // ToDo: Not Used?
      controlRow.RowControl = retValue;
      //mControlRows.SetControlRowWidth(mControlColumns, controlRow);
      return retValue;
    }

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

      // Get all leading borders.
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

    // Creates the ControlName.
    private string ControlName(string dataName, string controlTypeName)
    {
      string retValue = null;

      switch (controlTypeName.ToLower())
      {
        case "button":
          retValue = $"{dataName}Button";
          break;

        case "checkbox":
          retValue = $"{dataName}CheckBox";
          break;

        case "combobox":
          retValue = $"{dataName}ComboBox";
          break;

        case "label":
          retValue = $"{dataName}Label";
          break;

        case "textbox":
          retValue = $"{dataName}TextBox";
          break;
      }
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

    // Gets the Control Column Label Location.
    private Point LabelLocation(int tabIndex, int controlColumnIndex
      , int controlRowIndex)
    {
      ControlColumn controlColumn;
      int left;
      int top;
      Point retValue;

      // Local references.
      var config = ControlDetail;

      // Get all leading borders.
      left = config.BorderHorizontal * (controlColumnIndex + 1);

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
    #endregion
  }
}
