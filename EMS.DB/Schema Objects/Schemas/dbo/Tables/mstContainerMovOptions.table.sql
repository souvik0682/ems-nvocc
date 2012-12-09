CREATE TABLE [dbo].[mstContainerMovOptions] (
    [pk_MovmentOptID] INT IDENTITY (1, 1) NOT NULL,
    [fk_CurrMoveID]   INT NOT NULL,
    [fk_AvlMoveID]    INT NOT NULL
);

