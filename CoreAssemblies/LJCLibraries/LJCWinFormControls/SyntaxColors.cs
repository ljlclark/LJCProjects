// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// SyntaxColors.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LJCNetCommon;

namespace LJCWinFormControls
{
  // The line color settings.
  /// <include path='items/SyntaxColors/*' file='Doc/SyntaxColors.xml'/>
  public class SyntaxColors
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public SyntaxColors()
    {
      Lines = new List<string>();
      ColorSettings = new ColorSettings();
      //mTokens = new CodeTokenizer();
    }
    #endregion

    #region Public Methods

    // Parallel processing a text file for the ColorSettings.
    /// <include path='items/CreateColorSettings1/*' file='Doc/SyntaxColors.xml'/>
    public void CreateColorSettings(string fileSpec)
    {
      using (StreamReader streamReader = File.OpenText(fileSpec))
      {
        while (false == streamReader.EndOfStream)
        {
          Lines.Add(streamReader.ReadLine());
        }
      }
      string[] lines = Lines.ToArray();
      CreateColorSettings(lines);
    }

    // Parallel processing a string array for the ColorSettings.
    /// <include path='items/CreateColorSettings2/*' file='Doc/SyntaxColors.xml'/>
    public void CreateColorSettings(string[] lines)
    {
      CodeTokenizer setup = new CodeTokenizer();
      setup.InitializeKeywords();

      bool test = false;
      if (test)
      {
        for (int index = 0; index < lines.Length; index++)
        {
          string text = lines[index];
          CreateLineColorSettings(setup, text, index);
        }
      }
      else
      {
        Parallel.For(0, lines.Count(), lineIndex =>
        {
          CreateLineColorSettings(setup, lines[lineIndex], lineIndex);
        });
      }
    }

    // Creates the ColorSettings for the specified line.
    /// <include path='items/CreateLineColorSettings/*' file='Doc/SyntaxColors.xml'/>
    public void CreateLineColorSettings(CodeTokenizer setup, string lineText, int lineIndex)
    {
      string token;
      bool firstPass = false;

      CodeTokenizer tokens = new CodeTokenizer();
      tokens.SetKeywords(setup);

      tokens.SetTokens(lineText);
      short tokenIndex = -1;
      while ((token = tokens.GetNextToken(ref tokenIndex)) != null)
      {
        while (true)
        {
          // Only process if first token and token is "///".
          if (0 == tokenIndex
            && tokens.IsCodeXmlComment(token))
          {
            // Process entire line as one token.
            tokens.CombineXmlCommentTokens(tokenIndex);
            ColorSettings.Add(lineIndex, 0, lineText.Length, mXmlLineColor);

            if (tokens.IsParam(lineText))
            {
              ProcessParam(lineText, lineIndex);
            }
            else
            {
              ProcessXmlComment(tokens, lineText, lineIndex, "<summary>", "</summary>");
              ProcessXmlComment(tokens, lineText, lineIndex, "<returns>", "</returns>"
                , false);
            }
            break;
          }

          // Process if token is "//".
          if (tokens.IsComment(token))
          {
            tokens.Tokens[tokenIndex] = lineText.Trim();
            tokens.ClearRemainingTokens(tokenIndex + 1);
            int beginIndex = lineText.IndexOf("//");
            int textLength = lineText.Length - beginIndex;
            ColorSettings.Add(lineIndex, beginIndex, textLength, mCommentColor);
            break;
          }

          if ("#region" == token || "#endregion" == token)
          {
            ProcessKeyValue(tokens, lineText, lineIndex, token, mXmlLineColor);
            break;
          }

          // Must strip qualifiers from token before the following code.
          int prefixCount = 0;
          string stripToken = tokens.StripQualifier(token, ref prefixCount);

          if (tokens.IsModifier(stripToken, firstPass))
          {
            ProcessKeyValue(tokens, lineText, lineIndex, stripToken, mModifierColor);
            break;
          }

          if (tokens.IsDelimiters(token))
          {
            break;
          }

          if (tokens.IsRefType(token))
          {
            ProcessKeyValue(tokens, lineText, lineIndex, stripToken, mRefTypeColor);
            break;
          }

          // Stop processing line if a data value.
          if (tokens.IsDataValue(stripToken)
            && false == token.Contains("("))
          {
            tokens.EndTokens(ref tokenIndex);
            break;
          }

          if (tokens.IsKeyword(stripToken, firstPass))
          {
            ProcessKeyValue(tokens, lineText, lineIndex, stripToken, mKeywordColor);
            break;
          }

          if (tokens.IsDataType(stripToken, firstPass))
          {
            ProcessKeyValue(tokens, lineText, lineIndex, stripToken, mDataTypeColor);
            break;
          }

          if (tokens.IsLibType(stripToken))
          {
            ProcessKeyValue(tokens, lineText, lineIndex, stripToken, mLibTypeColor);
            break;
          }
          break;
        }
      }
      SetReplacementsColor(lineText, lineIndex);
    }

    // Processes the Param token.
    /// <include path='items/ProcessParam/*' file='Doc/SyntaxColors.xml'/>
    public void ProcessParam(string lineText, int lineIndex)
    {
      IsSummaryInProcess = false;
      IsReturnsInProcess = false;
      SetDelimitedColorSetting(lineText, lineIndex, "name=\"", "\">"
        , Color.Black);
      SetDelimitedColorSetting(lineText, lineIndex, "\">", "</param>"
        , mCommentColor);
    }

