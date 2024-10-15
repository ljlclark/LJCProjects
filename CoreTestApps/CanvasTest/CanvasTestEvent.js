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
  constructor()
  {
    // Shade values.
    this.BeginColor = 0xffff00;
    this.EndColor = 0x111100;
    this.VaryRed = true;
    this.VaryGreen = true;
    this.VaryBlue = false;
  }

  // Sets the shade values.
  SetShadeValues(beginColor = 0xffff00, endColor = 0x111100
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
    eCanvas.addEventListener("click", this.CanvasClick.bind(this));
    eXY.addEventListener("click", this.RadioClick.bind(this));
    eXYTip.addEventListener("click", this.RadioClick.bind(this));
    eXZ.addEventListener("click", this.RadioClick.bind(this));
    eZY.addEventListener("click", this.RadioClick.bind(this));
  }

  CanvasClick(event)
  {
    let point = new LJCPoint();
    point.X = event.clientX - gScene.TranslatePoint.X;
    point.Y = event.clientY - gScene.TranslatePoint.Y;
    gAnimatedCube.LightPoint.X = point.X;
    gAnimatedCube.LightPoint.Y = point.Y;
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

    let rotateXY = 1.7 * g.Radian;
    switch (target.id)
    {
      case "eXY":
        this.#CreateAnimatedCube("XY");
        gAnimatedCube.AddXY = rotateXY;
        break;

      case "eXYTip":
        this.#CreateAnimatedCube("XYTip");
        gAnimatedCube.AddXY = rotateXY;
        break;

      case "eXZ":
        this.#CreateAnimatedCube("XZ");
        gAnimatedCube.AddXY = rotateXY;
        break;

      case "eZY":
        this.#CreateAnimatedCube("ZY");
        gAnimatedCube.AddXY = rotateXY;
        break;
    }

    let rectangle = null;
    if (gScene != null)
    {
      let cube = gScene.Meshes[0];
      rectangle = gAnimatedCube.MaxRectangle;
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
        eXYTip.checked = false;
        eXZ.checked = false;
        eZY.checked = false;
        break;

      case "xytip":
        eXY.checked = false;
        eXZ.checked = false;
        eZY.checked = false;
        break;

      case "xz":
        eXY.checked = false;
        eXYTip.checked = false;
        eZY.checked = false;
        break;

      case "zy":
        eXY.checked = false;
        eXYTip.checked = false;
        eXZ.checked = false;
        break;
    }
  }
}