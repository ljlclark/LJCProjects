echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildRegionManager.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateRegionManager.cmd nopause
msbuild LJCRegionManager.sln -t:rebuild
pause
