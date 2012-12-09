CREATE TABLE [dbo].[mstExchangeRate] (
    [fk_ExchRateID]     INT             NOT NULL,
    [fk_CompanyID]      INT             NULL,
    [ExchDate]          DATE            NULL,
    [USDXchRate]        DECIMAL (12, 2) NULL,
    [fk_UserAdded]      INT             NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NULL,
    [EditedOn]          DATETIME        NULL
);

