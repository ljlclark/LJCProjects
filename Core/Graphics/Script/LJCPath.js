// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// LJCPath.js

// Represents a 3D path.
// ***************
class LJCPath
{
  #ScreenBeginPoint;

  // The Constructor method.
  constructor(name, beginPoint)
  {
    this.Name = name;
    this.Arc = 0;
    this.BeginPoint = beginPoint;
    this.DoClose = "true";
    this.FillStyle = "";
    this.Normal = new LJCPoint();
    this.PathPoints = [];
    this.PathRadius = new LJCPoint();
    this.#ScreenBeginPoint = beginPoint;
    this.ScreenNormal = new LJCPoint();
    this.StrokeStyle = "";
    this.Translate();
  }

  // Data Methods
  // ---------------
  // Clone();

  // Creates a Clone of this object.
  Clone()
  {
    let beginPoint = this.BeginPoint.Clone();
    let retPath = new LJCPath(this.Name, beginPoint);
    retPath.Arc = this.Arc;
    retPath.DoClose = this.DoClose;
    retPath.FillStyle = this.FillStyle;
    retPath.Normal = this.Normal.Clone();
    for (let pathPoint of this.PathPoints)
    {
      retPath.PathPoints.push(pathPoint.Clone());
    }
    retPath.PathRadius = this.PathRadius;
    retPath.ScreenNormal = this.ScreenNormal;
    retPath.StrokeStyle = this.StrokeStyle;
    return retPath;
  }

  // Class Methods
  // ---------------
  // AddRotateXY(addRadians);
  // AddRotateXZ(addRadians);
  // AddRotateZY(addRadians);
  // Move(x, y, z)
  // RotateXY(radians);
  // RotateXZ(radians);
  // RotateZY(radians);
  // Show()
  // Translate()

  // Add rotation on the XY plane.
  AddRotateXY(addRadians)
  {
    this.BeginPoint.AddRotateXY(addRadians);
    this.Normal.AddRotateXY(addRadians);
    for (let pathPoint of this.PathPoints)
    {
      pathPoint.AddRotateXY(addRadians);
    }
    this.Translate();
  }

  // Add rotation on the XZ plane.
  AddRotateXZ(addRadians)
  {
    this.BeginPoint.AddRotateXZ(addRadians);
    this.Normal.AddRotateXZ(addRadians);
    for (let pathPoint of this.PathPoints)
    {
      pathPoint.AddRotateXZ(addRadians);
    }
    this.Translate();
  }

  // Add rotation on the ZY plane.
  AddRotateZY(addRadians)
  {
    this.BeginPoint.AddRotateZY(addRadians);
    this.Normal.AddRotateZY(addRadians);
    for (let pathPoint of this.PathPoints)
    {
      pathPoint.AddRotateZY(addRadians);
    }
    this.Translate();
  }

  // Moves the path.
  Move(x, y, z)
  {
    this.BeginPoint.Move(x, y, z);
    this.Normal.Move(x, y, z);
    for (let pathPoint of this.PathPoints)
    {
      pathPoint.Move(x, y, z);
    }
    this.Translate();
  }

  // Rotation from beginning on the XY plane.
  RotateXY(radians)
  {
    this.BeginPoint.RotateXY(radians);
    this.Normal.RotateXY(rotation);
    for (let pathPoint of this.PathPoints)
    {
      pathPoint.RotateXY(radians);
    }
    this.Translate();
  }

  // Rotation from beginning on the XZ plane.
  RotateXZ(radians)
  {
    this.BeginPoint.RotateXZ(radians);
    this.Normal.RotateXZ(radians);
    for (let pathPoint of this.PathPoints)
    {
      pathPoint.RotateXZ(radians);
    }
    this.Translate();
  }

  // Rotation from beginning on the ZY plane.
  RotateZY(radians)
  {
    this.BeginPoint.RotateZY(radians);
    this.Normal.RotateZY(radians);
    for (let pathPoint of this.PathPoints)
    {
      pathPoint.RotateZY(radians);
    }
    this.Translate();
  }

  // Display the Path.
  Show()
  {
    let g = gLJCGraphics;

    if (this.ScreenNormal.Z > 5)
    {
      g.BeginPath();
      let beginPoint = this.#ScreenBeginPoint;
      g.MoveTo(beginPoint);
      for (let pathPoint of this.PathPoints)
      {
        switch (pathPoint.ItemType.toLowerCase())
        {
          case "arc":
            break;

          case "line":
            let screenPoint = pathPoint.getScreenPoint();
            g.NextLine(screenPoint);
            break;

          case "rectangle":
            break;
        }
      }

      g.Context.strokeStyle = this.StrokeStyle;
      if (this.DoClose)
      {
        g.ClosePath();
      }
      else
      {
        g.Stroke();
      }
      if (LJC.HasValue(this.FillStyle))
      {
        g.Context.fillStyle = this.FillStyle;
        g.Context.fill();
      }
    }
  }

  // Sets the screen points.
  Translate()
  {
    if (gScene.TranslatePoint != null)
    {
      this.#ScreenBeginPoint = this.BeginPoint.Clone();
      this.#ScreenBeginPoint.Translate();
      this.ScreenNormal = this.Normal.Clone();
      this.ScreenNormal.Translate();
      for (let pathPoint of this.PathPoints)
      {
        pathPoint.Translate();
      }
    }
  }

  // Getters and Setters
  // ---------------
  // getScreenBeginPoint()

  // Gets the ScreenBeginPoint value.
  getScreenBeginPoint()
  {
    let retValue = this.#ScreenBeginPoint;
    return retValue;
  }
}
