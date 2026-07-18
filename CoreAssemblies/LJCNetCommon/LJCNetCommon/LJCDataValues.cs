// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataValues.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a collection of LJCDataValue objects.
  /// <include file='Doc/LJCDataValues.xml'
  ///  path='members/LJCDataValues/*'/>
  [XmlRoot("LJCDataValues")]
  public class LJCDataValues : List<LJCDataValue>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static LJCDataValues LJCDeserialize(string fileSpec = null)
    {
      LJCDataValues retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(DbValues), fileSpec)
        as LJCDataValues;
      return retValue;
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataValues()
    {
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCDataValues(LJCDataValues items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDataValue(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDataValues Clone()
    {
      var retValue = new LJCDataValues();
      foreach (var dataValue in this)
      {
        retValue.Add(dataValue.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/HasItems/*'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCClearChanged/*'/>
    public void LJCClearChanged()
    {
      foreach (var dataValue in this)
      {
        dataValue.IsChanged = false;
      }
    }

    // Creates combined LJCDataColumns from LJCDataColumns and LJCDataValues.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCCreateColumns/*'/>
    public LJCDataColumns LJCCreateColumns(LJCDataColumns dataDefinition)
    {
      LJCDataColumn definitionColumn;
      LJCDataColumns retValue = null;

      if (dataDefinition != null)
      {
        retValue = new LJCDataColumns();
        foreach (var dataValue in this)
        {
          definitionColumn = dataDefinition.LJCGetWithColumnName(dataValue.PropertyName);
          var dataColumn = dataValue.CreateColumn(definitionColumn);
          retValue.Add(dataColumn);
        }
      }
      return retValue;
    }

    // Gets a collection of changed columns.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetChanged/*'/>
    public LJCDataValues LJCGetChanged()
    {
      List<LJCDataValue> dataValues;
      var retValue = new LJCDataValues();

      dataValues = FindAll(x => x.IsChanged);
      foreach (var dataValue in dataValues)
      {
        retValue.Add(dataValue.Clone());
      }
      return retValue;
    }

    // Serializes the collection
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCSerialize/*'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Creates the Object from the arguments and adds it to the collection.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/Add/*'/>
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
    #endregion

    #region Custom Retrieve Methods

    // Finds and returns the object that matches the supplied values.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetWithPropertyName/*'/>
    public LJCDataValue LJCGetWithPropertyName(string propertyName)
    {
      LJCDataValue retValue = null;

      if (Count != _PrevCount)
      {
        _PrevCount = Count;
        Sort();
      }

      var searchDataValue = new LJCDataValue()
      {
        PropertyName = propertyName
      };
      int index = BinarySearch(searchDataValue);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetBoolean/*'/>
    public bool LJCGetBoolean(string propertyName)
    {
      bool retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        try
        {
          retValue = Convert.ToBoolean(value);
        }
        catch
        {
          retValue = false;
        }
      }
      return retValue;
    }

    // Gets the column object value as a byte.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetByte/*'/>
    public byte LJCGetByte(string propertyName)
    {
      byte retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToByte(value);
      }
      return retValue;
    }

    // Gets the column object value as a char.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetChar/*'/>
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetDbDateTime/*'/>
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

    // Gets the column object value as a decimal value.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetDecimal/*'/>
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

    // Gets the column object value as a decimal double.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetDouble/*'/>
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetInt16/*'/>
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetInt32/*'/>
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetInt64/*'/>
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetMinSqlDate/*'/>
    public static string LJCGetMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Gets the column object value as an object.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetObject/*'/>
    public object LJCGetObject(string propertyName)
    {
      object retValue = default;

      var dbValue = LJCGetWithPropertyName(propertyName);
      if (dbValue != null
        && dbValue.Value != null)
      {
        retValue = dbValue.Value;
      }
      return retValue;
    }

    // Gets the column object value as a single.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetSingle/*'/>
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
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCGetString/*'/>
    public string LJCGetString(string propertyName)
    {
      string retValue = null;

      if (NetString.HasValue(propertyName))
      {
        var dbValue = LJCGetWithPropertyName(propertyName);
        if (dbValue != null
          && dbValue.Value != null
          && NetString.HasValue(dbValue.Value.ToString()))
        {
          retValue = dbValue.Value.ToString();
        }
      }
      return retValue;
    }

    // Sets the object value for the column with the specified name.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCSetValue/*'/>
    public void LJCSetValue(string propertyName, object value)
    {
      if (NetCommon.HasItems(this)
        && NetString.HasValue(propertyName))
      {
        var dbValue = LJCGetWithPropertyName(propertyName);
        if (dbValue != null)
        {
          dbValue.Value = value;
        }
      }
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "DbValues.xml"; }
    }

    // The column for the specified name.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/Item/*'/>
    public LJCDataValue this[string propertyName]
    {
      get { return LJCGetWithPropertyName(propertyName); }
    }
    #endregion

    #region Class Data

    private int _PrevCount;
    #endregion
  }
}
