CREATE TABLE [dbo].[ImpBLHeader] (
    [fk_CompanyID]           INT             NOT NULL,
    [fk_LocationID]          INT             NOT NULL,
    [pk_BLID]                BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_NVOCCID]             INT             NULL,
    [ImpBLNo]                VARCHAR (60)    NOT NULL,
    [ImpBLDate]              DATETIME        NOT NULL,
    [fk_ImpVesselID]         BIGINT          NOT NULL,
    [fk_ImpVoyageID]         BIGINT          NOT NULL,
    [ItemLinePrefix]         VARCHAR (8)     NULL,
    [ItemLineNo]             VARCHAR (6)     NULL,
    [MBLNumber]              VARCHAR (60)    NULL,
    [MBLDate]                DATE            NOT NULL,
    [MotherVessel]           VARCHAR (300)   NULL,
    [fk_BLIssuePortID]       BIGINT          NOT NULL,
    [fk_PlaceReceipt]        BIGINT          NOT NULL,
    [fk_PortLoading]         BIGINT          NOT NULL,
    [fk_PortDischarge]       BIGINT          NOT NULL,
    [fk_PlaceDelivery]       BIGINT          NOT NULL,
    [fk_FinalDestination]    BIGINT          NOT NULL,
    [fk_StockLocationID]     BIGINT          NULL,
    [TranShipment]           VARCHAR (500)   NULL,
    [CargoNature]            VARCHAR (2)     NOT NULL,
    [CargoMovement]          VARCHAR (2)     NOT NULL,
    [ItemType]               VARCHAR (2)     NOT NULL,
    [CargoType]              CHAR (1)        NOT NULL,
    [GrossWeight]            DECIMAL (16, 3) NULL,
    [fk_UnitOfWeight]        INT             NULL,
    [Volume]                 DECIMAL (16, 3) NULL,
    [fk_UnitOfVolume]        INT             NULL,
    [NumberPackage]          BIGINT          NOT NULL,
    [fk_UnitPackage]         INT             NOT NULL,
    [PackageDetail]          VARCHAR (30)    NULL,
    [HazFlag]                BIT             NOT NULL,
    [UNOCode]                VARCHAR (5)     NOT NULL,
    [IMOCode]                VARCHAR (3)     NOT NULL,
    [ShipperInformation]     VARCHAR (500)   NULL,
    [ConsigneeInformation]   VARCHAR (500)   NOT NULL,
    [NotifyPartyInformation] VARCHAR (500)   NOT NULL,
    [MarksNumbers]           VARCHAR (500)   NOT NULL,
    [GoodDescription]        VARCHAR (500)   NOT NULL,
    [fk_AddressCFSId]        INT             NOT NULL,
    [TPBondNo]               VARCHAR (10)    NULL,
    [CACode]                 VARCHAR (10)    NULL,
    [fk_AddressCHAID]        INT             NULL,
    [TransportMode]          VARCHAR (1)     NOT NULL,
    [MLOCode]                VARCHAR (16)    NULL,
    [DOLock]                 BIT             NOT NULL,
    [DOLockReason]           VARCHAR (100)   NULL,
    [DetentionFree]          INT             NOT NULL,
    [DetentionSlabType]      CHAR (1)        NULL,
    [PGR_FreeDays]           INT             NOT NULL,
    [FreightType]            CHAR (2)        NOT NULL,
    [FreigthToCollect]       DECIMAL (12, 2) NULL,
    [FreeOut]                BIT             NOT NULL,
    [PartBL]                 BIT             NOT NULL,
    [HookPoint]              BIT             NOT NULL,
    [TaxExemption]           BIT             NOT NULL,
    [Reefer]                 BIT             NOT NULL,
    [ONBR]                   BIT             NULL,
    [DocFact]                BIT             NOT NULL,
    [dtAdded]                DATETIME        NOT NULL,
    [dtEdited]               DATETIME        NULL,
    [fk_UserAdded]           BIGINT          NOT NULL,
    [fk_UserEdited]          BIGINT          NULL,
    [RsStatus]               BIT             NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default will be BLNo with user option to change', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'MBLNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default will be BL Date with user option to change', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'MBLDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DEFAULT CURRENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'fk_StockLocationID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Generally C but can be DB or LB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'CargoNature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'LC /  TC / IT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'CargoMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Generally OT but can be GC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'ItemType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'L-LCL / F-FCL / E-ETY', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'CargoType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When Haz. Cargo=0, it will be ZZZZZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'UNOCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'When Haz. Cargo="N", it will be ZZZ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'IMOCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Default will be N/M (No Mark)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'MarksNumbers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For CFS from address master', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'fk_AddressCFSId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'R-Regular / O-Override', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'DetentionSlabType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PP - Pre Paid / CC - To collect', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'FreightType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'For CC this should be in USD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLHeader', @level2type = N'COLUMN', @level2name = N'FreigthToCollect';

