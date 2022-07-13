// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// LJCTabPanel.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>A Tab control in a panel.</summary>
  public partial class LJCTabPanel : Panel
  {
    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LJCTabPanel()
    {
      InitializeComponent();
    }

    #region Control Event Overrides

    // The DragOver event method override.
    /// <include path='items/OnDragOver/*' file='Doc/LJCTabPanel.xml'/>
    protected override void OnDragOver(DragEventArgs drgevent)
    {
      Point gridPoint;

      base.OnDragOver(drgevent);

      if (drgevent.Data.GetDataPresent(typeof(TabPage)))
      {
        gridPoint = PointToClient(new Point(drgevent.X, drgevent.Y));
        if (gridPoint.X > ClientRectangle.Width - 20)
        {
          LJCOnAddTile();
        }
        else
        {
          LJCOnRemoveTile();
        }
      }
    }
    #endregion

    // The AddTile event method.
    /// <include path='items/OnAddTile/*' file='Doc/LJCTabPanel.xml'/>
    protected void LJCOnAddTile()
    {
      LJCAddTile?.Invoke(this, new EventArgs());
    }

    // The remove tile event method.
    /// <include path='items/OnRemoveTile/*' file='Doc/LJCTabPanel.xml'/>
    protected void LJCOnRemoveTile()
    {
      LJCRemoveTile?.Invoke(this, new EventArgs());
    }

    /// <summary>The AddTile event.</summary>
    public event EventHandler<EventArgs> LJCAddTile;

    /// <summary>The RemoveTile event.</summary>
    public event EventHandler<EventArgs> LJCRemoveTile;
  }
}
