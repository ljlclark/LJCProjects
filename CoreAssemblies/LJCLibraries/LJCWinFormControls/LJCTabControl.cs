// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJLCTabControl.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  /// <summary>Provides custom drag and drop functionality for a TabControl.</summary>
  public partial class LJCTabControl : TabControl
  {
    #region Constructors

    // Initializes an instance of the class.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public LJCTabControl()
    {
      InitializeComponent();
      InitializeControl();
    }

    // Instantiates an instance of the class and adds it to a container.
    /// <include path='items/LJCTabControlC/*' file='Doc/LJCTabControl.xml'/>
    public LJCTabControl(IContainer parentContainer)
    {
      parentContainer.Add(this);

      InitializeComponent();
      InitializeControl();
    }
    #endregion

    #region Override Event Methods

    // Provides custom MouseDown event code.
    /// <include path='items/OnMouseDown/*' file='Doc/LJCTabControl.xml'/>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);

      if (LJCAllowDrag)
      {
        // Initializes the drag and drop values.
        mIsDragStart = true;
        mDragStartBounds = CreateDragStartBounds(e.X, e.Y, 8, 6);
        mSourceTabPage = LJCGetTabPage(e.X, e.Y);
      }
    }

    // Provides custom MouseMove event code.
    /// <include path='items/OnMouseMove/*' file='Doc/LJCTabControl.xml'/>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      Point mousePoint;

      base.OnMouseMove(e);

      if (LJCAllowDrag)
      {
        // Starts the drag operation if the mouse moves outside the drag start bounds.
        mousePoint = new Point(e.X, e.Y);
        if (mIsDragStart
          && mSourceTabPage != null
          && mDragStartBounds.Contains(mousePoint) == false)
        {
          mIsDragStart = false;
          DoDragDrop(mSourceTabPage, DragDropEffects.Move);
        }
      }
    }

    // Provides custom DragOver event code.
    /// <include path='items/OnDragOver/*' file='Doc/LJCTabControl.xml'/>
    protected override void OnDragOver(DragEventArgs drgevent)
    {
      LJCTabControl sourceTabControl;
      TabPage sourceTabPage;
      TabPage targetTabPage;
      Point tabPoint;

      base.OnDragOver(drgevent);

      drgevent.Effect = DragDropEffects.None;

      if (drgevent.Data.GetDataPresent(typeof(TabPage)))
      {
        // Get source tab page.
        sourceTabPage = drgevent.Data.GetData(typeof(TabPage)) as TabPage;
        sourceTabControl = sourceTabPage.Parent as LJCTabControl;

        // Get target tab page.
        tabPoint = PointToClient(new Point(drgevent.X, drgevent.Y));
        if (1 == TabCount
          && TabPages[0].Name == "")
        {
          // Set target to temporary target page.
          targetTabPage = TabPages[0];
        }
        else
        {
          // Set target to drop target.
          targetTabPage = LJCGetTabPage(tabPoint.X, tabPoint.Y);
        }

        if (tabPoint.X > Width - 20)
        {
          // Add temporary target page.
          if (sourceTabControl == this)
          {
            LJCOnLJCPanelAdd();
          }
        }
        else
        {
          // Remove the temporary target page.
          if (targetTabPage != null
            && targetTabPage.Name == "")
          {
            LJCOnLJCPanelRemove();
          }
        }

        // Sets the drag effects icon to allow the move if the
        // target tab is not the same as the source tab.
        if (targetTabPage != null
          && sourceTabPage != targetTabPage)
        {
          drgevent.Effect = DragDropEffects.Move;
        }
      }
    }

    // Provides custom DragLeave event code.
    /// <include path='items/OnDragLeave/*' file='Doc/LJCTabControl.xml'/>
    protected override void OnDragLeave(EventArgs e)
    {
      base.OnDragLeave(e);

      // Allows the temporary tab page to be removed and to collapses the tile panel
      // if during the create function.
      if (TabCount == 1
        && TabPages[0].Name == "")
      {
        LJCOnLJCPanelRemove();
      }
    }

    // Provides custom DragDrop event code.
    /// <include path='items/OnDragDrop/*' file='Doc/LJCTabControl.xml'/>
    protected override void OnDragDrop(DragEventArgs drgevent)
    {
      TabControl sourceTabControl;
      TabPage sourceTabPage;
      TabPage targetTabPage;
      Point tabPoint;
      int targetIndex;

      base.OnDragDrop(drgevent);

      // Get source tab page.
      sourceTabPage = drgevent.Data.GetData(typeof(TabPage)) as TabPage;
      sourceTabControl = sourceTabPage.Parent as TabControl;

      // Get target tab page.
      if (TabCount == 1
        && TabPages[0].Name == "")
      {
        targetTabPage = TabPages[0];
      }
      else
      {
        tabPoint = PointToClient(new Point(drgevent.X, drgevent.Y));
        targetTabPage = LJCGetTabPage(tabPoint.X, tabPoint.Y);
      }

      if (targetTabPage != sourceTabPage
        && sourceTabControl != null
        && sourceTabPage != null
        && targetTabPage != null)
      {
        sourceTabControl.TabPages.Remove(sourceTabPage);
        if (targetTabPage.Name == "")
        {
          // Replace temporary tab page with source tab page.
          TabPages.Remove(targetTabPage);
          TabPages.Add(sourceTabPage);
        }
        else
        {
          // Move source tab page to target tab control.
          targetIndex = LJCGetTabPageIndex(targetTabPage);
          TabPages.Insert(targetIndex, sourceTabPage);
        }
        SelectedTab = sourceTabPage;
      }
    }

    // Provides custom MouseUp event code.
    /// <include path='items/OnMouseUp/*' file='Doc/LJCTabControl.xml'/>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);

      // Reset the drag start flag.
      mIsDragStart = false;
    }

    // Provides custom Resize event code.
    /// <include path='items/OnResize/*' file='Doc/LJCTabControl.xml'/>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      // Refresh the related controls as required.
      RefreshParentPanel();
    }
    #endregion

    #region Custom Event Methods

    // Calls the LJCPanelAdd event handlers.
    /// <include path='items/OnLJCPanelAdd/*' file='Doc/LJCTabControl.xml'/>
    protected void LJCOnLJCPanelAdd()
    {
      LJCPanelAdd?.Invoke(this, new EventArgs());
    }

    // Calls the LJCPanelRemove event handlers. 
    private void LJCOnLJCPanelRemove()
    {
      LJCPanelRemove?.Invoke(this, new EventArgs());
    }
    #endregion

    #region Public Methods

    // Closes the tiled split panel.
    /// <include path='items/LJCCloseEmptyPanel/*' file='Doc/LJCTabControl.xml'/>
    public void LJCCloseEmptyPanel()
    {
      LJCOnLJCPanelRemove();
    }

    // Gets the tab page with the matching text.
    /// <include path='items/LJCGetTabPage1/*' file='Doc/LJCTabControl.xml'/>
    public TabPage LJCGetTabPage(string text)
    {
      TabPage retValue = null;

      TabPage currentTabPage;
      for (int index = 0; index < TabPages.Count; index++)
      {
        currentTabPage = TabPages[index];
        if (currentTabPage.Text == text)
        {
          retValue = currentTabPage;
          break;
        }
      }
      return retValue;
    }

    // Gets the tab page if the position corresponds to a tab label.
    /// <include path='items/LJCGetTabPage2/*' file='Doc/LJCTabControl.xml'/>
    public TabPage LJCGetTabPage(int x, int y)
    {
      Rectangle tabRectangle;
      TabPage retValue = null;

      for (int index = 0; index < TabPages.Count; index++)
      {
        tabRectangle = GetTabRect(index);
        if (tabRectangle.Contains(new Point(x, y)))
        {
          retValue = TabPages[index];
          break;
        }
      }
      return retValue;
    }

    // Retrieves the tab page where the mouse was clicked.
    /// <include path='items/LJCGetTabPage3/*' file='Doc/LJCTabControl.xml'/>
    public TabPage LJCGetTabPage(MouseEventArgs e)
    {
      var retValue = LJCGetTabPage(e.X, e.Y);
      return retValue;
    }

    // Gets the tab index for a tab page.
    /// <include path='items/LJCGetTabPageIndex/*' file='Doc/LJCTabControl.xml'/>
    public int LJCGetTabPageIndex(TabPage tabPage)
    {
      int retVal = -1;

      for (int index = 0; index < TabPages.Count; index++)
      {
        if (tabPage.Equals(TabPages[index]))
        {
          retVal = index;
          break;
        }
      }
      return retVal;
    }

    // Moves a tab page.
    /// <include path='items/LJCMoveTabPage/*' file='Doc/LJCTabControl.xml'/>
    public void LJCMoveTabPage(int sourceIndex, int targetIndex)
    {
      var sourcePage = TabPages[sourceIndex];
      TabPages.RemoveAt(sourceIndex);
      TabPages.Insert(targetIndex, sourcePage);
    }

    // Moves the tab page left to the main tabs.
    /// <include path='items/LJCMoveTabPageLeft/*' file='Doc/LJCTabControl.xml'/>
    public void LJCMoveTabPageLeft(LJCTabControl targetTabs
      , SplitContainer split)
    {
      SelectedTab.Parent = targetTabs;
      if (0 == TabPages.Count)
      {
        split.Panel2Collapsed = true;
      }
    }

    // Moves the tab page right to the tile tabs.
    /// <include path='items/LJCMoveTabPageRight/*' file='Doc/LJCTabControl.xml'/>
    public void LJCMoveTabPageRight(LJCTabControl targetTabs
      , SplitContainer split)
    {
      if (TabPages.Count > 1)
      {
        split.Panel2Collapsed = false;
        SelectedTab.Parent = targetTabs;
      }
    }

    // Sets the current tab page.
    /// <include path='items/LJCSetCurrentTabPage/*' file='Doc/LJCTabControl.xml'/>
    public void LJCSetCurrentTabPage(MouseEventArgs e)
    {
      if (LJCGetTabPage(e) is TabPage tabPage)
      {
        SelectedTab = tabPage;
      }
    }
    #endregion

    #region Private Methods

    // Creates a bounding rectangle to determine if the move operation should start.
    private Rectangle CreateDragStartBounds(int x, int y, int width, int height)
    {
      Rectangle retVal;

      retVal = new Rectangle(x - (width / 2), y - (width / 2), width, height);
      return retVal;
    }

    // Refreshes the parent panel.
    private void RefreshParentPanel()
    {
      Panel panel;

      if (Parent is Panel)
      {
        panel = Parent as Panel;
        panel.Refresh();
      }
    }
    #endregion

    #region Setup Methods

    // Configures the control.
    private void InitializeControl()
    {
      // Initialize Class Data.
      mIsDragStart = false;

      // Initialize Property Values.
      LJCAllowDrag = false;
    }
    #endregion

    #region Properties

    /// <summary>Determines if the custom drag drop is allowed.</summary>
    [DefaultValue(false)]
    public bool LJCAllowDrag { get; set; }
    #endregion

    #region Member Data

    // Class Data.
    private TabPage mSourceTabPage;
    private Rectangle mDragStartBounds;
    private bool mIsDragStart;

    // Occurs during a dragdrop operation when a temporary target panel
    // should be displayed.
    /// <include path='items/LJCPanelAdd/*' file='Doc/LJCTabControl.xml'/>
    public event EventHandler<EventArgs> LJCPanelAdd;

    // Occurs during a dragdrop operation when a temporary target panel
    // should be hidden.
    /// <include path='items/LJCPanelRemove/*' file='Doc/LJCTabControl.xml'/>
    public event EventHandler<EventArgs> LJCPanelRemove;
    #endregion
  }
}
