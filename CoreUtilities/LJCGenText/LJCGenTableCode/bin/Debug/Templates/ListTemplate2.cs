// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _FullAppName_
// #Value _FullAppName_
// _FullAppName_.cs
using _FullAppName_DAL;
// #SectionEnd Title
using LJCDBClientLib;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCWinFormControls;
using System;
using System.Windows.Forms;

// #SectionBegin Class
// #Value _AppName_
// #Value _ClassName_
// #Value _CollectionName_
// #Value _FullAppName_
// #Value _NameSpace_
// #Value _VarClassName_
namespace _Namespace_
{
	// The list form.
	/// <include path='items/ListFormDAW/*' file='../../LJCGenDoc/Common/List.xml'/>
	internal partial class _FullAppName_List : Form
	{
    #region Constructors

    // Initializes an object instance.
    // ********************
    internal _FullAppName_List()
		{
			InitializeComponent();

			// Initialize property values.
			LJCIsSelect = false;
		}
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    // ********************
    private void _ClassName_List_Load(object sender, EventArgs e)
		{
			InitializeControls();
      CenterToParent();
		}
    #endregion

    #region Item Change Processing

    // Execute the list and related item functions.
    // ********************
    internal void DoChange(Change change)
    {
      Cursor = Cursors.WaitCursor;
      switch (change)
      {
        case Change.Startup:
          ConfigureControls();
          RestoreControlValues();

          // Load first control.
          m_ClassName_ComboCode.DatRetrieve();
          break;

        case Change.Combo:
          m_ClassName_GridCode.DataRetrieve();
          break;

        case Change._ClassName_:
          //DataRetrieveChild();
          _ClassName_Grid.LJCSetLastRow();
          //_ClassName_Grid.LJCSetCounter(_ClassName_Counter);
          break;
      }
      SetControlState();
      Cursor = Cursors.Default;
    }

    // The ChangeType values.
    internal enum Change
    {
      Startup,
      Combo,
      _ClassName_
    }

    #region Item Change Support

    // Starts the Timer with the Change value.
    // ********************
    internal void TimedChange(Change change)
    {
      ChangeTimer.DoChange(change.ToString());
    }

    // Start the Change processing.
    // ********************
    private void StartChangeProcessing()
    {
      ChangeTimer = new ChangeTimer();
      ChangeTimer.ItemChange += ChangeTimer_ItemChange;
      TimedChange(Change.Startup);
    }

    // Change Event Handler
    // ********************
    private void ChangeTimer_ItemChange(object sender, EventArgs e)
    {
      Change changeType;

      changeType = (Change)Enum.Parse(typeof(Change)
        , ChangeTimer.ChangeName);
      DoChange(changeType);
    }

    // Gets or sets the ChangeTimer object.
    private ChangeTimer ChangeTimer { get; set; }
    #endregion
    #endregion

    #region Action Event Handlers

    // Shared menu Exit event handler.
    // ********************
    internal void Exit_Click(object sender, EventArgs e)
    {
      SaveControlValues();
      Close();
    }
    #endregion
  }
}
// #SectionEnd Class
