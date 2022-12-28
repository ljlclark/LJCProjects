// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbRequest.cs
using System.Collections.Generic;
using System.Xml.Serialization;
using LJCNetCommon;
using LJCDataAccess;

namespace LJCDBMessage
{
  /// <include path='items/DbRequest/*' file='Doc/ProjectDBMessage.xml'/>
  [XmlRoot("DbRequest")]
  public class DbRequest
  {
    #region Static Functions

    // Deserializes the DbRequest message.
    /// <include path='items/Deserialize/*' file='Doc/DbRequest.xml'/>
    public static DbRequest Deserialize(string request)
    {
      DbRequest retValue = null;

      if (NetString.HasValue(request))
      {
        retValue = NetCommon.XmlDeserializeMessage(typeof(DbRequest)
          , request) as DbRequest;
        if (null == retValue)
        {
          retValue = new DbRequest();
        }
      }
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbRequest()
    {
      OrderByNames = new List<string>();
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbRequest(DbRequest item)
    {
      AddMissingColumns = item.AddMissingColumns;
      DbAssignedColumns = new DbColumns(item.DbAssignedColumns);
      ClientSql = item.ClientSql;
      Columns = new DbColumns(item.Columns);
      DataConfigName = item.DataConfigName;
      Filters = new DbFilters(item.Filters);
      Joins = new DbJoins(item.Joins);
      KeyColumns = new DbColumns(item.KeyColumns);
      OrderByNames = item.OrderByNames;
      PageSize = item.PageSize;
      Parameters = new ProcedureParameters(item.Parameters);
      PageStartIndex = item.PageStartIndex;
      ProcedureName = item.ProcedureName;
      RequestTypeName = item.RequestTypeName;
      SchemaName = item.SchemaName;
      TableName = item.TableName;
    }

    // Initializes an object instance with the supplied values.
    /// <include path='items/DbRequestC/*' file='Doc/DbRequest.xml'/>
    public DbRequest(RequestType requestType, string tableName
      , string dataConfigName = null)
    {
      RequestTypeName = requestType.ToString();
      TableName = tableName;
      DataConfigName = dataConfigName;
      OrderByNames = new List<string>();
    }
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbRequest Clone()
    {
      DbRequest retValue = MemberwiseClone() as DbRequest;
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
      return retValue;
    }

    // Serializes the object and returns the serialized string.
    /// <include path='items/Serialize1/*' file='Doc/DbRequest.xml'/>
    public string Serialize()
    {
      string retValue;

      retValue = NetCommon.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include path='items/Serialize2/*' file='Doc/DbRequest.xml'/>
    public void Serialize(string fileSpec)
    {
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Data Properties

    // The included table columns.
    /// <include path='items/Columns/*' file='Doc/DbRequest.xml'/>
    public DbColumns Columns { get; set; }

    /// <summary>The data configuration name.</summary>
    public string DataConfigName
    {
      get { return mDataConfigName; }
      set { mDataConfigName = NetString.InitString(value); }
    }
    private string mDataConfigName;

    /// <summary>The where clause filters.</summary>
    public DbFilters Filters { get; set; }

    /// <summary>The table joins.</summary>
    public DbJoins Joins { get; set; }

    // The key column values.
    /// <include path='items/KeyColumns/*' file='Doc/DbRequest.xml'/>
    public DbColumns KeyColumns { get; set; }

    /// <summary>Gets or sets the ProcedureName value.</summary>
    public string ProcedureName
    {
      get { return mProcedureName; }
      set { mProcedureName = NetString.InitString(value); }
    }
    private string mProcedureName;

    // The request type name.
    /// <include path='items/RequestTypeName/*' file='Doc/DbRequest.xml'/>
    public string RequestTypeName
    {
      get { return mRequestTypeName; }
      set { mRequestTypeName = NetString.InitString(value); }
    }
    private string mRequestTypeName;

    /// <summary>The schema name.</summary>
    public string SchemaName
    {
      get { return mSchemaName; }
      set { mSchemaName = NetString.InitString(value); }
    }
    private string mSchemaName;

    /// <summary>The table name.</summary>
    public string TableName
    {
      get { return mTableName; }
      set { mTableName = NetString.InitString(value); }
    }
    private string mTableName;
    #endregion

    #region Other Properties

    /// <summary>Indicates if the missing column should be added.</summary>
    public bool AddMissingColumns { get; set; }

    /// <summary>The Database Assigned columns.</summary>
    public DbColumns DbAssignedColumns { get; set; }

    /// <summary>Gets or sets the ClientSql value.</summary>
    public string ClientSql
    {
      get { return mClientSql; }
      set { mClientSql = NetString.InitString(value); }
    }
    private string mClientSql;

    /// <summary>The OrderBy column names.</summary>
    public List<string> OrderByNames { get; set; }

    /// <summary>The number of records in the page.</summary>
    public int PageSize { get; set; }

    /// <summary>Gets or sets the Parameters list reference.</summary>
    public ProcedureParameters Parameters { get; set; }

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
