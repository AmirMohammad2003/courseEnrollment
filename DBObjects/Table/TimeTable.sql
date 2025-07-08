--<<FileName:TimeTable.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

IF Object_ID('dbo.TimeTable') IS NULL
    CREATE TABLE [dbo].[TimeTable] (
    [TimeTableID] [bigint] NOT NULL,
	[SemesterCoursePlanItemRef] [bigint] NOT NULL,
    [Start] [int] NOT NULL,
    [End] [int] NOT NULL,
    [DayOfTheWeek] [int] NOT NULL,
    [Version] [timestamp] NOT NULL
    ) ON [PRIMARY]
GO
--TEXTIMAGE_ON [SG_LOBData]
--When a table has text, ntext, image, varchar(max), nvarchar(max), varbinary(max), xml or large user defined type columns uncomment above code

--<< ADD CLOLUMNS >>--

--<<Sample>>--
/*if not exists (select 1 from sys.columns where object_id=object_id('GNR3.Project') and
				[name] = 'ColumnName')
begin
    Alter table GNR3.Project Add ColumnName DataType Nullable
end
GO*/

--<< ALTER COLUMNS >>--

--<< PRIMARYKEY DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'PK_TimeTable')
    ALTER TABLE [dbo].[TimeTable] ADD  CONSTRAINT [PK_TimeTable] PRIMARY KEY CLUSTERED 
    (
	    [TimeTableID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'FK_TimeTable_SemesterCoursePlanItemRef')
    ALTER TABLE [dbo].[TimeTable]  ADD  CONSTRAINT [FK_TimeTable_SemesterCoursePlanItemRef] FOREIGN KEY(SemesterCoursePlanItemRef)
    REFERENCES [dbo].[SemesterCoursePlanItem] ([SemesterCoursePlanItemID])
GO

--<< DROP OBJECTS >>--
