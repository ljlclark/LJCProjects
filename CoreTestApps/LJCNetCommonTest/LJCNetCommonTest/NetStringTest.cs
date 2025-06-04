// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// NetString.cs
using LJCNetCommon;
using System;

namespace LJCNetCommonTest
{
  internal class NetStringTest
  {
    public static void Test()
    {
      TestCommon = new TestCommon("NetString");
      Console.WriteLine();
      Console.WriteLine("*** NetString ***");

      GetDelimitedAndIndexes();
      GetDelimitedString();
      GetStringWithDelimiters();
    }

    // Get the delimited string begin and end index.
    private static void GetDelimitedAndIndexes()
    {
      // Get text that has different begin and end delimiter.
      var source = "<summary>This is some text.</summary>";
      int startIndex = 0;
      var beginDelimiter = "<summary>";
      var endDelimiter = "</summary>";
      var text = NetString.GetDelimitedAndIndexes(source, beginDelimiter
        , out int beginIndex, out int endIndex, ref startIndex, endDelimiter);
      var result = text;
      var compare = "This is some text.";
      TestCommon.Write("GetDelimitedAndIndexes()1: text", result, compare);
      result = beginIndex.ToString();
      compare = "0";
      TestCommon.Write("GetDelimitedAndIndexes()1: beginIndex", result, compare);
      result = endIndex.ToString();
      compare = "27";
      TestCommon.Write("GetDelimitedAndIndexes()1: endIndex", result, compare);
      result = startIndex.ToString();
      compare = "-1";
      TestCommon.Write("GetDelimitedAndIndexes()1: startIndex", result, compare);

      // Get text that has the same begin and end delimiter.
      // The endDelimiter is not specified or null.
      source = "|This is some text.|";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetDelimitedAndIndexes(source, beginDelimiter
        , out beginIndex, out endIndex, ref startIndex);
      result = text;
      compare = "This is some text.";
      TestCommon.Write("GetDelimitedAndIndexes()2: text", result, compare);
      result = beginIndex.ToString();
      compare = "0";
      TestCommon.Write("GetDelimitedAndIndexes()2: beginIndex", result, compare);
      result = endIndex.ToString();
      compare = "19";
      TestCommon.Write("GetDelimitedAndIndexes()2: endIndex", result, compare);
      result = startIndex.ToString();
      compare = "-1";
      TestCommon.Write("GetDelimitedAndIndexes()2: startIndex", result, compare);

      // Get text that has no end delimiter.
      source = "|This is some text.";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetDelimitedAndIndexes(source, beginDelimiter
        , out beginIndex, out endIndex, ref startIndex, "#NoDelimiter");
      result = text;
      compare = "This is some text.";
      TestCommon.Write("GetDelimitedAndIndexes()3: text", result, compare);
      result = beginIndex.ToString();
      compare = "0";
      TestCommon.Write("GetDelimitedAndIndexes()3: beginIndex", result, compare);
      result = endIndex.ToString();
      compare = "19";
      TestCommon.Write("GetDelimitedAndIndexes()3: endIndex", result, compare);
      result = startIndex.ToString();
      compare = "-1";
      TestCommon.Write("GetDelimitedAndIndexes()3: startIndex", result, compare);

      // Get delimited text where the delimiters occur multiple times.
      source = "|This is some text.| |and some more here.|";
      startIndex = 0;
      beginDelimiter = "|";
      var first = true;
      while (startIndex > -1)
      {
        text = NetString.GetDelimitedAndIndexes(source, beginDelimiter
          , out beginIndex, out endIndex, ref startIndex);
        if (first)
        {
          result = text;
          compare = "This is some text.";
          TestCommon.Write("GetDelimitedAndIndexes()4: text", result, compare);
          result = beginIndex.ToString();
          compare = "0";
          TestCommon.Write("GetDelimitedAndIndexes()4: beginIndex", result
            , compare);
          result = endIndex.ToString();
          compare = "19";
          TestCommon.Write("GetDelimitedAndIndexes()4: endIndex", result
            , compare);
          result = startIndex.ToString();
          compare = "20";
          TestCommon.Write("GetDelimitedAndIndexes()4: startIndex", result
            , compare);
        }
        else
        {
          result = text;
          compare = "and some more here.";
          TestCommon.Write("GetDelimitedAndIndexes()4: text", result, compare);
          result = beginIndex.ToString();
          compare = "21";
          TestCommon.Write("GetDelimitedAndIndexes()4: beginIndex", result
            , compare);
          result = endIndex.ToString();
          compare = "41";
          TestCommon.Write("GetDelimitedAndIndexes()4: endIndex", result
            , compare);
          result = startIndex.ToString();
          compare = "-1";
          TestCommon.Write("GetDelimitedAndIndexes()4: startIndex", result
            , compare);
        }
        first = false;
      }
    }

