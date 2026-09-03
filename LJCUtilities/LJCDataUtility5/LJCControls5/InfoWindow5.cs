// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// InfoWindow5.cs
using LJCDataAccess5;
using LJCDataAccessConfig5;
using LJCNetCommon5;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace LJCControls5
{
  /// <summary>The Info window.</summary>
  public partial class InfoWindow : Form
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Constructor/*'/>
    public InfoWindow()
    {
      // Initialize properties.
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/InfoWindow.xml'
    ///  path='items/ParamConstructor/*'/>
    public InfoWindow(string? text = null, string? contents = null
      , Point? location = null) : this()
    {
      InitializeComponent();
      ExecuteButton.Visible = false;

      LJCText = text;
      LJCContents = contents;
      LJCLocation = location;
    }
    #endregion

    #region Form Event Handlers

    // Handles the form Load event.
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

    // Gets the Contents.
    /// <include file='Doc/InfoWindow.xml'
    ///  path='items/Contents/*'/>
    public string Contents()
    {
      return InfoRTBox.Text;
    }

    // Sets the execute button visibility.
    /// <include file='Doc/InfoWindow.xml'
    ///  path='items/ShowExecuteButton/*'/>
    public void ShowExecuteButton(bool visible = false)
    {
      ExecuteButton.Visible = visible;
    }

    // Sets the contents after the form is loaded.
    private void SetContents()
    {
      if (InfoRTBox != null
        && LJC.HasText(LJCContents))
      {
        InfoRTBox.Text = LJCContents;
      }
    }
    #endregion

    #region Control Event Handlers

    // Handles the ExecuteButton click event.
    private void ExecuteButton_Click(object sender, EventArgs e)
    {
      var text = InfoRTBox.SelectedText;
      if (!LJC.HasText(text))
      {
        text = Text;
      }

      var connectionString = DataConfig.ConnectionString(trusted: true);
      var providerName = LJCDataConfig.ProviderName();
      if (connectionString != null
        && LJC.HasText(text))
      {
        var dataAccess = new LJCDataAccess(connectionString, providerName);
        dataAccess.ExecuteScriptText(text);
        var errorText = dataAccess.ErrorText;
        if (LJC.HasText(errorText))
        {
          MessageBox.Show(errorText);
        }
      }
    }

    // Handles the OKButton click event.
    private void OKButton_Click(object sender, EventArgs e)
    {
      Close();
    }
    #endregion

    #region Properties

    /// <include file='Doc/InfoWindow.xml'
    ///  path='items/DataConfig/*'/>
    public LJCDataConfig DataConfig
    {
      get => _DataConfig;
      set
      {
        if (value != null)
        {
          _DataConfig = value;
          ExecuteButton.Visible = true;
        }
      }
    }
    private LJCDataConfig _DataConfig = null!;

    // Gets or sets the InfoWindow contents.
    /// <include file='Doc/InfoWindow.xml'
    ///  path='items/LJCContents/*'/>
    public string? LJCContents
    {
      get => _LJCContents;
      set
      {
        _LJCContents = value?.Trim();
      }
    }
    private string? _LJCContents = null!;

    // Gets or sets the form Title text.
    /// <include file='Doc/InfoWindow.xml'
    ///  path='items/LJCText/*'/>
    public string? LJCText
    {
      get => Text;
      set
      {
        Text = value?.Trim();
      }
    }

    // The form location.
    private Point? LJCLocation { get; set; }
    #endregion
  }
}
