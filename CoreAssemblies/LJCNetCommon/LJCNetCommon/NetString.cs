// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// NetString.xml
using System;
using System.Text;

namespace LJCNetCommon
{
  // Contains common string related static functions.
  /// <include path='items/NetString/*' file='Doc/NetString.xml'/>
  public class NetString
  {
    #region Checking String Values

    // Checks if a text value exists.
    /// <include path='items/HasValue/*' file='Doc/NetString.xml'/>
    public static bool HasValue(string text)
    {
      return !string.IsNullOrWhiteSpace(text);
    }

    // Checks a string value for digits.
    /// <include path='items/IsDigits/*' file='Doc/NetString.xml'/>
    public static bool IsDigits(string text)
    {
      string workString;
      bool retValue = true;

      if (false == HasValue(text))
      {
        retValue = false;
      }
      else
      {
        workString = text.Trim();
        foreach (char digit in workString)
        {
          if (false == char.IsDigit(digit))
          {
            retValue = false;
            break;
          }
        }
      }
      return retValue;
    }

    // Do an Ignore Case string compare.
    /// <include path='items/IsEqual/*' file='Doc/NetString.xml'/>
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

    // Gets a column name with underscores converted to Pascal case.
    /// <include path='items/GetPropertyName/*' file='Doc/NetString.xml'/>
    public static string GetPropertyName(string name)
    {
      StringBuilder builder;
      bool makeUpper = false;
      string retVal;

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
      return retVal;
    }

    // Creates an exception string with outer and inner exception.
    /// <include path='items/ExceptionString/*' file='Doc/NetString.xml'/>
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

    // Initializes a string to the trimmed value or null.
    /// <include path='items/InitString/*' file='Doc/NetString.xml'/>
    public static string InitString(string value)
    {
      string retVal = null;

      if (NetString.HasValue(value))
      {
        retVal = value.Trim();
      }
      return retVal;
    }
    #endregion

    #region Parsing Delimited String

    // Get the delimited string begin and end index.
    /// <include path='items/GetDelimitedAndIndexes/*' file='Doc/NetString.xml'/>
    public static string GetDelimitedAndIndexes(string text, string beginDelimiter
      , out int beginIndex, out int endIndex, ref int startIndex
      , string endDelimiter = null)
    {
      string retValue = null;

      beginIndex = -1;
      endIndex = -1;

      if (false == NetString.HasValue(endDelimiter))
      {
        endDelimiter = beginDelimiter;
      }

      if (startIndex > -1
        && startIndex < text.Length - 1
        && text.Contains(beginDelimiter))
      {
        beginIndex = text.IndexOf(beginDelimiter, startIndex);
        if (beginIndex > -1)
        {
          if (endDelimiter.ToLower() == "#nodelimiter")
          {
            // *** Next Statement *** Change- 9/16/22
            //endIndex = text.Length - 1;
            endIndex = text.Length;
          }
          else
          {
            // Start searching at the end of the beginDelimiter.
            endIndex = text.IndexOf(endDelimiter, beginIndex + 1);
          }

          startIndex = -1;
          if (endIndex > -1)
          {
            // Exclude Delimiters
            int beginLength = beginDelimiter.Length;
            int endLength = endDelimiter.Length;
            int startPosition = beginIndex + beginLength;
            int textLength = endIndex - beginIndex - beginLength;
            retValue = text.Substring(startPosition, textLength);

            startIndex = -1;
            if (endIndex < text.Length - (beginLength + endLength))
            {
              // There is enough text to potentially contain another begin
              // and end delimiter.
              startIndex = endIndex + 1;
            }
          }
        }
      }
      return retValue;
    }

    // Gets the string between the specified delimiters.
    /// <include path='items/GetDelimitedString/*' file='Doc/NetString.xml'/>
    public static string GetDelimitedString(string text, string beginDelimiter
      , ref int startIndex, string endDelimiter)
    {
      string retValue;

      // Excludes Delimiters
      retValue = GetDelimitedAndIndexes(text, beginDelimiter, out _
        , out _, ref startIndex, endDelimiter);
      return retValue;
    }

