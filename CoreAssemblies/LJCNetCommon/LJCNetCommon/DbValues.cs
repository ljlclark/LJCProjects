// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbValues.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a collection of DbValue objects.
  /// <include path='items/DbValues/*' file='Doc/DbValues.xml'/>
  [XmlRoot("DbValues")]
  public class DbValues : List<DbValue>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public static DbValues LJCDeserialize(string fileSpec = null)
    {
      DbValues retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(DbValues), fileSpec)
        as DbValues;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbValues()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public DbValues(DbValues items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbValue(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates the Object from the arguments and adds it to the collection.
    /// <include path='items/Add/*' file='Doc/DbValues.xml'/>
    public DbValue Add(string propertyName, object value
      , string dataTypeName = "String")
    {
      DbValue retValue = new DbValue()
      {
        DataTypeName = dataTypeName,
        PropertyName = propertyName,
        Value = value,
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbValues Clone()
    {
      var retValue = new DbValues();
      foreach (DbValue dbValue in this)
      {
        retValue.Add(dbValue.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
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
    /// <include path='items/LJCSerialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Item Methods

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include path='items/LJCClearChanged/*' file='Doc/DbValues.xml'/>
    public void LJCClearChanged()
    {
      foreach (DbValue dbValue in this)
      {
        dbValue.IsChanged = false;
      }
    }

    // Creates combined DbColumns from DbColumns and DbValues.
    /// <include path='items/LJCCreateColumns/*' file='Doc/DbValues.xml'/>
    public DbColumns LJCCreateColumns(DbColumns dataDefinition)
    {
      DbColumn definitionColumn;
      DbColumns retValue = null;

      if (dataDefinition != null)
      {
        retValue = new DbColumns();
        foreach (DbValue dbValue in this)
        {
          definitionColumn = dataDefinition.LJCSearchColumnName(dbValue.PropertyName);
          DbColumn dbColumn = dbValue.CreateColumn(definitionColumn);
          retValue.Add(dbColumn);
        }
      }
      return retValue;
    }

    // Gets a collection of changed columns.
    /// <include path='items/LJCGetChanged/*' file='Doc/DbValues.xml'/>
    public DbValues LJCGetChanged()
    {
      List<DbValue> values;
      DbValues retValue = new DbValues();

      values = FindAll(x => x.IsChanged);
      foreach (DbValue dbValue in values)
      {
        retValue.Add(dbValue.Clone());
      }
      return retValue;
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    /// <include path='items/LJCSearchPropertyName/*' file='Doc/DbValues.xml'/>
    public DbValue LJCSearchPropertyName(string propertyName)
    {
      DbValue retValue = null;

      if (Count != mPrevCount)
      {
        mPrevCount = Count;
        Sort();
      }

      DbValue searchDbColumn = new DbValue()
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
    /// <include path='items/LJCGetBoolean/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetByte/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetChar/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetDbDateTime/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetDecimal/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetDouble/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetInt16/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetInt32/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetInt64/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetMinSqlDate/*' file='Doc/DbColumns.xml'/>
    public static string LJCGetMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Gets the column object value as an object.
    /// <include path='items/LJCGetObject/*' file='Doc/DbColumns.xml'/>
    public object LJCGetObject(string propertyName)
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
    /// <include path='items/LJCGetSingle/*' file='Doc/DbColumns.xml'/>
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
    /// <include path='items/LJCGetString/*' file='Doc/DbColumns.xml'/>
    public string LJCGetString(string propertyName)
    {
      string retValue = null;

      if (NetString.HasValue(propertyName))
      {
        var dbValue = LJCSearchPropertyName(propertyName);
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
    /// <include path='items/LJCSetValue/*' file='Doc/DbValues.xml'/>
    public void LJCSetValue(string propertyName, object value)
    {
      if (NetCommon.HasItems(this)
        && NetString.HasValue(propertyName))
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

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DbValues.xml"; }
    }

    // The column for the specified name.
    /// <include path='items/Item/*' file='Doc/DbValues.xml'/>
    public DbValue this[string propertyName]
    {
      get { return LJCSearchPropertyName(propertyName); }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    #endregion
  }
}
