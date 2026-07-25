// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataValues.cs
using LJCNetCommon;
using System;

namespace TestData
{
  // Provides the LJCDataValues test methods.
  internal class TestDataValues
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataValues()
    {
      TestCommon = new TestCommon("TestDataValues");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataValues ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      #region Static Methods

      // Deserializes from the specified XML file.
      LJCDeserialize();

      // Get the minimum date value.
      LJCMinSqlDate();
      #endregion

      #region Constructor Methods

      // Initializes an object instance.
      Constructor();

      // Initializes an object from the supplied items.
      CopyConstructor();
      #endregion

      #region Collection Methods

      // Creates and returns a clone of the object.
      Clone();

      // Checks if the collection has items.
      HasItems();

      // Gets a collection of changed columns.
      LJCChanged();

      // Sets the IsChanged value to false for all items.
      LJCClearChanged();

      // Returns a collection of items that match a list of property names.
      LJCCreateColumns();

      // Serializes the collection
      LJCSerialize();
      #endregion

      #region Collection Data Methods

      // Creates item with Value and adds it to the collection.
      Add();

      // Returns the column that matches the key columns.
      LJCGetUnique();

      // Sorts on the current key columns.
      LJCSort();
      #endregion

      #region Value Methods

      // Gets the column object value as a bool.
      LJCGetBoolean();

      // Gets the column object value as a byte.
      LJCGetByte();

      // Gets the column object value as a char.
      LJCGetChar();

      // Gets the column object value as a DateTime.
      LJCGetDbDateTime();

      // Gets the column object value as a decimal.
      LJCGetDecimal();

      // Gets the column object value as a double.
      LJCGetDouble();

      // Gets the column object value as a short int.
      LJCGetInt16();

      // Gets the column object value as an int.
      LJCGetInt32();

      // Gets the column object value as a long int.
      LJCGetInt64();

      // Gets the column object value as a single.
      LJCGetSingle();

      // Gets the column object value as a string.
      LJCGetString();

      // Gets the column object value.
      LJCGetValue();

      // Sets the column object value.
      LJCSetValue();
      #endregion

      #region Properties

      // Gets or sets the key columns.
      LJCKeyColumns();

      // Returns the item with the supplied property name.
      PropertyNameIndexer();
      #endregion
    }
    #endregion

    #region Static Methods

    // Deserializes from the specified XML file.
    private void LJCDeserialize()
    {
      var methodName = "LJCDeserialize()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Get the minimum date value.
    private void LJCMinSqlDate()
    {
      var methodName = "LJCMinSqlDate()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    private void Constructor()
    {
      var methodName = "Constructor()";

      // Test Method
      var dataColumns = new LJCDataColumns();

      var dataColumn = dataColumns.Add("PropertyName");
      dataColumn.ColumnName = "ColumnName";
      var result = dataColumn.PropertyName;
      result += $", {dataColumn.ColumnName}";
      var compare = "PropertyName, ColumnName";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Initializes an object from the supplied item.
    private void CopyConstructor()
    {
      var methodName = "CopyConstructor()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var methodName = "Clone()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Checks if the collection has items.
    private void HasItems()
    {
      var methodName = "HasItems()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a collection of changed columns.
    private void LJCChanged()
    {
      var methodName = "LJCChanged()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sets the IsChanged value to false for all items.
    private void LJCClearChanged()
    {
      var methodName = "LJCClearChanged()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns a collection of items from the data object properties.
    private void LJCCreateColumns()
    {
      var methodName = "LJCColumns2()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Serializes the collection
    private void LJCSerialize()
    {
      var methodName = "LJCSerialize()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Collection Data Methods

    // Creates item with Value and adds it to the collection.
    private void Add()
    {
      var methodName = "Add()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the column that matches the key columns.
    private void LJCGetUnique()
    {
      var methodName = "LJCGetUnique()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts on the current key columns.
    private void LJCSort()
    {
      var methodName = "LJCSort()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    private void LJCGetBoolean()
    {
      var methodName = "LJCGetBoolean()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a byte.
    private void LJCGetByte()
    {
      var methodName = "LJCGetByte()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a char.
    private void LJCGetChar()
    {
      var methodName = "LJCGetChar()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a DateTime.
    private void LJCGetDbDateTime()
    {
      var methodName = "LJCGetDbDateTime()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a decimal.
    private void LJCGetDecimal()
    {
      var methodName = "LJCGetDecimal()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a double.
    private void LJCGetDouble()
    {
      var methodName = "LJCGetDouble()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a short int.
    private void LJCGetInt16()
    {
      var methodName = "LJCGetInt16()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as an int.
    private void LJCGetInt32()
    {
      var methodName = "LJCGetInt32()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a long int.
    private void LJCGetInt64()
    {
      var methodName = "LJCGetInt64()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a single.
    private void LJCGetSingle()
    {
      var methodName = "LJCGetSingle()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value as a string.
    private void LJCGetString()
    {
      var methodName = "LJCGetString()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets the column object value.
    private void LJCGetValue()
    {
      var methodName = "LJCGetValue()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sets the column object value.
    private void LJCSetValue()
    {
      var methodName = "LJCSetValue()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Properties

    // Returns the item with the supplied property name.
    private void LJCKeyColumns()
    {
      var methodName = "LJCKeyColumns()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the item with the supplied property name.
    private void PropertyNameIndexer()
    {
      var methodName = "PropertyNameIndexer()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Class Data

    // The test common object.
    private static TestCommon TestCommon { get; set; }
    #endregion
  }
}
