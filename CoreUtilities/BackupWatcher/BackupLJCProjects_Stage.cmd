echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BackupLocal.cmd
echo:

call Common\DeleteGenFiles.cmd
call Common\SetVars.cmd

echo -----
set targetPath="C:\Users\Les\Documents\Visual Studio 2022\LJCProjects_Stage"
echo bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
bin\LJCCreateFileChanges %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
echo bin\LJCBackupChanges %targetPath% %changeFile% %startFolder%
bin\LJCBackupChanges %targetPath% %changeFile% %startFolder%
