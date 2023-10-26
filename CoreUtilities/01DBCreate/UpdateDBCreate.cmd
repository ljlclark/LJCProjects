echo off
echo.
echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem UpdateDBCreate.cmd

echo.
if %1%. == BuildAll. goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
set to=..\01DBCreate
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\01DBCreate
set to=%toRoot%
:Process

rem ************************
rem *** Runtime Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCDataAccess\LJCDataAccess\%bin%
echo copy %src%\LJCDataAccess.dll %to%
copy %src%\LJCDataAccess.dll %to%

set src=%assmRoot%LJCDataAccessConfig\LJCDataAccessConfig\%bin%
echo copy %src%\LJCDataAccessConfig.dll %to%
copy %src%\LJCDataAccessConfig.dll %to%

set src=%utilRoot%LJCExecuteScript\LJCExecuteScripts\%bin%
echo copy %src%\LJCExecuteScripts.Exe %to%
copy %src%\LJCExecuteScripts.Exe %to%

set src=%utilRoot%LJCExecuteScript\LJCExecuteScripts\%bin%
echo copy %src%\LJCExecuteScripts.exe.config %to%
copy %src%\LJCExecuteScripts.exe.config %to%

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

if %1%. == BuildAll. goto End
if %1%. == nopause. goto End
pause
:End
