using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalcPad
{
  internal class Errors
  {
    #region Error messages.

    // 
    internal static void Declarations(string line)
    {
      var message = $"Line: {line}\r\n";
      message += "Missing Identifier name.";
      message += "\r\n\r\nDeclaration Syntax:\r\n";
      message += "  IDName|AltIDName";
      MessageBox.Show(message);
    }

    // 
    internal static void IDName(string text, string line)
    {
      var message = $"Line: {line}\r\n";
      message += $"{text} Invalid ID name.";
      MessageBox.Show(message);
    }

    // 
    internal static void IDNotFound(string text, string line
      , string syntax = null)
    {
      var message = $"Line: {line}\r\n";
      message += $"{text} ID name was not found.";
      if (syntax != null)
      {
        message += $"\r\n\r\n{syntax}";
      }
      MessageBox.Show(message);
    }

    // 
    internal static void InvalidOperator(string line)
    {
      var message = $"Line: {line}\r\n";
      message += "Operator must be { + | - | * | / }.";
      message += "\r\n\r\n" + SyntaxAssignment();
      MessageBox.Show(message);
    }

    // 
    internal static void Syntax(string line, string syntax)
    {
      var message = $"Line: {line}\r\n";
      message += "Too few or too many values.";
      message += "\r\n\r\n" + syntax;
      MessageBox.Show(message);
    }

    // 
    internal static void Value(string line)
    {
      var message = $"Line: {line}\r\n";
      message += "Value must be a number.";
      message += "\r\n\r\n" + SyntaxFormula();
      MessageBox.Show(message);
    }
    #endregion

    #region Syntax strings.

    // 
    internal static string SyntaxAssignment()
    {
      var retValue = "Assignment Syntax:\r\n";
      retValue += "     IDName = {IDName|value}\r\n";
      retValue += "or  IDName = {IDName|value} ";
      retValue += " {+|-|*|/} {IDName|Value}";
      return retValue;
    }

    // 
    internal static string SyntaxFormula()
    {
      var retValue = "Formula Syntax:\r\n";
      retValue += "  FirstIDName is {value} SecondIDName\r\n";
      retValue += "SecondIDName must be smaller part of FirstIDName.";
      return retValue;
    }

    // 
    internal static string SyntaxTotal()
    {
      var retValue = "Total Syntax:\r\n";
      retValue += "  Total: IDName { Ceililng | Floor | Round }";
      return retValue;
    }
    #endregion
  }
}
