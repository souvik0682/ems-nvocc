CREATE TABLE [dbo].[mstHaulageCharge] (
    [pk_HaulageChgID]   INT             IDENTITY (1, 1) NOT NULL,
    [fk_LocationFrom]   INT             NULL,
    [fk_LocationTo]     INT             NULL,
    [ContainerSize]     VARCHAR (2)     NULL,
    [WeightFrom]        DECIMAL (12, 3) NULL,
    [WeightTo]          DECIMAL (12, 3) NULL,
    [HaulageRate]       DECIMAL (12, 2) NULL,
    [HaulageStatus]     BIT             NULL,
    [fk_UserAdded]      INT             NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NULL,
    [EditedOn]          DATETIME        NULL
);

