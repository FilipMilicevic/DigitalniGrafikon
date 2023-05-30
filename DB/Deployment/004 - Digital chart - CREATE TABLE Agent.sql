USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[Agent]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [Agent]'
	DROP TABLE [Agent]
END
GO

PRINT 'CREATE Table [Agent]'
GO

CREATE TABLE [Agent](
	[AgentId] [int] IDENTITY(1,1) NOT NULL,
	[AgentCode] varchar(20),
	[DomainName] varchar(10),
	[AgentName] varchar(50),
	[AgentSurname] varchar(50),
	[AgentSecret] varchar(50),
	[AgentTypeId] int,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [Agent] ADD  CONSTRAINT [Agent_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [Agent] ADD  CONSTRAINT [Agent_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [Agent] ADD  CONSTRAINT [Agent_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [Agent] ADD  CONSTRAINT [Agent_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO

GO


