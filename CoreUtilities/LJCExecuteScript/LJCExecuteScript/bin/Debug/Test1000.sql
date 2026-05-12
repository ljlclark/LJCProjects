use DataTransform;
GO

drop table ProcessTest;
go

select top 1000 *
into ProcessTest
from Process;
Go
