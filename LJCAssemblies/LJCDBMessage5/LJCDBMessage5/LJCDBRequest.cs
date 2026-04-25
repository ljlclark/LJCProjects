// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBRequest.cs
using System.Xml.Serialization;
using LJCNetCommon5;
using LJCDataAccess5;
using System.Runtime.InteropServices.ObjectiveC;

namespace LJCDBMessage5
{
  /// <include path='items/DbRequest/*' file='Doc/ProjectDBMessage.xml'/>
  [XmlRoot("DbRequest")]
  public class LJCDBRequest
  {
    #region Static Functions

    // Deserializes the DbRequest message.
    /// <include path='items/Deserialize/*' file='Doc/DbRequest.xml'/>
    public static LJCDBRequest? Deserialize(string request)
    {
      LJCDBRequest? retValue = null;

      if (LJC.HasValue(request))
      {
        retValue = LJC.XmlDeserializeMessage(typeof(LJCDBRequest)
          , request) as LJCDBRequest;
        if (null == retValue)
        {
          retValue = new LJCDBRequest();
        }
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBRequest()
    {
      //OrderByNames = new List<string>();
      OrderByNames = [];
      mRequestTypeName = RequestType.Select.ToString();
      //mTableName = "TableName";
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBRequest(LJCDBRequest item)
    {
      mRequestTypeName = RequestType.Select.ToString();
      //mTableName = "TableName";

      AddMissingColumns = item.AddMissingColumns;
      if (item.DbAssignedColumns != null)
      {
        DbAssignedColumns = [.. item.DbAssignedColumns];
      }
      ClientSql = item.ClientSql;
      if (item.Columns != null)
      {
        Columns = [.. item.Columns];
      }
      DataConfigName = item.DataConfigName;
      if (item.Filters != null)
      {
        Filters = [.. item.Filters];
      }
      if (item.Joins != null)
      {
        Joins = [.. item.Joins];
      }
      if (item.KeyColumns != null)
      {
        KeyColumns = [.. item.KeyColumns];
      }
      OrderByNames = item.OrderByNames;
      PageSize = item.PageSize;
      //Parameters = new ProcedureParameters(item.Parameters);
      if (item.Parameters != null)
      {
        Parameters = [.. item.Parameters];
      }
      PageStartIndex = item.PageStartIndex;
      ProcedureName = item.ProcedureName;
      RequestTypeName = item.RequestTypeName;
      SchemaName = item.SchemaName;
      TableName = item.TableName;
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DbRequestC/*' file='Doc/DbRequest.xml'/>
    public LJCDBRequest(RequestType requestType, string? tableName
      , string? dataConfigName = null)
    {
      mRequestTypeName = requestType.ToString();
      mTableName = tableName;

      RequestTypeName = requestType.ToString();
      TableName = tableName;
      DataConfigName = dataConfigName;
      //OrderByNames = new List<string>();
      OrderByNames = [];
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBRequest? Clone()
    {
      LJCDBRequest? retValue = MemberwiseClone() as LJCDBRequest;
      if (retValue != null)
      {
        if (Columns != null)
        {
          retValue.Columns = Columns.Clone();
        }
        if (KeyColumns != null)
        {
          retValue.KeyColumns = KeyColumns.Clone();
        }
        if (Joins != null)
        {
          retValue.Joins = Joins.Clone();
        }
        if (Filters != null)
        {
          retValue.Filters = Filters.Clone();
        }
      }
      return retValue;
    }

    // Serializes the object and returns the serialized string.
    /// <include path='items/Serialize1/*' file='Doc/DbRequest.xml'/>
    public string Serialize()
    {
      string retValue;

      retValue = LJC.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include path='items/Serialize2/*' file='Doc/DbRequest.xml'/>
    public void Serialize(string fileSpec)
    {
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Data Properties

    // The included table columns.
    /// <include path='items/Columns/*' file='Doc/DbRequest.xml'/>
    public LJCDataColumns? Columns { get; set; }

    /// <summary>The data configuration name.</summary>
    public string? DataConfigName
    {
      get { return mDataConfigName; }
      set { mDataConfigName = LJCNetString.InitString(value); }
    }
    private string? mDataConfigName;

    /// <summary>The where clause filters.</summary>
    public LJCDBFilters? Filters { get; set; }

    /// <summary>The table joins.</summary>
    public LJCDBJoins? Joins { get; set; }

    // The key column values.
    /// <include path='items/KeyColumns/*' file='Doc/DbRequest.xml'/>
    public LJCDataColumns? KeyColumns { get; set; }

    /// <summary>Gets or sets the ProcedureName value.</summary>
    public string? ProcedureName
    {
      get => mProcedureName;
      set => mProcedureName = LJCNetString.InitString(value);
    }
    private string? mProcedureName;

    // The request type name.
    /// <include path='items/RequestTypeName/*' file='Doc/DbRequest.xml'/>
    public string RequestTypeName
    {
      get => mRequestTypeName;
      set
      {
        if (value != null)
        {
          mRequestTypeName = value.Trim();
        }
      }
    }
    private string mRequestTypeName;

    /// <summary>The schema name.</summary>
    public string? SchemaName
    {
      get => mSchemaName;
      set => mSchemaName = LJCNetString.InitString(value);
    }
    private string? mSchemaName;

    /// <summary>The table name.</summary>
    public string? TableName
    {
      get => mTableName;
      set
      {
        mTableName = value;
        if (value != null)
        {
          mTableName = value.Trim();
        }
      }
    }
    private string? mTableName;
    #endregion

    #region Other Properties

    /// <summary>Indicates if the missing column should be added.</summary>
    public bool AddMissingColumns { get; set; }

    /// <summary>The Database Assigned columns.</summary>
    public LJCDataColumns? DbAssignedColumns { get; set; }

    /// <summary>Gets or sets the ClientSql value.</summary>
    public string? ClientSql
    {
      get => mClientSql;
      set => mClientSql = LJCNetString.InitString(value);
    }
    private string? mClientSql;

    /// <summary>The OrderBy column names.</summary>
    public List<string>? OrderByNames { get; set; }

    /// <summary>The number of records in the page.</summary>
    public int PageSize { get; set; }

    /// <summary>Gets or sets the Parameters list reference.</summary>
    public LJCProcedureParameters? Parameters { get; set; }

    /// <summary>The page starting index.</summary>
    public int PageStartIndex { get; set; }
    #endregion
  }

  /// <summary>The RequestType values.</summary>
  public enum RequestType
  {
    /// <summary>The "Delete" request type.</summary>
    Delete,

    /// <summary>The "ExecuteSQL" request type.</summary>
    ExecuteSQL,

    /// <summary>The "Insert" request type.</summary>
    Insert,

    /// <summary>The "Load" request type.</summary>
    Load,

    /// <summary>The "LoadSQL" request type.</summary>
    LoadSQL,

    /// <summary>The "RetrieveSQL" request type.</summary>
    RetrieveSQL,

    /// <summary>The "SchemaOnly" request type.</summary>
    SchemaOnly,

    /// <summary>The "Select" request type.</summary>
    Select,

    /// <summary>The "SelectProcedure" request type.</summary>
    SelectProcedure,

    /// <summary>The "TableNames" request type.</summary>
    TableNames,

    /// <summary>The "Update" request type.</summary>
    Update,
  }
}
