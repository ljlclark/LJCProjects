// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// #CommentChars //
// #PlaceholderBegin _
// #PlaceholderEnd _
// #SectionBegin Title
// #Value _AppName_
// Values_AppName_.cs
// #SectionEnd Title
using LJCDBClientLib;
using LJCNetCommon;
using System.IO;

// #SectionBegin Class
// #Value _FullAppName_
// #Value _AppName_
// #Value _Namespace_
namespace _Namespace_
{
	/// <summary>The Application values singleton class.</summary>
	public sealed class Values_AppName_
	{
		#region Constructors

		// Initializes an instance of the object.
		/// <include path='items/DefaultConstructor/*' file='../../LJCGenDoc/Common/Data.xml'/>
		public Values_AppName_()
		{
			ArgError = new ArgError("_FullAppName_DAL.Values_AppName_");
			Errors = "";
			StandardSettings = new StandardUISettings();
		}

		/// <summary>Configures the settings.</summary>
		/// <param name="fileSpec">The config file name.</param>
		public void SetConfigFile(string fileSpec = "_FullAppName_.exe.config")
		{
			if (!File.Exists(fileSpec))
			{
				ArgError.MethodName = "SetConfigFile(fileSpec)";
				var message = ArgError.ToString();
				message += $"File {fileSpec} was not found.\r\n";
				Errors += message;
			}
			else
			{
				// Update for changed file name.
				fileSpec = fileSpec.Trim();
				if (!NetString.IsEqual(fileSpec, FileSpec))
				{
					FileSpec = fileSpec;
					StandardSettings.SetProperties(fileSpec);

					var settings = StandardSettings;
					Managers = new Managers_AppName_();
					Managers.SetDBProperties(settings.DbServiceRef
						, settings.DataConfigName);
				}
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets the Error message</summary>
		public string Errors { get; private set; }

		/// <summary>Gets the config FileSpec.</summary>
		public string FileSpec { get; private set; }

		/// <summary>Gets or sets the generated page count.</summary>
		public int GenPageCount { get; set; }

		/// <summary>Gets the singleton instance.</summary>
		public static Values_AppName_ Instance
		{
			get { return mInstance; }
		}

		/// <summary>Gets or sets the Managers class reference.</summary>
		public Managers_AppName_ Managers { get; set; }

		/// <summary>Gets the StandardSettings value.</summary>
		public StandardUISettings StandardSettings { get; private set; }

		// Represents Argument errors.
		private ArgError ArgError { get; set; }
		#endregion

		#region Class Data

		/// <summary>Initialize Singleton.</summary>
		private static readonly Values_AppName_ mInstance
			= new Values_AppName();
		#endregion
	}
}
