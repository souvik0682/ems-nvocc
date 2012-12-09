CREATE TABLE [dbo].[eqpRepairing] (
    [pk_RepairID]       BIGINT          NULL,
    [fk_ContainerID]    INT             NULL,
    [RepMaterialEst]    DECIMAL (12, 2) NULL,
    [RepLabourEst]      DECIMAL (12, 2) NULL,
    [RepMaterialAppr]   DECIMAL (12, 2) NULL,
    [RepLabourAppr]     DECIMAL (12, 2) NULL,
    [RepMaterialBilled] DECIMAL (12, 2) NULL,
    [RepLabourBilled]   DECIMAL (12, 2) NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);

