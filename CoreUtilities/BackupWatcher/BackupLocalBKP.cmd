echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocalBKP.cmd
echo:

call Common\DeleteGenFiles.cmd
call Common\SetVars.cmd

echo -----
set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDevBKP"
echo bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFilSpec% %multiFilter% %skipFiles%
bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFileSpec% %multiFilter% %skipFiles%
echo bin\LJCBackupChanges %targetPath% %changeFileSpec% %startFolder%
bin\LJCBackupChanges %targetPath% %changeFileSpec% %startFolder%
