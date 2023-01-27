// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeCommon.cs
using System;
using System.IO;

namespace LJCCodeLineCounter
{
  /// <summary>Provides common static CodeLineCounter methods.</summary>
  public class CodeCommon
  {
    // Checks for a valid source folder.
    /// <include path='items/IsValidFolder/*' file='Doc/CodeCommon.xml'/>
    public static bool IsValidFolder(string folderName)
    {
      bool retValue = true;

      if (IsEqual(folderName, ".vs")
        || IsEqual(folderName, "Properties")
        || IsEqual(folderName, "bin")
        || IsEqual(folderName, "obj")
        || IsEqual(folderName, "External")
        || IsEqual(folderName, "Help")
        || IsEqual(folderName, "Resources")
        || IsEqual(folderName, "Connected Services"))
      {
        retValue = false;
      }
      return retValue;
    }

    // Checks for a valid file.
    /// <include path='items/IsValidFile/*' file='Doc/CodeCommon.xml'/>
    public static bool IsValidFile(string fileSpec)
    {
      bool retValue = true;

      if (fileSpec.ToLower().Contains(".designer"))
      {
        retValue = false;
      }
      return retValue;
    }

    // Creates a backup file.
    /// <include path='items/CreateBackup/*' file='Doc/CodeCommon.xml'/>
    public static string CreateBackup(string filePath)
    {
      string retValue;
      retValue = filePath += ".bak";
      File.Move(filePath, retValue);
      return retValue;
    }

    // Do an Ignore Case string compare.
    /// <include path='items/IsEqual/*' file='Doc/CodeCommon.xml'/>
    public static bool IsEqual(string stringA, string stringB)
    {
      bool retValue = false;

      if (stringA != null)
      {
        retValue = stringA.Equals(stringB
          , System.StringComparison.InvariantCultureIgnoreCase);
      }
      return retValue;
    }
  }
}
