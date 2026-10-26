// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoin.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a database table join.
  /// <include file='Doc/DBJoin.xml'
  ///  path='items/LJCDBJoin/*'/>
  public class LJCDBJoin
  {
    #region Properties

    // The included join table columns.
    /// <include file='Doc/DbJoin.xml'
    ///  path='items/Columns/*'/>
    public LJCDataColumns Columns { get; set; }

    // The join on definitions.
    /// <include file='Doc/DbJoin.xml'
    ///  path='items/JoinOns/*'/>
    public LJCDBJoinOns JoinOns { get; set; }

    // The join type.
    /// <include file='Doc/DbJoin.xml'
    ///  path='items/JoinType/*'/>
    public string? JoinType
    {
      get => mJoinType;
      set => mJoinType = value?.Trim();
    }
    private string? mJoinType;

    // The table alias.
    /// <include file='Doc/DbJoin.xml'
    ///  path='items/TableAlias/*'/>
    public string? TableAlias
    {
      get => mTableAlias;
      set => mTableAlias = value?.Trim();
    }
    private string? mTableAlias;

    // The table name.
    /// <include file='Doc/DbJoin.xml'
    ///  path='items/TableName/*'/>
    public string TableName
    {
      get => mTableName;
      set => mTableName = value.Trim();
    }
    private string mTableName;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Constructor/*'/>
    public LJCDBJoin()
    {
      mTableName = "";
      JoinType = "Left";
      JoinOns = [];
      Columns = [];
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/ConstructorCopy/*'/>
    public LJCDBJoin(LJCDBJoin item)
    {
      mTableName = "";
      //Columns = new LJCDataColumns(item.Columns);
      Columns = [.. item.Columns];
      //JoinOns = new LJCDBJoinOns(item.JoinOns);
      JoinOns = [.. item.JoinOns];
      JoinType = item.JoinType;
      TableAlias = item.TableAlias;
      TableName = item.TableName;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCDBJoin Clone()
    {
      LJCDBJoin retValue = new()
      {
        JoinType = JoinType,
        TableName = TableName,
      };
      if (JoinOns != null)
      {
        retValue.JoinOns = JoinOns.Clone();
      }
      if (Columns != null)
      {
        retValue.Columns = Columns.Clone();
      }
      return retValue;
    }
    #endregion
  }
}
