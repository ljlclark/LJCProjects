// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestArgs.cs
using System;

namespace LJCCodeLineCounter
{
  // Provides test arguments.
  /// <include path = 'items/TestArgs/*' file='Doc/TestArgs.xml'/>
  public static class TestArgs
  {
    // Get the test argument.
    /// <include path = 'items/GetTestArgs/*' file='Doc/TestArgs.xml'/>
    public static string[] GetTestArgs(bool isHtml = false)
    {
      string[] retValue;

      if (false == isHtml)
      {
        retValue = new string[3];
        //args[0] = @"..\..\..\..\LJC.AppManager";
        retValue[0] = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev";
        retValue[1] = "*.cs";
        //retValue[2] = "SetGridColumns";
      }
      else
      {
        retValue = new string[2];
        //retValue[0] = @"..\..\..\..\LJC.DocLib\LJC.DocGen\bin\Debug\Doc";
        retValue[0] = @"C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\Doc";
        retValue[1] = "*.html";
      }
      return retValue;
    }
  }
}
