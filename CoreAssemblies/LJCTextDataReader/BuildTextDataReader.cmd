echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildTextDataReader.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateTextDataReader.cmd nopause
msbuild LJCTextDataReader.sln -t:rebuild
pause