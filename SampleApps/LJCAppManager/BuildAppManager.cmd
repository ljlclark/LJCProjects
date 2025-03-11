call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateAppManager.cmd nopause
msbuild LJCAppManager.sln -t:rebuild
pause