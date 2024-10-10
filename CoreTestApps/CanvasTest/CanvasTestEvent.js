// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// CanvasTestEvent.js

// Handles the CanvasTest.html events.
// ***************
class CanvasTestEvent
{
  // The Constructor methods.
  // ---------------

  // The Constructor method.
  constructor(beginColor = 0xffff00, endColor = 0x111100
    , varyRed = true, varyGreen = true, varyBlue = false)
  {
    this.BeginColor = beginColor;
    this.EndColor = endColor;
    this.VaryRed = varyRed;
    this.VaryGreen = varyGreen;
    this.VaryBlue = varyBlue;
  }

  // Sets the event handlers.
  SetEvents()
  {
    XY.addEventListener("click", this.RadioClick.bind(this));
    XYTip.addEventListener("click", this.RadioClick.bind(this));
    XZ.addEventListener("click", this.RadioClick.bind(this));
    ZY.addEventListener("click", this.RadioClick.bind(this));
  }

  // Radio Click event handler.
  RadioClick(event)
  {
    if (LJC.HasValue(event.target.id))
    {
      this.OptionClick(event.target);
    }
  }

  // Handle options click.
  OptionClick(target)
  {
    if (gAnimatedCube != null)
    {
      gAnimatedCube.Stop = true;
    }

    let rectangle = null;
    if (gScene != null)
    {
      let cube = gScene.Meshes[0];
      rectangle = gAnimatedCube.MaxRectangle;
    }

    let rotateXY = 2.12 * g.Radian;
    switch (target.id)
    {
      case "XY":
        this.#CreateAnimatedCube("XY");
        gAnimatedCube.AddXY = rotateXY;
        break;

      case "XYTip":
        this.#CreateAnimatedCube("XYTip");
        gAnimatedCube.AddXY = rotateXY;
        break;

      case "XZ":
        this.#CreateAnimatedCube("XZ");
        gAnimatedCube.AddXY = rotateXY;
        break;

      case "ZY":
        this.#CreateAnimatedCube("ZY");
        gAnimatedCube.AddXY = rotateXY;
        break;
    }
    gAnimatedCube.FirstRectangle = rectangle;
    gAnimatedCube.Animate();
  }

  // Creates the AnimatedCube.
  #CreateAnimatedCube(rotateType)
  {
    gAnimatedCube = AnimatedCube.Create(gAnimatedCube);
    gAnimatedCube.setBaseColor(this.BeginColor
      , this.EndColor, this.VaryRed, this.VaryGreen
      , this.VaryBlue);
    gAnimatedCube.RotateType = rotateType;
    this.#ResetOptions(rotateType);
  }

  // Unchecks the unused options.
  #ResetOptions(rotateType)
  {
    switch (rotateType.toLowerCase())
    {
      case "xy":
        XYTip.checked = false;
        XZ.checked = false;
        ZY.checked = false;
        break;

      case "xytip":
        XY.checked = false;
        XZ.checked = false;
        ZY.checked = false;
        break;

      case "xz":
        XY.checked = false;
        XYTip.checked = false;
        ZY.checked = false;
        break;

      case "zy":
        XY.checked = false;
        XYTip.checked = false;
        XZ.checked = false;
        break;
    }
  }
}