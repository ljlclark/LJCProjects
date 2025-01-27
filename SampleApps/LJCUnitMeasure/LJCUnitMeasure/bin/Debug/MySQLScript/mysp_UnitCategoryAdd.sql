-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- mysp_UnitCategoryAdd.sql
-- MySQL
DELIMITER //

drop procedure if exists mysp_UnitCategoryAdd;

CREATE PROCEDURE mysp_UnitCategoryAdd(
  vCode varchar(5),
  vName varchar(25)
)
BEGIN
  IF (not exists(select Code from UnitCategory
	 where Code = vCode)) then
    insert ignore into UnitCategory (`Code`, `Name`)
	 	 values (vCode, vName);
  END IF;
END;
//
DELIMITER ;