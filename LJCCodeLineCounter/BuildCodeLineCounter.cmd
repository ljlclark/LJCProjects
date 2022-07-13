echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateCodeLineCounter.cmd nopause
msbuild LJCCodeLineCounter.sln -t:rebuild
pause