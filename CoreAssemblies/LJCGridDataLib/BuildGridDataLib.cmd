echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildGridDataLib.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateGridDataLib.cmd nopause
msbuild LJCGridDataLib.sln -t:rebuild
pause