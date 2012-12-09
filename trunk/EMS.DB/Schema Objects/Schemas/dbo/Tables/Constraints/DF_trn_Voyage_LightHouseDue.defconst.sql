ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_LightHouseDue] DEFAULT ((0)) FOR [LightHouseDue];

