// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CodeTokenizer.cs
using System;
using System.Collections.Generic;
using System.Text;

namespace LJCNetCommon
{
  // A C# Code Tokenizer class.
  /// <include path='items/CodeTokenizer/*' file='Doc/CodeTokenizer.xml'/>
  public class CodeTokenizer
  {
    #region Constructors

    // Initializes an object instance. (R)
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public CodeTokenizer()
    {
    }

    // Initializes the Keywords.
    /// <include path='items/InitializeKeywords/*' file='Doc/CodeTokenizer.xml'/>
    public void InitializeKeywords()
    {
      CommonDataTypes = new CommonDataTypes();
      CommonDataTypes.Items = CommonDataTypes.Deserialize();
      CommonKeywords = new CommonKeywords();
      CommonKeywords.Items = CommonKeywords.Deserialize();
      CommonModifiers = new CommonModifiers();
      CommonModifiers.Items = CommonModifiers.Deserialize();
      DataTypes = new DataTypes();
      DataTypes.Items = DataTypes.Deserialize();
      Keywords = new Keywords();
      Keywords.Items = Keywords.Deserialize();
      LibTypes = new LibTypes();
      LibTypes.Items = LibTypes.Deserialize();
      Modifiers = new Modifiers();
      Modifiers.Items = Modifiers.Deserialize();
      RefTypes = new RefTypes();
      RefTypes.Items = RefTypes.Deserialize();
    }

    // Sets the Keywords from the previously initialized CodeTokens. (R)
    /// <include path='items/SetKeywords/*' file='Doc/CodeTokenizer.xml'/>
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
    // (E)
    /// <include path='items/ClearRemainingTokens/*' file='Doc/CodeTokenizer.xml'/>
    public void ClearRemainingTokens(int tokenIndex)
    {
      for (int index = tokenIndex; index < Tokens.Length; index++)
      {
        Tokens[index] = null;
      }
    }

    // Combines the tokens for an XMLComment. (E)
    /// <include path='items/CombineXmlCommentTokens/*' file='Doc/CodeTokenizer.xml'/>
    public void CombineXmlCommentTokens(short tokenIndex)
    {
      //string comment = CombineTokens(Tokens, tokenIndex);
      string comment = CombineTokens(Tokens);
      Tokens[tokenIndex] = comment;
      ClearRemainingTokens(tokenIndex + 1);
    }

    // Sets the tokenIndex to the end of the tokens array. (E)
    /// <include path='items/EndTokens/*' file='Doc/CodeTokenizer.xml'/>
    public void EndTokens(ref short tokenIndex)
    {
      tokenIndex = (short)Tokens.Length;
    }

    // Gets the next token after the specified token index.(E)
    /// <include path='items/GetNextToken/*' file='Doc/CodeTokenizer.xml'/>
    public string GetNextToken(ref short tokenIndex)
    {
      string retValue;

      tokenIndex++;
      retValue = GetToken(tokenIndex);
      return retValue;
    }

    // Gets the token at the specified token index. (E)
    /// <include path='items/GetToken/*' file='Doc/CodeTokenizer.xml'/>
    public string GetToken(short tokenIndex)
    {
      string retValue = null;

      if (tokenIndex >= 0 && Tokens.Length > tokenIndex)
      {
        retValue = Tokens[tokenIndex];
      }
      return retValue;
    }

