-- Copyright (c) Lester J. Clark 2021 - All Rights Reserved
DELIMITER //

drop procedure if exists sp_CreateUnitSystem;

CREATE PROCEDURE sp_CreateUnitSystem(
  vCode varchar(5),
  vName varchar(25)
)
BEGIN
  if (not exists(select Code from UnitSystem where Code = vCode)) then
    insert ignore into UnitSystem (Code, Name) values(vCode, vName);
  end if;
END //

DELIMITER ;