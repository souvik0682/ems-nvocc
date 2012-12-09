CREATE TABLE [dbo].[mstISO] (
    [pk_ISOID]          INT          IDENTITY (1, 1) NOT NULL,
    [ISOAbbr]           CHAR (4)     NULL,
    [ISOName]           VARCHAR (20) NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);

