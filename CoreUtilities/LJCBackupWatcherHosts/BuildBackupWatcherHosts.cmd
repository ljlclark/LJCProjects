echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildBackupWatcherHosts.cmd
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"

call UpdateBackupWatcherHosts.cmd nopause
msbuild BackupWatcherHosts.sln -t:rebuild
pause