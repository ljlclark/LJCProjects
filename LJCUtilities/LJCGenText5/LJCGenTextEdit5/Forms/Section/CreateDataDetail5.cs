// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CreateDataDetail5.cs
using LJCControls5;
using LJCDataAccessConfig5;
using LJCDBClientLib5;
using LJCDBMessage5;
using LJCNetCommon5;

namespace LJCGenTextEdit5
{
  /// <summary>The CreateData detail dialog.</summary>
  public partial class CreateDataDetail : Form
  {
    #region Properties

    /// <summary>Gets or sets the DataConfig Name.</summary>
    public string DataConfigName { get; set; } = null!;

    /// <summary>Gets or sets the Table Name.</summary>
    public string TableName { get; set; } = null!;

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    private LJCDataConfigs mDataConfigs = null!;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public CreateDataDetail()
    {
      InitializeComponent();

      // Initialize property values.
      BeginColor = Color.AliceBlue;
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
    /// <include path='items/OnPaintBackground/*' file='../../LJCGenDoc/Common/Detail.xml'/>
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
      Cursor = Cursors.WaitCursor;

      // Initialize Class Data.
      //FormCommon.SetLabelsBackColor(Controls, BeginColor);

      // Set control values.

      // Load control data.
      mDataConfigs = [];
      mDataConfigs.LoadData();
      foreach (LJCDataConfig dataConfig in mDataConfigs)
      {
        if (dataConfig.Name != null)
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
      LJCDataManager dataManager;
      LJCDBResult? dbResult;

      while (true)
      {
        string dataConfigName = ConfigNameCombo.Text;
        var dataConfig = mDataConfigs.Retrieve(dataConfigName);
        if (null == dataConfig)
        {
          break;
        }

        dataManager = new LJCDataManager(dataConfigName, null);
        dbResult = dataManager.GetTableNames();
        if (null == dbResult
          || !LJC.HasListItems(dbResult.Rows))
        {
          break;
        }
        foreach (LJCDBRow dbRow in dbResult.Rows)
        {
          if (null == dbRow
            || null == dbRow.Values)
          {
            continue;
          }
          string? tableName = dbRow.Values.LJCString("TABLE_NAME");
          if (!LJC.HasText(tableName)
            || tableName.StartsWith("sys"))
          {
            continue;
          }
          TableNameCombo.Items.Add(tableName);
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
  }
}
