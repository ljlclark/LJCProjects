// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataColumnsProgram.cs
using LJCNetCommon5;

namespace TestDataColumns5
{
  // The entry class.
  internal class DataColumnsProgram
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCDataColumns");
      Console.WriteLine();
      Console.WriteLine("*** LJCDataColumns ***");

      // Collecion Methods
      Add1();
      Add2();
      Add3();
      Add4();
      Clone();
      HasItems();
      LJCAddPropertyAs();
      LJCGetColumn();
      LJCGetColumns1();
      LJCGetColumns2();
      LJCPropertyNames();
      LJCRemoveColumn();
      LJCSerialize();
      LJCSetData();

      // Data Methods
      LJCClearChanged();
      LJCGetChanged();

      // Search and Sort Methods
      LJCSearchColumnName();
      LJCSearchPropertyName();
      LJCSearchRenameAs();
      LJCSortAddOrderIndex();
      LJCSortName();
      LJCSortProperty();
      LJCSortRenameAs();

      // Other Methods
      LJCGetMinSqlDate();
      LJCSetColumnCaptions();
      LJCMapNames();

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
      LJCGetObject();
      LJCGetSingle();
      LJCGetString();
      LJCSetValue();
    }

    #region Collection Methods

    // Adds the object element to the collection
    private static void Add1()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add1()", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection. (R)
    private static void Add2()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add2()", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection.
    private static void Add3()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add3()", result, compare);
    }

    // Creates the Object from the arguments and adds it to the collection.
    private static void Add4()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add4()", result, compare);
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

    // Creates the DbColumn from the supplied values and adds to the collection.
    private static void LJCAddPropertyAs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCAddPropertyAs()", result, compare);
    }

    // Returns a column by property name.
    private static void LJCGetColumn()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetColumn()", result, compare);
    }

    // Returns a set of columns that match the supplied list.
    private static void LJCGetColumns1()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetColumns1()", result, compare);
    }

    // Configure the Grid Columns from the Data object properties.
    private static void LJCGetColumns2()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetColumns2()", result, compare);
    }

    // Get the list of property names.
    private static void LJCPropertyNames()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCPropertyNames()", result, compare);
    }

    // Removes an LJCDataColumn item.
    private static void LJCRemoveColumn()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCRemoveColumn()", result, compare);
    }

    // Serializes the collection
    private static void LJCSerialize()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSerialize()", result, compare);
    }

    // Add or Update.
    private static void LJCSetData()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSetData()", result, compare);
    }
    #endregion

    #region Data Methods

    // Sets the IsChanged value to false for all elements in the collection.
    private static void LJCClearChanged()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCClearChanged()", result, compare);
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
    private static void LJCSearchColumnName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSearchColumnName()", result, compare);
    }

    // Finds and returns the column that contains the supplied property name.
    private static void LJCSearchPropertyName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSearchPropertyName()", result, compare);
    }

    // Finds and returns the column that contains the supplied property name.
    private static void LJCSearchRenameAs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSearchRenameAs()", result, compare);
    }

    // Sort on AddOrderIndex.
    private static void LJCSortAddOrderIndex()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSortAddOrderIndex()", result, compare);
    }

    // Sort on ColumnName.
    private static void LJCSortName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSortName()", result, compare);
    }

    // Sort on PropertyName.
    private static void LJCSortProperty()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSortProperty()", result, compare);
    }

    // Sort on PropertyName.
    private static void LJCSortRenameAs()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSortRenameAs()", result, compare);
    }
    #endregion

    #region Other Methods

    // Get the minimum date value.
    private static void LJCGetMinSqlDate()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCGetMinSqlDate()", result, compare);
    }

    // Sets the caption properties.
    private static void LJCSetColumnCaptions()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCSetColumnCaptions()", result, compare);
    }

    // Maps the column property and rename values.
    private static void LJCMapNames()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("LJCMapNames()", result, compare);
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

    // Update column value.
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
