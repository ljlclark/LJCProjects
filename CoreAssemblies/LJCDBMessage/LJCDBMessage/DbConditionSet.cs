// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbConditionSet.cs
using LJCNetCommon;

namespace LJCDBMessage
{
  // Represents the conditions and properties.
  /// <include file='Doc/DbConditionSet.xml'
  ///  path='items/DbConditionSet/*'/>
  public class DbConditionSet
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/DbConditionSet.xml'
    ///  path='items/DbConditionSetC/*'/>
    public DbConditionSet()
    {
      Conditions = new DbConditions();
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbConditionSet(DbConditionSet item)
    {
      BooleanOperator = item.BooleanOperator;
      Conditions = new DbConditions(item.Conditions);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='Doc/DbConditionSet.xml'
    ///  path='items/Clone/*'/>
    public DbConditionSet Clone()
    {
      DbConditionSet retValue = MemberwiseClone() as DbConditionSet;
      retValue.Conditions = Conditions.Clone();
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>The conditions boolean operator.</summary>
    public string BooleanOperator
    {
      get
      {
        return mBooleanOperator;
      }
      set
      {
        mBooleanOperator = NetString.InitString(value);
      }
    }
    private string mBooleanOperator;

    /// <summary>Gets or sets the conditions.</summary>
    public DbConditions Conditions { get; set; }
  }
  #endregion
}
