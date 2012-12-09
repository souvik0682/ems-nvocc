CREATE TABLE [dbo].[mstLinkRoleModule] (
    [pk_LinkID]   INT NULL,
    [fk_ModuleID] INT NULL,
    [fk_RoleID]   INT NULL,
    [InsRec]      BIT NULL,
    [EditRec]     BIT NULL,
    [DelRec]      BIT NULL,
    [ViewRec]     BIT NULL
);

