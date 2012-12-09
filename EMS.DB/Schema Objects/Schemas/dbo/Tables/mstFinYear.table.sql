CREATE TABLE [dbo].[mstFinYear] (
    [pk_FinYearID] INT         IDENTITY (1, 1) NOT NULL,
    [FYStartDate]  DATE        NULL,
    [FYEndDate]    DATE        NULL,
    [FYAbbr]       VARCHAR (5) NULL
);

