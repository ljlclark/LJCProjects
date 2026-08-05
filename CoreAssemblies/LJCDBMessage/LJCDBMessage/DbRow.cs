// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbRow.cs
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Represents a result Row.</summary>
  public class DbRow
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/HasItems1/*'/>
    public static bool HasItems(DbRow collection)
    {
      bool retValue = false;

      if (collection != null && collection.Values.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbRow()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbRow(DbRow items)
    {
      if (HasItems(items))
      {
        Values = new LJCDataValues();
        foreach (var item in items.Values)
        {
          Values.Add(new LJCDataValue(item));
        }
      }
    }
    #endregion

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbRow Clone()
    {
      DbRow retValue = MemberwiseClone() as DbRow;
      return retValue;
    }

    #region Properties

    // The row value for the specified value column index.
    /// <include file='Doc/DbRow.xml'
    ///  path='items/Item1/*'/>
    public LJCDataValue this[int columnIndex]
    {
      get
      {
        LJCDataValue retValue = null;
        if (null != Values)
        {
          retValue = Values[columnIndex];
        }
        return retValue;
      }
    }

    // The row value for the specified value property name.
    /// <include file='Doc/DbRow.xml'
    ///  path='items/Item2/*'/>
    public LJCDataValue this[string propertyName]
    {
      get
      {
        LJCDataValue retValue = null;
        if (null != Values)
        {
          //retValue = Values.LJCPropertyName(propertyName);
          retValue = Values[propertyName];
        }
        return retValue;
      }
    }

    /// <summary>Gets or sets the row values.</summary>
    public LJCDataValues Values { get; set; }
    #endregion
  }
}
