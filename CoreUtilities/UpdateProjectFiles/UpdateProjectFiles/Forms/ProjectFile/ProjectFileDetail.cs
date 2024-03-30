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
      LJCFileName = null;
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
      if (NetString.HasValue(LJCFileName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var parentKey = GetParentKey();
        //var manager = Managers.ProjectFileManager;
        //mOriginalRecord = manager.Retrieve(parentKey, LJCFileName);
        mOriginalRecord = ProjectFiles.LJCRetrieve(parentKey
          , LJCFileName);
        GetRecordValues(mOriginalRecord);

        FileNameText.ReadOnly = true;
        SourceCodeLineText.Select();
        SourceCodeLineText.Select(0, 0);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new ProjectFile();

        // Set parent values.
        TargetCodeLineText.Text = LJCTargetLine;
        TargetCodeGroupText.Text = LJCTargetGroup;
        TargetSolutionText.Text = LJCTargetSolution;
        TargetProjectText.Text = LJCTargetProject;
        TargetPathCodeGroupText.Text = LJCTargetGroup;
        TargetPathSolutionText.Text = LJCTargetSolution;
        TargetPathProjectText.Text = LJCTargetProject;

        FileNameText.Select();
        FileNameText.Select(0, 0);
      }
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
        FileNameText.Text = dataRecord.FileName;
        TargetPathCodeGroupText.Text = dataRecord.TargetPathCodeGroup;
        TargetPathSolutionText.Text = dataRecord.TargetPathSolution;
        TargetPathProjectText.Text = dataRecord.TargetPathProject;
        SourceCodeLineText.Text = dataRecord.SourceCodeLine;
        SourceCodeGroupText.Text = dataRecord.SourceCodeGroup;
        SourceSolutionText.Text = dataRecord.SourceSolution;
        SourceProjectText.Text = dataRecord.SourceProject;
        SourceFilePathText.Text = dataRecord.SourceFilePath;
        TargetFilePathText.Text = dataRecord.TargetFilePath;
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
      retValue.FileName = Trim(FileNameText);
      retValue.TargetPathCodeGroup = Trim(TargetPathCodeGroupText);
      retValue.TargetPathSolution = Trim(TargetPathSolutionText);
      retValue.TargetPathProject = Trim(TargetPathProjectText);
      retValue.SourceCodeLine = Trim(SourceCodeLineText);
      retValue.SourceCodeGroup = Trim(SourceCodeGroupText);
      retValue.SourceSolution = Trim(SourceSolutionText);
      retValue.SourceProject = Trim(SourceProjectText);
      retValue.SourceFilePath = Trim(SourceFilePathText);
      retValue.TargetFilePath = Trim(TargetFilePathText);

      // Get Reference key values.
      retValue.TargetCodeLine = LJCTargetLine;
      retValue.TargetCodeGroup = LJCTargetGroup;
      retValue.TargetSolution = LJCTargetSolution;
      retValue.TargetProject = LJCTargetProject;
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
          //manager.Update(LJCRecord);
          ProjectFiles.LJCUpdate(LJCRecord);
          manager.RecreateFile(ProjectFiles);
        }
        else
        {
          var parentKey = GetParentKey();
          var sourceKey = GetSourceKey();
          var projectFile = ProjectFiles.Add(parentKey, sourceKey
            , LJCRecord.FileName, LJCRecord.TargetFilePath
            , LJCRecord.SourceFilePath);
          projectFile.TargetPathCodeGroup = LJCRecord.TargetPathCodeGroup;
          projectFile.TargetPathSolution = LJCRecord.TargetPathSolution;
          projectFile.TargetPathProject = LJCRecord.TargetPathProject;
          manager.RecreateFile(ProjectFiles);
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
        CodeLine = Trim(SourceCodeLineText),
        CodeGroup = Trim(SourceCodeGroupText),
        Solution = Trim(SourceSolutionText),
        Project = Trim(SourceProjectText)
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

      if (!NetString.HasValue(FileNameText.Text))
      {
        retValue = false;
        builder.AppendLine($"  {FileNameLabel.Text}");
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

    // Gets the trimmed textbox value.
    private string Trim(TextBox textBox)
    {
      return textBox.Text.Trim();
    }
    #endregion

    #region Action Event Handlers

    // Selects the Source File info.
    private void DetailSource_Click(object sender, EventArgs e)
    {
      var dataHelper = new DataProjectFiles(Data);
      var codeLineName = Trim(TargetCodeLineText);
      var codeGroupName = Trim(TargetCodeGroupText);
      var solutionName = Trim(TargetSolutionText);
      var projectName = Trim(TargetProjectText);
      var targetFilePath = Trim(TargetFilePathText);
      var folder = dataHelper.GetFileSpec(codeLineName, codeGroupName
        , solutionName, projectName, targetFilePath);

      var filter = "DLLs(*.dll)|*.dll|All files(*.*)|*.*";
      var fileName = Trim(FileNameText);
      var fileSpec = FormCommon.SelectFile(filter, folder, fileName);
      if (fileSpec != null)
      {
        var projectFile = dataHelper.ProjectFileValues(fileSpec, targetFilePath);
        if (projectFile != null)
        {
          FileNameText.Text = projectFile.FileName;
          TargetPathProjectText.Text = projectFile.TargetPathProject;
          if (!NetString.HasValue(Trim(TargetFilePathText)))
          {
            TargetFilePathText.Text = projectFile.TargetFilePath;
          }
          SourceCodeLineText.Text = projectFile.SourceCodeLine;
          SourceCodeGroupText.Text = projectFile.SourceCodeGroup;
          SourceSolutionText.Text = projectFile.SourceSolution;
          SourceProjectText.Text = projectFile.SourceProject;
          SourceFilePathText.Text = projectFile.SourceFilePath;
        }
      }
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
      ProjectFiles = Data.ProjectFiles;
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
      FileNameText.KeyPress += TextBoxNoSpace_KeyPress;
      FileNameText.TextChanged += TextBoxNoSpace_TextChanged;
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
    internal string LJCFileName { get; set; }

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
    private ProjectFilesData Data { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // The Managers object.
    private ManagersProjectFiles Managers { get; set; }

    // Gets or sets the ProjectFiles object.
    private ProjectFiles ProjectFiles { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private ProjectFile mOriginalRecord;
    #endregion
  }
}
