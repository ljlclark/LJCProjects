echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem BuildDBClientLib.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDBClientLib.cmd nopause
msbuild LJCDBClientLib.sln -t:rebuild
pause
