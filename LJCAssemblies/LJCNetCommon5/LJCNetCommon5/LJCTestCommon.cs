// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCTestCommon.cs

namespace LJCNetCommon5
{
  // Provides test methods.
  /// <include file='Doc/LJCTestCommon.xml'
  ///  path='members/LJCTestCommon/*'/>
  public class LJCTestCommon
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/Constructor/*'/>
    public LJCTestCommon()
    {
      ShowNotImplemented = true;
      _ClassName = null;
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

    // Show the result.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/ShowResult/*'/>
    public void Show(string methodName, string? result
      , string compare)
    {
      while (true)
      {
        if (!ShowNotImplemented
          && compare == "Not Implemented")
        {
          break;
        }
        Write($"{methodName}", result, compare);
        break;
      }
    }

    // Writes a compare message to the console.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/Write/*'/>
    public void Write(string methodName, string? result
      , string? compare, bool bracket = false)
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
    public string CompareMessage(string methodName, string? result
      , string? compare, bool bracket = false)
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
        string? bracketChar = null;
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

    #region Properties

    // Gets or sets the show flag.
    /// <include file='Doc/LJCTestCommon.xml'
    ///  path='members/ShowNotImplemented/*'/>
    public bool ShowNotImplemented { get; set; }

    // Gets the class name.
    private readonly string? _ClassName;
    #endregion
  }
}
