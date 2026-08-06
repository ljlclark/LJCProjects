// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DBWindowsService.cs
using System;
using System.ServiceProcess;
using System.ServiceModel;
using LJCDBServiceLib;

namespace LJCDBServiceHost
{
  // The DataService windows service.
  /// <include path='items/DBWindowsService/*' file='Doc/ProjectDbServiceHost.xml'/>
  public partial class DBWindowsService : ServiceBase
  {
    #region Constructors

    // Initializes an object instance.
    /// <include file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'
    ///  path='items/DefaultConstructor/*'/>
    public DBWindowsService()
    {
      InitializeComponent();
    }
    #endregion

    // Starts the windows Database service.
    /// <include file='Doc/DBWindowsService.xml'
    ///  path='items/OnStart/*'/>
    protected override void OnStart(string[] args)
    {
      if (mServiceHost != null)
      {
        mServiceHost.Close();
        mServiceHost = null;
      }

      mServiceHost = new ServiceHost(typeof(DbService));

      //Uri address = new Uri("http://localhost:8080/DBService");
      //BasicHttpBinding binding = new BasicHttpBinding();
      Uri address = new Uri("net.Tcp://localhost:8090/DBService");
      NetTcpBinding binding = new NetTcpBinding();

      Type contract = typeof(IDbService);
      mServiceHost.AddServiceEndpoint(contract, binding, address);

      mServiceHost.Open();
    }

    // Stops the windows Database service.
    /// <include file='Doc/DBWindowsService.xml'
    ///  path='items/OnStop/*'/>
    protected override void OnStop()
    {
      mServiceHost.Close();
    }

    private ServiceHost mServiceHost;
  }
}
