echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildUnitMeasure.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateUnitMeasure.cmd nopause
msbuild LJCUnitMeasure.sln -t:rebuild
pause
