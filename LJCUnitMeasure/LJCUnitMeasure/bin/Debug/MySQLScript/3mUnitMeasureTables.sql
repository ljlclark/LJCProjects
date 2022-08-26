-- Copyright (c) Lester J. Clark 2021 - All Rights Reserved
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
CREATE TABLE if not exists `testdata`.`UnitCategory` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Code` varchar(5) NOT NULL,
  `Name` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`ID`),
  unique index `UKUnitCategory`
    (`Name` asc) visible
);

call sp_CreateUnitCategory('All', 'All');
call sp_CreateUnitCategory('Len', 'Length');
call sp_CreateUnitCategory('temp', 'Temperature');
call sp_CreateUnitCategory('v', 'Volume');
call sp_CreateUnitCategory('wt', 'Weight');

-- **************
CREATE TABLE if not exists `testdata`.`UnitSystem` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `Code` varchar(5) NOT NULL,
  `Name` VARCHAR(30) NOT NULL,
  PRIMARY KEY (`ID`),
  unique index `UKUnitSystem`
    (`Name` asc) visible
);

call sp_CreateUnitSystem('A', 'Australian');
call sp_CreateUnitSystem('B', 'Imperial');
call sp_CreateUnitSystem('I', 'International');
call sp_CreateUnitSystem('M', 'Metric');
call sp_CreateUnitSystem('US', 'US Units');

-- **************
CREATE TABLE if not exists `testdata`.`UnitMeasure` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `UnitCategoryID` int NOT NULL,
  `UnitSystemID` int NOT NULL,
  `Code` varchar(5) NOT NULL,
  `Name` VARCHAR(30) NOT NULL,
  `AltName` VARCHAR(30) NULL,
  `Sequence` int NOT NULL,
  `Description` VARCHAR(40) NULL,
  PRIMARY KEY (`ID`),
  unique index `UKUnitMeasure`
    (`Name` asc) visible
);

-- ***************
-- Length - Metric
set @UnitCategoryCode = 'Len';
set @UnitSystemCode = 'M';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'km', 'kilometer', null, '1000 meters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'hm', 'hectometer', null, '100 meters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dam', 'decameter', 'dekameter', '10 meters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'm', 'meter', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dm', 'decimeter', null, '10th meter');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'cm', 'centimeter', null, '100th meter');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'mm', 'millimeter', null, '1000th meter');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'um', 'micrometer', 'micron', 'Millionth meter');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'nm', 'nanometer', 'millimicrometer', 'Billionth meter');

-- -----------
-- Length - US
set @UnitCategoryCode = 'Len';
set @UnitSystemCode = 'US';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'mi', 'mile', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'yd', 'yard', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'ft', 'foot', '''', null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'in', 'inch', '''''', null);

-- ***************************
-- Temperature - International
set @UnitCategoryCode = 'temp';
set @UnitSystemCode = 'I';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'K', 'Kelvin', null, null);

-- --------------------
-- Temperature - Metric
set @UnitCategoryCode = 'temp';
set @UnitSystemCode = 'M';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dC', 'Celsius', null, null);

-- ----------------
-- Temperature - US
set @UnitCategoryCode = 'temp';
set @UnitSystemCode = 'US';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dF', 'Fahrenheit', null, null);

-- *****************
-- Volumn - Imperial
set @UnitCategoryCode = 'v';
set @UnitSystemCode = 'B';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'bgal', 'imperial gallon', null, '4.54609 liters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'bqt', 'imperial quart', null, '4th imperial gallon');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'bpnt', 'imperial pint', null, 'half imperial quart');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'bc', 'imperial cup', null, 'half imperial pint');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'bgil', 'imperial gill', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'bfloz', 'imperial fluid ounce', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'btbs', 'imperial tablespoon', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'btsp', 'imperial teaspoon', null, null);

-- ---------------
-- Volumn - Metric
set @UnitCategoryCode = 'v';
set @UnitSystemCode = 'M';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'kl', 'kiloliter', null, '1000 liters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'hl', 'hectoliter', null, '100 liters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dal', 'decaliter', null, '10 liters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'L', 'liter', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dl', 'deciliter', null, '10th liter');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'cl', 'centiliter', null, '100th liter');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'ml', 'milliliter', null, '1000th liter');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'ul', 'microliter', null, 'Millionth liter');

-- -----------
-- Volumn - US
set @UnitCategoryCode = 'v';
set @UnitSystemCode = 'US';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'gal', 'gallon', null, '3.78541 liters');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'gt', 'quart', null, '4th gallon');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'pnt', 'pint', null, 'half quart');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'c', 'customary cup', null, 'half pint');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'lc', 'legal cup', null, '240ml');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'gil', 'gill', null, 'half cup');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'tbs', 'tablespoon', null, '16th cup');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'floz', 'customary fluid ounce', null, 'half tablespoon');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'tsp', 'teaspoon', null, '3rd tablespoon');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'fdm', 'fluid dram', null, '8th ounce');

-- ***************
-- Weight - Metric
set @UnitCategoryCode = 'wt';
set @UnitSystemCode = 'M';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'mt', 'Metric Ton', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'kg', 'kilogram', null, '1000 grams');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'hg', 'hectogram', null, '100 grams');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dag', 'decagram', null, '10 grams');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'g', 'gram', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'dg', 'decigram', null, '10th gram');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'cg', 'centigram', null, '100th gram');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'mg', 'milligram', null, '1000th gram');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'ug', 'microgram', null, 'Millionth gram');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'ng', 'nanogram', 'micromilligram', 'Billionth gram');

-- -----------
-- Weight - US
set @UnitCategoryCode = 'wt';
set @UnitSystemCode = 'US';
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'T', 'Short Ton', null, '2000 pounds');
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'lb', 'pound', null, null);
call sp_CreateUnitMeasure(@UnitCategoryCode, @UnitSystemCode,
  'oz', 'ounce', null, '16th pound');

-- **************
CREATE TABLE if not exists `testdata`.`UnitConversion` (
  `FromUnitMeasureID` int NOT NULL,
  `ToUnitMeasureID` int NOT NULL,
  `Expression` varchar(25) NOT NULL,
  PRIMARY KEY (`FromUnitMeasureID`, `ToUnitMeasureID`)
);

-- -----------
-- Length - US
call sp_CreateUnitConversion('mi', 'yd', '{0} * 1760');
call sp_CreateUnitConversion('yd', 'mi', '{0} * 0.000568');

call sp_CreateUnitConversion('mi', 'ft', '{0} * 5280');
call sp_CreateUnitConversion('ft', 'mi', '{0} * 0.000189');

call sp_CreateUnitConversion('yd', 'ft', '{0} * 3');
call sp_CreateUnitConversion('ft', 'yd', '{0} * 0.333333');

-- --------------------
-- Length - US - Metric
call sp_CreateUnitConversion('mi', 'km', '{0} * 1.609344');
call sp_CreateUnitConversion('km', 'mi', '{0} * 0.621371');

call sp_CreateUnitConversion('yd', 'm', '{0} * 0.9144');
call sp_CreateUnitConversion('m', 'yd', '{0} * 1.093613');

call sp_CreateUnitConversion('ft', 'm', '{0} * 0.3048');
call sp_CreateUnitConversion('m', 'ft', '{0} * 3.28084');

call sp_CreateUnitConversion('in', 'cm', '{0} * 2.54');
call sp_CreateUnitConversion('cm', 'in', '{0} * 0.393701');

-- -------------------------
-- Temperature - US - Metric
call sp_CreateUnitConversion('dF', 'dC', '({0}-32)*5/9');
call sp_CreateUnitConversion('dC', 'dF', '({0}*9/5)+32');

-- -----------
-- Volumn - US
call sp_CreateUnitConversion('gal', 'qt', '{0} * 4');
call sp_CreateUnitConversion('qt', 'gal', '{0} * 0.25');

call sp_CreateUnitConversion('qt', 'pnt', '{0} * 2');
call sp_CreateUnitConversion('pnt', 'qt', '{0} * 0.5');

call sp_CreateUnitConversion('qt', 'c', '{0} * 4');
call sp_CreateUnitConversion('c', 'qt', '{0} * 0.25');

call sp_CreateUnitConversion('c', 'tbs', '{0} * 16');
call sp_CreateUnitConversion('tbs', 'c', '{0} * 0.0625');

call sp_CreateUnitConversion('c', 'floz', '{0} * 8');
call sp_CreateUnitConversion('floz', 'c', '{0} * 0.125');

call sp_CreateUnitConversion('tbs', 'tsp', '{0} * 3');
call sp_CreateUnitConversion('tsp', 'tbs', '{0} * 0.333333');

call sp_CreateUnitConversion('tbs', 'floz', '{0} * 0.5');
call sp_CreateUnitConversion('floz', 'tbs', '{0} * 2');

call sp_CreateUnitConversion('tsp', 'floz', '{0} * 0.166667');
call sp_CreateUnitConversion('floz', 'tsp', '{0} * 6');

-- --------------------
-- Volumn - US - Metric
call sp_CreateUnitConversion('gal', 'L', '{0} * 3.785412');
call sp_CreateUnitConversion('L', 'gal', '{0} * 0.264172');

call sp_CreateUnitConversion('qt', 'L', '{0} * 0.946353');
call sp_CreateUnitConversion('L', 'qt', '{0} * 1.056688');

call sp_CreateUnitConversion('tbs', 'ml', '{0} * 14.78676');
call sp_CreateUnitConversion('ml', 'tbs', '{0} * 0.067628');

call sp_CreateUnitConversion('tsp', 'ml', '{0} * 4.928922');
call sp_CreateUnitConversion('ml', 'tsp', '{0} * 0.202884');

call sp_CreateUnitConversion('floz', 'ml', '{0} * 29.57353');
call sp_CreateUnitConversion('ml', 'floz', '{0} * 0.033814');

-- ***********
-- Weight - US

-- --------------------
-- Weight - US - Metric
