// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// LJCTestCommon.cs

namespace LJCNetCommon5
{
  // Provides test methods.
  /// <include path="members/LJCTestCommon/*" file="Doc/LJCTestCommon.xml"/>
  public class LJCTestCommon
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path="members/Constructor/*" file="Doc/LJCTestCommon.xml"/>
    public LJCTestCommon()
    {
    }

    // Initializes an object instance with the supplied values.
    /// <include path="members/ConstructorWithValues/*" file="Doc/LJCTestCommon.xml"/>
    public LJCTestCommon(string className)
    {
      mClassName = className;
    }
    #endregion

    #region Methods

    // Writes a compare message to the console.
    /// <include path="members/Write/*" file="Doc/LJCTestCommon.xml"/>
    public void Write(string methodName, string? result
      , string? compare, bool bracket = false)
    {
      var message = CompareMessage(methodName, result, compare, bracket);
      if (LJC.HasValue(message))
      {
        Console.WriteLine(message);
      }
    }

    // Creates a compare message if the result value does not equal the compare
    // value.
    /// <include path="members/CompareMessage/*" file="Doc/LJCTestCommon.xml"/>
    public string CompareMessage(string methodName, string? result
      , string? compare, bool bracket = false)
    {
      var retMessage = "";

      if (!LJC.HasValue(result))
      {
        result = "No Result";
      }
      if (!LJC.HasValue(compare))
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
