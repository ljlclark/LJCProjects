echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildExecuteScript.cmd

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateExecuteScript.cmd nopause
msbuild LJCExecuteScript.sln -t:rebuild
pause
