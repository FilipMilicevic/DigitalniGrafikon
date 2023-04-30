USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[Vehicle]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [Vehicle]'
	DROP TABLE [Vehicle]
END
GO

PRINT 'CREATE Table [Vehicle]'
GO

CREATE TABLE [Vehicle](
	[VehicleId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] not null,
	[VehicleStatusId] [int] not null,
	[VehicleRent] [bit] null,
	[VehicleName] varchar(50) null,
	[VehicleClassId] int null,
	[VehicleModel] varchar(30) null,
	[VehicleColor] varchar(30) null,
	[VehicleYear] varchar(10) null, 
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [Vehicle] ADD  CONSTRAINT [Vehicle_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [Vehicle] ADD  CONSTRAINT [Vehicle_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [Vehicle] ADD  CONSTRAINT [Vehicle_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [Vehicle] ADD  CONSTRAINT [Vehicle_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO



GO
