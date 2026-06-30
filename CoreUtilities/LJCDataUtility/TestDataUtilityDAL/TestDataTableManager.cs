using LJCDataUtilityDAL;
using LJCNetCommon;

namespace TestDataUtilityDAL
{
  // Tests the DataTableManager object.
  internal class TestDataTableManager
  {
    // Initializes an object instance.
    public TestDataTableManager()
    {
      // Set program configuration Singleton.
      var values = ValuesDataUtility.Instance;
      values.SetConfigFile("LJCDataUtility.exe.config");
      var errors = values.Errors;
      if (NetString.HasValue(errors))
      {
        throw new System.Exception(errors);
      }

      DataTableManager = values.Managers.DataTableManager;
      TestCommon = new TestCommon("DataTableManager");
    }

    // Run the tests.
    public void Run()
    {
      // DataTable Data Methods
      Add();
      Delete();
      Load();
      Retrieve();
      Update();

      // DataTable Info Methods
      Columns();
      PropertyNames();

      // Additional Load and Retrieve Methods
      RetrieveWithID();
      RetrieveWithUnique();

      // Get Key Methods
      IDKey();
      ParentKey();
      UniqueKey();

      // Joins
      GetJoins();
    }

    #region Data Methods

    // Adds a record to the database.
    private void Add()
    {
    }

    // Deletes the records with the specified key values.
    private void Delete()
    {
    }

    // Retrieves a collection of data records.
    private void Load()
    {
    }

    // Retrieves a record from the database.
    private void Retrieve()
    {
      var methodName = "Retrieve()";

      var keyColumns = DataTableManager.IDKey(1);
      keyColumns.Add(DataUtilTable.ColumnDataSiteID, 1, "Int32");
      var dataUtilTable = DataTableManager.Retrieve(keyColumns);

      var result = dataUtilTable.Name;
      var compare = "DataModule";
      TestCommon.Write(methodName, result, compare);
    }

    // Updates the record.
    private void Update()
    {
    }
    #endregion

    #region Info Methods

    // Creates a collection of columns that match the supplied list.
    private void Columns()
    {
    }

    // Creates a list of BaseDefinition property names.
    private void PropertyNames()
    {
    }
    #endregion

    #region Additional Load and Retrieve Methods

    // Retrieves a record with the supplied ID values.
    private void RetrieveWithID()
    {
    }

    // Retrieves a record with the supplied unique values.
    private void RetrieveWithUnique()
    {
    }
    #endregion

    #region GetKey Methods

    // Gets the ID key columns.
    private void IDKey()
    {
    }

    // Gets the Parent ID key columns.
    private void ParentKey()
    {
    }

    // Gets the Unique ID key columns.
    private void UniqueKey()
    {
    }
    #endregion

    #region Joins

    // Creates and returns the Load Joins object.
    private void GetJoins()
    {
    }
    #endregion

    #region Properties

    // Gets or sets the DataTableManager reference.
    private DataTableManager DataTableManager { get; set; }
    #endregion

    private TestCommon TestCommon { get; set; }
  }
}
