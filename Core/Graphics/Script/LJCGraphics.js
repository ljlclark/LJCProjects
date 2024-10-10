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
  Arc(centerPoint, radius, endRadians = Math.PI * 2
    , beginRadians = 0, fillStyle = "", strokeStyle = "")
  {
    let ctx = this.Context;

    if (null == endRadians)
    {
      endRadians = Math.PI * 2;
    }
    if (null == beginRadians)
    {
      beginRadians = 0;
    }

    ctx.arc(centerPoint.X, centerPoint.Y, radius
      , beginRadians, endRadians);
    if (LJC.HasValue(fillStyle))
    {
      this.Fill(fillStyle);
    }
    if (LJC.HasValue(strokeStyle))
    {
      this.Stroke(strokeStyle);
    }
  }

  // Draw a line from beginPoint to endPoint.
  Line(beginPoint, endPoint, strokeStyle = "")
  {
    let ctx = this.Context;

    ctx.moveTo(beginPoint.X, beginPoint.Y)
    ctx.lineTo(endPoint.X, endPoint.Y);
    if (LJC.HasValue(strokeStyle))
    {
      this.Stroke(strokeStyle);
    }
  }

  // Draw a line from the previous end point to the provided endPoint.
  NextLine(endPoint, strokeStyle = "")
  {
    let ctx = this.Context;

    ctx.lineTo(endPoint.X, endPoint.Y);
    if (LJC.HasValue(strokeStyle))
    {
      this.Stroke(strokeStyle);
    }
  }

  // Draw a point.
  Point(point, strokeStyle = "")
  {
    let point1 = new LJCPoint(point.X + 0.4
      , point.Y + 0.4);
    this.Line(point, point1, strokeStyle);
  }

  // Draw a rectangle.
  Rectangle(beginPoint, width, height, fillStyle = ""
    , strokeStyle = "")
  {
    let ctx = this.Context;

    ctx.rect(beginPoint.X, beginPoint.Y, width, height);
    if (LJC.HasValue(fillStyle))
    {
      ctx.fillStyle = fillStyle;
      ctx.fillRect(beginPoint.X, beginPoint.Y, width
        , height)
    }
    if (LJC.HasValue(strokeStyle))
    {
      this.Stroke(strokeStyle);
    }
  }

  // Draw text.
  Text(text, beginPoint, font = "12px san-serif"
    , fillStyle = "", strokeStyle = "")
  {
    let ctx = this.Context;

    if (null == font)
    {
      font = "12px san-serif";
    }
    ctx.font = font;
    fillStyle = this.#GetFillStyle(fillStyle);

    if (LJC.HasValue(strokeStyle))
    {
      this.Stroke(strokeStyle);
    }
    ctx.fillStyle = fillStyle;
    ctx.fillText(text, beginPoint.X, beginPoint.Y);
  }

  // Path Methods
  // ---------------
  // BeginPath()
  // ClosePath()
  // MoveTo(point)

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

  // Get Radius and Rotation Methods
  // ---------------
  // CrossProduct(point1, point2)
  // PointRadius(point)
  // PointRotation()
  // Radius(adjacent, opposite)
  // Rotation(adjacent, opposite)
  // Square(value)
  // ToAngle(rotation)

  // Get the cross product of two vectors.
  CrossProduct(point1, point2)
  {
    let a = point1;
    let b = point2;
    let retResult = new LJCPoint();

    retResult.X = a.Y * b.Z - a.Z * b.Y;
    // switch first and second multiplication to
    // equal negative.
    retResult.Y = a.Z * b.X - a.X * b.Z;
    retResult.Z = a.X * b.Y - a.Y * b.X;
    return retResult;
  }

  // Gets the point radius.
  PointRadius(point)
  {
    let retRadius = 0.0;

    let sides = this.Square(point.X);
    sides += this.Square(point.Y);
    sides += this.Square(point.Z);
    retRadius = Math.sqrt(sides);
    return retRadius;
  }

  // Gets the point rotation in radians.
  PointRotation(point)
  {
    let retRotation = 0.0;

    let zOpposite = this.Radius(point.Z, point.Y);
    retRotation = this.Rotation(point.X, zOpposite);
    return retRotation;
  }

  // Gets the radius with sides.
  Radius(adjacent, opposite)
  {
    let retRadius = 0.0;

    let sides = this.Square(adjacent);
    sides += this.Square(opposite);
    retRadius = Math.sqrt(sides);
    return retRadius;
  }


  // Get the rotation in radians with sides.
  Rotation(adjacent, opposite)
  {
    let radian = gLJCGraphics.Radian;
    let retRotation = 0.0;

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

  // Squares a value.
  Square(value)
  {
    let retValue = Math.pow(value, 2);
    return retValue;
  }

  // Get rotation as angle in degrees.
  ToAngle(rotation)
  {
    let retAngle = rotation / this.Radian;
    return retAngle;
  }

  // Fill and Stroke Methods
  // ---------------
  // Fill(fillStyle = "")
  // Stroke(strokeStyle = "")

  // Show the fill path.
  Fill(fillStyle = "")
  {
    let ctx = this.Context;

    ctx.fillStyle = this.#GetFillStyle(fillStyle);
    ctx.fill();
  }

  // Show the line path.
  Stroke(strokeStyle = "")
  {
    let ctx = this.Context;

    ctx.strokeStyle = this.#GetStrokeStyle(strokeStyle);
    ctx.stroke();
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
