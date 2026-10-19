echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildDataAccessConfig5.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDataAccessConfigS5.cmd nopause
msbuild LJCDataAccessConfigS5.sln -t:rebuild
pause