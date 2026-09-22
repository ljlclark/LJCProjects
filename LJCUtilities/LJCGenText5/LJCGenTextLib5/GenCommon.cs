// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenCommon.cs
using LJCNetCommon5;
using System;
using System.IO;

namespace LJCGenTextLib5
{
  // 
  /// <include file='Doc/ProjectGenTextLib.xml'
  ///  path='items/GenCommon/*'/>
  public static class GenCommon
  {
    // Get the template lines from the template file.
    /// <include file='Doc/ProjectGenTextLib.xml'
    ///  path='items/GetTemplateLines/*'/>
    public static string[]? GetTemplateLines(string templateFileSpec
      , out string? errorText)
    {
      string[]? retValue = null;

      errorText = null;
      if (!LJC.HasText(templateFileSpec))
      {
        errorText += "Missing Template file specification.\r\n";
      }
      else
      {
        if (!File.Exists(templateFileSpec))
        {
          errorText += $"Template file '{templateFileSpec}' is not found.\r\n";
        }
        else
        {
          retValue = File.ReadAllLines(templateFileSpec);
          if (null == retValue || 0 == retValue.Length)
          {
            errorText += $"No lines found in Template file '{templateFileSpec}'.";
          }
        }
      }
      return retValue;
    }
  }
}
