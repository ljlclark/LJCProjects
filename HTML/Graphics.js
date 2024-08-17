class Graphics
{
  // The Constructor method.
  constructor(canvas)
  {
    this.Canvas = canvas;
    this.Context = canvas.getContext("2d");
    this.Radian = Math.PI / 180;
    this.StrokeStyle = this.#GetDefaultStyle(this.Canvas, "");
    this.FillStyle = this.#GetDefaultStyle(this.Canvas, "");
    this.X = 0;
    this.PrevX = 0;
  }

  // Draw Methods
  // ***************

  // Draw an arc from the beginRadians to the endRdians.
  Arc(centerPoint, radius, endRadians, beginRadians = 0, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStrokeStyle(strokeStyle);

    ctx.beginPath();
    ctx.arc(centerPoint.X, centerPoint.Y, radius, beginRadians, endRadians);

    ctx.strokeStyle = strokeStyle;
    ctx.stroke();
  }

  // Draw a line from beginPoint to endPoint.
  Line(beginPoint, endPoint, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStrokeStyle(strokeStyle);

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
    strokeStyle = this.#GetStrokeStyle(strokeStyle);

    ctx.lineTo(endPoint.X, endPoint.Y);

    ctx.strokeStyle = strokeStyle;
    ctx.stroke();
  }

  // Draw a polygon.
  Polygon(centerPoint, radius, verticeCount, strokeStyle = "", fillStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStrokeStyle(strokeStyle);
    fillStyle = this.#GetFillStyle(fillStyle);

    let arc = (Math.PI * 2) / verticeCount;
    let radians = arc;
    let beginPoint = new GPoint(centerPoint.X + radius, centerPoint.Y);
    ctx.beginPath();
    ctx.moveTo(beginPoint.X, beginPoint.Y); // Add
    for (let index = 0; index < verticeCount - 1; index++)
    {
      let x = radius * Math.cos(radians) + centerPoint.X;
      let y = radius * Math.sin(radians) + centerPoint.Y;
      let sx = Math.round(x);
      let sy = Math.round(y);
      let endPoint = new GPoint(sx, sy);
      this.NextLine(endPoint, strokeStyle);
      radians += arc;
    }
    ctx.closePath();

    ctx.strokeStyle = strokeStyle;
    ctx.stroke();
    if (LJC.HasValue(fillStyle))
    {
      ctx.fillStyle = fillStyle;
      ctx.fill();
    }
  }

  // Draw a rectangle.
  Rectangle(beginPoint, width, height, fillStyle = "")
  {
    let ctx = this.Context;
    fillStyle = this.#GetFillStyle(fillStyle);

    ctx.beginPath();
    ctx.rect(beginPoint.X, beginPoint.Y, width, height);
    ctx.stroke();
    ctx.fillStyle = fillStyle;
    ctx.fill();
  }

  // Draw text.
  Text(text, beginPoint, font = "10px san-serif", fillStyle = "")
  {
    let ctx = this.Context;
    fillStyle = this.#GetFillStyle(fillStyle);

    ctx.font = font;
    ctx.fillStyle = fillStyle;
    ctx.fillText(text, beginPoint.X, beginPoint.Y);
  }

  // Other Methods
  // ***************

  // 
  Animate()
  {
    let ctx = this.Context;

    let y = 100;
    let width = 50;
    let height = 50;

    ctx.clearRect(this.PrevX - 1, y - 1, width + 1, height + 2);
    ctx.strokeStyle = this.strokeStyle;

    ctx.strokeRect(this.X, y, width, height);
    ctx.fillStyle = 'red';
    ctx.fillRect(this.X, y, width, height);

    this.PrevX = this.X;
    this.X += 2;
    if (this.X < this.Canvas.width - 50)
    {
      requestAnimationFrame(this.Animate.bind(this));
    }
  }

  // Gets the default style color.
  #GetDefaultStyle(eItem, style)
  {
    let retValue = style;

    if (!LJC.HasValue(style))
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
  #GetFillStyle(fillStyle)
  {
    let retValue = fillStyle;

    if (!LJC.HasValue(fillStyle))
    {
      retValue = this.FillStyle;
    }
    return retValue;
  }

  // Get provided style or class Style.
  #GetStrokeStyle(strokeStyle)
  {
    let retValue = strokeStyle;

    if (!LJC.HasValue(strokeStyle))
    {
      retValue = this.StrokeStyle;
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