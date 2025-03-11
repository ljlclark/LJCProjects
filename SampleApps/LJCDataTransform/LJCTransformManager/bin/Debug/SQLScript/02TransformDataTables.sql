-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
USE [TransformData]
drop table TransformMap
drop table MapType;
drop table TransformMatch;
drop table TaskTransform;
drop table TaskSource;
drop table DataSource;
drop table SourceStatus;
drop table SourceType;
drop table LayoutColumn;
drop table DataType;
drop table Layout;
drop table Task;
drop table TaskStatus;
drop table TaskType;
drop table Step;
drop table ProcessGroupProcess;
drop table Process;
drop table ProcessStatus;
drop table ProcessGroup;
*/

-- The ProcessGroup table.
--------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ProcessGroup')
BEGIN
CREATE TABLE [dbo].[ProcessGroup](
  [ProcessGroupID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_ProcessGroup] PRIMARY KEY CLUSTERED
  (
    [ProcessGroupID] ASC
  )
)
END
GO

-- The ProcessStatus table.
---------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ProcessStatus')
BEGIN
CREATE TABLE [dbo].[ProcessStatus](
  [ProcessStatusID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_ProcesesStatus] PRIMARY KEY CLUSTERED
  (
    [ProcessStatusID] ASC
  )
)
END
GO

IF NOT EXISTS (select ProcessStatusID from ProcessStatus where Name = 'Available')
insert into ProcessStatus
 (Name, Description)
 values('Available', 'The data process is available.');

IF NOT EXISTS (select ProcessStatusID from ProcessStatus where Name = 'Active')
insert into ProcessStatus
 (Name, Description)
 values('Active', 'The data process is selected for execution.');

IF NOT EXISTS (select ProcessStatusID from ProcessStatus where Name = 'In-process')
insert into ProcessStatus
 (Name, Description)
 values('In-process', 'The data process is executing.');

IF NOT EXISTS (select ProcessStatusID from ProcessStatus where Name = 'Ready')
insert into ProcessStatus
 (Name, Description)
 values('Ready', 'The dataprocess is ready for the next process.');

IF NOT EXISTS (select ProcessStatusID from ProcessStatus where Name = 'Failed')
insert into ProcessStatus
 (Name, Description)
 values('Failed', 'The process failed.');
GO

-- The Process table.
---------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DataProcess')
BEGIN
CREATE TABLE [dbo].[DataProcess](
  [DataProcessID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [ProcessStatusID] [smallint] NOT NULL,
  CONSTRAINT [PK_Process] PRIMARY KEY CLUSTERED 
  (
    [DataProcessID] ASC
  )
)
ALTER TABLE [dbo].[DataProcess] WITH CHECK
  ADD CONSTRAINT [FK_DataProcess_ProcessStatus]
  FOREIGN KEY([ProcessStatusID])
  REFERENCES [dbo].[ProcessStatus] ([ProcessStatusID])
END
GO

-- The ProcessGroupProcess M:N table.
-------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'ProcessGroupProcess')
BEGIN
CREATE TABLE [dbo].[ProcessGroupProcess](
  [ProcessGroupID] [int] NOT NULL,
  [DataProcessID] [int] NOT NULL,
  [Sequence] [int] NOT NULL,
)
ALTER TABLE [dbo].[ProcessGroupProcess] WITH CHECK
  ADD CONSTRAINT [FK_ProcessGroupProcess_ProcessGroup]
  FOREIGN KEY([ProcessGroupID])
  REFERENCES [dbo].[ProcessGroup] ([ProcessGroupID])
ALTER TABLE [dbo].[ProcessGroupProcess] WITH CHECK
  ADD CONSTRAINT [FK_ProcessGroupProcess_DataProcess]
  FOREIGN KEY([DataProcessID])
  REFERENCES [dbo].[DataProcess] ([DataProcessID])
END
GO

-- The TaskStatus table.
------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'TaskStatus')
BEGIN
CREATE TABLE [dbo].[TaskStatus](
  [TaskStatusID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_TaskStatus] PRIMARY KEY CLUSTERED
  (
    [TaskStatusID] ASC
  )
)
END
GO

