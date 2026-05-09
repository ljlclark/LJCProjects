echo off
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ***BuildBackupCommonLib.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateCommonLib.cmd nopause
msbuild LJCBackupCommonLib.sln -t:rebuild
pause