-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
-- mysp_DropConstraint.sql
-- call mysp_DropConstraint('UnitMeasure', 'uq_UnitMeasure_Name');
delimiter $$
drop procedure if exists `TestData`.`mysp_DropConstraint`;$$
create procedure `TestData`.`mysp_DropConstraint` (
  in tableName varchar(30),
  in constraintName varchar(30)
)
begin
if (myf_IsConstraint(tableName, constraintName))
then
  set @drop = concat(
    'alter table ', tableName,
    ' drop constraint ', constraintName,
    ';');
  prepare statement from @drop;
  execute statement;
  deallocate PREPARE statement;
end if;
end$$
delimiter ;
