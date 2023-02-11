// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbColumns
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LJCNetCommon
{
  // Represents a collection of DbColumn objects.
  /// <include path='items/DbColumns/*' file='Doc/DbColumns.xml'/>
  [XmlRoot("DbColumns")]
  public class DbColumns : List<DbColumn>
  {
    #region Static Functions

    // Creates DbColumns from a Data Object.
    /// <include path='items/LJCCreateObjectColumns/*' file='Doc/DbColumns.xml'/>
    public static DbColumns LJCCreateObjectColumns(object dataObject
      , DbColumns dataDefinition = null)
    {
      DbColumn definitionColumn = null;
      DbColumns retValue = null;

      LJCReflect reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = new DbColumns();
        foreach (string propertyName in propertyNames)
        {
          if ("ChangedNames" == propertyName)
          {
            continue;
          }

          if (dataDefinition != null)
          {
            definitionColumn = dataDefinition.LJCSearchPropertyName(propertyName);
          }

          DbColumn dbColumn = new DbColumn()
          {
            Caption = propertyName,
            ColumnName = propertyName,
            // *** Next Statement *** Add - 11/24/22
            Value = reflect.GetValue(propertyName)
          };

          Type type = reflect.GetPropertyType(propertyName);
          if (type != null)
          {
            dbColumn.DataTypeName = type.Name;
            // *** Next Statement *** Change - 11/24/22
            if (definitionColumn != null
              && "String" == type.Name)
            {
              dbColumn.MaxLength = definitionColumn.MaxLength;
            }
            // *** Next Statement *** Delete - 11/24/22
            //dbColumn.Value = reflect.GetValue(propertyName)
          }
          retValue.Add(dbColumn);
        }
      }
      return retValue;
    }

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public static DbColumns LJCDeserialize(string fileSpec = null)
    {
      DbColumns retValue;

      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(DbColumns), fileSpec)
        as DbColumns;
      return retValue;
    }

    // Creates a ColumnNames list from a DataObject.
    /// <summary>
    /// Creates a PropertyNames list from a DataObject.
    /// </summary>
    /// <param name="dataObject"></param>
    /// <returns>The PropertyNames list.</returns>
    public static List<string> LJCGetPropertyNames(object dataObject)
    {
      List<string> retValue = null;

      LJCReflect reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = new List<string>();
        foreach (string propertyName in propertyNames)
        {
          if ("ChangedNames" == propertyName)
          {
            continue;
          }
          retValue.Add(propertyName);
        }
      }
      return retValue;
    }
    #endregion

    #region Static Conversion Functions

    // Creates a DbValues object from a DbColumns object.
    /// <include path='items/DbValues/*' file='Doc/DbColumns.xml'/>
    public static implicit operator DbValues(DbColumns dbColumns)
    {
      DbValues retValue = null;

      if (NetCommon.HasItems(dbColumns))
      {
        retValue = new DbValues();
        foreach (DbColumn dbColumn in dbColumns)
        {
          var dbValue = dbColumn;
          retValue.Add(dbValue);
        }
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public DbColumns()
    {
      mPrevCount = -1;
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public DbColumns(DbColumns items)
    {
      if (NetCommon.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the object element to the collection
    /// <include path='items/Add/*' file='Doc/DbColumns.xml'/>
    public new void Add(DbColumn dbColumn)
    {
      LJCSortAddOrderIndex();
      base.Add(dbColumn);
      int newIndex = Count - 1;
      dbColumn.AddOrderIndex = newIndex;
    }

    // Creates the Object from the arguments and adds it to the collection. (R)
    /// <include path='items/Add1/*' file='Doc/DbColumns.xml'/>
    public DbColumn Add(string columnName, int position, int maxLength)
    {
      DbColumn retValue = new DbColumn()
      {
        AutoIncrement = false,
        ColumnName = columnName,
        MaxLength = maxLength,
        Position = position
      };
      Add(retValue);
      return retValue;
    }

    // Creates the Object from the arguments and adds it to the collection.
    /// <include path='items/Add2/*' file='Doc/DbColumns.xml'/>
    public DbColumn Add(string columnName, string propertyName = null, string renameAs = null
      , string dataTypeName = "String", string caption = null)
    {
      DbColumn retValue = new DbColumn()
      {
        AutoIncrement = false,
        Caption = caption,
        ColumnName = columnName,
        DataTypeName = dataTypeName,
        PropertyName = propertyName,
        RenameAs = renameAs
      };
      Add(retValue);
      return retValue;
    }

    // Creates the Object from the arguments and adds it to the collection.
    /// <include path='items/Add3/*' file='Doc/DbColumns.xml'/>
    public DbColumn Add(string columnName, object value
      , string dataTypeName = "String")
    {
      DbColumn retValue = new DbColumn()
      {
        AutoIncrement = false,
        ColumnName = columnName,
        DataTypeName = dataTypeName,
        Value = value
      };
      Add(retValue);
      return retValue;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public DbColumns Clone()
    {
      var retValue = MemberwiseClone() as DbColumns;
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Creates the DbColumn from the supplied values and adds to the collection.
    /// <include path='items/LJCAddPropertyAs/*' file='Doc/DbColumns.xml'/>
    public DbColumn LJCAddPropertyAs(string propertyName, string caption = null
      , string renameAs = null, string dataTypeName = "String")
    {
      DbColumn retValue = new DbColumn()
      {
        ColumnName = propertyName,
        PropertyName = propertyName,
        Caption = caption,
        DataTypeName = dataTypeName,
        AutoIncrement = false,
        Value = null,
        RenameAs = renameAs
      };

      if (false == NetString.HasValue(renameAs))
      {
        retValue.RenameAs = retValue.PropertyName;
      }
      Add(retValue);
      return retValue;
    }

    // Serializes the collection
    /// <include path='items/LJCSerialize/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public void LJCSerialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Other Methods

    // Sets the IsChanged value to false for all elements in the collection.
    /// <include path='items/LJCClearChanged/*' file='Doc/DbColumns.xml'/>
    public void LJCClearChanged()
    {
      foreach (DbColumn dbColumn in this)
      {
        dbColumn.IsChanged = false;
      }
    }

    // Gets a collection of changed columns.
    /// <include path='items/LJCGetChanged/*' file='Doc/DbColumns.xml'/>
    public DbColumns LJCGetChanged()
    {
      List<DbColumn> columns;
      DbColumns retValue = new DbColumns();

      columns = FindAll(x => true == x.IsChanged);
      foreach (DbColumn dbColumn in columns)
      {
        retValue.Add(dbColumn.Clone());
      }
      return retValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public DbColumn LJCGetColumn(string propertyName)
    {
      DbColumn retValue = null;

      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.PropertyName) != 0)
      {
        retValue = Find(x => x.PropertyName == propertyName);
      }
      else
      {
        retValue = LJCSearchPropertyName(propertyName);
      }
      return retValue;
    }

    // Returns a set of columns that match the supplied list.
    /// <include path='items/LJCGetColumns/*' file='Doc/DbColumns.xml'/>
    public DbColumns LJCGetColumns(List<string> propertyNames)
    {
      DbColumn searchColumn;
      DbColumns retValue = null;

      if (propertyNames != null && propertyNames.Count > 0)
      {
        retValue = new DbColumns();
        foreach (string propertyName in propertyNames)
        {
          searchColumn = LJCSearchPropertyName(propertyName);
          if (null == searchColumn)
          {
            throw new ArgumentException($"Property {propertyName} was not found.");
          }
          retValue.Add(new DbColumn(searchColumn));
        }
      }
      return retValue;
    }

    // Get the minimum date value.
    /// <include path='items/LJCGetMinSqlDate/*' file='Doc/DbColumns.xml'/>
    public static string LJCGetMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Sets the caption properties.
    /// <include path='items/LJCSetColumnCaptions/*' file='Doc/DbColumns.xml'/>
    public void LJCSetColumnCaptions(DbColumns dbColumns)
    {
      DbColumn searchColumn;

      if (NetCommon.HasItems(dbColumns))
      {
        foreach (DbColumn dbColumn in dbColumns)
        {
          // *** Next Statement *** Change - 11/24/22
          //searchColumn = LJCSearchName(dbColumn.ColumnName);
          searchColumn = LJCSearchPropertyName(dbColumn.PropertyName);
          if (searchColumn != null)
          {
            dbColumn.Caption = searchColumn.Caption;
          }
        }
      }
    }

    // Maps the column property and rename values.
    /// <include path='items/MapNames/*' file='Doc/DbColumns.xml'/>
    public void MapNames(string columnName, string propertyName = null
      , string renameAs = null, string caption = null)
    {
      DbColumn dbColumn = LJCSearchColumnName(columnName);
      SetMapValues(dbColumn, propertyName, renameAs, caption);
    }

    // Sets the Map values.
    private void SetMapValues(DbColumn dbColumn, string propertyName = null
      , string renameAs = null, string caption = null)
    {
      if (dbColumn != null)
      {
        if (propertyName != null)
        {
          dbColumn.PropertyName = propertyName;
        }
        if (renameAs != null)
        {
          dbColumn.RenameAs = renameAs;
        }
        if (caption != null)
        {
          dbColumn.Caption = caption;
        }
      }
    }
    #endregion

    #region Search and Sort Methods

    // Finds and returns the object that matches the supplied values.
    /// <include path='items/LJCSearchName/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public DbColumn LJCSearchColumnName(string name)
    {
      DbColumnNameComparer comparer;
      DbColumn retValue = null;

      comparer = new DbColumnNameComparer();
      LJCSortName(comparer);

      DbColumn searchDbColumn = new DbColumn()
      {
        ColumnName = name
      };
      int index = BinarySearch(searchDbColumn, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and returns the column that contains the supplied property name.
    /// <include path='items/LJCSearchPropertyName/*' file='Doc/DbColumns.xml'/>
    public DbColumn LJCSearchPropertyName(string propertyName)
    {
      DbColumn retValue = null;

      var comparer = new DbColumnPropertyComparer();
      LJCSortProperty(comparer);
      DbColumn searchDbColumn = new DbColumn()
      {
        PropertyName = propertyName
      };
      int index = BinarySearch(searchDbColumn, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Finds and returns the column that contains the supplied property name.
    /// <include path='items/LJCSearchRenameAs/*' file='Doc/DbColumns.xml'/>
    public DbColumn LJCSearchRenameAs(string renameAs)
    {
      DbColumnRenameAsComparer comparer;
      DbColumn retValue = null;

      comparer = new DbColumnRenameAsComparer();
      LJCSortRenameAs(comparer);

      DbColumn searchDbColumn = new DbColumn()
      {
        RenameAs = renameAs
      };
      int index = BinarySearch(searchDbColumn, comparer);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    /// <summary>Sort on AddOrderIndex.</summary>
    public void LJCSortAddOrderIndex()
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.AddOrderIndex) != 0)
      {
        mPrevCount = Count;
        Sort();
        mSortType = SortType.AddOrderIndex;
      }
    }

    /// <summary>Sort on ColumnName.</summary>
    public void LJCSortName(DbColumnNameComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.ColumnName) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.ColumnName;
      }
    }

    /// <summary>Sort on RenameAs.</summary>
    public void LJCSortProperty(DbColumnPropertyComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.PropertyName) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.PropertyName;
      }
    }

    /// <summary>Sort on RenameAs.</summary>
    public void LJCSortRenameAs(DbColumnRenameAsComparer comparer)
    {
      if (Count != mPrevCount
        || mSortType.CompareTo(SortType.RenameAs) != 0)
      {
        mPrevCount = Count;
        Sort(comparer);
        mSortType = SortType.RenameAs;
      }
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include path='items/LJCGetBoolean/*' file='Doc/DbColumns.xml'/>
    public bool LJCGetBoolean(string propertyName)
    {
      bool retValue = default;

      var value = LJCGetValue(propertyName);
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

      var value = LJCGetValue(propertyName);
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

      var value = LJCGetValue(propertyName);
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

      var value = LJCGetValue(propertyName);
      if (value != null)
      {
        retValue = DateTime.Parse(value);
      }
      return retValue;
    }

    // Gets the column object value as a decimal.
    /// <include path='items/LJCGetDecimal/*' file='Doc/DbColumns.xml'/>
    public decimal LJCGetDecimal(string propertyName)
    {
      decimal retValue = default;

      var value = LJCGetValue(propertyName);
      if (value != null)
      {
        retValue = Convert.ToDecimal(value);
      }
      return retValue;
    }

    // Gets the column object value as a double.
    /// <include path='items/LJCGetDouble/*' file='Doc/DbColumns.xml'/>
    public double LJCGetDouble(string propertyName)
    {
      double retValue = default;

      var value = LJCGetValue(propertyName);
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
      short retValue = default;

      var value = LJCGetValue(propertyName);
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
      int retValue = default;

      var value = LJCGetValue(propertyName);
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
      long retValue = default;

      var value = LJCGetValue(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt64(value);
      }
      return retValue;
    }

    // Gets the column object value as a single.
    /// <include path='items/LJCGetSingle/*' file='Doc/DbColumns.xml'/>
    public float LJCGetSingle(string propertyName)
    {
      float retValue = default;

      var value = LJCGetValue(propertyName);
      if (value != null)
      {
        retValue = Convert.ToSingle(value);
      }
      return retValue;
    }

    // Gets the string value for the column with the specified name.
    /// <include path='items/LJCGetValue/*' file='Doc/DbColumns.xml'/>
    public string LJCGetValue(string propertyName)
    {
      string retValue = default;

      var dbColumn = LJCSearchPropertyName(propertyName);
      if (dbColumn != null
        && dbColumn.Value != null
        && NetString.HasValue(dbColumn.Value.ToString()))
      {
        retValue = dbColumn.Value.ToString();
      }
      return retValue;
    }

    // Sets the value for the column with the specified name.
    /// <include path='items/LJCSetValue/*' file='Doc/DbColumns.xml'/>
    public void LJCSetValue(string propertyName, object value)
    {
      if (value != null)
      {
        var dbColumn = LJCSearchPropertyName(propertyName);
        if (null == dbColumn)
        {
          Add(propertyName, value);
        }
        else
        {
          dbColumn.Value = value;
        }
      }
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DbColumns.xml"; }
    }

    // The column for the specified name.
    /// <include path='items/Item/*' file='Doc/DbColumns.xml'/>
    public DbColumn this[string propertyName]
    {
      get { return LJCSearchPropertyName(propertyName); }
    }
    #endregion

    #region Class Data

    private int mPrevCount;
    private SortType mSortType;

    private enum SortType
    {
      AddOrderIndex,
      ColumnName,
      PropertyName,
      RenameAs
    }
    #endregion
  }
}
