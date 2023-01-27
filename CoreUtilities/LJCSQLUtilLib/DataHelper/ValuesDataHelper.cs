// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// ValuesDataHelper.cs
using LJCDBClientLib;

namespace DataHelper
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class ValuesDataHelper
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public ValuesDataHelper()
		{
			StandardSettings = new StandardUISettings();
			StandardSettings.SetProperties("DataHelper.exe.config");
		}
		#endregion

		#region Properties

		// The singleton instance.
		internal static ValuesDataHelper Instance { get; } = new ValuesDataHelper();

		// Gets or sets the StandardSettings value.
		internal StandardUISettings StandardSettings { get; set; }
		#endregion
	}
}
