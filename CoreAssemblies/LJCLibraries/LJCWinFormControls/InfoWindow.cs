// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// InfoWindow.cs
using LJCNetCommon;
using LJCWinFormControls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

// Constructors
//   public InfoWindow(Point location, string text = null
//     , string contents = null)
// Event Methods
//   protected void LJCOnClose()
//   protected override void OnClosing(CancelEventArgs e)
// Properties
//   public string LJCContents
//   public string LJCText
// Class Data
//   public event EventHandler<EventArgs> LJCCloseEvent;

namespace LJCWinFormControls
{
  /// <summary>The Info window.</summary>
  public partial class InfoWindow : Form
  {
    // ******************************
    #region Constructors
    // ******************************

    /// <summary>Initializes an object instance.</summary>
    public InfoWindow()
    {
      // Initialize properties.
      LJCText = null;
      LJCContents = null;
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    // ********************
    public InfoWindow(string text = null, string contents = null
      , Point? location = null) : this()
    {
      InitializeComponent();

      LJCLocation = location;
      LJCText = text;
      LJCContents = contents;
    }
    #endregion

    // ******************************
    #region Form Event Handlers
    // ******************************

    // Handles the form Load event.
    // ********************
    private void InfoWindow_Load(object sender, EventArgs e)
    {
      SetContents();
      if (null == LJCLocation)
      {
        CenterToScreen();
      }
      else
      {
        Location = (Point)LJCLocation;
      }
    }
    #endregion

    #region Methods

    /// <summary>Gets the Contents.</summary>
    public string Contents()
    {
      return InfoRTBox.Text;
    }

    // Sets the contents after the form is loaded.
    private void SetContents()
    {
      if (InfoRTBox != null
        && NetString.HasValue(LJCContents))
      {
        InfoRTBox.Text = LJCContents;
      }
    }
    #endregion

    // ******************************
    #region Event Methods
    // ******************************

    // Fires the OnClose event.
    /// <include path='items/OnClose/*' file='Doc/InfoWindow.xml'/>
    // ********************
    protected void LJCOnClose()
    {
      LJCCloseEvent?.Invoke(this, new EventArgs());
    }

    // Fires the OnClosing event.
    /// <include path='items/OnClosing/*' file='Doc/InfoWindow.xml'/>
    // ********************
    protected override void OnClosing(CancelEventArgs e)
    {
      base.OnClosing(e);
      LJCOnClose();
    }
    #endregion

    // ******************************
    #region Control Event Handlers
    // ******************************

    // Handles the OKButton click event.
    // ********************
    private void OKButton_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    // ******************************
    #region Properties
    // ******************************

    /// <summary>Gets or sets the InfoWindow contents.</summary>
    public string LJCContents
    {
      get { return mLJCContents; }
      set
      {
        mLJCContents = NetString.InitString(value);
      }
    }
    private string mLJCContents;

    /// <summary>Gets or sets the form Title text.</summary>
    public string LJCText
    {
      get { return Text; }
      set { Text = !NetString.HasValue(value) ? Text : value.Trim(); }
    }

    // The form location.
    private Point? LJCLocation { get; set; }

    #endregion

    // ******************************
    #region Class Data
    // ******************************

    /// <summary>The Close event.</summary>
    public event EventHandler<EventArgs> LJCCloseEvent;

    #endregion
  }
}
