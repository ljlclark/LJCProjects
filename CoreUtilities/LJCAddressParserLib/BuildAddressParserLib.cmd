call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
call UpdateAddressParserLib.cmd nopause
msbuild LJCAddressParserLib.sln -t:rebuild
pause