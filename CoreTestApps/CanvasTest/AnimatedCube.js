// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// AnimatedCube.js

// Animates a cube mesh.
// ***************
class AnimatedCube
{
  // Create AnimatedCube object.
  static Create(animatedCube)
  {
    if (animatedCube != null)
    {
      animatedCube.Stop = true;
    }
    gScene = new LJCScene("Cube");
    gScene.AddCube(30);
    let cube = gScene.Meshes[0];
    let retAnimatedCube = new AnimatedCube(cube);
    return retAnimatedCube;
  }

  // The Constructor method.
  //constructor(name)
  constructor(cube)
  {
    let g = gLJCGraphics;

    this.Cube = cube;

    // Animate values.
    this.AddXY = 0;
    this.AddXZ = 0;
    this.AddZY = 0;
    this.FirstRectangle;
    this.MaxRectangle;
    this.MoveValue = 0;
    this.PrevRectangle;
    this.RotateType = "XYTip";
    this.Stop = false;
  }

  // Data Methods
  // ---------------
  // Clone()

  // Creates a Clone of this object.
  Clone()
  {
    let retAnimate = new AnimatedCube();

    // Animate values.
    retAnimate.AddXY = this.AddXY;
    retAnimate.AddXZ = this.AddXZ;
    retAnimate.AddZY = this.AddZY;
    rectangle.FirstRectangle = null;
    retAnimate.MaxRectangle = this.MaxRectangle;
    retAnimate.MoveValue = this.MoveValue;
    retAnimate.PrevRectangle = this.PrevRectangle;
    retAnimate.Stop = false;

    retAnimate.Cube = this.Cube.Clone();
    return retMesh;
  }

  // Test Animation
  Animate()
  {
    let g = gLJCGraphics;

    if (this.FirstRectangle != null)
    {
      this.ClearRectangle(this.FirstRectangle);
      this.FirstRectangle = null;
    }

    if (this.PrevRectangle != null)
    {
      this.ClearRectangle(this.PrevRectangle);
    }

    if (!this.Stop)
    {
      let cube = null;
      switch (this.RotateType)
      {
        case "XY":
          // Rotate clockwise
          this.Cube.AddRotateXY(this.AddXY);
          cube = this.Cube.Clone();
          break;

        case "XYTip":
          // Main Rotation accumulates.
          // Rotate clockwise
          this.Cube.AddRotateXY(this.AddXY);

          // Tip Angle is one time.
          cube = this.Cube.Clone();
          // Rotate counter
          cube.AddRotateZY(-55 * g.Radian);
          // Rotate clockwise
          cube.AddRotateXZ(-5 * g.Radian);
          break;

        case "XZ":
          // Rotate clockwise
          this.Cube.AddRotateXZ(this.AddXY);
          cube = this.Cube.Clone();
          break;

        case "ZY":
          // Rotate counter
          this.Cube.AddRotateZY(this.AddXY);
          cube = this.Cube.Clone();
          break;
      }

      this.PrevRectangle = cube.GetRectangle();
      this.SetMaxRectangle();
      this.Show(cube);
      //setTimeout(this.DoAnimate.bind(this), 5);
      requestAnimationFrame(this.Animate.bind(this));
    }
  }

  // Clears the rectangle.
  ClearRectangle(rectangle)
  {
    let g = gLJCGraphics;
    let ctx = g.Context;
    let rect = rectangle;

    if (rect != null)
    {
      let x = 1;
      let y = 1;
      let width = 2 + x;
      let height = 2 + y;
      ctx.clearRect(rect.Left - x, rect.Top - y
        , rect.Width + width, rect.Height + height);
    }
  }

  // Timeout do animation.
  DoAnimate()
  {
    requestAnimationFrame(this.Animate.bind(this));
  }

  // Sets the maximum rectangle values.
  SetMaxRectangle()
  {
    if (null == this.MaxRectangle)
    {
      this.MaxRectangle = this.PrevRectangle;
    }

    let prevRect = this.PrevRectangle;
    let maxRect = this.MaxRectangle;
    if (prevRect.Left < maxRect.Left)
    {
      maxRect.Left = prevRect.Left;
    }
    if (prevRect.Top < maxRect.Top)
    {
      maxRect.Top = prevRect.Top;
    }
    if (prevRect.Width > maxRect.Width)
    {
      maxRect.Width = prevRect.Width;
    }
    if (prevRect.Height > maxRect.Height)
    {
      maxRect.Height = prevRect.Height;
    }
  }

  // Shows the object.
  Show(cube)
  {
    let g = gLJCGraphics;

    //for (let path of this.Paths)
    for (let path of cube.Paths)
    {
      let normal = path.Normal;
      let rotation = g.GetRotation(normal.X, normal.Z);
      let angle = rotation / g.Radian;

      if (angle > 0
        && angle < 40)
      {
        path.FillStyle = gShades[0].GetStyle();
      }
      if (angle >= 40
        && angle < 70)
      {
        path.FillStyle = gShades[1].GetStyle();
      }
      if (angle >= 70
        && angle < 100)
      {
        path.FillStyle = gShades[2].GetStyle();
      }
      if (angle >= 100
        && angle < 125)
      {
        path.FillStyle = gShades[3].GetStyle();
      }
      if (angle >= 125
        && angle < 145)
      {
        path.FillStyle = gShades[4].GetStyle();
      }
      if (angle >= 145
        && angle < 155)
      {
        path.FillStyle = gShades[5].GetStyle();
      }
      if (angle >= 155
        && angle < 165)
      {
        path.FillStyle = gShades[6].GetStyle();
      }
      if (angle >= 165
        && angle < 180)
      {
        path.FillStyle = gShades[7].GetStyle();
      }
      if ("Front" == path.Name)
      {
        path.FillStyle = gShades[2].GetStyle();
      }

      path.Show();
    }
  }
}
