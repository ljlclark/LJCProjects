use LJCData
go

set ansi_nulls on
go

set quoted_identifier on
go

if not exists (select * from INFORMATION_SCHEMA.TABLES
 where TABLE_NAME = N'DetailConfig')
begin
create table dbo.DetailConfig(
  ID bigint identity(1,1) NOT NULL,
  UserID nvarchar(60) not null,
  DataConfigName nvarchar(60) not null,
  TableName nvarchar(60) not null,
  DataValueCount int not null,
  ColumnRowsLimit int not null,
  PageColumnsLimit int not null,
  CharacterPixels int not null,
  MaxControlCharacters int not null,
  BorderHorizontal int not null,
  BorderVertical int not null,
  ControlRowSpacing int not null,
  ControlRowHeight int not null,
  ControlsHeight int not null,
  constraint PK_DetailDialog
    primary key clustered (ID asc),
  constraint UK_DetailDialog
    unique (UserID, DataConfigName, TableName)
)
end

if not exists (select * from INFORMATION_SCHEMA.TABLES
 where TABLE_NAME = N'ControlColumn')
begin
create table dbo.ControlColumn(
  ID bigint identity(1,1) NOT NULL,
  DetailConfigID bigint not null,
  ColumnIndex int not null,
  TabPageIndex int not null,
  LabelsWidth int not null,
  ControlsWidth int not null,
  constraint PK_ControlColumn
    primary key clustered (ID asc),
  constraint UK_ControlColumn
    unique (DetailConfigID, ColumnIndex)
)
end

if not exists (select * from INFORMATION_SCHEMA.TABLES
 where TABLE_NAME = N'ControlRow')
begin
create table dbo.ControlRow(
  ID bigint identity(1,1) NOT NULL,
  ControlColumnID bigint not null,
  DataValueName nvarchar(60) NOT NULL,
  RowIndex int not null,
  TabbingIndex int not null,
  AllowDisplay bit not null default 1,
  constraint PK_ControlRow
    primary key clustered (ID asc),
  constraint UK_ControlRow
    unique (ControlColumnID, DataValueName)
)
end
