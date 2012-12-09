CREATE TABLE [dbo].[mstCountry] (
    [CountryName]       VARCHAR (100) NULL,
    [CountryAbbr]       CHAR (2)      NULL,
    [CountryStatus]     BIT           NULL,
    [pk_countryid]      INT           IDENTITY (1, 1) NOT NULL,
    [fk_UserAdded]      INT           NULL,
    [fk_UserLastEdited] INT           NULL,
    [AddedOn]           DATETIME      NULL,
    [EditedOn]          DATETIME      NULL
);

