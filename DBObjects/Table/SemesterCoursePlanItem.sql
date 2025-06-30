--<<FileName:SemesterCoursePlanItem.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.SemesterCoursePlanItem') Is Null
    CREATE TABLE [dbo].[SemesterCoursePlanItem] (
    [SemesterCoursePlanItemID] [bigint] NOT NULL,
	[SemesterCoursePlanRef] [bigint] NOT NULL,
	[PartyRef] [bigint] NOT NULL,
	[CourseRef] [bigint] NOT NULL,
    [Capacity] [int] NOT NULL,
	[Taken] [int] NOT NULL,
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

If not Exists (select 1 from sys.objects where name = 'PK_SemesterCoursePlanItem')
    ALTER TABLE [dbo].[SemesterCoursePlanItem] ADD  CONSTRAINT [PK_SemesterCoursePlanItem] PRIMARY KEY CLUSTERED 
    (
	    [SemesterCoursePlanItemID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--



If not Exists (select 1 from sys.objects where name = 'FK_SemesterCoursePlaItemn_PartyRef')
    ALTER TABLE [dbo].[SemesterCoursePlanItem]  ADD  CONSTRAINT [FK_SemesterCoursePlanItem_PartyRef] FOREIGN KEY(PartyRef)
    REFERENCES [GNR3].[Party] ([PartyID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_SemesterCoursePlanItem_SemesterRef')
    ALTER TABLE [dbo].[SemesterCoursePlanItem]  ADD  CONSTRAINT [FK_SemesterCoursePlanItem_SemesterRef] FOREIGN KEY(SemesterCoursePlanRef)
    REFERENCES [dbo].[SemesterCoursePlan] ([SemesterCoursePlanID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_SemesterCoursePlanItem_CourseRef')
    ALTER TABLE [dbo].[SemesterCoursePlanItem]  ADD  CONSTRAINT [FK_SemesterCoursePlanItem_CourseRef] FOREIGN KEY(CourseRef)
    REFERENCES [dbo].[Course] ([CourseID])
GO
--<< DROP OBJECTS >>--