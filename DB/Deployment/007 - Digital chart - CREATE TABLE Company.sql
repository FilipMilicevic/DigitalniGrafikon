USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[Company]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [Company]'
	DROP TABLE [Company]
END
GO

PRINT 'CREATE Table [Company]'
GO

CREATE TABLE [Company](
	[CompanyId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] varchar(50) null,
	[CompanyStatusId] [int] not null,
	[CompanyOIB] varchar(11) null,
	[CompanyEmail] varchar(50) null,
	[CompanyAddressId] int null,
	[CompanyOwnerName] varchar(30) null,
	[CompanyOwnerSurname] varchar(30) null,
	[CompanyMainContactPhoneNumber] varchar(30) null,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [Company] ADD  CONSTRAINT [Company_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [Company] ADD  CONSTRAINT [Company_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [Company] ADD  CONSTRAINT [Company_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [Company] ADD  CONSTRAINT [Company_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO



GO
