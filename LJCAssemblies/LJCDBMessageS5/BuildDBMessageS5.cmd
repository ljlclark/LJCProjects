echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildDBMessageS5.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDBMessageS5.cmd nopause
msbuild LJCDBMessageS5.sln -t:rebuild
pause