echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem BuildAll.cmd

call 2BuildAssemblies.cmd
call 3BuildUtilities.cmd
pause
call 4BuildTestApps.cmd
call 5BuildSampleApps.cmd
