// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// GenDataManager.cs
using LJCGenTextLib;
using LJCNetCommon;
using System;
using System.IO;

namespace LJCGenTextEdit
{
  // Provides GenData specific XML data manipulation methods.
  /// <include path='items/GenDataManager/*' file='Doc/GenDataManager.xml'/>
  public partial class GenDataManager
  {
    #region Constructors

    // Initializes an object instance.
    /// <include path='items/GenDataManagerC/*' file='Doc/GenDataManager.xml'/>
    public GenDataManager(string fileSpec)
    {
      Sections = null;
      FileSpec = fileSpec;
      if (!File.Exists(FileSpec))
      {
        string errorText = $"File '{FileSpec}' was not found.";
        throw new FileNotFoundException(errorText);
      }
      else
      {
        Sections = Sections.LJCDeserialize(FileSpec);
      }
    }
    #endregion

    #region Data Methods

    // Adds a Section record to the object data.
    /// <include path='items/AddSection/*' file='Doc/GenDataManager.xml'/>
    public void AddSection(Section section)
    {
      if (section != null
        && NetString.HasValue(section.Name))
      {
        Section searchSection = RetrieveSection(section.Name);
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
    /// <include path='items/RetrieveSection/*' file='Doc/GenDataManager.xml'/>
    public Section RetrieveSection(string sectionName)
    {
      Section retValue = null;

      if (NetString.HasValue(sectionName))
      {
        retValue = Sections.Retrieve(sectionName);
      }
      return retValue;
    }

    // Retrieves a collection of data records.
    /// <include path='items/LoadSections/*' file='Doc/GenDataManager.xml'/>
    public Sections LoadSections()
    {
      return Sections;
    }

    // Deletes the Section record from the object data.
    /// <include path='items/DeleteSection/*' file='Doc/GenDataManager.xml'/>
    public bool DeleteSection(string sectionName)
    {
      bool retValue = false;

      Section searchSection = GetSection(sectionName);
      if (searchSection != null)
      {
        // Check for child items.
        RepeatItems repeatItems = searchSection.RepeatItems;
        if (NetCommon.HasItems(repeatItems))
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
      Section retValue = RetrieveSection(sectionName);
      if (null == retValue)
      {
        string errorText = $"The Section '{sectionName}' does not exist.";
        throw new MissingMemberException(errorText);
      }
      return retValue;
    }
    #endregion

    #region Public Methods

    // Save the XML data.
    /// <include path='items/Save/*' file='Doc/GenDataManager.xml'/>
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
        if (!NetString.HasValue(FileSpec))
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

    #region Properties

    /// <summary>Gets or sets the XML data file specification.</summary>
    public string FileSpec
    {
      get { return mFileSpec; }
      set
      {
        mFileSpec = NetString.InitString(value);
        FileName = Path.GetFileName(mFileSpec);
      }
    }
    private string mFileSpec;

    /// <summary>Gets or sets the XML data file name.</summary>
    public string FileName
    {
      get { return mFileName; }
      private set { mFileName = NetString.InitString(value); }
    }
    private string mFileName;

    /// <summary>Gets or sets the Sections reference.</summary>
    public Sections Sections { get; set; }
    #endregion

    #region Class Data

    #endregion
  }
}
