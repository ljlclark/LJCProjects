// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// NetFileTest.cs
using LJCNetCommon;
using System;
using System.IO;

namespace LJCNetCommonTest
{
  internal class NetFileTest
  {
    internal static void Test()
    {
      TestCommon = new TestCommon("NetFile");
      Console.WriteLine();
      Console.WriteLine("*** NetFile ***");

      CreateFolder();
    }

    // Creates a Folder Path if it does not already exist.
    private static void CreateFolder()
    {
      var folder = "TestFolder";
      var fileSpec = $@"{folder}\File.txt";

      if (Directory.Exists(folder))
      {
        Directory.Delete(folder);
      }

      // Creates folder "SubFolder" from the current folder.
      NetFile.CreateFolder(fileSpec);
      var result = Directory.Exists(folder);
      if (!result)
      {
        TestCommon.Write("CreateFolder()", "true", result.ToString());
      }
    }

    #region Class Data

    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
