use DataTransform;
GO

drop table ProcessTest;
go

select top 899 *
into ProcessTest
from Process;
Go
