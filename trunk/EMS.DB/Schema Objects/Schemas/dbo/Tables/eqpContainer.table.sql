﻿CREATE TABLE [dbo].[eqpContainer] (
    [pk_ContainerID]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT             NULL,
    [ContainerNo]       VARCHAR (11)    NOT NULL,
    [fk_BLID]           BIGINT          NOT NULL,
    [fk_ImpVesselID]    BIGINT          NOT NULL,
    [fk_ImpVoyageID]    BIGINT          NOT NULL,
    [fk_LocationID]     INT             NULL,
    [GLDate]            DATETIME        NULL,
    [fk_ICDLocation]    INT             NULL,
    [ICDOutDate]        DATE            NULL,
    [ContainerSize]     CHAR (1)        NULL,
    [fk_ContainerType]  INT             NULL,
    [ImportBerth]       CHAR (3)        NULL,
    [Comodity]          VARCHAR (20)    NULL,
    [ContHTFT]          INT             NULL,
    [ContHTIN]          INT             NULL,
    [GrossWt]           DECIMAL (14, 4) NULL,
    [TareWt]            DECIMAL (14, 4) NULL,
    [SealNo]            VARCHAR (15)    NULL,
    [Package]           INT             NULL,
    [CagEnc]            VARCHAR (10)    NULL,
    [Status]            CHAR (3)        NULL,
    [ISO]               CHAR (1)        NULL,
    [SOC]               CHAR (1)        NULL,
    [DestuffDate]       DATETIME        NULL,
    [PassOutDate]       DATETIME        NULL,
    [PortDetention]     DECIMAL (12, 2) NULL,
    [Waiver]            DECIMAL (6, 2)  NULL,
    [YardReturnDate]    DATETIME        NULL,
    [Detention]         DECIMAL (12, 2) NULL,
    [DetentionWith]     DECIMAL (12, 2) NULL,
    [DetentionWO]       DECIMAL (12, 2) NULL,
    [DetentionPercent]  DECIMAL (6, 2)  NULL,
    [ISSShpr]           DATE            NULL,
    [RemDock]           DATE            NULL,
    [ExpectedDate]      DATE            NULL,
    [CFSDestuff]        CHAR (1)        NULL,
    [LeftBehind]        CHAR (1)        NULL,
    [OnHireDate]        DATE            NULL,
    [OnhireRef]         VARCHAR (20)    NULL,
    [OffHireDate]       DATE            NULL,
    [OffHireReference]  VARCHAR (30)    NULL,
    [ToSkip]            CHAR (1)        NULL,
    [EBLNo]             VARCHAR (20)    NULL,
    [Booking]           VARCHAR (10)    NULL,
    [ExpBerth]          CHAR (3)        NULL,
    [fk_ExpVesselID]    BIGINT          NULL,
    [fk_ExpVoyageID]    BIGINT          NULL,
    [SOB]               DATE            NULL,
    [Active]            CHAR (1)        NULL,
    [XchangeRate]       DECIMAL (12, 6) NULL,
    [fk_DeliveryPortID] BIGINT          NULL,
    [NOMVsl]            VARCHAR (20)    NULL,
    [fk_LoadPortID]     BIGINT          NULL,
    [RepCharges]        DECIMAL (12, 2) NULL,
    [RepTax]            DECIMAL (12, 2) NULL,
    [PRDetention]       DECIMAL (12, 2) NULL,
    [Damage]            CHAR (1)        NULL,
    [OnHold]            VARCHAR (5)     NULL,
    [PickNo]            VARCHAR (6)     NULL,
    [PickDate]          DATE            NULL,
    [PickValid]         DATE            NULL,
    [LGNo]              VARCHAR (20)    NULL,
    [PortBill]          VARCHAR (10)    NULL,
    [PortGLD]           DATE            NULL,
    [PortOutDate]       DATE            NULL,
    [PortXchangeRate]   DECIMAL (12, 6) NULL,
    [PortUSD]           DECIMAL (12, 2) NULL,
    [PortCharge1]       DECIMAL (12, 2) NULL,
    [PortCharge2]       DECIMAL (12, 2) NULL,
    [LockCCB]           BIT             NULL,
    [fk_PortLoadingID]  BIGINT          NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);

