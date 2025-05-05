The backup process runs in two parts. This is done to allow the user to review
the proposed changes if desired before updating the target files.

1. Run the LJCCreateFileChanges.exe program.

  The LJCreateFileChanges.exe program determines which source files should be
  deleted or copied in the target folders.

  It writes a "Changes" file with the file specs of those files to be deleted or
  copied.
 
  The program takes four required parameters and one optional parameter.

  sourceRoot - The full path to the source files root folder. The files in this
  folder and all subfolders are available for backup.

  targetRoot - The full path to the target files root folder. The files in this
  folder and all subfolders are the target of the backup.

  Note: Only those folders that exist in the target will be updated. This is how
  the user controls what to backup. The target folders must be manually created
  to be included in the backup. The source folders and target folders must have
  matching names.

  While this implicit method prevents the user from having to specifically list
  the folders that are available for backup, it does allow the possibililty for
  missing folders that should be included. The MissingFolders.txt file is
  written to show those folders that are not included.

  changesFileSpec - The full file specification for the "Changes" file.

  includeFilter - This is a list of file names that will are available for
  backup.The file names are separated with a vertial bar and can include wild
  cards and partial paths.
  Example: "*.cs|Doc\*.xml" This includes all files that end with the extension
  "cs" and all files that are in a "Doc" folder and end with the extension
  "xml".

  skipFiles - This optional parameter is a list of files that will not be
  changed. The file names are separated with a vertical bar and can include wild
  cards.
  Example: "Changes.txt|?Build*.cmd" Files matching these names will not be
  deleted or updated.

  A source file is considered to have been deleted if the source file exists in
  the "Target" folder but does not exist in the "Source" folder. In this case a
  "Delete" record is written to the "Changes" file.

  If the "Source" file is different from the "Target" file, then a "Copy" record
  is written to the "Changes" file.

2. Run the LJCBackupChanges.exe program.

  The LJCBackupChanges.exe program copies files from a source root folder and
  subfolders to a target root folder and subfolders.

  The file changes are determined by first running the LJCCreateFileChanges.exe
  program which creates a "Changes" text file.

  The program takes three parameters.

  targetRoot - The full path to the target files root folder. The files in this
  folder and all subfolders are the target of the backup.

  changesFileSpec - The file which contains the file changes that were
  determined from running the LJCCreateFileChanges.exe program.

  startFolder - 



