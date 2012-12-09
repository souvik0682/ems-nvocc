CREATE TABLE [dbo].[expBookingCharges] (
    [pk_BookingChgID] BIGINT          NOT NULL,
    [fk_BookingID]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [fk_ChargeID]     INT             NULL,
    [ActRatePerTEU]   DECIMAL (12, 2) NULL,
    [ActRatePerFEU]   DECIMAL (12, 2) NULL,
    [MnftRatePerTEU]  DECIMAL (12, 2) NULL,
    [MnftRatePerFEU]  DECIMAL (12, 2) NULL
);

