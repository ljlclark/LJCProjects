-- Copyright (c) Lester J. Clark 2021 - All Rights Reserved
DELIMITER //

drop procedure if exists sp_CreateUnitCategory;

CREATE PROCEDURE sp_CreateUnitCategory(
  vCode varchar(5),
  vName varchar(25)
)
BEGIN
  if (not exists(select Code from UnitCategory where Code = vCode)) then
    insert ignore into UnitCategory (Code, Name) values(vCode, vName);
  end if;
END //

DELIMITER ;