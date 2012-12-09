CREATE TABLE [dbo].[mstDocumentType] (
    [pk_DocTypeID]      INT          IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT          NULL,
    [DocName]           VARCHAR (40) NULL,
    [DocHeading]        VARCHAR (40) NULL,
    [DocType]           CHAR (1)     NULL,
    [Calculation]       CHAR (1)     NULL,
    [SeparateonNVOCC]   BIT          NULL,
    [DocAbbr]           VARCHAR (4)  NULL,
    [ResetPeriod]       CHAR (1)     NULL,
    [DocStatus]         BIT          NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'I - Import / E-Export', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstDocumentType', @level2type = N'COLUMN', @level2name = N'DocType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'P-Posiitive / N-Negative', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstDocumentType', @level2type = N'COLUMN', @level2name = N'Calculation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'M-Monthly / Y-Yearly / D-Daily', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstDocumentType', @level2type = N'COLUMN', @level2name = N'ResetPeriod';

