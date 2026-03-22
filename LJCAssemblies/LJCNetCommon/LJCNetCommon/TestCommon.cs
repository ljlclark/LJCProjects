// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestCommon.cs
using System;

namespace LJCNetCommon
{
  /// <summary>
  /// 
  /// </summary>
  public class TestCommon
  {
    #region Constructors

    /// <summary>
    /// Initializes an object instance.
    /// </summary>
    public TestCommon()
    {
    }

    /// <summary>
    /// Initializes an object instance with the supplied values.
    /// </summary>
    /// <param name="className">The class name.</param>
    public TestCommon(string className)
    {
      mClassName = className;
    }
    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="methodName"></param>
    /// <param name="result"></param>
    /// <param name="compare"></param>
    /// <param name="bracket">
    /// Indicates if the values should be bracketed.
    /// </param>
    public void Write(string methodName, string? result
      , string? compare, bool bracket = false)
    {
      var message = CompareMessage(methodName, result, compare, bracket);
      if (LJCNetString.HasValue(message))
      {
        Console.WriteLine(message);
      }
    }

    /// <summary>
    /// Creates a compare message.
    /// </summary>
    /// <param name="methodName">The method name.</param>
    /// <param name="result">The result value.</param>
    /// <param name="compare">The compare value.</param>
    /// <param name="bracket">
    /// Indicates if the values should be bracketed.
    /// </param>
    /// <returns>The compare message.</returns>
    public string CompareMessage(string methodName, string? result
      , string? compare, bool bracket = false)
    {
      var retMessage = "";

      if (!LJCNetString.HasValue(result))
      {
        result = "No Result";
      }
      if (!LJCNetString.HasValue(compare))
      {
        compare = "No Compare";
      }

      if (result != compare)
      {
        string? bracketChar = null;
        if (bracket)
        {
          bracketChar = "|";
        }

        var tb = new TextBuilder();
        tb.Text($"\r\n{mClassName}.{methodName}");
        tb.Text($"{bracketChar}{result}{bracketChar}");
        tb.Text(" !=");
        tb.Text($"{bracketChar}{compare}{bracketChar}");
        retMessage = tb.ToString();
      }
      return retMessage;
    }
    #endregion

    #region Properties

    // The class name.
    private readonly string? mClassName;
    #endregion
  }
}
