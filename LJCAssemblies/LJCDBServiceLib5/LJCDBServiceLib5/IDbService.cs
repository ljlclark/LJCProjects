// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// IDBService.cs
using System;
using System.ServiceModel;

namespace LJCDBServiceLib5
{
  // The Service Contract for performing database operations using request
  // XML messages. 
  /// <include file='Doc/IDbService.xml'
  ///  path='items/IDbService/*'/>
  [ServiceContract(Namespace = "ljspricket@gmail.com")]
  public interface IDbService
  {
    // Executes the specified request XML message.
    /// <include file='Doc/IDbService.xml'
    ///  path='items/Execute/*'/>
    [OperationContract]
    string Execute(string request);
  }
}
