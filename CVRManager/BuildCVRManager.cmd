echo Copyright (c) Lester J. Clark 2020 - All Rights Reserved

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateCVRManager.cmd nopause
msbuild CVRManager.sln -t:rebuild
call UpdateCVRManager.cmd nopause
pause