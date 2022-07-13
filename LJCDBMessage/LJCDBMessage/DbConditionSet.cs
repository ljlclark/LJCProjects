// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
using LJCNetCommon;

namespace LJCDBMessage
{
  // Represents the conditions and properties.
  /// <include path='items/DbConditionSet/*' file='Doc/DbConditionSet.xml'/>
  public class DbConditionSet
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DbConditionSetC/*' file='Doc/DbConditionSet.xml'/>
    public DbConditionSet()
    {
      Conditions = new DbConditions();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbConditionSet(DbConditionSet item)
    {
      BooleanOperator = item.BooleanOperator;
      Conditions = new DbConditions(item.Conditions);
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='Doc/DbConditionSet.xml'/>
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
