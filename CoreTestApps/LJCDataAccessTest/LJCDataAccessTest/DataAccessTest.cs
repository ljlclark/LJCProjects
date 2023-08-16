using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using LJCDataAccess;
using MySql.Data.MySqlClient;

namespace LJCDataAccessTest
{
	/// <summary>Contains the individual test methods.</summary>
	public class DataAccessTest
	{
		#region Constructors

		/// <summary>
		/// Initializes an object instance.
		/// </summary>
		/// <param name="dataAccess">The DataAccess object.</param>
		public DataAccessTest(DataAccess dataAccess)
		{
			DataAccess = dataAccess;
			ProviderName = dataAccess.ProviderName;
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Executes an Insert, Update or Delete statement.
		/// </summary>
		/// <param name="sql">The SQL statement.</param>
		public void ExecuteNonQuery(string sql)
		{
			int affectedRecords = DataAccess.ExecuteNonQuery(sql);
		}

		/// <summary>
		/// Executes a DB script file.
		/// </summary>
		public void ExecuteScript()
		{
			// Create a DB Script file.
			StringBuilder builder = new StringBuilder(64);
			builder.AppendLine("insert into Person ");
			builder.AppendLine(" (Name, PrincipleFlag) ");
			builder.AppendLine("values('ScriptPerson1', 0);");
			builder.AppendLine("go");
			builder.AppendLine("delete from Person ");
			builder.AppendLine("where Name = 'ScriptPerson1';");
			builder.AppendLine("go");
			string scriptText = builder.ToString();
			File.WriteAllText("TestScript.sql", scriptText);

			DataAccess.ExecuteScript("TestScript.sql");
		}

		/// <summary>
		/// Executes a DB Script text string.
		/// </summary>
		public void ExecuteScriptText()
		{
			// Create a DB Script string.
			StringBuilder builder = new StringBuilder(64);
			builder.AppendLine("insert into Person ");
			builder.AppendLine(" (Name, PrincipleFlag) ");
			builder.AppendLine("values('ScriptPerson1', 0);");
			builder.AppendLine("go");
			builder.AppendLine("delete from Person ");
			builder.AppendLine("where Name = 'ScriptPerson1';");
			builder.AppendLine("go");
			string scriptText = builder.ToString();

			DataAccess.ExecuteScriptText(scriptText);
		}

		/// <summary>
		/// Executes a Select statement and fills the specified DataTable.
		/// </summary>
		public void FillDataTable()
		{
			string sql = "select * from Person;";
			DataTable dataTable = new DataTable();
			DataAccess.FillDataTable(sql, dataTable);
			InspectTable(dataTable);
		}

		/// <summary>
		/// Executes a Select statement and retrieves the DbDataReader object.
		/// </summary>
		public void GetDataReader()
		{
			DbDataReader dbDataReader = null;

			string sql = "select * from Person";
			try
			{
				// The "using" statement disposes the object when the scope is exited.
				using (dbDataReader = DataAccess.GetDataReader(sql))
				{
					DataTable schemaTable = dbDataReader.GetSchemaTable();
					while (dbDataReader.Read())
					{
						char[] buffer = new char[5];
						dbDataReader.GetChars(1, 0, buffer, 0, 5);

						// Use the record here.
						for (int index = 0; index < dbDataReader.FieldCount; index++)
						{
							string value = dbDataReader[index].ToString();
						}
					}
				}
			}
			finally
			{
				// The calling program must close the connection when done.
				DataAccess.CloseConnection();
			}
		}

		/// <summary>
		/// Executes a Select statement and retrieves the DataSet object.
		/// </summary>
		public void GetDataSet()
		{
			DataTable dataTable;
			TableMapping tableMapping;
			string sql;

			tableMapping = new TableMapping();
			sql = "select * from Person; select * from Person";

			// Create table map collection.
			tableMapping.AddTableMap("FirstTable");
			tableMapping.AddColumnMap("FirstTable", "Name", "FullName");
			tableMapping.AddTableMap("SecondTable");
			tableMapping.AddColumnMap("SecondTable", "Name", "Person2Name");

			var dataSet = DataAccess.GetDataSet(sql, tableMapping.TableMaps);
			if (dataSet != null)
			{
				dataTable = dataSet.Tables["FirstTable"];
				InspectTable(dataTable);

				dataTable = dataSet.Tables["SecondTable"];
				InspectTable(dataTable);
			}
		}

		/// <summary>
		/// Executes a Select statement and retrieves the DataTable object.
		/// </summary>
		/// <returns>The DataTable object.</returns>
		public DataTable GetDataTable()
		{
			string sql = "select * from Person";
			DataTable retValue = DataAccess.GetDataTable(sql);
			InspectTable(retValue);
			return retValue;
		}

		/// <summary>
		/// Executes a Select statement and retrieves the DataTable object.
		/// </summary>
		public void GetProcedureDataTable()
		{
			ProcedureParameters parameters;
			string procedureName;

			if (IsMySql())
			{
				parameters = new ProcedureParameters()
				{
					{ "id", MySqlDbType.Int64, 0, 8 }
				};
				procedureName = "sp_PersonRetrieve";
			}
			else
			{
				parameters = new ProcedureParameters()
				{
					{ "@ID", SqlDbType.BigInt, 0, 20 }
				};
				procedureName = "dbo.sp_PersonRetrieve";
			}

			DataTable dataTable = DataAccess.GetProcedureDataTable(procedureName
				, parameters);
			InspectTable(dataTable);
		}

		/// <summary>
		/// Retrieves the DataTable object with schema only.
		/// </summary>
		public void GetSchemaOnly()
		{
			string sql = "select * from Person";

			DataTable dataTable = DataAccess.GetSchemaOnly(sql);
			if (dataTable != null)
			{
				foreach (DataColumn dataColumn in dataTable.Columns)
				{
					string value = dataColumn.ColumnName;
				}
			}
		}
		#endregion

		#region Private Methods

		// Iterates through the table data.
		private void InspectTable(DataTable dataTable)
		{
			if (dataTable != null)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					for (int index = 0; index < dataTable.Columns.Count; index++)
					{
						string value = dataRow[index].ToString();
					}
				}
			}
		}

		// Checks for the MySql Data Provider.
		private bool IsMySql()
		{
			bool retValue = false;

			if ("MySql.Data.MySqlClient" == ProviderName)
			{
				retValue = true;
			}
			return retValue;
		}
		#endregion

		#region Properties

		// Gets or sets the DataAccess value.
		private DataAccess DataAccess { get; set; }

		// Gets or sets the ProviderName value.
		private string ProviderName { get; set; }
		#endregion
	}
}
