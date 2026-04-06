// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataValuesProgram.cs
using LJCNetCommon5;
using System.Reflection.Emit;

namespace TestDataValues5
{
  // The entry class.
  internal class DataValuesProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataValues");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataValues ***");

      // Static Methods
      LJCDeserialize();

      // Constructor Methods
      Constructor();
      CopyConstructor();

      // Collection Methods
      Add();
      Clone();
      HasItems();
      LJCSerialize();

      // Item Methods
      LJCClearChanged();
      LJCCreateColumns();
      LJCGetChanged();

      // Search and Sort Methods
      LJCSearchPropertyName();

      // Value Methods
      LJCGetBoolean();
      LJCGetByte();
      LJCGetChar();
      LJCGetDbDateTime();
      LJCGetDecimal();
      LJCGetDouble();
      LJCGetInt16();
      LJCGetInt32();
      LJCGetInt64();
      LJCGetMinSqlDate();
      LJCGetObject();
      LJCGetSingle();
      LJCGetString();
      LJCSetValue();
    }

    #region Static Methods

    // Deserializes from the specified XML file.
    private static void LJCDeserialize()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCDeserialize()", result, compare);
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    private static void Constructor()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Constructor()", result, compare);
    }

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("CopyConstructor()", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates the Object from the arguments and adds it to the collection.
    private static void Add()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add()", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Clone()", result, compare);
    }

    // Checks if the collection has items.
    private static void HasItems()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("HasItems()", result, compare);
    }

    // Serializes the collection
    private static void LJCSerialize()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSerialize()", result, compare);
    }
    #endregion

    #region Item Methods

    // Sets the IsChanged value to false for all elements in the collection.
    private static void LJCClearChanged()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCClearChanged()", result, compare);
    }

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    private static void LJCCreateColumns()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCCreateColumns()", result, compare);
    }

    // Gets a collection of changed columns.
    private static void LJCGetChanged()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetChanged()", result, compare);
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    private static void LJCSearchPropertyName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSearchPropertyName()", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private static void LJCGetBoolean()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetBoolean()", result, compare);
    }

    // Gets the column object value as a byte.
    private static void LJCGetByte()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetByte()", result, compare);
    }

    // Gets the column object value as a char.
    private static void LJCGetChar()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetChar()", result, compare);
    }

    // Gets the column object value as a DateTime.
    private static void LJCGetDbDateTime()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetDbDateTime()", result, compare);
    }

    // Gets the column object value as a decimal.
    private static void LJCGetDecimal()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetDecimal()", result, compare);
    }

    // Gets the column object value as a double.
    private static void LJCGetDouble()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetDouble()", result, compare);
    }

    // Gets the column object value as a short int.
    private static void LJCGetInt16()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetInt16()", result, compare);
    }

    // Gets the column object value as an int.
    private static void LJCGetInt32()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetInt32()", result, compare);
    }

    // Gets the column object value as a long int.
    private static void LJCGetInt64()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetInt64()", result, compare);
    }

    // Get the minimum date value.
    private static void LJCGetMinSqlDate()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetMinSqlDate()", result, compare);
    }

    // Gets the column object value as an object.
    private static void LJCGetObject()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetObject()", result, compare);
    }

    // Gets the column object value as a single.
    private static void LJCGetSingle()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetSingle()", result, compare);
    }

    // Gets the string value for the column with the specified name.
    private static void LJCGetString()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetString()", result, compare);
    }

    // Sets the object value for the column with the specified name.
    private static void LJCSetValue()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSetValue()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
