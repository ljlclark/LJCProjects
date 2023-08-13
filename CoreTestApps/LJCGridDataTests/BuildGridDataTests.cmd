call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateGridDataTests.cmd nopause
msbuild LJCGridDataTests.sln -t:rebuild
pause
