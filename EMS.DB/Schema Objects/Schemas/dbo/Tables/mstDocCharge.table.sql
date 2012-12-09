CREATE TABLE [dbo].[mstDocCharge] (
    [fk_DocTypeID]      INT      NOT NULL,
    [fk_ChargesRateID]  INT      NOT NULL,
    [pk_DocTypeChg]     INT      IDENTITY (1, 1) NOT NULL,
    [Serial]            INT      NOT NULL,
    [DCRStatus]         BIT      NOT NULL,
    [fk_UserAdded]      INT      NULL,
    [fk_UserLastEdited] INT      NULL,
    [AddedOn]           DATETIME NULL,
    [EditedOn]          DATETIME NULL
);

