rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem SubFolders2.cmd

set bin=bin\Debug

rem *** Group Folder Names ***
set assm=LJCAssemblies
set util=LJCUtilities

rem *** Group Paths ***
set assmRoot=
set utilRoot=
if %1%. == BuildAll. goto Continue

rem *** Codeline Group Paths ***
set assmRoot=..\..\%assm%\
set utilRoot=..\..\%util%\
:Continue
