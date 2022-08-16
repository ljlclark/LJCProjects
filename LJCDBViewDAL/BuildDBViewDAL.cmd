call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDBViewDAL.cmd nopause
msbuild LJCDBViewDAL.sln -t:rebuild
pause
