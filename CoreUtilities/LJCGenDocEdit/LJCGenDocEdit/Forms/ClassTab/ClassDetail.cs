﻿// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ClassDetail.cs
using LJCDBClientLib;
using LJCDocLibDAL;
using LJCWinFormCommon;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCGenDocEdit
{
  /// <summary>The DocClass detail dialog.</summary>
  public partial class ClassDetail : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Detail.xml'/>
    public ClassDetail()
    {
      InitializeComponent();

      // Initialize property values.
      LJCID = 0;
      LJCRecord = null;
      LJCIsUpdate = false;

      // Set default class data.
      BeginColor = Color.AliceBlue;
      EndColor = Color.FromArgb(235, 240, 250);
    }
    #endregion

    #region Form Event Handlers

    // Configures the form and loads the initial control data.
    private void ClassDetail_Load(object sender, EventArgs e)
    {
      AcceptButton = OKButton;
      CancelButton = FormCancelButton;
      InitializeControls();
      //
      CenterToParent();
    }

    // Paint the form background.
    /// <include path='items/OnPaintBackground/*' file='../../LJCDocLib/Common/Detail.xml'/>
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
      FormCommon.CreateGradient(e.Graphics, ClientRectangle, BeginColor
        , EndColor);
    }
    #endregion

    #region Setup Methods

    // Configures the controls and loads the selection control data.
    private void InitializeControls()
    {
      // Get singleton values.
      Cursor = Cursors.WaitCursor;
      var values = ValuesGenDocEdit.Instance;
      mSettings = values.StandardSettings;
      BeginColor = mSettings.BeginColor;
      EndColor = mSettings.EndColor;

      // Set control values.
      FormCommon.SetLabelsBackColor(Controls, BeginColor);
      SetNoSpace(NameText);
      SetNumericOnly(SequenceText);

      //HeadingText.MaxLength = DocAssemblyGroup.LengthHeading;
      Cursor = Cursors.Default;
    }

    // Sets the NoSpace events.
    private void SetNoSpace(TextBox textBox)
    {
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
    }

    // Sets the NoSpace events.
    private void SetNumericOnly(TextBox textBox)
    {
      textBox.KeyPress += TextBoxNumeric_KeyPress;
      textBox.KeyPress += TextBoxNoSpace_KeyPress;
      textBox.TextChanged += TextBoxNoSpace_TextChanged;
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
        textBox.Text = FormCommon.StripBlanks(textBox.Text);
        textBox.SelectionStart = textBox.Text.Trim().Length;
      }
    }

    // Only allows numbers or edit keys.
    private void TextBoxNumeric_KeyPress(object sender, KeyPressEventArgs e)
    {
      e.Handled = FormCommon.HandleNumberOrEditKey(e.KeyChar);
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the primary ID value.</summary>
    internal short LJCID { get; set; }

    /// <summary>Gets the LJCIsUpdate value.</summary>
    internal bool LJCIsUpdate { get; private set; }

    /// <summary>Gets a reference to the record object.</summary>
    internal DocClass LJCRecord { get; private set; }

    // The Managers object.
    internal ManagersDocGen Managers { get; set; }

    // Gets or sets the Begin Color.
    private Color BeginColor { get; set; }

    // Gets or sets the End Color.
    private Color EndColor { get; set; }
    #endregion

    #region Class Data

    /// <summary>The Change event.</summary>
    public event EventHandler<EventArgs> LJCChange;

    private StandardUISettings mSettings;
    #endregion
  }
}
