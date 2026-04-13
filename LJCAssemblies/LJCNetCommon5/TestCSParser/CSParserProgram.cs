// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CSParserProgram.cs
using LJCNetCommon5;
using System.ComponentModel;
using System.Diagnostics;

namespace TestCSParser5
{
  // The entry class.
  internal class CSParserProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCCSParser");
      Console.WriteLine();
      Console.WriteLine("*** LJCCSParser ***");

      // Methods
      ClassName();
      MethodName();
      PropertyName();
    }

    #region Methods

    // Attempts to parse a class name.
    private static void ClassName()
    {
      var classList = new List<string>
      {
        "class ClassName",
        // One Modifier
        "internal class ClassName",
        "private class ClassName",
        "protected class ClassName",
        "public class ClassName",
        // Two Modifiers
        "private abstract class ClassName", // nested
        "private protected class ClassName", // nested
        "private sealed class ClassName", // nested
        "private static class ClassName", // nested
        "public abstract class ClassName",
        "public sealed class ClassName",
        "public static class ClassName",
      };

      var csParser = new LJCCSParser();
      for (int index = 0; index < classList.Count; index++)
      {
        var line = classList[index];
        var result = csParser.ClassName(line);
        var compare = "ClassName";
        TestCommon?.Write($"ClassName(){index}", result, compare);
      }
    }

    // Attempts to parse a method name.
    private static void MethodName()
    {
      var methodList = new List<string>
      {
        "void MethodName()",
        // One Modifier
        "internal void MethodName()",
        "private void MethodName()",
        "protected void MethodName()",
        "public void MethodName()",
        "static void MethodName()",
        // Two Modifiers
        "private protected void MethodName()",
        "private static void MethodName()",
        "protected internal void MethodName()",
        "public abstract void MethodName()",
        "public override void MethodName()",
        "public protected void MethodName()",
        "public static void MethodName()",
        "public virtual void MethodName()",
        // Three Modifiers
        "public override sealed void MethodName()",
      };

      var csParser = new LJCCSParser();
      for (int index = 0; index < methodList.Count; index++)
      {
        var line = methodList[index];
        var result = csParser.MethodName(line);
        var compare = "MethodName";
        TestCommon?.Write($"MethodName(){index}", result, compare);
      }
    }

    // Attempts to parse a property name.
    private static void PropertyName()
    {
      var propertyList = new List<string>
      {
        "void Property {",
        "void Property",
        "{",
        // One Modifier
        "internal void Property {",
        "internal void Property",
        "{",
        "private void Property {",
        "protected void Property {",
        "public void Property {",
        "static void Property {",
      };

      var csParser = new LJCCSParser();
      for (int index = 0; index < propertyList.Count; index++)
      {
        var line = propertyList[index];
        if (line != "{")
        {
          string nextLine = null;
          if (index < propertyList.Count - 1)
          {
            nextLine = propertyList[index + 1];
          }
          var result = csParser.PropertyName(line, nextLine);
          var compare = "Property";
          TestCommon?.Write($"PropertyName(){index}", result, compare);
        }
      }
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