IF NOT EXISTS (select TaskStatusID from TaskStatus where Name = 'Available')
insert into TaskStatus
 (Name, Description)
 values('Available', 'The task is available for processing.');

IF NOT EXISTS (select TaskStatusID from TaskStatus where Name = 'Active')
insert into TaskStatus
 (Name, Description)
 values('Active', 'The task is selected for processing.');

IF NOT EXISTS (select TaskStatusID from TaskStatus where Name = 'In-process')
insert into TaskStatus
 (Name, Description)
 values('In-process', 'The task is being processed.');

IF NOT EXISTS (select TaskStatusID from TaskStatus where Name = 'Ready')
insert into TaskStatus
 (Name, Description)
 values('Ready', 'The task is ready for the next task.');

IF NOT EXISTS (select TaskStatusID from TaskStatus where Name = 'Failed')
insert into TaskStatus
 (Name, Description)
 values('Failed', 'The task failed.');
GO

-- The Step N:1 table
---------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'Step')
BEGIN
CREATE TABLE [dbo].[Step](
  [StepID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [DataProcessID] [int] NOT NULL,
  [Sequence] [smallint] NOT NULL,
  [StatusID] [smallint] NOT NULL,
  CONSTRAINT [PK_Step] PRIMARY KEY CLUSTERED
  (
    [StepID] ASC
  )
)
ALTER TABLE [dbo].[Step] WITH CHECK
  ADD CONSTRAINT [FK_Step_DataProcess]
  FOREIGN KEY([DataProcessID])
  REFERENCES [dbo].[DataProcess] ([DataProcessID])
ALTER TABLE [dbo].[Step] WITH CHECK
  ADD CONSTRAINT [FK_Step_TaskStatus]
  FOREIGN KEY([StatusID])
  REFERENCES [dbo].[TaskStatus] ([TaskStatusID])
END
GO

-- The TaskType table.
----------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'TaskType')
BEGIN
CREATE TABLE [dbo].[TaskType](
  [TaskTypeID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_TaskType] PRIMARY KEY CLUSTERED
  (
    [TaskTypeID] ASC
  )
)
END
GO

IF NOT EXISTS (select TaskTypeID from TaskType where Name = 'Program')
insert into TaskType
 (Name, Description)
 values('Program', 'A program module.');

IF NOT EXISTS (select TaskTypeID from TaskType where Name = 'SQLScript')
insert into TaskType
 (Name, Description)
 values('SQLScript', 'A SQL script file.');

IF NOT EXISTS (select TaskTypeID from TaskType where Name = 'StoredProcedure')
insert into TaskType
 (Name, Description)
 values('StoredProcedure', 'A stored procedure.');
GO

-- The Task N:1 table.
----------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'StepTask')
BEGIN
CREATE TABLE [dbo].[StepTask](
  [StepTaskID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [StepID] [int] NOT NULL,
  [Sequence] [int] NOT NULL,
  [TaskTypeID] [smallint] NOT NULL,
  [ActionItemName] [nvarchar](100) NULL,
  [TaskStatusID] [smallint] NOT NULL,
  CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED
  (
    [StepTaskID] ASC
  )
)
ALTER TABLE [dbo].[StepTask] WITH CHECK
  ADD CONSTRAINT [FK_StepTask_Step]
  FOREIGN KEY([StepID])
  REFERENCES [dbo].[Step] ([StepID])
ALTER TABLE [dbo].[StepTask] WITH CHECK
  ADD CONSTRAINT [FK_StepTask_TaskType]
  FOREIGN KEY([TaskTypeID])
  REFERENCES [dbo].[TaskType] ([TaskTypeID])
ALTER TABLE [dbo].[StepTask] WITH CHECK
  ADD CONSTRAINT [FK_StepTask_TaskStatus]
  FOREIGN KEY([TaskStatusID])
  REFERENCES [dbo].[TaskStatus] ([TaskStatusID])
END
GO

-- The Layout table.
--------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'SourceLayout')
BEGIN
CREATE TABLE [dbo].[SourceLayout](
  [SourceLayoutID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_Layout] PRIMARY KEY CLUSTERED
  (
    [SourceLayoutID] ASC
  )
)
END
GO

