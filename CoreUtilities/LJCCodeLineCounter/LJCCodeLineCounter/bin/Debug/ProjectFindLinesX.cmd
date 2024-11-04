set Find="indent"

if %1. == . goto default
set folder=%1
goto work

:default
set folder="C:\Users\Les\Documents\Visual Studio 2022\LJCProjectsDev" 
rem set folder="e:\Visual Studio 2022\LJCProjectsDevBackup" 

:work
LJCCodeLineCounter %folder% "*.cs" %Find% > FindLines.txt
pause