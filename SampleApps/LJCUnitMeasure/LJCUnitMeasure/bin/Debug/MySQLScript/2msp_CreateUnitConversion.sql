-- Copyright (c) Lester J. Clark 2021 - All Rights Reserved
DELIMITER //

drop procedure if exists sp_CreateUnitConversion;

CREATE PROCEDURE sp_CreateUnitConversion(
  vFromUnitCode varchar(5),
  vToUnitCode varchar(5),
  vExpression varchar(25)
)
BEGIN
  set @FromID = (select ID from UnitMeasure where Code = vFromUnitCode);
  set @ToID = (select ID from UnitMeasure where Code = vToUnitCode);

  if (@FromID > 0 and @ToID > 0) then
    if (not exists (select 1 from UnitConversion
    where FromUnitMeasureID = @FromID and ToUnitMeasureID = @ToID)) then
      insert into UnitConversion
        (FromUnitMeasureID, ToUnitMeasureID, Expression)
        values(@FromID, @ToID, vExpression);
    end if;
  end if;     
END //

DELIMITER ;