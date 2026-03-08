C# Codelines

A C# Codeline consists of a root folder and subfolders. The code in these
folders use only the code in their codeline and do not refer to code
outside of their codeline except for third-party software.

Usually codelines are grouped together under a root folder. The root folder
could be something like "C:\Users\User\Documents\Visual Studio 2022". In
this case the main IDE used is Visual Studio which creates the code line
"Projects" in this root folder. So the custom C# codelines can be created
here also.

LJCProjects Codeline

This is the main codeline and is under source control online in GitHub at
"https://github.com/ljlclark/LJCProjects". Local code in this codeline can
be commited to GitHub.

Sample Visual Studio path:
"C:\Users\User\Documents\Visual Studio 2022\LJCProjects"

LJCProjectsDev Codeline

This is the development codeline. Development work is done here and then
promoted (copied) to the main codeline LJCProjects.

Sample Visual Studio path:
"C:\Users\User\Documents\Visual Studio 2022\LJCProjectsDev"

Solutions

A Solution contains at least one project. Solution projects work
together for a common purpose. Solutions and their projects are
organized under a Solution folder.

Solution folders usually contain several common helper command files.
They start with the verbs Build, Clear and Update followed by the short
project name. For example: The solution folder LJCGenDoc contains:
BuildGenDoc.cmd, ClearGenDoc.cmd and UpdateGenDoc.cmd.

 Build*.cmd  builds the solution without starting Visual Studio. This
             includes calling Update*.cms.
 Clear*.cmd  Deletes the compiled and generated files that are not saved
             in source control.
 Update*.cmd Copies the referenced and runtime dependencies into the
             folder "External".

Solution Group Folders

Solutions are grouped under the codeline folder in folders indicating
their purpose.

  Core Solutions - Folder: CoreAssemblies
  
  These solutions produce assemblies that form the core of the LJC
  Framework.

    Common Libraries

    *LJCNetCommon - The .NET Common Library

    *LJCLibraries
    
      * LJCWinFormCommon - The WinForm Common Library

      * LJCWinFormControls - The WinForm Controls Library

    *LJCTextDataReader - The Text DataReader Library

    Data Libraries

    *LJCDataAccess - The ADO.NET Data Access Library

    *LJCDataAccessConfig - The Data Access Configuration Library

    Message Based Data Access

    *LJCDBMessage - The Data Service Message Library

    *LJCDBClientLib - The Data Service Client Library

    *LJCDBDataAccess - The Message Data Access Library

    *LJCDBServiceLib - The Data Service Library

    *LJCDBServiceHosts

      *LJCDBServiceHost - The Data Service Library Windows Host

      *LJCDBServiceConsoleHost - The Data Service Library Console Host

    Other Data Access

    *LJCDBClientSQLLib - The Local Message Based SQL Data Access Library

  Core Utilities - Folder: CoreUtilities

  These solutions produce core supporting utilities for the LJC
  Framework.

    *LJCExecuteScript - The Execute SQL Script Console Program

    *LJCGenDoc - Generates HTML code documentation from the Visual Studio
                 XML Comment output files.
  
    *LJCGenDoc2 - Generates HTML documentation from the source file XML
                  comments.

    *LJCGenText - Generates text files by combining GenData XML file data
                  and a text template file.