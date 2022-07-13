echo Copyright (c) Lester J. Clark 2021,2022 - All Rights Reserved

call LJCCodeLineCounter\UpdateCodeLineCounter.cmd BuildAll > Update.txt
call LJCDataAccess\UpdateDataAccess.cmd BuildAll > Update.txt
call LJCDataAccessConfig\UpdateDataAccessConfig.cmd BuildAll > Update.txt
call LJCTextDataReader\UpdateTextDataReader.cmd BuildAll > Update.txt

