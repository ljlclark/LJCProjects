echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateAddressParserLib.cmd

if exist SubFolders.cmd goto BuildAll
set mainRoot=..\..\
call %mainRoot%SubFolders.cmd %1%
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd %1%
set toRoot=%util%\LJCAddressParserLib\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%

rem *****************************
rem *** Runtime-only Binaries ***

rem -------------------------------------
set to=%toRoot%LJCAddressParserLib\%bin%
rem echo.
rem echo *** %to% ***

if %mainRoot%. == . goto End
if %1%. == nopause. goto End
pause
:End
