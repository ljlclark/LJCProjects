echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildDBServiceHosts.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateDBServiceHosts.cmd nopause
msbuild LJCDBServiceHosts.sln -t:rebuild
pause
