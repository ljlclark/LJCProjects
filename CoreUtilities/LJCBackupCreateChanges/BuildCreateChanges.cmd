echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildCreateChanges.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateCreateChanges.cmd nopause
msbuild LJCCreateChanges.sln -t:rebuild
pause