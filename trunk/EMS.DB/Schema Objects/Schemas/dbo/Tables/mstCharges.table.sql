CREATE TABLE [dbo].[mstCharges] (
    [pk_ChargesID]      INT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT          NULL,
    [ChargeDescr]       VARCHAR (40) NULL,
    [ChargeType]        CHAR (1)     NULL,
    [IEC]               CHAR (1)     NULL,
    [fk_NVOCCID]        INT          NULL,
    [Sequence]          INT          NULL,
    [RateChangeable]    BIT          NULL,
    [ChargeActive]      BIT          NOT NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL,
    [freightcomponent]  BIT          NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'I-Import / E-Export / C-COMMON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstCharges', @level2type = N'COLUMN', @level2name = N'IEC';

