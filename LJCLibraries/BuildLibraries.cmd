echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem BuildLibraries.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateLibraries.cmd nopause
msbuild LJCLibraries.sln -t:rebuild
pause