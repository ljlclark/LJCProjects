// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CalcPadCode.js
// <script src="Script/LJCCommon.js"></script>
// <script src="Script/TextAreaCode.js"></script>

// CalcPad functions.
class CalcPadCode
{
  // The defined items.
  #Items = [];

  // Initializes an object instance.
  constructor()
  {
  }

  // Methods
  // ---------------

  // Perform the calculations.
  DoCalcs(text)
  {
    let success = true;
    if (null == text)
    {
      success = false;
    }

    let lines = [];
    if (success)
    {
      lines = text.split("\n");
      if (null == lines
        || !Array.isArray(lines)
        || lines.length < 1)
      {
        success = false;
      }
    }

    if (success)
    {
      for (let index = 0; index < lines.length; index++)
      {
        let line = lines[index].trimEnd();

        // If blank line or comment.
        if (!LJC.HasValue(line)
          && line.trim().startsWith("//"))
        {
          continue;
        }

        let refLineType = new RefLineType("");
        let lineItemRef = this.#ParseItem(line, refLineType);
        switch (refLineType.Name.toLowerCase())
        {
          case "assignment":
            this.#DoCalc(lineItemRef);
            break;

          case "value":
            text = this.#ClearValue(line);
            let itemRef = this.#GetItem(lineItemRef.ValueName);
            let value = this.#DoRound(lineItemRef.Rounding, itemRef.Value);
            text += ` (${value})`;

            lines[index] = text;
            let calcCode = new TextAreaCode(calcPad);
            calcCode.ReplaceLine(index, text);
            break;
        }
      }
    }
  }

  // Clears the old value.
  #ClearValue(line)
  {
    let retValue = line;
    let valueIndex = retValue.indexOf("(");
    if (valueIndex > 0)
    {
      retValue = retValue.Substring(0, valueIndex - 1);
    }
    return retValue;
  }

  // Line Parse Methods
  // ---------------

  // Parses a line int a line item.
  #ParseItem(line, refLineType)
  {
    let retValue = "";
    retValue = null;

    refLineType.Name = "None";
    let tokens = this.#SplitChars(" ", line);
    if (tokens.length > 0)
    {
      retValue = this.#ParseName(tokens, refLineType);
      if (null == retValue)
      {
        retValue = this.#ParseFormula(tokens, refLineType);
      }

      if (null == retValue)
      {
        retValue = this.#ParseAssignment(tokens, refLineType);
      }

      if (null == retValue)
      {
        retValue = this.#ParseValue(tokens, refLineType);
      }
    }
    return retValue;
  }

  // Creates a Formula line item.
  #ParseAssignment(tokens, refLineType)
  {
    let retValue = "";
    retValue = null;

    refLineType.Name = "None";
    if (tokens != null
      && tokens.length > 1
      && ("=" == tokens[1].toLowerCase()))
    {
      refLineType.Name = "Assignment";
      let tokenCount = tokens.length;
      let line = tokens.join(" ");
      let refSuccess = new RefSuccess(true);

      // Name = name/value operator name/value.
      if (tokenCount < 3
        || tokenCount > 5)
      {
        success.Value = false;
        Error.TokenCount(line, Error.SyntaxAssignment());
      }

      // Check assignment target name.
      if (refSuccess.Value)
      {
        let token = tokens[0];
        retValue = new Item(token, 0);
        // *** Next Statement *** Change
        if (!"string" == typeof token[0])
        {
          success.Value = false;
          Error.NameFormat(line, token);
        }
      }

      // Get first value.
      let firstValue = 0.0;
      if (refSuccess.Value
        && tokenCount > 2)
      {
        let token = tokens[2];
        firstValue = this.#GetItemValue(line, token, refSuccess
          , Error.SyntaxAssignment());
        if (3 == tokenCount)
        {
          retValue.Value = firstValue;
        }
      }

      // Get operation.
      let operation = null;
      if (refSuccess.Value
        && tokenCount > 3)
      {
        let token = tokens[3];
        operation = this.#GetOperation(line, token, refSuccess);
      }

      if (refSuccess.Value
        && tokenCount > 4)
      {
        // Get second value.
        let token = tokens[4];
        let secondValue = this.#GetItemValue(line, token
          , refSuccess, Error.SyntaxAssignment());
        retValue.Value = this.#Calc(firstValue, operation, secondValue);
      }

