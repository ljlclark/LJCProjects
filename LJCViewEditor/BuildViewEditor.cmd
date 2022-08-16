call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateViewEditor.cmd nopause
msbuild LJCViewEditor.sln -t:rebuild
pause
