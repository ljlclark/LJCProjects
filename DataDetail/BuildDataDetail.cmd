echo Copyright (c) Lester J. Clark 2020 - All Rights Reserved

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDataDetail.cmd nopause
msbuild DataDetail.sln -t:rebuild
pause