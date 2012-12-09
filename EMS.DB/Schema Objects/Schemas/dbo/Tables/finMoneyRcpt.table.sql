CREATE TABLE [dbo].[finMoneyRcpt] (
    [fk_InvoiceID]      BIGINT          NULL,
    [fk_LocationID]     INT             NULL,
    [fk_NVOCCID]        INT             NULL,
    [pk_MoneyRcptID]    BIGINT          NOT NULL,
    [MRNo]              VARCHAR (40)    NULL,
    [MRDate]            DATE            NULL,
    [CashPayment]       DECIMAL (14, 2) NULL,
    [ChequePayment]     DECIMAL (14, 2) NULL,
    [ChequeNo]          VARCHAR (6)     NULL,
    [ChequeDate]        DATE            NULL,
    [ChequeBank]        VARCHAR (50)    NULL,
    [TDSDeducted]       DECIMAL (14, 2) NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);

