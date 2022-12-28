// Copyright(c) Lester J.Clark and Contributors.
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
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbJoin()
    {
      JoinType = "Left";
      JoinOns = new DbJoinOns();
      Columns = new DbColumns();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbJoin(DbJoin item)
    {
      Columns = new DbColumns(item.Columns);
      JoinOns = new DbJoinOns(item.JoinOns);
      JoinType = item.JoinType;
      TableAlias = item.TableAlias;
      TableName = item.TableName;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
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
    /// <include path='items/Columns/*' file='Doc/DbJoin.xml'/>
    public DbColumns Columns { get; set; }

    /// <summary>The join on definitions.</summary>
    public DbJoinOns JoinOns { get; set; }

    // The join type.
    /// <include path='items/JoinType/*' file='Doc/DbJoin.xml'/>
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
