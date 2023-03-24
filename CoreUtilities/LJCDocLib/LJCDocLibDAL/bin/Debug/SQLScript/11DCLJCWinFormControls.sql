/* 11DCLJCWinFormControls.sql */
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
select
  ID, DocClassGroupID, Name, Description, Sequence
from DocClass;
*/

declare @assemblyName nvarchar(60) = 'LJCWinFormControls';
declare @headingName nvarchar(60) = 'Combobox';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCItem'
  , 'Represents an LJCItemCombo Item.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCItemCombo'
  , 'Provides custom functionality for a ComboBox control. (R)', 2;

set @headingName = 'DataGrid';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCHelper'
  , 'Provides methods for setting a complex list control when AutoScaleMode.Font is used.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCDataGrid'
  , 'Provides custom functionality for a DataGridView control. (D)', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCGridRow'
  , 'Provides custom functionality for a DataGridViewRow control.', 3;

set @headingName = 'Tab';
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCTabControl'
  , 'Provides custom drag and drop functionality for a TabControl.', 1;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'LJCTabPanel'
  , 'A Tab control in a panel.', 2;
exec sp_DCAddUnique @assemblyName, @headingName
  , 'PanelControlsAdjust'
  , 'Contains standard panel control adjustment values.', 3;
