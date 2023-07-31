echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem LJCWebPagesChanges.cmd
echo:

set mainPath="C:\Users\Les\Documents\Visual Studio 2022
set sourcePath=%mainPath%\WebSitesDev\CodeDoc\LJCCodeDoc"
set changeFilePath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\CmdFiles
set changeFileSpec=%changeFilePath%\PagesChangeFile.txt"
set multiFilter="*.html"
rem set skipFiles="ChangeFile.txt|BuildAll.cmd|ClearBuild.cmd|UpdateAll.cmd"

echo -----
set targetPath=%mainPath%\WebPages\LJCCodeDoc"
set binPath=%mainPath%\LJCProjectsDev\CoreUtilities\BackupWatcher\Bin
echo %binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
