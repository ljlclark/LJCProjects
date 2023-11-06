echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildDataDetail.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDataDetail.cmd nopause
msbuild LJCDataDetail.sln -t:rebuild
pause