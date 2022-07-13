echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem BuildDBMessage.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDBMessage.cmd nopause
msbuild LJCDBMessage.sln -t:rebuild
pause
