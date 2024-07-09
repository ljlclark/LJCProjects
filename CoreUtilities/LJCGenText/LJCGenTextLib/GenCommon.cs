// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenCommon.cs
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenTextLib
{
  /// <summary></summary>
  public static class GenCommon
  {
    /// <summary>Get the template lines from the template file.</summary>
    public static string[] GetTemplateLines(string templateFileSpec
      , out string errorText)
    {
      string[] retValue = null;

      errorText = null;
      if (!NetString.HasValue(templateFileSpec))
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
