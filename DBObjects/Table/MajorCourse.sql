--<<FileName:MajorCourse.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.MajorCourse') Is Null
    CREATE TABLE [dbo].[MajorCourse] (
    [MajorCourseID] [bigint] NOT NULL,
	[MajorRef] [bigint] NOT NULL,
	[CourseRef] [bigint] NOT NULL,
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

If not Exists (select 1 from sys.objects where name = 'PK_MajorCourse')
    ALTER TABLE [dbo].[MajorCourse] ADD  CONSTRAINT [PK_MajorCourse] PRIMARY KEY CLUSTERED 
    (
	    [MajorCourseID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'FK_MajorCourse_MajorRef')
    ALTER TABLE [dbo].[MajorCourse] ADD  CONSTRAINT [FK_MajorCourse_MajorRef] FOREIGN KEY(MajorRef)
    REFERENCES [dbo].[Major] ([MajorID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_MajorCourse_CourseRef')
    ALTER TABLE [dbo].[MajorCourse] ADD  CONSTRAINT [FK_MajorCourse_CourseRef] FOREIGN KEY(CourseRef)
    REFERENCES [dbo].[Course] ([CourseID])
GO

--<< DROP OBJECTS >>--
