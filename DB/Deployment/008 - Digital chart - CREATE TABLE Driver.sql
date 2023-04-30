USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[Driver]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [Driver]'
	DROP TABLE [Driver]
END
GO

PRINT 'CREATE Table [Driver]'
GO

CREATE TABLE [Driver](
	[DriverId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] not null,
	[DriverName] varchar(30) null,
	[DriverSurname] varchar(30) null,
	[DriverPhoneNumber] varchar(20) null,
	[DriverDispatcherNote] varchar(MAX) null,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [Driver] ADD  CONSTRAINT [Driver_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [Driver] ADD  CONSTRAINT [Driver_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [Driver] ADD  CONSTRAINT [Driver_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [Driver] ADD  CONSTRAINT [Driver_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO



GO
