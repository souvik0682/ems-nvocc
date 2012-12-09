CREATE TABLE [dbo].[eqpPickUp] (
    [fk_PickUpID]       BIGINT       NOT NULL,
    [fk_CompanyID]      INT          NOT NULL,
    [fk_LocationID]     INT          NOT NULL,
    [fk_NVOCCID]        INT          NOT NULL,
    [fk_FinYearID]      INT          NOT NULL,
    [PickUpNo]          VARCHAR (60) NULL,
    [IssueDate]         DATE         NULL,
    [ValidTill]         DATE         NULL,
    [fk_RelatedRefNo]   INT          NULL,
    [fk_BookingRef]     VARCHAR (50) NULL,
    [fk_EmptyYard]      INT          NULL,
    [Feeder]            VARCHAR (40) NULL,
    [Cargo]             VARCHAR (40) NULL,
    [PickActive]        BIT          NULL,
    [fk_UserAdded]      INT          NOT NULL,
    [fk_UserLastEdited] INT          NULL,
    [AddedOn]           DATETIME     NOT NULL,
    [EditedOn]          DATETIME     NULL
);

