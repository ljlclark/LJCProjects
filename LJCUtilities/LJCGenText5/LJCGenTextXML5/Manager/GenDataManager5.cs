// Copyright (c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenDataManager5.cs
using LJCNetCommon5;

namespace LJCGenTextXML5
{
  // Provides GenData specific XML data manipulation methods.
  /// <include file='Doc/GenDataManager5.xml'
  ///  path='items/GenDataManager/*'/>
  public partial class GenDataManager
  {
    #region Properties

    // Gets or sets the XML data file specification.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/FileSpec/*'/>
    public string FileSpec
    {
      get => mFileSpec;
      set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue)
          && mFileSpec != newValue)
        {
          mFileSpec = newValue;
          FileName = Path.GetFileName(mFileSpec);
        }
      }
    }
    private string mFileSpec = null!;

    // Gets or sets the XML data file name.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/FileName/*'/>
    public string FileName
    {
      get => mFileName;
      private set
      {
        var newValue = value?.Trim();
        if (LJC.HasText(newValue))
        {
          mFileName = newValue;
        }
      }
    }
    private string mFileName = null!;

    // Gets or sets the Sections reference.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/Sections/*'/>
    public Sections Sections { get; set; } = null!;
    #endregion

    #region Constructors

    // Initializes an object instance.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/Constructor/*'/>
    public GenDataManager(string fileSpec)
    {
      Sections = [];
      FileSpec = fileSpec;
      if (!File.Exists(FileSpec))
      {
        string errorText = $"File '{FileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        var newSections = Sections.LJCDeserialize(FileSpec);
        if (newSections != null)
        {
          Sections = newSections;
        }
      }
    }
    #endregion

    #region Data Methods

    // Adds a Section record to the object data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/AddSection/*'/>
    public void AddSection(Section section)
    {
      if (section != null
        && LJC.HasText(section.Name))
      {
        var searchSection = RetrieveSection(section.Name);
        if (searchSection != null)
        {
          string errorText = $"Section '{section.Name}' already exists.";
          throw new InvalidOperationException(errorText);
        }
        else
        {
          Sections.Add(section);
        }
      }
    }

    // Retrieves a Section record from the object data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/RetrieveSection/*'/>
    public Section? RetrieveSection(string sectionName)
    {
      Section? retValue = null;

      if (LJC.HasText(sectionName))
      {
        retValue = Sections.Retrieve(sectionName);
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/LoadSections/*'/>
    public Sections LoadSections()
    {
      return Sections;
    }

    // Deletes the Section record from the object data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/DeleteSection/*'/>
    public bool DeleteSection(string sectionName)
    {
      bool retValue = false;

      Section searchSection = GetSection(sectionName);
      if (searchSection != null)
      {
        // Check for child items.
        RepeatItems repeatItems = searchSection.RepeatItems;
        if (LJC.HasListItems(repeatItems))
        {
          var errorText = "The Section cannot be deleted becauses it has child items.";
          throw new InvalidOperationException(errorText);
        }
        else
        {
          retValue = Sections.Remove(searchSection);
        }
      }
      return retValue;
    }

    // Get the Section object.
    private Section GetSection(string sectionName)
    {
      Section? retValue = RetrieveSection(sectionName);
      if (null == retValue)
      {
        string errorText = $"The Section '{sectionName}' does not exist.";
        throw new MissingMemberException(errorText);
      }
      return retValue;
    }
    #endregion

    #region Methods

    // Save the XML data.
    /// <include file='Doc/GenDataManager5.xml'
    ///  path='items/Save/*'/>
    public bool Save()
    {
      string errorText;
      bool retValue = true;

      //ErrorText = null;
      if (null == Sections)
      {
        errorText = "Sections property is null.";
        throw new MissingMemberException(errorText);
      }
      else
      {
        if (!LJC.HasText(FileSpec))
        {
          errorText = "Missing FileSpec property value.";
          throw new MissingMemberException(errorText);
        }
        else
        {
          Sections.LJCSerialize(FileSpec);
        }
      }
      return retValue;
    }
    #endregion
  }
}
