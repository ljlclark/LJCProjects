// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// ProjectFileDetail.cs
using LJCWinFormCommon;
using ProjectFilesDAL;
using System;
using System.Drawing;
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
      LJCCodeLine = null;
      LJCCodeGroup = null;
      LJCSolution = null;
      LJCProject = null;
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
    private void ProjectFileDetail_Load(object sender, EventArgs e)
    {
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

    #region Properties

    // Gets or sets the CodeGroup ID value.
    internal string LJCCodeGroup { get; set; }

    // Gets or sets the CodeLine ID value.
    internal string LJCCodeLine { get; set; }

    // Gets the LJCIsUpdate value.
    internal bool LJCIsUpdate { get; private set; }

    // Gets a reference to the record object.
    internal ProjectFile LJCRecord { get; private set; }

    // Gets or sets the primary ID value.
    internal string LJCName { get; set; }

    // The Managers object.
    internal ManagersProjectFiles Managers { get; set; }

    // Gets or sets the Project ID value.
    internal string LJCProject { get; set; }

    // Gets or sets the Solution ID value.
    internal string LJCSolution { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    // The Change event.
    internal event EventHandler<EventArgs> LJCChange;

    //private readonly Project mOriginalRecord;
    //private readonly ValuesUpdateProjectFiles mValues;
    #endregion
  }
}
