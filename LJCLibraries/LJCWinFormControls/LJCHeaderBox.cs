// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCHeaderBox.cs
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LJCWinFormControls
{
  // A custom control for heading text. (R)
  /// <include path='items/LJCHeaderBox/*' file='Doc/LJCHeaderBox.xml'/>
  public partial class LJCHeaderBox : Control
  {
    #region Constructors

    // Initializes an instance of the class.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LJCHeaderBox()
    {
      InitializeComponent();
      InitializeControl();
    }
    #endregion

    #region Override Event Methods

    // Provides custom MouseClick event code.
    /// <include path='items/OnMouseClick/*' file='Doc/LJCHeaderBox.xml'/>
    protected override void OnMouseClick(MouseEventArgs e)
    {
      base.OnMouseClick(e);

      // Invokes the ClosePanel event if the Close button is clicked.
      if (LJCShowCloseButton
        && mCloseRectangle.Contains(new Point(e.X, e.Y)))
      {
        LJCOnClosePanel();
      }
    }

    // Provides custom MouseMove event code.
    /// <include path='items/OnMouseMove/*' file='Doc/LJCHeaderBox.xml'/>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);

      if (LJCShowCloseButton
        && mCloseRectangle.Contains(new Point(e.X, e.Y)))
      {
        // Draws the close button mouseover image.
        if (mMouseOver == false)
        {
          mMouseOver = true;
          Invalidate();
        }
      }
      else
      {
        // Draws the close button standard image.
        if (mMouseOver)
        {
          mMouseOver = false;
          Invalidate();
        }
      }
    }

    // Provides custom MouseLeave event code.
    /// <include path='items/OnMouseLeave/*' file='Doc/LJCHeaderBox.xml'/>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);

      // Redraw to show the standard close button image.
      mMouseOver = false;
      Invalidate();
    }

    // Provides custom Resize event code.
    /// <include path='items/OnResize/*' file='Doc/LJCHeaderBox.xml'/>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      // Redraw the control.
      Invalidate();
    }

    // Provides custom Paint event code.
    /// <include path='items/OnPaint/*' file='Doc/LJCHeaderBox.xml'/>
    protected override void OnPaint(PaintEventArgs pe)
    {
      LinearGradientBrush brush;
      Rectangle fillRectangle;
      PointF textPoint;
      Bitmap closeBoxImage;
      float left;
      float top;
      int closeWidth = 13;
      int closeHeight = 12;
      int closeRightBorder = 4;
      int closeLeft;
      int closeTop;

      base.OnPaint(pe);

      // Draw the border.
      pe.Graphics.DrawRectangle(Pens.LightSlateGray, ClientRectangle.X, ClientRectangle.Y
        , ClientRectangle.Width - 1, ClientRectangle.Height - 1);

      // Fill with gradient.
      fillRectangle = new Rectangle(ClientRectangle.X + 1, ClientRectangle.Y + 1
        , ClientRectangle.Width - 2, ClientRectangle.Height - 2);
      brush = new LinearGradientBrush(fillRectangle, LJCBeginColor, LJCEndColor
        , LinearGradientMode.Vertical);
      pe.Graphics.FillRectangle(brush, fillRectangle);
      brush.Dispose();

      // Draw the text.
      left = 5;
      top = (float)((ClientRectangle.Height / 2.0) - (Font.Height / 2.0));
      textPoint = new PointF(left, top);
      pe.Graphics.DrawString(Text, Font, Brushes.Black, textPoint);

      // Create close rectangle.
      closeLeft = Width - closeWidth - closeRightBorder;
      closeTop = (Height / 2) - (closeHeight / 2) + 1;
      mCloseRectangle = new Rectangle(closeLeft, closeTop, closeWidth, closeHeight);

      if (LJCShowCloseButton)
      {
        // Draw close box bitmap.
        if (mMouseOver)
        {
          closeBoxImage = CloseImageList.Images[1] as Bitmap;
        }
        else
        {
          closeBoxImage = CloseImageList.Images[0] as Bitmap;
        }
        closeBoxImage.MakeTransparent(Color.Magenta);
        pe.Graphics.DrawImage(closeBoxImage, mCloseRectangle.X, mCloseRectangle.Y);
      }
    }
    #endregion

    #region Custom Event Methods

    // Calls the LJCClosePanel event handlers.
    /// <include path='items/OnClosePanel/*' file='Doc/LJCHeaderBox.xml'/>
    protected void LJCOnClosePanel()
    {
      LJCClosePanel?.Invoke(this, new EventArgs());
    }
    #endregion

    #region Setup Methods

    // Configures the control.
    private void InitializeControl()
    {
      // Initialize Class Data.
      mMouseOver = false;

      // Initialize Property Values.
      LJCBeginColor = Color.AliceBlue;
      LJCEndColor = Color.LightSkyBlue;
      LJCShowCloseButton = false;

      // Configure Controls.
      CloseImageList.ImageSize = new Size(13, 12);
      CloseImageList.TransparentColor = Color.Magenta;
    }
    #endregion

    #region Properties

    /// <summary>The background gradient fill begin color.</summary>
    [DefaultValue(typeof(Color), "Color.AliceBlue")]
    public Color LJCBeginColor { get; set; }

    /// <summary>The background gradient fill end color.</summary>
    [DefaultValue(typeof(Color), "Color.LightSkyBlue")]
    public Color LJCEndColor { get; set; }

    /// <summary>Determines if the Close box is displayed or not.</summary>
    [DefaultValue(false)]
    public bool LJCShowCloseButton { get; set; }
    #endregion

    #region Member Data

    // Property values.

    // Class Data.
    private Rectangle mCloseRectangle;
    private bool mMouseOver;

    /// <summary>The ClosePanel notification event.</summary>
    public event EventHandler<EventArgs> LJCClosePanel;
    #endregion
  }
}
