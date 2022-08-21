// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// LJCRtControl.cs
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LJCWinFormCommon;

namespace LJCWinFormControls
{
  /// <summary>The enhanced Rich Text control.</summary>
  public partial class LJCRtControl : RichTextBox
  {
    #region Static Methods

    // Reads a file into an LJCRtControl and sets the leading tabs.
    /// <include path='items/LJCReadFileToRTF/*' file='Doc/LJCRtControl.xml'/>
    public static void LJCReadFileToRTF(string fileSpec, LJCRtControl rtfControl)
    {
      string line;

      if (false == File.Exists(fileSpec))
      {
        FormCommon.ShowError($"File '{fileSpec}' does not exist.");
      }
      else
      {
        rtfControl.Text = null;
        StreamReader reader = File.OpenText(fileSpec);
        while ((line = reader.ReadLine()) != null)
        {
          line = LJCPrepareLine(line);
          rtfControl.LJCAppendLine(line);
        }
        reader.Close();
      }
    }

    // Sets the text leading spaces to tabs. 
    /// <include path='items/LJCSetLeadingSpacesToTabs/*' file='Doc/LJCRtControl.xml'/>
    public static string LJCSetLeadingSpacesToTabs(string text, int leadingSpaces = 8)
    {
      StringBuilder builder;
      string retValue = text;

      int tabCount = LJCLeadingSpaceToTabCount(text, leadingSpaces);
      if (tabCount > 0)
      {
        builder = new StringBuilder(128);
        for (int index = 0; index < tabCount; index++)
        {
          builder.Append("\t");
        }
        builder.Append(text.Substring(tabCount * leadingSpaces));
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Sets the leading spaces to a different number of spaces.
    /// <include path='items/LJCSetLeadingSpacesToSpaces/*' file='Doc/LJCRtControl.xml'/>
    public static string LJCSetLeadingSpacesToSpaces(string text, int leadingSpaces = 8
      , int outputSpaces = 4)
    {
      StringBuilder builder;
      string retValue = text;

      int tabCount = LJCLeadingSpaceToTabCount(text, leadingSpaces);
      if (tabCount > 0)
      {
        builder = new StringBuilder(128);
        for (int index = 0; index < tabCount; index++)
        {
          builder.Append(string.Concat(Enumerable.Repeat(" ", outputSpaces)));
        }
        builder.Append(retValue.Substring(tabCount * leadingSpaces));
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Sets the leading tabs to spaces.
    /// <include path='items/LJCSetLeadingTabsToSpaces/*' file='Doc/LJCRtControl.xml'/>
    public static string LJCSetLeadingTabsToSpaces(string text, int outputSpaces = 4)
    {
      StringBuilder builder;
      string retValue = text;

      int tabCount = text.TakeWhile(x => x == '\t').Count();
      if (tabCount > 0)
      {
        builder = new StringBuilder(128);
        for (int index = 0; index < tabCount; index++)
        {
          builder.Append(new string(' ', outputSpaces));
        }
        builder.Append(retValue.Substring(tabCount));
        retValue = builder.ToString();
      }
      return retValue;
    }

    // Gets the number of tabs that are required based on the leadingSpaces
    /// <include path='items/LJCLeadingSpaceToTabCount/*' file='Doc/LJCRtControl.xml'/>
    public static int LJCLeadingSpaceToTabCount(string text, int leadingSpaces = 8)
    {
      int count = text.TakeWhile(x => x == ' ').Count();
      return count / leadingSpaces;
    }

    // Prepares the line for text editor display.
    /// <include path='items/LJCPrepareLine/*' file='Doc/LJCRtControl.xml'/>
    public static string LJCPrepareLine(string lineText)
    {
      string retValue;

      LJCSetLeadingSpacesToSpaces(lineText, 8, 2);
      retValue = LJCSetLeadingTabsToSpaces(lineText, 2);
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public LJCRtControl()
    {
      InitializeComponent();
    }
    #endregion

    #region Public Methods

    /// <summary>Retrieve the current line text.</summary>
    public string LJCGetCurrentLine()
    {
      string retValue;

      int lineIndex = LJCGetCurrentLineIndex();
      retValue = LJCGetLineText(lineIndex);
      return retValue;
    }

    /// <summary>Retrieve the current line index value.</summary>
    public int LJCGetCurrentLineIndex()
    {
      int retValue;

      int cursorPosition = SelectionStart;
      retValue = GetLineFromCharIndex(cursorPosition);
      return retValue;
    }

    // Reads a file into the Control and sets the leading tabs.
    /// <include path='items/LJCLoadFromFile/*' file='Doc/LJCRtControl.xml'/>
    public void LJCLoadFromFile(string fileSpec)
    {
      string line;

      if (false == File.Exists(fileSpec))
      {
        FormCommon.ShowError($"File '{fileSpec}' does not exist.");
      }
      else
      {
        Text = null;
        StreamReader reader = File.OpenText(fileSpec);
        while ((line = reader.ReadLine()) != null)
        {
          line = LJCPrepareLine(line);
          LJCAppendLine(line);
        }
        reader.Close();
      }
    }

    // Sets the text color for the delimited string value.
    /// <include path='items/LJCSetDelimitedTextColor/*' file='Doc/LJCRtControl.xml'/>
    public void LJCSetDelimitedTextColor(int lineIndex, string beginDelimiter
      , string endDelimiter, Color color)
    {
      int saveOffset = SelectionStart;
      Color saveColor = SelectionColor;

      int lineStartOffset = GetFirstCharIndexFromLine(lineIndex);
      int lineLength = Lines[lineIndex].Length;
      Select(lineStartOffset, lineLength);
      string text = SelectedText;

      int beginIndex;
      int endIndex;
      if (null == beginDelimiter)
      {
        beginIndex = 0;
      }
      else
      {
        beginIndex = text.IndexOf(beginDelimiter);
        beginIndex += beginDelimiter.Length;
      }
      if (null == endDelimiter)
      {
        endIndex = text.Length;
      }
      else
      {
        endIndex = text.IndexOf(endDelimiter, beginIndex);
      }

      if (beginIndex > -1
        && endIndex > -1)
      {
        // Begin and End delimiters are present.
        int textLength = endIndex - beginIndex;
        LJCSetTextColor(lineIndex, beginIndex, textLength, color);
      }
      Select(saveOffset, 0);
      SelectionColor = saveColor;
    }

    // Sets the text color from the beginning offset to length.
    /// <include path='items/LJCSetTextColor/*' file='Doc/LJCRtControl.xml'/>
    public void LJCSetTextColor(int lineIndex, int colorBeginIndex, int length
      , Color color)
    {
      int saveStartIndex = SelectionStart;
      Color saveColor = SelectionColor;

      int lineStartIndex = GetFirstCharIndexFromLine(lineIndex);
      int startIndex = lineStartIndex + colorBeginIndex;

      Select(startIndex, length);
      SelectionColor = color;

      Select(saveStartIndex, 0);
      SelectionColor = saveColor;
    }

    // Gets the text for the specified line.
    /// <include path='items/LJCGetLineText/*' file='Doc/LJCRtControl.xml'/>
    public string LJCGetLineText(int lineIndex)
    {
      string retValue;

      int saveOffset = SelectionStart;

      int lineStartOffset = GetFirstCharIndexFromLine(lineIndex);
      int lineLength = Lines[lineIndex].Length;
      Select(lineStartOffset, lineLength);
      retValue = SelectedText;

      Select(saveOffset, 0);
      return retValue;
    }
    #endregion

    #region Public Append Methods

    // Adds text to the control.
    /// <include path='items/LJCAppend1/*' file='Doc/LJCRtControl.xml'/>
    public void LJCAppend(string text)
    {
      if (text != null)
      {
        AppendText(text);
      }
    }

    // Adds text with a font.
    /// <include path='items/LJCAppend2/*' file='Doc/LJCRtControl.xml'/>
    public void LJCAppend(string text, FontStyle fontStyle)
    {
      LJCAppend(text, fontStyle, SelectionColor);
    }

    // Adds text with a font and a color.
    /// <include path='items/LJCAppend3/*' file='Doc/LJCRtControl.xml'/>
    public void LJCAppend(string text, FontStyle fontStyle, Color color)
    {
      if (text != null)
      {
        FontStyle saveFontStyle = Font.Style;
        Color saveColor = SelectionColor;
        SelectionFont = LJCSetFontStyle(fontStyle);
        SelectionColor = color;
        LJCAppend(text);
        SelectionFont = LJCSetFontStyle(saveFontStyle);
        SelectionColor = saveColor;
      }
    }

    // Adds a carriage return line feed.
    /// <include path='items/LJCAppendLine1/*' file='Doc/LJCRtControl.xml'/>
    public void LJCAppendLine()
    {
      LJCAppend("\r\n");
    }

    // Adds the text plus a CR/LF.
    /// <include path='items/LJCAppendLine2/*' file='Doc/LJCRtControl.xml'/>
    public void LJCAppendLine(string text)
    {
      LJCAppend(text + "\r\n");
    }

    // Adds text with a font and CR/LF.
    /// <include path='items/LJCAppendLine3/*' file='Doc/LJCRtControl.xml'/>
    public void LJCAppendLine(string text, FontStyle fontStyle)
    {
      LJCAppend(text + "\r\n", fontStyle);
    }

    // Adds text with a font, color and CR/LF.
    /// <include path='items/LJCAppendLine4/*' file='Doc/LJCRtControl.xml'/>
    public void LJCAppendLine(string text, FontStyle fontStyle, Color color)
    {
      LJCAppend(text + "\r\n", fontStyle, color);
    }
    #endregion

    #region Private Methods

    // Gets a font style.
    private Font LJCSetFontStyle(FontStyle style)
    {
      return new Font(Font.FontFamily, Font.Size, style);
    }
    #endregion
  }
}
