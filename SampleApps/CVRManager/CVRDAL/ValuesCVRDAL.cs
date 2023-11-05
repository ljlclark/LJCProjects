// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesCVRDAL.cs
using System;
using LJCDBClientLib;
using LJCNetCommon;

namespace CVRDAL
{
	/// <summary>The CVRDAL values.</summary>
	public class ValuesCVRDAL
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*'
		///   file='../../../CoreUtilities/LJCGenDoc/Common/Data.xml'/>
		public ValuesCVRDAL()
		{
		}
		#endregion

		#region Public Methods

		/// <summary>Sets the property values.</summary>
		/// <param name="programConfigFileSpec">The Program Config FileSpec.</param>
		public void SetProperties(string programConfigFileSpec)
		{
			StandardSettings = new StandardUISettings();
			StandardSettings.SetProperties(programConfigFileSpec);

			var settings = StandardSettings;
			Managers = new CVRManagers();
			Managers.SetDBProperties(settings.DbServiceRef, settings.DataConfigName);
		}
		#endregion

		#region Properties

		/// <summary>The singleton instance.</summary>
		public static ValuesCVRDAL Instance { get; }
			= new ValuesCVRDAL();

		/// <summary>Gets the CVRManagers object.</summary>
		public CVRManagers Managers { get; private set; }

		// Gets or sets the StandardSettings value.
		internal StandardUISettings StandardSettings { get; set; }
		#endregion
	}
}
