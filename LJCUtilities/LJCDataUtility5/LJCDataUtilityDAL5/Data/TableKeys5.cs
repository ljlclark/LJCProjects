// Copyright (c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// TableKeys5.cs
using LJCNetCommon5;
using System.Xml.Linq;

namespace LJCDataUtilityDAL5
{
  // Represents a collection of ForeignKey objects.
  /// <include file='Doc/TableKeys.xml'
  ///  path='members/TableKeys/*'/>
  public class TableKeys : List<TableKey>
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public TableKeys()
    {
      _ArgError = new LJCArgError("LJCDataUtilityDAL.DataModules");
      _PrevCount = -1;
    }

    // The Copy constructor.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/CopyConstructor/*'/>
    public TableKeys(TableKeys items) : this()
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new TableKey(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Creates and returns a clone of the object.
    /// <include file='../../LJCGenDoc5/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public TableKeys Clone()
    {
      var retValue = new TableKeys();
      foreach (TableKey foreignKey in this)
      {
        var newKey = foreignKey.Clone();
        if (newKey != null)
        {
          retValue.Add(newKey);
        }
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../LJCGenDoc5/Common/Collection.xml'
    ///  path='members/LJCHasItems/*'/>
    public bool LJCHasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }
    #endregion

    #region Collection Data Methods

    // Retrieve the collection element.
    /// <include file='Doc/TableKeys.xml'
    ///  path='members/LJCGetUnique/*'/>
    public TableKey? LJCGetUnique(string constraintName, int ordinalPosition)
    {
      TableKey? retValue = null;

      UniqueCheck(constraintName, ordinalPosition);
      _ArgError.ThrowError();

      LJCSortUnique();
      var searchItem = new TableKey()
      {
        ConstraintName = constraintName,
        OrdinalPosition = ordinalPosition,
      };
      int index = BinarySearch(searchItem);
      if (index > -1)
      {
        retValue = this[index];
      }
      return retValue;
    }

    // Checks the unique parameters.
    private void UniqueCheck(string constraintName, int ordinalPosition)
    {
      string message = "";
      if (ordinalPosition <= 0)
      {
        message += "ordinalPosition must be greater than zero.\r\n";
      }
      _ArgError.Add(constraintName, "constraintName");
      _ArgError.Add(message);
    }
    #endregion

    #region Sort Methods

    // Sort on unique values.
    /// <include file='Doc/TableKeys.xml'
    ///  path='members/LJCSortUnique/*'/>
    public void LJCSortUnique()
    {
      if (Count != _PrevCount)
      {
        _PrevCount = Count;
        Sort();
      }
    }
    #endregion

    #region Properties

    // The item for the supplied name.
    /// <include file='Doc/TableKeys.xml'
    ///  path='members/UniqueIndexer/*'/>
    public TableKey? this[string constraintName, int ordinalPosition]
    {
      get => LJCGetUnique(constraintName, ordinalPosition);
    }
    #endregion

    #region Class Data

    private readonly LJCArgError _ArgError;
    private int _PrevCount;
    #endregion
  }
}
