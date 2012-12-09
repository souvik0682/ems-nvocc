CREATE TABLE [dbo].[trnVessel] (
    [fk_CompanyID]      INT          NOT NULL,
    [pk_VesselID]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [fk_VesselPrefixID] INT          NOT NULL,
    [VesselName]        VARCHAR (60) NOT NULL,
    [CallSign]          VARCHAR (14) NULL,
    [IMONumber]         VARCHAR (14) NULL,
    [fk_CountryId]      INT          NULL,
    [ShippingLineCode]  VARCHAR (10) NULL,
    [AgentCode]         VARCHAR (10) NULL,
    [MasterName]        VARCHAR (50) NULL,
    [fk_PortID]         BIGINT       NULL,
    [dtAdded]           DATE         NOT NULL,
    [dtEdited]          DATE         NULL,
    [fk_UserAdded]      BIGINT       NOT NULL,
    [fk_UserEdited]     BIGINT       NULL,
    [VesselStatus]      BIT          NULL,
    [fk_locationid]     INT          NOT NULL
);

