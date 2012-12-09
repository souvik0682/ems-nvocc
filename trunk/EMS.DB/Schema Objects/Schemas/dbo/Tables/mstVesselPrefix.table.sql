CREATE TABLE [dbo].[mstVesselPrefix] (
    [fk_VesselPrefixID] INT          IDENTITY (1, 1) NOT NULL,
    [VesselPrefix]      VARCHAR (10) NULL,
    [fk_UserAdded]      INT          NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NULL,
    [EditedOn]          DATETIME     NULL
);

