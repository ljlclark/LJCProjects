use LJCData
go

set ansi_nulls on
go

set quoted_identifier on
go

if not exists (select * from INFORMATION_SCHEMA.TABLES
 where TABLE_NAME = N'ControlDetail')
begin
create table dbo.ControlDetail(
  ID bigint identity(1,1) NOT NULL,
  [Name] nvarchar(60) not null,
  [Description] nvarchar(60) not null,
  DataConfigName nvarchar(60) not null,
  TableName nvarchar(60) not null,
  UserID nvarchar(60) null,
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
  ControlsWidth int not null,
  ColumnRowCount int not null,
  constraint PKDetailDialog
    primary key clustered (ID asc),
  constraint UKDetailDialog
    unique ([Name])
)
end

if not exists (select * from INFORMATION_SCHEMA.TABLES
 where TABLE_NAME = N'ControlData')
begin
create table dbo.ControlData(
  ID bigint identity(1,1) not null,
  ControlDetailID bigint not null,
  AllowDBNull bit not null,
  AutoIncrement bit not null,
	Caption nvarchar(60) null,
	ColumnName nvarchar(60) not null,
	DataTypeName nvarchar(60) not null,
	[MaxLength] int null,
	Position int null,
	PropertyName nvarchar(60) not null,
	RenameAs nvarchar(60) null,
	SQLTypeName nvarchar(60) not null,
	[Value] nvarchar(60) null,
  constraint PKControlData
    primary key clustered (ID asc),
  constraint UKControlData
    unique (ControlDetailID, PropertyName),
  constraint FKControlData
    foreign key (ControlDetailID)
    references ControlDetail (ID)
    on delete no action on update no action
)
end

if not exists (select * from INFORMATION_SCHEMA.TABLES
 where TABLE_NAME = N'ControlTab')
begin
create table dbo.ControlTab(
  ID bigint identity(1,1) NOT NULL,
  ControlDetailID bigint not null,
  TabIndex int not null,
  Caption nvarchar(40) not null,
  [Description] nvarchar(60) not null,
  constraint PKControlTab
    primary key clustered (ID asc),
  constraint UKControlTab
    unique (ControlDetailID, TabIndex),
  constraint FKControlTab
    foreign key (ControlDetailID)
    references ControlDetail (ID)
    on delete no action on update no action
)
end

if not exists (select * from INFORMATION_SCHEMA.TABLES
 where TABLE_NAME = N'ControlColumn')
begin
create table dbo.ControlColumn(
  ID bigint identity(1,1) NOT NULL,
  ControlTabID bigint not null,
  ColumnIndex int not null,
  LabelsWidth int not null,
  ControlsWidth int not null,
  constraint PKControlColumn
    primary key clustered (ID asc),
  constraint UKControlColumn
    unique (ControlTabID, ColumnIndex),
  constraint FKControlColumn
    foreign key (ControlTabID)
    references ControlTab (ID)
    on delete no action on update no action
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
  constraint PKControlRow
    primary key clustered (ID asc),
  constraint UKControlRow
    unique (ControlColumnID, DataValueName),
  constraint FKControlRow
    foreign key (ControlColumnID)
    references ControlColumn (ID)
    on delete no action on update no action
)
end
