call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDocGroupEditor.cmd nopause
msbuild LJCDocGroupEditor.sln -t:rebuild
pause