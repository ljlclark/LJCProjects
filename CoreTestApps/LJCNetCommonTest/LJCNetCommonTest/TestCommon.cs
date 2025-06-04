// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestCommon.cs
using System;

namespace LJCNetCommonTest
{
  internal class TestCommon
  {
    internal TestCommon(string className)
    {
      mClassName = className;
    }

    internal void Write(string methodName, string result
      , string compare)
    {
      if (result != compare)
      {
        Console.WriteLine($"\r\n{mClassName}.{methodName}");
        Console.WriteLine(result);
        Console.WriteLine(" !=");
        Console.WriteLine(compare);
      }
    }

    private string mClassName;
  }
}
