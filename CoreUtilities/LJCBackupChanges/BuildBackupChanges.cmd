echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildBackupChanges.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateBackupChages.cmd nopause
msbuild LJCBackupChanges.sln -t:rebuild
pause