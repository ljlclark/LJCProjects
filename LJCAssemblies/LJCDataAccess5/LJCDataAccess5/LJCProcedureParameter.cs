// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcedureParameter.cs
using System.Data;
using LJCNetCommon5;

namespace LJCDataAccess5
{
  /// <summary>Represents a Procedure Parameter.</summary>
  public class LJCProcedureParameter : IComparable<LJCProcedureParameter>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCProcedureParameter()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CopyConstructor/*'/>
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

    /// <summary>A Create constructor.</summary>
    public LJCProcedureParameter(string parameterName, int sqlDbTypeID
      , int size, object? value = null
      , ParameterDirection direction = ParameterDirection.Input
      , int mySqlDbTypeID = -1)
    {
      ParameterName = parameterName;
      SqlDbTypeID = sqlDbTypeID;
      Size = size;
      Direction = direction;
      Value = value;
      MySqlDbTypeID = mySqlDbTypeID;
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/ToString/*'/>
    public override string? ToString()
    {
      return ParameterName;
    }

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCProcedureParameter? Clone()
    {
      LJCProcedureParameter? retValue = MemberwiseClone() as LJCProcedureParameter;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/CompareTo/*'/>
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

    #region Properties

    /// <summary>Gets or sets the ParameterDirection value.</summary>
    public ParameterDirection Direction { get; set; }

    /// <summary>Gets or sets the MySqlDbTypeID value.</summary>
    public int MySqlDbTypeID { get; set; }

    /// <summary>Gets or sets the ParameterName value.</summary>
    public string? ParameterName { get; set; }

    /// <summary>Gets or sets the Precision value.</summary>
    public short Precision { get; set; }

    /// <summary>Gets or sets the Size value.</summary>
    public int Size { get; set; }

    /// <summary>Gets or sets the SqlDbType value.</summary>
    public int SqlDbTypeID { get; set; }

    /// <summary>Gets or sets the Value.</summary>
    public object? Value { get; set; }
    #endregion
  }
}
