CREATE TABLE [dbo].[mstContainerType] (
    [pk_ContainerTypeID] INT          IDENTITY (1, 1) NOT NULL,
    [ContainerAbbr]      VARCHAR (2)  NULL,
    [CotainerDesc]       VARCHAR (20) NULL,
    [TareWeight20]       INT          NULL,
    [TareWeight40]       INT          NULL,
    [CntrHTFT]           INT          NULL,
    [CntrHTIN]           INT          NULL,
    [ISO20]              VARCHAR (4)  NULL,
    [ISO40]              VARCHAR (4)  NULL,
    [fk_UserAdded]       INT          NULL,
    [fk_UserLastEdited]  INT          NULL,
    [AddedOn]            DATETIME     NULL,
    [EditedOn]           DATETIME     NULL
);

