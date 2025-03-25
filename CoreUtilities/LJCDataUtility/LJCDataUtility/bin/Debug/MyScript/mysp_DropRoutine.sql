-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- mysp_DropRoutine.sql
-- call mysp_DropRoutine('mysp_DropConstraint', 'p');
delimiter $$
drop procedure if exists `TestData`.`mysp_DropRoutine`;$$
create procedure `TestData`.`mysp_DropRoutine` (
  in routineName varchar(64),
  in routineType varchar(1)
)
begin
  declare typeName varchar(20);
  if (myf_IsRoutine(routineName, routineType)) then
    set typeName = 'procedure';
    if (routinetype = 'f') then
      set typeName = 'function';
    end if;
  
    set @drop = concat(
      'drop ', typeName, ' ', routineName,
      ';');
    prepare statement from @drop;
    execute statement;
    deallocate prepare statement;
  end if;
end$$
delimiter ;
