// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TestNetString5Program.cs
using LJCNetCommon5;

namespace TestNetString5
{
  // The entry class.
  internal class TestNetString5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCNetString");
      Console.WriteLine();
      Console.WriteLine("*** LJCNetString ***");

      // Checking String Values
      HasValue();
      IsDigits();
      IsEqual();

      // Formatting a String
      AddDelimitedValue();
      DelimitValues();
      ExceptionString();
      FormatValue();
      GetPropertyName();
      GetSearchName();
      InitString();
      ScrubDelimitedValues();
      SplitSeparator();
      SplitSeparators();
      Truncate();

      // Soundex Methods
      CreateLSoundex();
      CreatePSoundex();
      Soundex();
      IsSoundexLetter();
      Phonetic();

      // Other Methods
      AddObjectArgError();
      ThrowArgError();

      Console.WriteLine();
      Console.WriteLine("Press any key to continue...");
      Console.ReadKey();
    }

    #region Checking String Values

    // Checks if a text value exists.
    private static void HasValue()
    {
      string? text = null;
      var value = LJC.HasValue(text);
      var result = value.ToString();
      var compare = "False";
      TestCommon?.Write("HasValue()1: text", result, compare);

      text = "  ";
      value = LJC.HasValue(text);
      result = value.ToString();
      compare = "False";
      TestCommon?.Write("HasValue()2: text", result, compare);

      text = " x ";
      value = LJC.HasValue(text);
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("HasValue()3: text", result, compare);
    }

    // Checks a string value for digits.
    private static void IsDigits()
    {
      string? text = null;
      var value = LJCNetString.IsDigits(text);
      var result = value.ToString();
      var compare = "False";
      TestCommon?.Write("IsDigits()1: text", result, compare);

      text = "A123";
      value = LJCNetString.IsDigits(text);
      result = value.ToString();
      compare = "False";
      TestCommon?.Write("IsDigits()2: text", result, compare);

      text = "123";
      value = LJCNetString.IsDigits(text);
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("IsDigits()3: text", result, compare);
    }

    // Do an Ignore Case string compare.
    private static void IsEqual()
    {
      string? stringA = null;
      string? stringB = null;
      var value = LJCNetString.IsEqual(stringA, stringB);
      var result = value.ToString();
      var compare = "True";
      TestCommon?.Write("IsEqual()1: text", result, compare);

      stringA = "Check";
      stringB = null;
      value = LJCNetString.IsEqual(stringA, stringB);
      result = value.ToString();
      compare = "False";
      TestCommon?.Write("IsEqual()2: text", result, compare);

      stringA = "Check";
      stringB = "check";
      value = LJCNetString.IsEqual(stringA, stringB);
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("IsEqual()3: text", result, compare);
    }
    #endregion

    #region Formatting a String

    // Adds a value to a comma delimited string.
    private static void AddDelimitedValue()
    {
      string? text = null;
      LJCNetString.AddDelimitedValue(ref text, "One");
      var result = text;
      var compare = "One";
      TestCommon?.Write("AddDelimitedValue()1: text", result, compare);

      LJCNetString.AddDelimitedValue(ref text, "Two");
      result = text;
      compare = "One, Two";
      TestCommon?.Write("AddDelimitedValue()2: text", result, compare);
    }

    // Adds delimiters to one or more values.
    private static void DelimitValues()
    {
      string? text = null;
      var result = LJCNetString.DelimitValues(text, "+", "-");
      string compare = "No Result";
      TestCommon?.Write("DelimitValues()1: text", result, compare);

      text = "One, Two";
      result = LJCNetString.DelimitValues(text, "+", "-");
      compare = "+One-, +Two-";
      TestCommon?.Write("DelimitValues()2: text", result, compare);

      text = "One-, +Two";
      result = LJCNetString.DelimitValues(text, "+", "-");
      compare = "+One-, +Two-";
      TestCommon?.Write("DelimitValues()3: text", result, compare);
    }

    // Creates an exception string with outer and inner exception.
    private static void ExceptionString()
    {
      var message = "The Main Message";
      var inner = new Exception("The Inner Message");
      var exception = new Exception(message, inner)
      {
        Source = "TestNetStringProgram"
      };
      var result = LJCNetString.ExceptionString(exception);
      var compare = "TestNetStringProgram\r\nThe Main Message\r\n";
      compare += "The Inner Message\r\n";
      TestCommon?.Write("ExeptionString(): exception", result, compare);
    }

    // Formats the column value for the SQL string.
    private static void FormatValue()
    {
      object? value = null;
      var result = LJCNetString.FormatValue(value, LJC.TypeString);
      var compare = "null";
      TestCommon?.Write("FormatValue()1: value", result, compare);

      string? text = null;
      result = LJCNetString.FormatValue(text, LJC.TypeString);
      compare = "null";
      TestCommon?.Write("FormatValue()2: text", result, compare);

      text = "What?";
      result = LJCNetString.FormatValue(text, LJC.TypeString);
      compare = "'What?'";
      TestCommon?.Write("FormatValue()3: text", result, compare);

      bool boolValue = false;
      result = LJCNetString.FormatValue(boolValue, LJC.TypeBoolean);
      compare = "0";
      TestCommon?.Write("FormatValue()4: boolValue", result, compare);

      var dateTime = new DateTime(2000, 1, 1);
      result = LJCNetString.FormatValue(dateTime, LJC.TypeDateTime);
      compare = "'2000/01/01 00:00:00'";
      TestCommon?.Write("FormatValue()5: dateTime", result, compare);

      dateTime = new DateTime(1752, 1, 1);
      result = LJCNetString.FormatValue(dateTime, LJC.TypeDateTime);
      compare = "null";
      TestCommon?.Write("FormatValue()6: dateTime", result, compare);
    }

    // Gets a column name with underscores converted to Pascal case.
    private static void GetPropertyName()
    {
      string? text = null;
      var result = LJCNetString.GetPropertyName(text);
      var compare = "No Result";
      TestCommon?.Write("GetPropertyName()1: text", result, compare);

      text = "makepropertyname";
      result = LJCNetString.GetPropertyName(text);
      compare = "Makepropertyname";
      TestCommon?.Write("GetPropertyName()2: text", result, compare);

      text = "make_property_name";
      result = LJCNetString.GetPropertyName(text);
      compare = "MakePropertyName";
      TestCommon?.Write("GetPropertyName()3: text", result, compare);
    }

    // Gets the Search Property name.
    private static void GetSearchName()
    {
      var text = "TableName.ColumnName";
      var result = LJCNetString.GetSearchName(text);
      var compare = "ColumnName";
      TestCommon?.Write("GetSearchName()1: text", result, compare);

      text = "ColumnName";
      result = LJCNetString.GetSearchName(text);
      compare = "ColumnName";
      TestCommon?.Write("GetSearchName()2: text", result, compare);
    }

    // Initializes a string to the trimmed value or null.
    private static void InitString()
    {
      string? text = null;
      string? result = LJCNetString.InitString(text);
      string compare = "No Result";
      TestCommon?.Write("InitString()1 text", result, compare);

      text = "  ";
      result = LJCNetString.InitString(text);
      compare = "No Result";
      TestCommon?.Write("InitString()2 text", result, compare);

      text = " What? ";
      result = LJCNetString.InitString(text);
      compare = "What?";
      TestCommon?.Write("InitString()3 text", result, compare);
    }

    // Scrubs extra blanks from the comma delimited string.
    private static void ScrubDelimitedValues()
    {
      string? text = null;
      string result = LJCNetString.ScrubDelimitedValues(text);
      string compare = "No Result";
      TestCommon?.Write("ScrubDelimitedValues()1 text", result, compare);

      text = "  One , Two ";
      result = LJCNetString.ScrubDelimitedValues(text);
      compare = "One, Two";
      TestCommon?.Write("ScrubDelimitedValues()2 text", result, compare);

      text = "  One  Two ";
      result = LJCNetString.ScrubDelimitedValues(text);
      compare = "One  Two";
      TestCommon?.Write("ScrubDelimitedValues()3 text", result, compare);
    }

    // Split a string on a single separator without empty entries.
    private static void SplitSeparator()
    {
      string? text = null;
      string[]? values = LJCNetString.Split(text);
      string? result = null;
      if (values != null)
      {
        result = string.Concat(values);
      }
      string compare = "No Result";
      TestCommon?.Write("SplitSeparator()1 text", result, compare);

      text = " One , Two ";
      values = LJCNetString.Split(text);
      result = null;
      if (values != null)
      {
        result = string.Concat(values);
      }
      compare = " One  Two ";
      TestCommon?.Write("SplitSeparator()2 text", result, compare);

      text = "One| Two";
      values = LJCNetString.Split(text, "|");
      result = null;
      if (values != null)
      {
        result = string.Concat(values);
      }
      compare = "One Two";
      TestCommon?.Write("SplitSeparator()3 text", result, compare);
    }

    // Split a string on multiple separators without empty entries.
    private static void SplitSeparators()
    {
      string? text = null;
      string[] separators = [",", "|"];
      string[]? values = LJCNetString.Split(text, separators);
      string? result = null;
      if (values != null)
      {
        result = string.Concat(values);
      }
      string compare = "No Result";
      TestCommon?.Write("SplitSeparators()1 text", result, compare);

      text = " One , Two | Three";
      values = LJCNetString.Split(text, separators);
      result = null;
      if (values != null)
      {
        result = string.Concat(values);
      }
      compare = " One  Two  Three";
      TestCommon?.Write("SplitSeparators()2 text", result, compare);
    }

    // Truncates a text string to the specified length.
    private static void Truncate()
    {
      string? text = null;
      string? result = LJCNetString.Truncate(text, 2);
      string compare = "No Result";
      TestCommon?.Write("Truncate()1 text", result, compare);

      text = "Test";
      result = LJCNetString.Truncate(text, 5);
      compare = "Test";
      TestCommon?.Write("Truncate()2 text", result, compare);

      text = "Test";
      result = LJCNetString.Truncate(text, 3);
      compare = "Tes";
      TestCommon?.Write("Truncate()3 text", result, compare);
    }
    #endregion

    #region Soundex Methods

    // Creates a letter based soundex value.
    private static void CreateLSoundex()
    {
      string? text = null;
      string? result = LJCNetString.CreateLSoundex(text);
      string compare = "No Result";
      TestCommon?.Write("CreateLSoundex()1 text", result, compare);

      text = "Rattan";
      result = LJCNetString.CreateLSoundex(text);
      compare = "RTN";
      TestCommon?.Write("CreateLSoundex()2 text", result, compare);
    }

    // Creates a Phonetic based soundex value.
    private static void CreatePSoundex()
    {
      string? text = null;
      string? result = LJCNetString.CreatePSoundex(text);
      string compare = "No Result";
      TestCommon?.Write("CreatePSoundex()1 text", result, compare);

      text = "Psychology";
      result = LJCNetString.CreatePSoundex(text);
      compare = "SCHLGY";
      TestCommon?.Write("CreatePSoundex()2 text", result, compare);

      text = "Carol";
      result = LJCNetString.CreatePSoundex(text);
      compare = "KRL";
      TestCommon?.Write("CreatePSoundex()3 text", result, compare);

      text = "Ceiling";
      result = LJCNetString.CreatePSoundex(text);
      compare = "SLNG";
      TestCommon?.Write("CreatePSoundex()3 text", result, compare);
    }

    // Creates a US Census soundex value.
    private static void Soundex()
    {
      var text = "Clark";
      var result = LJCNetString.Soundex(text);
      var compare = "C462";
      TestCommon?.Write("Soundex()1 text", result, compare);

      text = "Klum";
      result = LJCNetString.Soundex(text);
      compare = "K450";
      TestCommon?.Write("Soundex()2 text", result, compare);

      text = "Rodriguez";
      result = LJCNetString.Soundex(text);
      compare = "R362";
      TestCommon?.Write("Soundex()3 text", result, compare);
    }

    // Checks if the letter is a soundex skipped letter.
    private static void IsSoundexLetter()
    {
      // Blanks are not included.
      string text = " ";
      int index = 0;
      bool value = LJCNetString.IsSoundexLetter(text, index);
      string result = value.ToString();
      string compare = "False";
      TestCommon?.Write("IsSoundexLetter()1 text", result, compare);

      text = "tt";
      index = 1;
      value = LJCNetString.IsSoundexLetter(text, index);
      result = value.ToString();
      compare = "False";
      TestCommon?.Write("IsSoundexLetter()2 text", result, compare);

      text = "st";
      index = 1;
      value = LJCNetString.IsSoundexLetter(text, index);
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("IsSoundexLetter()3 text", result, compare);

      text = "a";
      index = 0;
      value = LJCNetString.IsSoundexLetter(text, index);
      result = value.ToString();
      compare = "False";
      TestCommon?.Write("IsSoundexLetter()4 text", result, compare);
    }

    // Creates a Phonetic character from the supplied text starting at the
    private static void Phonetic()
    {
      // If text is null.
      string? text = null;
      int index = 0;
      bool value = LJCNetString.Phonetic(text, ref index, out char? letter);
      // Letter is not converted.
      string? result = value.ToString();
      string compare = "False";
      TestCommon?.Write("Phonetic()1 text", result, compare);
      // Sequence is NOT converted.
      result = letter.ToString();
      compare = "No Result";
      TestCommon?.Write("Phonetic()2 text", result, compare);

      // Sequence is "PS" + Vowel "AEIOUY".
      text = "Psychology";
      index = 0;
      value = LJCNetString.Phonetic(text, ref index, out letter);
      // Sequence is converted.
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("Phonetic()3 text", result, compare);
      // Phonetic letter is "S".
      result = letter.ToString();
      compare = "S";
      TestCommon?.Write("Phonetic()4 text", result, compare);

      // Sequence is "C" + Vowel "AOU".
      text = "Carol";
      index = 0;
      value = LJCNetString.Phonetic(text, ref index, out letter);
      // Sequence is converted.
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("Phonetic()5 text", result, compare);
      // Phonetic letter is "K".
      result = letter.ToString();
      compare = "K";
      TestCommon?.Write("Phonetic()6 text", result, compare);

      text = "Ceiling";
      index = 0;
      value = LJCNetString.Phonetic(text, ref index, out letter);
      // Sequence is converted.
      result = value.ToString();
      compare = "True";
      TestCommon?.Write("Phonetic()7 text", result, compare);
      // Phonetic letter is "S".
      result = letter.ToString();
      compare = "S";
      TestCommon?.Write("Phonetic()8 text", result, compare);
    }
    #endregion

    #region Other Methods

    // Adds the missing argument name to the message.
    private static void AddObjectArgError()
    {
      string message = "";
      object? arg = null;
      string name = nameof(arg);
      string errorContext = "AddObjectArgError()";
      LJCNetString.AddObjectArgError(ref message, arg, name, errorContext);
      string result = message;
      string compare = "AddObjectArgError()\r\narg is missing.\r\n";
      TestCommon?.Write("AddObjectArgError()1 message", result, compare);

      message = "";
      arg = "  ";
      name = nameof(arg);
      LJCNetString.AddObjectArgError(ref message, arg, name, errorContext);
      result = message;
      compare = "AddObjectArgError()\r\narg is missing.\r\n";
      TestCommon?.Write("AddObjectArgError()2 message", result, compare);

      message = "";
      arg = " Now What?  ";
      name = nameof(arg);
      LJCNetString.AddObjectArgError(ref message, arg, name, errorContext);
      result = message;
      compare = "No Result";
      TestCommon?.Write("AddObjectArgError()3 message", result, compare);
    }

    // Throws an ArgumentException if the provided message has a value.
    private static void ThrowArgError()
    {
      string message = "";
      object? arg = null;
      string name = nameof(arg);
      string errorContext = "ThrowArgError()";
      LJCNetString.AddObjectArgError(ref message, arg, name, errorContext);
      string result = $"Missing or invalid arguments:\r\n{message.Trim()}";
      string compare = "Missing or invalid arguments:\r\n";
      compare += "ThrowArgError()\r\n";
      compare += "arg is missing.";
      TestCommon?.Write("ThrowArgError() message", result, compare);
    }
    #endregion

    #region Class Data

    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
