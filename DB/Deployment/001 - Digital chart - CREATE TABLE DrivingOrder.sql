USE [GVDB]
GO
 
SET ANSI_NULLS ON
GO
SET ANSI_PADDING ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM [dbo].[sysobjects] WHERE [id] = OBJECT_ID('[DrivingOrder]') AND [type] = 'U')
BEGIN
	PRINT 'DROP TABLE [DrivingOrder]'
	DROP TABLE [DrivingOrder]
END
GO

PRINT 'CREATE Table [DrivingOrder]'
GO

CREATE TABLE [DrivingOrder](
	[DrivingOrderId] [int] IDENTITY(1,1) NOT NULL,
	[RegionalCenter] varchar(50) null,
	[DrivingOrderDate] datetime null,
	[DrivingOrderTypeId] [int] null,
	[Pickup] [bit] null,
	[Private] [bit] null,
	[FlightCode] varchar(20) null,
	[FlightName] varchar(100) null,
	[OrderingPartnerId] [int] null,
	[OrderingPartnerName] varchar(100) null,
	[BillingPartner] varchar(100) null,
	[TransferCode] varchar(20) null,
	[TransferMain] varchar(100) null,
	[TransferDeparture] varchar(100) null,
	[TransferDestination] varchar(100) null,
	[PaxAdult] int null,
	[PaxChild] int null,
	[PaxInfant] int null,
	[PaxOutOfCost] int null, 
	[PaxTotal] int null,
	[PaxName] varchar(50) null,
	[PaxSurname] varchar(50) null,
	[DrivingOrderCode] varchar(20) null,
	[DrivingOrderNumber] int null,
	[DispatcherRemark] varchar(MAX) null,
	[DriverRemark] varchar(MAX) null,
	[VehicleId] int null,
	[KilometersAmount] decimal null,
	[Created] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[LastUpd] [datetime] NULL,
	[LastUpdBy] [varchar](50) NULL,
	[LastUpdApp] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DrivingOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [DrivingOrder] ADD  CONSTRAINT [DrivingOrder_Created]  DEFAULT (getdate()) FOR [Created]
GO

ALTER TABLE [DrivingOrder] ADD  CONSTRAINT [DrivingOrder_CreatedBy]  DEFAULT (suser_sname()) FOR [CreatedBy]
GO

ALTER TABLE [DrivingOrder] ADD  CONSTRAINT [DrivingOrder_LastUpd]  DEFAULT (getdate()) FOR [LastUpd]
GO

ALTER TABLE [DrivingOrder] ADD  CONSTRAINT [DrivingOrder_LastUpdBy]  DEFAULT (suser_sname()) FOR [LastUpdBy]
GO



GO
