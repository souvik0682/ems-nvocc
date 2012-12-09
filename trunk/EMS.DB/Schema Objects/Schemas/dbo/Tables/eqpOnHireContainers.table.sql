CREATE TABLE [dbo].[eqpOnHireContainers] (
    [fk_NVOCCID]           INT          NULL,
    [pk_OnHireContainerID] BIGINT       IDENTITY (1, 1) NOT NULL,
    [fk_OnHireID]          BIGINT       NULL,
    [OnHireReference]      VARCHAR (20) NULL,
    [OnHireDate]           DATE         NULL,
    [ValidTill]            DATE         NULL,
    [fk_ReturnPortID]      BIGINT       NULL,
    [fk_ContainerTypeID]   INT          NULL,
    [Total20]              INT          NULL,
    [Total40]              INT          NULL,
    [fk_UserAdded]         INT          NOT NULL,
    [fk_UserLastEdited]    INT          NULL,
    [AddedOn]              DATETIME     NOT NULL,
    [EditedOn]             DATETIME     NULL
);