    // Gets the string between the specified delimiters.
    private static void GetDelimitedString()
    {
      var source = "<summary>This is some text.</summary>";
      var startIndex = 0;
      var beginDelimiter = "<summary>";
      var endDelimiter = "</summary>";

      var text = NetString.GetDelimitedString(source, beginDelimiter
        , ref startIndex, endDelimiter);
      var result = text;
      var compare = "This is some text.";
      TestCommon.Write("GetDelimitedString(): text", result, compare);
      result = startIndex.ToString();
      compare = "-1";
      TestCommon.Write("GetDelimitedString(): startIndex", result, compare);
    }

    // Get the string including the specified delimiters.
    private static void GetStringWithDelimiters()
    {
      var source = "<summary>This is some text.</summary>";
      var startIndex = 0;
      var beginDelimiter = "<summary>";
      var endDelimiter = "</summary>";

      // Get text that has different begin and end delimiter.
      var text = NetString.GetStringWithDelimiters(source, beginDelimiter
        , ref startIndex, endDelimiter);
      var result = text;
      var compare = "<summary>This is some text.</summary>";
      TestCommon.Write("GetStringWithDelimiters()1: text", result, compare);
      result = startIndex.ToString();
      compare = "-1";
      TestCommon.Write("GetStringWithDelimiters()1: startIndex", result
        , compare);

      // Get text that has the same begin and end delimiter.
      // The endDelimiter is not specified or null.
      source = "|This is some text.|";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetStringWithDelimiters(source, beginDelimiter
        , ref startIndex);
      result = text;
      compare = "|This is some text.|";
      TestCommon.Write("GetStringWithDelimiters()2: text", result, compare);
      result = startIndex.ToString();
      compare = "-1";
      TestCommon.Write("GetStringWithDelimiters()2: startIndex", result
        , compare);

      // Get text that has no end delimiter.
      source = "|This is some text.";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetStringWithDelimiters(source, beginDelimiter
        , ref startIndex, "#NoDelimiter");
      result = text;
      compare = "|This is some text.";
      TestCommon.Write("GetStringWithDelimiters()3: text", result, compare);
      result = startIndex.ToString();
      compare = "-1";
      TestCommon.Write("GetStringWithDelimiters()3: startIndex", result
        , compare);

      // Get delimited text where the delimiters occur multiple times.
      source = "|This is some text.| |and some more here.|";
      startIndex = 0;
      beginDelimiter = "|";
      var first = true;
      while (startIndex > -1)
      {
        text = NetString.GetStringWithDelimiters(source, beginDelimiter
          , ref startIndex);
        if (first)
        {
          result = text;
          compare = "|This is some text.|";
          TestCommon.Write("GetStringWithDelimiters()4: text", result, compare);
          result = startIndex.ToString();
          compare = "20";
          TestCommon.Write("GetStringWithDelimiters()4: startIndex", result
            , compare);
        }
        else
        {
          result = text;
          compare = "|and some more here.|";
          TestCommon.Write("GetStringWithDelimiters()5: text", result, compare);
          result = startIndex.ToString();
          compare = "-1";
          TestCommon.Write("GetStringWithDelimiters()5: startIndex", result
            , compare);
        }
        first = false;
      }
    }

    #region Class Data

    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
