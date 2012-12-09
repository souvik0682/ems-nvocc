CREATE TABLE [dbo].[expBLCharges] (
    [pk_ExpBLChgID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_ExpBLID]    BIGINT          NULL,
    [fk_ChargeID]   INT             NULL,
    [RatePerTEU]    DECIMAL (12, 2) NULL,
    [RatePerFEU]    DECIMAL (12, 2) NULL,
    [Currency]      CHAR (3)        NULL,
    [XChangeRate]   DECIMAL (12, 6) NULL,
    [ChgTotal]      DECIMAL (12, 2) NULL,
    [ToCalc]        CHAR (1)        NULL,
    [SortOrder]     INT             NULL,
    [CollPrep]      CHAR (1)        NULL
);

