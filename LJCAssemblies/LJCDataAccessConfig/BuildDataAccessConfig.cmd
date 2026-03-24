echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildDataAccessConfig.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDataAccessConfig.cmd nopause
msbuild LJCDataAccessConfig.sln -t:rebuild
pause