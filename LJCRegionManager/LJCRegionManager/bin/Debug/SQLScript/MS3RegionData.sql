USE [LJCData]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (select ID from Region where Number = 'IX')
insert into Region
 (Number, Name, Description)
 values('IX', 'Zamboanga Peninsula', null);

IF NOT EXISTS (select ID from Region where Number = 'X')
insert into Region
 (Number, Name, Description)
 values('X', 'Northern Mindanao', null);

IF NOT EXISTS (select ID from Region where Number = 'XI')
insert into Region
 (Number, Name, Description)
 values('XI', 'Davao Region', null);

IF NOT EXISTS (select ID from Region where Number = 'XII')
insert into Region
 (Number, Name, Description)
 values('XII', 'Soccsksargen', null);

IF NOT EXISTS (select ID from Region where Number = 'XIII')
insert into Region
 (Number, Name, Description)
 values('XIII', 'Caraga Region', null);

IF NOT EXISTS (select ID from Region where Number = 'ARMM')
insert into Region
 (Number, Name, Description)
 values('ARMM', 'Autonomous Region in Muslim Mindanao', null);
GO
