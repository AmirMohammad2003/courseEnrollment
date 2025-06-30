--<<FileName:Enrollment.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.PartyMajor') Is Null
    CREATE TABLE [dbo].[PartyMajor] (
    [PartyMajorID] [bigint] NOT NULL,
	[MajorRef] [bigint] NOT NULL,
	[PartyRef] [bigint] NOT NULL,
    [ProfessorPartyRef] [bigint] NULL,
    [GPA] [float] NULL,
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
if not exists (select 1 from sys.columns where object_id=object_id('dbo.PartyMajor') and
				[name] = 'ProfessorPartyRef')
begin
    Alter table dbo.PartyMajor Add ProfessorPartyRef bigint NULL
end
GO
--<< ALTER COLUMNS >>--

--<< PRIMARYKEY DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'PK_PartyMajor')
    ALTER TABLE [dbo].[PartyMajor] ADD  CONSTRAINT [PK_PartyMajor] PRIMARY KEY CLUSTERED 
    (
	    [PartyMajorID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

If not Exists (select 1 from sys.objects where name = 'FK_PartyMajor_MajorRef')
    ALTER TABLE [dbo].[PartyMajor]  ADD  CONSTRAINT [FK_PartyMajor_MajorRef] FOREIGN KEY(MajorRef)
    REFERENCES [dbo].[Major] ([MajorID])
GO

If not Exists (select 1 from sys.objects where name = 'FK_PartyMajor_PartyRef')
    ALTER TABLE [dbo].[PartyMajor]  ADD  CONSTRAINT [FK_PartyMajor_PartyRef] FOREIGN KEY(PartyRef)
    REFERENCES [GNR3].[Party] ([PartyID])
GO

--<< DROP OBJECTS >>--
