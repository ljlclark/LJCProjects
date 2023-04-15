/* sp_ChangeSequence.sql */
/* use [LJCData] */
go
set ansi_nulls on
go
set quoted_identifier on
go

if object_id ( 'dbo.sp_ChangeSequence', 'P' ) is not null
  drop procedure dbo.sp_ChangeSequence;  
go  
create procedure dbo.sp_ChangeSequence
  @table varchar(100),
  @column varchar(100),
  @sourceSequence int,
  @targetSequence int,
  @where varchar(200) = null
as
begin
/*
  Add a sequence space: source = 0, target = space sequence.
  Remove a sequence space: source = -1, target = space sequence.
*/
declare @endSequence int;
declare @sql nvarchar(320);
declare @params nvarchar(100);

/* Get records end sequence. */
set @sql = 'select @endSequence = max([' + @column + ']) + 2';
set @sql += ' from ' + @table;
set @sql += ' ' + @where;
set @params = '@endSequence int output';
exec sp_executesql @sql, @params, @endSequence output;

/* Move source Sequence to end sequence. */
if (@sourceSequence > 0)
begin
  set @sql = 'update ' + @table;
  set @sql += ' set [' + @column + '] = ' + cast(@endSequence as nvarchar(20));
  set @sql += ' where [' + @column + '] = ' + cast(@sourceSequence as nvarchar(20));
  if (@where <> null)
  begin
    set @sql += ' and '+ @where;
  end
  exec sp_executesql @sql;
end
else
begin
  if (0 = @sourceSequence)
  begin
    /* set to move everything from the target sequence down. */
    set @sourceSequence = @endSequence;
  end
  else
  begin
    /* set to move everything from the target sequence up. */
    set @sourceSequence = @targetSequence;
	set @targetSequence = @endSequence;
  end
end

if (@sourceSequence < @targetSequence)
begin
  /* Move contained sequences up to fill moved source space. */
  set @sql = 'update ' + @table + ' set [' + @column + '] = ' + @column + ' - 1';
  set @sql += ' where [' + @column + '] > ' + cast(@sourceSequence as nvarchar(20));
  set @sql += ' and [' + @column + '] <= ' + cast(@targetSequence as nvarchar(20));
  if (@where <> null)
  begin
    set @sql += ' and '+ @where;
  end
end
else
begin
  if (@sourceSequence <> @targetSequence)
  begin
    /* Move contained sequences down to fill moved source space. */
    set @sql = 'update ' + @table + ' set [' + @column + '] = ' + @column + ' + 1';
    set @sql += ' where [' + @column + '] < ' + cast(@sourceSequence as nvarchar(20));
    set @sql += ' and [' + @column + '] >= ' + cast(@targetSequence as nvarchar(20));
    if (@where <> null)
    begin
      set @sql += ' and '+ @where;
    end
  end
end
exec sp_executesql @sql;

/* Set new source sequence. */
if (@sourceSequence <> @endSequence
and @sourceSequence <> @targetSequence)
begin
  set @sql = 'update ' + @table + ' set [' + @column + '] = ' + cast(@targetSequence as nvarchar(20));
  set @sql += ' where [' + @column + '] = ' + cast(@endSequence as nvarchar(20));
  if (@where <> null)
  begin
    set @sql += ' and '+ @where;
  end
  exec sp_executesql @sql;
end
end
go
