USE [Liner]
GO

/****** Object:  Table [exp].[mstSlot]    Script Date: 05/01/2013 18:16:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [exp].[mstSlot](
	[pk_SlotID] [bigint] IDENTITY(1,1) NOT NULL,
	[fk_CompanyID] [int] NOT NULL,
	[fk_SlotOperatorID] [bigint] NOT NULL,
	[fk_LineID] [int] NOT NULL,
	[fk_POL] [bigint] NOT NULL,
	[fk_POD] [bigint] NOT NULL,
	[fk_MovOrigin] [int] NOT NULL,
	[fk_MovDest] [int] NOT NULL,
	[PODTerminal] [varchar](50) NULL,
	[EffDate] [date] NULL,
	[SlotStatus] [bit] NOT NULL,
	[dtAdded] [date] NOT NULL,
	[dtEdited] [date] NULL,
	[fk_UserAdded] [int] NOT NULL,
	[fk_UserEdited] [int] NULL,
 CONSTRAINT [PK_mstSlot] PRIMARY KEY CLUSTERED 
(
	[pk_SlotID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


