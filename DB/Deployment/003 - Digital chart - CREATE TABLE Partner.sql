USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[Partner]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [Partner]'
	DROP TABLE [Partner]
END
GO

PRINT 'CREATE Table [Partner]'
GO

CREATE TABLE [Partner](
	[PartnerId] [int] IDENTITY(1,1) NOT NULL,
	[PartnerCode] varchar(20),
	[PartnerDesc] varchar(50),
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PartnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [Partner] ADD  CONSTRAINT [Partner_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [Partner] ADD  CONSTRAINT [Partner_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [Partner] ADD  CONSTRAINT [Partner_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [Partner] ADD  CONSTRAINT [Partner_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO

SET IDENTITY_INSERT [Partner] ON
GO

INSERT [Partner] ([PartnerId], [PartnerCode], [PartnerDesc]) VALUES (1, '47490', 'Kompas Turistièno podjetje d.o.o.')
GO

SET IDENTITY_INSERT [Partner] OFF
GO

GO


