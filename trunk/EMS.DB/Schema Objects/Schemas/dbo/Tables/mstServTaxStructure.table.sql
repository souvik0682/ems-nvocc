CREATE TABLE [dbo].[mstServTaxStructure] (
    [pk_StaxID]         INT            NULL,
    [StartDate]         DATE           NULL,
    [TaxPer]            DECIMAL (6, 2) NULL,
    [TaxCess]           DECIMAL (6, 2) NULL,
    [TaxAddCess]        DECIMAL (6, 2) NULL,
    [TaxStatus]         BIT            NULL,
    [fk_UserAdded]      INT            NULL,
    [fk_UserLastEdited] INT            NULL,
    [AddedOn]           DATETIME       NULL,
    [EditedOn]          DATETIME       NULL
);

