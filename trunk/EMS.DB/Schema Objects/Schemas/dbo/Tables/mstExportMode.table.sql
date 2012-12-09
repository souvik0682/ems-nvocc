CREATE TABLE [dbo].[mstExportMode] (
    [fk_ModeID]         INT          IDENTITY (1, 1) NOT NULL,
    [ModeName]          VARCHAR (10) NULL,
    [ModeStatus]        BIT          NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);

