// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TransformDetailCode.cs
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDataTransformDAL;

namespace LJCTransformManager
{
	/// <summary>The TaskTransform detail dialog.</summary>
	public partial class TransformDetail : Form
	{
		#region Data Methods

		// Retrieves the initial control data.
		/// <include path='items/DataRetrieve/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void DataRetrieve()
		{
			Cursor = Cursors.WaitCursor;
			if (LJCID > 0)
			{
				LJCIsUpdate = true;
				var record = mTaskTransformManager.RetrieveWithID(LJCID);
				GetRecordValues(record);
			}
			else
			{
				LJCIsUpdate = false;
				LJCRecord = new TaskTransform();
				ParentNameTextbox.Text = LJCParentName;
			}
			Cursor = Cursors.Default;
		}

		// Gets the record values and copies them to the controls.
		/// <include path='items/GetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void GetRecordValues(TaskTransform dataRecord)
		{
			if (dataRecord != null)
			{
				LJCParentID = dataRecord.StepTaskID;
				ParentNameTextbox.Text = LJCParentName;
				TransformNameTextbox.Text = dataRecord.Name;
				TransformDescriptionTextbox.Text = dataRecord.Description;
				SourceDataCombo.LJCSetByItemID(dataRecord.SourceDataID);
				TargetDataCombo.LJCSetByItemID(dataRecord.TargetDataID);
			}
		}

		// Creates and returns a record object with the data from
		/// <include path='items/SetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private TaskTransform SetRecordValues()
		{
			TaskTransform retValue = new TaskTransform()
			{
				TransformID = LJCID,
				StepTaskID = LJCParentID,
				Name = TransformNameTextbox.Text,
				Description = FormCommon.SetString(TransformDescriptionTextbox.Text),
				SourceDataID = SourceDataCombo.LJCSelectedItemID(),
				TargetDataID = TargetDataCombo.LJCSelectedItemID()
			};
			return retValue;
		}

		// Resets the empty record values.
		/// <include path='items/ResetRecordValues/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private void ResetRecordValues(TaskTransform record)
		{
			record.Description = FormCommon.SetString(record.Description);
		}

		// Saves the data.
		/// <include path='items/DataSave/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool DataSave()
		{
			TaskTransform lookupRecord;
			string title;
			string message;
			bool retValue = true;

			Cursor = Cursors.WaitCursor;
			LJCRecord = SetRecordValues();

			var keyColumns = new DbColumns()
			{
				{ TaskTransform.ColumnName, (object)LJCRecord.Name }
			};
			lookupRecord = mTaskTransformManager.Retrieve(keyColumns);
			if (mTaskTransformManager.IsDuplicate(lookupRecord, LJCRecord, LJCIsUpdate))
			{
				retValue = false;
				title = "Data Entry Error";
				message = "The TaskTransform record already exists.";
				Cursor = Cursors.Default;
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}

			if (retValue)
			{
				if (LJCIsUpdate)
				{
					keyColumns = new DbColumns()
					{
						{ TaskTransform.ColumnTransformID, LJCRecord.TransformID }
					};
					mTaskTransformManager.Update(LJCRecord, keyColumns);
					ResetRecordValues(LJCRecord);
				}
				else
				{
					TaskTransform addedRecord = mTaskTransformManager.Add(LJCRecord);
					ResetRecordValues(LJCRecord);
					if (addedRecord != null)
					{
						LJCRecord.TransformID = addedRecord.TransformID;
					}
				}
			}
			Cursor = Cursors.Default;
			return retValue;
		}

