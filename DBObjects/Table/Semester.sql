--<<FileName:Semester.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.Semester') Is Null
    CREATE TABLE [dbo].[Semester](
	[SemesterID] [bigint] NOT NULL,
    [Name] [nvarchar](50) NOT NULL,
    [StartDate] [datetime] NULL,
    [EndDate] [datetime] NULL,
    [EnrollmentStartTime] [datetime] NULL,
    [EnrollmentEndTime] [datetime] NULL,
    [State] [int] NOT NULL,
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
if not exists (select 1 from sys.columns where object_id=object_id('dbo.Semester') and
				[name] = 'State')
begin
    Alter table dbo.Semester Add [State] [int] NOT NULL
end
GO
--<< ALTER COLUMNS >>--

--<< PRIMARYKEY DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'PK_Semester')
    ALTER TABLE [dbo].[Semester] ADD  CONSTRAINT [PK_Semester] PRIMARY KEY CLUSTERED 
    (
	    [SemesterID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

--<< DROP OBJECTS >>--
