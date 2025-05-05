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



