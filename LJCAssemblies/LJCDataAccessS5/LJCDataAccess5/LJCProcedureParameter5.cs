// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCProcedureParameter5.cs
using System.Data;
using LJCNetCommon5;

namespace LJCDataAccess5
{
  // Represents a Procedure Parameter.
  /// <include file='Doc/ProcedureParameter.xml'
  ///  path='members/LJCProcedureParameter/*'/>
  public class LJCProcedureParameter : IComparable<LJCProcedureParameter>
  {
    #region Properties

    // Gets or sets the ParameterDirection value.
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/Direction/*'/>
    public ParameterDirection Direction { get; set; }

    // Gets or sets the MySqlDbTypeID value.
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/MySqlDbTypeID/*'/>
    public int MySqlDbTypeID { get; set; }

    // Gets or sets the ParameterName value.
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/ParameterName/*'/>
    public string? ParameterName { get; set; }

    // Gets or sets the Precision value.
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/Precision/*'/>
    public short Precision { get; set; }

    // Gets or sets the Size value.
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/Size/*'/>
    public int Size { get; set; }

    // Gets or sets the SqlDbType value.
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/SqlDbTypeID/*'/>
    public int SqlDbTypeID { get; set; }

    // Gets or sets the Value.
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/Value/*'/>
    public object? Value { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Constructor/*'/>
    public LJCProcedureParameter()
    {
    }

    // Initializes an object instance with the supplied values. 
    /// <include file='Doc/ProcedureParameter.xml'
    ///  path='members/ConstructorParam/*'/>
    public LJCProcedureParameter(string parameterName, int sqlDbTypeID
      , int size, object? value = null
      , ParameterDirection direction = ParameterDirection.Input
      , int mySqlDbTypeID = -1) : this()
    {
      ParameterName = parameterName;
      SqlDbTypeID = sqlDbTypeID;
      Size = size;
      Direction = direction;
      Value = value;
      MySqlDbTypeID = mySqlDbTypeID;
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/ConstructorCopy/*'/>
    public LJCProcedureParameter(LJCProcedureParameter item)
    {
      Direction = item.Direction;
      MySqlDbTypeID = item.MySqlDbTypeID;
      ParameterName = item.ParameterName;
      Precision = item.Precision;
      Size = item.Size;
      SqlDbTypeID = item.SqlDbTypeID;
      Value = item.Value;
    }
    #endregion

    #region Data Class Methods

    // The object string identifier.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/ToString/*'/>
    public override string? ToString()
    {
      return ParameterName;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/Clone/*'/>
    public LJCProcedureParameter? Clone()
    {
      LJCProcedureParameter? retValue = MemberwiseClone() as LJCProcedureParameter;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='members/CompareTo/*'/>
    public int CompareTo(LJCProcedureParameter? other)
    {
      int retValue;

      if (null == other)
      {
        retValue = LJCNetString.CompareGreater;
      }
      else
      {
        retValue = LJC.CompareNull(ParameterName, other.ParameterName);
        if (LJCNetString.CompareNotNullOrEqual == retValue)
        {
          // Not case sensitive.
          retValue = string.Compare(ParameterName, other.ParameterName, true);
        }
      }
      return retValue;
    }
    #endregion
  }
}
