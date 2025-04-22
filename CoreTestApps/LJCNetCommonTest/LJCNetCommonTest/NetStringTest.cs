using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJCNetCommonTest
{
  internal class NetStringTest
  {
    public static void Test()
    {
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
      // text = "This is some text.";
      // beginIndex = 0;
      // endIndex = 27;
      // startIndex = -1;

      // Get text that has the same begin and end delimiter.
      // The endDelimiter is not specified or null.
      source = "|This is some text.|";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetDelimitedAndIndexes(source, beginDelimiter
        , out beginIndex, out endIndex, ref startIndex);
      // text = "This is some text.";
      // beginIndex = 0;
      // endIndex = 19;
      // startIndex = -1;

      // Get text that has no end delimiter.
      source = "|This is some text.";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetDelimitedAndIndexes(source, beginDelimiter
        , out beginIndex, out endIndex, ref startIndex, "#NoDelimiter");
      // text = "This is some text.";
      // beginIndex = 0;
      // endIndex = 19;
      // startIndex = -1;

      // Get delimited text where the delimiters occur multiple times.
      source = "|This is some text.| |and some more here.|";
      startIndex = 0;
      beginDelimiter = "|";
      while (startIndex > -1)
      {
        text = NetString.GetDelimitedAndIndexes(source, beginDelimiter
          , out beginIndex, out endIndex, ref startIndex);
        // First time: text = "This is some text.";
        // beginIndex = 0;
        // endIndex = 19;
        // startIndex = 20;
        // Second time: text = "and some more here.";
        // beginIndex = 21;
        // endIndex = 41;
        // startIndex = -1
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
      // text = "This is some text.";
      // startIndex = -1;
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
      // text = "<summary>This is some text.</summary>";
      // startIndex = -1;

      // Get text that has the same begin and end delimiter.
      // The endDelimiter is not specified or null.
      source = "|This is some text.|";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetStringWithDelimiters(source, beginDelimiter
        , ref startIndex);
      // text = "|This is some text.|";
      // startIndex = -1;

      // Get text that has no end delimiter.
      source = "|This is some text.";
      startIndex = 0;
      beginDelimiter = "|";
      text = NetString.GetStringWithDelimiters(source, beginDelimiter
        , ref startIndex, "#NoDelimiter");
      // text = "|This is some text.";
      // startIndex = -1;

      // Get delimited text where the delimiters occur multiple times.
      source = "|This is some text.| |and some more here.|";
      startIndex = 0;
      beginDelimiter = "|";
      while (startIndex > -1)
      {
        text = NetString.GetStringWithDelimiters(source, beginDelimiter
          , ref startIndex);
        // First time: text = "|This is some text.|";
        // startIndex = 20;
        // Second time: text = "|and some more here.|";
        // startIndex = -1
      }
    }
  }
}
