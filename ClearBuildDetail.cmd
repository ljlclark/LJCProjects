echo Copyright (c) Lester J. Clark and Contributors.
echo Licensed under the MIT License.
rem ClearBuildDetail.cmd

rmdir %Solution%\.vs /s /q
del %Solution%\External\*.* /q
rmdir %Solution%\External
del %Solution%\%Project%\*.user
del %Solution%\%Project%\bin\Debug\%File%.exe
del %Solution%\%Project%\bin\Debug\*.dll
del %Solution%\%Project%\bin\Debug\*.pdb
del %Solution%\%Project%\bin\Debug\%File%.xml
rem del %Solution%\%Project%\obj\Debug\*.* /q
rmdir %Solution%\%Project%\obj /s /q