ALTER TABLE [dbo].[trnVoyage]
    ADD CONSTRAINT [DF_trn_Voyage_CrewList] DEFAULT ((1)) FOR [CrewList];

