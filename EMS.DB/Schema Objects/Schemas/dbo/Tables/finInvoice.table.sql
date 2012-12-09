CREATE TABLE [dbo].[finInvoice] (
    [pk_InvoiceID]      BIGINT        NULL,
    [fk_LocationID]     INT           NULL,
    [fk_NVOCCID]        INT           NULL,
    [fk_InvoiceTypeID]  INT           NULL,
    [InvoiceNo]         VARCHAR (20)  NULL,
    [InvoiceDate]       DATE          NULL,
    [fk_CHAID]          INT           NULL,
    [Account]           VARCHAR (300) NULL,
    [fk_UserAdded]      INT           NOT NULL,
    [fk_UserLastEdited] INT           NULL,
    [AddedOn]           DATETIME      NOT NULL,
    [EditedOn]          DATETIME      NULL
);

