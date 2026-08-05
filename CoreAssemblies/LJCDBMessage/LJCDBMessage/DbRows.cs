// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DbRows.cs
using System.Collections.Generic;
using System.Xml.Serialization;
using LJCNetCommon;
using LJC = LJCNetCommon.NetCommon;

namespace LJCDBMessage
{
  // Represents a collection of DbRow objects.
  /// <include file='Doc/DbRows.xml'
  ///  path='items/DbRows/*'/>
  [XmlRoot("DbRows")]
  public class DbRows : List<DbRow>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/LJCDeserialize/*'/>
    public static DbRows LJCDeserialize(string fileSpec = null)
    {
      DbRows retValue;

      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = NetCommon.XmlDeserialize(typeof(DbRows), fileSpec)
        as DbRows;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DbRows()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public DbRows(DbRows items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new DbRow(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the specified object.
    /// <include file='Doc/DbRows.xml'
    ///  path='items/Add/*'/>
    public DbRow Add(LJCDataValues dataValues)
    {
      DbRow retValue = null;

      if (LJC.HasListItems(dataValues))
      {
        retValue = new DbRow()
        {
          Values = new LJCDataValues(dataValues)
        };
        Add(retValue);
      }
      return retValue;
    }

    // Clones the structure of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public DbRows Clone()
    {
      var retValue = new DbRows();
      foreach (DbRow dbRow in this)
      {
        retValue.Add(dbRow.Clone());
      }
      return retValue;
    }

    // Checks if the collection has items.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/HasItems2/*'/>
    public bool HasItems()
    {
      bool retValue = false;

      if (Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Serialize the object to the specified file.
    /// <include file='Doc/DbResult.xml'
    ///  path='items/Serialize2/*'/>
    public void Serialize(string fileSpec = null)
    {
      if (!NetString.HasValue(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      NetCommon.XmlSerialize(GetType(), this, null, fileSpec);
    }
    #endregion

    #region Properties

    /// <summary>Gets the Default File Name.</summary>
    public static string LJCDefaultFileName
    {
      get { return "DbRows.xml"; }
    }
    #endregion
  }
}
