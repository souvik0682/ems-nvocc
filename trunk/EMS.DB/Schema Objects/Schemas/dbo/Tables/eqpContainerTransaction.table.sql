CREATE TABLE [dbo].[eqpContainerTransaction] (
    [pk_ContainerID]        BIGINT     IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]          INT        NOT NULL,
    [fk_LocationID]         NCHAR (10) NOT NULL,
    [fk_MovementGroup]      BIGINT     NOT NULL,
    [fk_MovementID]         INT        NOT NULL,
    [MovementDate]          DATE       NOT NULL,
    [fk_StockLocationID]    INT        NOT NULL,
    [fk_TransferLocationID] INT        NULL,
    [fk_AddressID]          INT        NULL,
    [fk_UserAdded]          INT        NOT NULL,
    [fk_UserLastEdited]     INT        NULL,
    [AddedOn]               DATETIME   NOT NULL,
    [EditedOn]              DATETIME   NULL
);

