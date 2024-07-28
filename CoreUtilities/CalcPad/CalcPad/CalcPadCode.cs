using LJCNetCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CalcPad
{
  public class CalcPadCode
  {
    // Initializes an object instance.
    public CalcPadCode()
    {
      Items = new List<Item>();
    }

    #region Public Methods
    public void DoCalcs(RichTextBox rtb)
    {
      RTB = rtb;
      var lines = rtb.Lines;
      for (int index = 0; index < lines.Count(); index++)
      {
        var line = lines[index].Trim();

        // If blank line or comment.
        if (!NetString.HasValue(line)
          || line.Trim().StartsWith("//"))
        {
          continue;
        }

        var lineItemRef = ParseItem(line, out LineType lineType);
        switch (lineType)
        {
          case LineType.Assignment:
            DoCalc(lineItemRef);
            break;

          case LineType.Value:
            var text = ClearValue(line);
            var itemRef = GetItem(lineItemRef.ValueName);
            var value = DoRound(lineItemRef.Rounding, itemRef.Value);
            text += $" ({value})";

            ReplaceLine(RTB, index, text);
            break;
        }
      }
    }
    #endregion

    #region Line Parse Methods

    // Parses a line into a line item.
    public Item ParseItem(string line, out LineType lineType)
    {
      Item retValue = null;

      lineType = LineType.None;
      var tokens = SplitChars(" ", line);
      if (tokens.Length > 0)
      {
        retValue = ParseName(tokens, out lineType);
        if (null == retValue)
        {
          retValue = ParseFormula(tokens, out lineType);
        }

        if (null == retValue)
        {
          retValue = ParseAssignment(tokens, out lineType);
        }

        if (null == retValue)
        {
          retValue = ParseValue(tokens, out lineType);
        }
      }
      return retValue;
    }

    // Creates a Assignment line item.
    private Item ParseAssignment(string[] tokens
      , out LineType lineType)
    {
      Item retValue = null;

      lineType = LineType.None;
      if (tokens != null
        && tokens.Length > 1
        && ("=" == tokens[1]))
      {
        lineType = LineType.Assignment;
        var tokenCount = tokens.Length;
        var line = string.Join(" ", tokens);
        var success = true;

        // Name = name/value operator name/value.
        if (tokenCount < 3
          || tokenCount > 5)
        {
          success = false;
          Error.TokenCount(line, Error.SyntaxAssignment());
        }

        // Check assignment target name.
        if (success)
        {
          var token = tokens[0];
          retValue = new Item(token, 0);
          if (!char.IsLetter(token[0]))
          {
            success = false;
            Error.NameFormat(line, token);
          }
        }

        // Get first value.
        var firstValue = 0.0;
        if (success
          && tokenCount > 2)
        {
          var token = tokens[2];
          firstValue = GetItemValue(line, token, out success
            , Error.SyntaxAssignment());
          if (3 == tokenCount)
          {
            retValue.Value = firstValue;
          }
        }

        // Get operation.
        string operation = null;
        if (success
          && tokenCount > 3)
        {
          var token = tokens[3];
          operation = GetOperation(line, token, out success);
        }

        if (success
           && tokenCount > 4)
        {
          // Get second value.
          var token = tokens[4];
          var secondValue = GetItemValue(line, token
            , out success, Error.SyntaxAssignment());
          retValue.Value = Calc(firstValue, operation, secondValue);
        }

        if (success)
        {
          retValue = SaveItem(retValue, lineType);
        }
      }
      return retValue;
    }

    // Creates a Formula line Item.
    private Item ParseFormula(string[] tokens, out LineType lineType)
    {
      Item retValue = null;

      lineType = LineType.None;
      if (tokens != null
        && tokens.Length > 1
        && "is" == tokens[1].ToLower())
      {
        lineType = LineType.Formula;
        var tokenCount = tokens.Length;
        var line = string.Join(" ", tokens);
        var success = true;

        // Name is # Name
        if (tokenCount < 4
          || tokenCount > 4)
        {
          success = false;
          Error.TokenCount(line, Error.SyntaxFormula());
        }

        retValue = new Item(tokens[0], 0);
        if (tokenCount > 2)
        {
          var token = tokens[2];
          if (!double.TryParse(token, out double value))
          {
            if (!char.IsLetter(token[0]))
            {
              success = false;
              Error.NameFormat(line, token);
            }
          }

          if (0 == value)
          {
            var findItem = GetItem(token);
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
          retValue = SaveItem(retValue, lineType);
        }
      }
      return retValue;
    }

    // Creates a Name Item.
    private Item ParseName(string[] tokens, out LineType lineType)
    {
      Item retValue = null;

      lineType = LineType.None;
      if (tokens != null
        && tokens.Length > 0)
      {
        foreach (string token in tokens)
        {
          // Name/AltName
          if (token.Contains("|"))
          {
            lineType = LineType.Name;
            retValue = new Item(null, 0);

            // Remove extra spaces.
            var success = true;
            var text = String.Join("", tokens);
            var names = SplitChars("|", text);
            if (0 == names.Length)
            {
              success = false;
              var line = string.Join(" ", tokens);
              Error.Declaration(line);
            }

            if (success)
            {
              retValue.Name = names[0];
              if (names.Length > 1)
              {
                retValue.AltName = names[1];
              }
              retValue = SaveItem(retValue, lineType);
            }
            break;
          }
        }
      }
      return retValue;
    }

    // Creates a Display Value line Item.
    private Item ParseValue(string[] tokens, out LineType lineType)
    {
      Item retValue = null;

      lineType = LineType.None;
      if (tokens != null
        && tokens.Length > 1
        && "value:" == tokens[0].ToLower())
      {
        lineType = LineType.Value;
        var tokenCount = tokens.Length;
        var line = string.Join(" ", tokens);

        // Value: Name
        if (tokenCount < 2
          || tokenCount > 4)
        {
          Error.TokenCount(line, Error.SyntaxValue());
        }

        retValue = new Item();
        if (tokenCount > 1)
        {
          var token = tokens[1];
          retValue.ValueName = token;
          var findItem = GetItem(token);
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
    #endregion

    #region Other Parse Methods

    // Clears the old value.
    private string ClearValue(string line)
    {
      var retValue = line;
      var valueIndex = retValue.IndexOf("(");
      if (valueIndex > 0)
      {
        retValue = retValue.Substring(0, valueIndex - 1);
      }
      return retValue;
    }

    // Gets the token or item value.
    private double GetItemValue(string line, string token, out bool success
      , string syntax = null)
    {
      if (success = IsItemOrNumber(line, token, out double retValue))
      {
        if (retValue < 0)
        {
          Item findItem = GetItem(token);
          if (null == findItem)
          {
            success = false;
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

    // Checs if tokenis an operation.
    private string GetOperation(string line, string token, out bool success)
    {
      string retValue = null;

      success = true;
      if ("+-*/".Contains(token))
      {
        retValue = token;
      }
      else
      {
        success = false;
        Error.Operation(line);
      }
      return retValue;
    }

    // Check if token is item name or number.
    //private bool IsItemOrNumber(string line, string token
    //  , out double value, string syntax = null)
    private bool IsItemOrNumber(string line, string token
      , out double value)
    {
      var retValue = true;

      if (!double.TryParse(token, out value))
      {
        value = -1.0;

        // Not a number and does not start with letter.
        if (!Error.IsNumber(token)
          && !char.IsLetter(token[0]))
        {
          retValue = false;
          Error.NameFormat(line, token);
        }

        //if (retValue
        //  && char.IsNumber(token[0])
        //  && !Error.IsNumber(token))
        //{
        //  retValue = false;
        //  Error.Number(line, token, syntax);
        //}
      }
      return retValue;
    }
    #endregion

    #region Other Private Methods

    // Performs an assignment calculation.
    private double Calc(double firstValue, string operation
      , double secondValue)
    {
      double retValue = 0.0;

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
    private void DoCalc(Item lineItemRef)
    {
      // "LeftName" is # "RightNames"
      // Calculate RightItem total.
      Item prevItemRef = lineItemRef;
      Item nextItemRef = null;
      if (NetString.HasValue(lineItemRef.FormulaName))
      {
        var formulaItemRef = GetItem(lineItemRef.FormulaName);
        if (formulaItemRef != null)
        {
          formulaItemRef.Value = lineItemRef.Value * lineItemRef.FormulaValue;
          nextItemRef = GetItemWithFormula(lineItemRef);
        }
      }

      if (null == nextItemRef)
      {
        // Look for related conversion.
        nextItemRef = GetItemWithFormula(lineItemRef);
      }

      // Calculate related formula totals.
      while (nextItemRef != null)
      {
        if (nextItemRef != null)
        {
          nextItemRef.Value = prevItemRef.Value / nextItemRef.FormulaValue;
          prevItemRef = nextItemRef;
          nextItemRef = GetItemWithFormula(nextItemRef);
        }
      }
    }

    // Performs the rounding.
    private double DoRound(string rounding, double value)
    {
      double retValue = value;

      if (NetString.HasValue(rounding))
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
    private Item GetItem(string name)
    {
      Item retValue = null;

      if (NetString.HasValue(name))
      {
        name = name.ToLower();
        retValue = Items.Find(x => 0 == string.Compare(x.Name, name, true));
        if (null == retValue)
        {
          retValue = Items.Find(x => 0 == string.Compare(x.AltName, name, true));
        }
      }
      return retValue;
    }

    // Gets the Formula name by Items Name or AltNames
    private Item GetItemWithFormula(Item item)
    {
      Item retValue = null;

      if (item != null)
      {
        if (NetString.HasValue(item.Name))
        {
          var name = item.Name.ToLower();
          retValue = Items.Find(x => x.FormulaName == name);
        }
        if (null == retValue
          && NetString.HasValue(item.AltName))
        {
          var altName = item.AltName.ToLower();
          retValue = Items.Find(x => x.FormulaName == altName);
        }
      }
      return retValue;
    }

    // Saves or Updates the Item.
    private Item SaveItem(Item item, LineType lineType)
    {
      var retValue = GetItem(item.Name);
      if (null == retValue)
      {
        retValue = item;
        Items.Add(retValue);
      }
      else
      {
        // Update Item values.
        switch (lineType)
        {
          case LineType.Assignment:
            retValue.Value = item.Value;
            break;

          case LineType.Formula:
            retValue.FormulaValue = item.FormulaValue;
            retValue.FormulaName = item.FormulaName;
            break;

          case LineType.Name:
            retValue.AltName = item.AltName;
            break;

          case LineType.Value:
            retValue.Rounding = item.Rounding;
            break;
        }
      }
      return retValue;
    }

    // Performs a split with no empty entries.
    private string[] SplitChars(string separators, string text)
    {
      return text.Split(separators.ToCharArray()
        , StringSplitOptions.RemoveEmptyEntries);
    }
    #endregion

    #region RTB Methods

    // Gets the RTB line first char index.
    private static int GetCharIndex(RichTextBox rtb, int lineIndex)
    {
      var retValue = rtb.GetFirstCharIndexFromLine(lineIndex);
      return retValue;
    }

    // Replaces RTB line.
    private static void ReplaceLine(RichTextBox rtb, int lineIndex, string text)
    {
      var beginIndex = GetCharIndex(rtb, lineIndex);

      var endIndex = rtb.Text.Length;
      if (lineIndex < rtb.Lines.Count() - 1)
      {
        lineIndex++;
        endIndex = GetCharIndex(rtb, lineIndex) - 1;
      }

      var length = endIndex - beginIndex;
      rtb.Select(beginIndex, length);
      rtb.SelectedText = text;
    }
    #endregion

    #region Properties

    // The defined Items.
    private List<Item> Items { get; set; }

    private RichTextBox RTB { get; set; }
    #endregion
  }

  // Represents a Data Item.
  public class Item
  {
    #region Constructors

    // Initializes an object instance.
    public Item()
    {
    }

    // Initializes an object instance.
    public Item(string name, double value)
    {
      Name = name;
      Value = value;
    }
    #endregion

    #region Data Methods

    // The debug display value.
    public override string ToString()
    {
      return $"{Name}: {Value}";
    }
    #endregion

    #region Methods

    // Gets int value.
    public bool ValueIfInt(out int value)
    {
      bool retValue = false;

      value = 0;
      if (0 == Value % 1)
      {
        Value = (int)Value;
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Properties

    // The Altername name.
    public string AltName { get; set; }

    // The Formula value.
    public double FormulaValue { get; set; }

    // The Formula Item name.
    public string FormulaName { get; set; }

    // The Item name.
    public string Name { get; set; }

    // The Rounding value.
    public string Rounding { get; set; }

    // The Value Item name.
    public string ValueName { get; set; }

    // The Item value.
    public double Value { get; set; }
    #endregion
  }

  // The line type values.
  public enum LineType
  {
    Assignment,
    Formula,
    Name,
    None,
    Value
  }
}
