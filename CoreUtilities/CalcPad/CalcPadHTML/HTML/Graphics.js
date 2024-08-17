class Graphics
{
  // The Constructor method.
  constructor(canvas)
  {
    this.Canvas = canvas;
    this.Context = canvas.getContext("2d");
    this.Radian = Math.PI / 180;
    this.DefaultStyle = this.#GetDefaultStyle(this.Canvas, "");
  }

  // Draw Methods
  // ***************

  // Draw an arc from the beginRadians to the endRdians.
  Arc(centerPoint, radius, endRadians, beginRadians = 0, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStyle(strokeStyle);
    ctx.beginPath();
    ctx.arc(centerPoint.X, centerPoint.Y, radius, beginRadians, endRadians);
    ctx.strokeStyle = strokeStyle;
    ctx.stroke();
  }

  // Draw a line from beginPoint to endPoint.
  Line(beginPoint, endPoint, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStyle(strokeStyle);
    ctx.moveTo(beginPoint.X, beginPoint.Y)
    ctx.lineTo(endPoint.X, endPoint.Y);
    ctx.strokeStyle = strokeStyle;
    ctx.stroke();
  }

  // Move start point.
  MoveTo(point)
  {
    this.Context.moveTo(point.X, point.Y);
  }

  // Draw a line from the previous end point to the provided endPoint.
  NextLine(endPoint, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStyle(strokeStyle);
    ctx.lineTo(endPoint.X, endPoint.Y);
    ctx.strokeStyle = strokeStyle;
    ctx.stroke();
  }

  // Draw a polygon.
  Polygon(centerPoint, radius, verticeCount)
  {
    let arc = (Math.PI * 2) / verticeCount;
    let radians = arc;
    let prevPoint = new GPoint(centerPoint.X + radius, centerPoint.Y);
    for (let index = 0; index < verticeCount; index++)
    {
      let x = radius * Math.cos(radians) + centerPoint.X;
      let y = radius * Math.sin(radians) + centerPoint.Y;
      let sx = Math.round(x);
      let sy = Math.round(y);
      let endPoint = new GPoint(sx, sy);
      this.Line(prevPoint, endPoint);
      prevPoint = endPoint;
      radians += arc;
    }
  }

  // Draw text.
  Text(text, beginPoint, font = "10px san-serif", fillStyle = "")
  {
    let ctx = this.Context;
    fillStyle = this.#GetStyle(fillStyle);
    ctx.font = font;
    ctx.fillStyle = fillStyle;
    ctx.fillText(text, beginPoint.X, beginPoint.Y);
  }

  // Other Methods
  // ***************

  // Gets the default style color.
  #GetDefaultStyle(eItem, strokeStyle)
  {
    let retValue = strokeStyle;
    if (!LJC.HasValue(strokeStyle))
    {
      retValue = "black";
      let backColor = LJC.ElementStyle(eItem, "background-color");
      if ("rgba(0, 0, 0, 0)" == backColor)
      {
        retValue = "white";
      }
    }
    return retValue;
  }

  // Get provided style or class Style.
  #GetStyle(style)
  {
    let retValue = style;
    if (!LJC.HasValue(style))
    {
      retValue = this.DefaultStyle;
    }
    return retValue;
  }
}

class GPoint
{
  // The Constructor method.
  constructor(x, y)
  {
    this.X = x;
    this.Y = y;
  }
}