--<<FileName:EnrollmentItem.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.EnrollmentItem') Is Null
    CREATE TABLE [dbo].[EnrollmentItem] (
    [EnrollmentItemID] [bigint] NOT NULL,
	[EnrollmentRef] [bigint] NOT NULL,
    [SemesterCoursePlanItemRef] [bigint] NOT NULL,
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

If not Exists (select 1 from sys.objects where name = 'PK_EnrollmentItem')
    ALTER TABLE [dbo].[EnrollmentItem] ADD  CONSTRAINT [PK_EnrollmentItem] PRIMARY KEY CLUSTERED 
    (
	    [EnrollmentItemID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'FK_EnrollmentItem_EnrollmentRef')
    ALTER TABLE [dbo].[EnrollmentItem]  ADD  CONSTRAINT [FK_EnrollmentItem_EnrollmentRef] FOREIGN KEY(EnrollmentRef)
    REFERENCES [dbo].[Enrollment] ([EnrollmentID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_EnrollmentItem_SemesterCoursePlaItemRef')
    ALTER TABLE [dbo].[EnrollmentItem]  ADD  CONSTRAINT [FK_EnrollmentItem_SemesterCoursePlanItemRef] FOREIGN KEY(SemesterCoursePlanItemRef)
    REFERENCES [dbo].[SemesterCoursePlanItem] ([SemesterCoursePlanItemID])
GO

--<< DROP OBJECTS >>--
