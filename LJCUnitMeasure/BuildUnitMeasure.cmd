echo Copyright (c) Lester J. Clark 2021 - All Rights Reserved
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateUnitMeasure.cmd nopause
msbuild LJCUnitMeasure.sln -t:rebuild
pause
