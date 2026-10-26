// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBJoinOn.cs
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a Join On definition.
  /// <include file='Doc/DBJoinOn.xml'
  ///  path='members/LJCDBJoinOn/*'/>
  public class LJCDBJoinOn
  {
    #region Properties

    // The Boolean Operator value.
    /// <include file='Doc/DBJoinOn.xml'
    ///  path='members/BooleanOperator/*'/>
    public string BooleanOperator
    {
      get => mBooleanOperator;
      set => mBooleanOperator = value;
    }
    private string mBooleanOperator;

    // The 'From' column name.
    /// <include file='Doc/DBJoinOn.xml'
    ///  path='members/FromColumnName/*'/>
    public string? FromColumnName
    {
      get => mFromColumnName;
      set => mFromColumnName = value?.Trim();
    }
    private string? mFromColumnName;

    // The Join On Operator.
    /// <include file='Doc/DBJoinOn.xml'
    ///  path='members/JoinOnOperator/*'/>
    public string JoinOnOperator
    {
      get => mJoinOnOperator;
      set => mJoinOnOperator = value;
    }
    private string mJoinOnOperator;

    // Gets or sets the contained JoinOns.
    /// <include file='Doc/DBJoinOn.xml'
    ///  path='members/JoinOns/*'/>
    public LJCDBJoinOns JoinOns { get; set; }

    // The 'To' column name.
    /// <include file='Doc/DBJoinOn.xml'
    ///  path='members/ToColumnName/*'/>
    public string? ToColumnName
    {
      get => mToColumnName;
      set => mToColumnName = value?.Trim();
    }
    private string? mToColumnName;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCDBJoinOn()
    {
      mBooleanOperator = "";
      mJoinOnOperator = "";

      BooleanOperator = "and";
      JoinOnOperator = "=";
      JoinOns = [];
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/CopyConstructor/*'/>
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
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCDBJoinOn? Clone()
    {
      LJCDBJoinOn? retValue = MemberwiseClone() as LJCDBJoinOn;
      return retValue;
    }
    #endregion
  }
}
