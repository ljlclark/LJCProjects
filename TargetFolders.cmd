rem Copyright (c) Lester J. Clark and Contributors.
rem Licensed under the MIT License.
rem TargetFolders.cmd

set to=%toRoot%External
if exist %to%\NUL goto continue
mkdir %to%
:continue