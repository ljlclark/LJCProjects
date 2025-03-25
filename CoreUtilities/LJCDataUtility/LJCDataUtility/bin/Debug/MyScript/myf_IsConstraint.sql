-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- myf_IsConstraint.sql
-- select myf_IsConstraint('UnitMeasure', 'uq_UnitMeasure_Name');
delimiter $$
drop function if exists `TestData`.`myf_IsConstraint`;$$
create function `TestData`.`myf_IsConstraint`(
  tableName varchar(30),
  constraintName varchar(30)
)
returns bool
deterministic
begin
declare result bool;
set result = 0;
if exists (
  select 1
  from Information_Schema.Table_Constraints
  where Table_Name = tableName
    and Constraint_Name = constraintName) then
  set result = 1;
end if;
return result;
end$$
delimiter ;
