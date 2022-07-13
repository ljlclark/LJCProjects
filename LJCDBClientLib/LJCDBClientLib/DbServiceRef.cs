// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// DbServiceRef.cs
using LJCDBDataAccessLib;
using LJCDBServiceLib;

namespace LJCDBClientLib
{
  // Contains the DbDataAccess, DbService and DbServiceClient proxy references.
  /// <include path='items/DbServiceRef/*' file='Doc/DbServiceRef.xml'/>
  public class DbServiceRef
  {
    // Gets or sets the DbDataAccess reference.
    /// <include path='items/DbDataAccess/*' file='Doc/DbServiceRef.xml'/>
    public DbDataAccess DbDataAccess { get; set; }

    // Gets or sets the DbService reference.
    /// <include path='items/DbService/*' file='Doc/DbServiceRef.xml'/>
    public DbService DbService { get; set; }

    // Gets or sets the DbServiceClient proxy reference.
    /// <include path='items/DbServiceClient/*' file='Doc/DbServiceRef.xml'/>
    public DbServiceClient DbServiceClient { get; set; }
  }
}
