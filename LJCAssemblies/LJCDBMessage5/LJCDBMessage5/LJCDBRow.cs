// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBRow.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  /// <summary>Represents a result Row.</summary>
  public class LJCDBRow
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/HasItems1/*'/>
    public static bool HasItems(LJCDBRow collection)
    {
      bool retValue = false;

      if (collection != null
        && LJC.HasListItems(collection.Values))
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
    public LJCDBRow()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public LJCDBRow(LJCDBRow items)
    {
      if (HasItems(items)
        && LJC.HasListItems(items.Values))
      {
        Values = [];
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
    public LJCDBRow? Clone()
    {
      LJCDBRow? retValue = MemberwiseClone() as LJCDBRow;
      return retValue;
    }

    #region Properties

    // The row value for the specified value column index.
    /// <include file='Doc/DbRow.xml'
    ///  path='items/Item1/*'/>
    public LJCDataValue? this[int columnIndex]
    {
      get
      {
        LJCDataValue? retValue = null;
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
    public LJCDataValue? this[string propertyName]
    {
      get
      {
        LJCDataValue? retValue = null;
        if (null != Values)
        {
          //retValue = Values.LJCSearchPropertyName(propertyName);
          retValue = Values[propertyName];
        }
        return retValue;
      }
    }

    /// <summary>Gets or sets the row values.</summary>
    public LJCDataValues? Values { get; set; }
    #endregion
  }
}
