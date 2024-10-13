// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// AnimatedCube.js

// Animates a cube mesh.
// ***************
class AnimatedCube
{
  #BaseColor;

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
  constructor(cube)
  {
    let g = gLJCGraphics;

    this.Cube = cube;

    // Animate values.
    this.AddXY = 0;
    this.AddXZ = 0;
    this.AddZY = 0;
    this.setBaseColor(0xffff00, 0x111100, true, true
      , false);
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

  // Animate Methods
  // ---------------
  // Animate()
  // ClearRectangle(rectangle)
  // DoAnimate()
  // SetMaxRectangle()
  // Show(cube)

  // Animate the cube.
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

      this.PrevRectangle = cube.Rectangle();
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

    for (let path of cube.Paths)
    {
      let normal = path.Normal;
      if (normal.Z > 0)
      {
        //if ("Back" == path.Name)
        //{
        let rotation = g.PointRotation(normal);
        let normalAngle = g.ToAngle(rotation);

        let lightPoint = new LJCPoint(5, 60, 10);
        let lightRotation = g.PointRotation(lightPoint);;
        let ligntAngle = g.ToAngle(lightRotation);

        let shadeRotation = Math.abs(rotation - lightRotation);
        let shadeFactor = g.ToAngle(shadeRotation);
        path.FillStyle
          = this.#BaseColor.ShadeStyle(shadeFactor);
        //}
      }
      path.Show();
    }
  }

  // Getters and Setters
  // ---------------

  // Gets the BaseColor object.
  getBaseColor()
  {
    return this.#BaseColor;
  }

  // Sets the BaseColor object.
  setBaseColor(beginColorValue, endColorValue, varyRed
    , varyGreen, varyBlue)
  {
    this.#BaseColor = new LJCColor(beginColorValue);
    this.#BaseColor.SetVaryValue(beginColorValue, endColorValue
      , varyRed, varyGreen, varyBlue);
  }
}
