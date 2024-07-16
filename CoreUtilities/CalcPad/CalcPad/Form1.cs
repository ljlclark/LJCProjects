using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CalcPad
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      IDs = new List<ID>();
    }

    #region Methods

    // Creates a Formula ID.
    private ID CreateAssignmentID(string[] tokens, out IDType idType)
    {
      ID retValue = null;

      idType = IDType.None;
      if ("=" == tokens[1].Trim().ToLower())
      {
        idType = IDType.Assignment;

        // Name is # Name
        retValue = new ID();
        retValue.Name = tokens[0];
        if (tokens.Length > 2)
        {
          if (double.TryParse(tokens[2], out double value))
          {
            retValue.Value = value;
            retValue.Total = value;
          }
        }
        retValue = SaveID(retValue, idType);
      }
      return retValue;
    }

    // Creates a Formula ID.
    private ID CreateFormulaID(string[] tokens, out IDType idType)
    {
      ID retValue = null;

      idType = IDType.None;
      if ("is" == tokens[1].Trim().ToLower())
      {
        idType = IDType.Formula;

        // Name is # Name
        retValue = new ID();
        retValue.Name = tokens[0];
        if (tokens.Length > 2)
        {
          if (double.TryParse(tokens[2], out double value))
          {
            retValue.FormulaValue = value;
          }
        }
        if (tokens.Length > 3)
        {
          retValue.FormulaIDName = tokens[3];
        }
        retValue = SaveID(retValue, idType);
      }
      return retValue;
    }

    private ID CreateID(string line, out IDType idType)
    {
      ID retValue = null;

      idType = IDType.None;
      var tokens = SplitChars(" ", line);
      if (tokens.Length > 0)
      {
        retValue = CreateNameID(tokens, out idType);
        if (null == retValue)
        {
          retValue = CreateFormulaID(tokens, out idType);
        }

        if (null == retValue)
        {
          retValue = CreateAssignmentID(tokens, out idType);
        }

        if (null == retValue)
        {
          retValue = CreateTotalID(tokens, out idType);
        }
      }
      return retValue;
    }

    // Gets the Formula name by ID Name or AltNames
    private ID GetFormulaID(ID id)
    {
      var retValue = IDs.Find(x => x.FormulaIDName == id.Name);
      if (null == retValue)
      {
        retValue = IDs.Find(x => x.FormulaIDName == id.AltName);
      }
      return retValue;
    }

    // Gets the ID by Name or AltNames
    private ID GetID(string name)
    {
      var retValue = IDs.Find(x => x.Name == name);
      if (null == retValue)
      {
        retValue = IDs.Find(x => x.AltName == name);
      }
      return retValue;
    }

    private ID SaveID(ID id, IDType idType)
    {
      var retValue = GetID(id.Name);
      if (null == retValue)
      {
        retValue = id;
        IDs.Add(retValue);
      }
      else
      {
        switch (idType)
        {
          case IDType.Assignment:
            retValue.Value = id.Value;
            break;

          case IDType.Formula:
            retValue.FormulaValue = id.FormulaValue;
            retValue.FormulaIDName = id.FormulaIDName;
            break;

          case IDType.Name:
            retValue.AltName = id.AltName;
            break;
        }
      }
      return retValue;
    }

    // Creates a Name ID.
    private ID CreateNameID(string[] tokens, out IDType idType)
    {
      ID retValue = null;

      idType = IDType.None;
      foreach (string token in tokens)
      {
        // Name/AltName
        if (token.Contains("/"))
        {
          idType = IDType.Name;

          retValue = new ID();
          var text = String.Join("", tokens);
          var names = SplitChars("/", text);
          retValue.Name = names[0];
          if (names.Length > 1)
          {
            retValue.AltName = names[1];
          }
          retValue = SaveID(retValue, idType);
          break;
        }
      }
      return retValue;
    }

    // Creates a Total ID.
    private ID CreateTotalID(string[] tokens, out IDType idType)
    {
      ID retValue = null;

      // Total: Name
      idType = IDType.None;
      if ("total:" == tokens[0].Trim().ToLower())
      {
        idType = IDType.Total;

        retValue = new ID();
        retValue.Name = tokens[0];
        retValue.FormulaIDName = tokens[1];
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

    #region Event handlers

    private void CalcRTB_DoubleClick(object sender, EventArgs e)
    {
      foreach (string line in CalcsRTB.Lines)
      {
        var success = true;
        if (string.IsNullOrWhiteSpace(line))
        {
          success = false;
        }

        ID lineIDRef = null;
        if (success)
        {
          lineIDRef = CreateID(line, out IDType idType);
          if (idType == IDType.Assignment)
          {
            // "FirstName" is # "SecondName"
            var firstIDRef = lineIDRef;

            // Calculate SecondID total.
            var secondIDRef = GetID(firstIDRef.FormulaIDName);
            secondIDRef.Total = firstIDRef.Value * firstIDRef.FormulaValue;

            // Calculate formula "To" totals.
            //var total = secondIDRef.Total;
            var prevIDRef = firstIDRef;
            var nextIDRef = GetFormulaID(firstIDRef);
            while (nextIDRef != null)
            {
              if (nextIDRef != null)
              {
                nextIDRef.Value = prevIDRef.Value / nextIDRef.FormulaValue;
                nextIDRef.Total = nextIDRef.Value;
                prevIDRef = nextIDRef;
                nextIDRef = GetFormulaID(nextIDRef);
              }
            }
          }
        }
      }
    }
    #endregion

    #region Properties

    private List<ID> IDs { get; set; }
    #endregion
  }

  // Represents an Identifier.s
  public class ID
  {
    #region Constructors

    // Initializes an object instance.
    public ID()
    {
    }

    // Initializes an object instance.
    public ID(string name, double value)
    {
      Name = name;
      ValueType = ValueType.Int;
      Value = value;
      if (0 == value % 1)
      {
        ValueType = ValueType.Int;
      }
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

    public string AltName { get; set; }

    public ValueType ValueType { get; set; }

    public double FormulaValue { get; set; }

    public string FormulaIDName { get; set; }

    public IDType IDType { get; set; }

    public string Name { get; set; }

    public double Total { get; set; }

    public double Value { get; set; }
    #endregion
  }

  public enum IDType
  {
    None,
    Name,
    Formula,
    Assignment,
    Total
  }

  public enum ValueType
  {
    Int,
    Double
  }
}
