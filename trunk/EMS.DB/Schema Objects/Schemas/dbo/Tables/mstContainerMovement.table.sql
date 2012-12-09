CREATE TABLE [dbo].[mstContainerMovement] (
    [pk_MovementID]     INT          IDENTITY (1, 1) NOT NULL,
    [MoveAbbr]          VARCHAR (4)  NULL,
    [MoveDescr]         VARCHAR (25) NULL,
    [MoveSeq]           INT          NULL,
    [MoveStatus]        BIT          NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);

