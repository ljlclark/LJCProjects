echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem  LJCWebPagesBackup.cmd
echo:

rem ** Set the code top-level path.
set mainPath="C:\Users\Les\Documents\Visual Studio 2022

rem ** Set the source (from) path.
set startFolder=LJCCodeDoc
set sourceRoot=%mainPath%\WebSitesDev\CodeDoc\%startFolder%"

rem ** Set the change file path and name.
set changesFilepath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles
set changesFilespec=%changesFilepath%\ChangesFilePages.txt"

rem ** Set the files to copy and explicit skips.
set includeFilter="*.html"
set skipFiles="index.html|CodeDoc.html"

echo -----
set targetFolder="LJCCodeDoc"
set targetRoot=%mainPath%\WebPages\%targetFolder%

rem ** Delete the previous changes file PagesChangeFile.txt.
if exist %changeFileSpec% del %changeFileSpec%

set binPath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\Bin

rem ** Create the new changes file PagesChangeFile.txt.
echo %binPath%\LJCCreateFileChanges" %sourceRoot% %targetRoot% %changesFilespec% %includeFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourceRoot% %targetRoot% %changesFilespec% %includeFilter% %skipFiles%

rem ** Delete the previous BackupLog.txt file.
if exist %changesFilepath%\BackupLog.txt" del %changesFilepath%\BackupLog.txt

rem ** Perform the backup using entries from the changes file PagesChangeFile.txt.
echo %binPath%\LJCBackupChanges" %targetRoot% %changesFilespec% %startFolder%
%binPath%\LJCBackupChanges" %targetRoot% %changesFilespec% %startFolder%

