rem BuildDBDataAccess.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateDBDataAccess.cmd nopause
msbuild LJCDBDataAccess.sln -t:rebuild
pause
