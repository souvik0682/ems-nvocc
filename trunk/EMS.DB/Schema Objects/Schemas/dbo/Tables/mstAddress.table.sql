CREATE TABLE [dbo].[mstAddress] (
    [fk_AddressID]      INT           IDENTITY (1, 1) NOT NULL,
    [fk_CompanyID]      INT           NOT NULL,
    [fk_LocationID]     INT           NOT NULL,
    [AddrName]          VARCHAR (60)  NOT NULL,
    [AddrAddress]       VARCHAR (500) NULL,
    [AddrSalutation]    VARCHAR (10)  NULL,
    [AddrType]          VARCHAR (5)   NOT NULL,
    [CFSCode]           VARCHAR (10)  NULL,
    [fk_terminalid]     INT           NULL,
    [AddrActive]        BIT           NOT NULL,
    [fk_UserAdded]      INT           NULL,
    [fk_UserLastEdited] INT           NULL,
    [AddedOn]           DATETIME      NULL,
    [EditedOn]          DATETIME      NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IC-Imp. Consignee / IN - Import Notify / OS -Overseas Agent / EY - Empty Yard / CF - CFS / IC - ICD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstAddress', @level2type = N'COLUMN', @level2name = N'AddrType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstAddress', @level2type = N'COLUMN', @level2name = N'fk_terminalid';

