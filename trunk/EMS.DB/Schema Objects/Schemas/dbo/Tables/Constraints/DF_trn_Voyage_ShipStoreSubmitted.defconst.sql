ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_ShipStoreSubmitted] DEFAULT ((1)) FOR [ShipStoreSubmitted];

