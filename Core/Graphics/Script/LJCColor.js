// Copyright(c) Lester J.Clark and Contributors. -- >
// Licensed under the MIT License.-- >
// LJCColors.js

// Provides color methods.
// ***************
class LJCColor
{
  #Blue;
  #Green;
  #Red;
  #Value;

  // Constructor methods.
  // ---------------
  // SetColors(colorValue)

  // The Constructor method.
  constructor(colorValue = 0)
  {
    this.#Blue = 0;
    this.#Green = 0;
    this.#Red = 0;
    this.#Value = 0;
    this.SetColors(colorValue);
    this.VaryBlue = 0;
    this.VaryGreen = 0;
    this.VaryRed = 0;
    this.VaryValue = 0;
  }

  // Sets the color decimal values.
  SetColors(colorValue)
  {
    this.setRed(this.ParseRed(colorValue));
    this.setGreen(this.ParseGreen(colorValue));
    this.setBlue(this.ParseBlue(colorValue));
  }

  // Data Methods
  // ---------------
  // Clone();

  // Creates a Clone of this object.
  Clone()
  {
    let retColor = new LJCColor();
    retColor.setBlue(this.getBlue());
    retColor.setGreen(this.getGreen());
    retColor.setRed(this.getRed());
    retColor.VaryBlue = this.VaryBlue;
    retColor.VaryGreen = this.VaryGreen;
    retColor.VaryRed = this.VaryRed;
    retColor.VaryValue = this.VaryValue;
    return retColor;
  }

  // Color Methods
  // ---------------
  // CreateColor(colorValue)
  // CreateStyle(colorValue)
  // ColorsStyle(redValue, greenValue, blueValue)
  // Shade(shadeColor, factor, isVary)
  // ShadeStyle(factor)
  // Style()
  // ParseBlue(colorValue)
  // ParseGreen(colorValue)
  // ParseRed(colorValue)
  // SetVaryValue(beginValue, endValue, varyRed, varyGreen
  //   , varyBlue)
  // ValueToHex(colorValue)
  // ValueToInt(colorValue)
  // ValueToStyle(colorValue)

  // Creates color object from style, hex or decimal value.
  CreateColor(colorValue)
  {
    colorValue = this.ValueToInt(colorValue);
    let hexValue = `0x${colorValue.toString(16)}`;
    let retColor = new LJCColor(hexValue);
    return retColor;
  }

  // Creates style from style, hex or decimal value.
  CreateStyle(colorValue)
  {
    let retStyle = "";

    let red = "00";
    let green = "00";
    let blue = "00";

    colorValue = this.ValueToInt(colorValue);
    let hexValue = colorValue.toString(16);
    if (hexValue.length >= 2)
    {
      red = hexValue.substring(0, 2);
    }
    if (hexValue.length >= 4)
    {
      green = hexValue.substring(2, 4);
    }
    if (hexValue.length >= 6)
    {
      blue = hexValue.substring(4, 6);
    }
    retStyle = `#${red}${green}${blue}`;
    return retStyle;
  }

  // Gets a style from the color values.
  ColorsStyle(redValue, greenValue, blueValue)
  {
    let retStyle = "";

    let red = this.ValueToInt(redValue);
    red = red.toString(16);
    if ("0" == red)
    {
      red = "00";
    }
    let green = greenValue.toString(16);
    if ("0" == green)
    {
      green = "00";
    }
    let blue = blueValue.toString(16);
    if ("0" == blue)
    {
      blue = "00";
    }
    retStyle = `#${red}${green}${blue}`;
    return retStyle;
  }


  // Vary the shade value.
  Shade(shadeColor, factor, isVary)
  {
    let retShadeColor = shadeColor;

    if (isVary
      && factor < 180)
    {
      let vary = this.VaryValue * factor;
      if (vary > 0
        && shadeColor > 0 + vary)
      {
        retShadeColor -= vary;
      }
      if (vary < 0
        && shadeColor < 255 - vary)
      {
        retShadeColor -= vary;
      }
    }
    retShadeColor = this.ValueToInt(retShadeColor);
    return retShadeColor;
  }

