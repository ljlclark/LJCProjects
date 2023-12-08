// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenCommon.cs
using System;
using System.IO;

namespace LJCGenTextLib
{
  internal static class GenCommon
  {
    // Get the template lines from the template file.
    internal static string[] GetTemplateLines(string templateFileSpec
      , out string errorText)
    {
      string[] retValue = null;

      errorText = null;
      if (string.IsNullOrWhiteSpace(templateFileSpec))
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
