// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCCodeTokenizer5.cs
using LJCNetCommon5;
using System.Text;

namespace LJCCSParser5
{
  // A C# Code Tokenizer class.
  /// <include file='Doc/LJCCodeTokenizer.xml'
  ///  path='members/LJCCodeTokenizer/*'/>
  public class LJCCodeTokenizer
  {
    #region Static Methods

    // Check if an XML comment.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsCodeXmlComment/*'/>
    public static bool IsCodeXmlComment(string text)
    {
      bool retValue = false;

      if (text.Trim().StartsWith("///"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if a code comment.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsComment/*'/>
    public static bool IsComment(string text)
    {
      bool retValue = false;

      if (!IsCodeXmlComment(text)
        && text.Trim().StartsWith("//"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Data value.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsDataValue/*'/>
    public static bool IsDataValue(string text)
    {
      double numericValue;
      bool isException = false;
      bool retValue = false;

      try
      {
        numericValue = double.Parse(text);
      }
      catch (SystemException)
      {
        isException = true;
      }
      if (!isException)
      {
        retValue = true;
      }
      if (!retValue)
      {
        if (text.Contains('\"')
          || text.Contains('\''))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text contains a common delimiter.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsDelimiters/*'/>
    public static bool IsDelimiters(string text)
    {
      bool retValue = false;

      if (":{}=\"".IndexOf(text) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a param comment.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsParam/*'/>
    public static bool IsParam(string text)
    {
      bool retValue = false;

      if (text.IndexOf("<param name=") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Strips the leading qualifiers and from next qualifier to end of string.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/StripQualifier/*'/>
    public static string StripQualifier(string text, ref int prefixCount)
    {
      string retValue = text;

      while (true)
      {
        // Static method.
        int index = retValue.IndexOf('.');
        if (index < 0)
        {
          // Array
          index = retValue.IndexOf('[');
        }
        if (index < 0)
        {
          // Generic
          index = retValue.IndexOf('<');
        }
        if (index < 0)
        {
          // Constructor
          index = retValue.IndexOf('(');
        }
        if (index < 0)
        {
          // Terminator
          index = retValue.IndexOf(';');
        }
        if (index < 0)
        {
          // Start array initializer.
          index = retValue.IndexOf('{');
        }
        if (index < 0)
        {
          // Item separator
          index = retValue.IndexOf(',');
        }
        if (index > -1)
        {
          if (0 == index)
          {
            prefixCount++;
            //retValue = retValue.Substring(index + 1);
            retValue = retValue[(index + 1)..];
            retValue = StripQualifier(retValue, ref prefixCount);
          }
          else
          {
            //retValue = retValue.Substring(0, index);
            retValue = retValue[..index];
            break;
          }
        }
        else
        {
          break;
        }
      }
      return retValue;
    }

    // Returns all the combined tokens as a single string.
    private static string CombineTokens(string[] tokens)
    {
      string retValue;

      //StringBuilder builder = new StringBuilder(128);
      StringBuilder builder = new(128);
      foreach (string token in tokens)
      {
        if (null == token)
        {
          break;
        }
        else
        {
          builder.Append($"{token} ");
        }
      }
      retValue = builder.ToString();

      // Remove trailing space.
      if (retValue.Length > 1)
      {
        //retValue = retValue.Substring(0, retValue.Length - 1);
        retValue = retValue[..^1];
      }
      return retValue;
    }
    #endregion

    #region Properties

    // Gets or sets the default parsing begin delimiter.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/BeginDelimiter/*'/>
    public string BeginDelimiter { get; set; } = null!;

    // Gets or sets the most common Data Types.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/CommonDataTypes/*'/>
    public LJCCommonDataTypes CommonDataTypes { get; set; } = null!;

    // Gets or sets the most common Keywords.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/CommonKeywords/*'/>
    public LJCCommonKeywords CommonKeywords { get; set; } = null!;

    // Gets or sets the most common Modifiers.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/CommonModifiers/*'/>
    public LJCCommonModifiers CommonModifiers { get; set; } = null!;

    // Gets or sets the additional Data Types.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/DataTypes/*'/>
    public LJCDataTypes DataTypes { get; set; } = null!;

    // Gets or sets the default parsing end delimiter.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/EndDelimiter/*'/>
    public string EndDelimiter { get; set; } = null!;

    // Gets or sets the additional Keywords.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/Keywords/*'/>
    public LJCKeywords Keywords { get; set; } = null!;

    // Gets or sets the Library Types.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/LibTypes/*'/>
    public LJCLibTypes LibTypes { get; set; } = null!;

    // Gets or sets the additional Modifiers.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/Modifiers/*'/>
    public LJCModifiers Modifiers { get; set; } = null!;

    // Gets or sets the Reference types.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/RefTypes/*'/>
    public LJCRefTypes RefTypes { get; set; } = null!;

    // Gets or sets the token values.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/Tokens/*'/>
    public string[] Tokens { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../LJCUtilities/LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCCodeTokenizer()
    {
      BeginDelimiter = "<";
      EndDelimiter = ">";
    }

    // Initializes the Keywords.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/InitializeKeywords/*'/>
    public void InitializeKeywords()
    {
      CommonDataTypes = [];
      CommonDataTypes.Items = LJCCommonDataTypes.Deserialize();
      CommonKeywords = [];
      CommonKeywords.Items = LJCCommonKeywords.Deserialize();
      CommonModifiers = [];
      CommonModifiers.Items = LJCCommonModifiers.Deserialize();
      DataTypes = [];
      DataTypes.Items = LJCDataTypes.Deserialize();
      Keywords = [];
      Keywords.Items = LJCKeywords.Deserialize();
      LibTypes = [];
      LibTypes.Items = LJCLibTypes.Deserialize();
      Modifiers = [];
      Modifiers.Items = LJCModifiers.Deserialize();
      RefTypes = [];
      RefTypes.Items = LJCRefTypes.Deserialize();
    }

    // Sets the Keywords from the previously initialized CodeTokens.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/SetKeywords/*'/>
    public void SetKeywords(LJCCodeTokenizer setup)
    {
      CommonDataTypes = setup.CommonDataTypes;
      CommonKeywords = setup.CommonKeywords;
      CommonModifiers = setup.CommonModifiers;
      DataTypes = setup.DataTypes;
      Keywords = setup.Keywords;
      LibTypes = setup.LibTypes;
      Modifiers = setup.Modifiers;
      RefTypes = setup.RefTypes;
    }
    #endregion

    #region Methods

    // Clears all the remaining tokens; starting with the specified token index.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/ClearRemainingTokens/*'/>
    public void ClearRemainingTokens(int tokenIndex)
    {
      for (int index = tokenIndex; index < Tokens.Length; index++)
      {
        Tokens[index] = "";
      }
    }

    // Combines the tokens for an XMLComment.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/CombineXmlCommentTokens/*'/>
    public void CombineXmlCommentTokens(short tokenIndex)
    {
      string comment = CombineTokens(Tokens);
      Tokens[tokenIndex] = comment;
      ClearRemainingTokens(tokenIndex + 1);
    }

    // Sets the tokenIndex to the end of the tokens array.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/EndTokens/*'/>
    public void EndTokens(ref short tokenIndex)
    {
      tokenIndex = (short)Tokens.Length;
    }

    // Gets the next token after the specified token index.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/GetNextToken/*'/>
    public string? GetNextToken(ref short tokenIndex)
    {
      string? retValue;

      tokenIndex++;
      retValue = GetToken(tokenIndex);
      return retValue;
    }

    // Gets the token at the specified token index.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/GetToken/*'/>
    public string? GetToken(short tokenIndex)
    {
      string? retValue = null;

      if (tokenIndex >= 0 && Tokens.Length > tokenIndex)
      {
        retValue = Tokens[tokenIndex];
      }
      return retValue;
    }

    // Check if the text has the begin delimiter.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/HasBeginDelimiter/*'/>
    public bool HasBeginDelimiter(string text, string? beginDelimiter = null)
    {
      bool retValue = false;

      if (null == beginDelimiter)
      {
        beginDelimiter = BeginDelimiter;
      }

      if (text.IndexOf(beginDelimiter) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a complete XML comment.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/HasBothDelimiters/*'/>
    public bool HasBothDelimiters(string text, string beginDelimiter
      , string endDelimiter)
    {
      bool retValue = false;

      if (HasBeginDelimiter(text, beginDelimiter)
        && HasEndDelimiter(text, endDelimiter))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text has the end delimiter.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/HasEndDelimiter/*'/>
    public bool HasEndDelimiter(string text, string? endDelimiter = null)
    {
      bool retValue = false;

      if (null == endDelimiter)
      {
        endDelimiter = EndDelimiter;
      }

      if (text.IndexOf(endDelimiter) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a DataType.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsDataType/*'/>
    public bool IsDataType(string text, bool firstPass = false)
    {
      bool retValue = false;

      if (text != null)
      {
        var keyword = CommonDataTypes.SearchName(text);
        if (LJC.HasText(keyword))
        {
          retValue = true;
        }
        else
        {
          if (!firstPass)
          {
            keyword = DataTypes.SearchName(text);
            if (LJC.HasText(keyword))
            {
              retValue = true;
            }
          }
        }
      }
      return retValue;
    }

    // Check if the text is a Keyword.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsKeyword/*'/>
    public bool IsKeyword(string text, bool firstPass = false)
    {
      bool retValue = false;
      if (text != null)
      {
        var keyword = CommonKeywords.SearchName(text);
        if (LJC.HasText(keyword))
        {
          retValue = true;
        }
        else
        {
          if (!firstPass)
          {
            keyword = Keywords.SearchName(text);
            if (LJC.HasText(keyword))
            {
              retValue = true;
            }
          }
        }
      }
      return retValue;
    }

    // Check if the text is a LibType.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsLibType/*'/>
    public bool IsLibType(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        var keyword = LibTypes.SearchName(text);
        if (LJC.HasText(keyword))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text is a Modifier.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsModifier/*'/>
    public bool IsModifier(string text, bool firstPass = false)
    {
      bool retValue = false;
      if (text != null)
      {
        var keyword = CommonModifiers.SearchName(text);
        if (LJC.HasText(keyword))
        {
          retValue = true;
        }
        else
        {
          if (!firstPass)
          {
            keyword = Modifiers.SearchName(text);
            if (LJC.HasText(keyword))
            {
              retValue = true;
            }
          }
        }
      }
      return retValue;
    }

    // Check if the text is a RefType.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/IsRefType/*'/>
    public bool IsRefType(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        var keyword = RefTypes.SearchName(text);
        if (LJC.HasText(keyword))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Sets the Tokens value, split on blanks.
    /// <include file='Doc/LJCCodeTokenizer.xml'
    ///  path='members/SetTokens/*'/>
    public void SetTokens(string text)
    {
      //char[] splitValues = new char[] { ' ' };
      char[] splitValues = [' '];
      Tokens = text.Trim().Split(splitValues
        , StringSplitOptions.RemoveEmptyEntries);
    }
    #endregion
  }
}