    // Get the string including the specified delimiters.
    /// <include path='items/GetStringWithDelimiters/*' file='Doc/NetString.xml'/>
    public static string GetStringWithDelimiters(string text, string beginDelimiter
      , ref int startIndex, string endDelimiter = null)
    {
      string retValue = null;

      if (false == NetString.HasValue(endDelimiter))
      {
        endDelimiter = beginDelimiter;
      }

      if (GetDelimitedAndIndexes(text, beginDelimiter, out int beginIndex
        , out int endIndex, ref startIndex, endDelimiter) != null)
      {
        // Include Delimiters
        //int beginLength = beginDelimiter.Length;
        int endLength = endDelimiter.Length;
        if (endDelimiter.ToLower() == "#nodelimiter")
        {
          endLength = 0;
        }
        int textLength = endIndex - beginIndex + endLength;
        retValue = text.Substring(beginIndex, textLength);
      }
      return retValue;
    }
    #endregion

    #region Soundex Functions

    // Creates a letter based soundex value. (D)
    /// <include path='items/CreateLSoundex/*' file='Doc/NetString.xml'/>
    public static string CreateLSoundex(string text)
    {
      string retValue;

      StringBuilder builder = new StringBuilder(64);

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
          if (IsSoundexLetter(text[index], text[index - 1]))
          {
            builder.Append(letter);
          }
        }
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Creates a Phonetic based soundex value. (D)
    /// <include path='items/CreatePSoundex/*' file='Doc/NetString.xml'/>
    public static string CreatePSoundex(string text)
    {
      string retValue;

      StringBuilder builder = new StringBuilder(64);

      text = text.ToUpper();
      for (int index = 0; index < text.Length; index++)
      {
        if (Phonetic(text, ref index, out char letter))
        {
          builder.Append(letter);
        }
        else
        {
          if (0 == index)
          {
            builder.Append(letter);
          }
          else
          {
            if (IsSoundexLetter(text[index], text[index - 1]))
            {
              builder.Append(letter);
            }
          }
        }
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Checks if the letter is a soundex skipped letter.
    /// <include path='items/IsSoundexLetter/*' file='Doc/NetString.xml'/>
    public static bool IsSoundexLetter(char letter, char prevLetter)
    {
      bool retValue = false;

      // Do not include vowels.
      if (-1 == "AEIOU".IndexOf(letter))
      {
        // Do not include double consonants.
        if (prevLetter != letter)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Creates a Phonetic character from the supplied text starting at the
    // supplied index. (D)
    /// <include path='items/Phonetic/*' file='Doc/NetString.xml'/>
    public static bool Phonetic(string text, ref int index, out char letter)
    {
      bool retValue = false;

      letter = text[index];

      if (index < text.Length - 1)
      {
        switch (letter)
        {
          case 'P':
            switch (text[index + 1])
            {
              case 'H':
                retValue = true;
                letter = 'F';
                index++;
                break;

              case 'S':
                if (text.Length > index + 2
                 && "AEIOUY".IndexOf(text[index + 2]) > -1)
                {
                  retValue = true;
                  letter = 'S';
                  index += 2;
                }
                break;
            }
            break;

          case 'C':
            switch (text[index + 1])
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

    // Adds the missing argument name to the message.
    /// <summary>
    /// Adds the missing argument name to the message.
    /// </summary>
    /// <param name="message">The message text.</param>
    /// <param name="argument">The argument value.</param>
    public static void AddMissingArgument(string message, string argument)
    {
      if (false == NetString.HasValue(argument))
      {
        message += $"{argument} is missing.\r\n";
      }
    }

    // Throws the invalid argument exception if message has a value.
    /// <summary>
    /// Throws the invalid argument exception if message has a value.
    /// </summary>
    /// <param name="message">The message text.</param>
    public static void ThrowInvalidArgument(string message)
    {
      if (NetString.HasValue(message))
      {
        var argMessage = $"Missing or invalid argument:\r\n{message}";
        throw new ArgumentException(argMessage);
      }
    }
    #endregion
  }
}
