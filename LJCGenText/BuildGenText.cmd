echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
rem BuildGenText.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateGenText.cmd nopause
msbuild LJCGenText.sln -t:rebuild
pause
