// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCHeaderBox5.cs
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace LJCControls5
{
  // A custom control for heading text. (R)
  /// <include file='Doc/LJCHeaderBox.xml'
  ///  path='items/LJCHeaderBox/*'/>
  public partial class LJCHeaderBox : Control
  {
    #region Constructors

    // Initializes an instance of the class.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCHeaderBox()
    {
      InitializeComponent();
      InitializeControl();
    }
    #endregion

    #region Override Event Methods

    // Provides custom Resize event code.
    /// <include file='Doc/LJCHeaderBox.xml'
    ///  path='items/OnResize/*'/>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      // Redraw the control.
      Invalidate();
    }

    // Provides custom Paint event code.
    /// <include file='Doc/LJCHeaderBox.xml'
    ///  path='items/OnPaint/*'/>
    protected override void OnPaint(PaintEventArgs pe)
    {
      LinearGradientBrush brush;
      Rectangle fillRectangle;
      PointF textPoint;
      float left;
      float top;

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
    }
    #endregion

    #region Setup Methods

    // Configures the control.
    private void InitializeControl()
    {
      // Initialize Class Data.

      // Initialize Property Values.
      LJCBeginColor = Color.AliceBlue;
      LJCEndColor = Color.LightSkyBlue;
    }
    #endregion

    #region Properties

    /// <summary>The background gradient fill begin color.</summary>
    [DefaultValue(typeof(Color), "Color.AliceBlue")]
    public Color LJCBeginColor { get; set; }

    /// <summary>The background gradient fill end color.</summary>
    [DefaultValue(typeof(Color), "Color.LightSkyBlue")]
    public Color LJCEndColor { get; set; }
    #endregion
  }
}
