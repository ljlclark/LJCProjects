// Copyright(c) Lester J. Clark and Contributors.
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
    /// <include path='items/HasItems1/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public static bool HasItems(LJCDBRow collection)
    {
      bool retValue = false;

      if (collection != null
        && LJC.HasItems(collection.Values))
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBRow()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public LJCDBRow(LJCDBRow items)
    {
      if (HasItems(items)
        && LJC.HasItems(items.Values))
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBRow? Clone()
    {
      LJCDBRow? retValue = MemberwiseClone() as LJCDBRow;
      return retValue;
    }

    #region Properties

    // The row value for the specified value column index.
    /// <include path='items/Item1/*' file='Doc/DbRow.xml'/>
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
    /// <include path='items/Item2/*' file='Doc/DbRow.xml'/>
    public LJCDataValue? this[string propertyName]
    {
      get
      {
        LJCDataValue? retValue = null;
        if (null != Values)
        {
          retValue = Values.LJCSearchPropertyName(propertyName);
        }
        return retValue;
      }
    }

    /// <summary>Gets or sets the row values.</summary>
    public LJCDataValues? Values { get; set; }
    #endregion
  }
}
