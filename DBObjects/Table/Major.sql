--<<FileName:Course.sqlبخاطر یونیکد>>--
--<< TABLE DEFINITION >>-- 

If Object_ID('dbo.Major') Is Null
    CREATE TABLE [dbo].[Major](
	    [MajorID] [bigint] NOT NULL,
	    [Name] [nvarchar](max) NOT NULL,
        [Units] [int] NOT NULL,
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

If not Exists (select 1 from sys.objects where name = 'PK_Major')
    ALTER TABLE [dbo].[Major] ADD  CONSTRAINT [PK_Major] PRIMARY KEY CLUSTERED 
    (
	    [MajorID] ASC
    ) ON [Primary]
GO

--<< DEFAULTS CHECKS DEFINITION >>--

--<< RULES DEFINITION >>--

--<< INDEXES DEFINITION >>--

--<< FOREIGNKEYS DEFINITION >>--

--<< DROP OBJECTS >>--
