CREATE TABLE [dbo].[mstPort] (
    [pk_PortID]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [PortCode]      VARCHAR (6)  NOT NULL,
    [PortName]      VARCHAR (30) NOT NULL,
    [ICDIndicator]  BIT          NOT NULL,
    [dtAdded]       DATE         NOT NULL,
    [dtEdited]      DATE         NULL,
    [fk_UserAdded]  BIGINT       NOT NULL,
    [fk_UserEdited] BIGINT       NULL,
    [RStatus]       BIT          NULL,
    [PortAddressee] VARCHAR (50) NULL,
    [Address2]      VARCHAR (50) NULL,
    [Address3]      VARCHAR (50) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0-No / 1- Yes  (For India port only where Left(IGCode,2)=''IN''', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstPort', @level2type = N'COLUMN', @level2name = N'ICDIndicator';