      if (refSuccess.Value)
      {
        retValue = this.#SaveItem(retValue, refLineType);
      }
    }
    return retValue;
  }

  // Creates a Formula line item.
  #ParseFormula(tokens, refLineType)
  {
    let retValue = null;

    refLineType.Name = "None";
    if (tokens != null
      && tokens.length > 1
      && "is" == tokens[1].toLowerCase())
    {
      refLineType.Name = "Formula";
      let tokenCount = tokens.length;
      let line = tokens.join(" ");
      let success = true;

      // Name is # Name
      if (tokenCount < 4
        || tokenCount > 4)
      {
        success = false;
        Error.TokenCount(line, Error.SyntaxFormula());
      }

      retValue = new Item(tokens[0], 0);

      let value = 0;
      if (tokenCount > 2)
      {
        let token = tokens[2];
        let pattern = /[0-9\-.]+/;
        if (pattern.test(token))
        {
          value = Number(token);
        }
        else
        {
          // *** Next Statement *** Change
          if (!"string" == typeof token[0])
          {
            success = false;
            Error.NameFormat(line, token);
          }
        }

        if (0 == value)
        {
          let findItem = this.#GetItem(token);
          if (null == findItem)
          {
            success = false;
            Error.FormulaValue(line);
          }
          else
          {
            value = findItem.Value;
          }
        }
        retValue.FormulaValue = value;
      }

      // *** Next Statement *** Add
      retValue.FormulaName = "";
      if (tokenCount > 3)
      {
        retValue.FormulaName = tokens[3];
      }

      if (success)
      {
        retValue = this.#SaveItem(retValue, refLineType);
      }
    }
    return retValue;
  }

  // Creates a Name line item.
  #ParseName(tokens, refLineType)
  {
    let retValue = null;

    refLineType.Name = "None";
    if (tokens != null
      && tokens.length > 0)
    {
      for (let index = 0; index < tokens.length; index++)
      {
        let token = tokens[index];

        // Name/AltName
        if (token.includes("|"))
        {
          refLineType.Name = "Name";
          retValue = new Item(null, 0);

          // Remove extra spaces.
          let refSuccess = new RefSuccess(true);
          let text = tokens.join("");
          let names = this.#SplitChars("|", text);
          if (0 == names.length)
          {
            refSuccess.Value = false;
            let line = tokens.join(" ");
            Error.Declaration(line);
          }

          if (refSuccess.Value)
          {
            retValue.Name = names[0];
            if (names.length > 1
              && LJC.HasValue(names[1]))
            {
              retValue.AltName = names[1];
            }
            retValue = this.#SaveItem(retValue, refLineType);
          }
          index = tokens.length;
        }
      }
    }
    return retValue;
  }

  // Creates a Display Value line item.
  #ParseValue(tokens, refLineType)
  {
    let retValue = null;

    refLineType.Name = "None";
    if (tokens != null
      && tokens.length > 1
      && "value:" == tokens[0].toLowerCase())
    {
      refLineType.Name = "Value";
      let tokenCount = tokens.length;
      let line = tokens.join(" ");

      // Value: Name
      if (tokenCount < 2
        || tokenCount > 4)
      {
        Error.TokenCount(line, Error.SyntaxValue());
      }

      retValue = new Item(null, 0);
      if (tokenCount > 1)
      {
        let token = tokens[1];
        retValue.ValueName = token;
        let findItem = this.#GetItem(token);
        if (null == findItem)
        {
          Error.ItemNotFound(token, line);
        }
      }

      if (tokenCount > 2)
      {
        retValue.Rounding = tokens[2];
      }
    }
    return retValue;
  }

  // Gets the token or data item value.
  #GetItemValue(line, token, refSuccess, syntax = null)
  {
    let refRetNumber = new RefNumber(-1.0);

    if (refSuccess.Value = this.#IsItemOrNumber(line, token, refRetNumber))
    {
      if (refRetNumber.Value < 0)
      {
        let findItem = this.#GetItem(token);
        if (null == findItem)
        {
          success.Value = false;
          Error.ItemNotFound(line, token, syntax);
        }
        else
        {
          refRetNumber.Value = findItem.Value;
        }
      }
    }
    return refRetNumber;
  }

  // Checs if token is an operation.
  #GetOperation(line, token, refSuccess)
  {
    let retValue = null;

    refSuccess.Value = true;
    if ("+-*/".includes(token))
    {
      retValue = token;
    }
    else
    {
      refSuccess.Value = false;
      Error.Operation(line);
    }
    return retValue;
  }

  // Check if token is item name or number.
  #IsItemOrNumber(line, token, refNumber, syntax = null)
  {
    let retValue = true;

    if (Error.IsNumber(token))
    {
      refNumber.Value = Number(token);
    }
    else
    {
      refNumber.Value = -1.0;

      // Not a number and does not start with letter.
      if (typeof token[0] != "string")
      {
        retValue = false;
        Error.NameFormat(line, token);
      }

      if (retValue)
      {
        retValue = false;
        Error.Number(line, token, syntax);
      }
    }
    return retValue;
  }

  // Other Private Methods
  // ---------------

  // Persorms an assignment calculation.
  #Calc(firstValue, operation, secondValue)
  {
    let retValue = 0.0;

    if (firstValue > 0
      && secondValue > 0)
    {
      switch (operation)
      {
        case "+":
          retValue = firstValue + secondValue;
          break;
        case "-":
          retValue = firstValue - secondValue;
          break;
        case "*":
          retValue = firstValue * secondValue;
          break;
        case "/":
          retValue = firstValue / secondValue;
          break;
      }
    }
    return retValue;
  }

  // Performs the related conversion calculations.
  #DoCalc(lineItemRef)
  {
    // "LeftName" is # "RightName"
    // Calculate RightItem total.
    let prevItemRef = lineItemRef;
    let nextItemRef = null;
    if (LJC.HasValue(lineItemRef.FormulaName))
    {
      var formulaItemRef = this.#GetItem(lineItemRef.FormulaName);
      if (formulaItemRef != null)
      {
        formulaItemRef.Value = lineItemRef.Value * lineItemRef.FormulaValue;
        nextItemRef = this.#GetItemWithFormula(lineItemRef);
      }
    }

    if (null == nextItemRef)
    {
      // Look for related conversion.
      nextItemRef = this.#GetItemWithFormula(lineItemRef);
    }

    // Calculate related formula totals.
    while (nextItemRef != null)
    {
      if (nextItemRef != null)
      {
        nextItemRef.Value = prevItemRef.Value / nextItemRef.FormulaValue;
        prevItemRef = nextItemRef;
        nextItemRef = this.#GetItemWithFormula(nextItemRef);
      }
    }
  }

  // Performs the rounding.
  #DoRound(rounding, value)
  {
    let retValue = value;

    if (LJC.HasValue(rounding))
    {
      switch (rounding.toLowerCase())
      {
        case "ceiling":
          retValue = Math.Ceiling(value);
          break;

        case "round":
          retValue = Math.Round(value);
          break;

        case "floor":
          retValue = Math.Floor(value);
          break;
      }
    }
    return retValue;
  }

  // Gets the Item by Name or AltName
  #GetItem(name)
  {
    let retValue = new Item();
    retValue = null;

    if (LJC.HasValue(name))
    {
      name = name.toLowerCase();
      retValue = this.#Items.find(x => x.Name.toLowerCase() == name);
      if (null == retValue)
      {
        retValue = this.#Items.find(x => x.AltName.toLowerCase() == name);
      }
    }
    return retValue;
  }

  // Gets the Formula name by Item's Name or AltNames
  #GetItemWithFormula(item)
  {
    let retValue = new Item();
    retValue = null;

    if (item != null)
    {
      if (LJC.HasValue(item.Name))
      {
        let name = item.Name.toLowerCase();
        retValue = this.#Items.find(x => x.FormulaName.toLowerCase() == name);
      }
      if (null == retValue
        && LJC.HasValue(item.AltName))
      {
        let altName = item.AltName.toLowerCase();
        retValue = this.#Items.find(x => x.FormulaName.toLowerCase() == altName);
      }
    }
    return retValue;
  }

  // Saves or Updates the Item.
  #SaveItem(lineItem, refLineType)
  {
    var retValue = this.#GetItem(lineItem.Name);
    if (null == retValue)
    {
      retValue = lineItem;
      this.#Items.push(retValue);
    }
    else
    {
      // Update Item values.
      switch (refLineType.Name.toLowerCase())
      {
        case "assignment":
          retValue.Value = lineItem.Value;
          break;

        case "formula":
          retValue.FormulaValue = lineItem.FormulaValue;
          retValue.FormulaName = lineItem.FormulaName;
          break;

        case "name":
          retValue.AltName = lineItem.AltName;
          break;

        case "value":
          retValue.Rounding = lineItem.Rounding;
          break;
      }
    }

    // *** Begin *** Add
    if (!LJC.HasValue(retValue.FormulaName))
    {
      retValue.FormulaName = "";
    }
    // *** End   *** Add
    return retValue;
  }

  // Performs a split with no empty entries.
  #SplitChars(separators, text)
  {
    if (separators == " ")
    {
      separators = "\\s+";
    }
    else
    {
      separators = `[${separators}]`;
    }
    let regex = new RegExp(separators, "g");
    let retValue = text.split(regex);
    return retValue;
  }
}

