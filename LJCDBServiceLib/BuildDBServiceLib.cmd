echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildDBServiceLib.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDBServiceLib.cmd nopause
msbuild LJCDBServiceLib.sln -t:rebuild
pause
