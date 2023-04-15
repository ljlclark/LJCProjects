/* sp_ResetSequence.sql */
/* use [LJCData] */
go
set ansi_nulls on
go
set quoted_identifier on
go

if object_id ( 'dbo.sp_ResetSequence', 'P' ) is not null
  drop procedure dbo.sp_ResetSequence;  
go  
create procedure dbo.sp_ResetSequence
  @table varchar(100),
  @idColumn varchar(100),
  @sequenceColumn varchar(100),
  @where varchar(200) = null
as
begin
declare @sql nvarchar(200);

create table #Resequence (
 ID int,
 [Sequence] int
);

/* Copy records to temp table. */
set @sql = 'insert into #Resequence'
set @sql += '(ID, [Sequence])';
set @sql += ' select [' + @idColumn + '], [' + @sequenceColumn + ']';
set @sql += ' from ' + @table;
if (@where is not null)
begin
  set @sql += ' ' + @where;
end
exec sp_executesql @sql;

/* Reset sequences in temp table. */
declare @newSequence int = 0;
declare @sequence int;
declare @id int;

declare varCursor cursor for
select ID, [Sequence] 
from #Resequence
order by ID, [Sequence]; 

open varCursor  
fetch next from varCursor into @id, @sequence

while @@FETCH_STATUS = 0  
begin
  set @newSequence = @newSequence + 1;
  if (@sequence <> @newSequence)
  begin
    update #Resequence
	set [Sequence] = @newSequence
	where ID = @id;
  end

  fetch next from varCursor into @id, @sequence 
end 

close varCursor  
deallocate varCursor

/* Copy resequence values to original table. */
set @sql = 'update t set';
set @sql += ' t.[' + @sequenceColumn + '] = r.[Sequence]';
set @sql += ' from ' + @table + ' t';
set @sql += ' inner join #Resequence r'
set @sql += ' on t.[' + @idColumn + '] = r.ID';
exec sp_executesql @sql;
end
go
