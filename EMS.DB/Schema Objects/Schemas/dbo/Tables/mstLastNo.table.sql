CREATE TABLE [dbo].[mstLastNo] (
    [pk_DocNoID]    INT    IDENTITY (1, 1) NOT NULL,
    [fk_LocationID] INT    NOT NULL,
    [fk_DocTypeID]  INT    NULL,
    [fk_NVOCCID]    INT    NULL,
    [fk_FinYearID]  INT    NULL,
    [LastNo]        BIGINT NULL
);

