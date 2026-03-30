// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// NetString.cs
using System;
using System.Text;

namespace LJCNetCommon
{
  // Contains common string related static methods.
  /// <include path="members/NetString/*" file="Doc/NetString.xml"/>
  public class NetString
  {
    #region Checking String Values

    // Checks if a text value exists.
    /// <include path="members/HasValue/*" file="Doc/NetString.xml"/>
    public static bool HasValue(string text)
    {
      return !string.IsNullOrWhiteSpace(text);
    }

    // Checks a string value for digits.
    /// <include path="members/IsDigits/*" file="Doc/NetString.xml"/>
    public static bool IsDigits(string text)
    {
      string textTrim;
      bool retValue = true;

      if (!HasValue(text))
      {
        retValue = false;
      }
      else
      {
        textTrim = text.Trim();
        foreach (char digit in textTrim)
        {
          if (!char.IsDigit(digit))
          {
            retValue = false;
            break;
          }
        }
      }
      return retValue;
    }

    // Do an Ignore Case string compare.
    /// <include path="members/IsEqual/*" file="Doc/NetString.xml"/>
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
    #endregion

    #region Formatting a String

    // Adds a value to a comma delimited string.
    /// <include path="members/AddDelimitedValue/*" file="Doc/NetString.xml"/>
    public static void AddDelimitedValue(ref string target, string value)
    {
      if (HasValue(target))
      {
        target += ", ";
      }
      if (value != null)
      {
        value = value.Trim();
      }
      target += value;
    }

    // Adds delimiters to one or more values.
    /// <include path="members/DelimitValues/*" file="Doc/NetString.xml"/>
    public static string DelimitValues(string values, string beginDelimiter
      , string endDelimiter)
    {
      string retName = "";

      var names = Split(values, ",");
      if (NetCommon.HasItems(names))
      {
        foreach (var name in names)
        {
          if (HasValue(retName))
          {
            retName += ", ";
          }
          var tempName = name.Trim();
          if (!tempName.StartsWith(beginDelimiter))
          {
            retName += beginDelimiter;
          }
          retName += tempName;
          if (!tempName.EndsWith(endDelimiter))
          {
            retName += endDelimiter;
          }
        }
      }
      return retName;
    }

    // Creates an exception string with outer and inner exception.
    /// <include path="members/ExceptionString/*" file="Doc/NetString.xml"/>
    public static string ExceptionString(Exception e)
    {
      string retValue;

      retValue = $"{e.Source}\r\n{e.Message}\r\n";
      if (null != e.InnerException)
      {
        retValue += $"{e.InnerException.Message}\r\n";
      }
      return retValue;
    }

    // Formats the column value for the SQL string.
    /// <include path="members/FormatValue/*" file="Doc/NetString.xml"/>
    public static string FormatValue(object value, string dataTypeName)
    {
      string retValue = "null";

      if (value != null)
      {
        retValue = value.ToString();

        switch (dataTypeName)
        {
          case NetCommon.TypeBoolean:
            if (IsEqual(retValue.ToLower(), "true"))
            {
              retValue = "1";
            }
            if (IsEqual(retValue.ToLower(), "false"))
            {
              retValue = "0";
            }
            break;

          case NetCommon.TypeDateTime:
            DateTime dateTime = Convert.ToDateTime(retValue);
            if (NetCommon.IsDbMinDate(dateTime))
            {
              retValue = "null";
            }
            else
            {
              retValue = $"'{dateTime:yyyy/MM/dd HH:mm:ss}'";
            }
            break;

          case NetCommon.TypeString:
            if (retValue != null
              && retValue != "null")
            {
              retValue = retValue.Replace("'", "''");
              retValue = $"'{retValue}'";
            }
            break;
        }
      }
      return retValue;
    }

