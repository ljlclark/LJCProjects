call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDataAccessTest.cmd nopause
msbuild LJCDataAccessTest.sln -t:rebuild
pause