    // Check if the text has the begin delimiter. (E)
    /// <include path='items/HasBeginDelimiter/*' file='Doc/CodeTokenizer.xml'/>
    public bool HasBeginDelimiter(string text, string beginDelimiter)
    {
      bool retValue = false;

      if (text.IndexOf(beginDelimiter) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Full summary comment. (E)
    /// <include path='items/HasBothDelimiters/*' file='Doc/CodeTokenizer.xml'/>
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

    // Check if the text has the end delimiter. (E)
    /// <include path='items/HasEndDelimiter/*' file='Doc/CodeTokenizer.xml'/>
    public bool HasEndDelimiter(string text, string endDelimiter)
    {
      bool retValue = false;

      if (text.IndexOf(endDelimiter) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if a XML comment. (E)
    /// <include path='items/IsCodeXmlComment/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsCodeXmlComment(string text)
    {
      bool retValue = false;

      if (text.Trim().StartsWith("///"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if a code comment. (E)
    /// <include path='items/IsComment/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsComment(string text)
    {
      bool retValue = false;

      if (false == IsCodeXmlComment(text)
        && text.Trim().StartsWith("//"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a DataType. (E)
    /// <include path='items/IsDataType/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsDataType(string text, bool firstPass = false)
    {
      bool retValue = false;

      if (text != null)
      {
        var keyword = CommonDataTypes.SearchName(text);
        if (NetString.HasValue(keyword))
        {
          retValue = true;
        }
        else
        {
          if (false == firstPass)
          {
            keyword = DataTypes.SearchName(text);
            if (NetString.HasValue(keyword))
            {
              retValue = true;
            }
          }
        }
      }
      return retValue;
    }

    // Check if the text is a Data value. (E)
    /// <include path='items/IsDataValue/*' file='Doc/CodeTokenizer.xml'/>
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
      if (false == isException)
      {
        retValue = true;
      }
      if (false == retValue)
      {
        if (text.Contains("\"")
          || text.Contains("'"))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text contains a common delimiter. (E)
    /// <include path='items/IsDelimiters/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsDelimiters(string text)
    {
      bool retValue = false;

      if (":{}=\"".IndexOf(text) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Keyword. (E)
    /// <include path='items/IsKeyword/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsKeyword(string text, bool firstPass = false)
    {
      bool retValue = false;
      if (text != null)
      {
        var keyword = CommonKeywords.SearchName(text);
        if (NetString.HasValue(keyword))
        {
          retValue = true;
        }
        else
        {
          if (false == firstPass)
          {
            keyword = Keywords.SearchName(text);
            if (NetString.HasValue(keyword))
            {
              retValue = true;
            }
          }
        }
      }
      return retValue;
    }

    // Check if the text is a LibType. (E)
    /// <include path='items/IsLibType/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsLibType(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        var keyword = LibTypes.SearchName(text);
        if (NetString.HasValue(keyword))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text is a Modifier. (E)
    /// <include path='items/IsModifier/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsModifier(string text, bool firstPass = false)
    {
      bool retValue = false;
      if (text != null)
      {
        var keyword = CommonModifiers.SearchName(text);
        if (NetString.HasValue(keyword))
        {
          retValue = true;
        }
        else
        {
          if (false == firstPass)
          {
            keyword = Modifiers.SearchName(text);
            if (NetString.HasValue(keyword))
            {
              retValue = true;
            }
          }
        }
      }
      return retValue;
    }

    // Check if the text is a param comment. (E)
    /// <include path='items/IsParam/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsParam(string text)
    {
      bool retValue = false;

      if (text.IndexOf("<param name=") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a RefType. (E)
    /// <include path='items/IsRefType/*' file='Doc/CodeTokenizer.xml'/>
    public bool IsRefType(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        var keyword = RefTypes.SearchName(text);
        if (NetString.HasValue(keyword))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Sets the Tokens value, split on blanks. (E)
    /// <include path='items/SetTokens/*' file='Doc/CodeTokenizer.xml'/>
    public void SetTokens(string text)
    {
      char[] splitValues = new char[] { ' ' };
      Tokens = text.Trim().Split(splitValues
        , StringSplitOptions.RemoveEmptyEntries);
    }

    // Strips the leading qualifiers and from next qualifier to end of string.
    // (E)
    /// <include path='items/StripQualifier/*' file='Doc/CodeTokenizer.xml'/>
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
          index = retValue.IndexOf("(");
        }
        if (index < 0)
        {
          // Terminator
          index = retValue.IndexOf(";");
        }
        if (index < 0)
        {
          // Start array initializer.
          index = retValue.IndexOf("{");
        }
        if (index < 0)
        {
          // Item separator
          index = retValue.IndexOf(",");
        }
        if (index > -1)
        {
          if (0 == index)
          {
            prefixCount++;
            retValue = retValue.Substring(index + 1);
            retValue = StripQualifier(retValue, ref prefixCount);
          }
          else
          {
            retValue = retValue.Substring(0, index);
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
    private string CombineTokens(string[] tokens)
    {
      string retValue;

      StringBuilder builder = new StringBuilder(128);
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
        retValue = retValue.Substring(0, retValue.Length - 1);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The most common Data Types.</summary>
    public CommonDataTypes CommonDataTypes { get; set; }

    /// <summary>The most common Keywords.</summary>
    public CommonKeywords CommonKeywords { get; set; }

    /// <summary>The most common Modifiers.</summary>
    public CommonModifiers CommonModifiers { get; set; }

    /// <summary>The additional Data Types.</summary>
    public DataTypes DataTypes { get; set; }

    /// <summary>The additional Keywords.</summary>
    public Keywords Keywords { get; set; }

    /// <summary>The Library Types.</summary>
    public LibTypes LibTypes { get; set; }

    /// <summary>The additional Modifiers.</summary>
    public Modifiers Modifiers { get; set; }

    /// <summary>The Reference types.</summary>
    public RefTypes RefTypes { get; set; }

    /// <summary>Gets or sets the Token values.</summary>
    public string[] Tokens { get; set; }
    #endregion
  }
}
