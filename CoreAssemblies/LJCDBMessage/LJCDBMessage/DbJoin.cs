// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbJoin.cs
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Represents a database table join.</summary>
  public class DbJoin
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbJoin()
    {
      JoinType = "Left";
      JoinOns = new DbJoinOns();
      Columns = new LJCDataColumns();
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbJoin(DbJoin item)
    {
      Columns = new LJCDataColumns(item.Columns);
      JoinOns = new DbJoinOns(item.JoinOns);
      JoinType = item.JoinType;
      TableAlias = item.TableAlias;
      TableName = item.TableName;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbJoin Clone()
    {
      DbJoin retValue = new DbJoin()
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

    #region Properties

    // The included join table columns.
    /// <include file='Doc/DbJoin.xml'
    ///  path='items/Columns/*'/>
    public LJCDataColumns Columns { get; set; }

    /// <summary>The join on definitions.</summary>
    public DbJoinOns JoinOns { get; set; }

    // The join type.
    /// <include file='Doc/DbJoin.xml'
    ///  path='items/JoinType/*'/>
    public string JoinType
    {
      get { return mJoinType; }
      set { mJoinType = NetString.InitString(value); }
    }
    private string mJoinType;

    /// <summary>The table alias.</summary>
    public string TableAlias
    {
      get { return mTableAlias; }
      set { mTableAlias = NetString.InitString(value); }
    }
    private string mTableAlias;

    /// <summary>The table name.</summary>
    public string TableName
    {
      get { return mTableName; }
      set { mTableName = NetString.InitString(value); }
    }
    private string mTableName;
    #endregion
  }
}
