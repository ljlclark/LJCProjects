echo Copyright (c) Lester J. Clark 2017-2019 - All Rights Reserved
call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\Tools\VsDevCmd.bat"
svcutil http://localhost:8080/DBService /out:ProxyDbService.cs /config:app.config
