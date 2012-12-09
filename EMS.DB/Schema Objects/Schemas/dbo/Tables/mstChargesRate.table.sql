CREATE TABLE [dbo].[mstChargesRate] (
    [fk_ChargesID]         INT             NOT NULL,
    [pk_ChargesRateID]     INT             IDENTITY (1, 1) NOT NULL,
    [fk_LocationID]        BIGINT          NOT NULL,
    [fk_TerminalID]        INT             NULL,
    [RatePerBL]            DECIMAL (12, 2) NULL,
    [SlabLow]              INT             NULL,
    [SlabHigh]             INT             NULL,
    [RatePerTEU]           DECIMAL (12, 2) NULL,
    [RatePerFEU]           DECIMAL (12, 2) NULL,
    [RatePerCBM]           DECIMAL (12, 2) NULL,
    [RatePerTon]           DECIMAL (12, 2) NULL,
    [ServiceTaxApplicable] BIT             NULL,
    [RateActive]           BIT             NOT NULL,
    [PRatePerTEU]          DECIMAL (12, 2) NULL,
    [PRatePerFEU]          DECIMAL (12, 2) NULL,
    [PRatePerBL]           DECIMAL (12, 2) NULL
);

