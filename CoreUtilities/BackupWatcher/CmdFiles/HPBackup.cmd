echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem HPBackup.cmd
echo:

call ..\Common\DeleteGenFiles.cmd
call ..\Common\LJCProjectsDevVars.cmd

echo -----
set targetPath="\OldComputer\Visual Studio 2022\LJCProjectsDevBackup"

if exist %changeFileSpec% del %changeFileSpec%
echo %binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%

if exist %changeFilePath%\BackupLog.txt" del %changeFilePath%\BackupLog.txt
echo %binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
%binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
