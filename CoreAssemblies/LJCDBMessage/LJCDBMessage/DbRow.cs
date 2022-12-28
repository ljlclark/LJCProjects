// Copyright(c) Lester J.Clark and Contributors.
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
    /// <include path='items/HasItems1/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(DbRow collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Values.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public DbRow()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Collection.xml'/>
    public DbRow(DbRow items)
    {
      if (HasItems(items))
      {
        Values = new DbValues();
        foreach (var item in items.Values)
        {
          Values.Add(new DbValue(item));
        }
      }
    }
    #endregion

    #region Properties

    // The row value for the specified value column index.
    /// <include path='items/Item1/*' file='Doc/DbRow.xml'/>
    public DbValue this[int columnIndex]
    {
      get
      {
        DbValue retValue = null;
        if (null != Values)
        {
          retValue = Values[columnIndex];
        }
        return retValue;
      }
    }

    // The row value for the specified value property name.
    /// <include path='items/Item2/*' file='Doc/DbRow.xml'/>
    public DbValue this[string propertyName]
    {
      get
      {
        DbValue retValue = null;
        if (null != Values)
        {
          retValue = Values.LJCSearchPropertyName(propertyName);
        }
        return retValue;
      }
    }

    /// <summary>Gets or sets the row values.</summary>
    public DbValues Values { get; set; }
    #endregion
  }
}
