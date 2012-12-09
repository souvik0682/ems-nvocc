CREATE TABLE [dbo].[trnRepairing] (
    [fk_ContainerID]    BIGINT          NULL,
    [fk_BLID]           BIGINT          NULL,
    [RepairingEstimate] VARCHAR (20)    NULL,
    [LabourEstimate]    DECIMAL (12, 2) NULL,
    [MaterialEstimate]  DECIMAL (12, 2) NULL,
    [WashingEstimate]   DECIMAL (12, 2) NULL,
    [LabourToCharge]    DECIMAL (12, 2) NULL,
    [MaterialToCharge]  DECIMAL (12, 2) NULL,
    [WashingToCharge]   DECIMAL (12, 2) NULL,
    [LabourApproved]    DECIMAL (12, 2) NULL,
    [MaterialApproved]  DECIMAL (12, 2) NULL,
    [WashingApproved]   DECIMAL (12, 2) NULL,
    [fk_ApprovedUser]   INT             NULL,
    [RepairingTax]      DECIMAL (12, 2) NULL,
    [RepairingBillNo]   VARCHAR (20)    NULL,
    [RepairingBillDate] DATE            NULL,
    [PRDetention]       DECIMAL (12, 2) NULL,
    [fk_UserAdded]      INT             NOT NULL,
    [fk_UserLastEdited] INT             NULL,
    [AddedOn]           DATETIME        NOT NULL,
    [EditedOn]          DATETIME        NULL
);

