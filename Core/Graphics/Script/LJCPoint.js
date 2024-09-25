// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// LJCPoint.js

// Represents a 3D point.
// ***************
class LJCPoint
{
  // The Constructor method.
  constructor(x = 0, y = 0, z = 0, radius = 0)
  {
    this.X = x;
    this.Y = y;
    this.Z = z;
    this.Radius = radius;
    this.ViewZ = 0;
  }

  // Data Methods
  // ---------------
  // Clone()

  // Creates a Clone of this object.
  Clone()
  {
    let retPoint = new LJCPoint(this.X, this.Y
      , this.Z, this.Radius);
    retPoint.ViewZ = this.ViewZ;
    return retPoint;
  }

  // Class Methods
  // ---------------
  // AddRotateXY(addRadians)
  // AddRotateXZ(addRadians)
  // AddRotateZY(addRadians)
  // Move(x, y, z)
  // RotateXY(rotation)
  // RotateXZ(rotation)
  // RotateZY(rotation)
  // Translate()

  // Add rotation on the XY plane.
  AddRotateXY(addRadians)
  {
    let g = gLJCGraphics;

    let rotation = g.GetRotation(this.X
      , this.Y);
    rotation += addRadians;
    this.RotateXY(rotation);
  }

  // Add rotation on the XZ plane.
  AddRotateXZ(addRadians)
  {
    let g = gLJCGraphics;

    let rotation = g.GetRotation(this.X
      , this.Z);
    rotation += addRadians;
    this.RotateXZ(rotation);
  }

  // Add rotation on the ZY plane.
  AddRotateZY(addRadians)
  {
    let g = gLJCGraphics;

    let rotation = g.GetRotation(this.Z
      , this.Y);
    rotation += addRadians;
    this.RotateZY(rotation);
  }

  // Moves the point.
  Move(x, y, z)
  {
    this.X += x;
    this.Y += y;
    this.Z += z;
  }

  // Rotate from beginning on the XY plane.
  RotateXY(rotation)
  {
    let calcPoint = this.#CalcPoint(rotation
      , this.X, this.Y);
    this.X = calcPoint.X;
    this.Y = calcPoint.Y;
  }

  // Rotate from beginning on the XZ plane.
  RotateXZ(rotation)
  {
    let calcPoint = this.#CalcPoint(rotation
      , this.X, this.Z);
    this.X = calcPoint.X;
    this.Z = calcPoint.Y;
  }

  // Rotate from beginning on the ZY plane.
  RotateZY(rotation)
  {
    let calcPoint = this.#CalcPoint(rotation
      , this.Z, this.Y);
    this.Z = calcPoint.X;
    this.Y = calcPoint.Y;
  }

  // 
  Translate()
  {
    let tPoint = gScene.TranslatePoint;

    if (tPoint != null)
    {
      // Perspective
      // Adjusted in relationship to view.
      // a = point adjacent length
      // b = point opposite length
      // c = view adjacent length (viewZ)
      // d = calculated view opposite length

      // Simplify equation
      // a/b = c/d (multiply by d)
      // ad / b = c (multiple by b)
      // ad = bc (divide by a)  // cross multiply equation
      // d = bc / a

      // Example
      // c = viewZ = tPoint.Z = 90;
      // a = adjacent = Z = (-20)
      // a = 90 - (-20) = 110
      // b = opposite = 21
      // a/b = 110 / 21 = 5.23809
      // d = bc / a
      // d = 21 * 90 / 110 = 17.181818
      // c/d = 5.23809

      // Perspective
      let viewZ = tPoint.ViewZ;
      // Z negative toward viewer
      //let a = viewZ + this.Z;
      // Z positive toward viewer
      let a = viewZ - this.Z;
      let sx = this.X * viewZ / a;
      let sy = this.Y * viewZ / a;

      // Translate (move) to screen.
      sx = sx + tPoint.X;
      sy = sy + tPoint.Y;
      this.X = Math.round(sx);
      this.Y = Math.round(sy);
    }
  }

  // Calculate rotated point.
  #CalcPoint(rotation, adjacent, opposite)
  {
    let g = gLJCGraphics;
    let retPoint = new LJCPoint();

    // cos(radians) = a/h
    // Multiply both sides by h.
    // a = h * cos(radians)
    let radius = g.GetRadius(adjacent, opposite);
    retPoint.X = radius * Math.cos(rotation);
    retPoint.Y = radius * Math.sin(rotation);
    retPoint.X = this.#GetValue(retPoint.X);
    retPoint.Y = this.#GetValue(retPoint.Y);
    return retPoint;
  }

  // Gets small values as zero.
  #GetValue(value)
  {
    let retValue = value;

    if (value > 0
      && value < 0.00001)
    {
      retValue = 0;
    }
    if (value < 0
      && value > -0.000001)
    {
      retValue = -0;
    }
    return retValue;
  }
}
