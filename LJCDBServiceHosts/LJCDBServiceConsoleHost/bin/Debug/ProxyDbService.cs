﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


/// <summary></summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace = "ljspricket@gmail.com", ConfigurationName = "IDbService")]
public interface IDbService
{
	/// <summary></summary>
	[System.ServiceModel.OperationContractAttribute(Action = "ljspricket@gmail.com/IDbService/Execute", ReplyAction = "ljspricket@gmail.com/IDbService/ExecuteResponse")]
	string Execute(string query);

	/// <summary></summary>
	[System.ServiceModel.OperationContractAttribute(Action = "ljspricket@gmail.com/IDbService/Execute", ReplyAction = "ljspricket@gmail.com/IDbService/ExecuteResponse")]
	System.Threading.Tasks.Task<string> ExecuteAsync(string query);
}

/// <summary></summary>
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IDbServiceChannel : IDbService, System.ServiceModel.IClientChannel
{
}

/// <summary></summary>
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class DbServiceClient : System.ServiceModel.ClientBase<IDbService>, IDbService
{
	/// <summary></summary>
	public DbServiceClient()
	{
	}

	/// <summary></summary>
	public DbServiceClient(string endpointConfigurationName) :
					base(endpointConfigurationName)
	{
	}

	/// <summary></summary>
	public DbServiceClient(string endpointConfigurationName, string remoteAddress) :
					base(endpointConfigurationName, remoteAddress)
	{
	}

	/// <summary></summary>
	public DbServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
					base(endpointConfigurationName, remoteAddress)
	{
	}

	/// <summary></summary>
	public DbServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
					base(binding, remoteAddress)
	{
	}

	/// <summary></summary>
	public string Execute(string query)
	{
		return base.Channel.Execute(query);
	}

	/// <summary></summary>
	public System.Threading.Tasks.Task<string> ExecuteAsync(string query)
	{
		return base.Channel.ExecuteAsync(query);
	}
}
