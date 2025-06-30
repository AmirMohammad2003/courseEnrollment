--<<FileName:SemesterCoursePlan.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.SemesterCoursePlan') Is Null
    CREATE TABLE [dbo].[SemesterCoursePlan] (
    [SemesterCoursePlanID] [bigint] NOT NULL,
	[SemesterRef] [bigint] NOT NULL,
    [MajorRef] [bigint] NOT NULL,
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

If not Exists (select 1 from sys.objects where name = 'PK_SemesterCoursePlan')
    ALTER TABLE [dbo].[SemesterCoursePlan] ADD CONSTRAINT [PK_SemesterCoursePlan] PRIMARY KEY CLUSTERED 
    (
	    [SemesterCoursePlanID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'UQ_SemesterCoursePlan_SemesterRef_MajorRef')
    ALTER TABLE [dbo].[SemesterCoursePlan] ADD CONSTRAINT UQ_SemesterCoursePlan_SemesterRef_MajorRef
        UNIQUE (SemesterRef, MajorRef)   
GO


--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--



If not Exists (select 1 from sys.objects where name = 'FK_SemesterCoursePlan_MajorRef')
    ALTER TABLE [dbo].[SemesterCoursePlan]  ADD CONSTRAINT [FK_SemesterCoursePlan_MajorRef] FOREIGN KEY(MajorRef)
    REFERENCES [dbo].[Major] ([MajorID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_SemesterCoursePlan_SemesterRef')
    ALTER TABLE [dbo].[SemesterCoursePlan]  ADD CONSTRAINT [FK_SemesterCoursePlan_SemesterRef] FOREIGN KEY(SemesterRef)
    REFERENCES [dbo].[Semester] ([SemesterID])
GO

--<< DROP OBJECTS >>--