-- The DataType table.
----------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DataType')
BEGIN
CREATE TABLE [dbo].[DataType](
  [DataTypeID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [SQLName] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_DataType] PRIMARY KEY CLUSTERED
  (
    [DataTypeID] ASC
  )
)
END
GO

IF NOT EXISTS (select Name from DataType where Name = 'Boolean')
insert into DataType
 (Name, SQLName, Description)
 values('Boolean', 'bit', 'True or false. 0 or 1.')

IF NOT EXISTS (select Name from DataType where Name = 'Byte')
insert into DataType
 (Name, SQLName, Description)
 values('Byte', 'tinyint', 'An eight bit value.');

IF NOT EXISTS (select Name from DataType where Name = 'DateTime')
insert into DataType
 (Name, SQLName, Description)
 values('DateTime', 'datetime', 'A Date and Time value.');

IF NOT EXISTS (select Name from DataType where Name = 'Decimal')
insert into DataType
 (Name, SQLName, Description)
 values('Decimal', 'decimal', 'A decimal value.');

IF NOT EXISTS (select Name from DataType where Name = 'Double')
insert into DataType
 (Name, SQLName, Description)
 values('Double', 'float', 'A double precision value.');

IF NOT EXISTS (select Name from DataType where Name = 'Int16')
insert into DataType
 (Name, SQLName, Description)
 values('Int16', 'smallint', 'A two byte integer.');

IF NOT EXISTS (select Name from DataType where Name = 'Int32')
insert into DataType
 (Name, SQLName, Description)
 values('Int32', 'int', 'A four byte integer.');

IF NOT EXISTS (select Name from DataType where Name = 'Int64')
insert into DataType
 (Name, SQLName, Description)
 values('Int64', 'bigint', 'An eight byte integer.');

IF NOT EXISTS (select Name from DataType where Name = 'Single')
insert into DataType
 (Name, SQLName, Description)
 values('Single', 'real', 'A single precision value.');

IF NOT EXISTS (select Name from DataType where Name = 'String')
insert into DataType
 (Name, SQLName, Description)
 values('String', 'nvarchar', 'A string value.');
GO

-- The LayoutColumn N:1 table.
------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'LayoutColumn')
BEGIN
CREATE TABLE [dbo].[LayoutColumn](
  [LayoutColumnID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [SourceLayoutID] [int] NOT NULL,
  [Sequence] [int] NOT NULL,
  [DataTypeID] [smallint] NOT NULL,
  [Length] [int] NULL,
  [IdentityKey] [bit] NOT NULL,
  [PrimaryKey] [bit] NOT NULL,
  [AllowNull] [bit] NOT NULL,
  CONSTRAINT [PK_LayoutColumn] PRIMARY KEY CLUSTERED
  (
    [LayoutColumnID] ASC
  )
)
ALTER TABLE [dbo].[LayoutColumn] WITH CHECK
  ADD CONSTRAINT [FK_LayoutColumn_SourceLayout]
  FOREIGN KEY([SourceLayoutID])
  REFERENCES [dbo].[SourceLayout] ([SourceLayoutID])
ALTER TABLE [dbo].[LayoutColumn] WITH CHECK
  ADD CONSTRAINT [FK_LayoutColumn_DataType]
  FOREIGN KEY([DataTypeID])
  REFERENCES [dbo].[DataType] ([DataTypeID])
END
GO

-- The SourceType table.
------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'SourceType')
BEGIN
CREATE TABLE [dbo].[SourceType](
  [SourceTypeID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_SourceType] PRIMARY KEY CLUSTERED
  (
    [SourceTypeID] ASC
  )
)
END
GO

IF NOT EXISTS (select SourceTypeID from SourceType where Name = 'File')
insert into SourceType
 (Name, Description)
 values('File', 'A delimited text file.');

IF NOT EXISTS (select SourceTypeID from SourceType where Name = 'Table')
insert into SourceType
 (Name, Description)
 values('Table', 'A database table.');
GO

-- The SourceStatus table.
--------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'SourceStatus')
BEGIN
CREATE TABLE [dbo].[SourceStatus](
  [SourceStatusID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_SourceStatus] PRIMARY KEY CLUSTERED
  (
    [SourceStatusID] ASC
  )
)
END
GO

