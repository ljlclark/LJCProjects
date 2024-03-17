// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProjectFileDetail.cs
using LJCNetCommon;
using LJCWinFormCommon;
using ProjectFilesDAL;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  public partial class ProjectFileDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    public ProjectFileDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCIsUpdate = false;
      LJCRecord = null;
      LJCSourceFileName = null;
      LJCTargetGroup = null;
      LJCTargetLine = null;
      LJCTargetProject = null;
      LJCTargetSolution = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ProjectFileDetail_Load(object sender, EventArgs e)
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
      Text = "Project File Detail";
      if (NetString.HasValue(LJCSourceFileName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var parentKey = GetParentKey();
        // *** Begin *** Change - Data
        //var manager = Managers.ProjectFileManager;
        //mOriginalRecord = manager.Retrieve(parentKey, LJCSourceFileName);
        mOriginalRecord = ProjectFiles.LJCRetrieve(parentKey);
        // *** End   *** Change - Data
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new ProjectFile();
        TargetCodeLineText.Text = LJCTargetLine;
        TargetCodeGroupText.Text = LJCTargetGroup;
        TargetSolutionText.Text = LJCTargetSolution;
        TargetProjectText.Text = LJCTargetProject;
      }
      SourceFileNameText.Select();
      SourceFileNameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(ProjectFile dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        TargetCodeLineText.Text = dataRecord.TargetCodeLine;
        TargetCodeGroupText.Text = dataRecord.TargetCodeGroup;
        TargetSolutionText.Text = dataRecord.TargetSolution;
        TargetProjectText.Text = dataRecord.TargetProject;
        SourceFileNameText.Text = dataRecord.SourceFileName;
        TargetFilePathText.Text = dataRecord.TargetFilePath;
        SourceCodeLineText.Text = dataRecord.SourceCodeLine;
        SourceCodeGroupText.Text = dataRecord.SourceCodeGroup;
        SourceSolutionText.Text = dataRecord.SourceSolution;
        SourceProjectText.Text = dataRecord.SourceProject;
        SourceFilePathText.Text = dataRecord.SourceFilePath;
      }
    }

    // Creates and returns a record object with the data from
    private ProjectFile SetRecordValues()
    {
      ProjectFile retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new ProjectFile();
      }

      // In control order.
      retValue.SourceFileName = SourceFileNameText.Text.Trim();
      retValue.SourceCodeLine = SourceCodeLineText.Text.Trim();
      retValue.SourceCodeGroup = SourceCodeGroupText.Text.Trim();
      retValue.SourceSolution = SourceCodeLineText.Text.Trim();
      retValue.SourceProject = SourceProjectText.Text.Trim();

      // Get Reference key values.
      retValue.TargetCodeLine = LJCTargetLine;
      retValue.TargetCodeGroup = LJCTargetGroup;
      retValue.TargetSolution = LJCTargetSolution;
      retValue.TargetProject = LJCTargetProject;
      retValue.SourceFileName = LJCSourceFileName;
      return retValue;
    }

    // Saves the data.
    private bool DataSave()
    {
      bool retValue = true;

      Cursor = Cursors.WaitCursor;
      LJCRecord = SetRecordValues();
      var manager = Managers.ProjectFileManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          // *** Begin *** Change - Data
          //manager.Update(LJCRecord);
          ProjectFiles.LJCUpdate(LJCRecord);
          //manager.WriteBackup();
          manager.RecreateFile(ProjectFiles);
          // *** End   *** Change - Data
        }
        else
        {
          var parentKey = GetParentKey();
          var sourceKey = GetSourceKey();
          // *** Begin *** Change - Data
          //manager.Add(parentKey, sourceKey, LJCRecord.SourceFileName
          //  , LJCRecord.SourceFilePath, LJCRecord.TargetFilePath);
          ProjectFiles.Add(parentKey, sourceKey, LJCRecord.TargetFilePath
            , LJCRecord.SourceFilePath);
          //manager.WriteBackup();
          manager.RecreateFile(ProjectFiles);
          // *** End   *** Change - Data
        }
      }
      Cursor = Cursors.Default;
      return retValue;
    }

    // Creates the parent key.
    private ProjectFileParentKey GetParentKey()
    {
      var retValue = new ProjectFileParentKey()
      {
        CodeLine = LJCTargetLine,
        CodeGroup = LJCTargetGroup,
        Solution = LJCTargetSolution,
        Project = LJCTargetProject
      };
      return retValue;
    }

    // Creates the source key.
    private ProjectFileParentKey GetSourceKey()
    {
      var retValue = new ProjectFileParentKey()
      {
        CodeLine = SourceCodeLineText.Text.Trim(),
        CodeGroup = SourceCodeGroupText.Text.Trim(),
        Solution = SourceSolutionText.Text.Trim(),
        Project = SourceProjectText.Text.Trim()
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

      if (!NetString.HasValue(SourceFileNameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {SourceFileNameLabel.Text}");
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
      var values = ValuesUpdateProjectFiles.Instance;
      // *** Begin *** Add - Data
      Data = values.Data;
      ProjectFiles = Data.ProjectFiles;
      // *** End   *** Add - Data
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
      SourceFileNameText.KeyPress += TextBoxNoSpace_KeyPress;
      SourceFileNameText.TextChanged += TextBoxNoSpace_TextChanged;
      SourceCodeLineText.KeyPress += TextBoxNoSpace_KeyPress;
      SourceCodeLineText.TextChanged += TextBoxNoSpace_TextChanged;
      SourceCodeGroupText.KeyPress += TextBoxNoSpace_KeyPress;
      SourceCodeGroupText.TextChanged += TextBoxNoSpace_TextChanged;
      SourceSolutionText.KeyPress += TextBoxNoSpace_KeyPress;
      SourceSolutionText.TextChanged += TextBoxNoSpace_TextChanged;
      SourceProjectText.KeyPress += TextBoxNoSpace_KeyPress;
      SourceProjectText.TextChanged += TextBoxNoSpace_TextChanged;
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

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // Gets a reference to the record object.
    internal ProjectFile LJCRecord { get; private set; }

    // Gets or sets the Source File ID value.
    internal string LJCSourceFileName { get; set; }

    // Gets or sets the Target CodeGroup ID value.
    internal string LJCTargetGroup { get; set; }

    // Gets or sets the Target CodeLine ID value.
    internal string LJCTargetLine { get; set; }

    // Gets or sets the Target Project ID value.
    internal string LJCTargetProject { get; set; }

    // Gets or sets the Target Solution ID value.
    internal string LJCTargetSolution { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the Data object.
    // *** Next Statement *** Add - Data
    private Data Data { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // The Managers object.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the ProjectFiles object.
    // *** Next Statement *** Add - Data
    private ProjectFiles ProjectFiles { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private ProjectFile mOriginalRecord;
    #endregion
  }
}
