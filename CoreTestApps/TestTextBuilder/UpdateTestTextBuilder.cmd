echo off
rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem UpdateTestTextBuilder.cmd

if exist SubFolders.cmd goto BuildAll
rem // Project is Current Folder
set mainRoot=..\..\
rem // Set %bin%, %assmRoot%, %testRoot%, %utilRoot% and %appsRoot%
call %mainRoot%SubFolders.cmd
rem // Set %to% as External
call %mainRoot%TargetFolders.cmd
goto Process:
:BuildAll
call SubFolders.cmd BuildAll
set toRoot=%test%\TestTextBuilder\
call TargetFolders.cmd
:Process

rem ***************************
rem *** Referenced Binaries ***
echo *** %to% ***

set src=%assmRoot%LJCNetCommon\LJCNetCommon\%bin%
echo copy %src%\LJCNetCommon.dll %to%
copy %src%\LJCNetCommon.dll %to%
