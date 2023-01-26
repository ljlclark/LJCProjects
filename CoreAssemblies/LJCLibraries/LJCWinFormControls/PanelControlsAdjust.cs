// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// PanelControlsAdjust.cs

namespace LJCWinFormControls
{
  /// <summary>Contains standard panel control adjustment values.</summary>
  public class PanelControlsAdjust
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public PanelControlsAdjust()
    {
    }

    // Initializes an object instance.
    /// <include path='items/PanelControlsAdjustC/*' file='Doc/PanelControlsAdjust.xml'/>
    public PanelControlsAdjust(int headerWidthAdjust = 0, int panelWidthAdjust = 0
      , int gridWidthAdjust = 0, int gridHeightAdjust = 0, int gridLeft = 0
      , int gridTop = 0)
    {
      HeaderWidthAdjust = headerWidthAdjust;
      PanelWidthAdjust = panelWidthAdjust;
      GridWidthAdjust = gridWidthAdjust;
      GridHeightAdjust = gridHeightAdjust;
      GridLeft = gridLeft;
      GridTop = gridTop;
    }
    #endregion

    #region Properties

    /// <summary>Header Width adjustment value.</summary>
    public int HeaderWidthAdjust { get; set; }

    /// <summary>ToolPanel Width adjustment value.</summary>
    public int PanelWidthAdjust { get; set; }

    /// <summary>Grid Width adjustment value.</summary>
    public int GridWidthAdjust { get; set; }

    /// <summary>Grid Height adjustment value.</summary>
    public int GridHeightAdjust { get; set; }

    /// <summary>Grid Left value.</summary>
    public int GridLeft { get; set; }

    /// <summary>Grid Top value.</summary>
    public int GridTop { get; set; }
    #endregion
  }
}
