// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// CodeLineDetil.cs
using LJCNetCommon;
using LJCWinFormCommon;
using ProjectFilesDAL;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UpdateProjectFiles
{
  // Initializes an object instance.
  public partial class CodeLineDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    public CodeLineDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCName = null;
      LJCIsUpdate = false;
      LJCRecord = null;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void CodeLineDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
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
      Text = "Code Line Detail";
      if (NetString.HasValue(LJCName))
      {
        Text += " - Edit";
        LJCIsUpdate = true;
        var manager = Managers.CodeLineManager;
        mOriginalRecord = manager.Retrieve(LJCName);
        GetRecordValues(mOriginalRecord);
      }
      else
      {
        Text += " - New";
        LJCIsUpdate = false;
        LJCRecord = new CodeLine();
      }
      NameText.Select();
      NameText.Select(0, 0);
      Cursor = Cursors.Default;
    }

    // Gets the record values and copies them to the controls.
    private void GetRecordValues(CodeLine dataRecord)
    {
      if (dataRecord != null)
      {
        // In control order.
        NameText.Text = dataRecord.Name;
        PathText.Text = dataRecord.Path;
      }
    }

    // Creates and returns a record object with the data from
    private CodeLine SetRecordValues()
    {
      CodeLine retValue = null;

      if (mOriginalRecord != null)
      {
        retValue = mOriginalRecord.Clone();
      }
      if (null == retValue)
      {
        retValue = new CodeLine();
      }

      // In control order.
      retValue.Name = NameText.Text.Trim();
      retValue.Path = FormCommon.SetString(PathText.Text);
      return retValue;
    }

    // Resets the empty record values.
    private void ResetRecordValues(CodeLine dataRecord)
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
      var manager = Managers.CodeLineManager;

      if (retValue)
      {
        if (LJCIsUpdate)
        {
          manager.Update(LJCRecord);
          ResetRecordValues(LJCRecord);
        }
        else
        {
          manager.Add(LJCRecord.Name, LJCRecord.Path);
          ResetRecordValues(LJCRecord);
        }
      }
      Cursor = Cursors.Default;
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

    #region Properties

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // Gets a reference to the record object.
    internal CodeLine LJCRecord { get; private set; }

    // Gets or sets the primary ID value.
    internal string LJCName { get; set; }

    // The Managers object.
    internal ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    private CodeLine mOriginalRecord;
    //private readonly ValuesUpdateProjectFiles mValues;
    #endregion
  }
}
