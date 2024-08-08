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

  // Public Methods
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
        let line = lines[index].trim();

        // If blank line or comment.
        if (!LJC.HasValue(line)
          || line.trim().startsWith("//"))
        {
          continue;
        }

        let refLineType = new RefLineType("None");
        let lineItemRef = this.#ParseItem(line, refLineType);
        switch (refLineType.getName())
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

  // Line Parse Methods
  // ---------------

  // Parses a line into a line item.
  #ParseItem(line, refLineType)
  {
    let retValue = null;  // Item

    refLineType.setName("None");
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

  // Creates a Assignment line item.
  #ParseAssignment(tokens, refLineType)
  {
    let retValue = null; // Item

    refLineType.setName("None");
    if (tokens != null
      && tokens.length > 1
      && ("=" == tokens[1]))
    {
      refLineType.setName("Assignment");
      let tokenCount = tokens.length;
      let line = tokens.join(" ");
      let refSuccess = new RefBool(true);

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
    let retValue = null;  // Item

    refLineType.setName("None");
    if (tokens != null
      && tokens.length > 1
      && "is" == tokens[1].toLowerCase())
    {
      refLineType.setName("Formula");
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
    let retValue = null;  // Item

    refLineType.setName("None");
    if (tokens != null
      && tokens.length > 0)
    {
      for (let index = 0; index < tokens.length; index++)
      {
        let token = tokens[index];

        // Name/AltName
        if (token.includes("|"))
        {
          refLineType.setName("Name");
          retValue = new Item(null, 0);

          // Remove extra spaces.
          let refSuccess = new RefBool(true);
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
    let retValue = null;  // Item

    refLineType.setName("None");
    if (tokens != null
      && tokens.length > 1
      && "value:" == tokens[0].toLowerCase())
    {
      refLineType.setName("Value");
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

  // Other Parse Methods
  // ---------------

  // Clears the old value.
  #ClearValue(line)
  {
    let retValue = "";

    if ("string" == typeof line)
    {
      retValue = line;
      let valueIndex = retValue.indexOf("(");
      if (valueIndex > 0)
      {
        retValue = retValue.substring(0, valueIndex - 1);
      }
    }
    return retValue;
  }

  // Gets the token or data item value.
  #GetItemValue(line, token, refSuccess, syntax = null)
  {
    let retValue = -1.0;

    let refRetNumber = new RefNumber(-1.0);
    if (refSuccess.Value = this.#IsItemOrNumber(line, token, refRetNumber))
    {
      retValue = refRetNumber.Value;
      if (retValue < 0)
      {
        let findItem = this.#GetItem(token);
        if (null == findItem)
        {
          success.Value = false;
          Error.ItemNotFound(line, token, syntax);
        }
        else
        {
          retValue = findItem.Value;
        }
      }
    }
    return retValue;
  }

  // Checs if token is an operation.
  #GetOperation(line, token, refSuccess)
  {
    let retValue = "";

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
  //#IsItemOrNumber(line, token, refNumber, syntax = null)
  #IsItemOrNumber(line, token, refNumber)
  {
    let retValue = true;

    if (LJC.IsNumber(token))
    {
      refNumber.Value = Number(token);
    }
    else
    {
      refNumber.Value = -1.0;

      // Does not start with letter.
      if (typeof token[0] != "string")
      {
        retValue = false;
        Error.NameFormat(line, token);
      }
    }
    return retValue;
  }

  // Other Private Methods
  // ---------------

  // Performs an assignment calculation.
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

    // Calculate smaller formula totals.
    let prevItemRef = lineItemRef;
    let nextItemRef = this.#GetItem(prevItemRef.FormulaName);
    while (nextItemRef != null)
    {
      nextItemRef.Value = prevItemRef.Value * prevItemRef.FormulaValue;
      prevItemRef = nextItemRef;
      nextItemRef = this.#GetItem(nextItemRef.FormulaName);
    }

    // Calculate larger formula totals.
    // *** Begin ***
    let items = this.#GetItemsWithFormula(lineItemRef);
    for (let index = 0; index < items.length; index++)
    {
      // *** End ***
      prevItemRef = lineItemRef;
      //nextItemRef = this.#GetItemWithFormula(lineItemRef);
      nextItemRef = items[index];
      while (nextItemRef != null)
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
          retValue = Math.ceil(value);
          break;

        case "round":
          retValue = Math.round(value);
          break;

        case "floor":
          retValue = Math.floor(value);
          break;
      }

      if (rounding.toLowerCase().startsWith("round:"))
      {
        let tokens = rounding.split(":");
        let text = value.toFixed(tokens[1]);
        retValue = parseFloat(text);
      }
    }
    return retValue;
  }

  // Gets the Item by Name or AltName
  #GetItem(name)
  {
    let retValue = null;  // Item;

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

  // Gets all where the Formula name = item's Name or AltName.
  #GetItemsWithFormula(findItem)
  {
    let retValue = [];
    for (let index = 0; index < this.#Items.length; index++)
    {
      let success = false;
      let item = this.#Items[index];

      if (LJC.HasValue(findItem.Name))
      {
        let name = findItem.Name.toLowerCase();
        if (item.FormulaName.toLowerCase() == name)
        {
          success = true;
          retValue.push(item);
        }
      }

      if (!success
        && LJC.HasValue(findItem.AltName))
      {
        let altName = findItem.AltName.toLowerCase();
        if (item.FormulaName.toLowerCase() == altName)
        {
          retValue.push(item);
        }
      }
    }
    return retValue;
  }

  // Gets first where the Formula name = item's Name or AltName.
  #GetItemWithFormula(item)
  {
    let retValue = null;  // Item

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
      switch (refLineType.getName())
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

    if (!LJC.HasValue(retValue.FormulaName))
    {
      retValue.FormulaName = "";
    }
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
  #Name = "";

  // Initializes an object instance.
  constructor(name = "None")
  {
    this.setName(name);
  }

  getName()
  {
    return this.#Name;
  }

  setName(name)
  {
    let value = name.toLowerCase();
    if (value == "assignment"
      || value == "formula"
      || value == "name"
      || value == "none"
      || value == "value")
    {
      this.#Name = value;
    }
  }
}

// Represents a Number that can be modified in a called method.
class RefNumber
{
  // Initializes an object instance.
  constructor(value = 0.0)
  {
    if (LJC.IsNumber(value))
    {
      this.Value = value;
    }
  }
}

// Represents a boolean value that can be modified in a called method.
class RefBool
{
  // Initializes an object instance.
  constructor(value = true)
  {
    if ("boolean" == typeof value)
    {
      this.Value = value;
    }
  }
}

// Represents a Data Item.
class Item
{
  // Initializes an object instance.
  constructor(name = "", value = 0.0)
  {
    // The Item name.
    if (LJC.HasValue(name))
    {
      this.Name = name;
    }

    // The Item value.
    if (LJC.IsNumber(value))
    {
      this.Value = value;
    }

    // The Altername name.
    this.AltName = "";

    // The Formula value.
    this.FormulaValue = 0.0;

    // The Formula Item name.
    this.FormulaName = "";

    // The Rounding value.
    this.Rounding = "";

    // The Value Item name.
    this.ValueName = "";
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
