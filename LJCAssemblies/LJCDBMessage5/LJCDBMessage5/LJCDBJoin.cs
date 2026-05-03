// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoin.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  /// <summary>Represents a database table join.</summary>
  public class LJCDBJoin
  {
    #region Constructor Methods

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoin()
    {
      mTableName = "";
      JoinType = "Left";
      JoinOns = new LJCDBJoinOns();
      Columns = new LJCDataColumns();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoin(LJCDBJoin item)
    {
      mTableName = "";
      Columns = new LJCDataColumns(item.Columns);
      JoinOns = new LJCDBJoinOns(item.JoinOns);
      JoinType = item.JoinType;
      TableAlias = item.TableAlias;
      TableName = item.TableName;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoin Clone()
    {
      LJCDBJoin retValue = new LJCDBJoin()
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
    public LJCDataColumns Columns { get; set; }

    /// <summary>The join on definitions.</summary>
    public LJCDBJoinOns JoinOns { get; set; }

    // The join type.
    /// <include path='items/JoinType/*' file='Doc/DbJoin.xml'/>
    public string? JoinType
    {
      get => mJoinType;
      set => mJoinType = LJCNetString.InitString(value);
    }
    private string? mJoinType;

    /// <summary>The table alias.</summary>
    public string? TableAlias
    {
      get => mTableAlias;
      set => mTableAlias = LJCNetString.InitString(value);
    }
    private string? mTableAlias;

    /// <summary>The table name.</summary>
    public string TableName
    {
      get => mTableName;
      set
      {
        if (value != null)
        {
          mTableName = value.Trim();
        }
      }
    }
    private string mTableName;
    #endregion
  }
}
