// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTokenizer.cs
using System;
using System.Collections.Generic;
using System.Text;

namespace LJCNetCommon5
{
  // A C# Code Tokenizer class.
  /// <include file='Doc/ProjectCodeTokenizer.xml'
  ///  path='members/CodeTokenizer/*'/>
  public class CodeTokenizer
  {
    #region Properties

    // 
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/BeginDelimiter/*'/>
    public string BeginDelimiter { get; set; } = null!;

    // The most common Data Types.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/CommonDataTypes/*'/>
    public CommonDataTypes CommonDataTypes { get; set; } = null!;

    // The most common Keywords.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/CommonKeywords/*'/>
    public CommonKeywords CommonKeywords { get; set; } = null!;

    // The most common Modifiers.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/CommonModifiers/*'/>
    public CommonModifiers CommonModifiers { get; set; } = null!;

    // The additional Data Types.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/DataTypes/*'/>
    public DataTypes DataTypes { get; set; } = null!;

    // 
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/EndDelimiter/*'/>
    public string EndDelimiter { get; set; } = null!;

    // The additional Keywords.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/Keywords/*'/>
    public Keywords Keywords { get; set; } = null!;

    // The Library Types.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/LibTypes/*'/>
    public LibTypes LibTypes { get; set; } = null!;

    // The additional Modifiers.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/Modifiers/*'/>
    public Modifiers Modifiers { get; set; } = null!;

    // The Reference types.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/RefTypes/*'/>
    public RefTypes RefTypes { get; set; } = null!;

    // Gets or sets the Token values.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/Tokens/*'/>
    public string[] Tokens { get; set; } = null!;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/DefaultConstructor/*'/>
    public CodeTokenizer()
    {
      BeginDelimiter = "<";
      EndDelimiter = ">";
    }

    // Initializes the Keywords.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/InitializeKeywords/*'/>
    public void InitializeKeywords()
    {
      CommonDataTypes = [];
      CommonDataTypes.Items = CommonDataTypes.Deserialize();
      CommonKeywords = [];
      CommonKeywords.Items = CommonKeywords.Deserialize();
      CommonModifiers = [];
      CommonModifiers.Items = CommonModifiers.Deserialize();
      DataTypes = [];
      DataTypes.Items = DataTypes.Deserialize();
      Keywords = [];
      Keywords.Items = Keywords.Deserialize();
      LibTypes = [];
      LibTypes.Items = LibTypes.Deserialize();
      Modifiers = [];
      Modifiers.Items = Modifiers.Deserialize();
      RefTypes = [];
      RefTypes.Items = RefTypes.Deserialize();
    }

    // Sets the Keywords from the previously initialized CodeTokens.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/SetKeywords/*'/>
    public void SetKeywords(CodeTokenizer setup)
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

    #region Public Methods

    // Clears all the remaining tokens; starting with the specified token index.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/ClearRemainingTokens/*'/>
    public void ClearRemainingTokens(int tokenIndex)
    {
      for (int index = tokenIndex; index < Tokens.Length; index++)
      {
        // *** Next Statement *** Change
        Tokens[index] = "";
      }
    }

    // Combines the tokens for an XMLComment.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/CombineXmlCommentTokens/*'/>
    public void CombineXmlCommentTokens(short tokenIndex)
    {
      //string comment = CombineTokens(Tokens, tokenIndex);
      string comment = CombineTokens(Tokens);
      Tokens[tokenIndex] = comment;
      ClearRemainingTokens(tokenIndex + 1);
    }

    // Sets the tokenIndex to the end of the tokens array.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/EndTokens/*'/>
    public void EndTokens(ref short tokenIndex)
    {
      tokenIndex = (short)Tokens.Length;
    }

    // Gets the next token after the specified token index.(E)
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/GetNextToken/*'/>
    public string? GetNextToken(ref short tokenIndex)
    {
      string retValue;

      tokenIndex++;
      retValue = GetToken(tokenIndex);
      return retValue;
    }

    // Gets the token at the specified token index.
    /// <include file='Doc/CodeTokenizer.xml'
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
    /// <include file='Doc/CodeTokenizer.xml'
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

    // Check if the text is a Full summary comment.
    /// <include file='Doc/CodeTokenizer.xml'
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
    /// <include file='Doc/CodeTokenizer.xml'
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

    // Check if a XML comment.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/IsCodeXmlComment/*'/>
    public bool IsCodeXmlComment(string text)
    {
      bool retValue = false;

      if (text.Trim().StartsWith("///"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if a code comment.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/IsComment/*'/>
    public bool IsComment(string text)
    {
      bool retValue = false;

      if (!IsCodeXmlComment(text)
        && text.Trim().StartsWith("//"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a DataType.
    /// <include file='Doc/CodeTokenizer.xml'
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

    // Check if the text is a Data value.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/IsDataValue/*'/>
    public bool IsDataValue(string text)
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
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/IsDelimiters/*'/>
    public bool IsDelimiters(string text)
    {
      bool retValue = false;

      if (":{}=\"".IndexOf(text) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Keyword.
    /// <include file='Doc/CodeTokenizer.xml'
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
    /// <include file='Doc/CodeTokenizer.xml'
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
    /// <include file='Doc/CodeTokenizer.xml'
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

    // Check if the text is a param comment.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/IsParam/*'/>
    public bool IsParam(string text)
    {
      bool retValue = false;

      if (text.IndexOf("<param name=") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a RefType.
    /// <include file='Doc/CodeTokenizer.xml'
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
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/SetTokens/*'/>
    public void SetTokens(string text)
    {
      //char[] splitValues = new char[] { ' ' };
      char[] splitValues = [' '];
      Tokens = text.Trim().Split(splitValues
        , StringSplitOptions.RemoveEmptyEntries);
    }

    // Strips the leading qualifiers and from next qualifier to end of string.
    /// <include file='Doc/CodeTokenizer.xml'
    ///  path='members/StripQualifier/*'/>
    public string StripQualifier(string text, ref int prefixCount)
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
    #endregion

    #region Private Methods

    // Returns all the combined tokens as a single string.
    //private string CombineTokens(string[] tokens, short startTokenIndex)
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
  }
}
