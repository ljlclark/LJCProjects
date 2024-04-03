// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProjectDetil.cs
using LJCNetCommon;
using LJCWinFormCommon;
using ProjectFilesDAL;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  internal partial class ProjectDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal ProjectDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCCodeGroup = null;
      LJCCodeLine = null;
      LJCIsUpdate = false;
      LJCName = null;
      LJCRecord = null;
      LJCSolution = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ProjectDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      DataRetrieve();
      CenterToParent();
    }

    // Paint the form background.
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
        , EndColor);
    }
    #endregion

    #region Data Methods

    // Retrieves the initial control data.
    private void DataRetrieve()
    {
      Cursor = Cursors.WaitCursor;
      Text = "Project Detail";
      if (NetString.HasValue(LJCName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var parentKey = GetParentKey();
        //var manager = Managers.ProjectManager;
        //mOriginalRecord = manager.Retrieve(parentKey, LJCName);
        mOriginalRecord = Projects.LJCRetrieve(parentKey, LJCName);
        GetRecordValues(mOriginalRecord);

        NameText.ReadOnly = true;
        PathText.Select();
        PathText.Select(0, 0);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new Project();
        CodeLineText.Text = LJCCodeLine;
        CodeGroupText.Text = LJCCodeGroup;
        SolutionText.Text = LJCSolution;

        NameText.Select();
        NameText.Select(0, 0);
      }
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(Project dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        CodeLineText.Text = LJCCodeLine;
        CodeGroupText.Text = LJCCodeGroup;
        SolutionText.Text = LJCSolution;
        NameText.Text = dataRecord.Name;
        PathText.Text = dataRecord.Path;
      }
    }

    // Creates and returns a record object with the data from
    private Project SetRecordValues()
    {
      Project retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new Project();
      }

      // In control order.
      if (!LJCIsUpdate)
      {
        retValue.Name = NameText.Text.Trim();
      }
      retValue.Path = FormCommon.SetString(PathText.Text);

      // Get Reference key values.
      retValue.CodeLine = LJCCodeLine;
      retValue.CodeGroup = LJCCodeGroup;
      retValue.Solution = LJCSolution;
      return retValue;
    }

    // Resets the empty record values.
    private void ResetRecordValues(Project dataRecord)
    {
      // In control order.
      dataRecord.Path = FormCommon.SetString(dataRecord.Path);
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();
      var manager = Managers.ProjectManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          //manager.Update(LJCRecord);
          Projects.LJCUpdate(LJCRecord);
          manager.RecreateFile(Projects);
          ResetRecordValues(LJCRecord);
        }
        else
        {
          var parentKey = GetParentKey();
          //manager.Add(parentKey, LJCRecord.Name, LJCRecord.Path);
          Projects.Add(parentKey, LJCRecord.Name, LJCRecord.Path);
          manager.RecreateFile(Projects);
          ResetRecordValues(LJCRecord);
        }
      }
      Cursor = Cursors.Default;
      return retValue;
    }

    // Creates the parent key.
    private ProjectParentKey GetParentKey()
    {
      var retValue = new ProjectParentKey()
      {
        CodeLine = LJCCodeLine,
        CodeGroup = LJCCodeGroup,
        Solution = LJCSolution
      };
      return retValue;
    }

    // Check for saved data.
    private bool IsDataSaved()
    {
      bool retValue = false;

      FormCancelButton.Select();
      if (IsValid() && DataSave())
      {
        retValue = true;
      }
      return retValue;
    }

    // Validates the data.
    private bool IsValid()
    {
      bool retValue = true;

      var builder = new StringBuilder(64);
      builder.AppendLine("Invalid or Missing Data:");

      if (!NetString.HasValue(NameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {NameLabel.Text}");
      }

      if (!retValue)
      {
        var title = "Data Entry Error";
        var message = builder.ToString();
        MessageBox.Show(message, title, MessageBoxButtons.OK
          , MessageBoxIcon.Exclamation);
      }
      return retValue;
    }
    #endregion

    #region Setup Methods

    // Configure the initial control settings.
    private void ConfigureControls()
    {
      if (AutoScaleMode == AutoScaleMode.Font)
      {
      }
    }

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = ValuesProjectFiles.Instance;
      Data = values.Data;
      Projects = Data.Projects;
      Managers = values.Managers;
      BeginColor = values.BeginColor;
      EndColor = values.EndColor;

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace();

      ConfigureControls();
      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace()
    {
      NameText.KeyPress += TextBoxNoSpace_KeyPress;
      NameText.TextChanged += TextBoxNoSpace_TextChanged;
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
      if (IsDataSaved())
      {
        LJCOnChange();
        DialogResult = DialogResult.OK;
      }
    }
    #endregion

    #region KeyEdit Event Handlers

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
        var prevStart = textBox.SelectionStart;
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = prevStart;
      }
    }
    #endregion

    #region Properties

    // Gets or sets the CodeGroup ID value.
    internal string LJCCodeGroup { get; set; }

    // Gets or sets the CodeLine ID value.
    internal string LJCCodeLine { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // Gets a reference to the record object.
    internal Project LJCRecord { get; private set; }

    // Gets or sets the primary ID value.
    internal string LJCName { get; set; }

    // Gets or sets the Solution ID value.
    internal string LJCSolution { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the Data object.
    private ProjectFilesData Data { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // The Managers object.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Projects object.
    private Projects Projects { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private Project mOriginalRecord;
    #endregion
  }
}
