echo off
echo:
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem PassportChanges.cmd
echo:

call ..\Common\DeleteGenFiles.cmd
call ..\Common\LJCProjectsDevVars.cmd

echo -----
set targetPath="\Visual Studio 2022\LJCProjectsDevBackup"
echo %binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
%binPath%\LJCCreateFileChanges" %sourcePath% %targetPath% %changeFile% %multiFilter% %skipFiles%
pause