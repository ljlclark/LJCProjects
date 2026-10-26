// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBConditionSet.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents the conditions and properties.
  /// <include file='Doc/DbConditionSet.xml'
  ///  path='members/LJCDbConditionSet/*'/>
  public class LJCDBConditionSet
  {
    #region Properties

    // The conditions boolean operator.
    /// <include file='Doc/DbConditionSet.xml'
    ///  path='members/BooleanOperator/*'/>
    public string? BooleanOperator
    {
      get => mBooleanOperator;
      set => mBooleanOperator = value?.Trim();
    }
    private string? mBooleanOperator;

    // Gets or sets the conditions.
    /// <include file='Doc/DbConditionSet.xml'
    ///  path='members/Conditions/*'/>
    public LJCDBConditions Conditions { get; set; }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/DbConditionSet.xml'
    ///  path='members/Constructor/*'/>
    public LJCDBConditionSet()
    {
      //Conditions = new DbConditions();
      Conditions = [];
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/ConstructorCopy/*'/>
    public LJCDBConditionSet(LJCDBConditionSet item)
    {
      BooleanOperator = item.BooleanOperator;
      //Conditions = new DbConditions(item.Conditions);
      Conditions = [.. item.Conditions];
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include file='Doc/DbConditionSet.xml'
    ///  path='members/Clone/*'/>
    public LJCDBConditionSet? Clone()
    {
      LJCDBConditionSet? retValue = MemberwiseClone() as LJCDBConditionSet;
      if (retValue != null)
      {
        retValue.Conditions = Conditions.Clone();
      }
      return retValue;
    }
    #endregion
  }
}
