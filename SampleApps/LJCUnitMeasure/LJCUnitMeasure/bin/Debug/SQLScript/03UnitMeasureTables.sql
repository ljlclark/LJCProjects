-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
USE [LJCData]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*
drop table UnitConversion;
drop table UnitMeasure;
drop table UnitSystem;
drop table UnitCategory;
drop procedure sp_CreateUnitConversion;
drop procedure sp_CreateUnitMeasure;
drop procedure sp_CreateUnitSystem;
drop procedure sp_CreateUnitCategory;
*/

-- **************
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UnitCategory')
BEGIN
CREATE TABLE [dbo].[UnitCategory](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Code] [nvarchar](5) NOT NULL,
  [Name] [nvarchar](30) NOT NULL,
  CONSTRAINT [PK_UnitCategory]
  PRIMARY KEY CLUSTERED (
    [ID] ASC)
)
END
GO

exec sp_CreateUnitCategory 'All', 'All';
exec sp_CreateUnitCategory 'Len', 'Length';
exec sp_CreateUnitCategory 'temp', 'Temperature';
exec sp_CreateUnitCategory 'v', 'Volume';
exec sp_CreateUnitCategory 'wt', 'Weight';

-- **************
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UnitSystem')
BEGIN
CREATE TABLE [dbo].[UnitSystem](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [Code] [nvarchar](5) NOT NULL,
  [Name] [nvarchar](30) NOT NULL,
  CONSTRAINT [PK_UnitSystem]
  PRIMARY KEY CLUSTERED (
    [ID] ASC)
)
END
GO

exec sp_CreateUnitSystem 'A', 'Australian';
exec sp_CreateUnitSystem 'B', 'Imperial';
exec sp_CreateUnitSystem 'I', 'International';
exec sp_CreateUnitSystem 'M', 'Metric';
exec sp_CreateUnitSystem 'US', 'US Customary Units';

-- ***************
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UnitMeasure')
BEGIN
CREATE TABLE [dbo].[UnitMeasure](
  [ID] [int] IDENTITY(1,1) NOT NULL,
  [UnitCategoryID] [int] NOT NULL,
  [UnitSystemID] [int] NOT NULL,
  [Code] [nvarchar](5) NOT NULL,
  [Name] [nvarchar](30) NOT NULL,
  [AltName] [nvarchar](30) NULL,
  [Sequence] [int] NOT NULL,
  [Description] [nvarchar](40) NULL,
  CONSTRAINT [PK_UnitMeasure]
  PRIMARY KEY CLUSTERED (
    [ID] ASC)
)
END
GO

declare @UnitCategoryCode varchar(5);
declare @UnitSystemCode varchar(5);

-- ***************
-- Length - Metric
set @UnitCategoryCode = 'Len';
set @UnitSystemCode = 'M';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'km', 'kilometer', null, '1000 meters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'hm', 'hectometer', null, '100 meters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dam', 'decameter', 'dekameter', '10 meters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'm', 'meter', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dm', 'decimeter', null, '10th meter';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'cm', 'centimeter', null, '100th meter';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'mm', 'millimeter', null, '1000th meter';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'um', 'micrometer', 'micron', 'Millionth meter';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'nm', 'nanometer', 'millimicrometer', 'Billionth meter';

