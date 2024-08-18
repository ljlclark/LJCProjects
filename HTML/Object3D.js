// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// Object3D.js

// Represents a 3D ojbect.
class Object3D
{
  // The Constructor method.
  constructor(graphics)
  {
    this.Graphics = graphics;
    this.Paths = [];
  }

  // Adds a Path.
  AddPath(name, beginPoint, translatePoint, pathItems)
  {
    let path = new Path(name, beginPoint, translatePoint);
    for (let index = 0; index < pathItems.length; index++)
    {
      path.PathItems.push(pathItems[index]);
    }
  }

  // Creates a Polygon path.
  CreatePolygonPath(name, beginPoint, translatePoint, radius, verticeCount)
  {
    let retValue = new Path(name, beginPoint, translatePoint);
    retValue.DoClosePath = true;

    let arc = (Math.PI * 2) / verticeCount;
    let radians = arc;
    for (let index = 0; index < verticeCount - 1; index++)
    {
      let x = radius * Math.cos(radians);
      let y = radius * Math.sin(radians);
      let nextPoint = new Point3D(Math.round(x), Math.round(y), 0)
      let pathItem = new PathItem("line", nextPoint);
      retValue.PathItems.push(pathItem);
      radians += arc;
    }
    return retValue;
  }

  // Shows the object.
  Show()
  {
    for (let index = 0; index < this.Paths.length; index++)
    {
      let path = this.Paths[index];
      this.ShowPath(path);
    }
  }

  // Display the Path.
  ShowPath(path)
  {
    let g = this.Graphics;
    let pathItems = path.PathItems;

    g.BeginPath();
    let beginPoint = path.Translate(path.BeginPoint);
    g.MoveTo(beginPoint);
    for (let index = 0; index < pathItems.length; index++)
    {
      let pathItem = pathItems[index];
      switch (pathItem.ItemType.toLowerCase())
      {
        case "arc":
          break;

        case "line":
          let nextPoint = path.Translate(pathItem.NextPoint);
          g.NextLine(nextPoint, pathItem.StrokeStyle);
          break;

        case "rectangle":
          break;
      }
    }
    if (path.DoClosePath)
    {
      g.ClosePath();
    }
    g.Stroke();
  }
}

// Represents a 3D path.
class Path
{
  // The Constructor method.
  constructor(name, beginPoint, translatePoint)
  {
    this.BeginPoint = beginPoint;
    this.DoClosePath = false;
    this.Name = name;
    this.PathItems = [];
    this.TranslatePoint = translatePoint;
  }

  // 
  Translate(point)
  {
    let tlPoint = this.TranslatePoint;
    let retValue = new Point3D(point.X + tlPoint.X, point.Y + tlPoint.Y
      , point.Z + tlPoint.Z);
    return retValue;
  }
}

// Represents a 3D Path item.
class PathItem
{
  // The Constructor method.
  constructor(itemType, nextPoint)
  {
    // ItemType: Arc, Line, Rectangle
    this.ItemType = itemType;
    this.NextPoint = nextPoint;
    this.StrokeStyle = "";
    this.FillStyle = "";
  }
}

// Represents a 3D point.
class Point3D
{
  // The Constructor method.
  constructor(x, y, z)
  {
    this.X = x;
    this.Y = y;
    this.Z = z;
  }
}