class RefLineType
{
  // Initializes an object instance.
  constructor(name)
  {
    this.Name = "";
    this.Name = name;
  }
}

class RefNumber
{
  // Initializes an object instance.
  constructor(value)
  {
    this.Value = 0.0;
    this.Value = value;
  }
}

class RefSuccess
{
  // Initializes an object instance.
  constructor(value)
  {
    this.Value = false;
    this.Value = value;
  }
}

// Represents a Data Item.
class Item
{
  // Initializes an object instance.
  constructor(name, value)
  {
    // The Item name.
    this.Name = "";
    this.Name = name;

    // The Item value.
    this.Value = 0.0;
    this.Value = value;

    // The Altername name.
    this.AltName = "";
    this.AltName = null;

    // The Formula value.
    this.FormulaValue = 0.0;

    // The Formula Item name.
    this.FormulaName = "";
    this.FormulaName = null;

    // The Rounding value.
    this.Rounding = "";
    this.Rounding = null;

    // The Value Item name.
    this.ValueName = "";
    this.ValueName = null;
  }

  // Methods
  // ---------------

  // Gets int value.
  ValueIfInt(refNumber)
  {
    let retValue = false;

    refNumber.Value = 0;
    if (0 == Value % 1)
    {
      refNumber.Value = this.Value; // (int)
      retValue = true;
    }
    return retValue;
  }
}
