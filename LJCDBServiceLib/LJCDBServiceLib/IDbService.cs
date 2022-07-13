// Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
// IDBService.cs
using System.ServiceModel;

namespace LJCDBServiceLib
{
  // The Service Contract for performing database operations using request
  // XML messages. 
  /// <include path='items/IDbService/*' file='Doc/IDbService.xml'/>
  [ServiceContract(Namespace = "ljspricket@gmail.com")]
  public interface IDbService
  {
    // Executes the specified request XML message.
    /// <include path='items/Execute/*' file='Doc/IDbService.xml'/>
    [OperationContract]
    string Execute(string request);
  }
}
