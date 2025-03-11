// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataViewer.cs
using System;
using System.Diagnostics;
using System.IO;
using LJCNetCommon;
using LJCWinFormCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	/// <summary>The SourceData Viewer.</summary>
	public class DataViewer
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DataViewerC/*' file='Doc/DataViewer.xml'/>
		public DataViewer(DbServiceRef dbServiceRef, string dataConfigName)
		{
			mDbServiceRef = dbServiceRef;
			mDataConfigName = dataConfigName;

			InitializeControls();
		}
		#endregion

		#region Public Methods

		// Displays the Source data.
		/// <include path='items/Show/*' file='Doc/DataViewer.xml'/>
		public void Show(int dataSourceID)
		{
			string errorText = null;

			DataSource dataSource = mDataSourceManager.RetrieveWithID(dataSourceID);
			string sourceItemName = dataSource.SourceItemName;
			switch (dataSource.SourceTypeID)
			{
				case SourceTypeFile:
					if (!File.Exists(sourceItemName))
					{
						errorText = $"File '{sourceItemName}' was not found.";
						throw new FileNotFoundException(errorText);
					}
					else
					{
						Process process = new Process()
						{
							StartInfo = new ProcessStartInfo()
							{
								FileName = sourceItemName
							}
						};
						process.Start();
					}
					break;

				case SourceTypeTable:
					TableViewer tableViewer = new TableViewer(mDbServiceRef
						, mDataConfigName, sourceItemName);
					tableViewer.ShowDialog();
					break;
			}
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../../CoreUtilities/LJCGenDoc/Common/List.xml'/>
		private void InitializeControls()
		{
			// Initialize Class Data.
			mDataSourceManager = new DataSourceManager(mDbServiceRef, mDataConfigName);
		}
		#endregion

		#region Class Data

		private const short SourceTypeFile = 1;
		private const short SourceTypeTable = 2;

		private readonly string mDataConfigName;
		private readonly DbServiceRef mDbServiceRef;

		private DataSourceManager mDataSourceManager;
		#endregion
	}
}