IF NOT EXISTS (select SourceStatusID from SourceStatus where Name = 'Available')
insert into SourceStatus
 (Name, Description)
 values('Available', 'The data source is available for processing.');

IF NOT EXISTS (select SourceStatusID from SourceStatus where Name = 'Active')
insert into SourceStatus
 (Name, Description)
 values('Active', 'The data source is selected for processing.');

IF NOT EXISTS (select SourceStatusID from SourceStatus where Name = 'In-process')
insert into SourceStatus
 (Name, Description)
 values('In-process', 'The data source is being processed.');

IF NOT EXISTS (select SourceStatusID from SourceStatus where Name = 'Ready')
insert into SourceStatus
 (Name, Description)
 values('Ready', 'The data source is ready for the next task.');
GO

-- The DataSource table.
--------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'DataSource')
BEGIN
CREATE TABLE [dbo].[DataSource](
  [DataSourceID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [SourceTypeID] [smallint] NOT NULL,
  [DataConfigName] [nvarchar](100) NULL,
  [SourceItemName] [nvarchar](100) NULL,
  [SourceLayoutID] [int] NOT NULL,
  [SourceStatusID] [smallint] NOT NULL,
  CONSTRAINT [PK_DataSource] PRIMARY KEY CLUSTERED
  (
    [DataSourceID] ASC
  )
)
ALTER TABLE [dbo].[DataSource] WITH CHECK
  ADD CONSTRAINT [FK_DataSource_SourceType]
  FOREIGN KEY([SourceTypeID])
  REFERENCES [dbo].[SourceType] ([SourceTypeID])
ALTER TABLE [dbo].[DataSource] WITH CHECK
  ADD CONSTRAINT [FK_DataSource_SourceLayout]
  FOREIGN KEY([SourceLayoutID])
  REFERENCES [dbo].[SourceLayout] ([SourceLayoutID])
ALTER TABLE [dbo].[DataSource] WITH CHECK
  ADD CONSTRAINT [FK_DataSource_SourceStatus]
  FOREIGN KEY([SourceStatusID])
  REFERENCES [dbo].[SourceStatus] ([SourceStatusID])
END
GO

-- The TaskSource M:N table.
----------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'TaskSource')
BEGIN
CREATE TABLE [dbo].[TaskSource](
  [StepTaskID] [int] NOT NULL,
  [DataSourceID] [int] NOT NULL,
)
ALTER TABLE [dbo].[TaskSource] WITH CHECK
  ADD CONSTRAINT [FK_TaskSource_StepTask]
  FOREIGN KEY([StepTaskID])
  REFERENCES [dbo].[StepTask] ([StepTaskID])
ALTER TABLE [dbo].[TaskSource] WITH CHECK
  ADD CONSTRAINT [FK_TaskSource_DataSource]
  FOREIGN KEY([DataSourceID])
  REFERENCES [dbo].[DataSource] ([DataSourceID])
END
GO

-- The TaskTransform N:M table.
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'TaskTransform')
BEGIN
CREATE TABLE [dbo].[TaskTransform](
  [TransformID] [int] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  [StepTaskID] [int] NOT NULL,
  [DataSourceID] [int] NOT NULL,
  [TargetID] [int] NOT NULL,
  CONSTRAINT [PK_TaskTransform] PRIMARY KEY CLUSTERED
  (
    [TransformID] ASC
  )
)
ALTER TABLE [dbo].[TaskTransform] WITH CHECK
  ADD CONSTRAINT [FK_TaskTransform_StepTask]
  FOREIGN KEY([StepTaskID])
  REFERENCES [dbo].[StepTask] ([StepTaskID])
ALTER TABLE [dbo].[TaskTransform] WITH CHECK
  ADD CONSTRAINT [FK_TaskTransform_DataSource]
  FOREIGN KEY([DataSourceID])
  REFERENCES [dbo].[DataSource] ([DataSourceID])
ALTER TABLE [dbo].[TaskTransform] WITH CHECK
  ADD CONSTRAINT [FK_TaskTransform_DataSource1]
  FOREIGN KEY([TargetID])
  REFERENCES [dbo].[DataSource] ([DataSourceID])