    // Processes the Summary and Returns tokens.
    /// <include path='items/ProcessXmlComment/*' file='Doc/SyntaxColors.xml'/>
    public void ProcessXmlComment(CodeTokenizer tokens, string lineText, int lineIndex
      , string beginTag, string endTag, bool isSummary = true)
    {
      // Both tags are in the line.
      if (tokens.HasBothDelimiters(lineText, beginTag, endTag))
      {
        // Highlight middle of line.
        SetInProcess(isSummary, false);
        SetDelimitedColorSetting(lineText, lineIndex, beginTag, endTag
          , mXMLCommentColor);
      }
      else
      {
        // XML Comment processing has not started.
        if (false == IsInProcess(isSummary))
        {
          if (tokens.HasBeginDelimiter(lineText, beginTag))
          {
            // Set "In Process" flag and Highlight end of line.
            SetInProcess(isSummary, true);
            SetDelimitedColorSetting(lineText, lineIndex, beginTag, null
              , mXMLCommentColor);
          }
        }
        else
        {
          // XML Comment processing has started.
          if (tokens.HasEndDelimiter(lineText, endTag))
          {
            // Clear "In Process" flag and Highlight beginning of line.
            SetInProcess(isSummary, false);
            SetDelimitedColorSetting(lineText, lineIndex, "///", endTag
              , mXMLCommentColor);
          }
          else
          {
            // No Tag and processing has started.
            if (IsInProcess(isSummary))
            {
              // Highlight entire contents.
              SetDelimitedColorSetting(lineText, lineIndex, "///", "#NoDelimiter"
                , mXMLCommentColor);
            }
          }
        }
      }
    }
    #endregion

    #region Private Methods

    //  Sets the InProcess value.
    private void SetInProcess(bool isSummary, bool inProcess)
    {
      if (isSummary)
      {
        IsSummaryInProcess = inProcess;
      }
      else
      {
        IsReturnsInProcess = inProcess;
      }
    }

    // Gets the InProcess values.
    private bool IsInProcess(bool isSummary)
    {
      bool retValue = false;

      if ((isSummary && IsSummaryInProcess)
        || (false == isSummary && IsReturnsInProcess))
      {
        retValue = true;
      }
      return retValue;
    }

    // Processes the Key value.
    private void ProcessKeyValue(CodeTokenizer _, string lineText
      , int lineIndex, string token, Color color)
    {
      int beginIndex;
      int textLength;

      // Find token and trailing space.
      string findToken = $"{token} ";
      beginIndex = lineText.IndexOf(findToken);
      if (-1 == beginIndex)
      {
        // Find token if at the end of the line.
        beginIndex = lineText.LastIndexOf(token);
      }
      textLength = token.Length;

      if (beginIndex > -1)
      {
        ColorSettings.Add(lineIndex, beginIndex, textLength, color);
      }
    }

    // Sets the Replacement values color.
    private void SetReplacementsColor(string lineText, int lineIndex)
    {
      int startIndex = 0;
      NetString.GetDelimitedAndIndexes(lineText, "_", out int beginIndex
        , out int endIndex, ref startIndex);
      if (endIndex > -1)
      {
        startIndex = 0;
        string text = NetString.GetStringWithDelimiters(lineText, "_"
          , ref startIndex);
        while (NetString.HasValue(text))
        {
          // Use if there are no embeded spaces.
          string[] tokens = text.Split(' ');
          if (1 == tokens.Length)
          {
            ColorSettings.Add(lineIndex, beginIndex, text.Length
              , mReplacementColor);
          }

          text = null;
          int saveStartIndex = startIndex;
          NetString.GetDelimitedAndIndexes(lineText, "_", out beginIndex
            , out endIndex, ref startIndex);
          if (endIndex > -1)
          {
            startIndex = saveStartIndex;
            text = NetString.GetStringWithDelimiters(lineText, "_", ref startIndex);
          }
        }
      }
    }

    // Adds the ColorSetting for the delimited string value.
    private void SetDelimitedColorSetting(string lineText, int lineIndex
      , string beginDelimiter, string endDelimiter, Color color)
    {
      int startIndex = 0;
      string text = NetString.GetDelimitedAndIndexes(lineText, beginDelimiter
        , out int beginIndex, out int _, ref startIndex, endDelimiter);
      if (NetString.HasValue(text))
      {
        int beginLength = beginDelimiter.Length;
        int startPosition = beginIndex + beginLength;
        ColorSettings.Add(lineIndex, startPosition, text.Length, color);
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the text lines.</summary>
    public List<string> Lines { get; set; }

    /// <summary>Gets or sets the ColorSettings collection.</summary>
    public ColorSettings ColorSettings { get; set; }

    // Gets or sets the XmlCommentInProcess flag.
    private bool IsSummaryInProcess { get; set; }

    // Gets or sets the XmlCommentInProcess flag.
    private bool IsReturnsInProcess { get; set; }
    #endregion

    #region Class Data

    //private readonly CodeTokenizer mTokens;
    private readonly Color mXmlLineColor = Color.Gray;
    private readonly Color mXMLCommentColor = Color.FromArgb(10, 114, 13);
    private readonly Color mCommentColor = Color.FromArgb(10, 114, 13);
    private readonly Color mModifierColor = Color.Blue;
    private readonly Color mKeywordColor = Color.Blue;
    //private readonly Color mUserTypeColor = Color.FromArgb(10, 125, 194);
    private readonly Color mDataTypeColor = Color.Blue;
    private readonly Color mRefTypeColor = Color.Blue;
    private readonly Color mLibTypeColor = Color.Blue;
    private readonly Color mReplacementColor = Color.FromArgb(10, 125, 194);
    #endregion
  }
}
