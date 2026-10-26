// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBRequest.cs
using System.Xml.Serialization;
using LJCNetCommon5;
using LJCDataAccess5;

namespace LJCDBMessage5
{
  /// <include file='Doc/DBRequest.xml'
  ///  path='items/LJCDbRequest/*'/>
  [XmlRoot("DbRequest")]
  public class LJCDBRequest
  {
    #region Static Methods

    // Deserializes the DbRequest message.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/Deserialize/*'/>
    public static LJCDBRequest? Deserialize(string request)
    {
      LJCDBRequest? retValue = null;

      if (LJC.HasText(request))
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

    #region Data Properties

    // The included table columns.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/Columns/*'/>
    public LJCDataColumns? Columns { get; set; }

    // The data configuration name.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/DataConfigName/*'/>
    public string? DataConfigName
    {
      get => mDataConfigName;
      set => mDataConfigName = value?.Trim();
    }
    private string? mDataConfigName;

    // The where clause filters.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/Filters/*'/>
    public LJCDBFilters? Filters { get; set; }

    // The table joins.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/Joins/*'/>
    public LJCDBJoins? Joins { get; set; }

    // The key column values.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/KeyColumns/*'/>
    public LJCDataColumns? KeyColumns { get; set; }

    // Gets or sets the ProcedureName value.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/ProcedureName/*'/>
    public string? ProcedureName
    {
      get => mProcedureName;
      set => mProcedureName = value?.Trim();
    }
    private string? mProcedureName;

    // The request type name.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/RequestTypeName/*'/>
    public string RequestTypeName
    {
      get => mRequestTypeName;
      set => mRequestTypeName = value.Trim();
    }
    private string mRequestTypeName;

    // The schema name.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/SchemaName/*'/>
    public string? SchemaName
    {
      get => mSchemaName;
      set => mSchemaName = value?.Trim();
    }
    private string? mSchemaName;

    // The table name.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/TableName/*'/>
    public string? TableName
    {
      get => mTableName;
      set => mTableName = value?.Trim();
    }
    private string? mTableName;
    #endregion

    #region Other Properties

    // Indicates if the missing column should be added.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/AddMissingColumns/*'/>
    public bool AddMissingColumns { get; set; }

    // The Database Assigned columns.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/DbAssignedColumns/*'/>
    public LJCDataColumns? DbAssignedColumns { get; set; }

    // Gets or sets the ClientSql value.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/ClientSql/*'/>
    public string? ClientSql
    {
      get => mClientSql;
      set => mClientSql = value?.Trim();
    }
    private string? mClientSql;

    // The OrderBy column names.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/OrderByNames/*'/>
    public List<string>? OrderByNames { get; set; }

    // The number of records in the page.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/PageSize/*'/>
    public int PageSize { get; set; }

    // Gets or sets the Parameters list reference.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/Parameters/*'/>
    public LJCProcedureParameters? Parameters { get; set; }

    // The page starting index.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/PageStartIndex/*'/>
    public int PageStartIndex { get; set; }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Constructor/*'/>
    public LJCDBRequest()
    {
      //OrderByNames = new List<string>();
      OrderByNames = [];
      mRequestTypeName = RequestType.Select.ToString();
      //mTableName = "TableName";
    }

    // Initializes an object instance with the supplied values.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/ConstructorParam/*'/>
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

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/ConstructorCopy/*'/>
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
    #endregion

    #region Data Methods

    // Creates and returns a clone of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
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

    // Get DataColumns from the DbRequest object definition.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/GetGridColumns1/*'/>
    public LJCDataColumns? DataColumns(List<string>? propertyNames = null)
    {
      LJCDataColumns? retColumns = null;

      if (Columns != null)
      {
        retColumns = Columns.Clone();
        if (propertyNames != null)
        {
          retColumns = Columns.LJCColumns(propertyNames);
          if (Joins != null)
          {
            foreach (var dbJoin in Joins)
            {
              retColumns = dbJoin.Columns.LJCColumns(propertyNames);
              if (LJC.HasListItems(retColumns))
              {
                foreach (var column in retColumns)
                {
                  var cloneColumn = column.Clone();
                  if (cloneColumn != null)
                  {
                    retColumns.Add(cloneColumn);
                  }
                }
              }
            }
          }
        }
      }
      return retColumns;
    }

    // Serializes the object and returns the serialized string.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/Serialize1/*'/>
    public string Serialize()
    {
      string retValue;

      retValue = LJC.XmlSerializeToString(GetType(), this, null);
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include file='Doc/DbRequest.xml'
    ///  path='items/Serialize2/*'/>
    public void Serialize(string fileSpec)
    {
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion
  }

  // The RequestType values.
  /// <include file='Doc/DbRequest.xml'
  ///  path='items/RequestType/*'/>
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
