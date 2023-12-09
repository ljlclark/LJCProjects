// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _NameSpace_
// #Value _VarClassName_
// _FullAppName_.cs
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using _FullAppName_DAL;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace _Namespace_
{
  // The list form.
  /// <include path='items/ListFormDAW/*' file='../../LJCDocLib/Common/List.xml'/>
  internal partial class _ClassName_List : Form
  {
    #region Constructors

    // Initializes an object instance.
    internal _ClassName_List()
    {
      Cursor = Cursors.WaitCursor;
      InitializeComponent();

      // Initialize property values.
      LJCHelpFile = "_AppName_.chm";
      LJCHelpPageList = "_ClassName_List.html";
      LJCHelpPageDetail = "_ClassName_Detail.html";
      LJCIsSelect = false;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.LightSkyBlue;
      mViewTableName = _ClassName_.TableName;
      Cursor = Cursors.Default;
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void _ClassName_List_Load(object sender, EventArgs e)
    {
      InitializeControls();
      CenterToParent();
    }
    #endregion

    #region Action Event Handlers

    #region _ClassName_

    // Calls the New method.
    private void _ClassName_ToolNew_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoNew();
    }

    // Calls the Edit method.
    private void _ClassName_ToolEdit_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoEdit();
    }

    // Calls the Delete method.
    private void _ClassName_ToolDelete_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoDelete();
    }

    // Calls the New method.
    private void _ClassName_MenuNew_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoNew();
    }

    // Calls the Edit method.
    private void _ClassName_MenuEdit_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoEdit();
    }

    // Calls the Delete method.
    private void _ClassName_MenuDelete_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoDelete();
    }

    // Calls the Refresh method.
    private void _ClassName_MenuRefresh_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoRefresh();
    }

    // Export a text file.
    private void MainMenuExportText_Click(object sender, EventArgs e)
    {
      string extension = mSettings.ExportTextExtension;
      string fileSpec = $@"ExportFiles\_ClassName_.{extension}";
      _ClassName_Grid.LJCExportData(fileSpec);
    }

    // Export a CSV file.
    private void MainMenuExportCSV_Click(object sender, EventArgs e)
    {
      string fileSpec = $@"ExportFiles\_ClassName_.csv";
      _ClassName_Grid.LJCExportData(fileSpec);
    }

    // Calls the Select method.
    private void _ClassName_MenuSelect_Click(object sender, EventArgs e)
    {
      _ClassName_GridCode.DoSelect();
    }

    // Performs the Close function.
    private void _ClassName_MenuClose_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }

    // Shows the help page.
    private void _ClassName_MenuHelp_Click(object sender, EventArgs e)
    {
      Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
        , LJCHelpPageList);
    }
    #endregion
    #endregion

    #region Control Event Handlers

    #region Combo

    //// Handles the SelectedIndexChanged event.
    //private void Combo_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //	ChangeTimer.DoChange(Change.Startup.ToString());
    //}
    #endregion

    #region _ClassName_

    // Handles the form keys.
    private void _ClassName_Grid_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          DoDefault_ClassName_();
          e.Handled = true;
          break;

        case Keys.F1:
          Help.ShowHelp(this, LJCHelpFile, HelpNavigator.Topic
            , LJCHelpPageList);
          e.Handled = true;
          break;

        case Keys.F5:
          DoRefresh_ClassName_();
          e.Handled = true;
          break;

        case Keys.M:
          if (e.Control)
          {
            var position = FormCommon.GetMenuScreenPoint(_ClassName_Grid
              , MousePosition);
            _ClassName_Menu.Show(position);
            _ClassName_Menu.Select();
            e.Handled = true;
          }
          break;

        case Keys.Tab:
          if (e.Shift)
          {
            //Combo.Select();
          }
          else
          {
            //Combo.Select();
          }
          e.Handled = true;
          break;
      }
    }

    // Handles the MouseDoubleClick event.
    private void _ClassName_Grid_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      //if (_ClassName_Grid.LJCGetMouseRowIndex(e) > -1)
      if (_ClassName_Grid.LJCGetMouseRow(e) != null)
      {
        DoDefault_ClassName_();
      }
    }

    // Handles the MouseDown event.
    private void _ClassName_Grid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Right)
      {
        // LJCIsDifferentRow() Sets the LJCLastRowIndex for new row.
        _ClassName_Grid.Select();
        if (_ClassName_Grid.LJCIsDifferentRow(e))
        {
          // LJCSetCurrentRow sets the LJCAllowSelectionChange property.
          _ClassName_Grid.LJCSetCurrentRow(e);
          TimedChange(Change._ClassName_);
        }
      }
    }

    // Handles the SelectionChanged event.
    private void _ClassName_Grid_SelectionChanged(object sender, EventArgs e)
    {
      if (_ClassName_Grid.LJCAllowSelectionChange)
      {
        TimedChange(Change._ClassName_);
      }
      _ClassName_Grid.LJCAllowSelectionChange = true;
    }
    #endregion
    #endregion

    #region Properties

    // Gets or sets the parent ID value.
    internal int LJCParentID { get; set; }

    // Gets or sets the LJCParentName value.
    internal string LJCParentName
    {
      get { return mParentName; }
      set { mParentName = NetString.InitString(value); }
    }
    private string mParentName;

    // Gets or sets the LJCIsSelect value.
    internal bool LJCIsSelect { get; set; }

    // Gets a reference to the selected record.
    internal _ClassName_ LJCSelectedRecord { get; private set; }

    // The help file name.
    internal string LJCHelpFile
    {
      get { return mHelpFile; }
      set { mHelpFile = NetString.InitString(value); }
    }
    private string mHelpFile;

    // The List help page name.
    internal string LJCHelpPageList
    {
      get { return mHelpPageList; }
      set { mHelpPageList = NetString.InitString(value); }
    }
    private string mHelpPageList;

    // The Detail help page name.
    internal string LJCHelpPageDetail
    {
      get { return mHelpPageDetail; }
      set { mHelpPageDetail = NetString.InitString(value); }
    }
    private string mHelpPageDetail;

    // The Managers object.
    internal Managers_ClassName_ Managers { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }

    // Gets or sets the _ClassName_GridClass value.
    private _ClassName_GridClass _ClassName_GridClass { get; set; }
    #endregion

    #region Class Data

    private string mControlValuesFileName;
    private StandardUISettings mSettings;

    // Foreign Keys
    #endregion
  }
}
// #SectionEnd Class
