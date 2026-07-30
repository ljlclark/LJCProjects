// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDataColumns.cs
using System.Xml.Serialization;

namespace LJCNetCommon5
{
  // Represents a collection of LJCDataColumn objects.
  /// <include file='Doc/LJCDataColumns.xml'
  ///  path='members/LJCDataColumns/*'/>
  [XmlRoot("LJCDataColumns")]
  public class LJCDataColumns : List<LJCDataColumn>
  {
    #region Static Methods

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCDeserialize/*'/>
    public static LJCDataColumns? LJCDeserialize(string? fileSpec = null)
    {
      LJCDataColumns retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = LJC.XmlDeserialize(typeof(LJCDataColumns), fileSpec)
        as LJCDataColumns;
      return retValue;
    }

    // Get the minimum date value.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCMinSqlDate/*'/>
    public static string LJCMinSqlDate()
    {
      return "1753/01/01 00:00:00";
    }

    // Gets a collection of items from a data object.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCObjectColumns/*'/>
    public static LJCDataColumns? LJCObjectColumns(object dataObject
      , LJCDataColumns? dataDefinition = null)
    {
      LJCDataColumn definitionColumn = null;
      LJCDataColumns retValue = null;

      var reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = [];
        foreach (string propertyName in propertyNames)
        {
          if ("ChangedNames" == propertyName)
          {
            continue;
          }

          if (dataDefinition != null)
          {
            // Get where DataColumn property = "PropertyName"
            //   , value = propertyName.
            var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
              , propertyName);
            definitionColumn = dataDefinition.LJCGetUnique(keys);
          }

          var dataColumn = new LJCDataColumn()
          {
            Caption = propertyName,
            ColumnName = propertyName,
            Value = reflect.GetValue(propertyName)
          };

          Type type = reflect.GetPropertyType(propertyName);
          if (type != null)
          {
            dataColumn.DataTypeName = type.Name;
            if (definitionColumn != null
              && "String" == type.Name)
            {
              dataColumn.MaxLength = definitionColumn.MaxLength;
            }
          }
          retValue.Add(dataColumn);
        }
      }
      return retValue;
    }

    // Gets a collection of items from a data object that match the supplied
    // property Names.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCObjectColumnsInList/*'/>
    public static LJCDataColumns? LJCObjectColumnsInList(object dataObject
      , List<string>? propertyNames = null)
    {
      var retValue = LJCObjectColumns(dataObject);
      if (retValue != null
        && propertyNames != null)
      {
        retValue = retValue.LJCColumns(propertyNames);
      }
      return retValue;
    }

    // Gets a list of property names from a data object.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCObjectPropertyNames/*'/>
    public static List<string>? LJCObjectPropertyNames(object dataObject)
    {
      List<string> retValue = null;

      var reflect = new LJCReflect(dataObject);
      List<string> propertyNames = reflect.GetPropertyNames();

      if (propertyNames != null)
      {
        retValue = [];
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

    // Operator to create LJCDataValues from LJCDataColumns.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/DataColumnsToDataValues/*'/>
    public static implicit operator LJCDataValues(LJCDataColumns dataColumns)
    {
      LJCDataValues retValue = [];

      if (LJC.HasListItems(dataColumns))
      {
        foreach (LJCDataColumn dataColumn in dataColumns)
        {
          var dataValue = dataColumn;
          retValue.Add(dataValue);
        }
      }
      return retValue;
    }

    // Checks if the key columns value has changed.
    private static bool IsKeyColumnsChanged(LJCDataColumns? newKeys
      , LJCDataColumns? currentKeys)
    {
      bool retValue = false;

      while (true)
      {
        var hasNewColumns = LJC.HasListItems(newKeys);
        var hasSortColumns = LJC.HasListItems(currentKeys);

        // One value has no columns.
        if ((!hasNewColumns
          && hasSortColumns)
          || hasNewColumns
          && !hasSortColumns)
        {
          retValue = true;
          break;
        }

        if (hasNewColumns)
        {
          if (newKeys!.Count != currentKeys!.Count)
          {
            retValue = true;
            break;
          }

          for (short index = 0; index < newKeys.Count; index++)
          {
            var newColumn = newKeys[index];
            var currentColumn = currentKeys[index];

            var propertyName = newColumn.PropertyName;
            var propertyValue = newColumn.Value;
            var sortPropertyName = currentColumn.PropertyName;
            var sortPropertyValue = currentColumn.Value;
            if (propertyName.CompareTo(sortPropertyName) != 0
              || !EqualityComparer<object>.Default.Equals(propertyValue
              , sortPropertyValue))
            {
              retValue = true;
              break;
            }
          }
        }
        break;
      }
      return retValue;
    }

    // Sets the Map values.
    private static void SetMapValues(LJCDataColumn dataColumn
      , string? propertyName = null, string? renameAs = null
      , string? caption = null)
    {
      if (dataColumn != null)
      {
        if (propertyName != null)
        {
          dataColumn.PropertyName = propertyName;
        }
        if (renameAs != null)
        {
          dataColumn.RenameAs = renameAs;
        }
        if (caption != null)
        {
          dataColumn.Caption = caption;
        }
      }
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDataColumns()
    {
      _PrevCount = -1;
    }

    // Initializes an object from the supplied items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public LJCDataColumns(LJCDataColumns items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDataColumn(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDataColumns Clone()
    {
      var retValue = new LJCDataColumns();
      foreach (var dataColumn in this)
      {
        var newDataColumn = dataColumn.Clone();
        if (newDataColumn != null)
        {
          retValue.Add(newDataColumn);
        }
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

    // Gets a collection of changed columns.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCChanged/*'/>
    public LJCDataColumns LJCChanged()
    {
      List<LJCDataColumn> dataColumns;
      var retValue = new LJCDataColumns();

      dataColumns = FindAll(x => x.IsChanged);
      foreach (LJCDataColumn dataColumn in dataColumns)
      {
        var newDataColumn = dataColumn.Clone();
        if (newDataColumn != null)
        {
          retValue.Add(newDataColumn);
        }
      }
      return retValue;
    }

    // Sets the IsChanged value to false for all items.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCClearChanged/*'/>
    public void LJCClearChanged()
    {
      foreach (LJCDataColumn dataColumn in this)
      {
        dataColumn.IsChanged = false;
      }
    }

    // Returns a collection of items that match a list of property names.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCColumns/*'/>
    public LJCDataColumns? LJCColumns(List<string> propertyNames)
    {
      LJCDataColumn searchColumn;
      LJCDataColumns retValue = null;

      if (LJC.HasListItems(propertyNames))
      {
        retValue = [];
        foreach (string propertyName in propertyNames)
        {
          var searchName = LJCNetString.GetSearchName(propertyName);
          // Get where DataColumn property = "PropertyName"
          //   , value = searchName.
          var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
            , searchName);
          searchColumn = LJCGetUnique(keys);
          if (searchColumn != null)
          {
            retValue.Add(new LJCDataColumn(searchColumn));
          }
        }
      }
      return retValue;
    }

    // Gets a list of property names from the collection items.
    /// <include file='Doc/LJCDataValues.xml'
    ///  path='members/LJCKeyPropertyNames/*'/>
    public List<string>? LJCKeyPropertyNames(LJCDataColumns? dataColumns = null)
    {
      List<string>? retList = null;

      if (!LJC.HasListItems(dataColumns))
      {
        dataColumns = _Keys;
      }
      if (LJC.HasListItems(dataColumns))
      {
        retList = [];
        foreach (var dataColumn in dataColumns)
        {
          retList.Add(dataColumn.PropertyName);
        }
      }
      return retList;
    }

    // Gets a list of property names from the collection items.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCPropertyNames/*'/>
    public List<string>? LJCPropertyNames(LJCDataColumns? dataColumns = null)
    {
      List<string>? retList = null;

      if (!LJC.HasListItems(dataColumns))
      {
        dataColumns = this;
      }
      if (LJC.HasListItems(dataColumns))
      {
        retList = [];
        foreach (var dataColumn in dataColumns)
        {
          retList.Add(dataColumn.PropertyName);
        }
      }
      return retList;
    }

    // Serializes the collection
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='members/LJCSerialize/*'/>
    public void LJCSerialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Collection Data Methods

    // Adds the supplied item to the collection
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Add1/*'/>
    public new void Add(LJCDataColumn dataColumn)
    {
      base.Add(dataColumn);
      int newIndex = Count - 1;
      dataColumn.AddOrderIndex = newIndex;
    }

    // Creates item with Position and MaxLength and adds it to the collection.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Add2/*'/>
    public LJCDataColumn Add(string propertyName, int position, int maxLength)
    {
      var retValue = new LJCDataColumn()
      {
        PropertyName = propertyName,
        Position = position,
        MaxLength = maxLength,

        AutoIncrement = false,
        DataTypeName = "string",
      };
      Add(retValue);
      return retValue;
    }

    // Creates item with Value and adds it to the collection.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Add3/*'/>
    public LJCDataColumn Add(string propertyName, object? value = null
      , string dataTypeName = "string", int maxLength = 5)
    {
      var retValue = new LJCDataColumn()
      {
        PropertyName = propertyName,
        Value = value,
        DataTypeName = dataTypeName,
        MaxLength = maxLength,

        AutoIncrement = false,
      };
      Add(retValue);
      return retValue;
    }

    // Returns the column that matches the key columns.
    // The column is identified by its property names and values.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetUnique/*'/>
    public LJCDataColumn? LJCGetUnique(LJCDataColumns? keys = null)
    {
      LJCDataColumn retValue = null;

      if (keys != null)
      {
        LJCKeys = keys;
      }

      if (LJC.HasListItems(LJCKeys))
      {
        LJCSort();

        // Create search item.
        var dataColumn = new LJCDataColumn();
        var reflect = new LJCReflect(dataColumn);
        foreach (var keyColumn in LJCKeys)
        {
          reflect.SetValue(keyColumn.PropertyName, keyColumn.Value);
        }

        // Create comparer.
        DataColumnKeyComparer comparer = null;
        var propertyNames = LJCPropertyNames(LJCKeys);
        if (propertyNames != null)
        {
          comparer = new DataColumnKeyComparer()
          {
            LJCPropertyNames = propertyNames,
          };
        }

        int index = BinarySearch(dataColumn, comparer);
        if (index > -1)
        {
          retValue = this[index];
        }
      }
      return retValue;
    }

    // Removes the item with the supplied property name.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCRemove/*'/>
    public void LJCRemove(string propertyName)
    {
      LJCDataColumn column = Find(x => x.PropertyName == propertyName);
      if (column != null)
      {
        Remove(column);
      }
    }

    // Add or Update.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCSetData/*'/>
    public void LJCSetData(LJCDataColumn dataColumn)
    {
      if (LJC.HasListItems(this))
      {
        // Get where DataColumn property = "PropertyName"
        //   , value = dataColumn.PropertyName.
        var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
          , dataColumn.PropertyName);
        var updateColumn = LJCGetUnique(keys);
        if (updateColumn != null)
        {
          updateColumn.AllowDBNull = dataColumn.AllowDBNull;
          updateColumn.AutoIncrement = dataColumn.AutoIncrement;
          updateColumn.Caption = dataColumn.Caption;
          updateColumn.ColumnName = dataColumn.ColumnName;
          updateColumn.DataTypeName = dataColumn.DataTypeName;
          updateColumn.MaxLength = dataColumn.MaxLength;
          updateColumn.Position = dataColumn.Position;
          updateColumn.PropertyName = dataColumn.PropertyName;
          updateColumn.RenameAs = dataColumn.RenameAs;
          updateColumn.SQLTypeName = dataColumn.SQLTypeName;
          updateColumn.Value = dataColumn.Value;
        }
        else
        {
          Add(dataColumn);
        }
      }
    }

    // Sorts on the current key columns.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCSort/*'/>
    public void LJCSort(LJCDataColumns? keys = null)
    {
      if (LJC.HasListItems(keys))
      {
        LJCKeys = keys;
      }

      if (_IsPendingSort)
      {
        var sortNames = LJCKeyPropertyNames(_Keys);
        if (sortNames != null)
        {
          var uniqueComparer = new DataColumnKeyComparer
          {
            LJCPropertyNames = sortNames
          };
          Sort(uniqueComparer);
        }
      }
      _IsPendingSort = false;
    }
    #endregion

    #region Other Public Methods

    // Sets caption properties for supplied columns from current columns.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCSetCaptions/*'/>
    public void LJCSetCaptions(LJCDataColumns dataColumns)
    {
      if (LJC.HasListItems(dataColumns))
      {
        foreach (var dataColumn in dataColumns)
        {
          // Get where DataColumn property = "PropertyName"
          //   , value = dataColumn.PropertyName.
          var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
            , dataColumn.PropertyName);
          var foundColumn = LJCGetUnique(keys);
          if (foundColumn != null)
          {
            dataColumn.Caption = foundColumn.Caption;
          }
        }
      }
    }

    // Maps the column property and rename values.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCMapNames/*'/>
    public void LJCMapNames(string columnName, string? propertyName = null
      , string? renameAs = null, string? caption = null)
    {
      // Get where DataColumn property = "PropertyName"
      //   , value = columnName.
      var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
        , columnName);
      var dataColumn = LJCGetUnique(keys);
      if (dataColumn != null)
      {
        SetMapValues(dataColumn, propertyName, renameAs, caption);
      }
    }
    #endregion

    #region Value Methods

    // Gets the column object value as a bool.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetBoolean/*'/>
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
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetByte/*'/>
    public byte LJCGetByte(string propertyName)
    {
      byte retValue = default;

      if (LJC.HasText(propertyName))
      {
        var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName, propertyName);
        var dataColumn = LJCGetUnique(keys);
        if (dataColumn != null
          && dataColumn.Value != null)
        {
          retValue = LJC.GetByte(dataColumn.Value);
        }
      }
      return retValue;
    }

    // Gets the column object value as a byte array.
    /// <include file="Doc/LJCDataValues.xml"
    /// path="members/LJCGetBytes/*"/>
    public byte[]? LJCGetBytes(string propertyName)
    {
      byte[] retValue = default;

      if (LJC.HasText(propertyName))
      {
        // Get where DataColumn property = "PropertyName"
        //   , value = propertyName.
        var keys = LJC.Keys("PropertyName", propertyName);
        var dataColumn = LJCGetUnique(keys);
        if (dataColumn != null
          && dataColumn.Value != null)
        {
          retValue = LJC.GetBytes(dataColumn.Value);
        }
      }
      return retValue;
    }

    // Gets the column object value as a char.
    /// <include file='Doc/LJCDataColumns.xml'
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
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetDbDateTime/*'/>
    public DateTime LJCGetDbDateTime(string propertyName)
    {
      DateTime retValue = DateTime.Parse(LJCMinSqlDate());

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = DateTime.Parse(value);
      }
      return retValue;
    }

    // Gets the column object value as a decimal.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetDecimal/*'/>
    public decimal LJCGetDecimal(string propertyName)
    {
      decimal retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToDecimal(value);
      }
      return retValue;
    }

    // Gets the column object value as a double.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetDouble/*'/>
    public double LJCGetDouble(string propertyName)
    {
      double retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToDouble(value);
      }
      return retValue;
    }

    // Gets the column object value as a short int.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetInt16/*'/>
    public short LJCGetInt16(string propertyName)
    {
      short retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt16(value);
      }
      return retValue;
    }

    // Gets the column object value as an int.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetInt32/*'/>
    public int LJCGetInt32(string propertyName)
    {
      int retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt32(value);
      }
      return retValue;
    }

    // Gets the column object value as a long int.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetInt64/*'/>
    public long LJCGetInt64(string propertyName)
    {
      long retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToInt64(value);
      }
      return retValue;
    }

    // Gets the column object value as a single.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetSingle/*'/>
    public float LJCGetSingle(string propertyName)
    {
      float retValue = default;

      var value = LJCGetString(propertyName);
      if (value != null)
      {
        retValue = Convert.ToSingle(value);
      }
      return retValue;
    }

    // Gets the column object value as a string.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetString/*'/>
    public string? LJCGetString(string propertyName)
    {
      string retValue = default;

      if (LJC.HasText(propertyName))
      {
        // Get where DataColumn property = "PropertyName"
        //   , value = propertyName.
        var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
          , propertyName);
        var dataColumn = LJCGetUnique(keys);

        if (dataColumn != null
          && dataColumn.Value != null
          && LJC.HasText(dataColumn.Value.ToString()))
        {
          retValue = dataColumn.Value.ToString();
        }
      }
      return retValue;
    }

    // Gets the column object value.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCGetValue/*'/>
    public object? LJCGetValue(string propertyName)
    {
      object retValue = default;

      if (LJC.HasListItems(this)
        && LJC.HasText(propertyName))
      {
        // Get where DataColumn property = "PropertyName"
        //   , value = propertyName.
        var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
          , propertyName);
        var dataColumn = LJCGetUnique(keys);
        if (dataColumn != null
          && dataColumn.Value != null)
        {
          retValue = dataColumn.Value;
        }
      }
      return retValue;
    }

    // Sets the column object value.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCSetValue/*'/>
    public void LJCSetValue(string propertyName, object value)
    {
      if (LJC.HasListItems(this)
        && LJC.HasText(propertyName))
      {
        // Get where DataColumn property = "PropertyName"
        //   , value = propertyName.
        var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
          , propertyName);
        var dataColumn = LJCGetUnique(keys);

        if (dataColumn != null)
        {
          dataColumn.Value = value;
        }
      }
    }
    #endregion

    #region Properties

    // Gets the Default File Name.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCDefaultFileName/*'/>
    public static string LJCDefaultFileName
    {
      get { return "LJCDataColumns.xml"; }
    }

    // Gets or sets the key columns.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/LJCKeyColumns/*'/>
    public LJCDataColumns? LJCKeys
    {
      get => _Keys;
      set
      {
        if (IsKeyColumnsChanged(value, _Keys))
        {
          _IsPendingSort = true;
        }
        // Must be done after check for changes.
        _Keys = value;

        // New sort if count has changed.
        if (Count != _PrevCount)
        {
          _IsPendingSort = true;
          _PrevCount = Count;
        }
      }
    }
    private LJCDataColumns? _Keys;

    // Returns the item with the supplied property name.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Item/*'/>
    public LJCDataColumn? this[string propertyName]
    {
      get
      {
        // Get where DataColumn property = "PropertyName"
        //   , value = propertyName.
        var keys = LJC.Keys(LJCDataColumn.ColumnPropertyName
          , propertyName);
        return LJCGetUnique(keys);
      }
    }
    #endregion

    #region Class Data

    private bool _IsPendingSort;
    private int _PrevCount;
    #endregion
  }

  // Sort and search on key values.
  /// <include file='Doc/LJCDataColumns.xml'
  ///  path='members/DataColumnKeyComparer/*'/>
  public class DataColumnKeyComparer : IComparer<LJCDataColumn>
  {
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/ColumnNames/*'/>
    public List<string>? LJCPropertyNames { get; set; }

    // Compares two objects.
    /// <include file='Doc/LJCDataColumns.xml'
    ///  path='members/Compare/*'/>
    public int Compare(LJCDataColumn? x, LJCDataColumn? y)
    {
      int retValue;

      // Check for null objects.
      retValue = LJC.CompareNull(x, y);

      while (true)
      {
        // End if one or both objects are null.
        if (null == LJCPropertyNames
          || retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        var xReflect = new LJCReflect(x!);
        var yReflect = new LJCReflect(y!);

        // Check for null values.
        foreach (string propertyName in LJCPropertyNames)
        {
          var xValue = xReflect.GetString(propertyName);
          var yValue = yReflect.GetString(propertyName);
          retValue = LJC.CompareNull(xValue, yValue);

          // Break if one or both values are null.
          if (retValue != LJCNetString.CompareNotNullOrEqual)
          {
            break;
          }
        }

        // End if one or both values are null.
        if (retValue != LJCNetString.CompareNotNullOrEqual)
        {
          break;
        }

        for (int index = 0; index < LJCPropertyNames.Count; index++)
        {
          var propertyName = LJCPropertyNames[index];
          var xValue = xReflect.GetString(propertyName);
          var yValue = yReflect.GetString(propertyName);

          if (xValue != null)
          {
            if (index < LJCPropertyNames.Count - 1)
            {
              // Compare parent keys.
              retValue = xValue.CompareTo(yValue);
              if (retValue != LJCNetString.CompareEqual)
              {
                break;
              }
            }
            else
            {
              // Compare value if parent keys are equal.
              retValue = xValue.CompareTo(yValue);
            }
          }
        }
        break;
      }
      return retValue;
    }
  }
}
