echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearGenTextData.cmd

if %1%. == ClearAll. goto ClearAll
set RootFolder=..\LJCGenText
goto Clear
:ClearAll
set RootFolder=CoreUtilities\LJCGenText

:Clear
set AppFolder=LJCGenTableCode
del %RootFolder%\%AppFolder%\bin\Debug\DataFiles\*.* /q
del %RootFolder%\%AppFolder%\bin\Debug\FormFiles\*.* /q
del %RootFolder%\%AppFolder%\bin\Debug\ManagerFiles\*.* /q
del %RootFolder%\%AppFolder%\bin\Debug\XMLFiles\*.* /q

set AppFolder=LJCGenTextEdit
del %RootFolder%\%AppFolder%\bin\Debug\DataXML\*.* /q
del %RootFolder%\%AppFolder%\bin\Debug\TempFiles\*.* /q
