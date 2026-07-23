// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestDataColumns.cs
using LJCNetCommon;
using System;

namespace TestData
{
  // Provides the LJCDataColumns test methods.
  internal class TestDataColumns
  {
    #region Constructor Methods

    // Initializes an object instance.
    public TestDataColumns()
    {
      TestCommon = new TestCommon("TestDataColumns");
      Console.WriteLine();
      Console.WriteLine("**********************");
      Console.Write("*** LJCDataColumns ***");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      #region Static Methods

      // Deserializes from the specified XML file.
      LJCDeserialize();

      // Gets a collection of items from a data object.
      LJCGetObjectColumns();

      // Gets a list of property names from a data object.
      LJCGetObjectPropertyNames();

      // Operator to create LJCDataValues from LJCDataColumns.
      DataColumnsToDataValues();
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

      // Sets the IsChanged value to false for all items.
      LJCClearChanged();

      // Gets a collection of changed columns.
      LJCGetChanged();

      // Returns a collection of items that match a list of property names.
      LJCGetColumns1();

      // Returns a collection of items from the data object properties.
      LJCGetColumns2();

      // Gets a list of property names from the collection items.
      LJCGetPropertyNames();

      // Serializes the collection
      LJCSerialize();
      #endregion

      #region Collection Data Methods

      // Adds the supplied item to the collection
      Add1();

      // Creates item with the supplied values and adds it to the collection.
      Add2();

      // Creates item with Position and MaxLength and adds it to the collection.
      Add3();

      // Creates item with Value and adds it to the collection.
      Add4();

      // Creates item with Caption and RenameAs and adds to the collection.
      LJCAddPropertyAs();

      // Removes the item with the supplied property name.
      LJCRemove();

      // Add or Update.
      LJCSetData();
      #endregion

      #region Custom Data Methods

      // Returns the item with the supplied column name.
      LJCGetWithColumnName();

      // Returns the item with the supplied property name.
      LJCGetWithPropertyName();

      // Returns the item with the supplied RenameAs value.
      LJCGetWithRenameAs();
      #endregion

      #region Sort Methods

      // Sorts by AddOrderIndex.
      LJCSortByAddOrderIndex();

      // Sorts by ColumnName.
      LJCSortByColumnName();

      // Sorts by PropertyName.
      LJCSortByPropertyName();

      // Sorts by RenameAs.
      LJCSortByRenameAs();
      #endregion

      #region Other Public Methods

      // Gets the minimum date value.
      LJCGetMinSqlDate();

      // Sets the caption properties.
      LJCSetColumnCaptions();

      // Maps the column property and rename values.
      LJCMapNames();
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

    // Gets a collection of items from a data object.
    private void LJCGetObjectColumns()
    {
      var methodName = "LJCCreateObjectColumns()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a list of property names from a data object.
    private void LJCGetObjectPropertyNames()
    {
      var methodName = "LJCGetPropertyNames()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Operator to creates LJCDataValues from LJCDataColumns.
    private void DataColumnsToDataValues()
    {
      var methodName = "DataColumnsToDataValues()";

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

    // Sets the IsChanged value to false for all items.
    private void LJCClearChanged()
    {
      var methodName = "LJCClearChanged()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a collection of changed columns.
    private void LJCGetChanged()
    {
      var methodName = "LJCGetChanged()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns a collection of items that match a list of property names.
    private void LJCGetColumns1()
    {
      var methodName = "LJCGetColumns1()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns a collection of items from the data object properties.
    private void LJCGetColumns2()
    {
      var methodName = "LJCGetColumns2()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Gets a list of property names from the collection items.
    private void LJCGetPropertyNames()
    {
      var methodName = "LJCGetPropertyNames()";

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

    // Adds the supplied item to the collection
    private void Add1()
    {
      var methodName = "Add1()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates item with the supplied values and adds it to the collection.
    private void Add2()
    {
      var methodName = "Add2()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates item with Position and MaxLength and adds it to the collection.
    private void Add3()
    {
      var methodName = "Add3()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates item with Value and adds it to the collection.
    // Use (object) cast with a string value to use this overload.
    private void Add4()
    {
      var methodName = "Add4()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Creates item with Caption and RenameAs and adds to the collection.
    private void LJCAddPropertyAs()
    {
      var methodName = "LJCAddPropertyAs()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Removes the item with the supplied property name.
    private void LJCRemove()
    {
      var methodName = "LJCRemove()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Add or Update.
    private void LJCSetData()
    {
      var methodName = "LJCSetData()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Custom Data Methods

    // Returns the item with the supplied column name.
    private void LJCGetWithColumnName()
    {
      var methodName = "LJCGetWithColumnName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the item with the supplied property name.
    private void LJCGetWithPropertyName()
    {
      var methodName = "LJCGetWithPropertyName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Returns the item with the supplied RenameAs value.
    private void LJCGetWithRenameAs()
    {
      var methodName = "LJCGetWithRenameAs()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Sort Methods

    // Sorts by AddOrderIndex.
    private void LJCSortByAddOrderIndex()
    {
      var methodName = "LJCSortAddOrderIndex()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts by ColumnName.
    private void LJCSortByColumnName()
    {
      var methodName = "LJCSortColumnName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts by PropertyName.
    private void LJCSortByPropertyName()
    {
      var methodName = "LJCSortPropertyName()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sorts by RenameAs.
    private void LJCSortByRenameAs()
    {
      var methodName = "LJCSortRenameAs()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }
    #endregion

    #region Other Public Methods

    // Get the minimum date value.
    private void LJCGetMinSqlDate()
    {
      var methodName = "LJCGetMinSqlDate()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Sets the caption properties.
    private void LJCSetColumnCaptions()
    {
      var methodName = "LJCSetColumnCaptions()";

      var result = "";
      var compare = "Not Implemented";
      TestCommon.Write($"{methodName}", result, compare);
    }

    // Maps the column property and rename values.
    private void LJCMapNames()
    {
      var methodName = "LJCMapNames()";

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
