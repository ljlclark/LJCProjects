// Copyright(c) Lester J.Clark and Contributors.
// Licensed under the MIT License.
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
