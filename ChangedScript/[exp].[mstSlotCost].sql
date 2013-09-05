USE [Liner]
GO

/****** Object:  Table [exp].[mstSlotCost]    Script Date: 05/01/2013 21:16:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [exp].[mstSlotCost](
	[pk_SlotCostID] [bigint] IDENTITY(1,1) NOT NULL,
	[fk_SlotID] [bigint] NULL,
	[CargoType] [varchar](1) NULL,
	[CntrSize] [char](2) NULL,
	[fk_ContainerType] [int] NULL,
	[SpecialType] [varchar](1) NULL,
	[ContainerRate] [decimal](12, 2) NULL,
	[RatePerTon] [decimal](12, 2) NULL,
	[RatePerCBM] [decimal](12, 2) NULL,
	[SlotCostStatus] [bit] NULL,
 CONSTRAINT [PK_mstSlotCost] PRIMARY KEY CLUSTERED 
(
	[pk_SlotCostID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'F-FCL / E-ETY / B-Bulk' , @level0type=N'SCHEMA',@level0name=N'exp', @level1type=N'TABLE',@level1name=N'mstSlotCost', @level2type=N'COLUMN',@level2name=N'CargoType'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'G-Gen / H-Haz / R-Reefer' , @level0type=N'SCHEMA',@level0name=N'exp', @level1type=N'TABLE',@level1name=N'mstSlotCost', @level2type=N'COLUMN',@level2name=N'SpecialType'
GO


