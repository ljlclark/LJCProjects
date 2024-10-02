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

  // The Constructor method.
  constructor(colorValue = 0)
  {
    this.#Blue = 0;
    this.#Green = 0;
    this.#Red = 0;
    this.#Value = 0;
    this.SetColors(colorValue);
  }

  // Sets the color decimal values.
  SetColors(colorValue)
  {
    this.setBlue(this.ParseBlue(colorValue));
    this.setGreen(this.ParseGreen(colorValue));
    this.setRed(this.ParseRed(colorValue));
  }

  // Data Methods
  // ---------------
  // Clone();

  Clone()
  {
    let retColor = new LJCColor();
    retColor.setBlue(this.getBlue());
    retColor.setGreen(this.getGreen());
    retColor.setRed(this.getRed());
    return retColor;
  }

  // Color Methods
  // ---------------
  // GetShades(beginValue, endValue, count, varyRed
  //   , varyGreen, varyBlue)
  // GetStyle()
  // ParseBlue(colorValue)
  // ParseGreen(colorValue)
  // ParseRed(colorValue)
  // ValueToHex(colorValue)
  // ValueToInt(colorValue)
  // ValueToStyle(colorValue)

  // Gets color shades.
  GetShades(beginValue, endValue, count, varyRed
    , varyGreen, varyBlue)
  {
    let retShades = [];

    beginValue = this.ValueToInt(beginValue);
    let hexValue = `0x${beginValue.toString(16)}`;
    let beginColor = new LJCColor(hexValue);

    endValue = this.ValueToInt(endValue);
    hexValue = `0x${endValue.toString(16)}`;
    let endColor = new LJCColor(hexValue);

    let blueDiff = beginColor.getBlue() - endColor.getBlue();
    let greenDiff = beginColor.getGreen() - endColor.getGreen();
    let redDiff = beginColor.getRed() - endColor.getRed();

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
    let varyValue = Math.round(varyRange / count);

    let shade = beginColor.Clone();
    retShades.push(shade);
    for (let index = 0; index < count - 1; index++)
    {
      shade = shade.Clone();
      if (varyBlue)
      {
        shade.setBlue(shade.#Blue - varyValue);
      }
      if (varyGreen)
      {
        shade.setGreen(shade.#Green - varyValue);
      }
      if (varyRed)
      {
        shade.setRed(shade.#Red - varyValue);
      }
      retShades.push(shade);
    }
    return retShades;
  }

  // Gets the current color as a style value.
  GetStyle()
  {
    let retStyle = "";

    let blue = this.#Blue.toString(16);
    if ("0" == blue)
    {
      blue = "00";
    }
    let green = this.#Green.toString(16);
    if ("0" == green)
    {
      green = "00";
    }
    let red = this.#Red.toString(16);
    if ("0" == red)
    {
      red = "00";
    }

    retStyle = `#${red}${green}${blue}`;
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
    let retStyle = "";

    colorValue = this.ValueToInt(colorValue);
    let blue = this.ParseBlue(colorValue);
    let green = this.ParseGreen(colorValue);
    let red = this.ParseRed(colorValue);

    blue = blue.toString(16);
    if ("0" == blue)
    {
      blue = "00";
    }
    green = green.toString(16);
    if ("0" == green)
    {
      green = "00";
    }
    red = red.toString(16);
    if ("0" == red)
    {
      red = "00";
    }

    retStyle = `#${red}${green}${blue}`;
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
  setBlue(colorValue)
  {
    this.#Blue = this.ValueToInt(colorValue);
    let style = this.GetStyle();
    let value = this.ValueToInt(style);
    this.#Value = value;
  }

  // Gets the decimal green value.
  getGreen()
  {
    return this.#Green;
  }

  // Sets the blue value.
  setGreen(colorValue)
  {
    this.#Green = this.ValueToInt(colorValue);
    let style = this.GetStyle();
    let value = this.ValueToInt(style);
    this.#Value = value;
  }

  // Gets the decimal red value.
  getRed()
  {
    return this.#Red;
  }

  // Sets the red value.
  setRed(colorValue)
  {
    this.#Red = this.ValueToInt(colorValue);
    let style = this.GetStyle();
    let value = this.ValueToInt(style);
    this.#Value = value;
  }

  // Gets the color value.
  getValue()
  {
    return this.#Value;
  }
}
