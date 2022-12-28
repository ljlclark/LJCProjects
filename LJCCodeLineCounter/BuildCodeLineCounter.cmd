echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildCodeLineCounter.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateCodeLineCounter.cmd nopause
msbuild LJCCodeLineCounter.sln -t:rebuild
pause