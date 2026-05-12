use DataTransform;
GO

drop table ProcessTest;
go

select top 2001 *
into ProcessTest
from Process;
Go
