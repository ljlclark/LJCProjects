// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBRows.cs
using System.Xml.Serialization;
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of DbRow objects.
  /// <include file='Doc/DbRows.xml'
  ///  path='items/DbRows/*'/>
  [XmlRoot("DbRows")]
  public class LJCDBRows : List<LJCDBRow>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/LJCDeserialize/*'/>
    public static LJCDBRows? LJCDeserialize(string? fileSpec = null)
    {
      LJCDBRows? retValue;

      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      retValue = LJC.XmlDeserialize(typeof(LJCDBRows), fileSpec)
        as LJCDBRows;
      return retValue;
    }
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public LJCDBRows()
    {
    }

    // The Copy constructor.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'
    ///  path='items/CopyConstructor/*'/>
    public LJCDBRows(LJCDBRows items)
    {
      if (LJC.HasListItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDBRow(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the supplied object.
    /// <include file='Doc/DbRows.xml'
    ///  path='items/Add/*'/>
    public LJCDBRow? Add(LJCDataValues dataValues)
    {
      LJCDBRow? retValue = null;

      if (LJC.HasListItems(dataValues))
      {
        retValue = new LJCDBRow()
        {
          Values = [.. dataValues]
        };
        Add(retValue);
      }
      return retValue;
    }

    // Clones the structure of the object.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/Clone/*'/>
    public LJCDBRows Clone()
    {
      var retValue = new LJCDBRows();
      foreach (LJCDBRow dbRow in this)
      {
        if (dbRow != null)
        {
          var clone = dbRow.Clone();
          if (clone != null)
          {
            retValue.Add(clone);
          }
        }
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
    public void Serialize(string? fileSpec = null)
    {
      if (!LJC.HasText(fileSpec))
      {
        fileSpec = LJCDefaultFileName;
      }
      LJC.XmlSerialize(GetType(), this, null, fileSpec);
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
