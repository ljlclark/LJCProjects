echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildCreateFileChanges.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateCreateFileChanges.cmd nopause
msbuild LJCCreateFileChanges.sln -t:rebuild
pause