// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// LJCPathPoint.js

// Represents a 3D Path point.
// ***************
class LJCPathPoint
{
  #Point;
  #ScreenPoint;

  // The Constructor method.
  constructor(itemType, nextPoint)
  {
    this.FillStyle = "";
    // ItemType: Arc, Line, Rectangle
    this.ItemType = itemType;
    this.#Point = nextPoint;
    this.#ScreenPoint = nextPoint;
    this.StrokeStyle = "";
    this.Translate();
  }

  // Data Methods
  // ---------------
  // Clone()

  // Creates a Clone of this object.
  Clone()
  {
    let point = this.#Point.Clone();
    let retPathPoint = new LJCPathPoint(this.ItemType
      , point);
    retPathPoint.FillStyle = this.FillStyle;
    retPathPoint.SrokeStyle = this.StrokeStyle;
    return retPathPoint;
  }

  // Class Methods
  // ---------------
  // AddRotateXY(addRadians)
  // AddRotateXZ(addRadians)
  // AddRotateZY(addRadians)
  // Move(x, y, z)
  // RotateXY(radians)
  // RotateXZ(radians)
  // RotateZY(radians)
  // Translate()

  // Add rotation on the XY plane.
  AddRotateXY(addRadians)
  {
    this.#Point.AddRotateXY(addRadians);
    this.Translate();
  }

  // Add rotation on the XZ plane.
  AddRotateXZ(addRadians)
  {
    this.#Point.AddRotateXZ(addRadians);
    this.Translate();
  }

  // Add rotation on the ZY plane.
  AddRotateZY(addRadians)
  {
    this.#Point.AddRotateZY(addRadians);
    this.Translate();
  }

  // Moves the next point.
  Move(x, y, z)
  {
    this.#Point.Move(x, y, z);
    this.Translate();
  }

  // Rotate from beginning on the XY plane.
  RotateXY(radians)
  {
    this.#Point.RotateXY(radians);
    this.Translate();
  }

  // Rotate from beginning on the XZ plane.
  RotateXZ(radians)
  {
    this.#Point.RotateXZ(radians);
    this.Translate();
  }

  // Rotate from beginning on the ZY plane.
  RotateZY(radians)
  {
    this.#Point.RotateZY(radians);
    this.Translate();
  }

  // Sets the screen points.
  Translate()
  {
    this.#ScreenPoint = this.#Point.Clone();
    this.#ScreenPoint.Translate();
  }

  // Getters and Setters
  // ---------------
  // getPoint()
  // getScreenPoint()

  // Gets the Point value.
  getPoint()
  {
    let retValue = this.#Point;
    return retValue;
  }

  // Gets the ScreenPoint value.
  getScreenPoint()
  {
    let retValue = this.#ScreenPoint;
    return retValue;
  }
}