		// Validates the data.
		/// <include path='items/IsValid/*' file='../../../CoreUtilities/LJCGenDoc/Common/Detail.xml'/>
		private bool IsValid()
		{
			StringBuilder builder;
			string title;
			string message;
			bool retValue = true;

			builder = new StringBuilder(64);
			builder.AppendLine("Invalid or Missing Data:");

			if (!NetString.HasValue(TransformNameTextbox.Text))
			{
				retValue = false;
				builder.AppendLine($"  {TransformNameLabel.Text}");
			}

			if (!NetString.HasValue(SourceDataCombo.Text))
			{
				retValue = false;
				builder.AppendLine($"  {SourceDataLabel.Text}");
			}

			if (!NetString.HasValue(TargetDataCombo.Text))
			{
				retValue = false;
				builder.AppendLine($"  {TargetDataLabel.Text}");
			}

			if (retValue == false)
			{
				title = "Data Entry Error";
				message = builder.ToString();
				MessageBox.Show(message, title, MessageBoxButtons.OK
					, MessageBoxIcon.Exclamation);
			}
			return retValue;
		}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		private void InitializeControls()
		{
			// Get singleton values.
			var values = ValuesTransformManager.Instance;

			mSettings = values.StandardSettings;
			BeginColor = mSettings.BeginColor;
			EndColor = mSettings.EndColor;

			// Initialize Class Data.
			//mDbServiceRef = mSettings.DbServiceRef;
			//mDataConfigName = mSettings.DataConfigName;
			mManagers = new TransformManagers(mSettings.DbServiceRef
				, mSettings.DataConfigName);
			//mStepManager = mManagers.StepManager;
			//mTaskManager = mManagers.StepTaskManager;
			mTaskTransformManager = mManagers.TaskTransformManager;
			mDataSourceManager = mManagers.DataSourceManager;
			mSourceLayoutManager = mManagers.SourceLayoutManager;
			mDataViewer = new DataViewer(mSettings.DbServiceRef
				, mSettings.DataConfigName);

			ParentNameLabel.BackColor = BeginColor;
			TransformNameLabel.BackColor = BeginColor;
			TransformDescriptionLabel.BackColor = BeginColor;
			SourceDataLabel.BackColor = BeginColor;
			TargetDataLabel.BackColor = BeginColor;

			TransformNameTextbox.MaxLength = StepTask.LengthName;
			TransformDescriptionTextbox.MaxLength = StepTask.LengthDescription;

			// Load control data.
			LoadSourceCombo(SourceDataCombo);
			LoadSourceCombo(TargetDataCombo);

			// Set control layout.
			if (0 == LJCParentID)
			{
				ParentNameLabel.Visible = false;
				ParentNameTextbox.Visible = false;
				Height -= ParentNameTextbox.Height;
			}
		}

		// Load the DataSource Combo.
		private void LoadSourceCombo(LJCItemCombo itemCombo)
		{
			DataSources dataSources = mDataSourceManager.Load();
			if (NetCommon.HasItems(dataSources))
			{
				foreach (DataSource dataSource in dataSources)
				{
					itemCombo.LJCAddItem(dataSource.DataSourceID, dataSource.Description);
				}
			}
		}
		#endregion

		#region Properties

		/// <summary>Gets or sets the primary ID value.</summary>
		public int LJCID { get; set; }

		/// <summary>Gets or sets the Parent ID value.</summary>
		public int LJCParentID { get; set; }

		/// <summary>Gets or sets the LJCParentName value.</summary>
		public string LJCParentName
		{
			get { return mParentName; }
			set { mParentName = NetString.InitString(value); }
		}
		private string mParentName;

		/// <summary>Gets a reference to the record object.</summary>
		public TaskTransform LJCRecord { get; private set; }

		/// <summary>Gets the LJCIsUpdate value.</summary>
		public bool LJCIsUpdate { get; private set; }

		// Gets or sets the Begin Color.
		private Color BeginColor { get; set; }

		// Gets or sets the End Color.
		private Color EndColor { get; set; }
		#endregion

		#region Class Data

		private StandardUISettings mSettings;
		private TransformManagers mManagers;
		//private StepManager mStepManager;
		//private StepTaskManager mTaskManager;
		private TaskTransformManager mTaskTransformManager;
		private DataSourceManager mDataSourceManager;
		private SourceLayoutManager mSourceLayoutManager;
		private DataViewer mDataViewer;

		/// <summary>The Change event.</summary>
		public event EventHandler<EventArgs> LJCChange;
		#endregion
	}
}