  // Gets the shade style.
  ShadeStyle(factor)
  {
    let red = this.Shade(this.#Red, factor, this.VaryRed);
    let green = this.Shade(this.#Green, factor, this.VaryGreen);
    let blue = this.Shade(this.#Blue, factor, this.VaryBlue);
    let retStyle = this.ColorsStyle(red, green, blue);
    return retStyle;
  }

  // Gets the current color as a style value.
  Style()
  {
    let retStyle = this.ColorsStyle(this.#Red, this.#Green
      , this.#Blue);
    return retStyle;
  }

  // Gets the deciml Blue color from Hex color.
  ParseBlue(colorValue)
  {
    let retValue = 0;

    colorValue = this.ValueToInt(colorValue);
    let hexValue = colorValue.toString(16);
    if (hexValue.length >= 4)
    {
      retValue = `0x${hexValue.substring(4, 6)}`;
      retValue = parseInt(retValue);
    }
    return retValue;
  }

  // Gets the deciml Green color from Hex color.
  ParseGreen(colorValue)
  {
    let retValue = 0;

    colorValue = this.ValueToInt(colorValue);
    let hexValue = colorValue.toString(16);
    if (hexValue.length >= 6)
    {
      retValue = `0x${hexValue.substring(2, 4)}`;
      retValue = parseInt(retValue);
    }
    return retValue;
  }

  // Gets the deciml Red value from the color value.
  ParseRed(colorValue)
  {
    let retValue = 0;

    colorValue = this.ValueToInt(colorValue);
    let hexValue = colorValue.toString(16);
    if (hexValue.length >= 2)
    {
      retValue = `0x${hexValue.substring(0, 2)}`;
      retValue = parseInt(retValue);
    }
    return retValue;
  }

  // Sets the available vary range;
  SetVaryValue(beginValue, endValue, varyRed, varyGreen
    , varyBlue)
  {
    this.VaryRed = varyRed;
    this.VaryGreen = varyGreen;
    this.VaryBlue = varyBlue;

    let beginColor = this.CreateColor(beginValue);
    let endColor = this.CreateColor(endValue);

    let redDiff = beginColor.getRed() - endColor.getRed();
    let greenDiff = beginColor.getGreen() - endColor.getGreen();
    let blueDiff = beginColor.getBlue() - endColor.getBlue();

    let varyRange = 255;
    if (varyRed)
    {
      varyRange = redDiff;
    }
    if (varyGreen)
    {
      if (greenDiff < varyRange)
      {
        varyRange = greenDiff;
      }
    }
    if (varyBlue)
    {
      if (blueDiff < varyRange)
      {
        varyRange = blueDiff;
      }
    }
    this.VaryValue = varyRange / 180;
  }

  // Convert style, hex or decimal to Hex.
  ValueToHex(colorValue)
  {
    let retValue = 0;

    colorValue = this.ValueToInt(colorValue);
    retValue = `0x${colorValue.toString(16)}`;
    return retValue;
  }

  // Convert style, hex or decimal to int.
  ValueToInt(colorValue)
  {
    let retValue = 0;

    if (null == colorValue)
    {
      colorValue = 0;
    }
    if (typeof colorValue == "string")
    {
      if (colorValue.startsWith("#"))
      {
        colorValue = colorValue.substring(1);
      }
      if (colorValue.startsWith("0x"))
      {
        colorValue = colorValue.substring(2);
      }
    }
    colorValue = `0x${colorValue.toString(16)}`;
    retValue = parseInt(colorValue);
    return retValue;
  }

  // Gets the style value.
  ValueToStyle(colorValue)
  {
    colorValue = this.ValueToInt(colorValue);
    let red = this.ParseRed(colorValue);
    let green = this.ParseGreen(colorValue);
    let blue = this.ParseBlue(colorValue);

    let retStyle = this.GetColorsStyle(red, green, blue);
    return retStyle;
  }

  // Getters and Setters
  // ---------------
  // getBlue()
  // setBlue()
  // getGreen()
  // setGreen()
  // getRed()
  // setRed()
  // getValue()

  // Gets the decimal blue value.
  getBlue()
  {
    return this.#Blue;
  }

  // Sets the blue value.
  setBlue(blueValue)
  {
    this.#Blue = this.ValueToInt(blueValue);
    let style = this.Style();
    let value = this.ValueToInt(style);
    this.#Value = value;
  }

  // Gets the decimal green value.
  getGreen()
  {
    return this.#Green;
  }

  // Sets the blue value.
  setGreen(greenValue)
  {
    this.#Green = this.ValueToInt(greenValue);
    let style = this.Style();
    let value = this.ValueToInt(style);
    this.#Value = value;
  }

  // Gets the decimal red value.
  getRed()
  {
    return this.#Red;
  }

  // Sets the red value.
  setRed(redValue)
  {
    this.#Red = this.ValueToInt(redValue);
    let style = this.Style();
    let value = this.ValueToInt(style);
    this.#Value = value;
  }

  // Gets the color value.
  getValue()
  {
    return this.#Value;
  }
}
