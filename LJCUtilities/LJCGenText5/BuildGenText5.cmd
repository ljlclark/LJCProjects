echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildGenText5.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateGenText5.cmd nopause
msbuild LJCGenText5.sln -t:rebuild
pause