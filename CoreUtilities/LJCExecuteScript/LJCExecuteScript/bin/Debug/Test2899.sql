use DataTransform;
GO

drop table ProcessTest;
go

select top 2899 *
into ProcessTest
from Process;
Go
