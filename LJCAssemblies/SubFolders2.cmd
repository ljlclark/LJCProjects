rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem SubFolders2.cmd

set bin=bin\Debug

rem *** Setup Solution Group Folders ***
set assm=LJCAssemblies
set util=LJCUtilities

set assmRoot=%assm%\
set utilRoot=%util%\

if %1%. == BuildAll. goto Continue
set assmRoot=..\..\%assm%\
set utilRoot=..\..\%util%\
:Continue
