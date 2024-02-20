set Find="SetupUpdate"

if %1. == . goto default
set folder=%1
goto work

:default
set folder="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev" 
rem set folder="e:\Visual Studio 2022\LJCProjectsDevBackup" 

:work
LJCCodeLineCounter %folder% "*.cmd" %Find% > FindLines.txt
pause