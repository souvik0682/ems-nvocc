CREATE TABLE [dbo].[mstTerminal] (
    [fk_TerminalID]     INT          IDENTITY (1, 1) NOT NULL,
    [fk_LocationID]     INT          NOT NULL,
    [TerminalName]      VARCHAR (20) NOT NULL,
    [TerminalStatus]    BIT          NOT NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);

