// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ProcedureParameter.cs
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace LJCDataAccess
{
  /// <summary>Represents a Procedure Parameter.</summary>
  public class ProcedureParameter : IComparable<ProcedureParameter>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ProcedureParameter()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ProcedureParameter(ProcedureParameter item)
    {
      Direction = item.Direction;
      MySqlDbType = item.MySqlDbType;
      ParameterName = item.ParameterName;
      Precision = item.Precision;
      Size = item.Size;
      SqlDbType = item.SqlDbType;
      Value = item.Value;
    }
    #endregion

    #region Methods

    // The object string identifier.
    /// <include path='items/ToString/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public override string ToString()
    {
      return ParameterName;
    }

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public ProcedureParameter Clone()
    {
      ProcedureParameter retValue = MemberwiseClone() as ProcedureParameter;
      return retValue;
    }

    // Provides the default Sort functionality.
    /// <include path='items/CompareTo/*' file='../../../CoreUtilities/LJCDocLib/Common/Data.xml'/>
    public int CompareTo(ProcedureParameter other)
    {
      int retValue;

      if (null == other)
      {
        retValue = 1;
      }
      else
      {
        // Case sensitive.
        //retValue = _CompareToName_.CompareTo(other._CompareToName_);

        // Not case sensitive.
        retValue = string.Compare(ParameterName, other.ParameterName, true);
      }
      return retValue;
    }
    #endregion

    #region Properties

    /// <summary>Gets or sets the ParameterDirection value.</summary>
    public ParameterDirection Direction { get; set; }

    /// <summary>Gets or sets the MySqlDbType value.</summary>
    public MySqlDbType MySqlDbType { get; set; }

    /// <summary>Gets or sets the ParameterName value.</summary>
    public string ParameterName { get; set; }

    /// <summary>Gets or sets the Precision value.</summary>
    public short Precision { get; set; }

    /// <summary>Gets or sets the Size value.</summary>
    public int Size { get; set; }

    /// <summary>Gets or sets the SqlDbType value.</summary>
    public SqlDbType SqlDbType { get; set; }

    /// <summary>Gets or sets the Value.</summary>
    public object Value { get; set; }
    #endregion
  }
}
