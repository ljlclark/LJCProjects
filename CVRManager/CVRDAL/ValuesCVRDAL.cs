// Copyright (c) Lester J. Clark 2020 - All Rights Reserved
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
		///   file='../../LJCDocLib/Common/Data.xml'/>
		public ValuesCVRDAL()
		{
		}
		#endregion

		#region Public Methods

		/// <summary>Sets the property values.</summary>
		/// <param name="programConfigFileSpec">The Program Config FileSpec.</param>
		public void SetProperties(string programConfigFileSpec)
		{
			StandardSettings = new StandardSettings();
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
		internal StandardSettings StandardSettings { get; set; }
		#endregion
	}
}
