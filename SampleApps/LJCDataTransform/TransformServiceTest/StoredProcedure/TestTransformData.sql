-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
-- ProcessGroup
IF NOT EXISTS (select Name from ProcessGroup where Name = 'TestGroup')
insert into ProcessGroup (Name, Description) values('TestGroup', 'The test process group.');
go

-- DataProcess
declare @ProcessStatusID int = (select ProcessStatusID from ProcessStatus where Name = 'Available');
IF NOT EXISTS (select Name from DataProcess where Name = 'TestProcess')
insert into DataProcess (Name, Description, ProcessStatusID)
 values('TestProcess', 'The test process.', @ProcessStatusID);
go

-- ProcessGroupProcess
declare @ProcessGroupID int;
declare @ProcessID int;

set @ProcessGroupID = (select ProcessGroupID from ProcessGroup where Name = 'TestGroup');
set @ProcessID = (select DataProcessID from DataProcess where Name = 'TestProcess');
IF NOT EXISTS (select DataProcessID from ProcessGroupProcess where DataProcessID = @ProcessID)
insert into ProcessGroupProcess (ProcessGroupID, DataProcessID, Sequence)
 values(@ProcessGroupID, @ProcessID, 1);

-- Step
declare @TaskStatusID int = (select TaskStatusID from TaskStatus where Name = 'Available');
IF NOT EXISTS (select Name from Step where Name = 'TestStep')
insert into Step (Name, Description, DataProcessID, Sequence, StatusID)
 values('TestStep', 'The test step.', @ProcessID, 1, @TaskStatusID);
go

-- Layout
IF NOT EXISTS (select Name from SourceLayout where Name = 'AddressTextLayout')
insert into SourceLayout (Name, Description) values('AddressTextLayout', 'The Address Text Data Layout.');
IF NOT EXISTS (select Name from SourceLayout where Name = 'AddressTableLayout')
insert into SourceLayout (Name, Description) values('AddressTableLayout', 'The Address Table Layout.');
go

-- LayoutColumn
-- Columns for AddressTextLayout
declare @LayoutName varchar(60) = 'AddressTextLayout';
declare @DataTypeName varchar(60) = 'String';
exec sp_CreateLayoutColumn 'SSN', 'SSN', @LayoutName, 1
 , @DataTypeName, 11, 0, 0, 0

exec sp_CreateLayoutColumn 'Address', 'Test address.', @LayoutName, 2
 , @DataTypeName, 80, 0, 0, 1

exec sp_CreateLayoutColumn 'Name', 'Test name.', @LayoutName, 3
 , @DataTypeName, 60, 0, 0, 0

exec sp_CreateLayoutColumn 'City1', 'Test city.', @LayoutName, 4
 , @DataTypeName, 60, 0, 0, 1

exec sp_CreateLayoutColumn 'State', 'Test state.', @LayoutName, 5
 , @DataTypeName, 60, 0, 0, 1

exec sp_CreateLayoutColumn 'Zip', 'Test ZipCode.', @LayoutName, 6
 , @DataTypeName, 10, 0, 0, 1

-- Columns for AddressTable Layout
set @LayoutName = 'AddressTableLayout';
set @DataTypeName = 'Int32';
exec sp_CreateLayoutColumn 'ID', 'ID', @LayoutName, 1
 , @DataTypeName, 0, 1, 1, 0

set @DataTypeName = 'String';
exec sp_CreateLayoutColumn 'SSN', 'SSN', @LayoutName, 2
 , @DataTypeName, 11, 0, 0, 0

exec sp_CreateLayoutColumn 'Name', 'Test name.', @LayoutName, 3
 , @DataTypeName, 60, 0, 0, 0

exec sp_CreateLayoutColumn 'Address', 'Test address.', @LayoutName, 4
 , @DataTypeName, 80, 0, 0, 1

exec sp_CreateLayoutColumn 'City', 'Test city.', @LayoutName, 5
 , @DataTypeName, 60, 0, 0, 1

exec sp_CreateLayoutColumn 'City', 'Test city.', @LayoutName, 6
 , @DataTypeName, 60, 0, 0, 1

exec sp_CreateLayoutColumn 'State', 'Test state.', @LayoutName, 7
 , @DataTypeName, 60, 0, 0, 1

exec sp_CreateLayoutColumn 'Zip', 'Test ZipCode.', @LayoutName, 8
 , @DataTypeName, 10, 0, 0, 1

