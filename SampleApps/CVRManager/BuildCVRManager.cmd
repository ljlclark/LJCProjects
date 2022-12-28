echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildCVManager.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateCVRManager.cmd nopause
msbuild CVRManager.sln -t:rebuild
call UpdateCVRManager.cmd nopause
pause