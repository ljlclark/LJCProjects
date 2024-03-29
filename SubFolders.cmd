rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem SubFolders.cmd

set bin=bin\Debug
set assm=CoreAssemblies
set test=CoreTestApps
set util=CoreUtilities
set apps=SampleApps

set assmRoot=%assm%\
set testRoot=%test%\
set utilRoot=%util%\
set appsRoot=%apps%\

if %1%. == BuildAll. goto Continue
set assmRoot=..\..\%assm%\
set testRoot=..\..\%test%\
set utilRoot=..\..\%util%\
set appsRoot=..\..\%apps%\
:Continue
