// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateDataDetail.cs
using LJCDataAccessConfig;
using LJCDBClientLib;
using LJCDBMessage;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenTextEdit
{
  /// <summary>The CreateData detail dialog.</summary>
  public partial class CreateDataDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CreateDataDetail()
    {
      InitializeComponent();

      // Initialize property values.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void CreateDataDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      GetRecordValues();
      CenterToParent();
    }

    // Paint the form background.
    /// <include path='items/OnPaintBackground/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
        , EndColor);
    }
    #endregion

    #region Data Methods

    // Gets the record values and copies them to the controls.
    private void GetRecordValues()
    {
      //ConfigNameCombo.Text = DataConfigName;
      int index = ConfigNameCombo.FindString(DataConfigName);
      if (index > -1)
      {
        ConfigNameCombo.SelectedIndex = index;
      }
      TableNameCombo.Text = TableName;
    }

    // Creates and returns a record object with the data from
    private void SetRecordValues()
    {
      DataConfigName = ConfigNameCombo.Text.Trim();
      TableName = TableNameCombo.Text.Trim();
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Set control values.
      ConfigNameLabel.BackColor = BeginColor;
      TableNameLabel.BackColor = BeginColor;

      // Load control data.
      mDataConfigs = new DataConfigs();
      mDataConfigs.LJCLoadData();
      foreach (DataConfig dataConfig in mDataConfigs)
      {
        ConfigNameCombo.Items.Add(dataConfig.Name);
      }

      Cursor = Cursors.Default;
    }
    #endregion

    #region Control Event Handlers

    // Load the table names.
    private void ConfigNameCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      DataManager dataManager;
      DbResult dbResult;

      bool success = true;
      string dataConfigName = ConfigNameCombo.Text;
      var dataConfig = mDataConfigs.LJCGetByName(dataConfigName);
      if (null == dataConfig)
      {
        success = false;
      }

      if (success)
      {
        dataManager = new DataManager(dataConfigName, null);
        dbResult = dataManager.GetTableNames();
        foreach (DbRow dbRow in dbResult.Rows)
        {
          string tableName = dbRow.Values.LJCGetValue("TABLE_NAME");
          if (false == tableName.StartsWith("sys"))
          {
            TableNameCombo.Items.Add(tableName);
          }
        }
      }
    }

    // Saves the data and closes the form.
    private void OKButton_Click(object sender, EventArgs e)
    {
      SetRecordValues();
      DialogResult = DialogResult.OK;
    }

    // Closes the form without saving the data.
    private void FormCancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    #region KeyEdit Event Handlers

    // Does not allow spaces.
    private void ConfigNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleSpace(e.KeyChar);
    }

    // Strips blanks from the text value.
    private void ConfigNameTextBox_TextChanged(object sender, EventArgs e)
    {
      if (sender is TextBox textBox)
      {
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = textBox.Text.Trim().Length;
      }
    }

    // Does not allow spaces.
    private void TableNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleSpace(e.KeyChar);
    }

    // Strips blanks from the text value.
    private void TableNameTextBox_TextChanged(object sender, EventArgs e)
    {
      if (sender is TextBox textBox)
      {
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = textBox.Text.Trim().Length;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataConfig Name.</summary>
    public string DataConfigName { get; set; }

    /// <summary>Gets or sets the Table Name.</summary>
    public string TableName { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private DataConfigs mDataConfigs;
    #endregion
  }
}
