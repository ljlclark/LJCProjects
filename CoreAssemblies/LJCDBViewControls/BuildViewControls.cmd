echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildViewControls.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateViewControls.cmd nopause
msbuild LJCViewControls.sln -t:rebuild
pause