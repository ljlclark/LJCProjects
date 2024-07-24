// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// CalcPadCode.js
// <script src="Script/LJCCommon.js"></script>

// CalcPad functions.
class CalcPadCode
{
  // The Constructor function.
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
          && line.trim().StartsWith("//"))
        {
          continue;
        }

        let refLineType = new RefLineType("None");
        var lineItemRef = this.#ParseItem(line, refLineType);
        switch (refLineType)
        {
          case "Assignment":
            this.#DoCalc(lineItemRef);
            break;

          case "Value":
            var text = line;
            var valueIndex = text.IndexOf("(");
            if (valueIndex > 0)
            {
              text = text.Substring(0, valueIndex - 1);
            }

            var itemRef = this.#GetItem(lineItemRef.ValueName);
            var value = this.#DoRound(lineItemRef.Rounding, itemRef.Value);
            text += `(${value})`;
            //this.#ReplaceLine(CalcsRTB, index, text);
            break;
        }
      }
    }
  }

  // Line Parse Methods
  // ---------------

  // Error.FormulaValue
  // Error.ItemNotFound
  // Error.NameFormat
  // Error.Number
  // Error.Operation
  // Error.TokenCount
  #ParseItem(line, refLineType)
  {
    let retValue = "";
    retValue = null;

    refLineType = new RefLineType("None");
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

  // Creates a Formula Item.
  // Error.Declaration
  // Error.FormulaValue
  // Error.ItemNotFound
  // Error.NameFormat
  // Error.Number
  // Error.Operation
  // Error.TokenCount
  #ParseAssignment(tokens, refLineType)
  {
    let retValue = "";
    retValue = null;

    refLineType.Name = "None";
    if (tokens != null
      && tokens.Length > 1
      && ("=" == tokens[1].ToLower()))
    {
      refLineType.Name = "Assignment";
      let tokenCount = tokens.Length;
      let line = string.Join(" ", tokens);
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
        retValue = new Item()
        {
          Name = token
        };
        if (!char.IsLetter(token[0]))
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
        firstValue = this.GetItemValue(line, token, refSuccess
          , Error.SyntaxAssignment());
        if (3 == tokenCount)
        {
          retValue.Value = firstValue;
        }
      }

      // Get operation.
      operation = null;
      if (refSuccess.Value
        && tokenCount > 3)
      {
        let token = tokens[3];
        operation = this.GetOperation(line, token, refSuccess);
      }

      if (refSuccess.Value
        && tokenCount > 4)
      {
        // Get second value.
        let token = tokens[4];
        let secondValue = this.GetItemValue(line, token
          , refSuccess, Error.SyntaxAssignment());
        retValue.Value = this.Calc(firstValue, operation, secondValue);
      }

      if (refSuccess.Value)
      {
        retValue = this.SaveItem(retValue, lineType);
      }
    }
    return retValue;
  }

  // Creates a Formula Item.
  // Error.FormulaValue
  // Error.NameFormat
  // Error.TokenCount
  #ParseFormula(tokens, refLineType)
  {
    let retValue = null;

    refLineType.Name = "None";
    if (tokens != null
      && tokens.length > 1
      && "is" == tokens[1].ToLower())
    {
      refLineType.Name = "Formula";
      let tokenCount = tokens.length;
      let line = string.Join(" ", tokens);
      let success = true;

      // Name is # Name
      if (tokenCount < 4
        || tokenCount > 4)
      {
        success = false;
        Error.TokenCount(line, Error.SyntaxFormula());
      }

      retValue = new Item()
      {
        Name = tokens[0]
      };

      if (tokenCount > 2)
      {
        let token = tokens[2];
        if (!double.TryParse(token, value))
        {
          if (!char.IsLetter(token[0]))
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

      if (tokenCount > 3)
      {
        retValue.FormulaName = tokens[3];
      }

      if (success)
      {
        retValue = this.#SaveItem(retValue, lineType);
      }
    }
    return retValue;
  }

  // Creates a Name Item.
  // Error.Declaration
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

          retValue = new Item()
          {
            this.Value = 0
          };

          // Remove extra spaces.
          let refSuccess = new RefSuccess(true);
          //let text = String.Join("", tokens);
          let text = tokens.join("");
          let names = this.#SplitChars("|", text);
          if (0 == names.length)
          {
            refSuccess.Value = false;
            let line = string.Join(" ", tokens);
            Error.Declaration(line);
          }

          if (refSuccess.Value)
          {
            retValue.Name = names[0];
            if (names.length > 1)
            {
              if (LJC.HasValue(names[1]))
              {
                retValue.AltName = names[1];
              }
            }
            retValue = this.#SaveItem(retValue, refLineType);
          }
          index = tokens.length;
        }
      }
    }
    return retValue;
  }

  // Creates a Display Value Item.
  // Error.ItemNotFound
  // Error.TokenCount
  #ParseValue(tokens, refLineType)
  {
    let retValue = null;

    refLineType.Name = "None";
    if (tokens != null
      && tokens.length > 1
      && "value:" == tokens[0].ToLower())
    {
      refLineType.Name = "Value";
      let tokenCount = tokens.length;
      let line = string.Join(" ", tokens);

      // Value: Name
      if (tokenCount < 2
        || tokenCount > 4)
      {
        Error.TokenCount(line, Error.SyntaxValue());
      }

      retValue = new Item();
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

  // Gets the token or item value.
  // Error.ItemNotFound
  // Error.NameFormat
  // Error.Number
  #GetItemValue(line, token, refSuccess, syntax = null)
  {
    refRetValue = new RefNumber(-1.0);

    if (refSuccess.Value = this.#IsItemOrNumber(line, token, refRetValue))
    {
      if (refRetValue.Value < 0)
      {
        let findItem = this.#GetItem(token);
        if (null == findItem)
        {
          success.Value = false;
          Error.ItemNotFound(line, token, syntax);
        }
        else
        {
          refRetValue.Value = findItem.Value;
        }
      }
    }
    return refRetValue;
  }

  // Checs if tokenis an operation.
  // Error.Operation
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
  // Error.NameFormat
  // Error.Number
  #IsItemOrNumber(line, token, refNumber, syntax = null)
  {
    let retValue = true;

    if (!double.TryParse(token, refNumber))
    {
      refNumber.Value = -1.0;

      // Not a number and does not start with letter.
      if (!Error.IsNumber(token)
        && !char.IsLetter(token[0]))
      {
        retValue = false;
        Error.NameFormat(line, token);
      }

      if (retValue
        && char.IsNumber(token[0])
        && !Error.IsNumber(token))
      {
        retValue = false;
        Error.Number(line, token, syntax);
      }
    }
    return retValue;
  }

  // Other Private Methods
  // ---------------

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
    // "LeftName" is # "RightNames"
    // Calculate RightItem total.
    let prevItemRef = lineItemRef;
    let nextItemRef = null;
    if (!string.IsNullOrWhiteSpace(lineItemRef.FormulaName))
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

    if (!string.IsNullOrWhiteSpace(rounding))
    {
      switch (rounding.ToLower())
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
      retValue = this.#Items.find(x => x.Name.equalsIgnoreCase(name));
      if (null == retValue)
      {
        retValue = this.#Items.find(x => x.AltName.equalsIgnoreCase(name));
      }
    }
    return retValue;
  }

  // Gets the Formula name by Items Name or AltNames
  #GetItemWithFormula(item)
  {
    let retValue = new Item();
    retValue = null;

    if (item != null)
    {
      if (!string.IsNullOrWhiteSpace(item.Name))
      {
        retValue = this.Items.Find(x => x.FormulaName == item.Name);
      }
      if (null == retValue)
      {
        if (!string.IsNullOrWhiteSpace(item.AltName))
        {
          retValue = this.Items.Find(x => x.FormulaName == item.AltName);
        }
      }
    }
    return retValue;
  }

  // Saves or Updates the Item.
  #SaveItem(item, RefLineType)
  {
    var retValue = this.#GetItem(item.Name);
    if (null == retValue)
    {
      retValue = item;
      //this.#Items.Add(retValue);
      this.#Items.push(retValue);
    }
    else
    {
      // Update Item values.
      switch (refLineType.Name)
      {
        case "Assignment":
          retValue.Value = item.Value;
          break;

        case "Formula":
          retValue.FormulaValue = item.FormulaValue;
          retValue.FormulaName = item.FormulaName;
          break;

        case "Name":
          retValue.AltName = item.AltName;
          break;

        case "Value":
          retValue.Rounding = item.Rounding;
          break;
      }
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

  // Properties
  // ---------------

  // The defined items.
  #Items = [];
}

class RefNumber
{
  // The Constructor function.
  constructor(value)
  {
    this.Value = 0.0;
    this.Value = value;
  }
}

class RefLineType
{
  // The Constructor function.
  constructor(name)
  {
    this.Name = "";
    this.Name = name;
  }
}

class RefSuccess
{
  // The Constructor function.
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
