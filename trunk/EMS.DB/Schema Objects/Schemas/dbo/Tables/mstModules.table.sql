CREATE TABLE [dbo].[mstModules] (
    [pk_Modules]   INT          IDENTITY (1, 1) NOT NULL,
    [ModuleName]   VARCHAR (50) NULL,
    [ModuleType]   CHAR (1)     NULL,
    [ModuleStatus] BIT          NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'E-Entry/P-Process/R-Report', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstModules', @level2type = N'COLUMN', @level2name = N'ModuleType';

