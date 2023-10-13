-- Copyright(c) Lester J. Clark and Contributors.
-- Licensed under the MIT License.
USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (select ID from Province where Name = 'Zamboanga del Norte')
insert into Province
 (RegionID, Name, Description, Abbreviation)
 values('1', 'Zamboanga del Norte', null, 'ZDN');

IF NOT EXISTS (select ID from Province where Name = 'Zamboanga del Sur')
insert into Province
 (RegionID, Name, Description, Abbreviation)
 values('1', 'Zamboanga del Sur', null, 'ZDS');

IF NOT EXISTS (select ID from Province where Name = 'Zamboanga Sibugay')
insert into Province
 (RegionID, Name, Description)
 values('1', 'Zamboanga Sibugay', null);

IF NOT EXISTS (select ID from Province where Name = 'Zamboanga del Sur')
insert into Province
 (RegionID, Name, Description)
 values('1', 'Zamboanga del Sur', null);

IF NOT EXISTS (select ID from Province where Name = 'Bukidnon')
insert into Province
 (RegionID, Name, Description)
 values('2', 'Bukidnon', null);

IF NOT EXISTS (select ID from Province where Name = 'Camiguin')
insert into Province
 (RegionID, Name, Description)
 values('2', 'Camiguin', null);

IF NOT EXISTS (select ID from Province where Name = 'Lanao Del Norte')
insert into Province
 (RegionID, Name, Description)
 values('2', 'Lanao Del Norte', null);

IF NOT EXISTS (select ID from Province where Name = 'Misamis Occidental')
insert into Province
 (RegionID, Name, Description)
 values('2', 'Misamis Occidental', null);

IF NOT EXISTS (select ID from Province where Name = 'Misamis Oriental')
insert into Province
 (RegionID, Name, Description)
 values('2', 'Misamis Oriental', null);

IF NOT EXISTS (select ID from Province where Name = 'Compostela Valley')
insert into Province
 (RegionID, Name, Description)
 values('3', 'Compostela Valley', null);

IF NOT EXISTS (select ID from Province where Name = 'Davao del Norte')
insert into Province
 (RegionID, Name, Description)
 values('3', 'Davao del Norte', null);

IF NOT EXISTS (select ID from Province where Name = 'Davao del Sur')
insert into Province
 (RegionID, Name, Description)
 values('3', 'Davao del Sur', null);

IF NOT EXISTS (select ID from Province where Name = 'Davao Occidental')
insert into Province
 (RegionID, Name, Description)
 values('3', 'Davao Occidental', null);

IF NOT EXISTS (select ID from Province where Name = 'Davao Oriental')
insert into Province
 (RegionID, Name, Description)
 values('3', 'Davao Oriental', null);

IF NOT EXISTS (select ID from Province where Name = 'Cotabato')
insert into Province
 (RegionID, Name, Description)
 values('4', 'Cotabato', null);

IF NOT EXISTS (select ID from Province where Name = 'Sarangani')
insert into Province
 (RegionID, Name, Description)
 values('4', 'Sarangani', null);

IF NOT EXISTS (select ID from Province where Name = 'South Cotabato')
insert into Province
 (RegionID, Name, Description)
 values('4', 'South Cotabato', null);

IF NOT EXISTS (select ID from Province where Name = 'Sultan Kudarat')
insert into Province
 (RegionID, Name, Description)
 values('4', 'Sultan Kudarat', null);

IF NOT EXISTS (select ID from Province where Name = 'Agusan del Norte')
insert into Province
 (RegionID, Name, Description)
 values('5', 'Agusan del Norte', null);

IF NOT EXISTS (select ID from Province where Name = 'Agusan del Sur')
insert into Province
 (RegionID, Name, Description)
 values('5', 'Agusan del Sur', null);

IF NOT EXISTS (select ID from Province where Name = 'Suriago del Norte')
insert into Province
 (RegionID, Name, Description)
 values('5', 'Suriago del Norte', null);

IF NOT EXISTS (select ID from Province where Name = 'Suriago del Sur')
insert into Province
 (RegionID, Name, Description)
 values('5', 'Suriago del Sur', null);

IF NOT EXISTS (select ID from Province where Name = 'Dinagat Islands')
insert into Province
 (RegionID, Name, Description)
 values('5', 'Dinagat Islands', null);

IF NOT EXISTS (select ID from Province where Name = 'Basilan')
insert into Province
 (RegionID, Name, Description)
 values('6', 'Basilan', null);

IF NOT EXISTS (select ID from Province where Name = 'Lanao del Sur')
insert into Province
 (RegionID, Name, Description)
 values('6', 'Lanao del Sur', null);

IF NOT EXISTS (select ID from Province where Name = 'Maquindanao')
insert into Province
 (RegionID, Name, Description)
 values('6', 'Maquindanao', null);

IF NOT EXISTS (select ID from Province where Name = 'Sulu')
insert into Province
 (RegionID, Name, Description)
 values('6', 'Sulu', null);

IF NOT EXISTS (select ID from Province where Name = 'Tawi-Tawi')
insert into Province
 (RegionID, Name, Description)
 values('6', 'Tawi-Tawi', null);
GO