-- -----------
-- Length - US
set @UnitCategoryCode = 'Len';
set @UnitSystemCode = 'US';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'mi', 'mile', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'yd', 'yard', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'ft', 'foot', '''', null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'in', 'inch', '''''', null;

-- ***************************
-- Temperature - International
set @UnitCategoryCode = 'temp';
set @UnitSystemCode = 'I';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'K', 'Kelvin', null, null;

-- --------------------
-- Temperature - Metric
set @UnitCategoryCode = 'temp';
set @UnitSystemCode = 'M';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dC', 'Celsius', null, null;

-- ----------------
-- Temperature - US
set @UnitCategoryCode = 'temp';
set @UnitSystemCode = 'US';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dF', 'Fahrenheit', null, null;

-- *****************
-- Volumn - Imperial
set @UnitCategoryCode = 'v';
set @UnitSystemCode = 'B';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'bgal', 'imperial gallon', null, '4.54609 liters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'bqt', 'imperial quart', null, '4th imperial gallon';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'bpnt', 'imperial pint', null, 'half imperial quart';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'bc', 'imperial cup', null, 'half imperial pint';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'bgil', 'imperial gill', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'bfloz', 'imperial fluid ounce', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'btbs', 'imperial tablespoon', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'btsp', 'imperial teaspoon', null, null;

-- ---------------
-- Volumn - Metric
set @UnitCategoryCode = 'v';
set @UnitSystemCode = 'M';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'kl', 'kiloliter', null, '1000 liters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'hl', 'hectoliter', null, '100 liters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dal', 'decaliter', null, '10 liters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'L', 'liter', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dl', 'deciliter', null, '10th liter';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'cl', 'centiliter', null, '100th liter';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'ml', 'milliliter', null, '1000th liter';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'ul', 'microliter', null, 'Millionth liter';

-- -----------
-- Volumn - US
set @UnitCategoryCode = 'v';
set @UnitSystemCode = 'US';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'gal', 'gallon', null, '3.78541 liters';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'gt', 'quart', null, '4th gallon';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'pnt', 'pint', null, 'half quart';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'c', 'customary cup', null, 'half pint';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'lc', 'legal cup', null, '240ml';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'gil', 'gill', null, 'half cup';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'tbs', 'tablespoon', null, '16th cup';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'floz', 'customary fluid ounce', null, 'half tablespoon';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'tsp', 'teaspoon', null, '3rd tablespoon';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'fdm', 'fluid dram', null, '8th ounce';

-- ***************
-- Weight - Metric
set @UnitCategoryCode = 'wt';
set @UnitSystemCode = 'M';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'mt', 'Metric Ton', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'kg', 'kilogram', null, '1000 grams';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'hg', 'hectogram', null, '100 grams';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dag', 'decagram', null, '10 grams';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'g', 'gram', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'dg', 'decigram', null, '10th gram';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'cg', 'centigram', null, '100th gram';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'mg', 'milligram', null, '1000th gram';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'ug', 'microgram', null, 'Millionth gram';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'ng', 'nanogram', 'micromilligram', 'Billionth gram';

-- -----------
-- Weight - US
set @UnitCategoryCode = 'wt';
set @UnitSystemCode = 'US';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'T', 'Short Ton', null, '2000 pounds';
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'lb', 'pound', null, null;
exec sp_CreateUnitMeasure @UnitCategoryCode, @UnitSystemCode,
  'oz', 'ounce', null, '16th pound';

-- **************
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES
 WHERE TABLE_NAME = N'UnitConversion')
BEGIN
CREATE TABLE [dbo].[UnitConversion](
  [FromUnitMeasureID] [int] NOT NULL,
  [ToUnitMeasureID] [int] NOT NULL,
  [Expression] [nvarchar](25) NOT NULL,
  CONSTRAINT [PK_UnitConversion]
  PRIMARY KEY CLUSTERED (
    [FromUnitMeasureID], [ToUnitMeasureID] ASC)
)
END
GO

-- -----------
-- Length - US
exec sp_CreateUnitConversion 'mi', 'yd', '{0} * 1760';
exec sp_CreateUnitConversion 'yd', 'mi', '{0} * 0.000568';

exec sp_CreateUnitConversion 'mi', 'ft', '{0} * 5280';
exec sp_CreateUnitConversion 'ft', 'mi', '{0} * 0.000189';

exec sp_CreateUnitConversion 'yd', 'ft', '{0} * 3';
exec sp_CreateUnitConversion 'ft', 'yd', '{0} * 0.333333';

-- --------------------
-- Length - US - Metric
exec sp_CreateUnitConversion 'mi', 'km', '{0} * 1.609344';
exec sp_CreateUnitConversion 'km', 'mi', '{0} * 0.621371';

exec sp_CreateUnitConversion 'yd', 'm', '{0} * 0.9144';
exec sp_CreateUnitConversion 'm', 'yd', '{0} * 1.093613';

exec sp_CreateUnitConversion 'ft', 'm', '{0} * 0.3048';
exec sp_CreateUnitConversion 'm', 'ft', '{0} * 3.28084';

exec sp_CreateUnitConversion 'in', 'cm', '{0} * 2.54';
exec sp_CreateUnitConversion 'cm', 'in', '{0} * 0.393701';

-- -------------------------
-- Temperature - US - Metric
exec sp_CreateUnitConversion 'dF', 'dC', '({0}-32)*5/9';
exec sp_CreateUnitConversion 'dC', 'dF', '({0}*9/5)+32';

-- -----------
-- Volumn - US
exec sp_CreateUnitConversion 'gal', 'qt', '{0} * 4';
exec sp_CreateUnitConversion 'qt', 'gal', '{0} * 0.25';

exec sp_CreateUnitConversion 'qt', 'pnt', '{0} * 2';
exec sp_CreateUnitConversion 'pnt', 'qt', '{0} * 0.5';

exec sp_CreateUnitConversion 'qt', 'c', '{0} * 4';
exec sp_CreateUnitConversion 'c', 'qt', '{0} * 0.25';

exec sp_CreateUnitConversion 'c', 'tbs', '{0} * 16';
exec sp_CreateUnitConversion 'tbs', 'c', '{0} * 0.0625';

exec sp_CreateUnitConversion 'c', 'floz', '{0} * 8';
exec sp_CreateUnitConversion 'floz', 'c', '{0} * 0.125';

exec sp_CreateUnitConversion 'tbs', 'tsp', '{0} * 3';
exec sp_CreateUnitConversion 'tsp', 'tbs', '{0} * 0.333333';

exec sp_CreateUnitConversion 'tbs', 'floz', '{0} * 0.5';
exec sp_CreateUnitConversion 'floz', 'tbs', '{0} * 2';

exec sp_CreateUnitConversion 'tsp', 'floz', '{0} * 0.166667';
exec sp_CreateUnitConversion 'floz', 'tsp', '{0} * 6';

-- --------------------
-- Volumn - US - Metric
exec sp_CreateUnitConversion 'gal', 'L', '{0} * 3.785412';
exec sp_CreateUnitConversion 'L', 'gal', '{0} * 0.264172';

exec sp_CreateUnitConversion 'qt', 'L', '{0} * 0.946353';
exec sp_CreateUnitConversion 'L', 'qt', '{0} * 1.056688';

exec sp_CreateUnitConversion 'tbs', 'ml', '{0} * 14.78676';
exec sp_CreateUnitConversion 'ml', 'tbs', '{0} * 0.067628';

exec sp_CreateUnitConversion 'tsp', 'ml', '{0} * 4.928922';
exec sp_CreateUnitConversion 'ml', 'tsp', '{0} * 0.202884';

exec sp_CreateUnitConversion 'floz', 'ml', '{0} * 29.57353';
exec sp_CreateUnitConversion 'ml', 'floz', '{0} * 0.033814';

-- ***********
-- Weight - US

-- --------------------
-- Weight - US - Metric
GO
