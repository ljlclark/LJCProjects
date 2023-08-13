call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateNetCommonTest.cmd nopause
msbuild LJCNetCommonTest.sln -t:rebuild
pause
