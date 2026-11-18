// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCTestCommon.cs
using System;

namespace LJCNetCommon
{
  // Provides test methods.
  /// <include file='Doc/LJCTestCommon.xml'
  ///  path='members/LJCTestCommon/*'/>
  public class LJCTestCommon
  {
    #region Properties

    // The class name.
    private readonly string _ClassName;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/Constructor/*'/>
    public LJCTestCommon()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/ParamConstructor/*'/>
    public LJCTestCommon(string className) : this()
    {
      _ClassName = className;
    }
    #endregion

    #region Methods

    // Writes a compare message to the console.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/Write/*'/>
    public void Write(string methodName, string result
      , string compare, bool bracket = false)
    {
      var message = CompareMessage(methodName, result, compare, bracket);
      if (LJC.HasText(message))
      {
        Console.WriteLine(message);
      }
    }

    // Creates a compare message if the result value does not equal the compare
    // value.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/CompareMessage/*'/>
    public string CompareMessage(string methodName, string result
      , string compare, bool bracket = false)
    {
      var retMessage = "";

      if (!LJC.HasText(result))
      {
        result = "No Result";
      }
      if (!LJC.HasText(compare))
      {
        compare = "No Compare";
      }

      if (result != compare)
      {
        string bracketChar = null;
        if (bracket)
        {
          bracketChar = "|";
        }

        var tb = new LJCTextBuilder();
        tb.Text($"\r\n{_ClassName}.{methodName}");
        tb.Text($"{bracketChar}{result}{bracketChar}");
        tb.Text(" !=");
        tb.Text($"{bracketChar}{compare}{bracketChar}");
        retMessage = tb.ToString();
      }
      return retMessage;
    }
    #endregion
  }
}
