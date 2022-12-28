// Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LJCWinFormCommon;
using LJCWinFormControls;
using LJCNetCommon;
using LJCDBClientLib;
using LJCDBServiceLib;
using LJCSQLUtilLib;
using LJCSQLUtilLibDAL;

namespace ForeignKeyManagerTest
{
	/// <summary>The Test form.</summary>
	public partial class Form1 : Form
	{
		#region Constructors

		// Initializes an object instance.
		/// <include path='items/DefaultConstructor/*' file='../../LJCDocLib/Common/Data.xml'/>
		public Form1()
		{
			InitializeComponent();
		}
		#endregion

		#region Form Event Handlers

		// Configures the form and loads the initial control data.
		private void Form1_Load(object sender, EventArgs e)
		{
			InitializeControls();

			mMetadata.LoadSchemaPrimaryKeys("Business");
			mMetadata.LoadSchemaForeignKeys("Business");
			TestUpdateMDFromSchema();
			TestRenameColumn();
			//TestTableCreate();
			Close();
		}
		#endregion

		#region Test Methods

		private void TestRenameColumn()
		{
			List<ColumnAction> columnActions = new List<ColumnAction>();
			ColumnAction columnAction = new ColumnAction()
			{
				ActionType = 2,
				FromColumnName = null,
				ToColumnName = "IsPrimaryKey",
				Sequence = 11,
				DataTypeName = "Boolean",
				//MaxLength = 30,
				AllowDBNull = true,
				DefaultValue = "0"
			};
			columnActions.Add(columnAction);

			string script = ModifyColumnScript("ViewColumn", columnActions);
			if (NetString.HasValue(script))
			{
				TextDisplay textDisplay = new TextDisplay();
				LJCRtControl rtControl = textDisplay.LJCRichTextBox;
				rtControl.LJCAppendLine(script);
				textDisplay.ShowDialog();
			}
		}

		// Creates the Modify Column script.
		private string ModifyColumnScript(string tableName
			, List<ColumnAction> tableColumnActions)
		{
			DbMetaDataTable mdTable;
			string retValue = null;

			// Uses the Metadata columns but the Schema foreign keys.
			DbMetaDataColumns fromMDColumns;
			DbMetaDataColumns toMDColumns;
			DbMetaDataColumn mdColumn;
			DbMetaDataColumn addColumn;

			// Get Table Metadata.
			mdTable = mMetadata.MdTableManager.RetrieveWithUniqueKey(tableName);
			if (null == mdTable)
			{
				mMetadata.UpdateMetadataFromSchema(mDataConfigName, tableName);
				mdTable = mMetadata.MdTableManager.RetrieveWithUniqueKey(tableName);
			}

			if (mdTable != null)
			{
				fromMDColumns = mMetadata.MdColumnManager.LoadByTableID(mdTable.ID);
				if (fromMDColumns != null)
				{
					toMDColumns = fromMDColumns.Clone();

					foreach (ColumnAction columnAction in tableColumnActions)
					{
						switch (columnAction.ActionType)
						{
							case 1:
								// Rename the column.
								mdColumn = toMDColumns.LJCSearchColumnName(columnAction.FromColumnName);
								if (null != mdColumn)
								{
									mdColumn.ColumnName = columnAction.ToColumnName;
								}
								break;

							case 2:
								addColumn = toMDColumns.Add(0, columnAction.ToColumnName);
								addColumn.Sequence = columnAction.Sequence;
								addColumn.DataTypeName = columnAction.DataTypeName;
								addColumn.MaxLength = columnAction.MaxLength;
								addColumn.AllowDBNull = columnAction.AllowDBNull;
								addColumn.DefaultValue = columnAction.DefaultValue.ToString();
								break;
						}
					}

					// Set the column create order.
					toMDColumns.LJCSortSequence(new MDSequenceComparer());
					retValue = mMetadata.GetRemakeTableScript(tableName, fromMDColumns, toMDColumns);
				}
			}
			return retValue;
		}

		// 
		private void TestUpdateMDFromSchema()
		{
			string dataConfigName = "FacilityManager";

			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewData");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewTable");
			mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewColumn");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewGridColumn");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewJoin");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewJoinOn");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewJoinColumn");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewFilter");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewConditionSet");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewCondition");
			//mMetadata.UpdateMetadataFromSchema(dataConfigName, "ViewOrderBy");
		}

		//// 
		//private void TestTableCreate()
		//{
		//	DbMetaDataTables mdTables = mMetadata.MdTableManager.Load();
		//	string createTableSql = mMetadata.GetCreateTableSql("TestTable");
		//	TextDisplay textDisplay = new TextDisplay();
		//	LJCRtControl rtControl = textDisplay.LJCRichTextBox;
		//	rtControl.LJCAppendLine(createTableSql);
		//	textDisplay.ShowDialog();
		//}
		#endregion

		#region Setup Methods

		// Configures the controls and loads the selection control data.
		/// <include path='items/InitializeControls/*' file='../../LJCDocLib/Common/List.xml'/>
		private void InitializeControls()
		{
			mDbServiceRef = new DbServiceRef()
			{
				DbService = new DbService()
			};
			//mDataConfigName = "AppManager";
			mDataConfigName = "FacilityManager";

			// Initialize Class Data.
			mMetadata = new LJCMetadata(mDbServiceRef, mDataConfigName);
		}
		#endregion

		#region Class Data

		private DbServiceRef mDbServiceRef;
		private string mDataConfigName;
		private LJCMetadata mMetadata;
		#endregion
	}

	internal class ColumnAction
	{
		public ColumnAction()
		{
		}

		public ColumnAction(string fromColumnName, string toColumnName
			, int actionType = 1)
		{
			ActionType = actionType;
			FromColumnName = fromColumnName;
			ToColumnName = toColumnName;
		}

		// 1 = Rename, 2 = Add
		public int ActionType { get; set; }

		public string FromColumnName { get; set; }

		public string ToColumnName { get; set; }

		public int Sequence { get; set; }

		public string DataTypeName { get; set; }

		public int MaxLength { get; set; }

		public bool AllowDBNull { get; set; }

		public object DefaultValue { get; set; }
	}
}
