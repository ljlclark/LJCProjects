echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved
set bin=bin\Debug
if %1%. == ClearAll. goto ClearAll
set Solution=..\LJCCodeLineCounter
set ClearBuild=..\ClearBuildDetail.cmd
goto Clear

:ClearAll
set Solution=LJCCodeLineCounter
set ClearBuild=ClearBuildDetail.cmd

:Clear
set Project=LJCCodeLineCounter
set File=LJCCodeLineCounter
call %ClearBuild%
del %Solution%\%Project%\%bin%\CountLines.txt
del %Solution%\%Project%\%bin%\FindLines.txt
del %Solution%\%Project%\%bin%\HelpLines.txt
del %Solution%\%Project%\%bin%\LargeFiles.txt
