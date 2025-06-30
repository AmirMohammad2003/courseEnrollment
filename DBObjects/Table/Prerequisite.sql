--<<FileName:Prerequisite.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.Prerequisite') Is Null
    CREATE TABLE [dbo].[Prerequisite] (
    [PrerequisiteID] [bigint] NOT NULL,
	[CourseRef] [bigint] NOT NULL,
	[PrerequisiteCourseRef] [bigint] NOT NULL,
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

If not Exists (select 1 from sys.objects where name = 'PK_Prerequisite')
    ALTER TABLE [dbo].[Prerequisite] ADD  CONSTRAINT [PK_Prerequisite] PRIMARY KEY CLUSTERED 
    (
	    [PrerequisiteID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'FK_Prerequisite_CourseRef')
    ALTER TABLE [dbo].[Prerequisite]  ADD  CONSTRAINT [FK_Prerequisite_CourseRef] FOREIGN KEY(CourseRef)
    REFERENCES [dbo].[Course] ([CourseID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_Prerequisite_PrerequisiteCourseRef')
    ALTER TABLE [dbo].[Prerequisite]  ADD  CONSTRAINT [FK_Prerequisite_PrerequisiteCourseRef] FOREIGN KEY(PrerequisiteCourseRef)
    REFERENCES [dbo].[Course] ([CourseID])
GO

--<< DROP OBJECTS >>--
