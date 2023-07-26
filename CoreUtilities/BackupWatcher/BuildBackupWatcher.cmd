echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildBackupWatcher.cmd

set path="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev\CoreUtilities
set project=LJCBackupWatcherLib"
call %path%\%project%\BuildBackupWatcherLib.cmd
set project=LJCCreateFileChanges"
call %path%\%project%\BuildCreateFileChanges.cmd
call UpdateBackupWatcher.cmd nopause
pause