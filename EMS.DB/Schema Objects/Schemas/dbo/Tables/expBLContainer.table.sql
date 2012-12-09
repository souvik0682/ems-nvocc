CREATE TABLE [dbo].[expBLContainer] (
    [pk_ExpBLContainerID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_ExpBLID]          BIGINT          NULL,
    [fj_ContainerID]      BIGINT          NULL,
    [SealNo]              VARCHAR (15)    NULL,
    [GrossWeight]         DECIMAL (12, 3) NULL,
    [TareWeight]          DECIMAL (12, 3) NULL,
    [Package]             INT             NULL
);

