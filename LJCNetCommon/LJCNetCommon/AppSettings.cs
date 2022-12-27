// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace LJCNetCommon
{
  // Represents the Configuration AppSettings.
  /// <include path='items/AppSettings/*' file='Doc/ProjectNetCommon.xml'/>
  public class AppSettings
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/AppSettingsC/*' file='Doc/AppSettings.xml'/>
    public AppSettings(string fileSpec)
    {
      XElement AppSettings;
      string errorText;

      FileSpec = fileSpec;
      if (false == File.Exists(fileSpec))
      {
        string value = Directory.GetCurrentDirectory();
        errorText = $"File '{FileSpec}' was not found.";
        errorText += $"\r\nFolder '{value}'";
        throw new FileNotFoundException(errorText);
      }

      mRootElement = XElement.Load(FileSpec);
      AppSettings = mRootElement.Element("appSettings");

      if (null == AppSettings)
      {
        errorText = $"File '{FileSpec}' 'AppSettings' Element not found.";
        throw new MissingMemberException(errorText);
      }

      mSettings = AppSettings.Elements("add");
      if (0 == mSettings.Count())
      {
        errorText = $"File '{FileSpec}' AppSettings 'add' Elements not found.";
        throw new MissingMemberException(errorText);
      }
    }
    #endregion

    #region Get Setting Types

    // Gets the bool value of the specified setting. (E)
    /// <include path='items/GetBool/*' file='Doc/AppSettings.xml'/>
    public bool GetBool(string keyName)
    {
      bool retValue = false;

      string keyValue = GetKeyValue(keyName);
      if (NetString.HasValue(keyValue))
      {
        retValue = true;
        bool.TryParse(keyValue, out retValue);
      }
      return retValue;
    }

    // Gets the Color value of the specified setting. (E)
    /// <include path='items/GetColor/*' file='Doc/AppSettings.xml'/>
    public Color GetColor(string keyName, Color defaultColor)
    {
      Color retValue = defaultColor;

      if (NetString.HasValue(keyName))
      {
        string keyValue = GetKeyValue(keyName);
        if (NetString.HasValue(keyValue))
        {
          if (keyValue.Contains(","))
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

    // Gets a Color from an RGB string. (E)
    /// <include path='items/GetColorFromRGBString/*' file='Doc/AppSettings.xml'/>
    public Color GetColorFromRGBString(string rgbText)
    {
      string[] rgb;
      string errorText;
      Color retValue;

      // Check for comma.
      if (false == rgbText.Contains(","))
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

    // Gets an integer color value from a string. (E)
    /// <include path='items/GetColorValue/*' file='Doc/AppSettings.xml'/>
    public int GetColorValue(string text)
    {
      string errorText;
      int retValue;

      if (false == NetString.IsDigits(text))
      {
        errorText = "Color value does not contain only digits.";
        throw new ArgumentException(errorText);
      }
      else
      {
        int.TryParse(text, out retValue);
        if (retValue < 0 || retValue > 255)
        {
          errorText = "Color value must be 0 to 255.";
          throw new ArgumentOutOfRangeException(errorText);
        }
      }
      return retValue;
    }

    // Get the string value of the specified setting. (E)
    /// <include path='items/GetString/*' file='Doc/AppSettings.xml'/>
    public string GetString(string keyName, bool allowMissingValue = true)
    {
      IEnumerable<XElement> settings = null;
      string errorText = null;
      string retValue = null;

      settings = mSettings.Where(x => x.Attribute("key").Value == keyName);
      if (settings != null)
      {
        if (0 == settings.Count())
        {
          if (false == allowMissingValue)
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
    #endregion

    #region Private Methods

    // Get the Key Value.
    private string GetKeyValue(string keyName)
    {
      string retValue = GetString(keyName);
      if (false == NetString.HasValue(retValue))
      {
        string errorText = $"AppSetting 'key={keyName}' not found.";
        throw new MissingMemberException(errorText);
      }
      return retValue;
    }
    #endregion

    #region Class Data

    /// <summary>The Config File specification.</summary>
    public string FileSpec { get; set; }

    private readonly XElement mRootElement;
    private readonly IEnumerable<XElement> mSettings;
    #endregion
  }
}
