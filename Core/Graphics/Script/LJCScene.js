// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// LJCScene.js

// Represents a group of objects.
class LJCScene
{
  // The Constructor method.
  constructor(name)
  {
    this.Name = name;
    let canvas = gLJCGraphics.Canvas;
    this.TranslatePoint = new LJCPoint(canvas.width / 2
      , canvas.height / 2);
    this.TranslatePoint.ViewZ = 90;
    this.Meshes = [];
  }

  // Class Methods
  // ---------------
  // AddCube(radius)
  // Translate()
  // Show()

  // Creates a cube.
  AddCube(radius)
  {
    let cube = new LJCMesh("Cube");
    this.Meshes.push(cube);

    let base = cube.CreateFace("Base", radius
      , 4);
    base.StrokeStyle = "cyan";

    let square = base.Clone();
    square.Name = "Front";
    square.FillStyle = "red";
    square.Move(0, 0, square.PathRadius);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Back";
    square.FillStyle = "green";
    // Rotate clockwise.
    square.AddRotateXZ(square.Arc * 2);
    square.Move(0, 0, -square.PathRadius);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Left";
    square.FillStyle = "steelblue";
    // Rotate clockwise.
    square.AddRotateXZ(square.Arc);
    square.Move(-square.PathRadius, 0, 0);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Right";
    square.FillStyle = "lightblue";
    // Rotate counter.
    square.AddRotateXZ(-square.Arc);
    square.Move(square.PathRadius, 0, 0);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Top";
    square.FillStyle = "lightsteelblue";
    // Rotate clockwise.
    square.AddRotateZY(square.Arc);
    square.Move(0, -square.PathRadius, 0);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Bottom";
    square.FillStyle = "lightgreen";
    // Rotate counter.
    square.AddRotateZY(-square.Arc);
    square.Move(0, square.PathRadius, 0);
    cube.Paths.push(square);
  }

  // 
  Translate()
  {
    for (let mesh of this.Meshes)
    {
      mesh.Translate();
    }
  }

  // Shows the group.
  Show()
  {
    for (let mesh of this.Meshes)
    {
      mesh.Show();
    }
  }
}
