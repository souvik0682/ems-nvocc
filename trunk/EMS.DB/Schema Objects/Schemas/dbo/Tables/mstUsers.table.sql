CREATE TABLE [dbo].[mstUsers] (
    [pk_UserID]        INT          IDENTITY (1, 1) NOT NULL,
    [UserName]         VARCHAR (10) NOT NULL,
    [Password]         VARCHAR (50) NOT NULL,
    [FirstName]        VARCHAR (30) NOT NULL,
    [LastName]         VARCHAR (30) NOT NULL,
    [fk_RoleID]        INT          NOT NULL,
    [fk_LocID]         INT          NOT NULL,
    [emailID]          VARCHAR (50) NULL,
    [UserActive]       BIT          NOT NULL,
    [AllowMultipleLoc] BIT          NOT NULL,
    [IsDeleted]        BIT          NOT NULL
);

