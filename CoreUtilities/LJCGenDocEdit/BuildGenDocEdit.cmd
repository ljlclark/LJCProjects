echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildGenDocEdit.cmd

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateGenDocEdit.cmd nopause
msbuild LJCGenDocEdit.sln -t:rebuild
pause