    // Gets a column name with underscores converted to Pascal case.
    /// <include path="members/GetPropertyName/*" file="Doc/NetString.xml"/>
    public static string GetPropertyName(string name)
    {
      StringBuilder builder;
      bool makeUpper = false;
      string retVal = null;

      if (HasValue(name))
      {
        builder = new StringBuilder(64);
        foreach (char ch in name)
        {
          if (builder.Length == 0
            || makeUpper)
          {
            makeUpper = false;
            builder.Append(char.ToUpper(ch));
          }
          else
          {
            makeUpper = false;
            if (ch == '_')
            {
              makeUpper = true;
            }
            else
            {
              builder.Append(ch);
            }
          }
        }
        retVal = builder.ToString();
      }
      return retVal;
    }

    // Gets the Search Property name.
    /// <include path="members/GetSearchName/*" file="Doc/NetString.xml"/>
    public static string GetSearchName(string columnName)
    {
      var retValue = columnName;

      var index = columnName.IndexOf(".");
      if (index > -1)
      {
        // Get property name from qualified name.
        retValue = columnName.Substring(index + 1);
      }
      return retValue;
    }

    // Initializes a string to the trimmed value or null.
    /// <include path="members/InitString/*" file="Doc/NetString.xml"/>
    public static string InitString(string value)
    {
      string retVal = null;

      if (HasValue(value))
      {
        retVal = value.Trim();
      }
      return retVal;
    }

    // Scrubs extra blanks from the comma delimited string.
    /// <include path="members/ScrubDelimitedValues/*" file="Doc/NetString.xml"/>
    public static string ScrubDelimitedValues(string values)
    {
      string retValues = "";

      if (HasValue(values))
      {
        if (!values.Contains(","))
        {
          retValues = values.Trim();
        }
        else
        {
          string[] items = Split(values, ",");
          if (NetCommon.HasElements(items))
          {
            foreach (string item in items)
            {
              if (NetString.HasValue(retValues))
              {
                retValues += ", ";
              }
              retValues += item.Trim();
            }
          }
        }
      }
      return retValues;
    }

    // Split a string without empty entries.
    /// <include path="members/Split/*" file="Doc/NetString.xml"/>
    public static string[] Split(string text, string separator = ",")
    {
      string[] retValues = null;

      if (HasValue(text))
      {
        var separators = new string[] { separator };
        retValues = text.Split(separators
          , StringSplitOptions.RemoveEmptyEntries);
      }
      return retValues;
    }

    // Split a string on multiple separators without empty entries.
    /// <include path="members/Split2/*" file="Doc/NetString.xml"/>
    public static string[] Split(string text, string[] separators)
    {
      string[] retValues = null;

      if (HasValue(text))
      {
        retValues = text.Split(separators
          , StringSplitOptions.RemoveEmptyEntries);
      }
      return retValues;
    }

    // Truncates a text string to the specified length.
    /// <include path="members/Truncate/*" file="Doc/NetString.xml"/>
    public static string Truncate(string text, int length)
    {
      var retValue = text;

      if (HasValue(text)
        && text.Length > length)
      {
        retValue = text.Substring(0, length);
      }
      return retValue;
    }
    #endregion

    #region Soundex Methods

