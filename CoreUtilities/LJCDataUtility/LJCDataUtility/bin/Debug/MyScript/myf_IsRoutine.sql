-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- myf_IsRoutine.sql
-- select myf_IsRoutine('myf_IsConstraint', 'f');
delimiter $$
drop function if exists `TestData`.`myf_IsRoutine`; $$
create function `TestData`.`myf_IsRoutine`(
  routineName varchar(30),
  routineType varchar(1)
)
returns bool
deterministic
begin
declare result bool;
declare routineTypeName varchar(20);
set routineTypeName = 'procedure';
if (routineType = 'f') then
  set routineTypeName = 'function';
end if;
set result = 0;
if exists (
  select 1
  from Information_Schema.Routines
  where Routine_Name = routineName
    and Routine_Type = routineTypeName)
then
  set result = 1;
end if;
return result;
end$$
delimiter ;
