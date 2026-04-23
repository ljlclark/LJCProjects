// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCDBRows.cs
using System.Xml.Serialization;
using LJCNetCommon5;

namespace LJCDBMessage5
{
  // Represents a collection of DbRow objects.
  /// <include path='items/DbRows/*' file='Doc/DbRows.xml'/>
  [XmlRoot("DbRows")]
  public class LJCDBRows : List<LJCDBRow>
  {
    #region Static Functions

    // Deserializes from the specified XML file.
    /// <include path='items/LJCDeserialize/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public static LJCDBRows? LJCDeserialize(string? fileSpec = null)
    {
      LJCDBRows? retValue;

      if (!LJC.HasValue(fileSpec))
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
    /// <include path='items/DefaultConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
    public LJCDBRows()
    {
    }

    // The Copy constructor.
    /// <include path='items/CopyConstructor/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
    public LJCDBRows(LJCDBRows items)
    {
      if (LJC.HasItems(items))
      {
        foreach (var item in items)
        {
          Add(new LJCDBRow(item));
        }
      }
    }
    #endregion

    #region Collection Methods

    // Adds the specified object.
    /// <include path='items/Add/*' file='Doc/DbRows.xml'/>
    public LJCDBRow? Add(LJCDataValues dataValues)
    {
      LJCDBRow? retValue = null;

      if (LJC.HasItems(dataValues))
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
    /// <include path='items/Clone/*' file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
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
    /// <include path='items/HasItems2/*' file='../../../CoreUtilities/LJCGenDoc/Common/Collection.xml'/>
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
    public void Serialize(string? fileSpec = null)
    {
      if (!LJC.HasValue(fileSpec))
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