    // Creates a letter based soundex value.
    /// <include path="members/CreateLSoundex/*" file="Doc/NetString.xml"/>
    public static string CreateLSoundex(string text)
    {
      string retValue = null;

      if (HasValue(text))
      {
        var builder = new StringBuilder(64);

        text = text.ToUpper();
        for (int index = 0; index < text.Length; index++)
        {
          char letter = text[index];

          if (0 == index)
          {
            builder.Append(letter);
          }
          else
          {
            if (IsSoundexLetter(text, index))
            {
              builder.Append(letter);
            }
          }
        }
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Creates a Phonetic based soundex value.
    /// <include path="members/CreatePSoundex/*" file="Doc/NetString.xml"/>
    public static string CreatePSoundex(string text)
    {
      string retValue = null;

      if (HasValue(text))
      {
        var builder = new StringBuilder(64);

        text = text.ToUpper();
        for (int index = 0; index < text.Length; index++)
        {
          char letter = text[index];
          if (Phonetic(text, ref index, out char? phonetic))
          {
            builder.Append(phonetic);
          }
          else
          {
            if (0 == index)
            {
              builder.Append(letter);
            }
            else
            {
              if (IsSoundexLetter(text, index))
              {
                builder.Append(letter);
              }
            }
          }
        }
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Checks if the letter is a soundex skipped letter.
    /// <include path="members/IsSoundexLetter/*" file="Doc/NetString.xml"/>
    public static bool IsSoundexLetter(string text, int index)
    {
      bool retValue = false;

      if (HasValue(text)
        && index < text.Length)
      {
        text = text.ToUpper();
        string currentChar = text[index].ToString();
        string prevChar = null;
        if (index > 0)
        {
          prevChar = text[index - 1].ToString();
        }

        // Do not include blank or vowels.
        if (!" AEIOU".Contains(currentChar))
        {
          // Do not include double consonants.
          if (prevChar != null
            && prevChar != currentChar)
          {
            retValue = true;
          }
        }
      }
      return retValue;
    }

    // Creates a Phonetic character from the supplied text starting at the
    // supplied index.
    /// <include path="members/Phonetic/*" file="Doc/NetString.xml"/>
    public static bool Phonetic(string text, ref int index, out char? letter)
    {
      bool retValue = false;

      letter = null;

      // Index is in text and a next char is available.
      if (HasValue(text)
        && index < text.Length - 1)
      {
        text = text.ToUpper();
        char currentChar = text[index];
        char nextChar = text[index + 1];
        switch (currentChar)
        {
          case 'P':
            switch (nextChar)
            {
              case 'H':
                retValue = true;
                letter = 'F';
                index++;
                break;

              case 'S':
                if (text.Length > index + 2)
                {
                  var secondChar = text[index + 2].ToString();
                  if ("AEIOUY".Contains(secondChar))
                  {
                    retValue = true;
                    letter = 'S';
                    index += 2;
                  }
                }
                break;
            }
            break;

          case 'C':
            switch (nextChar)
            {
              case 'A':
              case 'O':
              case 'U':
                retValue = true;
                letter = 'K';
                index++;
                break;

              case 'E':
              case 'I':
              case 'Y':
                retValue = true;
                letter = 'S';
                index++;
                break;
            }
            break;
        }
      }
      return retValue;
    }
    #endregion

    #region Other Functions

    /// <summary>Adds the missing argument name to the message.</summary>
    /// <include path="members/AddObjectArgError/*" file="Doc/NetString.xml"/>
    public static void AddObjectArgError(ref string message, object argument
      , string name = null, string errorContext = null)
    {
      bool missing = false;
      if (null == argument)
      {
        missing = true;
      }
      else
      {
        if (typeof(string) == argument.GetType())
        {
          if (!NetString.HasValue(argument.ToString()))
          {
            missing = true;
          }
        }
      }

      if (missing)
      {
        if (NetString.HasValue(errorContext))
        {
          message += $"{errorContext}\r\n";
        }
        if (!NetString.HasValue(name))
        {
          name = "argument";
        }
        message += $"{name} is missing.\r\n";
      }
    }

    // Throws an ArgumentException if the provided message has a value.
    /// <include path="members/ThrowArgError/*" file="Doc/NetString.xml"/>
    public static void ThrowArgError(string message)
    {
      if (HasValue(message))
      {
        message = message.Trim();
        var argMessage = $"Missing or invalid arguments:\r\n{message}";
        throw new ArgumentException(argMessage.ToString());
      }
    }
    #endregion

    #region Constants

    /// <summary>The compare object is equal to the compareto ojbect..</summary>
    public const int CompareEqual = 0;

    /// <summary>
    /// The compare pbkect is greater than the compareto object.
    /// </summary>
    public const int CompareGreater = 1;

    /// <summary>
    /// The compare object  is less than the compareto object.
    /// </summary>
    public const int CompareLess = -1;

    /// <summary>
    /// The compare object is notnull or equal is equal to the compareto object.
    /// </summary>
    public const int CompareNotNullOrEqual = -2;
    #endregion
  }
}