set @DataTypeName = 'Boolean';
exec sp_CreateLayoutColumn 'MatchFlag', 'The Match flag.', @LayoutName, 9
 , @DataTypeName, 0, 0, 0, 1
go

-- Task
-- Check Address File Columns
declare @StepName varchar(60) = 'TestStep';
declare @TaskTypeName varchar(60) = 'Program';
declare @TaskStatusName varchar(60) = 'Available';
exec sp_CreateTask 'CheckAddressColumns', 'Validate the Address data columns.'
 , @StepName, 1, @TaskTypeName
 , 'LJC.CommonModuleLib/CommonModule/VerifyTextColumns', @TaskStatusName

-- Check Address File Values
exec sp_CreateTask 'CheckAddressValues', 'Validate the Address column values.'
 , @StepName, 2, @TaskTypeName
 , 'LJC.CommonModuleLib/CommonModule/VerifyTextData', @TaskStatusName

-- Create Target Tables
exec sp_CreateTask 'CreateTable', 'Create a table.'
 , @StepName, 3, @TaskTypeName
 , 'LJC.CommonModuleLib/CommonModule/CreateTable', @TaskStatusName

-- Upload Data to Tables
exec sp_CreateTask 'UploadTable', 'Uploads the table.'
 , @StepName, 4, @TaskTypeName
 , 'LJC.CommonModuleLib/CommonModule/UploadTable', @TaskStatusName

-- Match Data
exec sp_CreateTask 'MatchTable', 'Matches the table data.'
 , @StepName, 5, @TaskTypeName
 , 'LJC.CommonModuleLib/CommonModule/MatchTable', @TaskStatusName

-- Merge Tables
exec sp_CreateTask 'MergeTable', 'Merges the tables.'
 , @StepName, 5, @TaskTypeName
 , 'LJC.CommonModuleLib/CommonModule/MergeTable', @TaskStatusName
go

-- DataSource
declare @SourceTypeName varchar(60) = 'File';
declare @DataConfigName varchar(60) = 'ProcessData';
declare @LayoutName varchar(60) = 'AddressTextLayout';
declare @SourceStatusName varchar(60) = 'Available';
exec sp_CreateDataSource 'AddressData1', 'The Main Address Data file.'
 , @SourceTypeName, @DataConfigName, 'AddressData1.txt', @LayoutName
 , @SourceStatusName

exec sp_CreateDataSource 'AddressData2', 'The Merge Address Data file.'
 , @SourceTypeName, @DataConfigName, 'AddressData2.txt', @LayoutName
 , @SourceStatusName

set @SourceTypeName = 'Table';
set @LayoutName = 'AddressTableLayout';
exec sp_CreateDataSource 'Address1', 'The Main Address table.'
 , @SourceTypeName, @DataConfigName, 'AddressData1', @LayoutName
 , @SourceStatusName

exec sp_CreateDataSource 'Address2', 'The Merge Address table.'
 , @SourceTypeName, @DataConfigName, 'AddressData2', @LayoutName
 , @SourceStatusName
go

-- TaskSource
-- Check Columns for AddressData1
declare @T varchar(60) = 'CheckAddressColumns'; -- TaskName
declare @L varchar(60) = 'AddressTextLayout'; -- LayoutName
declare @S varchar(60) = 'AddressData1'; -- SourceName
exec sp_CreateTaskSource @T, @L, @S

-- Check Values for AddressData1
set @T = 'CheckAddressValues'; -- TaskName
exec sp_CreateTaskSource @T, @L, @S

-- Create Address1 Table
set @T = 'CreateTable'; -- TaskName
set @L = 'AddressTableLayout'; -- LayoutName
set @S = 'Address1'; -- SourceName
exec sp_CreateTaskSource @T, @L, @S

-- Check Columns for AddressData2
set @T = 'CheckAddressColumns'; -- TaskName
set @L = 'AddressTextLayout'; -- LayoutName
set @S = 'AddressData2'; -- SourceName
exec sp_CreateTaskSource @T, @L, @S

-- Check Values for AddressData2
set @T = 'CheckAddressValues'; -- TaskName
exec sp_CreateTaskSource @T, @L, @S

-- Create AddressData2 Table
set @T = 'CreateTable'; -- TaskName
set @L = 'AddressTableLayout'; -- LayoutName
set @S = 'Address2'; -- SourceName
exec sp_CreateTaskSource @T, @L, @S
go

