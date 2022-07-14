// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// DbRecords.cs
using System;
using System.Collections.Generic;
using LJCNetCommon;

namespace LJCDBMessage
{
  // Represents a collection of LJC.Net.Common.DbColumns record objects.
  /// <include path='items/DbRecords/*' file='Doc/DbRecords.xml'/>
  public class DbRecords : List<DbValues>
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbRecords()
    {
    }
    #endregion

    #region Methods

    // Creates and returns a clone of the object.
    /// <include path='items/Clone/*' file='../../LJCDocLib/Common/Data.xml'/>
    public DbRecords Clone()
    {
      DbRecords retValue = new DbRecords();
      foreach (DbValues items in this)
      {
        retValue.Add(items.Clone());
      }
      return retValue;
    }
    #endregion
  }
}
