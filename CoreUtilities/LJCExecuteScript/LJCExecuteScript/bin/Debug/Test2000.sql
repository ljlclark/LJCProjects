use DataTransform;
GO

drop table ProcessTest;
go

select top 2000 *
into ProcessTest
from Process;
Go
