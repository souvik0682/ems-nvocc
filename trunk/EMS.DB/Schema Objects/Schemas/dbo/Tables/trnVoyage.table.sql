CREATE TABLE [dbo].[trnVoyage] (
    [pk_VoyageID]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]       INT             NOT NULL,
    [fk_VesselID]        BIGINT          NOT NULL,
    [fl_TerminalID]      INT             NULL,
    [VoyageNo]           VARCHAR (10)    NOT NULL,
    [IGMNo]              VARCHAR (10)    NULL,
    [IGMDate]            DATE            NULL,
    [LandingDate]        DATE            NULL,
    [AddLandingDate]     DATE            NULL,
    [VoyageType]         CHAR (1)        NOT NULL,
    [LGNo]               VARCHAR (40)    NULL,
    [AltLGNo]            VARCHAR (40)    NULL,
    [fk_LPortID]         BIGINT          NULL,
    [fk_LPortID1]        BIGINT          NULL,
    [fk_LPortID2]        BIGINT          NULL,
    [VesselType]         VARCHAR (1)     NULL,
    [MotherDaughterDtl]  VARCHAR (500)   NULL,
    [TotalLines]         VARCHAR (5)     NULL,
    [CargoDesc]          VARCHAR (50)    NOT NULL,
    [ETADate]            DATETIME        NULL,
    [ETATime]            TIME (7)        NULL,
    [LightHouseDue]      INT             NULL,
    [SameButtonCargo]    BIT             NOT NULL,
    [ShipStoreSubmitted] BIT             NOT NULL,
    [CrewList]           BIT             NOT NULL,
    [PassengerList]      BIT             NOT NULL,
    [CrewEffectList]     BIT             NOT NULL,
    [MaritimeList]       BIT             NOT NULL,
    [CallSign]           VARCHAR (7)     NULL,
    [ImpXChangeRate]     DECIMAL (12, 7) NULL,
    [PCCNo]              VARCHAR (10)    NULL,
    [PCCDate]            DATE            NULL,
    [VIANo]              VARCHAR (10)    NULL,
    [VCN]                VARCHAR (14)    NULL,
    [SailDate]           DATE            NULL,
    [ETD]                DATE            NULL,
    [ExpXchangeRate]     DECIMAL (12, 7) NULL,
    [CutOffDate]         DATE            NULL,
    [EGMNo]              BIGINT          NULL,
    [EGMDate]            DATE            NULL,
    [BondNo]             VARCHAR (10)    NULL,
    [VesselApplNo]       VARCHAR (7)     NULL,
    [VesselAppDate]      DATE            NULL,
    [BondAmount]         DECIMAL (10, 2) NULL,
    [BondBalance]        DECIMAL (10, 2) NULL,
    [VesselSerial]       VARCHAR (6)     NULL,
    [dtAdded]            DATE            NULL,
    [dtEdited]           DATE            NULL,
    [fk_UserAdded]       BIGINT          NULL,
    [fk_UserEdited]      BIGINT          NULL,
    [VoyageStatus]       BIT             NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'C - With Cargo / E - Empty', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'VesselType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No (Default) / 1-Yes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'LightHouseDue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 (Default) - No / 1 - Yes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'SameButtonCargo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - No / 1 - Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'ShipStoreSubmitted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / 1-Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'CrewList';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0 - No (Default) / 1 - Yes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'PassengerList';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / Y-Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'CrewEffectList';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / 1-Yes (Default)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'trnVoyage', @level2type = N'COLUMN', @level2name = N'MaritimeList';

