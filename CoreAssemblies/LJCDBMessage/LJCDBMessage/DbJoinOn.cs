// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbJoinOn.cs
using LJCNetCommon;

namespace LJCDBMessage
{
  /// <summary>Represents a Join On definition.</summary>
  public class DbJoinOn
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbJoinOn()
    {
      BooleanOperator = "and";
      JoinOns = new DbJoinOns();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbJoinOn(DbJoinOn item)
    {
      BooleanOperator = item.BooleanOperator;
      FromColumnName = item.FromColumnName;
      JoinOnOperator = item.JoinOnOperator;
      JoinOns = new DbJoinOns(item.JoinOns);
      ToColumnName = item.ToColumnName;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public DbJoinOn Clone()
    {
      DbJoinOn retValue = MemberwiseClone() as DbJoinOn;
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The Boolean Operator value.</summary>
    public string BooleanOperator
    {
      get { return mBooleanOperator; }
      set { mBooleanOperator = NetString.InitString(value); }
    }
    private string mBooleanOperator;

    /// <summary>The 'From' column name.</summary>
    public string FromColumnName
    {
      get { return mFromColumnName; }
      set { mFromColumnName = NetString.InitString(value); }
    }
    private string mFromColumnName;

    /// <summary>The Join On Operator.</summary>
    public string JoinOnOperator
    {
      get { return mJoinOnOperator; }
      set { mJoinOnOperator = NetString.InitString(value); }
    }
    private string mJoinOnOperator;

    /// <summary>Gets or sets the contained JoinOns.</summary>
    public DbJoinOns JoinOns { get; set; }

    /// <summary>The 'To' column name.</summary>
    public string ToColumnName
    {
      get { return mToColumnName; }
      set { mToColumnName = NetString.InitString(value); }
    }
    private string mToColumnName;
    #endregion
  }
}
