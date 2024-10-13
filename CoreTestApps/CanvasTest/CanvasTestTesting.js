// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// CanvasTestTesting.js

// Handles the CanvasTest.html events.
// ***************
class CanvasTestTesting
{
  // The Constructor methods.
  // ---------------

  // Test AddRotateXY and graphics Point().
  static AddRotateXY()
  {
    let point = new LJCPoint(30);
    let addRadians = g.Radian;
    let radians = 0;
    for (let degrees = 0; degrees <= 360; degrees++)
    {
      switch (degrees)
      {
        case 90:
          radians = Math.PI / 2 - g.Radian;
          // 1.5533430342749532
          point = point.RotatedPoint(radians, 30);
          break;

        case 180:
          radians = Math.PI - g.Radian;
          // 3.12413936106985
          point = point.RotatedPoint(radians, 30);
          break;

        case 270:
          radians = Math.PI + (Math.PI / 2) - g.Radian;
          // 4.694935687864747
          point = point.RotatedPoint(radians, 30);
          break;

        case 360:
          radians = Math.PI * 2 - g.Radian;
          // 6.265732014659643
          point = point.RotatedPoint(radians, 30);
          break;
      }

      point.AddRotateXY(addRadians);
      let sPoint = point.Clone();
      sPoint.Translate();
      g.Point(sPoint, "yellow");
    }
  }

  // Shows animation.
  //<script src="../../CoreTestApps/CanvasTest/CanvasTestEvent.js"></script>
  static Animate()
  {
    this.Options("Show");

    let beginColor = 0xffff00;
    let endColor = 0x111100;
    let varyRed = true;
    let varyGreen = true;
    let varyBlue = false;
    gEvents.SetShadeValues(beginColor, endColor
      , varyRed, varyGreen, varyBlue);

    eXYTip.checked = true;
    gEvents.OptionClick(eXYTip);
  }

  // Hides the option buttons.
  static Options(typeName = "Hide")
  {
    let display = "";
    switch (typeName.toLowerCase())
    {
      case "hide":
        display = "none";
        break;

      case "show":
        display = "initial";
        break;
    }

    eXY.style.display = display;
    eXYLabel.style.display = display;

    eXYTip.style.display = display;
    eXYTipLabel.style.display = display;

    eXZ.style.display = display;
    eXZLabel.style.display = display;

    eZY.style.display = display;
    eZYLabel.style.display = display;
  }

  // Test Rotate XY and graphics Point();
  static RotateXY()
  {
    for (let degrees = 0; degrees <= 360; degrees++)
    {
      let point = new LJCPoint(30);
      let radians = degrees * g.Radian;
      switch (degrees)
      {
        case 90:
          radians = Math.PI / 2;
          break;

        case 180:
          radians = Math.PI;
          break;

        case 270:
          radians = Math.PI + (Math.PI / 2);
          break;

        case 360:
          radians = Math.PI * 2;
          break;
      }

      point.RotateXY(radians);
      let sPoint = point.Clone();
      sPoint.Translate();
      g.Point(sPoint, "yellow");
    }
  }

  // The Constructor method.
  constructor()
  {
  }
}