END
GO

-- The TransformMatch N:M table.
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'TransformMatch')
BEGIN
CREATE TABLE [dbo].[TransformMatch](
  [TransformMatchID] [int] IDENTITY(1,1) NOT NULL,
  [TransformID] [int] NOT NULL,
  [Sequence] [int] NOT NULL,
  [SourceColumnID] [smallint] NOT NULL,
  [TargetColumnID] [smallint] NOT NULL,
  CONSTRAINT [PK_TransformMatch] PRIMARY KEY CLUSTERED
  (
    [TransformMatchID] ASC
  )
)
ALTER TABLE [dbo].[TransformMatch] WITH CHECK
  ADD CONSTRAINT [FK_TransformMatch_Transform]
  FOREIGN KEY([TransformID])
  REFERENCES [dbo].[TaskTransform] ([TransformID])
ALTER TABLE [dbo].[TransformMatch] WITH CHECK
  ADD CONSTRAINT [FK_TransformMatch_LayoutColumn]
  FOREIGN KEY([SourceColumnID])
  REFERENCES [dbo].[LayoutColumn] ([LayoutColumnID])
ALTER TABLE [dbo].[TransformMatch] WITH CHECK
  ADD CONSTRAINT [FK_TransformMatch_LayoutColumn1]
  FOREIGN KEY([TargetColumnID])
  REFERENCES [dbo].[LayoutColumn] ([LayoutColumnID])
END
GO

-- The MapType table.
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'MapType')
BEGIN
CREATE TABLE [dbo].[MapType](
  [MapTypeID] [smallint] IDENTITY(1,1) NOT NULL,
  [Name] [varchar](60) NOT NULL,
  [Description] [varchar](100) NULL,
  CONSTRAINT [PK_MapType] PRIMARY KEY CLUSTERED
  (
    [MapTypeID] ASC
  )
)
END
GO

IF NOT EXISTS (select MapTypeID from MapType where Name = 'Merge')
insert into MapType
 (Name, Description)
 values('Merge', 'Copy the value only if the target value is missing.');

IF NOT EXISTS (select MapTypeID from MapType where Name = 'Override')
insert into MapType
 (Name, Description)
 values('Overwrite', 'Always copy the value if it exists.');

IF NOT EXISTS (select MapTypeID from MapType where Name = 'Insert')
insert into MapType
 (Name, Description)
 values('InsertInclude', 'Include in an insert of a new row.');
GO

-- The TransformMap N:M table.
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'TransformMap')
BEGIN
CREATE TABLE [dbo].[TransformMap](
  [TransformMapID] [int] IDENTITY(1,1) NOT NULL,
  [TransformID] [int] NOT NULL,
  [Sequence] [int] NOT NULL,
  [SourceColumnID] [smallint] NOT NULL,
  [TargetColumnID] [smallint] NOT NULL,
  [MapTypeID] [smallint] NOT NULL,
  CONSTRAINT [PK_TransformMap] PRIMARY KEY CLUSTERED
  (
    [TransformMapID] ASC
  )
)
ALTER TABLE [dbo].[TransformMap] WITH CHECK
  ADD CONSTRAINT [FK_TransformMap_Transform]
  FOREIGN KEY([TransformID])
  REFERENCES [dbo].[TaskTransform] ([TransformID])
ALTER TABLE [dbo].[TransformMap] WITH CHECK
  ADD CONSTRAINT [FK_TransformMap_LayoutColumn]
  FOREIGN KEY([SourceColumnID])
  REFERENCES [dbo].[LayoutColumn] ([LayoutColumnID])
ALTER TABLE [dbo].[TransformMap] WITH CHECK
  ADD CONSTRAINT [FK_TransformMap_LayoutColumn1]
  FOREIGN KEY([TargetColumnID])
  REFERENCES [dbo].[LayoutColumn] ([LayoutColumnID])
ALTER TABLE [dbo].[TransformMap] WITH CHECK
  ADD CONSTRAINT [FK_TransformMap_MapType]
  FOREIGN KEY([MapTypeID])
  REFERENCES [dbo].[MapType] ([MapTypeID])
END
GO
