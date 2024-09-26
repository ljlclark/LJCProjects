// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// LJCGraphics.js

// Provides graphics methods.
// ***************
class LJCGraphics
{
  // The Constructor method.
  constructor(canvas)
  {
    this.Canvas = canvas;
    this.Context = canvas.getContext("2d");
    this.Radian = Math.PI / 180;
    this.FillStyle = this.#GetDefaultStyle(this.Canvas, "");
    this.StrokeStyle = this.#GetDefaultStyle(this.Canvas, "");
  }

  // Draw Methods
  // ---------------
  // Arc(centerPoint, radius, endRadians, beginRadians = 0, strokeStyle = "")
  // Line(beginPoint, endPoint, strokeStyle = "")
  // NextLine(endPoint, strokeStyle = "")
  // Point(point)
  // Rectangle(beginPoint, width, height, fillStyle = "")
  // Text(text, beginPoint, font = "10px san-serif", fillStyle = "")

  // Draw an arc from the beginRadians to the endRdians.
  Arc(centerPoint, radius, endRadians, beginRadians = 0, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStrokeStyle(strokeStyle);

    ctx.beginPath();
    ctx.arc(centerPoint.X, centerPoint.Y, radius, beginRadians, endRadians);
    ctx.strokeStyle = strokeStyle;
  }

  // Draw a line from beginPoint to endPoint.
  Line(beginPoint, endPoint, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStrokeStyle(strokeStyle);

    ctx.moveTo(beginPoint.X, beginPoint.Y)
    ctx.lineTo(endPoint.X, endPoint.Y);
    ctx.strokeStyle = strokeStyle;
  }

  // Draw a line from the previous end point to the provided endPoint.
  NextLine(endPoint, strokeStyle = "")
  {
    let ctx = this.Context;
    strokeStyle = this.#GetStrokeStyle(strokeStyle);

    ctx.lineTo(endPoint.X, endPoint.Y);
    ctx.strokeStyle = strokeStyle;
  }

  // Draw a point.
  Point(point)
  {
    let point1 = new LJCPoint(point.X + 0.4
      , point.Y + 0.4);
    this.Line(point, point1);
  }

  // Draw a rectangle.
  Rectangle(beginPoint, width, height, fillStyle = "")
  {
    let ctx = this.Context;
    fillStyle = this.#GetFillStyle(fillStyle);

    ctx.beginPath();
    ctx.rect(beginPoint.X, beginPoint.Y, width, height);
    ctx.fillStyle = fillStyle;
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

  // Path Methods
  // ---------------
  // BeginPath()
  // ClosePath()
  // MoveTo(point)
  // Square(value)

  // Performs the Ctx.beginPath() method.
  BeginPath()
  {
    this.Context.beginPath();
  }

  // Add the remaining side of the path.
  ClosePath()
  {
    this.Context.closePath();
  }

  // Move start point.
  MoveTo(point)
  {
    this.Context.moveTo(point.X, point.Y);
  }

  // Squares a value.
  Square(value)
  {
    let retValue = Math.pow(value, 2);
    return retValue;
  }

  // Get Radius and Rotation Methods
  // ---------------
  // CrossProduct(point1, point2)
  // GetPointRadius(point)
  // GetRadius(adjacent, opposite)
  // GetRotation(adjacent, opposite)

  // Get the cross product of two vectors.
  CrossProduct(point1, point2)
  {
    let a = point1;
    let b = point2;
    let retResult = new LJCPoint();

    retResult.X = a.Y * b.Z - a.Z * b.Y;
    retResult.Y = a.Z * b.X - a.X * b.Z;
    retResult.Z = a.X * b.Y - a.Y * b.X;
    return retResult;
  }

  // Get the radius with a point.
  GetPointRadius(point)
  {
    let retRadius = 0.0;

    let sides = this.Square(point.X);
    sides += this.Square(point.Y);
    sides += this.Square(point.Z);
    retRadius = Math.sqrt(sides);
    return retRadius;
  }

  // Get the radius with sides.
  GetRadius(adjacent, opposite)
  {
    let retRadius = 0.0;

    let sides = this.Square(adjacent);
    sides += this.Square(opposite);
    retRadius = Math.sqrt(sides);
    return retRadius;
  }

  // Get the radians of an angle with sides.
  GetRotation(adjacent, opposite)
  {
    let retRotation = 0.0;
    let radian = gLJCGraphics.Radian;

    retRotation = Math.atan2(opposite, adjacent);
    if (retRotation < 0)
    {
      retRotation = Math.abs(retRotation);
    }

    // Not Quandrant I or II.
    if (opposite < 0)
    {
      retRotation = Math.atan2(Math.abs(opposite)
        , Math.abs(adjacent));
    }

    // Quadrant III
    if (adjacent <= 0
      && opposite < 0)
    {
      retRotation += 180 * radian;
    }

    // Quadrant IV
    if (adjacent > 0
      && opposite < 0)
    {
      retRotation = Math.PI * 2 - retRotation;
    }
    return retRotation;
  }

  // Fill and Stroke Methods
  // ---------------
  // Fill(fillStyle = "")
  // Stroke(strokeStyle = "")

  // Show the fill path.
  Fill(fillStyle = "")
  {
    fillStyle = this.#GetFillStyle(fillStyle);
    this.Context.fill();
  }

  // Show the line path.
  Stroke(strokeStyle = "")
  {
    strokeStyle = this.#GetStrokeStyle(strokeStyle);
    this.Context.stroke();
  }

  // Gets the default style color.
  #GetDefaultStyle(eItem, style)
  {
    let retStyle = style;

    if (!LJC.HasValue(style))
    {
      retStyle = "black";
      let backColor = LJC.ElementStyle(eItem, "background-color");
      if ("rgba(0, 0, 0, 0)" == backColor)
      {
        retStyle = "white";
      }
    }
    return retStyle;
  }

  // Get provided style or class Style.
  #GetFillStyle(fillStyle)
  {
    let retStyle = fillStyle;

    if (!LJC.HasValue(fillStyle))
    {
      retStyle = this.FillStyle;
    }
    return retStyle;
  }

  // Get provided style or class Style.
  #GetStrokeStyle(strokeStyle)
  {
    let retStyle = strokeStyle;

    if (!LJC.HasValue(strokeStyle))
    {
      retStyle = this.StrokeStyle;
    }
    return retStyle;
  }
}
