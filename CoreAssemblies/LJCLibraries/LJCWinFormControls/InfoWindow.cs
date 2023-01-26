// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// InfoWindow.cs
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>The Info window.</summary>
  public partial class InfoWindow : Form
  {
    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public InfoWindow()
    {
      InitializeComponent();
    }

    #region Form Event Handlers

    // 
    private void InfoWindow_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    // 
    private void OKButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    // Handles the dialog closing.
    /// <include path='items/OnClosing/*' file='Doc/InfoWindow.xml'/>
    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      LJCOnClose();
    }
    #endregion

    #region Event Methods

    // Fires the OnClose event.
    /// <include path='items/OnClose/*' file='Doc/InfoWindow.xml'/>
    protected void LJCOnClose()
    {
      LJCCloseEvent?.Invoke(this, new EventArgs());
    }
    #endregion

    /// <summary>Gets or sets the form Title text.</summary>
    public string LJCText
    {
      get { return Text; }
      set { Text = string.IsNullOrWhiteSpace(value) ? Text : value.Trim(); }
    }

    /// <summary>Gets or sets the Info data.</summary>
    public string LJCInfoData
    {
      get { return InfoRTBox.Text; }
      set { InfoRTBox.Text = value; }
    }

    /// <summary>The Close event.</summary>
    public event EventHandler<EventArgs> LJCCloseEvent;
  }
}
