echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem SeagateBackup.cmd
echo:

call ..\Common\DeleteGenFiles.cmd
call ..\Common\LJCProjectsDevVars.cmd

echo -----
set targetPath="\My Data\Visual Studio 2022\LJCProjectsDevBackup"

if exist %changeFileSpec% del %changeFileSpec%
echo %binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%

if exist %changeFilePath%\BackupLog.txt" del %changeFilePath%\BackupLog.txt
echo %binPath%\LJCBackupChanges" %targetPath% %changeFile% %startFolder%
%binPath%\LJCBackupChanges" %targetPath% %changeFileSpec% %startFolder%
