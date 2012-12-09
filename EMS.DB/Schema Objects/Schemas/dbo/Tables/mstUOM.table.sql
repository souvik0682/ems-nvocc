CREATE TABLE [dbo].[mstUOM] (
    [pk_UOMId]      INT          IDENTITY (1, 1) NOT NULL,
    [UnitCode]      VARCHAR (3)  NOT NULL,
    [UnitName]      VARCHAR (30) NOT NULL,
    [UnitType]      VARCHAR (1)  NOT NULL,
    [dtAdded]       DATE         NOT NULL,
    [dtEdited]      DATE         NULL,
    [fk_UserAdded]  BIGINT       NOT NULL,
    [fk_UserEdited] BIGINT       NULL,
    [UnitStatus]    BIT          NULL,
    [DefaultValue]  BIT          NULL
);

