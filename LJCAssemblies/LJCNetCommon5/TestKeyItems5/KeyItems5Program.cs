// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// KeyItems5Program.cs
using LJCNetCommon5;

namespace TestKeyItems5
{
  // The entry class.
  internal class KeyItems5Program
  {
    // The entry method.
    static void Main()
    {
      TestCommon = new LJCTestCommon("LJCKeyItems");
      Console.WriteLine();
      Console.WriteLine("*** LJCKeyItems ***");

      // Static Methods
      GetCollection();
      HasItems1();

      // Constructor Methods
      Constructor();
      CopyConstructor();

      // Collection Methods
      Add1();
      Add2();
      Append();
      Clone();
      HasItems2();

      // Other Methods
      GetDescription();
      GetIndex();
      GetItem();
      GetItems();

      // Search and Sort Methods
      SearchPropertyName();
      SortPropertyName();

      // IEnumerable Methods
      GetEnumerator();

      // IEnumerable Properties
      Count();
      Indexer();
    }

    #region Static Methods

    // Get custom collection from List<T>.
    private static void GetCollection()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetCollection()", result, compare);
    }

    // Checks if the collection has items.
    private static void HasItems1()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("HasItems()", result, compare);
    }
    #endregion

    #region Constructor Methods

    // Initializes an object instance.
    private static void Constructor()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Constructor()", result, compare);
    }

    // The Copy constructor.
    private static void CopyConstructor()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("CopyConstructor()", result, compare);
    }
    #endregion

    #region Collection Methods

    // Adds the specified object.
    private static void Add1()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add1()", result, compare);
    }

    // Creates and adds the object from the provided values.
    private static void Add2()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Add2()", result, compare);
    }

    // Appends the supplied objects to the collection.
    private static void Append()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Append()", result, compare);
    }

    // Creates and returns a clone of the object.
    private static void Clone()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Clone()", result, compare);
    }

    // Checks if the collection has items.
    private static void HasItems2()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("HasItems2()", result, compare);
    }
    #endregion

    #region Other Methods

    // Gets the Item Description with Value as index within PropertyName.
    private static void GetDescription()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetDescription()", result, compare);
    }

    // Get index from Value.
    private static void GetIndex()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetIndex()", result, compare);
    }

    // Gets the KeyItem with Value as index within PropertyName.
    private static void GetItem()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetItem()", result, compare);
    }

    // Gets the Items with the PropertyName.
    private static void GetItems()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetItems()", result, compare);
    }
    #endregion

    #region Search and Sort Methods

    // Retrieve the collection element.
    private static void SearchPropertyName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SearchPropertyName()", result, compare);
    }

    // Sort on Name.
    private static void SortPropertyName()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("SortPropertyName()", result, compare);
    }
    #endregion

    #region IEnumerable Methods

    // Gets the Collection Enumerator.
    private static void GetEnumerator()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("GetEnumerator()", result, compare);
    }
    #endregion

    #region IEnumerable Properties

    // The Collection count.
    private static void Count()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Count()", result, compare);
    }

    // Gets the item by index value.
    private static void Indexer()
    {
      var result = "Not Implemented";
      var compare = "";
      TestCommon?.Write("Indexer()", result, compare);
    }
    #endregion

    #region Properties

    // Gets or sets the TestCommon object.
    private static LJCTestCommon? TestCommon { get; set; }
    #endregion
  }
}
