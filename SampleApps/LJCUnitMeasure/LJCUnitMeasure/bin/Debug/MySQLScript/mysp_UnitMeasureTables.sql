-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- mysp_UnitMeasureTables.sql
DELIMITER //
CREATE PROCEDURE `mysp_UnitMeasureTables` (
)
BEGIN
call `mysp_UnitCategory`();
call `mysp_UnitSysem`();
call `mysp_UnitMeasure`();
call `mysp_UnitConversion`();
END//
DELIMITER ;