-- TaskTransform
-- Upload AddressData1 to Address1
declare @T varchar(60) = 'UploadTable'; -- TaskName
declare @FS varchar(60) = 'AddressData1'; -- FromSourceName
declare @TS varchar(60) = 'Address1'; -- ToSourceName
exec sp_CreateTransform 'UploadTable1', 'Uploads the table.', @T, @FS, @TS

-- Upload AddressData2 to Address2
set @FS = 'AddressData2'; -- FromSourceName
set @TS = 'Address2'; -- ToSourceName
exec sp_CreateTransform 'UploadTable2', 'Uploads the table.', @T, @FS, @TS

-- Match Address1 to Address2
set @T = 'MatchTable'; -- TaskName
set @FS = 'Address1'; -- FromSourceName
set @TS = 'Address2'; -- ToSourceName
exec sp_CreateTransform 'MatchTable', 'Matches the tables.', @T, @FS, @TS

-- Merge Address2 into Address1
set @T = 'MergeTable'; -- TaskName
set @FS = 'Address2'; -- FromSourceName
set @TS = 'Address1'; -- ToSourceName
exec sp_CreateTransform 'MergeTable', 'Merges the tables.', @T, @FS, @TS
go

-- TransformMatch
-- Match Address Tables
declare @T varchar(60) = 'MergeTable'; -- TaskTransformName
declare @SL varchar(60) = 'AddressTableLayout'; -- SourceLayoutName
declare @TL varchar(60) = 'AddressTableLayout'; -- TargetLayoutName
exec sp_CreateTransformMatch @T, 1, @SL, 'SSN', @TL, 'SSN'
go

-- TransformMap
-- AddressData1 text to Address1 table.
declare @T varchar(60) = 'UploadTable1'; -- TaskTransformName
declare @SL varchar(60) = 'AddressTextLayout'; -- SourceLayoutName
declare @TL varchar(60) = 'AddressTableLayout'; -- TargetLayoutName
declare @M varchar(60) = 'Merge'; -- MapTypeName
exec sp_CreateTransformMap @T, 1, @SL, 'SSN', @TL, 'SSN', @M
exec sp_CreateTransformMap @T, 2, @SL, 'Name', @TL, 'Name', @M
exec sp_CreateTransformMap @T, 2, @SL, 'Address', @TL, 'Address', @M
exec sp_CreateTransformMap @T, 2, @SL, 'City1', @TL, 'City', @M
exec sp_CreateTransformMap @T, 2, @SL, 'State', @TL, 'State', 'Overwrite'
exec sp_CreateTransformMap @T, 2, @SL, 'Zip', @TL, 'Zip', @M
go

-- Upload AddressData2 text to Address2 table.
declare @T varchar(60) = 'UploadTable2'; -- TaskTransformName
declare @SL varchar(60) = 'AddressTextLayout'; -- SourceLayoutName
declare @TL varchar(60) = 'AddressTableLayout'; -- TargetLayoutName
declare @M varchar(60) = 'Merge'; -- MapTypeName
exec sp_CreateTransformMap @T, 1, @SL, 'SSN', @TL, 'SSN', @M
exec sp_CreateTransformMap @T, 2, @SL, 'Name', @TL, 'Name', @M
exec sp_CreateTransformMap @T, 2, @SL, 'Address', @TL, 'Address', @M
exec sp_CreateTransformMap @T, 2, @SL, 'City1', @TL, 'City', @M
exec sp_CreateTransformMap @T, 2, @SL, 'State', @TL, 'State', @M
exec sp_CreateTransformMap @T, 2, @SL, 'Zip', @TL, 'Zip', @M
go

-- Merge Address Tables
declare @T varchar(60) = 'MergeTable'; -- TaskTransformName
declare @L varchar(60) = 'AddressTableLayout'; -- LayoutName
declare @M varchar(60) = 'Merge'; -- MapTypeName
exec sp_CreateTransformMap @T, 1, @L, 'SSN', @L, 'SSN', 'InsertInclude'
exec sp_CreateTransformMap @T, 2, @L, 'Name', @L, 'Name', @M
exec sp_CreateTransformMap @T, 3, @L, 'Address', @L, 'Address', @M
exec sp_CreateTransformMap @T, 4, @L, 'City', @L, 'City', @M
exec sp_CreateTransformMap @T, 5, @L, 'State', @L, 'State', 'Overwrite'
exec sp_CreateTransformMap @T, 6, @L, 'Zip', @L, 'Zip', @M
go
