-- Copyright (c) Lester J. Clark 2021 - All Rights Reserved
DELIMITER //

drop procedure if exists sp_CreateUnitMeasure;

CREATE PROCEDURE sp_CreateUnitMeasure(
  vUnitCategoryCode varchar(5),
  vUnitSystemCode varchar(5),
  vCode varchar(5),
  vName varchar(25),
  vAltName varchar(25),
  vDescription varchar(25)
)
BEGIN
  set @UnitCategoryID = (select ID from UnitCategory where Code = vUnitCategoryCode);
  set @UnitSystemID = (select ID from UnitSystem where Code = vUnitSystemCode);
  set @Sequence = (select max(Sequence) from UnitMeasure) + 1;
  if (@Sequence is null) then
    set @Sequence = 1;
  end if;

  if (@UnitCategoryID > 0 and @UnitSystemID > 0) then
    if (not exists (select 1 from UnitMeasure where Code = vCode)) then
      insert into UnitMeasure
        (UnitCategoryID, UnitSystemID, Code, Name, AltName, Sequence, Description)
        values(@UnitCategoryID, @UnitSystemID, vCode, vName, vAltName, @Sequence, vDescription);
    end if;
  end if;
END //

DELIMITER ;