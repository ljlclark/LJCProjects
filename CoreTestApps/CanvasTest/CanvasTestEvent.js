// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// CanvasTestEvent.js

// Handles the CanvasTest.html events.
// ***************
class CanvasTestEvent
{
  // Constructor methods.
  // ---------------
  // SetEvents()

  // The Constructor method.
  constructor()
  {
    // Shade values.
    this.BeginColor = 0xffff00;
    this.EndColor = 0x111100;
    this.VaryRed = true;
    this.VaryGreen = true;
    this.VaryBlue = false;
    this.PrevPoint = null;
  }

  // Sets the event handlers.
  SetEvents()
  {
    eCanvas.addEventListener("click", this.CanvasClick.bind(this));
    eCanvas.addEventListener("mousemove", this.MouseMove.bind(this));
    eCanvas.addEventListener("wheel", this.CanvasWheel.bind(this));
    eXY.addEventListener("click", this.RadioClick.bind(this));
    eXYTip.addEventListener("click", this.RadioClick.bind(this));
    eXZ.addEventListener("click", this.RadioClick.bind(this));
    eZY.addEventListener("click", this.RadioClick.bind(this));
  }

  // Event handlers.
  // ---------------
  // CanvasClick(event)
  // MouseMove(event)
  // RadioClick(event)

  // The eCanvas click event handler.
  CanvasClick(event)
  {
    let modelPoint = this.ModelPoint(event.clientX
      , event.clientY);
    let lightPoint = gAnimatedCube.LightPoint;
    lightPoint.X = modelPoint.X;
    lightPoint.Y = modelPoint.Y;
    lightPoint.Z = modelPoint.Z;
    this.#SetSourceText();
  }

  // The eCanvas wheel event handler.
  CanvasWheel(event)
  {
    if (event.deltaY < 0)
    {
      gScene.TranslatePoint.ViewZ++;
    }
    if (event.deltaY > 0)
    {
      gScene.TranslatePoint.ViewZ--;
    }
    let viewZ = gScene.TranslatePoint.ViewZ;
    eWheel.innerText = `ViewZ: ${viewZ}`;
  }

  // the eCanvas mouse move event handler.
  MouseMove(event)
  {
    let g = gLJCGraphics;

    let canvas = g.Canvas;
    let tPoint = gScene.TranslatePoint;
    let radius = tPoint.Y;

    let modelPoint = this.ModelPoint(event.clientX, event.clientY);
    let radiusPoint = this.RadiusPoint(modelPoint);
    if (this.PrevPoint != null)
    {
      g.Clear(0, 0, canvas.width, canvas.height);
    }
    this.PrevPoint = radiusPoint;
    g.Arc(radiusPoint, 2, null, null, "cyan");

    let p = modelPoint;
    eMouse.innerText = `Mouse Position X:${p.X}  Y:${p.Y}`;
    eMouse.innerText += `  Z:${p.Z}`;
  }

  // Radio Click event handler.
  RadioClick(event)
  {
    if (LJC.HasValue(event.target.id))
    {
      this.OptionClick(event.target);
    }
  }

  // Public methods.
  // ---------------
  // modelPoint = ModelPoint(clientX, clientY)
  // OptionClick(target)
  // radiusPoint = RadiusPoint(modelPoint)
  // SetShadeValues(beginColor = 0xffff00, endColor = 0x111100
  //   , varyRed = true, varyGreen = true, varyBlue = false)

  // Gets the model point.
  ModelPoint(clientX, clientY)
  {
    let g = gLJCGraphics;

    let canvas = g.Canvas;
    let tPoint = gScene.TranslatePoint;
    let radius = tPoint.Y;

    let canvasPoint = new LJCPoint();
    canvasPoint.X = clientX - canvas.offsetLeft;
    canvasPoint.Y = clientY - canvas.offsetTop;

    // Calculate model rotation.
    let retModelPoint = canvasPoint.Clone();
    retModelPoint.X -= tPoint.X;
    retModelPoint.Y -= tPoint.Y;

    // Get z value.
    let radiusPoint = this.RadiusPoint(retModelPoint);
    let diffX = Math.abs(radiusPoint.X - canvasPoint.X);
    let diffY = Math.abs(radiusPoint.Y - canvasPoint.Y);
    retModelPoint.Z = Math.round(g.Radius(diffX, diffY));
    return retModelPoint;
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

  // Get show radius point.
  RadiusPoint(modelPoint)
  {
    let tPoint = gScene.TranslatePoint;
    let radius = tPoint.Y;

    let rotation = g.Rotation(modelPoint.X
      , modelPoint.Y);
    let retRadiusPoint = tPoint.RotatedPoint(rotation
      , radius);
    retRadiusPoint.X += tPoint.X;
    retRadiusPoint.Y += tPoint.Y;
    return retRadiusPoint;
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

  // Private methods.
  // ---------------
  // #CreateAnimatedCube(rotateType)
  // #ResetOptions(rotateType)
  // #SetSourceText()

  // Creates the AnimatedCube.
  #CreateAnimatedCube(rotateType)
  {
    gAnimatedCube = AnimatedCube.Create(gAnimatedCube);
    gAnimatedCube.setBaseColor(this.BeginColor
      , this.EndColor, this.VaryRed, this.VaryGreen
      , this.VaryBlue);
    gAnimatedCube.RotateType = rotateType;
    this.#ResetOptions(rotateType);
    this.#SetSourceText();
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

  // Sets the light source position text.
  #SetSourceText()
  {
    let lightPoint = gAnimatedCube.LightPoint;
    eSource.innerText = `Light Source X:${lightPoint.X} `
    eSource.innerText += `  Y:${lightPoint.Y} Z:${lightPoint.Z} `;
  }
}