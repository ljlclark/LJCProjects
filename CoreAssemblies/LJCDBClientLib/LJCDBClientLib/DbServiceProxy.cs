//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LJCDBClientLib
{
	/// <summary>The Proxy DbService contract.</summary>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	[System.ServiceModel.ServiceContractAttribute(Namespace = "ljspricket@gmail.com", ConfigurationName = "IDbService")]
	public interface IDbService
	{
		/// <summary>The Proxy Execute interface.</summary>
		[System.ServiceModel.OperationContractAttribute(Action = "ljspricket@gmail.com/IDbService/Execute", ReplyAction = "ljspricket@gmail.com/IDbService/ExecuteResponse")]
		string Execute(string query);

		/// <summary>The Proxy ExecuteAsync interface.</summary>
		[System.ServiceModel.OperationContractAttribute(Action = "ljspricket@gmail.com/IDbService/Execute", ReplyAction = "ljspricket@gmail.com/IDbService/ExecuteResponse")]
		System.Threading.Tasks.Task<string> ExecuteAsync(string query);
	}

	/// <summary>The Proxy ServiceChannel contract.</summary>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public interface IDbServiceChannel : IDbService, System.ServiceModel.IClientChannel
	{
	}

	/// <summary>The Proxy client object.</summary>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
	public partial class DbServiceClient : System.ServiceModel.ClientBase<IDbService>, IDbService
	{
		/// <summary>The Proxy default constructor.</summary>
		public DbServiceClient()
		{
		}

		// The Proxy constructor with values.
		/// <include path='items/DbServiceClientC1/*' file='Doc/DbServiceProxy.xml'/>
		public DbServiceClient(string endpointConfigurationName) :
						base(endpointConfigurationName)
		{
		}

		// The Proxy constructor with values.
		/// <include path='items/DbServiceClientC2/*' file='Doc/DbServiceProxy.xml'/>
		public DbServiceClient(string endpointConfigurationName, string remoteAddress) :
						base(endpointConfigurationName, remoteAddress)
		{
		}

		// The Proxy constructor with values.
		/// <include path='items/DbServiceClientC3/*' file='Doc/DbServiceProxy.xml'/>
		public DbServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
						base(endpointConfigurationName, remoteAddress)
		{
		}

		// The Proxy constructor with values.
		/// <include path='items/DbServiceClientC4/*' file='Doc/DbServiceProxy.xml'/>
		public DbServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
						base(binding, remoteAddress)
		{
		}

		/// <summary>The Proxy Execute method.</summary>
		/// <param name="query">The query message.</param>
		public string Execute(string query)
		{
			return base.Channel.Execute(query);
		}

		/// <summary>The Proxy ExecuteAsync method.</summary>
		/// <param name="query">The query message.</param>
		public System.Threading.Tasks.Task<string> ExecuteAsync(string query)
		{
			return base.Channel.ExecuteAsync(query);
		}
	}
}
