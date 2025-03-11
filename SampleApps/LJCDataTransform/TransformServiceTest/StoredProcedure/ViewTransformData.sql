-- Copyright (c) Lester J Clark 2019 - All Rights Reserved
use DataTransform;

-- Processes and Steps
select * from ProcessGroup;
select * from Process;
select * from ProcessGroupProcess order by ProcessGroupID, Sequence;
select * from Step order by ProcessID, Sequence;

-- Layouts
select * from Layout;
select * from LayoutColumn order by LayoutID, Sequence;

-- Action Sources
select * from Task order by StepID, Sequence;
select * from TaskSource order by TaskID, SourceID;
select * from DataSource;

-- Transform Sources
select * from Task order by StepID, Sequence;
select * from TaskTransform order by TaskID;
select * from DataSource;

-- Transform Matches
select * from TaskTransform order by TaskID;
select * from TransformMatch order by TransformID, Sequence;
select * from LayoutColumn order by LayoutID, Sequence;

-- Transform Maps
select * from TaskTransform order by TaskID;
select * from TransformMap order by TransformID, Sequence;
select * from LayoutColumn order by LayoutID, Sequence;
