echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildViewEditor.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateViewEditor.cmd nopause
msbuild LJCViewEditor.sln -t:rebuild
pause
