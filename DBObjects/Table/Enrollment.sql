--<<FileName:Enrollment.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.Enrollment') Is Null
    CREATE TABLE [dbo].[Enrollment] (
    [EnrollmentID] [bigint] NOT NULL,
	[SemesterCoursePlanRef] [bigint] NOT NULL,
	[PartyRef] [bigint] NOT NULL,
    [State] [int] NOT NULL,
    [GPA] [float] NULL,
    [Version] [timestamp] NOT NULL,
    [Creator] [bigint] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifier] [bigint] NOT NULL,
	[LastModificationDate] [datetime] NOT NULL,
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

If not Exists (select 1 from sys.objects where name = 'PK_Enrollment')
    ALTER TABLE [dbo].[Enrollment] ADD  CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
    (
	    [EnrollmentID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'FK_Enrollment_SemesterCoursePlanRef')
    ALTER TABLE [dbo].[Enrollment]  ADD  CONSTRAINT [FK_Enrollment_SemesterCoursePlanRef] FOREIGN KEY(SemesterCoursePlanRef)
    REFERENCES [dbo].[SemesterCoursePlan] ([SemesterCoursePlanID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_Enrollment_PartyRef')
    ALTER TABLE [dbo].[Enrollment]  ADD  CONSTRAINT [FK_Enrollment_PartyRef] FOREIGN KEY(PartyRef)
    REFERENCES [GNR3].[Party] ([PartyID])
GO

--<< DROP OBJECTS >>--
