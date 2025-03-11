// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// LJCAssembly.cs
using System;
using System.IO;
using System.Reflection;
using LJCNetCommon;

namespace LJCDataTransformProcess
{
	// Provides Assembly reflection.
	/// <include path='items/LJCAssembly/*' file='Doc/LJCAssembly.xml'/>
	public class LJCAssembly
	{
		#region Constructors
		#endregion

		#region Public Methods

		// Invokes the method.
		/// <include path='items/MethodInvoke/*' file='Doc/LJCAssembly.xml'/>
		public bool MethodInvoke(object[] parameters)
		{
			bool retValue = true;

			retValue = (bool)MethodInfo.Invoke(ObjectInstance, parameters);
			return retValue;
		}

		// Creates the Assembly property value.
		/// <include path='items/SetAssembly/*' file='Doc/LJCAssembly.xml'/>
		public bool SetAssembly(string nameSpace, string assemblyFileName)
		{
			string errorText = null;
			bool retValue = true;

			mAssemblyNameSpace = nameSpace;
			AssemblyFileName = assemblyFileName;
			Assembly = null;
			ObjectType = null;
			ObjectInstance = null;
			MethodInfo = null;

			if (!File.Exists(mAssemblyFileName))
			{
				retValue = false;
				errorText = $"The assembly file '{mAssemblyFileName}' is not"
					+ " found.\r\n";
				throw new FileNotFoundException(errorText);
			}
			else
			{
				Assembly = Assembly.LoadFrom(mAssemblyFileName);
				if (null == Assembly)
				{
					errorText = "Unable to create Assembly reference for"
						+ $" '{mAssemblyFileName}'.";
					throw new InvalidOperationException(errorText);
				}
			}
			return retValue;
		}

		// Creates the MethodInfo property value.
		/// <include path='items/SetMethodInfo/*' file='Doc/LJCAssembly.xml'/>
		public bool SetMethodInfo(string methodName)
		{
			bool retValue = true;

			MethodInfo = null;

			MethodInfo = ObjectType.GetMethod(methodName);
			if (null == MethodInfo)
			{
				retValue = false;
				string objectName = mAssemblyNameSpace + "." + methodName;
				string errorText = "Unable to get Method information for"
					+ $" '{objectName}'.\r\n";
				throw new InvalidOperationException(errorText);
			}
			return retValue;
		}

		// Creates the ObjectInstance property value.
		/// <include path='items/SetObjectInstance/*' file='Doc/LJCAssembly.xml'/>
		public bool SetObjectInstance()
		{
			string errorText = null;
			bool retValue = true;

			ObjectInstance = null;
			MethodInfo = null;

			ObjectInstance = Activator.CreateInstance(ObjectType);
			if (null == ObjectInstance)
			{
				retValue = false;
				errorText = $"Unable to create object Instance for '{ObjectName}'.\r\n";

			}
			return retValue;
		}

		// Creates the ObjectType property value.
		/// <include path='items/SetObjectType/*' file='Doc/LJCAssembly.xml'/>
		public bool SetObjectType(string objectName)
		{
			string errorText = null;
			bool retValue = true;

			ObjectType = null;
			ObjectInstance = null;
			MethodInfo = null;
			ObjectName = objectName;

			if (!NetString.HasValue(ObjectName))
			{
				retValue = false;
				errorText = $"LJCAssembly.SetObjectType - '{ObjectName}' is null.";
			}
			else
			{
				string typeName = mAssemblyNameSpace + "." + ObjectName;
				ObjectType = Assembly.GetType(typeName);
				if (null == ObjectType)
				{
					retValue = false;
					errorText = $"The object '{ObjectName}' was not found.\r\n";
					throw new InvalidOperationException(errorText);
				}
			}
			return retValue;
		}
		#endregion

		#region Properties

		/// <summary>The Assembly Reflection object.</summary>
		public Assembly Assembly { get; set; }

		/// <summary>The Assembly File name.</summary>
		public string AssemblyFileName
		{
			get { return mAssemblyFileName; }
			set { mAssemblyFileName = NetString.InitString(value); }
		}
		private string mAssemblyFileName;

		/// <summary>The Assembly Namespace.</summary>
		public string AssemblyNameSpace
		{
			get { return mAssemblyNameSpace; }
			set { mAssemblyNameSpace = NetString.InitString(value); }
		}
		private string mAssemblyNameSpace;

		/// <summary>The Reflected Method info object.</summary>
		public MethodInfo MethodInfo { get; set; }

		/// <summary>The Reflected item Object Instance.</summary>
		public object ObjectInstance { get; set; }

		/// <summary>The Reflectedion item Object name.</summary>
		public string ObjectName
		{
			get { return mObjectName; }
			set { mObjectName = NetString.InitString(value); }
		}
		private string mObjectName;

		/// <summary>The Reflected item Object Type.</summary>
		public Type ObjectType { get; set; }
		#endregion
	}
}
