// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// TestKeyItems5.cs
using LJCNetCommon5;

namespace TestNetCommon5
{
  // Provides LJCKeyItems test methods.
  internal class TestKeyItems
  {
    #region Properties

    // Gets or sets the TestCommon object.
    private LJCTestCommon TestCommon { get; set; } = null!;
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    public TestKeyItems()
    {
      TestCommon = new LJCTestCommon("TestKeyItems");
      Run();
    }

    // Runs the test methods.
    private void Run()
    {
      // Static Test Methods
      GetCollection();
      HasItems1();

      // Collection Test Methods
      Add1();
      Add2();
      Append();
      Clone();
      HasItems2();

      // Other Test Methods
      GetDescription();
      GetIndex();
      GetItem();
      GetItems();

      // Search and Sort Test Methods
      SearchPropertyName();
      SortPropertyName();

      // IEnumerable Test Methods
      GetEnumerator();

      // IEnumerable Property Test Methods
      Count();
      Indexer();
    }
    #endregion

    #region Static Test Methods

    // Get custom collection from List<T>.
    private void GetCollection()
    {
      // Create KeyItems list.
      var items = new List<LJCKeyItem>();
      items!.Add(new LJCKeyItem("ID", 1, "Primary Key"));
      var item = new LJCKeyItem("Name", 2, "Unique Key")
      {
        MaxLength = 60
      };
      items.Add(item);

      // Test Method
      // Convert to KeyItems collection.
      var keyItems = LJCKeyItems.GetCollection(items);

      var dataColumn = new LJCDataColumn("ID", "1", "Int64");
      var result = keyItems?.GetDescription(dataColumn);
      var compare = "Primary Key";
      TestCommon.Write("GetCollection()", result, compare);
    }

    // Checks if the collection has items.
    private void HasItems1()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };

      // Test Method
      var value = LJCKeyItems.HasItems(keyItems);

      var result = value.ToString();
      var compare = "True";
      TestCommon.Write("HasItems1()", result, compare);
    }
    #endregion

    #region Collection Test Methods

    // Adds the specified object.
    private void Add1()
    {
      var keyItems = new LJCKeyItems();
      var keyItem = new LJCKeyItem()
      {
        PropertyName = "ID",
        ID = 1,
        Description = "Primary Key",
      };

      // Test Method
      keyItems.Add(keyItem);

      var findItem = keyItems.LJCRetrieve("ID");
      var result = findItem?.Description;
      var compare = "Primary Key";
      TestCommon.Write("Add1()", result, compare);
    }

    // Creates and adds the object from the provided values.
    private void Add2()
    {
      var keyItems = new LJCKeyItems();

      // Test Method
      keyItems!.Add("ID", 1, "Primary Key");

      var findItem = keyItems.LJCRetrieve("ID");
      var result = findItem?.Description;
      var compare = "Primary Key";
      TestCommon.Write("Add2()", result, compare);
    }

    // Appends the supplied objects to the collection.
    private void Append()
    {
      var addKeyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };
      var keyItems = new LJCKeyItems();

      // Test Method
      keyItems.Append(addKeyItems);

      var findItem = keyItems.LJCRetrieve("Name");
      var result = findItem?.Description;
      var compare = "Unique Key";
      TestCommon.Write("Append()", result, compare);
    }

    // Creates and returns a clone of the object.
    private void Clone()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };

      // Test Method
      var cloneKeyItems = keyItems.Clone();

      var findItem = cloneKeyItems.LJCRetrieve("Name");
      var result = findItem?.Description;
      var compare = "Unique Key";
      TestCommon.Write("Clone()", result, compare);
    }

    // Checks if the collection has items.
    private void HasItems2()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };

      // Test Method
      var value = keyItems.HasItems();

      var result = value.ToString();
      var compare = "True";
      TestCommon.Write("HasItems2()", result, compare);
    }
    #endregion

    #region Other Test Methods

    // Gets the Item Description with Value as index within PropertyName.
    private void GetDescription()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };
      var dataColumn = new LJCDataColumn("ID", "1");

      // Test Method
      var result = keyItems.GetDescription(dataColumn);

      var compare = "Primary Key";
      TestCommon.Write("GetDescription()", result, compare);
    }

    // Get index from Value.
    private void GetIndex()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };
      var dataColumn = new LJCDataColumn("Name", "2");

      // Test Method
      var value = keyItems.GetIndex(dataColumn);

      var result = value.ToString();
      var compare = "1";
      TestCommon.Write("GetIndex()", result, compare);
    }

    // Gets the KeyItem with Value as index within PropertyName.
    private void GetItem()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };
      var dataColumn = new LJCDataColumn("Name", "2");

      // Test Method
      var keyItem = keyItems.GetItem(dataColumn);

      var result = keyItem?.Description;
      var compare = "Unique Key";
      TestCommon.Write("GetItem()", result, compare);
    }

    // Gets the Items with the PropertyName.
    private void GetItems()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };
      var dataColumn = new LJCDataColumn("Name", "2");

      // Test Method
      var findItems = keyItems.GetItems(dataColumn);

      var value = findItems?[0];
      var result = value?.Description;
      var compare = "Unique Key";
      TestCommon.Write("GetItems()", result, compare);
    }
    #endregion

    #region Search and Sort Test Methods

    // Retrieve the collection element.
    private void SearchPropertyName()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 1, "Primary Key" },
        { "Name", 2, "Unique Key" },
      };

      // Test Method
      var findItems = keyItems.SearchPropertyName("Name");

      var keyItem = findItems?[0];
      var result = keyItem?.Description;
      var compare = "Unique Key";
      TestCommon.Write("SearchPropertyName()", result, compare);
    }

    // Sort on Name.
    private void SortPropertyName()
    {
      var keyItems = new LJCKeyItems()
      {
        { "Name", 1, "Unique Key" },
        { "ID", 2, "Primary Key" },
      };

      // Test Method
      keyItems.SortPropertyName();

      var keyItem = keyItems[1];
      var result = keyItem?.Description;
      var compare = "Unique Key";
      TestCommon.Write("SortPropertyName()", result, compare);
    }
    #endregion

    #region IEnumerable Test Methods

    // Gets the Collection Enumerator.
    private void GetEnumerator()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 2, "Primary Key" },
        { "Name", 1, "Unique Key" },
      };

      // Test
      string result = null;
      foreach (var keyItem in keyItems)
      {
        result = keyItem.Description;
      }

      var compare = "Unique Key";
      TestCommon.Write("GetEnumerator()", result, compare);
    }
    #endregion

    #region IEnumerable Property Test Methods

    // The Collection count.
    private void Count()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 2, "Primary Key" },
        { "Name", 1, "Unique Key" },
      };

      // Test Method
      var value = keyItems.Count;

      var result = value.ToString();
      var compare = "2";
      TestCommon.Write("Count()", result, compare);
    }

    // Gets the item by index value.
    private void Indexer()
    {
      var keyItems = new LJCKeyItems()
      {
        { "ID", 2, "Primary Key" },
        { "Name", 1, "Unique Key" },
      };

      // Test Method
      var keyItem = keyItems[1];

      var result = keyItem?.Description;
      var compare = "Unique Key";
      TestCommon.Write("Indexer()", result, compare);
    }
    #endregion
  }
}
