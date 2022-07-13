echo Copyright (c) Lester J. Clark 2022 - All Rights Reserved
rmdir %Solution%\.vs /s /q
del %Solution%\External\*.* /q
del %Solution%\%Project%\bin\Debug\%File%.exe
del %Solution%\%Project%\bin\Debug\*.dll
del %Solution%\%Project%\bin\Debug\*.pdb
del %Solution%\%Project%\bin\Debug\%File%.xml
rem del %Solution%\%Project%\obj\Debug\*.* /q
rmdir %Solution%\%Project%\obj /s /q