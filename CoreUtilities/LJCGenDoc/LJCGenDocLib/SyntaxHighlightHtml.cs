// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// SyntaxHighlightHtml.cs
using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LJCGenDocLib
{
  // Provides methods for HTML Syntax Highighting.
  /// <include path='items/SyntaxHighlight/*' file='Doc/SyntaxHighlight.xml'/>
  public class SyntaxHighlightHtml
  {
    #region Static Methods

    // Retrieves the leading whitespace string.
    /// <include path='items/SaveLeadingWhiteSpace/*' file='Doc/SyntaxHighlight.xml'/>
    public static string SaveLeadingWhiteSpace(string text)
    {
      string retValue = text;

      for (int index = 0; index < retValue.Length; index++)
      {
        if (retValue[index] != ' '
          && retValue[index] != '\t')
        {
          retValue = retValue.Substring(0, index);
          break;
        }
      }
      return retValue;
    }

    // Strips the specified number of leading whitespace characters from a string.
    /// <include path='items/StripLeadingWhiteSpace/*' file='Doc/SyntaxHighlight.xml'/>
    public static string StripExtraLeadingWhiteSpace(string text, int stripLength)
    {
      string retValue = null;

      // *** Next Statement *** Add - 5/24
      string workText = text.Replace("\t", "  ");
      if (stripLength < workText.Length)
      {
        retValue = workText.Substring(stripLength);
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
    public SyntaxHighlightHtml()
    {
      //DoSort();
    }
    #endregion

    #region Public Main Methods

    // Adds the Syntax Highlighting to the HTML code line.
    /// <include path='items/AddSyntaxHighlight/*' file='Doc/SyntaxHighlight.xml'/>
    public string AddSyntaxHighlight(string line)
    {
      string text;
      string token;
      // *** Next Statement *** Add - 2/5/24
      string leadingWhiteSpace = SaveLeadingWhiteSpace(line);
      string[] tokens = GetTokens(line);

      short tokenIndex = -1;
      while ((token = GetNextToken(tokens, ref tokenIndex)) != null)
      {
        while (true)
        {
          if (0 == tokenIndex
            && IsCodeXmlComment(token))
          {
            CombineXmlCommentTokens(tokens, tokenIndex);
            AddXmlCommentSpan(ref tokens[tokenIndex]);
            text = GetToken(tokens, tokenIndex);
            if (IsParam(text))
            {
              IsSummaryInProcess = false;
              tokens[tokenIndex] = SetParamSpan(text);
            }
            // *** Begin *** Add - 2/5/24
            text = text.Replace("<", "&lt;");
            text = text.Replace(">", "&gt;");
            //text = $"{leadingWhiteSpace}{text}";
            // *** End   *** Add
            ProcessSummary(text, ref tokens, tokenIndex);
            ProcessReturns(text, ref tokens, tokenIndex);
            break;
          }

          if (IsComment(token))
          {
            tokens[tokenIndex] = line.Trim();
            ClearRemainingTokens(tokens, tokenIndex + 1);
            AddCommentSpan(ref tokens[tokenIndex]);
            break;
          }

          if (IsDelimiters(token))
          {
            break;
          }

          // Stop processing line if a data value.
          if (IsDataValue(token)
            && !token.Contains("("))
          {
            EndTokens(tokens, ref tokenIndex);
            break;
          }

          if (IsKeyword(token))
          {
            AddKeyWordSpan(ref tokens[tokenIndex]);
            break;
          }

          if (IsDataType(token))
          {
            AddCommonTypeSpan(ref tokens[tokenIndex]);
            if (tokens.Length > tokenIndex + 1)
            {
              // Skip identifier.
              tokenIndex++;
            }
            break;
          }

          if (IsRefType(token))
          {
            ProcessRefTypeTokens(tokens, ref tokenIndex);
            break;
          }

          if (IsModifier(token))
          {
            ProcessModifierTokens(tokens, ref tokenIndex);
            break;
          }

          if (IsUserType(token))
          {
            AddUserTypeSpan(ref tokens[tokenIndex]);
            ProcessGenericToken(ref tokens[tokenIndex]);
            break;
          }

          // Treat as user or other type.
          ProcessUserTypeTokens(tokens, ref tokenIndex);
          break;
        }
      }

      // Rebuild line from tokens.
      line = CombineTokens(tokens, 0);
      // *** Next Statement *** Add - 2/5/24
      line = $"{leadingWhiteSpace}{line}";
      return line;
    }

    // Strips the defined leading white spaces and applies Syntax Highlighting.
    /// <include path='items/FormatCode/*' file='Doc/SyntaxHighlight.xml'/>
    public string FormatCode(string dataTypeName, string dataMemberName
      , string code, bool stripLeadingSpaces = true)
    {
      string retValue;

      DataTypeName = dataTypeName;
      DataMemberName = dataMemberName;

      SetUserTypes(dataTypeName, dataMemberName, code);

      StringBuilder builder = new StringBuilder(128);
      code = code.Replace("<", "&lt;");
      code = code.Replace(">", "&gt;");
      int stripLength = 8;
      string[] lines = code.Split('\n');

      for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
      {
        string saveWhiteSpace;
        string line = lines[lineIndex];
        if (stripLeadingSpaces)
        {
          // *** Next Statement *** Add - 5/25
          int length = line.Replace("\t", "  ").Length;
          if (length > stripLength)
          {
            // Remove and save the leading spaces.
            line = StripExtraLeadingWhiteSpace(line, stripLength);
            saveWhiteSpace = SaveLeadingWhiteSpace(line);

            if (NetString.HasValue(line))
            {
              line = AddSyntaxHighlight(line);
            }

            // Add the leading white space back in.
            line = $"{saveWhiteSpace}{line}";
          }
        }
        builder.AppendLine(line);
      }
      retValue = builder.ToString();
      return retValue;
    }

    // Reads the User Types from a file or creates the file if it does not exist
    /// <include path='items/SetUserTypes/*' file='Doc/SyntaxHighlight.xml'/>
    public void SetUserTypes(string dataTypeName, string dataMemberName
      , string code)
    {
      UserTypes = new UserTypes();
      dataTypeName = dataTypeName.Replace(".", null);
      if (!Directory.Exists("UserTypeFiles"))
      {
        Directory.CreateDirectory("UserTypeFiles");
      }
      string fileName = $"UserTypeFiles\\{dataTypeName}{dataMemberName}.dat";
      if (File.Exists(fileName))
      {
        string[] userTypes = File.ReadAllLines(fileName);
        foreach (string userType in userTypes)
        {
          AddMissingUserType(userType);
        }
      }
      else
      {
        code = code.Replace("<", "&lt;");
        code = code.Replace(">", "&gt;");
        string[] lines = code.Split('\n');
        foreach (string line in lines)
        {
          UpdateUserTypes(line);
        }

        if (UserTypes.Count > 0)
        {
          foreach (string userType in UserTypes)
          {
            File.AppendAllText(fileName, $"{userType}\r\n");
          }
        }
      }
    }

    // Updates the User Types if a new one is found in the HTML code line.
    /// <include path='items/UpdateUserTypes/*' file='Doc/SyntaxHighlight.xml'/>
    public void UpdateUserTypes(string line)
    {
      int dotIndex;
      string token;
      string nextToken;
      string[] tokens = GetTokens(line);

      bool isUsing = false;
      short tokenIndex = -1;
      while ((token = GetNextToken(tokens, ref tokenIndex)) != null)
      {
        while (true)
        {
          // Stop processing line if a comment.
          if (IsTokenTypeAny(token, "CodeXmlComment", "Comment"))
          {
            EndTokens(tokens, ref tokenIndex);
            break;
          }

          // Stop processing line if a data value.
          if (IsDataValue(token)
            && !token.Contains("("))
          {
            EndTokens(tokens, ref tokenIndex);
            break;
          }

          // Is "class", "interface", etc.
          if (IsRefType(token))
          {
            if ((nextToken = GetNextToken(tokens, ref tokenIndex)) != null)
            {
              // Processes the nextToken as a type name.
              AddMissingUserType(nextToken);
              ProcessGenericToken(ref nextToken, true);
              break;
            }
          }

          if ("new" == token)
          {
            if ((nextToken = GetNextToken(tokens, ref tokenIndex)) != null)
            {
              if (!IsAnyKeyValue(nextToken))
              {
                AddMissingUserType(nextToken);
                ProcessGenericToken(ref nextToken, true);
              }
              else
              {
                // Backup to process arguments for constructor.
                tokenIndex--;
              }
              break;
            }
          }

          if (token == "using")
          {
            isUsing = true;
          }

          if (!isUsing)
          {
            // Process static class method.
            if ((dotIndex = token.IndexOf(".")) != -1)
            {
              string valueToken = token.Substring(0, dotIndex);
              AddMissingUserType(valueToken);
            }
          }

          // Token is a method definition or call.
          if (token.IndexOf("(") > 0)
          {
            // Increments the tokenIndex if the there are definition arguments
            // or multiple arguments.
            if (ProcessMethod(tokens, ref token, ref tokenIndex))
            {
              break;
            }
          }
          else
          {
            ProcessGenericToken(ref token, true);

            // Increments the tokenIndex if token is not a key value.
            ProcessTwoUnknownKeys(tokens, ref token, ref tokenIndex);
          }
          break;
        }
      }
    }
    #endregion

    #region Process Methods

    // Processes a Generic Type token.
    private void ProcessGenericToken(ref string token, bool getTypesOnly = false)
    {
      if (token.Contains("&lt;")
        && token.Contains("&gt;"))
      {
        int index = token.IndexOf("&");
        string saveTypeValue = token.Substring(0, index);
        string processValue = token.Substring(index);

        string tail = null;
        int tailIndex = token.IndexOf("&gt;");
        if (tailIndex > -1)
        {
          tailIndex += "&gt;".Length;
          if (tailIndex < token.Length)
          {
            tail = token.Substring(tailIndex);
          }
        }

        string[] separators = new string[] { "&lt;", ",", "&gt;" };
        string[] genericTokens = processValue.Split(separators
          , StringSplitOptions.RemoveEmptyEntries);
        for (int genericIndex = 0; genericIndex < genericTokens.Length; genericIndex++)
        {
          string genericToken = genericTokens[genericIndex];
          while (true)
          {
            if (IsLibType(genericToken))
            {
              if (!getTypesOnly)
              {
                AddLibTypeSpan(ref genericTokens[genericIndex]);
              }
              break;
            }

            if (IsDataType(genericToken))
            {
              if (!getTypesOnly)
              {
                AddCommonTypeSpan(ref genericTokens[genericIndex]);
              }
              break;
            }

            if (IsUserType(genericToken))
            {
              if (!getTypesOnly)
              {
                AddUserTypeSpan(ref genericTokens[genericIndex]);
              }
              break;
            }

            AddMissingUserType(genericToken);
            if (!getTypesOnly)
            {
              AddUserTypeSpan(ref genericTokens[genericIndex]);
            }
            break;
          }
        }

        // Rebuild token.
        if (!getTypesOnly)
        {
          bool first = true;
          StringBuilder builder = new StringBuilder(64);
          builder.Append(saveTypeValue);
          builder.Append("&lt;");
          foreach (string genericToken in genericTokens)
          {
            if (!genericToken.Contains(")"))
            {
              if (!first)
              {
                builder.Append(", ");
              }
              first = false;
              builder.Append(genericToken);
            }
          }
          builder.Append("&gt;");
          builder.Append(tail);
          token = builder.ToString();
        }
      }
    }

    // Processes the Method token.
    private bool ProcessMethod(string[] tokens, ref string token, ref short tokenIndex)
    {
      string nextToken;
      bool retValue = false;

      int index = token.IndexOf("(");
      if (index > 0)
      {
        string[] newTokens = token.Split('(');

        // If there are arguments.
        if (newTokens.Length > 1
          && !newTokens[1].StartsWith(")"))
        {
          // If not last argument token.
          if (-1 == newTokens[1].IndexOf(")"))
          {
            // if first argument is not a method call.
            if (newTokens.Length < 3)
            {
              while (true)
              {
                nextToken = newTokens[1];
                if (IsAnyKeyValue(nextToken))
                {
                  break;
                }
                if (nextToken.EndsWith(","))
                {
                  break;
                }

                // Increments the tokenIndex if token is not a key value.
                ProcessTwoUnknownKeys(tokens, ref nextToken, ref tokenIndex);
                break;
              }
            }

            // Start argument processing.
            retValue = true;
            short methodIndex = tokenIndex;
            while ((nextToken = GetNextToken(tokens, ref methodIndex)) != null)
            {
              tokenIndex++;
              while (true)
              {
                if (IsDataValue(nextToken))
                {
                  EndTokens(tokens, ref methodIndex);
                  break;
                }

                if (IsAnyKeyValue(nextToken))
                {
                  break;
                }

                if (nextToken.EndsWith(","))
                {
                  // Move to next set of arguments.
                  break;
                }

                // Last argument token.
                if (nextToken.IndexOf(")") > -1)
                {
                  EndTokens(tokens, ref methodIndex);
                  break;
                }

                // Increments the tokenIndex if token is not a key value.
                ProcessTwoUnknownKeys(tokens, ref nextToken, ref methodIndex);
                tokenIndex = methodIndex;
                break;
              }
            }
          }
        }
      }
      return retValue;
    }

    // Processes Modifier tokens.
    private void ProcessModifierTokens(string[] tokens, ref short tokenIndex
      , bool getTypesOnly = false)
    {
      if (!getTypesOnly)
      {
        AddModifierSpan(ref tokens[tokenIndex]);
      }

      // Type token.
      string token;
      bool isCommonType = false;
      while ((token = GetNextToken(tokens, ref tokenIndex)) != null)
      {
        while (true)
        {
          if (IsModifier(token))
          {
            if (!getTypesOnly)
            {
              AddModifierSpan(ref tokens[tokenIndex]);
            }
            break;
          }

          ProcessTypesAndKeywords(tokens, ref tokenIndex, ref isCommonType
            , getTypesOnly);

          // Treat as UserType or other RefType.
          if (!isCommonType)
          {
            if (!getTypesOnly)
            {
              // *** Next Statement *** Add - 11/29/22
              if (tokenIndex < tokens.Length - 1)
              {
                AddUserTypeSpan(ref tokens[tokenIndex]);
              }
            }

            // Don't process any more tokens as user type.
            isCommonType = true;
          }
          break;
        }
      }
    }

    // Processes RefType (class, interface, struct, enum or delegate) token.
    private void ProcessRefTypeTokens(string[] tokens, ref short tokenIndex
      , bool getTypesOnly = false)
    {
      if (!getTypesOnly)
      {
        AddRefTypeSpan(ref tokens[tokenIndex]);
      }
      tokenIndex++;
      string token = GetToken(tokens, tokenIndex);
      if (token != null)
      {
        if (!getTypesOnly)
        {
          AddUserTypeSpan(ref tokens[tokenIndex]);
        }
      }
    }

    // Processes the Returns token.
    private void ProcessReturns(string text, ref string[] tokens, int tokenIndex)
    {
      if (IsFullReturns(text))
      {
        IsReturnsInProcess = false;
        tokens[tokenIndex] = SetReturnsSpan(text);
      }
      else
      {
        if (!IsReturnsInProcess)
        {
          if (IsBeginReturns(text))
          {
            IsReturnsInProcess = true;
            tokens[tokenIndex] = SetReturnsSpan(text);
          }
        }
        else
        {
          if (IsEndReturns(text))
          {
            IsReturnsInProcess = false;
          }
          tokens[tokenIndex] = SetReturnsSpan(text);
        }
      }
    }

    // Processes the Summary token.
    private void ProcessSummary(string text, ref string[] tokens, int tokenIndex)
    {
      if (IsFullSummary(text))
      {
        IsSummaryInProcess = false;
        tokens[tokenIndex] = SetSummarySpan(text);
      }
      else
      {
        if (!IsSummaryInProcess)
        {
          if (IsBeginSummary(text))
          {
            IsSummaryInProcess = true;
            tokens[tokenIndex] = SetSummarySpan(text);
          }
        }
        else
        {
          if (IsEndSummary(text))
          {
            IsSummaryInProcess = false;
          }
          tokens[tokenIndex] = SetSummarySpan(text);
        }
      }
    }

    // Increments the tokenIndex if token is not a key value.
    private bool ProcessTwoUnknownKeys(string[] tokens, ref string token, ref short tokenIndex)
    {
      string nextToken;
      bool retValue = false;

      // Look for two unknown keys together.
      if (!IsAnyKeyValue(token))
      {
        // Processes the next token here so increments tokenIndex.
        if ((nextToken = GetNextToken(tokens, ref tokenIndex)) != null)
        {
          if (!IsAnyKeyValue(nextToken))
          {
            AddMissingUserType(token);

            // Should this be after ProcessMethod?
            ProcessGenericToken(ref token, true);

            // Process nextToken method.
            if (nextToken.IndexOf("(") > 0)
            {
              // Increments the tokenIndex if the there are definition arguments
              // or multiple arguments.
              ProcessMethod(tokens, ref nextToken, ref tokenIndex);
              token = nextToken;
            }
            retValue = true;
          }
        }
      }
      return retValue;
    }

    // Processes data type and keyword tokens.
    private void ProcessTypesAndKeywords(string[] tokens, ref short tokenIndex
      , ref bool isCommonType, bool getTypesOnly = false)
    {
      // Backup to start with the same token.
      short index = (short)(tokenIndex - 1);
      string token;
      while ((token = GetNextToken(tokens, ref index)) != null)
      {
        tokenIndex = index;
        while (true)
        {
          if (IsDelimiters(token))
          {
            break;
          }

          if (IsKeyword(token))
          {
            if (!getTypesOnly)
            {
              AddKeyWordSpan(ref tokens[tokenIndex]);
            }
            break;
          }

          if (IsDataType(token))
          {
            if (!getTypesOnly)
            {
              AddCommonTypeSpan(ref tokens[index]);
            }

            // Don't process any more tokens as user type.
            isCommonType = true;
            break;
          }

          if (IsRefType(token))
          {
            ProcessRefTypeTokens(tokens, ref index, getTypesOnly);

            // Don't process any more tokens as user type.
            isCommonType = true;
            break;
          }

          if (IsLibType(token))
          {
            if (!getTypesOnly)
            {
              AddLibTypeSpan(ref tokens[index]);
            }
            ProcessGenericToken(ref tokens[index], getTypesOnly);

            // Don't process any more tokens as user type.
            isCommonType = true;
            break;
          }

          if (IsUserType(token))
          {
            if (!getTypesOnly)
            {
              AddUserTypeSpan(ref tokens[index]);
            }
            ProcessGenericToken(ref token, getTypesOnly);

            // Don't process any more tokens as user type.
            isCommonType = true;
          }

          if (token.Contains("("))
          {
            int saveIndex = tokens[index].IndexOf("(");
            string saveHead = tokens[index].Substring(0, saveIndex);
            int tailIndex = token.IndexOf("(");
            string tail = token.Substring(tailIndex + 1);
            if (IsUserType(tail))
            {
              if (!getTypesOnly)
              {
                AddUserTypeSpan(ref tail);
              }
            }
            tokens[index] = $"{saveHead}({tail}";
            break;
          }
          break;
        }
      }
      tokenIndex = index;
    }

    // Processes User Type tokens.
    private void ProcessUserTypeTokens(string[] tokens, ref short tokenIndex)
    {
      bool isType = false;
      if (tokenIndex < tokens.Length)
      {
        string token = tokens[tokenIndex];
        if (token != null)
        {
          if (token.Contains("("))
          {
            if (tokenIndex > 0)
            {
              string prevToken = tokens[tokenIndex - 1];
              if (prevToken.Contains(">new<"))
              {
                // Constructor
                isType = true;
              }
            }
          }
        }

        if (isType)
        {
          AddMissingUserType(token);
          AddUserTypeSpan(ref tokens[tokenIndex]);

          // Skip identifier.
          if (tokens.Length > tokenIndex + 1)
          {
            tokenIndex++;
          }
        }
      }
    }
    #endregion

    #region Token Methods

    // Adds a token to the UserTypes list.
    private void AddMissingUserType(string token)
    {
      int prefixCount = 0;

      if (char.IsUpper(token[0]))
      {
        token = StripQualifier(token, ref prefixCount);
        if (null == UserTypes.LJCSearchName(token))
        {
          UserTypes.Add(token);
          UserTypes.Sort();
        }
      }
    }

    // Clears all the remaining tokens; starting with the specified token index.
    private void ClearRemainingTokens(string[] tokens, int tokenIndex)
    {
      for (int index = tokenIndex; index < tokens.Length; index++)
      {
        tokens[index] = null;
      }
    }

    // Returns all the combined tokens as a single string.
    //private string CombineTokens(string[] tokens, short startTokenIndex)
    private string CombineTokens(string[] tokens, short _)
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

    // Combines the tokens for an XMLComment.
    private void CombineXmlCommentTokens(string[] tokens, short tokenIndex)
    {
      string comment = CombineTokens(tokens, tokenIndex);
      tokens[tokenIndex] = comment;
      ClearRemainingTokens(tokens, tokenIndex + 1);
    }

    // Sets the tokenIndex to the end of the tokens array.
    private void EndTokens(string[] tokens, ref short tokenIndex)
    {
      tokenIndex = (short)tokens.Length;
    }

    // Gets the next token after the specified token index.
    private string GetNextToken(string[] tokens, ref short tokenIndex)
    {
      string retValue = null;

      tokenIndex++;
      if (tokenIndex >= 0 && tokens.Length > tokenIndex)
      {
        retValue = tokens[tokenIndex];
      }
      return retValue;
    }

    // Gets the token at the specified token index.
    private string GetToken(string[] tokens, short tokenIndex)
    {
      string retValue = null;

      if (tokenIndex >= 0 && tokens.Length > tokenIndex)
      {
        retValue = tokens[tokenIndex];
      }
      return retValue;
    }

    // Returns the string as a set of tokens, split on blanks.
    private string[] GetTokens(string text)
    {
      string[] retValue;

      char[] splitValues = new char[] { ' ' };
      retValue = text.Trim().Split(splitValues
        , StringSplitOptions.RemoveEmptyEntries);
      return retValue;
    }
    #endregion

    #region Check Token Methods

    // Check if any key value.
    private bool IsAnyKeyValue(string token)
    {
      int prefixCount = 0;
      bool retValue = false;

      string stripToken = StripQualifier(token, ref prefixCount);
      if (IsTokenTypeAny(stripToken, "Delimiters", "Modifier", "Keyword", "DataType"
          , "RefType", "LibType", "UserType", "ComparisonOperator", "BooleanOperator"
          , "OtherModifier", "UncommonKeyword"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Begin returns comment.
    private bool IsBeginReturns(string text)
    {
      bool retValue = false;

      if (text.IndexOf(";returns") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Begin summary comment.
    private bool IsBeginSummary(string text)
    {
      bool retValue = false;

      if (text.IndexOf(";summary") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Boolean operator.
    private bool IsBooleanOperator(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        if (BooleanOperators.BinarySearch(text) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if a XML comment.
    private bool IsCodeXmlComment(string text)
    {
      bool retValue = false;

      var trimText = text.Trim();
      if (trimText.StartsWith("///")
        || "#region" == trimText
        || "#endregion" == trimText)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if a code comment.
    private bool IsComment(string text)
    {
      bool retValue = false;

      if (!IsCodeXmlComment(text)
        && text.Trim().StartsWith("//"))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Comparison operator.
    private bool IsComparisonOperator(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        if (ComparisonOperators.BinarySearch(text) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text is a DataType.
    private bool IsDataType(string text)
    {
      int prefixCount = 0;
      bool retValue = false;

      if (text != null)
      {
        string stripText = StripQualifier(text, ref prefixCount);
        if (DataTypes.BinarySearch(stripText) > -1)
        {
          retValue = true;
        }

        if (!retValue)
        {
          retValue = IsUncommonDataType(stripText);
        }
      }
      return retValue;
    }

    // Check if the text is a Data value.
    private bool IsDataValue(string text)
    {
      double numericValue;
      bool isException = false;
      int prefixCount = 0;
      bool retValue = false;

      string stripText = StripQualifier(text, ref prefixCount);
      try
      {
        numericValue = double.Parse(stripText);
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
        if (text.Contains("\"")
          || text.Contains("'"))
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text contains a common delimiter.
    private bool IsDelimiters(string text)
    {
      bool retValue = false;

      if (":{}=\"".IndexOf(text) > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is an End returns comment.
    private bool IsEndReturns(string text)
    {
      bool retValue = false;

      if (text.IndexOf(";/returns") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is an End summary comment.
    private bool IsEndSummary(string text)
    {
      bool retValue = false;

      if (text.IndexOf(";/summary") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Full returns comment.
    private bool IsFullReturns(string text)
    {
      bool retValue = false;

      if (IsBeginReturns(text)
        && IsEndReturns(text))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a Full summary comment.
    private bool IsFullSummary(string text)
    {
      bool retValue = false;

      if (IsBeginSummary(text)
        && IsEndSummary(text))
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a KeyWord.
    private bool IsKeyword(string text)
    {
      int prefixCount = 0;
      bool retValue = false;

      if (text != null)
      {
        string stripText = StripQualifier(text, ref prefixCount);
        if (KeyWords.BinarySearch(stripText) > -1)
        {
          retValue = true;
        }

        if (!retValue)
        {
          retValue = IsUncommonKeyword(stripText);
        }
      }
      return retValue;
    }

    // Check if the text is a LibType.
    private bool IsLibType(string text)
    {
      int prefixCount = 0;
      bool retValue = false;

      if (text != null)
      {
        string stripText = StripQualifier(text, ref prefixCount);
        if (LibTypes.BinarySearch(stripText) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text is a Modifer.
    private bool IsModifier(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        if (Modifiers.BinarySearch(text) > -1)
        {
          retValue = true;
        }

        if (!retValue)
        {
          retValue = IsOtherModifier(text);
        }
      }
      return retValue;
    }

    // Check if the text is one of the OtherModifiers
    private bool IsOtherModifier(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        if (OtherModifiers.BinarySearch(text) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text is a param comment.
    private bool IsParam(string text)
    {
      bool retValue = false;

      if (text.IndexOf("&lt;param name=") > -1)
      {
        retValue = true;
      }
      return retValue;
    }

    // Check if the text is a RefType.
    private bool IsRefType(string text)
    {
      bool retValue = false;

      if (text != null)
      {
        if (RefTypes.BinarySearch(text) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Checks for any of the specified token types.
    private bool IsTokenTypeAny(string token, params string[] names)
    {
      bool retValue = false;

      foreach (string name in names)
      {
        if ("delimiters" == name.ToLower())
        {
          if (IsDelimiters(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("codexmlcomment" == name.ToLower())
        {
          if (IsCodeXmlComment(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("comment" == name.ToLower())
        {
          if (IsComment(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("modifier" == name.ToLower())
        {
          if (IsModifier(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("othermodifier" == name.ToLower())
        {
          if (IsOtherModifier(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("keyword" == name.ToLower())
        {
          if (IsKeyword(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("datatype" == name.ToLower())
        {
          if (IsDataType(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("reftype" == name.ToLower())
        {
          if (IsRefType(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("libtype" == name.ToLower())
        {
          if (IsLibType(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        if ("usertype" == name.ToLower())
        {
          if (IsUserType(token))
          {
            retValue = true;
            break;
          }
        }
        if ("comparisonoperator" == name.ToLower())
        {
          if (IsComparisonOperator(token))
          {
            retValue = true;
            break;
          }
        }
        if ("booleanoperator" == name.ToLower())
        {
          if (IsBooleanOperator(token))
          {
            retValue = true;
            break;
          }
        }
        if ("uncommonkeyword" == name.ToLower())
        {
          if (IsUncommonKeyword(token))
          {
            retValue = true;
            break;
          }
          continue;
        }
        continue;
      }
      return retValue;
    }

    // Checks if the text is an uncommon DataType.
    private bool IsUncommonDataType(string text)
    {
      int prefixCount = 0;
      bool retValue = false;

      if (text != null)
      {
        string stripText = StripQualifier(text, ref prefixCount);
        if (UncommonDataTypes.BinarySearch(stripText) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text is an uncommon KeyWord.
    private bool IsUncommonKeyword(string text)
    {
      int prefixCount = 0;
      bool retValue = false;

      if (text != null)
      {
        string stripText = StripQualifier(text, ref prefixCount);
        if (UncommonKeyWords.BinarySearch(stripText) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }

    // Check if the text is a UserType.
    private bool IsUserType(string text)
    {
      int prefixCount = 0;
      bool retValue = false;

      if (text != null)
      {
        string stripText = StripQualifier(text, ref prefixCount);
        if (UserTypes.BinarySearch(stripText) > -1)
        {
          retValue = true;
        }
      }
      return retValue;
    }
    #endregion

    #region Include Span Methods

    // Set the text as Comments text.
    internal string SetCommentSpan(string line)
    {
      string retValue = line;
      StringBuilder builder = new StringBuilder();
      int index = retValue.IndexOf("//");
      string comment = retValue.Substring(index);
      builder.Append(GetWithCommentSpan(comment));
      retValue = builder.ToString();
      return retValue;
    }

    // Set the text as Params text.
    private string SetParamSpan(string text)
    {
      string retValue = text;

      int startIndex = 0;
      string replaceTarget = NetString.GetDelimitedString(text, "name=\""
        , ref startIndex, ";");
      if (replaceTarget != null)
      {
        replaceTarget += ";";
        int trimLength = replaceTarget.Length - "\"&gt;".Length;
        string trimmedTarget = replaceTarget.Substring(0, trimLength);
        string replaceValue = $"</span>{trimmedTarget}{BeginXmlCommentSpan}"
          + $"\"&gt;</span>{BeginCommentSpan}";
        text = text.Replace(replaceTarget, replaceValue);

        replaceTarget = "&lt;/param";
        if (text.IndexOf(replaceTarget) > -1)
        {
          replaceValue = $"</span>{BeginXmlCommentSpan}&lt;/param";
          retValue = text.Replace(replaceTarget, replaceValue);
        }
      }
      return retValue;
    }

    // Set the text as Returns text.
    private string SetReturnsSpan(string text)
    {
      return SetSimpleCommentSpan(text, ";returns&gt;", "&lt;/returns&gt;");
    }

    // Set the text between the delimiters as Comments text.
    private string SetSimpleCommentSpan(string text, string beginDelimiter
      , string endDelimiter = null)
    {
      string replaceTarget;
      string replaceValue;
      string retValue = text;

      if (null == endDelimiter)
      {
        endDelimiter = beginDelimiter;
      }

      int startIndex = 0;
      int beginIndex = text.IndexOf(beginDelimiter);
      int endIndex = text.IndexOf(endDelimiter);
      if (beginIndex > -1
        && endIndex > -1)
      {
        // Begin and End delimiters are present.
        replaceTarget = NetString.GetDelimitedString(text, beginDelimiter
          , ref startIndex, endDelimiter);
        replaceValue = $"</span>{BeginCommentSpan}{replaceTarget}</span>"
          + $"{BeginXmlCommentSpan}";
      }
      else
      {
        if (-1 == beginIndex
          && -1 == endIndex)
        {
          // No delimiters are present.
          beginDelimiter = "///";
          //beginIndex = text.IndexOf(beginDelimiter);
          replaceTarget = NetString.GetDelimitedString(text, beginDelimiter
            , ref startIndex, "</span>");
        }
        else
        {
          if (-1 == beginIndex)
          {
            // Only End delimiter is present.
            beginDelimiter = "///";
            //beginIndex = text.IndexOf(beginDelimiter);
            replaceTarget = NetString.GetDelimitedString(text, beginDelimiter
              , ref startIndex, endDelimiter);
          }
          else
          {
            // Only Begin delimiter is present.
            replaceTarget = NetString.GetDelimitedString(text, beginDelimiter
              , ref startIndex, "</span>");
          }
        }
        replaceValue = $"{BeginCommentSpan}{replaceTarget}</span>";
      }
      if (NetString.HasValue(replaceTarget))
      {
        retValue = text.Replace(replaceTarget, replaceValue);
      }
      return retValue;
    }

    // Set the text as Summary text.
    private string SetSummarySpan(string text)
    {
      return SetSimpleCommentSpan(text, ";summary&gt;", "&lt;/summary&gt;");
    }
    #endregion

    #region Get with Span methods.

    // Returns the text wrapped in a "comment" span.
    private string GetWithCommentSpan(string text)
    {
      return Span("comment", text);
    }

    // Returns the text wrapped in a "commonType" span.
    private string GetWithCommonTypeSpan(string text)
    {
      int prefixCount = 0;
      string retValue;

      string stripText = StripQualifier(text, ref prefixCount);
      if (stripText.Length < text.Length)
      {
        retValue = Span("commonType", stripText);
        retValue += text.Substring(stripText.Length + prefixCount);
      }
      else
      {
        retValue = Span("commonType", text);
      }
      return retValue;
    }

    // Returns the text wrapped in a "keyWord" span.
    private string GetWithKeyWordSpan(string text)
    {
      int prefixCount = 0;
      string retValue;

      string stripText = StripQualifier(text, ref prefixCount);
      if (stripText.Length < text.Length)
      {
        retValue = Span("keyWord", stripText);
        retValue += text.Substring(stripText.Length);
      }
      else
      {
        retValue = Span("keyWord", text);
      }
      return retValue;
    }

    // Returns the text wrapped in a "libType" span.
    private string GetWithLibTypeSpan(string text)
    {
      int prefixCount = 0;
      string retValue;

      string stripText = StripQualifier(text, ref prefixCount);
      if (stripText.Length < text.Length)
      {
        retValue = Span("libType", stripText);
        retValue += text.Substring(stripText.Length);
      }
      else
      {
        retValue = Span("libType", text);
      }
      return retValue;
    }

    // Returns the text wrapped in a "modifier" span. 
    private string GetWithModifierSpan(string text)
    {
      return Span("modifier", text);
    }

    // Returns the text wrapped in a "refType" span.
    private string GetWithRefTypeSpan(string text)
    {
      return Span("refType", text);
    }

    // Returns the text wrapped in a "userType" span.
    private string GetWithUserTypeSpan(string text)
    {
      int prefixCount = 0;
      string retValue;

      string stripText = StripQualifier(text, ref prefixCount);
      if (stripText.Length < text.Length)
      {
        retValue = Span("userType", stripText);
        retValue += text.Substring(stripText.Length);
      }
      else
      {
        retValue = Span("userType", text);
      }
      return retValue;
    }

    // Returns the text wrapped in an "xmlComment" span.
    private string GetWithXmlCommentSpan(string text)
    {
      return Span("xmlComment", text);
    }

    // Returns the text wrapped in a Span with the supplied class name.
    private string Span(string className, string text, bool includeEnd = true)
    {
      string retValue;

      retValue = $"<span class='{className}'>{text}";
      if (includeEnd)
      {
        retValue = $"{retValue}</span>";
      }
      return retValue;
    }

    // Strips the trailing text from the last qualifier in a qualifier chain.
    private string StripQualifier(string text, ref int prefixCount)
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
          // Generic
          index = retValue.IndexOf("&lt;");
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

    #region Add Span Methods

    // Adds a "comment" span to the supplied text.
    private void AddCommentSpan(ref string text)
    {
      text = GetWithCommentSpan(text);
    }

    // Adds a "commonType" span to the supplied text.
    private void AddCommonTypeSpan(ref string text)
    {
      text = GetWithCommonTypeSpan(text);
    }

    // Adds a "keyWord" span to the supplied text.
    private void AddKeyWordSpan(ref string text)
    {
      text = GetWithKeyWordSpan(text);
    }

    // Adds a "libType" span to the supplied text.
    private void AddLibTypeSpan(ref string text)
    {
      text = GetWithLibTypeSpan(text);
    }

    // Adds a "modifier" span to the supplied text.
    private void AddModifierSpan(ref string text)
    {
      text = GetWithModifierSpan(text);
    }

    // Adds a "refType" span to the supplied text.
    private void AddRefTypeSpan(ref string text)
    {
      text = GetWithRefTypeSpan(text);
    }

    // Adds a "userType" span to the supplied text.
    private void AddUserTypeSpan(ref string text)
    {
      text = GetWithUserTypeSpan(text);
    }

    // Adds an "xmlComment" span to the supplied text.
    private void AddXmlCommentSpan(ref string text)
    {
      text = GetWithXmlCommentSpan(text);
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the Allow Debug Breaks value.</summary>
    public bool AllowDebugBreaks { get; set; }

    // Gets the BeginComment span.
    private string BeginCommentSpan
    {
      get { return Span("comment", null, false); }
    }

    // Gets the BeginXmlComment span.
    private string BeginXmlCommentSpan
    {
      get { return Span("xmlComment", null, false); }
    }

    // Gets or sets the DataMember name value.
    internal string DataMemberName { get; set; }

    // Gets or sets the DataType name value.
    internal string DataTypeName { get; set; }

    // Gets the ReturnsInProcess flag.
    private bool IsReturnsInProcess { get; set; }

    // Gets or sets the SummaryInProcess flag.
    private bool IsSummaryInProcess { get; set; }
    #endregion

    #region Fields

    internal void DoSort()
    {
      BooleanOperators.Sort();
      ComparisonOperators.Sort();
      DataTypes.Sort();
      KeyWords.Sort();
      LibTypes.Sort();
      Modifiers.Sort();
      OtherModifiers.Sort();
      RefTypes.Sort();
      UncommonDataTypes.Sort();
      UncommonKeyWords.Sort();
    }

    // The list of Boolean operators.
    private readonly List<string> BooleanOperators = new List<string>()
    {
      "&&",
      "||"
    };

    // The list of Comparison operators.
    private readonly List<string> ComparisonOperators = new List<string>()
    {
      "!=",
      "&gt",
      "&gt;",
      "&lt",
      "&lt;",
      "<",
      "<=",
      "==",
      ">",
      ">="
    };

    // The list of common DataTypes.
    private readonly List<string> DataTypes = new List<string>()
    {
      "bool",
      "Boolean",
      "int",
      "Int16",
      "Int32",
      "object",
      "Object",
      "short",
      "string",
      "String",
      "var",
      "void"
    };

    // The list of common Keywords.
    private readonly List<string> KeyWords = new List<string>()
    {
      "as",
      "break",
      "case",
      "catch",
      "continue",
      "default",
      "do",
      "else",
      "false",
      "finally",
      "for",
      "foreach",
      "get",
      "if",
      "in",
      "is",
      "namespace",
      "new",
      "null",
      "out",
      "ref",
      "return",
      "set",
      "switch",
      "this",
      "throw",
      "true",
      "try",
      "typeof",
      "using",
      "while"
    };

    // The list of common Library types.
    private readonly List<string> LibTypes = new List<string>()
    {
      "event",
      "List",
      "params",
      "StringBuilder"
    };

    // The list of common object modifiers.
    private readonly List<string> Modifiers = new List<string>()
    {
      "override",
      "private",
      "protected",
      "public",
      "static"
    };

    // The list of uncommon object modifiers.
    private readonly List<string> OtherModifiers = new List<string>()
    {
      "abstract",
      "extern",
      "internal",
      "protected",
      "sealed",
      "virtual"
    };

    // The list of common Reference types.
    private readonly List<string> RefTypes = new List<string>()
    {
      "class",
      "delegate",
      "enum",
      "interface",
      "struct"
    };

    // The list of uncommon DataTypes.
    private readonly List<string> UncommonDataTypes = new List<string>()
    {
      "byte",
      "Byte",
      "char",
      "Char",
      "decimal",
      "Decimal",
      "double",
      "Double",
      "float",
      "Int64",
      "long",
      "Single"
    };

    // The list of uncommon Keywords.
    private readonly List<string> UncommonKeyWords = new List<string>()
    {
      "abstract",
      "base",
      "checked",
      "const",
      "explicit",
      "fixed",
      "goto",
      "implicit",
      "operator",
      "readonly",
      "sizeof",
      "stacalloc",
      "unchecked",
      "unsafe",
      "volatile"
    };

    // The list of UserTypes.
    private UserTypes UserTypes = new UserTypes()
    {
    };
    #endregion
  }
}
