echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildSQLUtilLib.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateSQLUtilLib.cmd nopause
msbuild LJCSQLUtilLib.sln -t:rebuild
pause
