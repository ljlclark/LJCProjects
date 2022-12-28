echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildGenText.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateGenText.cmd nopause
msbuild LJCGenText.sln -t:rebuild
pause
