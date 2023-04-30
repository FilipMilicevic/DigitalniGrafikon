USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[VehicleClass]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [VehicleClass]'
	DROP TABLE [VehicleClass]
END
GO

PRINT 'CREATE Table [VehicleClass]'
GO

CREATE TABLE [VehicleClass](
	[VehicleClassId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleClassName] varchar(50) null,
	[VehicleSeatNumber] int null,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [VehicleClass] ADD  CONSTRAINT [VehicleClass_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [VehicleClass] ADD  CONSTRAINT [VehicleClass_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [VehicleClass] ADD  CONSTRAINT [VehicleClass_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [VehicleClass] ADD  CONSTRAINT [VehicleClass_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO



GO
