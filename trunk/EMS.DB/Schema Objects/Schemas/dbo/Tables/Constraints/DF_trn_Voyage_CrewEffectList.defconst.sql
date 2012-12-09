ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_CrewEffectList] DEFAULT ((1)) FOR [CrewEffectList];

