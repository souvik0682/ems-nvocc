CREATE TABLE [dbo].[mstCompany] (
    [pk_CompanyID]     INT           IDENTITY (1, 1) NOT NULL,
    [CompName]         VARCHAR (60)  NOT NULL,
    [CompAbbr]         VARCHAR (5)   NOT NULL,
    [CompAddress]      VARCHAR (300) NOT NULL,
    [CompCity]         VARCHAR (20)  NULL,
    [CompPIN]          VARCHAR (10)  NULL,
    [ActivityNature]   CHAR (1)      NULL,
    [PANNo]            VARCHAR (20)  NULL,
    [ICEGateLogin]     VARCHAR (20)  NOT NULL,
    [InstType]         NCHAR (10)    NULL,
    [CustHouseCode]    VARCHAR (20)  NULL,
    [PCSLogin]         VARCHAR (20)  NULL,
    [ShippingLineCode] VARCHAR (10)  NULL,
    [GateWayPort]      VARCHAR (6)   NULL,
    [BankDetail]       VARCHAR (300) NULL,
    [CompStatus]       BIT           NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'B-Bulk / C-Container', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstCompany', @level2type = N'COLUMN', @level2name = N'ActivityNature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'I-Import / E-Export / E-Equipment / A-All', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstCompany', @level2type = N'COLUMN', @level2name = N'InstType';

