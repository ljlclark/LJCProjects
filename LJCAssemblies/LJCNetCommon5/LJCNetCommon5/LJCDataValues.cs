// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataValues.cs
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // Represents a collection of LJCDataValue objects.
  /// <include path="members/LJCDataValues/*" file="Doc/LJCDataValues.xml"/>
  /// <group name="static">Static Methods</group>
  /// <group name="constructors">Constructors</group>
  /// <group name="collection">Collection Methods</group>
  /// <group name="item">Item Methods</group>
  /// <group name="search">Search and Sort Methods</group>
  /// <group name="value">Value Methods</group>
  //[XmlRoot("LJCDataValues")]
  public class LJCDataValues : List<LJCDataValue>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include path="members/LJCDeserialize/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    /// <parentGroup>static</parentGroup>
    public static LJCDataValues? LJCDeserialize(string? fileSpec = null)
    {
      LJCDataValues retValue;

      if (!LJC.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = LJC.XmlDeserialize(typeof(LJCDataValues), fileSpec)
        as LJCDataValues;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path="members/DefaultConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>constructors</parentGroup>
    public LJCDataValues()
    {
    }

    // The Copy constructor.
    /// <include path="members/CopyConstructor/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    /// <parentGroup>constructors</parentGroup>
    public LJCDataValues(LJCDataValues items)
    {
      if (LJC.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDataValue(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates the Object from the arguments and adds it to the collection.
    /// <include path="members/Add/*" file="Doc/LJCDataValues.xml"/>
    /// <parentGroup>collection</parentGroup>
    public LJCDataValue Add(string propertyName, object value
      , string dataTypeName = "String")
    {
      var retValue = new LJCDataValue()
      {
        DataTypeName = dataTypeName,
        PropertyName = propertyName,
        Value = value,
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path="members/Clone/*" file="../../../CoreUtilities/LJCGenDoc/Common/Data.xml"/>
    /// <parentGroup>collection</parentGroup>
    public LJCDataValues Clone()
    {
      var retValue = new LJCDataValues();
      foreach (LJCDataValue dataValue in this)
      {
        var newDataValue = dataValue.Clone();
        if (newDataValue != null)
        {
          retValue.Add(newDataValue);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path="members/HasItems2/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    /// <parentGroup>collection</parentGroup>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Serializes the collection
    /// <include path="members/LJCSerialize/*" file="../../../CoreUtilities/LJCGenDoc/Common/Collection.xml"/>
    /// <parentGroup>collection</parentGroup>
    public void LJCSerialize(string? fileSpec = null)
    {
      if (!LJC.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Item Methods

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include path="members/LJCClearChanged/*" file="Doc/LJCDataValues.xml"/>
    /// <parentGroup>item</parentGroup>
    public void LJCClearChanged()
    {
      foreach (LJCDataValue dataValue in this)
      {
        dataValue.IsChanged = false;
      }
    }

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    /// <include path="members/LJCCreateColumns/*" file="Doc/LJCDataValues.xml"/>
    /// <parentGroup>item</parentGroup>
    public LJCDataColumns? LJCCreateColumns(LJCDataColumns dataColumns)
    {
      LJCDataColumns retValue = null;

      if (dataColumns != null)
      {
        retValue = [];
        foreach (LJCDataValue dataValue in this)
        {
          if (dataValue.PropertyName != null)
          {
            var dataColumn = dataColumns.LJCSearchColumnName(dataValue.PropertyName);
            var newDataValue = dataValue.CreateColumn(dataColumn!);
            if (newDataValue != null)
            {
              retValue.Add(newDataValue);
            }
          }
        }
      }
      return retValue;
    }

    // Gets a collection of changed columns.
    /// <include path="members/LJCGetChanged/*" file="Doc/LJCDataValues.xml"/>
    /// <parentGroup>item</parentGroup>
    public LJCDataValues LJCGetChanged()
    {
      List<LJCDataValue> dataValues;
      var retValue = new LJCDataValues();

      dataValues = FindAll(x => x.IsChanged);
      foreach (LJCDataValue dataValue in dataValues)
      {
        var newDataValue = dataValue.Clone();
        if (newDataValue != null)
        {
          retValue.Add(newDataValue);
        }
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    /// <include path="members/LJCSearchPropertyName/*" file="Doc/LJCDataValues.xml"/>
    /// <parentGroup>search</parentGroup>
    public LJCDataValue? LJCSearchPropertyName(string propertyName)
    {
      LJCDataValue retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      var searchDbColumn = new LJCDataValue()
      {
        PropertyName = propertyName
      };
      int index = BinarySearch(searchDbColumn);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include path="members/LJCGetBoolean/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public bool LJCGetBoolean(string propertyName)
    {
      bool retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        if (LJCNetString.IsDigits(value))
        {
          var checkValue = Convert.ToInt16(value);
          retValue = Convert.ToBoolean(checkValue);
        }
        else
        {
          _ = bool.TryParse(value, out retValue);
        }
      }
      return retValue;
    }

    // Gets the column object value as a byte.
    /// <include path="members/LJCGetByte/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public byte LJCGetByte(string propertyName)
    {
      byte retValue = default;

      if (LJC.HasValue(propertyName))
      {
        var dataValue = LJCSearchPropertyName(propertyName);
        if (dataValue != null
          && dataValue.Value != null)
        {
          retValue = LJC.GetByte(dataValue.Value);
        }
      }
      return retValue;
    }

    // Gets the column object value as a byte array.
    /// <include path="members/LJCGetBytes/*" file="Doc/LJCDataColumns.xml"/>
    public byte[]? LJCGetBytes(string propertyName)
    {
      byte[] retValue = default;

      if (LJC.HasValue(propertyName))
      {
        var dataValue = LJCSearchPropertyName(propertyName);
        if (dataValue != null
          && dataValue.Value != null)
        {
          retValue = LJC.GetBytes(dataValue.Value);
        }
      }
      return retValue;
    }

    // Gets the column object value as a char.
    /// <include path="members/LJCGetChar/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public char LJCGetChar(string propertyName)
    {
      char retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToChar(value);
      }
      return retValue;
    }

    // Gets the column object value as a DateTime.
    /// <include path="members/LJCGetDbDateTime/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public DateTime LJCGetDbDateTime(string propertyName)
    {
      DateTime retValue = DateTime.Parse(LJCGetMinSqlDate());

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = DateTime.Parse(value);
      }
      return retValue;
    }

    // Gets the column object value as a decimal.
    /// <include path="members/LJCGetDecimal/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public decimal LJCGetDecimal(string propertyName)
    {
      decimal retValue = 0;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToDecimal(value);
      }
      return retValue;
    }

    // Gets the column object value as a double.
    /// <include path="members/LJCGetDouble/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public double LJCGetDouble(string propertyName)
    {
      double retValue = 0;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToDouble(value);
      }
      return retValue;
    }

    // Gets the column object value as a short int.
    /// <include path="members/LJCGetInt16/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public short LJCGetInt16(string propertyName)
    {
      short retValue = 0;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt16(value);
      }
      return retValue;
    }

    // Gets the column object value as an int.
    /// <include path="members/LJCGetInt32/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public int LJCGetInt32(string propertyName)
    {
      int retValue = 0;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt32(value);
      }
      return retValue;
    }

    // Gets the column object value as a long int.
    /// <include path="members/LJCGetInt64/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public long LJCGetInt64(string propertyName)
    {
      long retValue = 0;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt64(value);
      }
      return retValue;
    }

    // Get the minimum date value.
    /// <include path="members/LJCGetMinSqlDate/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public static string LJCGetMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Gets the column object value as an object.
    /// <include path="members/LJCGetObject/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public object? LJCGetObject(string propertyName)
    {
      object retValue = default;

      var dbValue = LJCSearchPropertyName(propertyName);
      if (dbValue != null
        && dbValue.Value != null)
      {
        retValue = dbValue.Value;
      }
      return retValue;
    }

    // Gets the column object value as a single.
    /// <include path="members/LJCGetSingle/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public float LJCGetSingle(string propertyName)
    {
      float retValue = 0;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToSingle(value);
      }
      return retValue;
    }

    // Gets the string value for the column with the specified name.
    /// <include path="members/LJCGetString/*" file="Doc/LJCDataColumns.xml"/>
    /// <parentGroup>value</parentGroup>
    public string? LJCGetString(string propertyName)
    {
      string retValue = null;

      if (LJC.HasValue(propertyName))
      {
        var dataValue = LJCSearchPropertyName(propertyName);
        if (dataValue != null
          && dataValue.Value != null
          && LJC.HasValue(dataValue.Value.ToString()))
        {
          retValue = dataValue.Value.ToString();
        }
      }
      return retValue;
    }

    // Sets the object value for the column with the specified name.
    /// <include path="members/LJCSetValue/*" file="Doc/LJCDataValues.xml"/>
    /// <parentGroup>value</parentGroup>
    public void LJCSetValue(string propertyName, object value)
    {
      if (LJC.HasItems(this)
        && LJC.HasValue(propertyName))
      {
        var dbValue = LJCSearchPropertyName(propertyName);
        if (dbValue != null)
        {
          dbValue.Value = value;
        }
      }
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include path="members/LJCDefaultFileName/*" file="Doc/LJCDataValues.xml"/>
    public static string LJCDefaultFileName
    {
      get { return "LJCDataValues.xml"; }
    }

    // The column for the specified name.
    /// <include path="members/Item/*" file="Doc/LJCDataValues.xml"/>
    public LJCDataValue? this[string propertyName]
    {
      get { return LJCSearchPropertyName(propertyName); }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
