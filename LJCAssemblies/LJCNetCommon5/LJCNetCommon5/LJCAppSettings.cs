// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAppSettings.cs
using System.Drawing;
using System.Xml.Linq;

namespace LJCNetCommon5
{
  // Represents the Configuration AppSettings.
  /// <include path="members/AppSettings/*" file="Doc/ProjectNetCommon.xml"/>
  /// <group name="constructors">Constructors</group>
  /// <group name="settings">Get Setting Types</group>
  /// <group name="classData">Class Data</group>
  public class LJCAppSettings
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCAppSettings.xml"/>
    /// <parentGroup>constructors</parentGroup>
    public LJCAppSettings(string fileSpec)
    {
      XElement appSettings;
      string errorText;

      FileSpec = fileSpec;
      if (!File.Exists(fileSpec))
      {
        string value = Directory.GetCurrentDirectory();
        errorText = $"File '{FileSpec}' was not found.";
        errorText += $"\r\nFolder '{value}'";
        throw new FileNotFoundException(errorText);
      }

      mRootElement = XElement.Load(FileSpec);
      appSettings = mRootElement.Element("appSettings");

      if (null == appSettings)
      {
        errorText = $"File '{FileSpec}' 'AppSettings' Element not found.";
        throw new MissingMemberException(errorText);
      }

      mSettings = appSettings.Elements("add");
      //if (0 == mSettings.Count())
      if (!mSettings.Any())
      {
        errorText = $"File '{FileSpec}' AppSettings 'add' Elements not found.";
        throw new MissingMemberException(errorText);
      }
    }
    #endregion

    #region Get Setting Types

    // Gets the bool value of the specified setting.
    /// <include path="members/GetBool/*" file="Doc/LJCAppSettings.xml"/>
    /// <parentGroup>settings</parentGroup>
    public bool GetBool(string keyName)
    {
      bool retValue = false;

      string keyValue = GetKeyValue(keyName);
      if (LJC.HasValue(keyValue))
      {
        //retValue = true;
        _ = bool.TryParse(keyValue, out retValue);
      }
      return retValue;
    }

    // Gets the Color value of the specified setting.
    /// <include path="members/GetColor/*" file="Doc/LJCAppSettings.xml"/>
    /// <parentGroup>settings</parentGroup>
    public Color GetColor(string keyName, Color defaultColor)
    {
      Color retValue = defaultColor;

      if (LJC.HasValue(keyName))
      {
        string keyValue = GetKeyValue(keyName);
        if (LJC.HasValue(keyValue))
        {
          if (keyValue.Contains(','))
          {
            retValue = GetColorFromRGBString(keyValue);
          }
          else
          {
            retValue = Color.FromName(keyValue);
          }
        }
      }
      return retValue;
    }

    // Gets a Color from an RGB string.
    /// <include path="members/GetColorFromRGBString/*" file="Doc/LJCAppSettings.xml"/>
    /// <parentGroup>settings</parentGroup>
    public static Color GetColorFromRGBString(string rgbText)
    {
      string[] rgb;
      string errorText;
      Color retValue;

      // Check for comma.
      if (!rgbText.Contains(','))
      {
        errorText = "'rgbText' does not contain commas.";
        throw new ArgumentException(errorText);
      }

      // Split and check length.
      rgb = rgbText.Split(',');
      if (rgb.Length != 3)
      {
        errorText = "rgbText does not contain 3 comma separated values.";
        throw new ArgumentException(errorText);
      }
      else
      {
        // Get RGB Color.
        int red = GetColorValue(rgb[0]);
        int green = GetColorValue(rgb[1]);
        int blue = GetColorValue(rgb[2]);
        retValue = Color.FromArgb(red, green, blue);
      }
      return retValue;
    }

    // Gets an integer color value from a string.
    /// <include path="members/GetColorValue/*" file="Doc/LJCAppSettings.xml"/>
    /// <parentGroup>settings</parentGroup>
    public static int GetColorValue(string text)
    {
      string errorText;
      int retValue;

      if (!LJCNetString.IsDigits(text))
      {
        errorText = "Color value does not contain only digits.";
        throw new ArgumentException(errorText);
      }
      else
      {
        _ = int.TryParse(text, out retValue);
        if (retValue < 0 || retValue > 255)
        {
          errorText = "Color value must be 0 to 255.";
          throw new ArgumentOutOfRangeException(errorText);
        }
      }
      return retValue;
    }

    // Get the string value of the specified setting.
    /// <include path="members/GetString/*" file="Doc/LJCAppSettings.xml"/>
    /// <parentGroup>settings</parentGroup>
    public string? GetString(string keyName, bool allowMissingValue = true)
    {
      IEnumerable<XElement> settings = null;
      string errorText = null;
      string retValue = null;

      settings = mSettings.Where(x => x.Attribute("key")?.Value == keyName);
      if (settings != null)
      {
        //if (0 == settings.Count())
        if (!settings.Any())
        {
          if (!allowMissingValue)
          {
            errorText = $"AppSetting 'key={keyName}' not found.";
            throw new MissingMemberException(errorText);
          }
        }
        else
        {
          XAttribute attribute = settings.First().Attribute("value");
          if (null == attribute)
          {
            errorText
              = $"AppSetting 'key={keyName}' Attribute 'value' not found.";
            throw new MissingMemberException(errorText);
          }
          else
          {
            retValue = attribute.Value;
          }
        }
      }
      return retValue;
    }

    // Get the Key Value.
    /// <parentGroup>settings</parentGroup>
    private string GetKeyValue(string keyName)
    {
      string retValue = GetString(keyName);
      if (!LJC.HasValue(retValue))
      {
        string errorText = $"AppSetting 'key={keyName}' not found.";
        throw new MissingMemberException(errorText);
      }
      return retValue;
    }
    #endregion

    #region Class Data

    // The Config File specification.
    /// <include path="members/FileSpec/*" file="Doc/LJCAppSettings.xml"/>
    /// <parentGroup>classData</parentGroup>
    public string FileSpec { get; set; }

    private readonly XElement mRootElement;
    private readonly IEnumerable<XElement> mSettings;
    #endregion
  }
}
