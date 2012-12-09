CREATE TABLE [dbo].[ImpBLFooter] (
    [fk_BLID]            BIGINT          NULL,
    [pk_BLFooterID]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [CntrNo]             VARCHAR (11)    NOT NULL,
    [CntrSize]           CHAR (2)        NOT NULL,
    [fk_ContainerTypeID] INT             NOT NULL,
    [SealNo]             VARCHAR (15)    NOT NULL,
    [Comodity]           VARCHAR (20)    NOT NULL,
    [GrossWeight]        DECIMAL (13, 3) NOT NULL,
    [Package]            INT             NOT NULL,
    [CargoWtTon]         DECIMAL (12, 3) NOT NULL,
    [fk_ISOID]           INT             NOT NULL,
    [SOC]                BIT             NOT NULL,
    [Temperature]        DECIMAL (6, 2)  NULL,
    [TempUnit]           CHAR (1)        NULL,
    [CustomSeal]         VARCHAR (15)    NULL,
    [CAgent]             VARCHAR (150)   NULL,
    [TempMax]            DECIMAL (6, 2)  NULL,
    [TempMin]            DECIMAL (6, 2)  NULL,
    [PCSTemp]            CHAR (3)        NULL,
    [DIMCode]            VARCHAR (3)     NULL,
    [ODLength]           DECIMAL (12, 2) NULL,
    [ODWidth]            DECIMAL (12, 2) NULL,
    [ODHeight]           DECIMAL (12, 2) NULL,
    [Cargo]              CHAR (3)        NULL,
    [Stowage]            VARCHAR (6)     NULL,
    [CallNo]             CHAR (1)        NULL,
    [IMCO]               VARCHAR (4)     NULL,
    [fk_MovementID]      INT             NULL,
    [DateLastMoved]      DATE            NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'for reefer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLFooter', @level2type = N'COLUMN', @level2name = N'Temperature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'C-Centegrate / F-farenhight', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ImpBLFooter', @level2type = N'COLUMN', @level2name = N'TempUnit';

