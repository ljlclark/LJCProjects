echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildDataUtility5.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDataUtility5.cmd nopause
msbuild LJCDataUtility5.sln -t:rebuild
pause