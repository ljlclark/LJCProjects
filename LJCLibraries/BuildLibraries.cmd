echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildLibraries.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateLibraries.cmd nopause
msbuild LJCLibraries.sln -t:rebuild
pause