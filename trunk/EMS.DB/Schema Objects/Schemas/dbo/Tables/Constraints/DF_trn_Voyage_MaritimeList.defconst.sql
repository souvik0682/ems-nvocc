ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_MaritimeList] DEFAULT ((1)) FOR [MaritimeList];

