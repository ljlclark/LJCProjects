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

    let square = base.Clone();
    square.Name = "Front";
    square.Move(0, 0, square.PathRadius);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Back";
    // Rotate clockwise.
    square.AddRotateXZ(square.Arc * 2);
    square.Move(0, 0, -square.PathRadius);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Left";
    // Rotate clockwise.
    square.AddRotateXZ(square.Arc);
    square.Move(-square.PathRadius, 0, 0);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Right";
    // Rotate counter.
    square.AddRotateXZ(-square.Arc);
    square.Move(square.PathRadius, 0, 0);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Top";
    // Rotate clockwise.
    // *** Next Statement *** Change
    square.AddRotateZY(square.Arc);
    square.Move(0, -square.PathRadius, 0);
    cube.Paths.push(square);

    square = base.Clone();
    square.Name = "Bottom";
    // Rotate counter.
    // *** Next Statement *** Change
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
