echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildBackupWatcherLib.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateBackupWatcherLib.cmd nopause
msbuild BackupWatcherLib.sln -t:rebuild
pause