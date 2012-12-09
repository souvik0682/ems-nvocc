CREATE TABLE [dbo].[eqpOnHire] (
    [pk_OnHireContID]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT          NULL,
    [fk_LocationID]     INT          NOT NULL,
    [fk_OnHireID]       BIGINT       NULL,
    [fk_ContainerID]    BIGINT       NULL,
    [fk_VesselID]       BIGINT       NULL,
    [fk_VoyageID]       BIGINT       NULL,
    [LinePrefix]        VARCHAR (20) NULL,
    [LGNo]              VARCHAR (40) NULL,
    [IGMNo]             BIGINT       NULL,
    [IGMDate]           DATE         NULL,
    [ActualOnHireDate]  DATE         NULL,
    [fk_MovementID]     INT          NULL,
    [StatusDate]        DATE         NULL,
    [fk_UserAdded]      INT          NOT NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NOT NULL,
    [EditedOn]          DATETIME     NULL
);

