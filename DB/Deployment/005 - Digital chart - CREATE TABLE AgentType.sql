USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[AgentType]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [AgentType]'
	DROP TABLE [AgentType]
END
GO

PRINT 'CREATE Table [AgentType]'
GO

CREATE TABLE [AgentType](
	[AgentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AgentTypeDesc] varchar(50),
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AgentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [AgentType] ADD  CONSTRAINT [AgentType_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [AgentType] ADD  CONSTRAINT [AgentType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [AgentType] ADD  CONSTRAINT [AgentType_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [AgentType] ADD  CONSTRAINT [AgentType_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO

SET IDENTITY_INSERT [AgentType] ON
GO

INSERT [AgentType] ([AgentTypeId], [AgentTypeDesc]) VALUES (1, 'Admin')
GO
INSERT [AgentType] ([AgentTypeId], [AgentTypeDesc]) VALUES (2, 'Dispatcher')
GO
INSERT [AgentType] ([AgentTypeId], [AgentTypeDesc]) VALUES (3, 'Operator')
GO
INSERT [AgentType] ([AgentTypeId], [AgentTypeDesc]) VALUES (4, 'Other')
GO

SET IDENTITY_INSERT [AgentType] OFF
GO

GO


