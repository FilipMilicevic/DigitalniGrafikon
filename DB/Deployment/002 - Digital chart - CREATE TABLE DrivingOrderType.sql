USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[DrivingOrderType]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [DrivingOrderType]'
	DROP TABLE [DrivingOrderType]
END
GO

PRINT 'CREATE Table [DrivingOrderType]'
GO

CREATE TABLE [DrivingOrderType](
	[DrivingOrderTypeId] [int] IDENTITY(1,1) NOT NULL,
	[DrivingOrderTypeDesc] varchar(50),
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DrivingOrderTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


GO

ALTER TABLE [DrivingOrderType] ADD  CONSTRAINT [DrivingOrderType_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [DrivingOrderType] ADD  CONSTRAINT [DrivingOrderType_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [DrivingOrderType] ADD  CONSTRAINT [DrivingOrderType_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [DrivingOrderType] ADD  CONSTRAINT [DrivingOrderType_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO

SET IDENTITY_INSERT [DrivingOrderType] ON
GO

INSERT [DrivingOrderType] ([DrivingOrderTypeId], [DrivingOrderTypeDesc]) VALUES (1, 'Dolazni')
GO
INSERT [DrivingOrderType] ([DrivingOrderTypeId], [DrivingOrderTypeDesc]) VALUES (2, 'Odlazni')
GO

SET IDENTITY_INSERT [DrivingOrderType] OFF
GO

GO


