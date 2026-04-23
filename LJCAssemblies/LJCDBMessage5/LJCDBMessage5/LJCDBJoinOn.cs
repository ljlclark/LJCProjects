// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoinOn.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  /// <summary>Represents a Join On definition.</summary>
  public class LJCDBJoinOn
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoinOn()
    {
      mBooleanOperator = "";
      mJoinOnOperator = "";

      BooleanOperator = "and";
      JoinOnOperator = "=";
      JoinOns = [];
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoinOn(LJCDBJoinOn item)
    {
      mBooleanOperator = "";
      mJoinOnOperator = "";

      BooleanOperator = item.BooleanOperator;
      FromColumnName = item.FromColumnName;
      JoinOnOperator = item.JoinOnOperator;
      JoinOns = [.. item.JoinOns];
      ToColumnName = item.ToColumnName;
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBJoinOn? Clone()
    {
      LJCDBJoinOn? retValue = MemberwiseClone() as LJCDBJoinOn;
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The Boolean Operator value.</summary>
    public string BooleanOperator
    {
      get { return mBooleanOperator; }
      set
      {
        if (value != null)
        {
          mBooleanOperator = value;
        }
      }
    }
    private string mBooleanOperator;

    /// <summary>The 'From' column name.</summary>
    public string? FromColumnName
    {
      get { return mFromColumnName; }
      set { mFromColumnName = LJCNetString.InitString(value); }
    }
    private string? mFromColumnName;

    /// <summary>The Join On Operator.</summary>
    public string JoinOnOperator
    {
      get { return mJoinOnOperator; }
      set
      {
        if (value != null)
        {
          mJoinOnOperator = value;
        }
      }
    }
    private string mJoinOnOperator;

    /// <summary>Gets or sets the contained JoinOns.</summary>
    public LJCDBJoinOns JoinOns { get; set; }

    /// <summary>The 'To' column name.</summary>
    public string? ToColumnName
    {
      get { return mToColumnName; }
      set { mToColumnName = LJCNetString.InitString(value); }
    }
    private string? mToColumnName;
    #endregion
  }
}
