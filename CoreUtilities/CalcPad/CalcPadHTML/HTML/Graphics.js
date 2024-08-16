class Graphics
{
  // The Constructor method.
  constructor(canvas)
  {
    this.Canvas = canvas;
    this.Context = this.Canvas.getContext("2d");
    this.Radian = Math.PI / 180;
  }

  // Draw an arc from the beginRadians to the endRdians.
  Arc(centerPoint, radius, endRadians, beginRadians = 0, strokeStyle = "white")
  {
    let c = this.Context;
    c.beginPath();
    c.arc(centerPoint.X, centerPoint.Y, radius, beginRadians, endRadians);
    c.strokeStyle = strokeStyle;
    c.stroke();
  }

  // Draw a line from beginPoint to endPoint.
  Line(beginPoint, endPoint, strokeStyle = "white")
  {
    let c = this.Context;
    c.moveTo(beginPoint.X, beginPoint.Y)
    c.lineTo(endPoint.X, endPoint.Y);
    c.strokeStyle = strokeStyle;
    c.stroke();
  }

  // Move start point.
  MoveTo(point)
  {
    this.Context.moveTo(point.X, point.Y);
  }

  // Draw a line from the previous endPoint to the provided endPoint.
  NextLine(endPoint, strokeStyle = "white")
  {
    let c = this.Context;
    c.lineTo(endPoint.X, endPoint.Y);
    c.strokeStyle = strokeStyle;
    c.stroke();
  }

  // Draw a polygon.
  Polygon(centerPoint, radius, verticeCount)
  {
    let beginPoint = new GPoint(centerPoint.X + radius, centerPoint.Y);
    this.MoveTo(beginPoint);
    let arc = (2 * Math.PI) / verticeCount;
    let radians = arc;
    for (let index = 0; index < verticeCount - 1; index++)
    {
      let x = radius * Math.cos(radians) + centerPoint.X;
      let y = radius * Math.sin(radians) + centerPoint.Y;
      let sx = Math.round(x);
      let sy = Math.round(y);
      let endPoint = new GPoint(sx, sy);
      this.NextLine(endPoint);
      radians += arc;
    }
    this.NextLine(beginPoint);
  }

  // Draw text.
  Text(text, beginPoint, font = "10px san-serif", fillStyle = "white")
  {
    let c = this.Context;
    c.font = font;
    c.fillStyle = fillStyle;
    c.fillText(text, beginPoint.X, beginPoint.Y);
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