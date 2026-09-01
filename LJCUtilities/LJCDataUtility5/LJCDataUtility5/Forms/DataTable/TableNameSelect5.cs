// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TableNameSelect5.cs
using LJCControls5;
using LJCDataAccessConfig5;
using LJCDBClientLib5;
using LJCDBMessage5;

namespace LJCDataUtility5
{
  /// <summary>The CreateData detail dialog.</summary>
  internal partial class TableNameSelect : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='members/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    internal TableNameSelect()
    {
      InitializeComponent();

      // Initialize property values.
      TableName = "";
      BeginColor = Color.AliceBlue;
      //EndColor = Color.LightSkyBlue;
      EndColor = Color.SkyBlue;
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
    /// <include path='members/OnPaintBackground/*' file='../../LJCGenDoc/Common/Detail.xml'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      //FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
      //  , EndColor);
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
      //ConfigNameLabel.BackColor = BeginColor;
      //TableNameLabel.BackColor = BeginColor;

      // Load control data.
      _DataConfigs = [];
      _DataConfigs.LoadData();
      foreach (LJCDataConfig dataConfig in _DataConfigs)
      {
        if (dataConfig != null
          && dataConfig.Name != null)
        {
          ConfigNameCombo.Items.Add(dataConfig.Name);
        }
      }

      Cursor = Cursors.Default;
    }
    #endregion

    #region Control Event Handlers

    // Load the table names.
    private void ConfigNameCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
      //LJCDataManager dataManager;

      //bool isContinue = true;
      while (true)
      {
        string dataConfigName = ConfigNameCombo.Text;
        var dataConfig = _DataConfigs.Retrieve(dataConfigName);
        if (null == dataConfig)
        {
          break;
        }

        TableNameCombo.Items.Clear();
        var dataManager = new LJCDataManager(dataConfigName, null)
        {
          OrderByNames =
          [
            "TableName",
          ]
        };
        var dbResult = dataManager.GetTableNames();
        if (dbResult != null)
        {
          foreach (LJCDBRow dbRow in dbResult.Rows)
          {
            if (dbRow.Values != null)
            {
              string? tableName = dbRow.Values.LJCString("TABLE_NAME");
              if (tableName != null
                && !tableName.StartsWith("sys"))
              {
                TableNameCombo.Items.Add(tableName);
              }
            }
          }
        }
        break;
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
        var prevStart = textBox.SelectionStart;
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = prevStart;
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
        var prevStart = textBox.SelectionStart;
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = prevStart;
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the DataConfig Name.</summary>
    public string DataConfigName { get; set; } = null!;

    /// <summary>Gets or sets the Table Name.</summary>
    public string TableName { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private LJCDataConfigs _DataConfigs = null!;
    #endregion
  }
}
