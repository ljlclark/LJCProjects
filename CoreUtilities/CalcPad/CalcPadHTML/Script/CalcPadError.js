// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CalcPadError.js
// <script src="../LJCCommon.js"></script>

// CalcPad error functions.
class Error
{
  // Static Functions
  // ---------------

  // Checks if the text is a number.
  static IsNumber(text)
  {
    let retValue = true;

    for (let index = 0; index < text.length; index++)
    {
      let ch = text.charAt(index);
      //let result = /^\d+$/.test(ch);
      let result = /^\d+/.test(ch);
      if (!result)
      {
        if (!"+-.".includes(ch))
        {
          retValue = false;
          break;
        }
      }
    }
    return retValue;
  }

  // Error messages.
  // ---------------

  // 
  static Declaration(line)
  {
    var message = "Declaration\r\n\r\n";
    message += `Line: ${line}\r\n`;
    message += "Missing Item name.";
    message += "\r\n\r\nDeclaration Syntax:\r\n";
    message += "  Name|AltName";
    alert(message);
  }

  // 
  static FormulaValue(line)
  {
    var message = "Formula Value\r\n\r\n";
    message += `Line: ${line}\r\n`;
    message += "Value must be a number.";
    message += "\r\n\r\n" + Error.SyntaxFormula();
    alert(message);
  }

  // 
  static ItemNotFound(line, itemName = null, syntax = null)
  {
    var message = "Item Not Found\r\n\r\n";
    message += `Line: ${line}\r\n`;
    if (LJC.HasValue(itemName))
    {
      message += `ItemName: ${itemName}\r\n`;
    }
    message += "Item name was not found.";
    if (syntax != null)
    {
      message += `\r\n\r\n${syntax}`;
    }
    alert(message);
  }

  // 
  static NameFormat(line, itemName = null)
  {
    var message = "Name Formmat\r\n\r\n";
    message += `Line: ${line}\r\n`;
    if (LJC.HasValue(itemName))
    {
      message += `ItemName: ${itemName}`;
    }
    message += "\r\n\r\n" + Error.SyntaxNameFormat();
    alert(message);
  }

  // 
  static Number(line, value, syntax = null)
  {
    var message = "Number\r\n\r\n";
    message += `Line: ${line}\r\n`;
    message += `Value ${value} is not a number`;
    if (syntax != null)
    {
      message += "\r\n\r\n" + syntax;
    }
    alert(message);
  }

  // 
  static Operation(line)
  {
    var message = "Operation\r\n\r\n";
    message += `Line: ${line}\r\n`;
    message += "Operator must be { + | - | * | / }.";
    message += "\r\n\r\n" + Error.SyntaxAssignment();
    alert(message);
  }

  // 
  static TokenCount(line, syntax = null)
  {
    var message = "Token Count\r\n\r\n";
    message += `Line: ${line}\r\n`;
    message += "Too few or too many values.";
    if (LJC.HasValue(syntax))
    {
      message += "\r\n\r\n" + syntax;
    }
    alert(message);
  }

  // Syntax strings.
  // ---------------

  // 
  static SyntaxAssignment()
  {
    let retValue = "Assignment Syntax:\r\n";
    retValue += "      ItemName = {ItemName|value}\r\n";
    retValue += "or  ItemName = {ItemName|value} ";
    retValue += " {+|-|*|/} {ItemName|Value}";
    return retValue;
  }

  // 
  static SyntaxFormula()
  {
    let retValue = "Formula Syntax:\r\n";
    retValue += "  LeftItemName is {value} RightItemName\r\n";
    retValue += "RightItemName must be smaller part of LeftItemName.";
    return retValue;
  }

  // 
  static SyntaxNameFormat()
  {
    let retValue = "Name Syntax:\r\n";
    retValue += "  A123\r\n";
    retValue += "A Item name must start with an Alpha character.";
    return retValue;
  }

  // 
  static SyntaxValue()
  {
    let retValue = "Value Syntax:\r\n";
    retValue += "  Value: ItemName { Ceililng | Floor | Round }";
    return retValue;
  }
}