// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
// DbRows.cs
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using LJCNetCommon;

namespace LJCDBMessage
{
  // Represents a collection of DbRow objects.
  /// <include path='items/DbRows/*' file='Doc/DbRows.xml'/>
  [XmlRoot("DbRows")]
  public class DbRows : List<DbRow>
  {
    #region Static Functions

    // Checks if the collection has items.
    /// <include path='items/HasItems1/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static bool HasItems(DbRows collectionObject)
    {
      bool retValue = false;

      if (collectionObject != null && collectionObject.Count > 0)
      {
        retValue = true;
      }
      return retValue;
    }

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public static DbRows LJCDeserialize(string fileSpec = null)
    {
      DbRows retValue;

      if (false == NetString.HasValue(fileSpec))
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
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbRows()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../LJCDocLib/Common/Collection.xml'/>
    public DbRows(DbRows items)
    {
      if (HasItems(items))
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
    /// <include path='items/Add/*' file='Doc/DbRows.xml'/>
    public DbRow Add(DbValues dbValues)
    {
      DbRow retValue = null;

      if (DbValues.HasItems(dbValues))
      {
        retValue = new DbRow()
        {
          Values = new DbValues(dbValues)
        };
        Add(retValue);
      }
      return retValue;
    }

    // Clones the structure of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbResult Clone()
    {
      DbResult retValue = MemberwiseClone() as DbResult;
      return retValue;
    }

    // Checks if the collection has items.
    /// <include path='items/HasItems2/*' file='../../LJCDocLib/Common/Collection.xml'/>
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
    /// <include path='items/Serialize2/*' file='Doc/DbResult.xml'/>
    public void Serialize(string fileSpec = null)
    {
      if (false == NetString.HasValue(fileSpec))
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
