CREATE TABLE [dbo].[mstRoles] (
    [pk_RoleID] INT          IDENTITY (1, 1) NOT NULL,
    [RoleName]  VARCHAR (50) NULL,
    [RollType]  CHAR (10)    NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'A-Admin / U-User/C-CFS/E-Empty Yard/Surveyors', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'mstRoles', @level2type = N'COLUMN', @level2name = N'RollType';

