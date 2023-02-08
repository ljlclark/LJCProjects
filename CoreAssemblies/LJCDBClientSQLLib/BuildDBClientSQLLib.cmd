echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildSQLManagerLib.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDBClientSQLLib.cmd nopause
msbuild LJCDBClientSQLLib.sln -t:rebuild
pause
