using System.Linq;
using System.Windows.Forms;

namespace CalcPad
{
  internal class Error
  {
    #region Static Functions

    // 
    internal static bool IsNumber(string text)
    {
      bool retValue = true;

      foreach (char ch in text)
      {
        if (!char.IsNumber(ch))
        {
          if (!"+-.".Contains(ch))
          {
            retValue = false;
            break;
          }
        }
      }
      return retValue;
    }
    #endregion
    #region Error messages.

    // 
    internal static void Declarations(string line)
    {
      var message = $"Line: {line}\r\n";
      message += "Missing Item name.";
      message += "\r\n\r\nDeclaration Syntax:\r\n";
      message += "  Name|AltName";
      MessageBox.Show(message);
    }

    // 
    internal static void FormulaValue(string line)
    {
      var message = $"Line: {line}\r\n";
      message += "Value must be a number.";
      message += "\r\n\r\n" + SyntaxFormula();
      MessageBox.Show(message);
    }

    // 
    internal static void NameFormat(string line, string itemName= null)
    {
      var message = $"Line: {line}\r\n";
      if (!string.IsNullOrWhiteSpace(itemName))
      {
        message += $"ItemName: {itemName}";
      }
      message += "\r\n\r\n" + SyntaxNameFormat();
      MessageBox.Show(message);
    }

    // 
    internal static void ItemNotFound(string line, string itemName = null
      , string syntax = null)
    {
      var message = $"Line: {line}";
      if (!string.IsNullOrWhiteSpace(itemName))
      {
        message += $"\r\nItemName: {itemName}\r\n";
      }
      message += "Item name was not found.";
      if (syntax != null)
      {
        message += $"\r\n\r\n{syntax}";
      }
      MessageBox.Show(message);
    }

    // 
    internal static void Number(string line, string value
      , string syntax = null)
    {
      var message = $"Line: {line}\r\n";
      message += $"Value {value} is not a number";
      if (syntax != null)
      {
        message += "\r\n\r\n" + syntax;
      }
      MessageBox.Show(message);
    }

    // 
    internal static void Operation(string line)
    {
      var message = $"Line: {line}\r\n";
      message += "Operator must be { + | - | * | / }.";
      message += "\r\n\r\n" + SyntaxAssignment();
      MessageBox.Show(message);
    }

    // 
    internal static void TokenCount(string line, string syntax= null)
    {
      var message = $"Line: {line}\r\n";
      message += "Too few or too many values.";
      if (!string.IsNullOrWhiteSpace(syntax))
      {
        message += "\r\n\r\n" + syntax;
      }
      MessageBox.Show(message);
    }
    #endregion

    #region Syntax strings.

    // 
    internal static string SyntaxAssignment()
    {
      var retValue = "Assignment Syntax:\r\n";
      retValue += "      ItemName = {ItemName|value}\r\n";
      retValue += "or  ItemName = {ItemName|value} ";
      retValue += " {+|-|*|/} {ItemName|Value}";
      return retValue;
    }

    // 
    internal static string SyntaxFormula()
    {
      var retValue = "Formula Syntax:\r\n";
      retValue += "  LeftItemName is {value} RightItemName\r\n";
      retValue += "RightItemName must be smaller part of LeftItemName.";
      return retValue;
    }

    // 
    internal static string SyntaxNameFormat()
    {
      var retValue = "Name Syntax:\r\n";
      retValue += "  A123\r\n";
      retValue += "A Item name must start with an Alpha character.";
      return retValue;
    }

    // 
    internal static string SyntaxValue()
    {
      var retValue = "Value Syntax:\r\n";
      retValue += "  Value: ItemName { Ceililng | Floor | Round }";
      return retValue;
    }
    #endregion
  }
}
